using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared;


namespace ScriptCoreLib.JavaScript.Silverlight.Controls
{
    // http://msdn2.microsoft.com/en-us/library/system.windows.controls.mediaelement.aspx

    [Script(IsStringEnum = true)]
    public enum MediaState
    {
        /// <summary>
        ///  The MediaElement is loading the media for playback. Its Position does not advance during this state. If the MediaElement was already playing video, it continues to display the last displayed frame.
        /// </summary>
        Buffering,

        /// <summary>
        /// The MediaElement contains no media. The MediaElement displays a transparent frame.
        /// </summary>
        Closed,
        /// <summary>
        /// The MediaElement encountered an error during media playback. If the MediaElement was playing video, it continues to display the last displayed frame. This MediaElement changes to this state after raising the MediaFailed event.
        /// </summary>
        Error,
        /// <summary>
        /// The MediaElement is validating and attempting to open the Uri specified by its Source property. While in this state, the MediaElement queues any Play, Pause, or Stop commands it receives and processes them if the media is successfully opened.
        /// </summary>
        Opening,
        /// <summary>
        /// The MediaElement does not advance its Position. If the MediaElement was playing video, it continues to display the current frame.
        /// </summary>
        Paused,
        /// <summary>
        /// The MediaElement is playing the media specified by its source property. Its Position advances forward.
        /// </summary>
        Playing,
        /// <summary>
        /// The MediaElement contains media but is not playing or paused. Its Position is 0 and does not advance. If the loaded media is video, the MediaElement displays the first frame.
        /// </summary>
        Stopped
    }

    [Script(InternalConstructor = true)]
    public class MediaElement : UIElement
    {

        // <object property="[days.]hours:minutes:seconds[.fractionalSeconds]" .../>
        //- or -
        //<object property="[days.]hours:minutes" .../>
        //- or -
        //<object property="days" .../>
        //- or -
        //<object property="Automatic" .../>
        //- or -
        //<object property="Forever" .../>

        public string NaturalDuration;
        public double NaturalVideoHeight;
        public double NaturalVideoWidth;

        public bool AutoPlay;
        public string Source;

        public double BufferingProgress;

        public MediaState CurrentState;

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void Stop()
        {
        }

        #region InternalConstructor
        public MediaElement(SilverlightControl ag)
        {

        }

        internal static MediaElement InternalConstructor(SilverlightControl ag)
        {
            return (MediaElement)ag.CreateElement("MediaElement");
        }
        #endregion

        #region events

        public event Action<MediaElement> MediaOpened
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MediaOpened", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MediaOpened", value); }
        }

        public event Action<MediaElement> MediaEnded
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MediaEnded", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MediaEnded", value); }
        }

        public event Action<MediaElement> MediaFailed
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("MediaFailed", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("MediaFailed", value); }
        }

        public event Action<MediaElement> BufferingProgressChanged
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("BufferingProgressChanged", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("BufferingProgressChanged", value); }
        }

        public event Action<MediaElement> CurrentStateChanged
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("CurrentStateChanged", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("CurrentStateChanged", value); }
        }

        #endregion
    }
}