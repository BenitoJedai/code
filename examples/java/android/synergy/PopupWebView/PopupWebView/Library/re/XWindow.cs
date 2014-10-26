using android.content;
using android.graphics.drawable;
using android.os;
using android.view;
using android.view.accessibility;
using android.widget;
using java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.Extensions;
using android.util;
using PopupWebView.Library.re;

namespace PopupWebView.Library
{
    public class XWindow : FrameLayout, Drawable.Callback, KeyEvent.Callback, AccessibilityEventSource, ViewParent, ViewManager
    {
        public const int VISIBILITY_GONE = 0;
        public const int VISIBILITY_TRANSITION = 2;
        public const int VISIBILITY_VISIBLE = 1;

        public Class cls;
        public Bundle data;
        public int flags;
        public bool focused;
        public int id;
        public XStandOutWindow.StandOutLayoutParams originalParams;
        public XTouchInfo touchInfo;
        public int visibility;

        public XStandOutWindow mContext;

        //location: class PopupWebView.Library.XWindow
        //super(class35.context);

        public XWindow(Context xcontext)
            : base(xcontext)
        {
            // ?
            mContext = null;
        }

        public int displayWidth, displayHeight;

        class XOnTouchListener : OnTouchListener
        {
            public Func<View, MotionEvent, bool> yield;

            public bool onTouch(View arg0, MotionEvent arg1)
            {
                return yield(arg0, arg1);
            }
        }

        public XWindow(XStandOutWindow xcontext, int id)
            : base(xcontext)
        {
            var context = xcontext;

            context.setTheme(context.getThemeStyle());

            mContext = context;
            //mLayoutInflater = LayoutInflater.from(context);

            this.cls = context.GetType().ToClass();
            this.id = id;
            this.originalParams = context.getParams(id, this);
            this.flags = context.getFlags(id);
            this.touchInfo = new XTouchInfo();
            touchInfo.ratio = (float)originalParams.width / originalParams.height;
            this.data = new Bundle();
            DisplayMetrics metrics = mContext.getResources()
                    .getDisplayMetrics();
            displayWidth = metrics.widthPixels;
            displayHeight = (int)(metrics.heightPixels - 25 * metrics.density);

            // create the window contents
            View content;
            FrameLayout body;


            content = new FrameLayout(context);
            body = (FrameLayout)content;

            addView(content);

            body.setOnTouchListener(new XOnTouchListener
            {

                yield = (View v, MotionEvent @event) =>
                {
                    // pass all touch events to the implementation
                    var consumed = false;

                    // handle move and bring to front
                    consumed = context.onTouchHandleMove(id, this, v, @event)
                            || consumed;

                    // alert implementation
                    consumed = context.onTouchBody(id, this, v, @event)
                            || consumed;

                    return consumed;
                }
            });

            // attach the view corresponding to the id from the
            // implementation
            context.createAndAttachView(id, body);

            // make sure the implementation attached the view
            if (body.getChildCount() == 0)
            {
                throw new System.Exception(
                        "You must attach your view to the given frame in createAndAttachView()");
            }

            //// implement StandOut specific workarounds
            //if (!XUtils.isSet(flags,
            //        XStandOutFlags.FLAG_FIX_COMPATIBILITY_ALL_DISABLE))
            //{
            //    fixCompatibility(body);
            //}
            //// implement StandOut specific additional functionality
            //if (!XUtils.isSet(flags,
            //        XStandOutFlags.FLAG_ADD_FUNCTIONALITY_ALL_DISABLE))
            //{
            //    addFunctionality(body);
            //}

            // attach the existing tag from the frame to the window
            setTag(body.getTag());
        }

        public override bool dispatchKeyEvent(KeyEvent @event)
        {
            if (mContext.onKeyEvent(id, this, @event))
            {
                //Log.d(TAG, "Window " + id + " key event " + @event
                //        + " cancelled by implementation.");
                return false;
            }

            if (@event.getAction() == KeyEvent.ACTION_UP)
            {
                if (@event.getKeyCode() == KeyEvent.KEYCODE_BACK)
                {
                    mContext.unfocus(this);
                    return true;
                }
            }

            return base.dispatchKeyEvent(@event);
        }

        public virtual XWindow.Editor edit()
        {
            return new Editor(this);
        }

