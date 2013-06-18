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

            // Constructs a FooImpl
            ICorDebug foo = new CorDebug { MyProperty = 5 };

            foo.Bar();
        }
    }
}
