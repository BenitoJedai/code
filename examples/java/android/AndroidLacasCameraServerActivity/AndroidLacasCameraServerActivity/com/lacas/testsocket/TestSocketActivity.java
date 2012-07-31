package com.lacas.testsocket;

import java.util.Timer;
import com.lacas.testsocket.helper.CameraHelper.DrawOnTop;
import com.lacas.testsocket.helper.CameraHelper.Preview;
import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.util.Log;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;

/*
 * created by Leslie Kundera (hungary) 2011.09.10.  
 */

public class TestSocketActivity extends Activity {

	private Timer timer=new Timer();
	
	final static String authName="username";
	final static String authPass="password";

	private ServerThread serverThread;

	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        requestWindowFeature(Window.FEATURE_NO_TITLE); 
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE); 
        
        Preview mPreview 	= new Preview(this); 
        DrawOnTop mDraw 	= new DrawOnTop(this); 
        
        setContentView(mPreview); 
        addContentView(mDraw, new LayoutParams (LayoutParams.WRAP_CONTENT,  LayoutParams.WRAP_CONTENT));       
    
        new Thread(serverThread = new ServerThread(this, authName, authPass)).start();
    }
	
    @Override
    public void onDestroy() {
    	super.onDestroy();

    	if (timer!=null) timer.cancel();
    	
    	try {
			if (serverThread.serversocket!=null) {
				serverThread.closeConnections();
			} else {
				Log.e("out", "serversocket null");
			}
		} catch (Exception ex) {
			Log.e("ex", ""+ex);
		}
    }
    
    
}