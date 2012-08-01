package com.lacas.testsocket.helper;

import java.io.IOException;

import com.lacas.testsocket.utils.Utils;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PixelFormat;
import android.hardware.Camera;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;

public class CameraHelper {

	public static Camera camera; 
	
	public static class DrawOnTop extends View { 
        public DrawOnTop(Context context) { 
            super(context); 
        }

	    @Override 
	    protected void onDraw(Canvas canvas) { 
	    	
	        Paint paint = new Paint(); 
	        
	        paint.setStyle(Paint.Style.STROKE); 
	        paint.setColor(Color.WHITE);
	        paint.setTextSize(20);
	        
	        canvas.drawText("press the 'take picture' on your browser", 10, Utils.getHeight(getContext())- 60, paint); 
	        
	        super.onDraw(canvas); 
	    } 
    }
    
    
    public static class Preview extends SurfaceView implements SurfaceHolder.Callback { 
        SurfaceHolder mHolder; 

        public Preview(Context context) { 
            super(context); 
            mHolder = getHolder(); 
            mHolder.addCallback(this); 
            mHolder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
        } 

        public void surfaceCreated(SurfaceHolder holder) { 
		// http://developer.android.com/reference/android/hardware/Camera.html#open(int)
            camera = Camera.open(0); 
            try {
            	 camera.setPreviewDisplay(holder);
	        } catch (IOException e) {
	            e.printStackTrace();
	        } 
        } 

        public void surfaceDestroyed(SurfaceHolder holder) { 
           camera.stopPreview();
           camera.release();
           camera = null; 
        } 

        public void surfaceChanged(SurfaceHolder holder, int format, int w, int h) { 
            Camera.Parameters parameters = camera.getParameters(); 
            parameters.setPictureFormat(PixelFormat.JPEG);
            camera.setParameters(parameters); 
            camera.startPreview(); 
       } 
    } 
    
    
}