        public override ViewGroup.LayoutParams getLayoutParams()
        {
            XStandOutWindow.StandOutLayoutParams @params = (XStandOutWindow.StandOutLayoutParams)base.getLayoutParams();
            if (@params == null)
            {
                @params = originalParams;
            }
            return @params;
        }
        public virtual bool onFocus(bool focus)
        {
            if (!XUtils.isSet(flags, XStandOutFlags.FLAG_WINDOW_FOCUSABLE_DISABLE))
            {
                // window is focusable

                if (focus == focused)
                {
                    // window already focused/unfocused
                    return false;
                }

                focused = focus;

                // alert callbacks and cancel if instructed
                if (mContext.onFocusChange(id, this, focus))
                {
                    //Log.d(TAG, "Window " + id + " focus change "
                    //        + (focus ? "(true)" : "(false)")
                    //        + " cancelled by implementation.");
                    focused = !focus;
                    return false;
                }



                // set window manager params
                XStandOutWindow.StandOutLayoutParams @params = (XStandOutWindow.StandOutLayoutParams)getLayoutParams();
                @params.setFocusFlag(focus);
                mContext.updateViewLayout(id, @params);

                if (focus)
                {
                    mContext.setFocusedWindow(this);
                }
                else
                {
                    if (mContext.getFocusedWindow() == this)
                    {
                        mContext.setFocusedWindow(null);
                    }
                }

                return true;
            }
            return false;
        }
        public override bool onInterceptTouchEvent(MotionEvent @event)
        {
            XStandOutWindow.StandOutLayoutParams @params = (XStandOutWindow.StandOutLayoutParams)getLayoutParams();

            // focus window
            if (@event.getAction() == MotionEvent.ACTION_DOWN)
            {
                if (mContext.getFocusedWindow() != this)
                {
                    mContext.focus(id);
                }
            }

            // multitouch

            // script: error JSC1000: Java : Opcode not implemented: brfalse.s at PopupWebView.Library.XWindow.onInterceptTouchEvent

            var flag1 = @event.getPointerCount() >= 2;
            var flag2 = (@event.getAction() & MotionEvent.ACTION_MASK) == MotionEvent.ACTION_POINTER_DOWN;

            var flag = flag1
                    && XUtils.isSet(flags, XStandOutFlags.FLAG_WINDOW_PINCH_RESIZE_ENABLE)
                    && flag2;

            if (flag)
            {
                touchInfo.scale = 1;
                touchInfo.dist = -1;
                touchInfo.firstWidth = @params.width;
                touchInfo.firstHeight = @params.height;
                return true;
            }

            return false;
        }
        public override bool onTouchEvent(MotionEvent @event)
        {
            // handle touching outside
            if (@event.getAction() == MotionEvent.ACTION_OUTSIDE)
            {
                // unfocus window
                if (mContext.getFocusedWindow() == this)
                {
                    mContext.unfocus(this);
                }

                // notify implementation that ACTION_OUTSIDE occurred
                mContext.onTouchBody(id, this, this, @event);
            }

            // handle multitouch
            if (@event.getPointerCount() >= 2
                    && XUtils.isSet(flags,
                            XStandOutFlags.FLAG_WINDOW_PINCH_RESIZE_ENABLE))
            {
                // 2 fingers or more

                float x0 = @event.getX(0);
                float y0 = @event.getY(0);
                float x1 = @event.getX(1);
                float y1 = @event.getY(1);

                double dist = System.Math
                        .Sqrt(System.Math.Pow(x0 - x1, 2) + System.Math.Pow(y0 - y1, 2));

                if ((@event.getAction() & MotionEvent.ACTION_MASK) == MotionEvent.ACTION_MOVE)
                {
                    if (touchInfo.dist == -1)
                    {
                        touchInfo.dist = dist;
                    }
                    touchInfo.scale *= dist / touchInfo.dist;
                    touchInfo.dist = dist;

                    // scale the window with anchor point set to middle
                    edit().setAnchorPoint(.5f, .5f)
                            .setSize(
                                    (int)(touchInfo.firstWidth * touchInfo.scale),
                                    (int)(touchInfo.firstHeight * touchInfo.scale))
                            .commit();
                }
                mContext.onResize(id, this, this, @event);
            }

            return true;
        }
        public override void setLayoutParams(ViewGroup.LayoutParams @params)
        {
            if (@params is XStandOutWindow.StandOutLayoutParams)
            {
                base.setLayoutParams(@params);
            }
            else
            {
                throw new System.Exception(
                        "Window"
                                + id
                                + ": LayoutParams must be an instance of StandOutLayoutParams.");
            }
        }

        public class Editor
        {
            public const int UNCHANGED = -2147483648;

            XWindow that;
            float anchorX, anchorY;
            XStandOutWindow.StandOutLayoutParams mParams;

            public Editor(XWindow that)
            {
                this.that = that;

                mParams = (XStandOutWindow.StandOutLayoutParams)this.that.getLayoutParams();
                anchorX = 0;
                anchorY = 0;
            }

            public virtual void commit()
            {
                if (mParams != null)
                {
                    that.mContext.updateViewLayout(that.id, mParams);
                    mParams = null;
                }
            }

            public virtual XWindow.Editor setAnchorPoint(float x, float y)
            {
                var flag0 = x < 0 || x > 1 || y < 0 || y > 1;

                if (flag0)
                {
                    throw new System.Exception(
                            "Anchor point must be between 0 and 1, inclusive." + new { x, y });
                }

                anchorX = x;
                anchorY = y;

                return this;
            }
            public virtual XWindow.Editor setPosition(float percentWidth, float percentHeight)
            {
                return setPosition((int)(that.displayWidth * percentWidth),
                    (int)(that.displayHeight * percentHeight));
            }

