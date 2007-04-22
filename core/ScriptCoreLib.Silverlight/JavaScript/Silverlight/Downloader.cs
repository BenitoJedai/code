using ScriptCoreLib;
using ScriptCoreLib.Shared;



namespace ScriptCoreLib.JavaScript.Silverlight
{
    // http://msdn2.microsoft.com/en-us/library/bb232870.aspx

    [Script(InternalConstructor = true)]
    public class Downloader : ISilverlightEventSink
    {
        /// <summary>
        /// Gets a value between 0 and 1 inclusive that represents the percentage amount of total content downloaded.
        /// </summary>
        public readonly double DownloadProgress;

        // http://msdn2.microsoft.com/en-us/library/bb232866.aspx

        #region InternalConstructor
        public Downloader(SilverlightControl ag)
        {

        }

        internal static Downloader InternalConstructor(SilverlightControl ag)
        {
            return (Downloader)ag.CreateObject("downloader");
        }
        #endregion

        #region ISilverlightEventSink Members

        public void AddEventListener(string eventType, string functionName)
        {
        }

        public void RemoveEventListener(string eventType, string functionName)
        {
        }

        #endregion

        #region events

        /// <summary>
        /// The DownloadProgressChanged event can be used monitor the progress of a download request. The DownloadProgressChanged event occurs whenever the percentage of total content downloaded increases by 0.05 or more, or reaches 1.0. The Completed event occurs when the state of the download request has changed.
        /// </summary>
        public event Action<UIElement> DownloadProgressChanged
        {
            [Script(DefineAsStatic = true)]
            add { this.AddEventListenerAsProxy("DownloadProgressChanged", value); }
            [Script(DefineAsStatic = true)]
            remove { this.RemoveEventListenerAsProxy("DownloadProgressChanged", value); }
        }
        #endregion

    }
}
