using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCustomStack
{
    public class ILFlowStackItem
    {
    }

    public class ILFlowEvaluationStack : Stack<ILFlowStackItem>
    {
        public ILFlowEvaluationStack Clone()
        {
            throw null;
        }
    }

    public abstract class CommandLineOptionsBase<T>
       where T : CommandLineOptionsBase<T>
    {
    }


    public class CommandLineOptions : CommandLineOptionsBase<CommandLineOptions>
    {

    }
}
