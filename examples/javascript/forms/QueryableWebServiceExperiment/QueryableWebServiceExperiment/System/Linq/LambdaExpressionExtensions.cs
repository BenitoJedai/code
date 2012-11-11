using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace System.Linq
{
    static class ExpressionExtensions
    {
        // e = {x => x.Text.Contains("foo")}
        public static XElement ToXElement(this LambdaExpression e)
        {
            dynamic __e_Body = e.Body;

            //+		Arguments	{System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>}	dynamic {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>}
            //+		Method	{Boolean Contains(System.String)}	dynamic {System.Reflection.RuntimeMethodInfo}
            //+		Object	{x.Text}	dynamic {System.Linq.Expressions.FieldExpression}


            var e_Body = new
            {
                Arguments = (ICollection<System.Linq.Expressions.Expression>)__e_Body.Arguments,
                Method = (MethodInfo)__e_Body.Method,
                Object = __e_Body.Object
            };

            //            -		e.Body	{x.Text.Contains("foo")}	System.Linq.Expressions.Expression {System.Linq.Expressions.InstanceMethodCallExpressionN}
            //+		Arguments	Count = 1	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>}
            //        CanReduce	false	bool
            //        DebugView	".Call ($x.Text).Contains(\"foo\")"	string
            //+		Method	{Boolean Contains(System.String)}	System.Reflection.MethodInfo {System.Reflection.RuntimeMethodInfo}
            //        NodeType	Call	System.Linq.Expressions.ExpressionType
            //+		Object	{x.Text}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
            //+		Type	{Name = "Boolean" FullName = "System.Boolean"}	System.Type {System.RuntimeType}

            //System.Linq.Expressions.InstanceMethodCallExpressionN

            var xe = new XElement("LambdaExpression",


                new XElement("Parameters",
                    from p in e.Parameters
                    select new XElement("Parameter",
                        new XElement("Name", p.Name),
                        new XElement("Type", p.Type.Name)
                    )
                ),

                new XElement("ReturnType",
                    new XElement("Name", e.ReturnType.Name)
                ),

                new XElement("Body",
                    new XElement("Method", e.Body)
                )
            );

            return xe;
        }
    }
}
