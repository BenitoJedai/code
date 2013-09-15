using ScriptCoreLib.JavaScript.DOM.HTML;
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

        public static List<Notification> AllNotifications = new List<Notification>();

        public Notification(
            string notificationId = null,

                string title = "my.jsc-solutions.net",

                string message = "",

                string type = "basic",
                 string iconUrl = "assets/ScriptCoreLib/jsc.png"

            )
        {

            if (notificationId == null)
                notificationId = "Notification#" + AllNotifications.Count;

            AllNotifications.Add(this);

            this._title = title;
            this._message = message;
            this._iconUrl = iconUrl;
            this.Key = notificationId;

            this.IsClosed = false;

            this.update = async delegate
            {
                var wasUpdated = await chrome.notifications.update(
                    this.Key,
                    new NotificationOptions
                    {
                        title = _title,
                        message = _message,
                        type = type,
                        iconUrl = _iconUrl
                    }
                );

                Console.WriteLine(new { Key, wasUpdated });
            };


            // tested by
            // X:\jsc.svn\examples\javascript\chrome\ChromeNotificationExperiment\ChromeNotificationExperiment\Application.cs

            chrome.notifications.create(
                this.Key,
                new NotificationOptions
                {
                    title = _title,
                    message = _message,
                    type = type,
                    iconUrl = _iconUrl
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


                }
            );
        }

    }
}
