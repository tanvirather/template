package models

import "flag"

type Param struct {
	Input  string
	Csharp string
	Vue    string
}

func Load() Param {
	input := flag.String("input", "", "Path to the input files for the generator")
	csharp := flag.String("csharp", "", "Path to the output files for c# project")
	vue := flag.String("vue", "", "Path to the output files for vue project")
	flag.Parse()
	return Param{
		Input:  *input,
		Csharp: *csharp,
		Vue:    *vue,
	}
}
