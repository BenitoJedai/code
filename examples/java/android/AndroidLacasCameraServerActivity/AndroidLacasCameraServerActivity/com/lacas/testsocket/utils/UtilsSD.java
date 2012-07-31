package com.lacas.testsocket.utils;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;

import android.content.Context;
import android.graphics.Bitmap;
import android.os.Environment;
import android.util.Log;

public class UtilsSD {

	
	// final static String SAVE_PATH  =Environment.getExternalStorageDirectory().getAbsolutePath()+"/testsocket";
	final static String SAVE_PATH  =Environment.getExternalStoragePublicDirectory(
            Environment.DIRECTORY_PICTURES)+"/";
	
	static boolean isSDCardPrepared() {
    	String state = android.os.Environment.getExternalStorageState();
	    if (!state.equals(android.os.Environment.MEDIA_MOUNTED)) return false; else return true;
	}

	public static void saveBitmapToSD(Context mycontext, Bitmap bmp, String filename) {
		if (isSDCardPrepared()) {
			File directory = new File(SAVE_PATH);
		    directory.mkdirs();
		    
		    ByteArrayOutputStream bytes = new ByteArrayOutputStream();
		    bmp.compress(Bitmap.CompressFormat.JPEG, 100, bytes);

		    File f = new File(SAVE_PATH+File.separator+filename);
		    
		    try {
			    f.createNewFile();
			    
			    FileOutputStream fo = new FileOutputStream(f);
			    fo.write(bytes.toByteArray());
		    } catch (Exception ex) {
		    	Log.e("ex saveBitmapToSD", ex+"");
		    }

		} else {
			Utils.alert("Figyelem!", "Hiba! Kérem helyezze be az SD kártyát és húzza ki az usb kábelt!", mycontext);		
		}
	}
	public static InputStream loadBitmapFromSD(Context mycontext, String filename) {
		if (isSDCardPrepared()) {
		    
		    File f = new File(SAVE_PATH+File.separator+filename);
		    
		    try {
			    if (f.exists()) return new FileInputStream(f);	    
		    } catch (Exception ex) {
		    	Log.e("ex loadBitmapFromSD", ex+"");
		    }

		} else {
			Utils.alert("Figyelem!", "Hiba! Kérem helyezze be az SD kártyát és húzza ki az usb kábelt!", mycontext);		
		}
		
		return null;
	}
	
	public static boolean isBitmapExistOnSD(String filename) {
		return new File(SAVE_PATH+File.separator+filename).exists();
	}
	
	public static String getBitmapPath(String filename) {
		return SAVE_PATH+File.separator+filename;
	}
	
	
}
