using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System;

namespace ScriptCoreLib.JavaScript.DOM
{



    partial class IWindow
    {
        #region async
        [Script]
        public new class Tasks
        {
            internal IWindow that;

            #region onmessage
            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public Task<MessageEvent> onmessage
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<MessageEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    that.onmessage +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    return x.Task;
                }
            }
            #endregion

            #region onresize
            public Task<IEvent> onresize
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    that.onresize +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    return x.Task;
                }
            }
			#endregion


			#region onerror
			public Task<IEvent> onblur
			{
				[Script(DefineAsStatic = true)]
				get
				{
					// X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs

					var x = new TaskCompletionSource<IEvent>();
					that.onblur += x.SetResult;
					return x.Task;
				}
			}
			#endregion

			#region onerror
			public Task<IErrorEvent> onerror
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IErrorEvent>();
                    that.onerror += x.SetResult;
                    return x.Task;
                }
            }
            #endregion


            #region onload
            public Task<IEvent> onload
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onload += x.SetResult;
                    return x.Task;
                }
            }
            #endregion

            #region onframe
            public Task<FrameEvent> onframe
            {
                [Script(DefineAsStatic = true)]
                get
                {
					// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Application.cs
					var e = new FrameEvent();

					var x = new TaskCompletionSource<FrameEvent>();
					that.requestAnimationFrame +=
						delegate
						{
							x.SetResult(e);
						};


                    return x.Task;
                }
            }
            #endregion



            #region onscrollToBottom
            [Obsolete("how to name this?")]
            public Task<IEvent> onscrollToBottom
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\Application.cs

                    var x = new TaskCompletionSource<IEvent>();

                    that.onscroll +=
                          e =>
                          {
                              if (x == null)
                                  return;

                              var body = that.document.body;


                              if (body.scrollHeight - 1 <= Native.window.Height + body.scrollTop)
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
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
        #endregion


    }
}
