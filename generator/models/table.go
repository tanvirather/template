package models

import "strings"

// TableModel represents a row in the Table.csv file.
type Table struct {
	Schema     string `csv:"schema"`
	Table      string `csv:"table"`
	BaseSchema string `csv:"base_schema"`
	BaseTable  string `csv:"base_table"`
}

func (t Table) SchemaLower() string {
	return strings.ToLower(t.Schema)
}

func (t Table) TableLower() string {
	return strings.ToLower(t.Table)
}

func (t Table) SchemaCsharp() string {
	return strings.ReplaceAll(t.Schema, "_", "")
}

func (t Table) TableCsharp() string {
	return strings.ReplaceAll(t.Table, "_", "")
}
