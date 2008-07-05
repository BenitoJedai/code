using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    // types defined in later versions of the framework

    public delegate T Func<T>();

    public delegate TResult Func<TArg0, TResult>(TArg0 arg0);

    public delegate TResult Func<TArg0, TArg1, TResult>(TArg0 arg0, TArg1 arg1);

    public delegate TResult Func<TArg0, TArg1, TArg2, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2);

    public delegate TResult Func<TArg0, TArg1, TArg2, TArg3, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);

}
