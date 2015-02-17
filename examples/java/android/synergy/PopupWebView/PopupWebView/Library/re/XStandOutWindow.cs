using android.app;
using android.view;
using System;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.Extensions;
using android.view.animation;
using java.lang;
using android.content;
using android.os;
using android.widget;
using java.util;
using PopupWebView.Library.re;
using android.graphics;
using System.Collections.Generic;

namespace PopupWebView.Library
{
    public abstract class XStandOutWindow : Service
    {
        public class StandOutLayoutParams : WindowManager_LayoutParams, Parcelable
        {
            public const int AUTO_POSITION = -2147483647;
            public const int BOTTOM = 2147483647;
            public const int CENTER = -2147483648;
            public const int LEFT = 0;
            public const int RIGHT = 2147483647;
            public const int TOP = 0;

            public int maxHeight;
            public int maxWidth;
            public int minHeight;
            public int minWidth;
            public int threshold;

            XStandOutWindow that;

            public StandOutLayoutParams(XStandOutWindow that, int id)
                : base(200, 200, TYPE_PHONE,
                        StandOutLayoutParams.FLAG_NOT_TOUCH_MODAL
                                | StandOutLayoutParams.FLAG_WATCH_OUTSIDE_TOUCH,
                        PixelFormat.TRANSLUCENT)
            {
                this.that = that;


                int windowFlags = that.getFlags(id);

                setFocusFlag(false);

                if (!XUtils.isSet(windowFlags,
                        XStandOutFlags.FLAG_WINDOW_EDGE_LIMITS_ENABLE))
                {
                    // windows may be moved beyond edges
                    flags |= FLAG_LAYOUT_NO_LIMITS;
                }

                x = getX(id, width);
                y = getY(id, height);

                gravity = Gravity.TOP | Gravity.LEFT;

                threshold = 10;
                minWidth = minHeight = 0;
                maxWidth = maxHeight = Integer.MAX_VALUE;
            }
            public StandOutLayoutParams(XStandOutWindow that, int id, int w, int h)
                : this(that, id)
            {
                width = w;
                height = h;
            }

            public StandOutLayoutParams(XStandOutWindow that, int id, int w, int h, int xpos, int ypos)
                : this(that, id, w, h)
            {
                if (xpos != AUTO_POSITION)
                {
                    x = xpos;
                }
                if (ypos != AUTO_POSITION)
                {
                    y = ypos;
                }

                Display display = that.mWindowManager.getDefaultDisplay();
                int width = display.getWidth();
                int height = display.getHeight();

                if (x == RIGHT)
                {
                    x = width - w;
                }
                else if (x == CENTER)
                {
                    x = (width - w) / 2;
                }

                if (y == BOTTOM)
                {
                    y = height - h;
                }
                else if (y == CENTER)
                {
                    y = (height - h) / 2;
                }
            }
            public StandOutLayoutParams(XStandOutWindow that, int id, int w, int h, int xpos, int ypos, int minWidth, int minHeight)
                : this(that, id, w, h, xpos, ypos)
            {

                this.minWidth = minWidth;
                this.minHeight = minHeight;
            }

            public StandOutLayoutParams(XStandOutWindow that, int id, int w, int h, int xpos, int ypos, int minWidth, int minHeight, int threshold)
                : this(that, id, w, h, xpos, ypos, minWidth, minHeight)
            {
                this.threshold = threshold;
            }

            // helper to create cascading windows
            private int getX(int id, int width)
            {
                Display display = that.mWindowManager.getDefaultDisplay();
                int displayWidth = display.getWidth();

                int types = XStandOutWindow.sWindowCache.size();

                int initialX = 100 * types;
                int variableX = 100 * id;
                int rawX = initialX + variableX;

                return rawX % (displayWidth - width);
            }

