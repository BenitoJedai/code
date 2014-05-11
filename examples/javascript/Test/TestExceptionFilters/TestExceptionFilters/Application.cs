using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestExceptionFilters;
using TestExceptionFilters.Design;
using TestExceptionFilters.HTML.Pages;

namespace TestExceptionFilters
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {


            try
            {
                throw null;
            }
            catch (Exception e) if (myfilter(e))
            {
                //             02000002 TestExceptionFilters.Application
                //             script: error JSC1000: Method: .ctor, Type: TestExceptionFilters.Application; emmiting failed : System.NullReferenceException: Object reference not set to an instance of an object.
                //                at jsc.ILInstruction.InternalGetIfElseConstructVerified() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILInstruction.InlineIfElseConstruct.cs:line 86
                //at jsc.ILInstruction.InternalGetIfElseConstruct() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILInstruction.InlineIfElseConstruct.cs:line 64
                //at jsc.ILInstruction.get_InlineIfElseConstruct() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILInstruction.InlineIfElseConstruct.cs:line 54
                //at jsc.ILBlock.PrestatementBlock.Populate(ILInstruction First, ILInstruction Last) in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1511
                //at jsc.ILBlock.PrestatementBlock.Populate() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1433
                //at jsc.ILBlock.get_Prestatements() in x:\jsc.internal.svn\compiler\jsc\CodeModel\ILBlock.cs:line 1759
                //at jsc.Languages.JavaScript.MethodBodyOptimizer.TryOptimize(IdentWriter w, ILBlock xb) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\MethodBodyOptimizer.cs:line 89
                //at jsc.IL2Script.EmitBody(IdentWriter w, MethodBase SourceMethod, Boolean define_self) in x:\jsc.internal.svn\compiler\jsc\Languages\JavaScript\IL2Script.cs:line 566

            }


        }

        private bool myfilter(Exception e)
        {
            //Exception filters are preferable to catching and rethrowing because they leave the stack unharmed. If the exception later causes the stack to be dumped, you can see where it originally came from, rather than just the last place it was rethrown.

            return false;
        }
    }
}
