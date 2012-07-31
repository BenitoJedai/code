package com.lacas.testsocket;

import java.io.InputStream;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.hardware.Camera;
import com.lacas.testsocket.helper.CameraHelper;
import com.lacas.testsocket.utils.Utils;
import com.lacas.testsocket.utils.UtilsSD;

public class TakePictureThread implements Runnable {
	
	Context mycontext;
	ServerThread serverThread;
	
	TakePictureThread(Context mycontext, ServerThread serverThread) {
		this.mycontext=mycontext;
		this.serverThread=serverThread;
	}
	
	public void run() {
    	Camera.PictureCallback mPictureCallback = new Camera.PictureCallback() {
			public void onPictureTaken(byte[] data, Camera c) {
				if (data != null) {

                	CameraHelper.camera.startPreview();
                	//save pic
                	Bitmap bmp = BitmapFactory.decodeByteArray(data, 0,	data.length );
               		UtilsSD.saveBitmapToSD(mycontext, bmp, "shot.jpg");

               		//load pic and send to browser via local ip
                    InputStream content=Utils.openFileFromSD("shot.jpg", mycontext);
                    if (content!=null) {
                    	serverThread.send(content, "image/jpeg");
                    } 

                	//send html
                    serverThread.send("<head>" +
                			"<link rel=\"stylesheet\" type=\"text/css\" " +
                			"href=\""+serverThread.getHost()+"/css.css\" />" +
                	
                			"<meta http-equiv=\"Content-type\" value=\"text/html; charset=ISO-8859-2\">OK " +
                			"<img src='"+serverThread.getHost()+"/shot.jpg'><div id='asd' style='clear:left'>0</div> "+
                			""+"</head>");

                	serverThread.closeInputOutput();
				}
			}
		};
		
		CameraHelper.camera.takePicture(null, null, mPictureCallback);
	}
}
