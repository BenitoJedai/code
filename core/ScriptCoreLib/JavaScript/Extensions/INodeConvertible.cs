using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Extensions
{
    // Error	16	The type 'ScriptCoreLib.JavaScript.DOM.INode' 
    // cannot be used as type parameter 'TNode' in the generic type or 
    // method 'ScriptCoreLib.JavaScript.Extensions.INodeConvertible<TNode>'. 
    // There is no implicit reference conversion from 'ScriptCoreLib.JavaScript.DOM.INode' 
    // to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement'.	
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\Extensions\INodeConvertible.cs	12	22	ScriptCoreLib
    [Script]
    // IHTMLElementConvertible
    public interface INodeConvertible<out TNode>
        //where TNode : INode

        // we are talking about dom nodes
        where TNode : IHTMLElement
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("Since jsc does not yet handle native explicit interface use INodeConvertibleExtensions.AsNode instead!")]
        // could we not just define that this method is always to be defined as static?
        // how would that work?
        //[Script(DefineAsStatic = true)]
        TNode InternalAsNode();

        // we are testing it on
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\WebGLRenderingContext.cs

    }


    [Script]
    public static class INodeConvertibleExtensions
    {
        // Error	20	The type 'ScriptCoreLib.JavaScript.DOM.INode' cannot be used as type parameter 'THTMLElement' in the generic type or method 'ScriptCoreLib.JavaScript.Extensions.IConvertToHTMLElement<THTMLElement>'. There is no implicit reference conversion from 'ScriptCoreLib.JavaScript.DOM.INode' to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement'.	X:\jsc.svn\core\ScriptCoreLib\JavaScript\Extensions\IConvertToHTMLElement.cs	22	25	ScriptCoreLib


        //static INode as_INode_like_element;

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        static INode as_INode(this object e)
        {
            if (e != null)
            {
                // every browser names the Node type differently?

                // what about comments?
                if (Expando.InternalIsMember(e, "nodeType"))
                {
                    var nodeType = (int)Expando.InternalGetMember(e, "nodeType");

                    if (nodeType != 0)
                        return (INode)e;
                }

                // not yet?
                // see also: X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\WebGLRenderingContext.cs
                //if (Expando.InternalIsMember(e, "canvas"))
                //{
                //    return as_INode(Expando.InternalGetMember(e, "canvas"));
                //}
            }



            return null;
        }

        public static TNode AsNode<TNode>(this INodeConvertible<TNode> e)
            //where TNode : INode

            // we are talking about dom nodes
            where TNode : IHTMLElement
        {
            if (e == null)
                return null;

            // will this work?
            // jsc needs to maintain a dispatch vtable for all native objects for which we want to add and extend interfaces.
            var xWebGLRenderingContext = e as ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
            if (xWebGLRenderingContext != null)
            {
                //return xWebGLRenderingContext.InternalAsNode();
                // hack. inline that method for now..
                return (TNode)(IHTMLElement)xWebGLRenderingContext.canvas;
            }

            var n = e.as_INode();
            if (n != null)
            {
                return (TNode)n;
            }


            return e.InternalAsNode();
        }



    }

    [Script]
    public static class INodeConvertibleExtensionsNamed
    {
        [Obsolete]
        public static IEnumerable<IHTMLAudio> AudioElements(this INodeConvertible<IHTMLElement> e)
        {
            return e.AsNode().querySelectorAll(IHTMLElement.HTMLElementEnum.audio).Select(k => (IHTMLAudio)k);
        }

        [Obsolete]
        public static IEnumerable<IHTMLImage> ImageElements(this INodeConvertible<IHTMLElement> e)
        {
            return e.AsNode().querySelectorAll(IHTMLElement.HTMLElementEnum.img).Select(k => (IHTMLImage)k);
        }
    }
}
