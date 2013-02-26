using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using TestMemberExpression.Design;
using TestMemberExpression.HTML.Pages;

namespace TestMemberExpression
{
    // http://gaaton.blogspot.com/2013/02/c-property-names-without-harcoded_26.html
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }


    }

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        private static string GetPropertyName<T>
(Expression<Func<T>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            return body.Member.Name;

        }
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //error: System.InvalidOperationException: OpCodes.Ldtoken
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__44(ILRewriteContext e) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs:line 372
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<>c__DisplayClassc6.<set_Item>b__c4(ILRewriteContext e) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs:line 845
            //   at jsc.Languages.IL.ILTranslationExtensions.EmitTo(MethodBase SourceMethod, ILGenerator il, EmitToArguments x, TypeBuilder DeclaringType) in x:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.cs:line 270

            var x = GetPropertyName(() => new Person().Id);

            x.ToDocumentTitle();

        }

    }
}
