using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ScriptCoreLib.JavaScript.DOM.SVG;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    public /* abstract */ partial class IHTMLElement :
        IElement,

        // Error	17	'ScriptCoreLib.JavaScript.DOM.INode' does not implement interface member 'ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.INode>.ToNode()'	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\INode.cs	14	15	ScriptCoreLib
        // circular ref?
        INodeConvertible<IHTMLElement>
    {
        // see also
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLElementGrouping.cs


        #region async
        [Script]
        public new class Tasks<TElement> where TElement : IHTMLElement
        {
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\Extensions\flash\display\InteractiveObject.cs

            internal TElement that;


            public virtual Task<IEvent> onmutation
            {
                [Script(DefineAsStatic = true)]
                get
                {
					// X:\jsc.svn\examples\javascript\xml\FindByClassAndObserve\FindByClassAndObserve\Application.cs
					// X:\jsc.svn\examples\javascript\svg\SVGFromHTMLDivObservable\SVGFromHTMLDivObservable\Application.cs

					var x = new TaskCompletionSource<IEvent>();
                    //that.onmouseover += x.SetResult;

                    new MutationObserver(
                                new MutationCallback(
                                    (MutationRecord[] mutations, MutationObserver observer) =>
                                    {
                                        //Console.WriteLine("Mutations len " + mutations.Length);


                                        // mutationevent?
                                        //x.SetResult(null);


                                        // non null will yield to await
                                        x.SetResult((IEvent)new object());

                                        observer.disconnect();
                                    }
                                )
                            ).observe(that,
                                new
                                {
                                    // Set to true if mutations to target's children are to be observed.
                                    childList = true,
                                    // Set to true if mutations to target's attributes are to be observed. Can be omitted if attributeOldValue and/or attributeFilter is specified.
                                    attributes = true,
                                    // Set to true if mutations to target's data are to be observed. Can be omitted if characterDataOldValue is specified.
                                    characterData = true,
                                    // Set to true if mutations to not just target, but also target's descendants are to be observed.
                                    subtree = true,
                                }
                            );

                    return x.Task;
                }
            }

            #region onclick
            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public virtual Task<IEvent> onclick
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    that.onclick +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    // tested by?
                    ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.InternalTaskNameLookup[x.Task] = "onclick";

                    return x.Task;
                }
            }
			#endregion

			public virtual Task<IEvent> onchange
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\chrome\apps\ChromeHTMLTextToGLSLBytes\ChromeHTMLTextToGLSLBytes\Application.cs

					var x = new TaskCompletionSource<IEvent>();
					that.onchange += x.SetResult;
					return x.Task;
				}
			}



			// X:\jsc.svn\examples\javascript\async\Test\TestAsyncMouseOver\TestAsyncMouseOver\Application.cs
			public virtual Task<IEvent> onmouseover
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseover += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onmouseout
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseout += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onmousedown
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmousedown += x.SetResult;
                    return x.Task;
                }
            }


            public virtual Task<IEvent> onmouseup
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseup += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onkeyup
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onkeyup += x.SetResult;
                    return x.Task;
                }
            }




            #region onscrollToBottom
            [Obsolete("how to name this?")]
            public Task<IEvent> onscrollToBottom
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\Application.cs
                    // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs

                    var x = new TaskCompletionSource<IEvent>();

                    that.onscroll +=
                          e =>
                          {
                              if (x == null)
                                  return;



                              if (that.scrollHeight - 1 <= that.clientHeight + that.scrollTop)
                              {
                                  x.SetResult(e);
                                  x = null;
                              }
                          };

                    return x.Task;
                }
            }
            #endregion
        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks<IHTMLElement> async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks<IHTMLElement> { that = this };
            }
        }
        #endregion
    }
}
