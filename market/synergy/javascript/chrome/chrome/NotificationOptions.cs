extern alias xglobal;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chrome;

// since the developer 
// added us via jsc market
// and is using a namespace indicates they want our features
namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class ChromeNotificationExtensions
    {
        public static Notification ToNotification(this string Message)
        {
            return new Notification
            {
                Message = Message
            };
        }
    }
}


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

        //public static string DefaultTitle = "my.jsc-solutions.net";
        public static string DefaultTitle = null;
        public static string DefaultIconUrl = "assets/ScriptCoreLib/jsc.png";

        public Notification(
            string notificationId = null,
                string title = null,

                string message = "",

                string type = "basic",
                 string iconUrl = null

            )
        {
            //Console.WriteLine("Notification .ctor");

            if (title == null)
            {
                if (DefaultTitle == null)
                    title = Native.document.title;
                else
                    title = DefaultTitle;
            }

            if (iconUrl == null)
                iconUrl = DefaultIconUrl;

            if (notificationId == null)
            {

                //notificationId = "Notification#" + AllNotifications.Count;
                notificationId = string.Concat(
                    "Notification#", AllNotifications.Count
                );
            }

            //Console.WriteLine("Notification .ctor :139");
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

            //Console.WriteLine("Notification .ctor :158");

            Task.Delay(1).GetAwaiter().OnCompleted(
                delegate
                {
                //Console.WriteLine("Notification .ctor :63");

                Console.WriteLine("at Delay " + new { this._title, this._message });

                    // tested by
                    // X:\jsc.svn\examples\javascript\chrome\ChromeNotificationExperiment\ChromeNotificationExperiment\Application.cs
                    var doupdate = false;

                    this.update = delegate
                    {
                        doupdate = true;

                    };


                    // https://github.com/darwin/chromium-src-chrome-browser/blob/master/notifications/notification.cc
                    // https://github.com/darwin/chromium-src-chrome-browser/blob/master/notifications/notification.h
                    xglobal::chrome.notifications.create(
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
                            xglobal::chrome.notifications.Closed +=
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
                            xglobal::chrome.notifications.Clicked +=
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
                                var wasUpdated = await xglobal::chrome.notifications.update(
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

                    //Console.WriteLine(
                    //    new { chrome.runtime.lastError, chrome.runtime.id }
                    //    );
                }
                );

        }


        public static implicit operator Task(Notification n)
        {
            var x = new TaskCompletionSource<object>();

            n.Clicked += delegate
            {
                x.SetResult(null);
            };

            return x.Task;
        }
    }
}
