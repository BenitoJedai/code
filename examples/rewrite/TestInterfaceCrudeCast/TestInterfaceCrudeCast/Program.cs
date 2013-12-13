using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInterfaceCrudeCast
{
    interface IApp
    {
        object output { get; set; }
    }

    class AppFromDocument : IApp
    {
        public object output { get; set; }
    }

    interface IConceptX
    {
        object output { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var layout0 = new AppFromDocument();


            // Additional information: Unable to 
            // cast object of type 'TestInterfaceCrudeCast.AppFromDocument' 
            // to type 'TestInterfaceCrudeCast.IConceptX'.

            //Bug report https://connect.microsoft.com/VisualStudio/feedback/details/811506/compiler-doesnt-report-casting-issue

            var layout = (IConceptX)layout0;
        }
    }
}
