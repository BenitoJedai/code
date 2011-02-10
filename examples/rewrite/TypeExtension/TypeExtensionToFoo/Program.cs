extern alias source;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeExtension
{
    partial class Program
    {
        /*
         * Steps taken to implement this:
         * 01. Make jsc rewrite only TypeExtensionToFoo and verify jsc rewrite works
         * 02. Commit to svn before we break it again.
         * 03. Make JSC do AssemblyMerge with TypeExtension and attach a debugger to verify the crash
         * 04. Switch to AssemblyMergeExtension flag and verify only the source assembly is rewritten
         * 05. Include AssemblyMergeExtension entries for AssemblyMerge.
         * 06. Make JSC merge types that are not extensions like type NotExtensionType below
         * 07. Make JSC remember which types are extension types for use later
         * 08. Make JSC extend fields
         */
    }

    partial class Program
    {
        public static string StaticStringFieldExtension;
        public NotExtensionType InstanceNotExtensionTypeFieldExtension;

        // this method shall update the existing implementation
        public static void Foo2(global::TypeExtension.FooType f)
        {
            Console.WriteLine("extension!");
            Console.WriteLine(f.FooText);
        }
    }

    public class FooType
    {
        public source::TypeExtension.BarType Bar;
        public Dictionary<source::TypeExtension.BarType, NotExtensionType> BarToNotExtensionType;

        public event Action<Dictionary<source::TypeExtension.BarType, NotExtensionType>> ComplexEvent;

        public void RaiseComplexEvent(Dictionary<source::TypeExtension.BarType, NotExtensionType> e)
        {
            if (ComplexEvent != null)
                ComplexEvent(e);
        }

        // this member shall be resolved as already existing member
        public string Text;

        // this member shall be added
        public string FooText { get { return this.Text + " foo"; } }
    }

    public class NotExtensionType
    {
        public string Text { get; set; }

        public void Apply(FooType k)
        {
            var t = k.Text + " apply";
        }
    }
}