            // helper to create cascading windows
            private int getY(int id, int height)
            {
                Display display = that.mWindowManager.getDefaultDisplay();
                int displayWidth = display.getWidth();
                int displayHeight = display.getHeight();

                int types = XStandOutWindow.sWindowCache.size();

                int initialY = 100 * types;
                int variableY = x + 200 * (100 * id) / (displayWidth - width);

                int rawY = initialY + variableY;

                return rawY % (displayHeight - height);
            }

            public virtual void setFocusFlag(bool focused)
            {
                if (focused)
                {
                    flags = flags ^ StandOutLayoutParams.FLAG_NOT_FOCUSABLE;
                }
                else
                {
                    flags = flags | StandOutLayoutParams.FLAG_NOT_FOCUSABLE;
                }
            }
        }

        public const string ACTION_CLOSE = "CLOSE";
        public const string ACTION_CLOSE_ALL = "CLOSE_ALL";
        public const string ACTION_HIDE = "HIDE";
        public const string ACTION_RESTORE = "RESTORE";
        public const string ACTION_SEND_DATA = "SEND_DATA";
        public const string ACTION_SHOW = "SHOW";
        public const int DEFAULT_ID = 0;
        public const int DISREGARD_ID = -2;
        public const int ONGOING_NOTIFICATION_ID = -1;


        // internal map of ids to shown/hidden views
        public static XWindowCache sWindowCache;
        static XWindow sFocusedWindow;

        // static constructors
        static XStandOutWindow()
        {
            sWindowCache = new XWindowCache();
            sFocusedWindow = null;
        }

        // internal system services
        public WindowManager mWindowManager;
        public NotificationManager mNotificationManager;
        //LayoutInflater mLayoutInflater;

        // internal state variables
        private bool startedForeground;


        public virtual void bringToFront(int id)
        {
            XWindow window = getWindow(id);
            if (window == null)
            {
                throw new System.Exception("Tried to bringToFront(" + id
                        + ") a null window.");
            }

            if (window.visibility == XWindow.VISIBILITY_GONE)
            {
                throw new System.Exception("Tried to bringToFront(" + id
                        + ") a window that is not shown.");
            }

            if (window.visibility == XWindow.VISIBILITY_TRANSITION)
            {
                return;
            }

            // alert callbacks and cancel if instructed
            if (onBringToFront(id, window))
            {
                //Log.w(TAG, "Window " + id
                //+ " bring to front cancelled by implementation.");
                return;
            }

            StandOutLayoutParams @params = (StandOutLayoutParams)window.getLayoutParams();

            // remove from window manager then add back
            try
            {
                mWindowManager.removeView(window);
            }
            catch // (Exception ex) 
            {
                //ex.printStackTrace();
            }
            try
            {
                mWindowManager.addView(window, @params);
            }
            catch //(Exception ex) 
            {
                //ex.printStackTrace();
            }
        }

        public virtual void close(int id)
        {
            // get the view corresponding to the id
            XWindow window = getWindow(id);

            if (window == null)
            {
                throw new System.Exception("Tried to close(" + id
                        + ") a null window.");
            }

            if (window.visibility == XWindow.VISIBILITY_TRANSITION)
            {
                return;
            }

            // alert callbacks and cancel if instructed
            if (onClose(id, window))
            {
                //Log.w(TAG, "Window " + id + " close cancelled by implementation.");
                return;
            }

            // remove hidden notification
            mNotificationManager.cancel(this.GetType().ToClass().GetHashCode() + id);

            unfocus(window);

            window.visibility = XWindow.VISIBILITY_TRANSITION;

            // get animation
            Animation animation = getCloseAnimation(id);

            // remove window
            try
            {
                // animate
                //if (animation != null) {
                //    animation.setAnimationListener(new AnimationListener() {

                //        @Override
                //        public void onAnimationStart(Animation animation) {
                //        }

                //        @Override
                //        public void onAnimationRepeat(Animation animation) {
                //        }

                //        @Override
                //        public void onAnimationEnd(Animation animation) {
                //            // remove the window from the window manager
                //            mWindowManager.removeView(window);
                //            window.visibility = Window.VISIBILITY_GONE;

                //            // remove view from internal map
                //            sWindowCache.removeCache(id,
                //                    StandOutWindow.this.getClass());

                //            // if we just released the last window, quit
                //            if (getExistingIds().size() == 0) {
                //                // tell Android to remove the persistent
                //                // notification
                //                // the Service will be shutdown by the system on low
                //                // memory
                //                startedForeground = false;
                //                stopForeground(true);
                //            }
                //        }
                //    });
                //    window.getChildAt(0).startAnimation(animation);
                //} else
                {
                    // remove the window from the window manager
                    mWindowManager.removeView(window);

                    // remove view from internal map
                    sWindowCache.removeCache(id, this.GetType().ToClass());

                    // if we just released the last window, quit
                    if (sWindowCache.getCacheSize(this.GetType().ToClass()) == 0)
                    {
                        // tell Android to remove the persistent notification
                        // the Service will be shutdown by the system on low memory
                        startedForeground = false;
                        stopForeground(true);
                    }
                }
            }
            catch // (Exception ex)
            {
                //ex.printStackTrace();
            }
        }

