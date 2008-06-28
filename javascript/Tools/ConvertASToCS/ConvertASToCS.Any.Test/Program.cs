using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConvertASToCS.js.Any;

namespace ConvertASToCS.Any.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new ProxyProvider(Properties.Settings.Default.TestProxyInput);
           // var p = new ReflectionProvider(Properties.Settings.Default.TestInput);


        }
    }
}
