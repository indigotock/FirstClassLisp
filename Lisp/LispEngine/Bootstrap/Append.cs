﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LispEngine.Datums;
using LispEngine.Evaluation;

namespace LispEngine.Bootstrap
{
    class Append : DatumHelpers, Function
    {
        // We could implement this in pure lisp but I'm lazy
        // and it's trivial.
        public static readonly StackFunction Instance = new Append().ToStack();
        public Datum Evaluate(Datum args)
        {
            var result = enumerate(args).Aggregate(Enumerable.Empty<Datum>(), (current, a) => current.Concat(enumerate(a)));
            return compound(result.ToArray());
        }
    }
}
