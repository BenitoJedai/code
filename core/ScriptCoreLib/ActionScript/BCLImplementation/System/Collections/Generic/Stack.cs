using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(Stack<>))]
    internal class __Stack<T> 
    {
        readonly Array items = new Array();

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
