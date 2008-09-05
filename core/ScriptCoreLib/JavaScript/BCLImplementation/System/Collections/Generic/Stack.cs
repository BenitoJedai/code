using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(Stack<>))]
    internal class __Stack<T> 
    {
		readonly IArray<T> items = new IArray<T>();

        public T Pop()
        {
            return (T)items.pop();
        }

        public void Push(T item)
        {
            items.push(item);
        }

        public int Count { get { return (int)items.length; } }

    }
}
