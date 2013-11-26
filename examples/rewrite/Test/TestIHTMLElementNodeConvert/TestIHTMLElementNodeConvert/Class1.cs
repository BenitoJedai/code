using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIHTMLElementNodeConvert
{
    public /* abstract */ partial class IHTMLElement :
        //IElement,
        INodeConvertible<IHTMLElement>
    {
        IHTMLElement INodeConvertible<IHTMLElement>.InternalAsNode()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720
            return this;
        }
    }

    public class IHTMLAudio : IHTMLElement
    {
    }

    public interface INodeConvertible<out TNode> where TNode : IHTMLElement
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/20/20130720

        TNode InternalAsNode();
    }

    public static class INodeConvertibleExtensions
    {

        public static TNode AsNode<TNode>(this INodeConvertible<TNode> e)
            //where TNode : INode

            // we are talking about dom nodes
            where TNode : IHTMLElement
        {
            if (e == null)
                return null;

            // will this work?

            //var n = e.as_INode();
            //if (n != null)
            //{
            //    return (TNode)n;
            //}


            return e.InternalAsNode();
        }


        public static IEnumerable<IHTMLAudio> AudioElements(this INodeConvertible<IHTMLElement> e)
        {
            //return e.AsNode().querySelectorAll(IHTMLElement.HTMLElementEnum.audio).Select(k => (IHTMLAudio)k);

            return null;
        }

    }
}