        public static void close(Context context, Class cls, int id)
        {
            context.startService(getCloseIntent(context, cls, id));
        }

        public virtual void closeAll()
        {
            // alert callbacks and cancel if instructed
            if (onCloseAll())
            {
                //Log.w(TAG, "Windows close all cancelled by implementation.");
                return;
            }

            // add ids to temporary set to avoid concurrent modification
            var ids = getExistingIds().toArray();


            // close each window
            foreach (int id in ids)
            {
                close(id);
            }
        }

        public static void closeAll(Context context, Class cls)
        {
            context.startService(getCloseAllIntent(context, cls));
        }
        public abstract void createAndAttachView(int arg0, FrameLayout arg1);
        public virtual bool focus(int id)
        {
            // check if that window is focusable
            XWindow window = getWindow(id);
            if (window == null)
            {
                throw new System.Exception("Tried to focus(" + id
                        + ") a null window.");
            }

            if (!XUtils.isSet(window.flags,
                    XStandOutFlags.FLAG_WINDOW_FOCUSABLE_DISABLE))
            {
                // remove focus from previously focused window
                if (sFocusedWindow != null)
                {
                    unfocus(sFocusedWindow);
                }

                return window.onFocus(true);
            }

            return false;
        }
        public abstract int getAppIcon();
        public abstract string getAppName();
        public static Intent getCloseAllIntent(Context context, Class cls)
        {
            return new Intent(context, cls).setAction(ACTION_CLOSE_ALL);
        }
        public virtual Animation getCloseAnimation(int value)
        {
            return AnimationUtils.loadAnimation(this, android.R.anim.fade_out);
        }
        public static Intent getCloseIntent(Context context, Class cls, int id)
        {
            return new Intent(context, cls).putExtra("id", id).setAction(
                ACTION_CLOSE);
        }
        public virtual PopupWindow getDropDown(int value)
        {
            return null;
        }
        public virtual List getDropDownItems(int value)
        {
            return null;
        }
        public virtual Set getExistingIds()
        {
            return sWindowCache.getCacheIds(GetType().ToClass());
        }
        public virtual int getFlags(int value)
        {
            return 0;
        }
        public virtual XWindow getFocusedWindow()
        {
            return sFocusedWindow;
        }
        public virtual int getHiddenIcon()
        {
            return getAppIcon();
        }
        public virtual Notification getHiddenNotification(int id)
        {
            // same basics as getPersistentNotification()
            int icon = getHiddenIcon();
            long when = java.lang.System.currentTimeMillis();
            Context c = getApplicationContext();
            string contentTitle = getHiddenNotificationTitle(id);
            string contentText = getHiddenNotificationMessage(id);
            string tickerText = contentTitle + ": " + contentText;

            // the difference here is we are providing the same id
            Intent notificationIntent = getHiddenNotificationIntent(id);

            PendingIntent contentIntent = null;

            if (notificationIntent != null)
            {
                contentIntent = PendingIntent.getService(this, 0,
                        notificationIntent,
                    // flag updates existing persistent notification
                        PendingIntent.FLAG_UPDATE_CURRENT);
            }

            Notification notification = new Notification(icon, tickerText, when);
            notification.setLatestEventInfo(c, contentTitle, contentText,
                    contentIntent);
            return notification;
        }
        public virtual Intent getHiddenNotificationIntent(int value)
        {
            return null;
        }
        public virtual string getHiddenNotificationMessage(int value)
        {
            return "";
        }
        public virtual string getHiddenNotificationTitle(int value)
        {
            return getAppName() + " Hidden";
        }
        public virtual Animation getHideAnimation(int value)
        {
            return AnimationUtils.loadAnimation(this, android.R.anim.fade_out);
        }
        public static Intent getHideIntent(Context context, Class cls, int id)
        {
            return new Intent(context, cls).putExtra("id", id).setAction(
                ACTION_HIDE);
        }
        public virtual int getIcon(int value)
        {
            return getAppIcon();
        }
        public abstract StandOutLayoutParams getParams(int id, XWindow window);

