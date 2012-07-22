using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.CSharp;

namespace WebForms
{
	public class Calculator
	{
		public string Compute(string expression)
		{
			var source = "class Evaluator { public static string Evaluate() { return ("+expression+").ToString(); } }";

			var compileUnit = new CodeSnippetCompileUnit(source);
			var provider = new CSharpCodeProvider();

			var parameters = new CompilerParameters();
			var results = provider.CompileAssemblyFromDom(parameters, compileUnit);

			var type = results.CompiledAssembly.GetType("Evaluator");
			var method = type.GetMethod("Evaluate");
			return (string) method.Invoke(null, null);
		}
	}
}