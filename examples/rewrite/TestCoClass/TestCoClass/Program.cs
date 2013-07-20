using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestCoClass
{
    [ComImport, Guid("579A4F68-4E51-479A-A7AA-A4DDC4031F3F")]
    public interface ICorDebug
    {
        int MyProperty { get; set; }
        void Bar();
    }

    [ComImport, Guid("079A4F68-4E51-479A-A7AA-A4DDC4031F3F"), CoClass(typeof(CorDebugClass))]
    public interface CorDebug : ICorDebug
    {
        // no members
    }

    [ClassInterface((short)0)]
    [TypeLibType(2)]
    public class CorDebugClass : CorDebug
    {
        public int MyProperty { get; set; }
        public void Bar() { Console.WriteLine("foo"); }
    }

    class Program
    {
        // http://stackoverflow.com/questions/8094441/is-it-ok-to-abuse-coclassattribute-to-provide-a-default-implementation-for-an

        static void Main(string[] args)
        {
            // Error	2	Cannot create an instance of the abstract class or interface 'TestCoClass.IApp'	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	37	21	TestCoClass
            var x = new IApp { foo = "foo" };
            var doc = new Document();

            // Error	1	Cannot implicitly convert type 'TestCoClass.Document' to 'TestCoClass.IApp'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	41	23	TestCoClass
            //IApp xx = doc;

            var xx = doc.ToApp();


            // Constructs a FooImpl
            ICorDebug foo = new CorDebug { MyProperty = 5 };

            foo.Bar();
        }
    }

    // Error	1	Error emitting 'System.Runtime.InteropServices.GuidAttribute' attribute -- 'Incorrect UUID format.'	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	49	39	TestCoClass
    // Error	1	An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	47	44	TestCoClass
    // Error	1	Duplicate 'CoClass' attribute	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	49	39	TestCoClass
    [ComImport, CoClass(typeof(App)), Guid("00000000-0000-0000-0000-000000000000")]
    public interface IApp
    {
        // Error	1	The Guid attribute must be specified with the ComImport attribute	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	48	22	TestCoClass
        //Warning	1	'TestCoClass.IApp' interface marked with 'CoClassAttribute' not marked with 'ComImportAttribute'	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	46	22	TestCoClass

        string foo { set; get; }
        void Bar();


    }

    public static class X
    {
        public static IApp ToApp(this Document e)
        {
            return null;
        }
    }

    public class App : IApp
    {
        public string foo { set; get; }

        public void Bar()
        {
        }

        // Error	1	User-defined conversion must convert to or from the enclosing type	X:\jsc.svn\examples\rewrite\TestCoClass\TestCoClass\Program.cs	70	23	TestCoClass
        public static implicit operator App(Document e)
        {
            return null;
        }
    }

    public class Document
    {
    }
}