        public class PersistentNotification
        {
            public Notification Notification;
            public string contentTitle;
            public string contentText;
            public PendingIntent contentIntent;

            public Context context;
            public int id;

            public Action update;
        }
        public System.Collections.Generic.List<PersistentNotification> PersistentNotifications = new System.Collections.Generic.List<PersistentNotification>();

        public virtual PersistentNotification getPersistentNotification(int id)
        {
            // basic notification stuff
            // http://developer.android.com/guide/topics/ui/notifiers/notifications.html
            int icon = getAppIcon();
            long when = java.lang.System.currentTimeMillis();
            Context c = getApplicationContext();
            string contentTitle = getPersistentNotificationTitle(id);
            string contentText = getPersistentNotificationMessage(id);
            string tickerText = contentTitle + ": " + contentText;

            // getPersistentNotification() is called for every new window
            // so we replace the old notification with a new one that has
            // a bigger id
            Intent notificationIntent = getPersistentNotificationIntent(id);

            PendingIntent contentIntent = null;

            if (notificationIntent != null)
            {
                contentIntent = PendingIntent.getService(this, 0,
                        notificationIntent,
                    // flag updates existing persistent notification
                        PendingIntent.FLAG_UPDATE_CURRENT);
            }

            Notification notification = new Notification(icon, tickerText, when);
            notification.setLatestEventInfo(c, contentTitle, contentText,
                    contentIntent);
            var nn = new PersistentNotification
                {
                    Notification = notification,

                    id = id,
                    contentIntent = contentIntent,
                    contentText = contentText,
                    contentTitle = contentTitle,
                    context = c
                };

            PersistentNotifications.Add(nn);

            return nn;
        }
        public virtual Intent getPersistentNotificationIntent(int value)
        {
            return null;
        }
        public virtual string getPersistentNotificationMessage(int value)
        {
            return "";
        }
        public virtual string getPersistentNotificationTitle(int value)
        {
            return getAppName() + " Running";
        }
        public static Intent getSendDataIntent(Context context, Class toCls, int toId, int requestCode, Bundle data, Class fromCls, int fromId)
        {
            return new Intent(context, toCls).putExtra("id", toId)
                .putExtra("requestCode", requestCode)
                .putExtra(XStandOutWindow_data, data)
                .putExtra(XStandOutWindow_fromCls, fromCls)
                .putExtra("fromId", fromId).setAction(ACTION_SEND_DATA);
        }
        public virtual Animation getShowAnimation(int value)
        {
            return AnimationUtils.loadAnimation(this, android.R.anim.fade_in);
        }
        public static Intent getShowIntent(Context context, Class cls, int id)
        {
            bool cached = sWindowCache.isCached(id, cls);
            string action = cached ? ACTION_RESTORE : ACTION_SHOW;
            android.net.Uri uri = null;

            if (cached) uri = android.net.Uri.parse("standout://" + cls + "/" + id);
            return new Intent(context, cls).putExtra("id", id).setAction(action)
                    .setData(uri);
        }
        public virtual int getThemeStyle()
        {
            return 0;
        }
        public virtual string getTitle(int value)
        {
            return getAppName();
        }
        public virtual int getUniqueId()
        {
            int unique = DEFAULT_ID;

            foreach (int id in getExistingIds().toArray())
            {
                unique = System.Math.Max(unique, id + 1);
            }
            return unique;
        }
        public virtual XWindow getWindow(int id)
        {
            return sWindowCache.getCache(id, GetType().ToClass());
        }
        public virtual void hide(int id)
        {
            // get the view corresponding to the id
            XWindow window = getWindow(id);

            if (window == null)
            {
                throw new System.Exception("Tried to hide(" + id
                        + ") a null window.");
            }

            if (window.visibility == XWindow.VISIBILITY_GONE)
            {
                throw new System.Exception("Tried to hide(" + id
                        + ") a window that is not shown.");
            }

            // alert callbacks and cancel if instructed
            if (onHide(id, window))
            {
                //Log.w(TAG, "Window " + id + " hide cancelled by implementation.");
                return;
            }

            // check if hide enabled
            if (XUtils.isSet(window.flags, XStandOutFlags.FLAG_WINDOW_HIDE_ENABLE))
            {
                window.visibility = XWindow.VISIBILITY_TRANSITION;

                // get the hidden notification for this view
                Notification notification = getHiddenNotification(id);

                // get animation
                Animation animation = getHideAnimation(id);

                try
                {
                    // animate
                    //if (animation != null) {
                    //    animation.setAnimationListener(new AnimationListener() {

                    //        @Override
                    //        public void onAnimationStart(Animation animation) {
                    //        }

                    //        @Override
                    //        public void onAnimationRepeat(Animation animation) {
                    //        }

                    //        @Override
                    //        public void onAnimationEnd(Animation animation) {
                    //            // remove the window from the window manager
                    //            mWindowManager.removeView(window);
                    //            window.visibility = Window.VISIBILITY_GONE;
                    //        }
                    //    });
                    //    window.getChildAt(0).startAnimation(animation);
                    //} else {
                    // remove the window from the window manager
                    mWindowManager.removeView(window);
                    //}
                }
                catch// (Exception ex) 
                {
                    //ex.printStackTrace();
                }

                // display the notification
                notification.flags = notification.flags
                        | Notification.FLAG_NO_CLEAR
                        | Notification.FLAG_AUTO_CANCEL;

                mNotificationManager.notify(GetType().ToClass().GetHashCode() + id,
                        notification);

            }
            else
            {
                // if hide not enabled, close window
                close(id);
            }
        }
        public static void hide(Context context, Class cls, int id)
        {
            context.startService(getShowIntent(context, cls, id));
        }
        public virtual bool isExistingId(int id)
        {
            return sWindowCache.isCached(id, GetType().ToClass());
        }