            public virtual XWindow.Editor setPosition(int x, int y)
            {
                return setPosition(x, y, false);
            }

            private Editor setPosition(int x, int y, bool skip)
            {
                if (mParams != null)
                {
                    var flag0 = anchorX < 0 || anchorX > 1 || anchorY < 0 || anchorY > 1;

                    if (flag0)
                    {
                        throw new System.Exception(
                                "Anchor point must be between 0 and 1, inclusive." + new { anchorX, anchorY });
                    }


                    // sets the x and y correctly according to anchorX and
                    // anchorY
                    if (x != UNCHANGED)
                    {
                        mParams.x = (int)(x - mParams.width * anchorX);
                    }
                    if (y != UNCHANGED)
                    {
                        mParams.y = (int)(y - mParams.height * anchorY);
                    }

                    if (XUtils.isSet(that.flags,
                            XStandOutFlags.FLAG_WINDOW_EDGE_LIMITS_ENABLE))
                    {
                        // if gravity is not TOP|LEFT throw exception
                        if (mParams.gravity != (Gravity.TOP | Gravity.LEFT))
                        {
                            throw new System.Exception(
                                    "The window "
                                            + that.id
                                            + " gravity must be TOP|LEFT if FLAG_WINDOW_EDGE_LIMITS_ENABLE or FLAG_WINDOW_EDGE_TILE_ENABLE is set.");
                        }

                        // keep window inside edges
                        mParams.x = System.Math.Min(System.Math.Max(mParams.x, 0), that.displayWidth
                                - mParams.width);


                        //mParams.y = System.Math.Min(
                        //    System.Math.Max(mParams.y, 0), that.displayHeight - mParams.height
                        //);

                        mParams.y = System.Math.Max(mParams.y, 0);
                        //System.Math.Min(
                        //    System.Math.Max(mParams.y, 0), that.displayHeight - 32
                        //);
                    }
                }

                return this;
            }

            public virtual XWindow.Editor setSize(float percentWidth, float percentHeight)
            {
                return setSize((int)(that.displayWidth * percentWidth),
            (int)(that.displayHeight * percentHeight));
            }
            public virtual XWindow.Editor setSize(int width, int height)
            {
                return setSize(width, height, false);
            }

            private Editor setSize(int width, int height, bool skip)
            {
                if (mParams != null)
                {
                    var flag0 = (anchorX < 0 || anchorX > 1 || anchorY < 0 || anchorY > 1);

                    if (flag0)
                    {
                        throw new System.Exception(
                                "Anchor point must be between 0 and 1, inclusive.");
                    }

                    int lastWidth = mParams.width;
                    int lastHeight = mParams.height;

                    if (width != UNCHANGED)
                    {
                        mParams.width = width;
                    }
                    if (height != UNCHANGED)
                    {
                        mParams.height = height;
                    }

                    // set max width/height
                    int maxWidth = mParams.maxWidth;
                    int maxHeight = mParams.maxHeight;

                    if (XUtils.isSet(that.flags,
                            XStandOutFlags.FLAG_WINDOW_EDGE_LIMITS_ENABLE))
                    {
                        maxWidth = (int)System.Math.Min(maxWidth, that.displayWidth);
                        //maxHeight = (int)System.Math.Min(maxHeight, that.displayHeight);
                        //maxHeight = (int)System.Math.Min(maxHeight, that.displayHeight + mParams.height - 32);
                    }

                    // keep window between min and max
                    mParams.width = System.Math.Min(
                            System.Math.Max(mParams.width, mParams.minWidth), maxWidth);
                    mParams.height = System.Math.Min(
                            System.Math.Max(mParams.height, mParams.minHeight), maxHeight);

                    // keep window in aspect ratio
                    if (XUtils.isSet(that.flags,
                            XStandOutFlags.FLAG_WINDOW_ASPECT_RATIO_ENABLE))
                    {
                        int ratioWidth = (int)(mParams.height * that.touchInfo.ratio);
                        int ratioHeight = (int)(mParams.width / that.touchInfo.ratio);
                        if (ratioHeight >= mParams.minHeight
                                && ratioHeight <= mParams.maxHeight)
                        {
                            // width good adjust height
                            mParams.height = ratioHeight;
                        }
                        else
                        {
                            // height good adjust width
                            mParams.width = ratioWidth;
                        }
                    }

                    if (!skip)
                    {
                        // set position based on anchor point
                        setPosition((int)(mParams.x + lastWidth * anchorX),
                                (int)(mParams.y + lastHeight * anchorY));
                    }
                }

                return this;
            }
        }

        public class WindowDataKeys
        {
            public const string HEIGHT_BEFORE_MAXIMIZE = "heightBeforeMaximize";
            public const string IS_MAXIMIZED = "isMaximized";
            public const string WIDTH_BEFORE_MAXIMIZE = "widthBeforeMaximize";
            public const string X_BEFORE_MAXIMIZE = "xBeforeMaximize";
            public const string Y_BEFORE_MAXIMIZE = "yBeforeMaximize";

        }
    }
}
