using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace DynamicExperssionEvaluatorExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void TryInvokeMember(string Namespace, string TypeName, string MethodName, XElement args = null, Action<string> y = null)
        {

            Console.WriteLine(
                new { Namespace, TypeName, MethodName, args }
                );
        }

    }


    public class DynamicApplicationWebServiceX : DynamicObject
    {
        public DynamicApplicationWebServiceX Context;
        public GetMemberBinder ContextTryGetMember;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new DynamicApplicationWebServiceX { Context = this, ContextTryGetMember = binder };

            return true;
        }

        // Cannot invoke a non-delegate type

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            new ApplicationWebService().TryInvokeMember(
                Namespace: this.Context.ContextTryGetMember.Name,
                TypeName: this.ContextTryGetMember.Name,
                MethodName: binder.Name,


                args: new XElement("args",
                    from x in args
                    select new XElement("item", x)
                )

            );

            result = null;

            return true;
        }
    }
}
