using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using android.view;
using android.graphics;
using android.util;
using android.content.res;
using android;
using android.graphics.drawable;
using android.graphics.drawable.shapes;


namespace TestAndroidCircleProgressbar.Activities
{
    public class ApplicationActivity : Activity
    {
        CircularSeekBar circularSeekbar;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            //// http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);


            var b = new Button(this);

            b.setText("Vibrate!");

            b.AtClick(
                delegate
                {
                    var vibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);

                    vibrator.vibrate(600);
                }
            );

            ll.addView(b);





            this.setContentView(sv);


            //this.ShowLongToast("Monese circle progressbar");

            //circularSeekbar = new CircularSeekBar(this);
            //circularSeekbar.setMaxProgress(100);
            //circularSeekbar.setProgress(100);
            //setContentView(circularSeekbar);
            //circularSeekbar.invalidate();

            //circularSeekbar.setSeekBarChangeListener(new TestAndroidCircleProgressbar.Activities.CircularSeekBar.MyOnSeekChangeListener());

        }
    }

    public class CircularSeekBar : View
    {
        /** The context */
    public Context mContext;

	/** The listener to listen for changes */
	public OnSeekChangeListener mListener;

	/** The color of the progress ring */
    public Paint circleColor;

	/** the color of the inside circle. Acts as background color */
    public Paint innerColor;

	/** The progress circle ring background */
    public Paint circleRing;

	/** The angle of progress */
    public int angle = 0;

	/** The start angle (12 O'clock */
    public int startAngle = 270;

	/** The width of the progress ring */
    public int barWidth = 5;

	/** The width of the view */
    public int width;

	/** The height of the view */
    public int height;

	/** The maximum progress amount */
    public int maxProgress = 100;

	/** The current progress */
    public int progress;

	/** The progress percent */
    public int progressPercent;

	/** The radius of the inner circle */
    public float innerRadius;

	/** The radius of the outer circle */
    public float outerRadius;

	/** The circle's center X coordinate */
    public float cx;

	/** The circle's center Y coordinate */
    public float cy;

	/** The left bound for the circle RectF */
    public float left;

	/** The right bound for the circle RectF */
    public float right;

	/** The top bound for the circle RectF */
    public float top;

	/** The bottom bound for the circle RectF */
    public float bottom;

	/** The X coordinate for the top left corner of the marking drawable */
    public float dx;

	/** The Y coordinate for the top left corner of the marking drawable */
    public float dy;

	/** The X coordinate for 12 O'Clock */
    public float startPointX;

	/** The Y coordinate for 12 O'Clock */
    public float startPointY;

	/**
	 * The X coordinate for the current position of the marker, pre adjustment
	 * to center
	 */
    public float markPointX;

	/**
	 * The Y coordinate for the current position of the marker, pre adjustment
	 * to center
	 */
    public float markPointY;

	/**
	 * The adjustment factor. This adds an adjustment of the specified size to
	 * both sides of the progress bar, allowing touch events to be processed
	 * more user friendly (yes, I know that's not a word)
	 */
    public float adjustmentFactor = 100;

	/** The progress mark when the view isn't being progress modified */
    public Bitmap progressMark;

	/** The progress mark when the view is being progress modified. */
    public Bitmap progressMarkPressed;

	/** The flag to see if view is pressed */
	bool IS_PRESSED = false;

	/**
	 * The flag to see if the setProgress() method was called from our own
	 * View's setAngle() method, or externally by a user.
	 */
    public bool CALLED_FROM_ANGLE = false;

    public bool SHOW_SEEKBAR = true;

	/** The rectangle containing our circles and arcs. */
    public RectF rect = new RectF();

    public class MyOnSeekChangeListener : OnSeekChangeListener
    {
        public void onProgressChange(CircularSeekBar view, int newProgress) {
                    Log.d("Welcome", "Progress:" + view.getProgress() + "/" + view.getMaxProgress());
                }
    }
	

    ///**
    // * Instantiates a new circular seek bar.
    // * 
    // * @param context
    // *            the context
    // * @param attrs
    // *            the attrs
    // * @param defStyle
    // *            the def style
    // */
    //public CircularSeekBar(Context context, AttributeSet attrs, int defStyle) {
    //    //super(context, attrs, defStyle);
    //    mContext = context;
    //    //initDrawable();
    //}

    ///**
    // * Instantiates a new circular seek bar.
    // * 
    // * @param context
    // *            the context
    // * @param attrs
    // *            the attrs
    // */
    //public CircularSeekBar(Context context, AttributeSet attrs) {
    //    //super(context, attrs);
    //    mContext = context;
    //    //initDrawable();
    //}

	/**
	 * Instantiates a new circular seek bar.
	 * 
	 * @param context
	 *            the context
	 */
	public CircularSeekBar(Context context) {
		//super(context);
		mContext = context;

        circleColor = new Paint();
        innerColor = new Paint();
        circleRing = new Paint();

        circleColor.setColor(Color.parseColor("#ff33b5e5")); // Set default
        // progress
        // color to holo
        // blue.
        innerColor.setColor(Color.BLACK); // Set default background color to
        // black
        circleRing.setColor(Color.GRAY);// Set default background color to Gray

        circleColor.setAntiAlias(true);
        innerColor.setAntiAlias(true);
        circleRing.setAntiAlias(true);

        circleColor.setStrokeWidth(5);
        innerColor.setStrokeWidth(5);
        circleRing.setStrokeWidth(5);

        circleColor.setStyle(Paint.Style.FILL);

        //initDrawable();

	}

	/**
	 * Inits the drawable.
	 */
    //public void initDrawable()
    //{
    //    progressMark = BitmapFactory.decodeResource(mContext.getResources(), R.drawable.arrow_up_float);
    //    progressMarkPressed = BitmapFactory.decodeResource(mContext.getResources(),
    //            R.drawable.arrow_down_float);

    //}

    ///*
    // * (non-Javadoc)
    // * 
    // * @see android.view.View#onMeasure(int, int)
    // */
    //protected override void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
    //    onMeasure(widthMeasureSpec, heightMeasureSpec);

    //    width = getWidth(); // Get View Width
    //    height = getHeight();// Get View Height

    //    int size = (width > height) ? height : width; // Choose the smaller
    //    // between width and
    //    // height to make a
    //    // square

    //    cx = width / 2; // Center X for circle
    //    cy = height / 2; // Center Y for circle
    //    outerRadius = size / 2; // Radius of the outer circle

    //    innerRadius = outerRadius - barWidth; // Radius of the inner circle

    //    left = cx - outerRadius; // Calculate left bound of our rect
    //    right = cx + outerRadius;// Calculate right bound of our rect
    //    top = cy - outerRadius;// Calculate top bound of our rect
    //    bottom = cy + outerRadius;// Calculate bottom bound of our rect

    //    startPointX = cx; // 12 O'clock X coordinate
    //    startPointY = cy - outerRadius;// 12 O'clock Y coordinate
    //    markPointX = startPointX;// Initial locatino of the marker X coordinate
    //    markPointY = startPointY;// Initial locatino of the marker Y coordinate

    //    rect.set(left, top, right, bottom); // assign size to rect
    //}

    ///*
    // * (non-Javadoc)
    // * 
    // * @see android.view.View#onDraw(android.graphics.Canvas)
    // */
    //protected override void onDraw(Canvas canvas) {
    //    canvas.drawCircle(cx, cy, outerRadius, circleRing);
    //    canvas.drawArc(rect, startAngle, angle, true, circleColor);
    //    canvas.drawCircle(cx, cy, innerRadius, innerColor);
    //    if(SHOW_SEEKBAR){
    //        dx = getXFromAngle();
    //        dy = getYFromAngle();
    //        drawMarkerAtProgress(canvas);
    //    }
    //    onDraw(canvas);
    //}

    ///**
    // * Draw marker at the current progress point onto the given canvas.
    // * 
    // * @param canvas
    // *            the canvas
    // */
    //public void drawMarkerAtProgress(Canvas canvas) {
    //    if (IS_PRESSED) {
    //        canvas.drawBitmap(progressMarkPressed, dx, dy, null);
    //    } else {
    //        canvas.drawBitmap(progressMark, dx, dy, null);
    //    }
    //}

    ///**
    // * Gets the X coordinate of the arc's end arm's point of intersection with
    // * the circle
    // * 
    // * @return the X coordinate
    // */
    //public float getXFromAngle() {
    //    int size1 = progressMark.getWidth();
    //    int size2 = progressMarkPressed.getWidth();
    //    int adjust = (size1 > size2) ? size1 : size2;
    //    float x = markPointX - (adjust / 2);
    //    return x;
    //}

    ///**
    // * Gets the Y coordinate of the arc's end arm's point of intersection with
    // * the circle
    // * 
    // * @return the Y coordinate
    // */
    //public float getYFromAngle() {
    //    int size1 = progressMark.getHeight();
    //    int size2 = progressMarkPressed.getHeight();
    //    int adjust = (size1 > size2) ? size1 : size2;
    //    float y = markPointY - (adjust / 2);
    //    return y;
    //}

    ///**
    // * Get the angle.
    // * 
    // * @return the angle
    // */
    //public int getAngle() {
    //    return angle;
    //}

    ///**
    // * Set the angle.
    // * 
    // * @param angle
    // *            the new angle
    // */
    ////public void setAngle(int angle) {
    ////    this.angle = angle;
    ////    float donePercent = (((float) this.angle) / 360) * 100;
    ////    float progress = (donePercent / 100) * getMaxProgress();
    ////    setProgressPercent((int)Math.Round(donePercent));
    ////    CALLED_FROM_ANGLE = true;
    ////    setProgress((int)Math.Round(progress));
    ////}

    ///**
    // * Sets the seek bar change listener.
    // * 
    // * @param listener
    // *            the new seek bar change listener
    // */
    //public void setSeekBarChangeListener(OnSeekChangeListener listener) {
    //    mListener = listener;
    //}

    ///**
    // * Gets the seek bar change listener.
    // * 
    // * @return the seek bar change listener
    // */
    //public OnSeekChangeListener getSeekBarChangeListener() {
    //    return mListener;
    //}

    ///**
    // * Gets the bar width.
    // * 
    // * @return the bar width
    // */
    //public int getBarWidth() {
    //    return barWidth;
    //}

    ///**
    // * Sets the bar width.
    // * 
    // * @param barWidth
    // *            the new bar width
    // */
    //public void setBarWidth(int barWidth) {
    //    this.barWidth = barWidth;
    //}

    ///**
    // * The listener interface for receiving onSeekChange events. The class that
    // * is interested in processing a onSeekChange event implements this
    // * interface, and the object created with that class is registered with a
    // * component using the component's
    // * <code>setSeekBarChangeListener(OnSeekChangeListener)<code> method. When
    // * the onSeekChange event occurs, that object's appropriate
    // * method is invoked.
    // * 
    // * @see OnSeekChangeEvent
    // */
    public interface OnSeekChangeListener
    {

        /**
         * On progress change.
         * 
         * @param view
         *            the view
         * @param newProgress
         *            the new progress
         */
        void onProgressChange(CircularSeekBar view, int newProgress);
    }

    ///**
    // * Gets the max progress.
    // * 
    // * @return the max progress
    // */
    public int getMaxProgress()
    {
        return maxProgress;
    }

    ///**
    // * Sets the max progress.
    // * 
    // * @param maxProgress
    // *            the new max progress
    // */
    public void setMaxProgress(int maxProgress)
    {
        this.maxProgress = maxProgress;
    }

    ///**
    // * Gets the progress.
    // * 
    // * @return the progress
    // */
    public int getProgress()
    {
        return progress;
    }

    ///**
    // * Sets the progress.
    // * 
    // * @param progress
    // *            the new progress
    // */
    public void setProgress(int progress)
    {
        if (this.progress != progress)
        {
            this.progress = progress;
            if (!CALLED_FROM_ANGLE)
            {
                int newPercent = (this.progress * 100) / this.maxProgress;
                int newAngle = (newPercent * 360) / 100;
                //this.setAngle(newAngle);
                //this.setProgressPercent(newPercent);
            }
            mListener.onProgressChange(this, this.getProgress());
            CALLED_FROM_ANGLE = false;
        }
    }

    ///**
    // * Gets the progress percent.
    // * 
    // * @return the progress percent
    // */
    //public int getProgressPercent() {
    //    return progressPercent;
    //}

    ///**
    // * Sets the progress percent.
    // * 
    // * @param progressPercent
    // *            the new progress percent
    // */
    //public void setProgressPercent(int progressPercent) {
    //    this.progressPercent = progressPercent;
    //}

    ///**
    // * Sets the ring background color.
    // * 
    // * @param color
    // *            the new ring background color
    // */
    //public void setRingBackgroundColor(int color) {
    //    circleRing.setColor(color);
    //}

    ///**
    // * Sets the back ground color.
    // * 
    // * @param color
    // *            the new back ground color
    // */
    //public void setBackGroundColor(int color) {
    //    innerColor.setColor(color);
    //}

    ///**
    // * Sets the progress color.
    // * 
    // * @param color
    // *            the new progress color
    // */
    //public void setProgressColor(int color) {
    //    circleColor.setColor(color);
    //}

    ///*
    // * (non-Javadoc)
    // * 
    // * @see android.view.View#onTouchEvent(android.view.MotionEvent)
    // */
    ////public override bool onTouchEvent(MotionEvent ev) {
    ////    float x = ev.getX();
    ////    float y = ev.getY();
    ////    bool up = false;
    ////    switch (ev.getAction()) {
    ////    case MotionEvent.ACTION_DOWN:
    ////        moved(x, y, up);
    ////        break;
    ////    case MotionEvent.ACTION_MOVE:
    ////        moved(x, y, up);
    ////        break;
    ////    case MotionEvent.ACTION_UP:
    ////        up = true;
    ////        moved(x, y, up);
    ////        break;
    ////    }
    ////    return true;
    ////}

    ///**
    // * Moved.
    // * 
    // * @param x
    // *            the x
    // * @param y
    // *            the y
    // * @param up
    // *            the up
    // */
    ////private void moved(float x, float y, bool up) {
    ////    float distance = (float) Math.Sqrt(Math.Pow((x - cx), 2) + Math.Pow((y - cy), 2));
    ////    if (distance < outerRadius + adjustmentFactor && distance > innerRadius - adjustmentFactor && !up) {
    ////        IS_PRESSED = true;

    ////        markPointX = (float) (cx + outerRadius * Math.Cos(Math.Atan2(x - cx, cy - y) - (Math.PI /2)));
    ////        markPointY = (float) (cy + outerRadius * Math.Sin(Math.Atan2(x - cx, cy - y) - (Math.PI /2)));

    ////        float degrees = (float) ((float) ((Math.ToDegrees(Math.Atan2(x - cx, cy - y)) + 360.0)) % 360.0);
    ////        // and to make it count 0-360
    ////        if (degrees < 0) {
    ////            degrees += 2 * Math.PI;
    ////        }

    ////        setAngle((int)Math.Round(degrees));
    ////        invalidate();

    ////    } else {
    ////        IS_PRESSED = false;
    ////        invalidate();
    ////    }

    ////}

    ///**
    // * Gets the adjustment factor.
    // * 
    // * @return the adjustment factor
    // */
    //public float getAdjustmentFactor() {
    //    return adjustmentFactor;
    //}

    ///**
    // * Sets the adjustment factor.
    // * 
    // * @param adjustmentFactor
    // *            the new adjustment factor
    // */
    //public void setAdjustmentFactor(float adjustmentFactor) {
    //    this.adjustmentFactor = adjustmentFactor;
    //}

    ///**
    // * To display seekbar
    // */
    //public void ShowSeekBar() {
    //    SHOW_SEEKBAR = true;
    //}

    ///**
    // * To hide seekbar
    // */
    //public void hideSeekBar() {
    //    SHOW_SEEKBAR = false;
    //}
}
}
