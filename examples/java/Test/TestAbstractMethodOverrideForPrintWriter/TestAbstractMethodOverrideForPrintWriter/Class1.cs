using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/scriptcorelibandroid-natives
// http://docs.oracle.com/javase/7/docs/api/java/io/PrintWriter.html
// http://docs.oracle.com/javase/7/docs/api/java/io/Writer.html

namespace java.io
{
    public abstract class Writer
    {
        public  void write(char[] cbuf) { }

        public abstract void write(char[] cbuf, int off, int len);
    }

    public class PrintWriter : Writer
    {
        // Error	1	'java.io.Writer.write(char[], int, int)': virtual or abstract members cannot be private	X:\jsc.svn\examples\java\test\TestAbstractMethodOverrideForPrintWriter\TestAbstractMethodOverrideForPrintWriter\Class1.cs	17	23	TestAbstractMethodOverrideForPrintWriter


        public override void write(char[] cbuf, int off, int len)
        {
        }
    }
}
