using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using System.Windows;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Avalon;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.IO;
using ScriptCoreLib.JavaScript.BCLImplementation.System.IO;

namespace ScriptCoreLib.JavaScript.UCLImplementation
{
    [Script(Implements = typeof(global::ScriptCoreLib.Shared.Avalon.Extensions.AvalonExtensions))]
    internal static class __AvalonExtensions
    {
        public static AvalonSoundChannel ToSound(this string asset)
        {
            var a = new IHTMLAudio { src = asset, autobuffer = true };


            a.AttachToDocument();
            //a.style.display = IStyle.DisplayEnum.none;

            // we can now use HTML5 audio element
            var x = new AvalonSoundChannel
            {

                Start =
                    delegate
                    {
                        a.play();
                    },

                Stop =
                    delegate
                    {
                        a.pause();

                    }
            };

            a.onended +=
                delegate
                {
                    x.RaisePlaybackComplete();
                };


            return x;
        }

        public static AvalonSoundChannel PlaySound(this string asset)
        {
            // we can now use HTML5 audio element
            var a = asset.ToSound();

            a.Start();

            return a;
        }

        public static void NavigateTo(this Uri e, DependencyObject context)
        {
            //var _e = (__Uri)(object)e;

            var w = Native.window.open(e.OriginalString, "_blank");

        }

        public static void ToStringAsset(this string e, Action<string> h)
        {
            new IXMLHttpRequest(
                ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                e,
                r =>
                {
                    h(r.responseText);
                }
            );
        }

        public static void ToMemoryStreamAsset(this string e, Action<MemoryStream> h)
        {
            new IXMLHttpRequest(
                ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                e,
                r =>
                {
                    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\MemoryStream.cs
                    var m = new __MemoryStream { InternalBuffer = r.responseText };

                    h((MemoryStream)(object)m);
                }
            );

            //h(KnownEmbeddedResources.Default[e].ToByteArrayAsset().ToMemoryStream());
        }

        public static ImageSource ToSource(this string e)
        {
            // the c# version must do some internal work to figure
            // out the right stream name
            // in actionscript we are using [Embed]

            return new __ImageSource { InternalManifestResourceAlias = e };
        }
    }
}
