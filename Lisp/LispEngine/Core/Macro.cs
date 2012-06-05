﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LispEngine.Datums;
using LispEngine.Evaluation;
using LispEngine.Stack;
using Environment = LispEngine.Evaluation.Environment;

namespace LispEngine.Core
{
    /**
     * Turns a function into something that
     * can be used as a macro.
     */
    class Macro : DatumHelpers, Function
    {
        public static readonly Function Instance = new Macro();
        class MacroClosure : FExpression
        {
            private readonly Function argFunction;

            public MacroClosure(Function argFunction)
            {
                this.argFunction = argFunction;
            }

            public Datum Evaluate(Evaluator evaluator, Environment env, Datum args)
            {
                var expansion = argFunction.Evaluate(evaluator, args);
                return evaluator.Evaluate(env, expansion);
            }

            public void Evaluate(EvaluatorStack evaluator, Environment env, Datum args)
            {
                // TODO: Functions to be evaluated using non-implicit stack
                var expansion = argFunction.Evaluate(new Evaluator(), args);
                evaluator.PushTask(new EvaluateTask(expansion, env));
            }
        }

        public static FExpression ToMacro(Function function)
        {
            return new MacroClosure(function);
        }

        private static Datum evaluate(Datum args)
        {
            var a = enumerate(args).ToArray();
            if (a.Length != 1)
                // TODO: Move this error boiler plate into DatumHelpers
                throw error("Incorrect arguments. Expected {0}, got {1}", 1, a.Length);
            var function = a[0] as Function;
            if (function == null)
                throw error("Expected function argument");
            return ToMacro(function);            
        }

        public Datum Evaluate(Evaluator evaluator, Datum args)
        {
            return evaluate(args);
        }
    }
}
