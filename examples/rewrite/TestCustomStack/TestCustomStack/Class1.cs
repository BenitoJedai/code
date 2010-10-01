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
        public CommandLineOptionsBase()
        {
            var u = 6;

            Action<int> i = 
            x => u += x;

            i(4);
            i(4);
            i(4);

            u--;
        }
    }


    public class CommandLineOptions : CommandLineOptionsBase<CommandLineOptions>
    {

    }
}