        public override IBinder onBind(Intent value)
        {
            return null;
        }
        public virtual bool onBringToFront(int arg0, XWindow arg1)
        {
            return false;
        }
        public virtual bool onClose(int arg0, XWindow arg1)
        {
            return false;
        }
        public virtual bool onCloseAll()
        {
            return false;
        }
        public override void onCreate()
        {
            base.onCreate();

            mWindowManager = (WindowManager)getSystemService(Context.WINDOW_SERVICE);
            mNotificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);
            //mLayoutInflater = (LayoutInflater)getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            startedForeground = false;
        }
        public override void onDestroy()
        {
            base.onDestroy();

            // closes all windows
            closeAll();
        }
        public virtual bool onFocusChange(int arg0, XWindow arg1, bool arg2)
        {
            return false;
        }
        public virtual bool onHide(int arg0, XWindow arg1)
        {
            return false;
        }
        public virtual bool onKeyEvent(int arg0, XWindow arg1, KeyEvent arg2)
        {
            return false;
        }
        public virtual void onMove(int arg0, XWindow arg1, View arg2, MotionEvent arg3)
        { }
        public virtual void onReceiveData(int arg0, int arg1, Bundle arg2, Class arg3, int arg4)
        { }

        //public event Action SizeChanged;

        public virtual void onResize(int arg0, XWindow arg1, View arg2, MotionEvent arg3)
        {
            //if (SizeChanged != null)
            //    SizeChanged();
        }

