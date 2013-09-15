using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chrome
{
    // tested by X:\jsc.svn\examples\javascript\chrome\ChromeNotificationExperiment\ChromeNotificationExperiment\Application.cs
    public sealed class NotificationOptions
    {
        public string iconUrl = "assets/ScriptCoreLib/jsc.png";
        public string message;
        public string title;
        public string type = "basic";
    }


    public sealed class Notification
    {
        //public readonly string notificationId;
        public readonly string Key;

        //public NotificationOptions options { get; private set; }

        public event Action<bool> Closed;
        public event Action Clicked;

        //public event Action<int> ButtonClicked;


        string _title;
        public string Title { get { return _title; } set { _title = value; update(); } }

        string _message;
        public string Message { get { return _message; } set { _message = value; update(); } }

        string _iconUrl;
        public string IconUrl { get { return _iconUrl; } set { _iconUrl = value; update(); } }

        Action update;

        public bool IsClosed { get; private set; }

        #region IconCanvas
        IHTMLCanvas _IconCanvas;
        Action _IconCanvasSync;

        public IHTMLCanvas IconCanvas
        {
            get { return _IconCanvas; }
            set
            {
                _IconCanvas = value;
                if (_IconCanvasSync == null)
                {
                    _IconCanvasSync = async delegate
                    {

                        while (!this.IsClosed)
                        {
                            //n.Message = new { t.ElapsedMilliseconds }.ToString();
                            this.IconUrl = _IconCanvas.toDataURL();

                            await Task.Delay(1000 / 15);
                        }

                        _IconCanvasSync = null;
                    };

                    _IconCanvasSync();
                }

            }
        }
        #endregion

        public static List<Notification> AllNotifications = new List<Notification>();

        public static string DefaultTitle = "my.jsc-solutions.net";

        public Notification(
            string notificationId = null,
                string title = null,

                string message = "",

                string type = "basic",
                 string iconUrl = "assets/ScriptCoreLib/jsc.png"

            )
        {
            if (title == null)
                title = DefaultTitle;

            if (notificationId == null)
                notificationId = "Notification#" + AllNotifications.Count;

            AllNotifications.Add(this);

            this._title = title;
            this._message = message;
            this._iconUrl = iconUrl;
            this.Key = notificationId;

            this.IsClosed = false;

            this.update = delegate
            {

            };

            // wait for any property initialization

            //Console.WriteLine("before Delay");
            Task.Delay(1).GetAwaiter().OnCompleted(
                delegate
                {
                    //Console.WriteLine("at Delay");

                    // tested by
                    // X:\jsc.svn\examples\javascript\chrome\ChromeNotificationExperiment\ChromeNotificationExperiment\Application.cs
                    var doupdate = false;

                    this.update = delegate
                    {
                        doupdate = true;

                    };

                    chrome.notifications.create(
                        this.Key,
                        new NotificationOptions
                        {
                            title = this._title,
                            message = this._message,
                            type = type,
                            iconUrl = this._iconUrl
                        }
                    ).GetAwaiter().OnCompleted(
                        delegate
                        {

                            #region Closed
                            //chrome.notifications.onClosed.addListener(
                            chrome.notifications.Closed +=
                                //new Action<string, bool>(
                                     (__notificationId, __byUser) =>
                                     {
                                         if (__notificationId != this.Key)
                                             return;

                                         IsClosed = true;

                                         if (this.Closed != null)
                                             this.Closed(__byUser);

                                         //Console.WriteLine("onClosed " + new { __notificationId, __byUser });
                                     };
                            //    )
                            //);

                            #endregion

                            #region Clicked
                            chrome.notifications.Clicked +=
                                (__notificationId) =>
                                {
                                    if (__notificationId != this.Key)
                                        return;

                                    //Console.WriteLine("onClicked " + new { __notificationId });

                                    if (this.Clicked != null)
                                        this.Clicked();

                                    // 'tabs' is only allowed for extensions and legacy packaged apps, and this is a packaged app.

                                    //dynamic createProperties = new object();

                                    //createProperties.url = "http://example.com";

                                    //chrome.tabs.create(createProperties,

                                    //   new Action<Tab>(
                                    //       tab =>
                                    //       {
                                    //           Console.WriteLine("tab " + new { tab.id, tab.windowId });
                                    //       }
                                    //   )
                                    //);


                                    //Native.window.open("http://example.com", "_blank");
                                };
                            //    )
                            //);
                            #endregion

                            this.update = async delegate
                            {
                                var wasUpdated = await chrome.notifications.update(
                                    this.Key,
                                    new NotificationOptions
                                    {
                                        title = this._title,
                                        message = this._message,
                                        type = type,
                                        iconUrl = this._iconUrl
                                    }
                                );

                                Console.WriteLine(new { Key, wasUpdated });
                            };

                            if (doupdate)
                                this.update();

                        }
                    );
                }
                );

        }

    }
}
