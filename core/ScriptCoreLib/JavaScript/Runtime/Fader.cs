using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;


using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script]
	[System.Obsolete("To be moved out of CoreLib or removed")]
    public class Fader 
    {
        static public void FadeOut(IHTMLElement target)
        {
            FadeOut(target, 0, 300);
        }

        static public void FadeOut(IHTMLElement target, int waittime, int fadetime)
        {
            target.style.Opacity = 1;

            new Timer(
                delegate
                {
                    Timer a = null;

                    a = new Timer(
                        delegate
                        {

                            target.style.Opacity = 1 - (a.Counter / a.TimeToLive);

                            if (a.Counter == a.TimeToLive)
                            {
                                target.Hide();

                            }

                        }
                    );


                    a.StartInterval(fadetime / 25, 25);
                }
            ).StartTimeout(waittime);
        }

        static public void FadeAndRemove(IHTMLElement target)
        {
            FadeAndRemove(target, 0, 300);
        }

        /// <summary>
        /// fades an element and provides async callback
        /// </summary>
        /// <param name="target"></param>
        /// <param name="waittime"></param>
        /// <param name="fadetime"></param>
        /// <param name="done"></param>
        static public void Fade(IHTMLElement target, int waittime, int fadetime, EventHandler done)
        {
            // if IE
            target.style.height = target.clientHeight + "px";

            new Timer(
                delegate
                {
                    Timer a = null;

                    a = new Timer(
                        delegate
                        {

                            target.style.Opacity = 1 - (a.Counter / a.TimeToLive);

                            if (a.Counter == a.TimeToLive)
                            {
                                if (done != null)
                                    done();

                            }

                        }
                        );


                    a.StartInterval(fadetime / 25, 25);
                }
            ).StartTimeout(waittime);
        }

        static public void FadeAndRemove(IHTMLElement target, int waittime, int fadetime, params IHTMLElement[] cotargets)
        {
            // if IE
            target.style.height = target.clientHeight + "px";

            new Timer(
                delegate
                {
                    Timer a = null;

                    a = new Timer(
                        delegate
                        {

                            target.style.Opacity = 1 - (a.Counter / a.TimeToLive);

                            if (a.Counter == a.TimeToLive)
                            {

                                target.Dispose();

                                foreach (IHTMLElement z in cotargets)
                                    z.Dispose();

                                
                                
                            }

                        }
                        );


                    a.StartInterval(fadetime / 25, 25);
                }
            ).StartTimeout(waittime);
        }

        public static WorkPool FlashAndFadeOut(IHTMLElement e, int interval)
        {

            WorkPool p = new WorkPool(interval);

            p += delegate { e.Hide(); };
            p += delegate { e.Show(); };
            p += delegate { e.Hide(); };
            p += delegate { e.Show(); };

            e.style.zIndex = 1000;

            return p;
        }
    }
}