        public virtual bool onShow(int arg0, XWindow arg1)
        {
            return false;
        }
        public override int onStartCommand(Intent intent, int flags, int startId)
        {
            base.onStartCommand(intent, flags, startId);

            // intent should be created with
            // getShowIntent(), getHideIntent(), getCloseIntent()
            if (intent != null)
            {
                string action = intent.getAction();
                int id = intent.getIntExtra("id", DEFAULT_ID);

                // this will interfere with getPersistentNotification()
                if (id == ONGOING_NOTIFICATION_ID)
                {
                    throw new System.Exception(
                            "ID cannot equals StandOutWindow.ONGOING_NOTIFICATION_ID");
                }

                if (ACTION_SHOW == action || ACTION_RESTORE == action)
                {
                    show(id);
                }
                else if (ACTION_HIDE == action)
                {
                    hide(id);
                }
                else if (ACTION_CLOSE == action)
                {
                    close(id);
                }
                else if (ACTION_CLOSE_ALL == action)
                {
                    closeAll();
                }
                else if (ACTION_SEND_DATA == action)
                {
                    if (!isExistingId(id) && id != DISREGARD_ID)
                    {
                        //Log.w(TAG,
                        //        "Sending data to non-existant window. If this is not intended, make sure toId is either an existing window's id or DISREGARD_ID.");
                    }
                    Bundle data = intent.getBundleExtra(XStandOutWindow_data);
                    int requestCode = intent.getIntExtra("requestCode", 0);
                    //@SuppressWarnings("unchecked")
                    Class fromCls = (Class)intent.getSerializableExtra(XStandOutWindow_fromCls);
                    int fromId = intent.getIntExtra("fromId", DEFAULT_ID);
                    onReceiveData(id, requestCode, data, fromCls, fromId);
                }
            }
            else
            {
                //Log.w(TAG, "Tried to onStartCommand() with a null intent.");
            }

            // the service is started in foreground in show()
            // so we don't expect Android to kill this service
            return START_NOT_STICKY;
        }


        public const string XStandOutWindow_data = "XStandOutWindow_data";
        public const string XStandOutWindow_fromCls = "XStandOutWindow_fromCls";

