using LispEngine.Evaluation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authority.Environments
{
    public class EnvironmentDefinition
    {
        public ICollection<Function> Functions = new HashSet<Function>(); 
    }
}
