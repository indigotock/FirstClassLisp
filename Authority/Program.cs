using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LispEngine.Bootstrap;
using LispEngine.Datums;
using LispEngine.Evaluation;
using LispEngine.Lexing;
using LispEngine.Parsing;
using LispEngine.Util;

class Program
{

	private static IEnumerable<Datum> ReadDatums(string source)
	{
		var s = new Scanner(new StringReader(source)) { Filename = "embedded" };
		var p = new Parser(s);
		Datum d;
		while ((d = p.parse()) != null)
		{
			yield return d;
		}
	}

	static void Main(string[] args)
	{
		try
		{
			var env = StandardEnvironment.CreateSandbox();
			env.Define("args", DatumHelpers.atomList(args));
			var statistics = new Statistics();
			env = statistics.AddTo(env);
			var assembly = typeof(Program).GetTypeInfo().Assembly;

			var evaluator = new Evaluator();
			Datum result = default(Datum);
			foreach (var d in ReadDatums(Console.ReadLine()))
				result = evaluator.Evaluate(statistics, env, d);

			Console.Write(result);
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("ERROR:\n{0}\n{1}\n", ex, ex.StackTrace);
		}
	}
}