        public virtual bool onTouchBody(int arg0, XWindow arg1, View arg2, MotionEvent arg3)
        {
            return false;
        }
        public virtual bool onTouchHandleMove(int id, XWindow window, View view, MotionEvent @event)
        {
            var @params = (StandOutLayoutParams)window.getLayoutParams();

            // how much you have to move in either direction in order for the
            // gesture to be a move and not tap

            int totalDeltaX = window.touchInfo.lastX - window.touchInfo.firstX;
            int totalDeltaY = window.touchInfo.lastY - window.touchInfo.firstY;

            if (@event.getAction() == MotionEvent.ACTION_DOWN)
            {
                window.touchInfo.lastX = (int)@event.getRawX();
                window.touchInfo.lastY = (int)@event.getRawY();

                window.touchInfo.firstX = window.touchInfo.lastX;
                window.touchInfo.firstY = window.touchInfo.lastY;
            }
            else if (@event.getAction() == MotionEvent.ACTION_MOVE)
            {
                int deltaX = (int)@event.getRawX() - window.touchInfo.lastX;
                int deltaY = (int)@event.getRawY() - window.touchInfo.lastY;

                window.touchInfo.lastX = (int)@event.getRawX();
                window.touchInfo.lastY = (int)@event.getRawY();

                if (window.touchInfo.moving
                        || System.Math.Abs(totalDeltaX) >= @params.threshold
                        || System.Math.Abs(totalDeltaY) >= @params.threshold)
                {
                    window.touchInfo.moving = true;

                    // if window is moveable
                    if (XUtils.isSet(window.flags,
                            XStandOutFlags.FLAG_BODY_MOVE_ENABLE))
                    {

                        // update the position of the window
                        if (@event.getPointerCount() == 1)
                        {
                            @params.x += deltaX;
                            @params.y += deltaY;
                        }

                        window.edit().setPosition(@params.x, @params.y).commit();
                    }
                }
            }
            else if (@event.getAction() == MotionEvent.ACTION_UP)
            {
                window.touchInfo.moving = false;

                if (@event.getPointerCount() == 1)
                {

                    // bring to front on tap
                    var tap = System.Math.Abs(totalDeltaX) < @params.threshold
                            && System.Math.Abs(totalDeltaY) < @params.threshold;
                    if (tap
                            && XUtils.isSet(
                                    window.flags,
                                    XStandOutFlags.FLAG_WINDOW_BRING_TO_FRONT_ON_TAP))
                    {
                        this.bringToFront(id);
                    }
                }

                // bring to front on touch
                else if (XUtils.isSet(window.flags,
                        XStandOutFlags.FLAG_WINDOW_BRING_TO_FRONT_ON_TOUCH))
                {
                    this.bringToFront(id);
                }

            }

            onMove(id, window, view, @event);

            return true;
        }
        public virtual bool onTouchHandleResize(int id, XWindow window, View view, MotionEvent @event)
        {
            StandOutLayoutParams @params = (StandOutLayoutParams)window
                .getLayoutParams();

            if (@event.getAction() == MotionEvent.ACTION_DOWN)
            {
                window.touchInfo.lastX = (int)@event.getRawX();
                window.touchInfo.lastY = (int)@event.getRawY();

                window.touchInfo.firstX = window.touchInfo.lastX;
                window.touchInfo.firstY = window.touchInfo.lastY;
            }
            else
                if (@event.getAction() == MotionEvent.ACTION_MOVE)
                {
                    int deltaX = (int)@event.getRawX() - window.touchInfo.lastX;
                    int deltaY = (int)@event.getRawY() - window.touchInfo.lastY;

                    // update the size of the window
                    @params.width += deltaX;
                    @params.height += deltaY;

                    // keep window between min/max width/height
                    if (@params.width >= @params.minWidth
                            && @params.width <= @params.maxWidth)
                    {
                        window.touchInfo.lastX = (int)@event.getRawX();
                    }

                    if (@params.height >= @params.minHeight
                            && @params.height <= @params.maxHeight)
                    {
                        window.touchInfo.lastY = (int)@event.getRawY();
                    }

                    window.edit().setSize(@params.width, @params.height).commit();
                }

            onResize(id, window, view, @event);

            return true;
        }

