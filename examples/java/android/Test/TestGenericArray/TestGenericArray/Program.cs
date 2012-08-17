using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using TestGenericArray.Activities;

namespace TestGenericArray
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            {
            
                foo[] z = new foo[0];
                // Error	1	Cannot implicitly convert type 'object[]' to 'TestGenericArray.Activities.foo[]'. An explicit conversion exists (are you missing a cast?)	Y:\jsc.svn\examples\java\android\Test\TestGenericArray\TestGenericArray\Program.cs	24	23	TestGenericArray
                // Unable to cast object of type 'System.Object[]' to type 'TestGenericArray.Activities.foo[]'.
                object[] y = z;
            }

            //{
            //    var x = new __List<foo>
            //    {
            //        Item1 = new foo(),
            //        Item2 = new foo(),
            //    };

            //    object[] z = new object[0];
            //    // Error	1	Cannot implicitly convert type 'object[]' to 'TestGenericArray.Activities.foo[]'. An explicit conversion exists (are you missing a cast?)	Y:\jsc.svn\examples\java\android\Test\TestGenericArray\TestGenericArray\Program.cs	24	23	TestGenericArray
            //    // Unable to cast object of type 'System.Object[]' to type 'TestGenericArray.Activities.foo[]'.
            //    foo[] y = (foo[])/* recreate array? */(object)z;
            //    var a = x.ToArray();
            //}

            global::jsc.AndroidLauncher.Launch(
                 typeof(TestGenericArray.Activities.ApplicationActivity)
            );
        }
    }
}
