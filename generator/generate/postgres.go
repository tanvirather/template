package generate

import (
	"fmt"
	"log"
	"os"

	"zuhid.com/generator/models"
	"zuhid.com/generator/tools"
)

// Postgres represents a PostgreSQL code generator.
type Postgres struct {
	InputPath  string
	OutputPath string
}

// Generate generates PostgreSQL code based on the provided table data.
func (postgres *Postgres) Generate(tables []models.Table) error {

	if err := os.RemoveAll(postgres.OutputPath); err != nil {
		log.Fatalf("Error deleting output folder: %v", err)
	}

	postgres.generateDatabase("product")

	for _, table := range tables {
		filePath := fmt.Sprintf("%s/%s/%s.csv", postgres.InputPath, table.Schema, table.Table)
		csv := tools.Csv{}
		columns, err := csv.ReadColumns(filePath)
		if err != nil {
			return err
		}

		postgres.generateTable(table, columns)
		postgres.generateForeignKeys(table, columns)
		postgres.generateCustomScript(table)
	}
	return nil
}

func (p *Postgres) generateDatabase(dbName string) {
	tools.CreateFile(p.OutputPath, "db.sh", `#!/bin/bash
DB_HOST="localhost"
DB_PORT="5432"
DB_USER="postgres"
DB_PASSWORD="P@ssw0rd"
DB_NAME=`+dbName+`

export PGPASSWORD=$DB_PASSWORD
dropdb --host=$DB_HOST --port=$DB_PORT --username=$DB_USER --force --if-exists $DB_NAME
createdb --host=$DB_HOST --port=$DB_PORT --username=$DB_USER $DB_NAME
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -c "create extension pgcrypto;"
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -c "CREATE SCHEMA IF NOT EXISTS product;"
psql --host=$DB_HOST --port=$DB_PORT --username=$DB_USER -d $DB_NAME -f "product/numeric_type.table.sql"
unset PGPASSWORD
`)

	// script := "#!/bin/bash\n\n"
	// script += "set -e\n\n"
	//
	// schemas := make(map[string]bool)
	//
	//	for _, table := range tables {
	//		schemas[table.Schema] = true
	//	}
	//
	//	for schema := range schemas {
	//		script += fmt.Sprintf("psql -c \"CREATE SCHEMA IF NOT EXISTS %s;\"\n", schema)
	//	}
	//
	// script += "\n"
	//
	//	for _, table := range tables {
	//		script += fmt.Sprintf("psql -f %s/%s.table.sql\n", table.Schema, table.Table)
	//		script += fmt.Sprintf("if [ -f %s/%s.fk.sql ]; then psql -f %s/%s.fk.sql; fi\n", table.Schema, table.Table, table.Schema, table.Table)
	//		script += fmt.Sprintf("psql -f %s/%s.custom.sql\n", table.Schema, table.Table)
	//	}
	//
	// tools.CreateFile(p.OutputPath, "db.sh", script)
}

func (p *Postgres) generateTable(table models.Table, columns []models.Column) {
	schema := table.SchemaLower()
	tableName := table.TableLower()
	tableSql := fmt.Sprintf("CREATE TABLE IF NOT EXISTS %s.%s ();\n", schema, tableName)
	for _, col := range columns {
		tableSql += fmt.Sprintf("ALTER TABLE %s.%s ADD COLUMN IF NOT EXISTS %s %s", schema, tableName, col.ColumnLower(), col.Datatype)

		if col.Length != "" && col.Precision != "" {
			tableSql += fmt.Sprintf("(%s, %s)", col.Length, col.Precision)
		} else if col.Length != "" {
			tableSql += fmt.Sprintf("(%s)", col.Length)
		}

		if col.Required == "1" || col.Required == "true" {
			tableSql += " NOT NULL"
		}

		if col.Default != "" {
			tableSql += fmt.Sprintf(" DEFAULT %s", col.Default)
		}

		if col.Unique != "" {
			tableSql += " UNIQUE"
		}

		tableSql += ";\n"
	}

	tools.CreateFile(p.OutputPath+"/"+table.SchemaLower(), table.TableLower()+".table.sql", tableSql)
}

func (p *Postgres) generateForeignKeys(table models.Table, columns []models.Column) {
	fkSql := ""
	for _, col := range columns {
		if col.FkSchema != "" && col.FkTable != "" && col.FkColumn != "" {
			constraintName := fmt.Sprintf("fk_%s_%s_%s_%s_%s", table.SchemaLower(), table.TableLower(), col.ColumnLower(), col.FkSchemaLower(), col.FkTableLower())
			fkSql += fmt.Sprintf(`DO $$
BEGIN
	IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = '%[1]s') THEN
  	ALTER TABLE %[2]s.%[3]s ADD CONSTRAINT %[1]s FOREIGN KEY (%[4]s) REFERENCES %[5]s.%[6]s (%[7]s);
  END IF;
END $$;
`, constraintName, table.SchemaLower(), table.TableLower(), col.ColumnLower(), col.FkSchemaLower(), col.FkTableLower(), col.FkColumnLower())
		}
	}

	if fkSql != "" {
		tools.CreateFile(p.OutputPath+"/"+table.SchemaLower(), table.TableLower()+".fk.sql", fkSql)
	}
}

func (p *Postgres) generateCustomScript(table models.Table) {
	tools.CreateFile(p.OutputPath+"/"+table.SchemaLower(), table.TableLower()+".custom.sql", "-- insert custom scripts here")
}