        public virtual bool onUpdate(int arg0, XWindow arg1, XStandOutWindow.StandOutLayoutParams arg2)
        {
            return false;
        }
        public virtual void sendData(int fromId, Class toCls, int toId, int requestCode, Bundle data)
        {
            XStandOutWindow.sendData(this, toCls, toId, requestCode, data,
                GetType
                ().ToClass(), fromId);
        }
        public static void sendData(Context context, Class toCls, int toId, int requestCode, Bundle data, Class fromCls, int fromId)
        {
            context.startService(getSendDataIntent(context, toCls, toId,
                requestCode, data, fromCls, fromId));
        }
        public virtual void setFocusedWindow(XWindow window)
        {
            sFocusedWindow = window;
        }
        public virtual void setIcon(int arg0, int arg1)
        { }
        public virtual void setTitle(int arg0, string arg1)
        { }
        public virtual XWindow show(int id)
        {
            // get the window corresponding to the id
            XWindow cachedWindow = getWindow(id);
            XWindow window;

            // check cache first
            if (cachedWindow != null)
            {
                window = cachedWindow;
            }
            else
            {
                window = new XWindow(this, id);
            }

            if (window.visibility == XWindow.VISIBILITY_VISIBLE)
            {
                throw new System.Exception("Tried to show(" + id
                        + ") a window that is already shown.");
            }

            // alert callbacks and cancel if instructed
            if (onShow(id, window))
            {
                //Log.d(TAG, "Window " + id + " show cancelled by implementation.");
                return null;
            }

            window.visibility = XWindow.VISIBILITY_VISIBLE;

            // get animation
            Animation animation = getShowAnimation(id);

            // get the params corresponding to the id
            StandOutLayoutParams @params = (StandOutLayoutParams)window.getLayoutParams();

            try
            {
                // add the view to the window manager
                mWindowManager.addView(window, @params);

                // animate
                if (animation != null)
                {
                    window.getChildAt(0).startAnimation(animation);
                }
            }
            catch // (Exception ex) 
            {
                //ex.printStackTrace();
            }

            // add view to internal map
            sWindowCache.putCache(id, GetType().ToClass(), window);

            // get the persistent notification
            var nn = getPersistentNotification(id);
            var notification = nn.Notification;
            // show the notification
            if (notification != null)
            {
                notification.flags = notification.flags
                        | Notification.FLAG_NO_CLEAR;

                nn.update = delegate
                {
                    mNotificationManager.notify(
                        GetType().ToClass().GetHashCode()
                           + ONGOING_NOTIFICATION_ID, notification);
                };
                // only show notification if not shown before
                if (!startedForeground)
                {
                    // tell Android system to show notification
                    startForeground(
                            GetType().ToClass().GetHashCode() + ONGOING_NOTIFICATION_ID,
                            notification);
                    startedForeground = true;
                }
                else
                {
                    // update notification if shown before
                    nn.update();

                }
            }
            else
            {
                // notification can only be null if it was provided before
                if (!startedForeground)
                {
                    throw new System.Exception("Your StandOutWindow service must"
                            + "provide a persistent notification."
                            + "The notification prevents Android"
                            + "from killing your service in low"
                            + "memory situations.");
                }
            }

            focus(id);

            return window;
        }
        public static void show(Context context, Class cls, int id)
        {
            context.startService(getShowIntent(context, cls, id));
        }
        public virtual bool unfocus(int id)
        {
            XWindow window = getWindow(id);
            return unfocus(window);
        }
        public virtual bool unfocus(XWindow window)
        {
            if (window == null)
            {
                throw new System.Exception(
                        "Tried to unfocus a null window.");
            }
            return window.onFocus(false);
        }
        public virtual void updateViewLayout(int id, XStandOutWindow.StandOutLayoutParams @params)
        {
            XWindow window = getWindow(id);

            if (window == null)
            {
                throw new System.Exception("Tried to updateViewLayout("
                        + id + ") a null window.");
            }

            if (window.visibility == XWindow.VISIBILITY_GONE)
            {
                return;
            }

            if (window.visibility == XWindow.VISIBILITY_TRANSITION)
            {
                return;
            }

            // alert callbacks and cancel if instructed
            if (onUpdate(id, window, @params))
            {
                //Log.w(TAG, "Window " + id + " update cancelled by implementation.");
                return;
            }

            try
            {
                window.setLayoutParams(@params);
                mWindowManager.updateViewLayout(window, @params);
            }
            catch// (Exception ex) 
            {
                //ex.printStackTrace();
            }
        }

        //public class DropDownListItem
        //{
        //    public Runnable action;
        //    public string description;
        //    public int icon;

        //    protected DropDownListItem();
        //    public DropDownListItem(XStandOutWindow arg0, int arg1, string arg2, Runnable arg3);

        //    public override string ToString();
        //}


    }
}
