using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions
{
    /// <summary>
    /// To be used with FrameAttribute and TypeOfByNameOverrideAttribute
    /// </summary>
    [Script]
    public abstract class PreloaderSprite : Sprite
    {
        public abstract DisplayObject CreateInstance();

        public virtual bool AutoCreateInstance()
        {
            return true;
        }

        public PreloaderSprite()
        {
            this.LoadingComplete +=
                delegate
                {
                    if (AutoCreateInstance())
                        CreateInstance().AttachTo(this);
                };
        }

        public Action InternalLoadingComplete;

        public event Action LoadingComplete
        {
            add
            {

                // http://stackoverflow.com/questions/1269875/why-loaderinfo-bytestotal-is-zero/14964646#14964646
                // X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.PromotionPreloader\ApplicationSprite.cs

                if (InternalLoadingComplete == null)
                {
                    this.root.loaderInfo.complete +=
                        delegate
                        {

                            var e = default(Action<Event>);


                            e = delegate
                            {
                                this.enterFrame -= e;


                                if (InternalLoadingComplete != null)
                                    InternalLoadingComplete();



                            };

                            this.enterFrame += e;


                        };
                }

                InternalLoadingComplete += value;



            }
            remove
            {
            }
        }

        public event Action LoadingInProgress
        {
            add
            {
                var e = default(Action<Event>);

                e = delegate
                {
                    value();
                };

                this.enterFrame += e;

                this.LoadingComplete +=
                    delegate
                    {
                        this.enterFrame -= e;
                    };
            }
            remove
            {

            }
        }
    }

}
