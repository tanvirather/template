package main

import (
	"zuhid.com/generator/generate"
	"zuhid.com/generator/tools"
)

func main() {
	csv := tools.Csv{}
	entries, _ := csv.ReadTables("input/table.csv")

	postgres := generate.Postgres{InputPath: "input", OutputPath: "../db.postgres"}
	postgres.Generate(entries)

	// 	csharp := generate.Csharp{ InputPath:  "input",OutputPath: "../Product"}
	// 	csharp := generate.Csharp{Company: "Zuhid", Product: "Product", InputPath: "input", OutputPath: "output/Product"}
	csharp := generate.Csharp{Company: "Zuhid", Product: "Product", InputPath: "input", OutputPath: "../"}
	csharp.Generate(entries)
}
