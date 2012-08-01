package com.lacas.testsocket;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.Enumeration;
import java.util.StringTokenizer;

import android.content.Context;
import android.os.Handler;
import android.util.Log;

import com.lacas.testsocket.helper.Base64;
import com.lacas.testsocket.helper.StringCutter;
import com.lacas.testsocket.utils.Utils;

public class ServerThread implements Runnable {

	private Context mycontext;
	ServerSocket serversocket;
	
	private Socket clientsocket;
	private BufferedReader input;
	private OutputStream output;
	
	StringCutter cutter=new StringCutter();

	final static Handler mHandler = new Handler();

	boolean isAuthorized=false;
	boolean isRunning=false;
	
	private String host;
	private String authName;
	private String authPass;
	
	ServerThread(Context mycontext, String authName, String authPass) {
		this.mycontext=mycontext;
		this.authName=authName;
		this.authPass=authPass;
		
		isRunning=true;
	}

	public void run() {
    	
        try {
            host		= getLocalIpAddress();
            int port 	= 1112;
            
            serversocket = new ServerSocket(port);
            serversocket.setReuseAddress(true);
            
            while (isRunning) {
            	
                    clientsocket	= serversocket.accept();
                    input 			= new BufferedReader(new InputStreamReader(clientsocket.getInputStream(), "ISO-8859-2"));
                    output 			= clientsocket.getOutputStream(); 
                    
            		mHandler.post(new Runnable() {
            			@Override public void run() {
            				Utils.hint(mycontext, "new client connected FROM "+clientsocket.getInetAddress()+" "+clientsocket.getPort());
            		}});
                    

                    String sAll			= getStringFromInput(input);
                    final String header	= sAll.split("\n")[0];

                    StringTokenizer s = new StringTokenizer(header);
                    String temp = s.nextToken();
                    
                    if (temp.equals("GET")) { //send picture if any
                    	
                    	String fileName = s.nextToken();
                        String localfile=fileName.replace(host+"/","").replace("/", "");
                        
                        InputStream content=Utils.openFileFromAssets(localfile, mycontext);
                        if (content!=null) {
                        	send(content, ContentType.getContentType(localfile));
                        } 
                    }
                    
                	if (header.equals("GET /secondpage HTTP/1.1")) {
                		String js="";
                		
                    	send("<head>" +
                    			"<link rel=\"stylesheet\" type=\"text/css\" " +
                    			"href=\"/css.css\" />" +
                    	
                    			"<meta http-equiv=\"Content-type\" value=\"text/html; charset=ISO-8859-2\">Second page " +
                    			"<img src='/icon.png'>" +
                    			"<br><a href='/'>back</a>" +
                    			"<br><a href='/secondpage'>secondpage</a>" +
                    			"<br><a href='/takepicture'>take picture</a>" +
                    			"<div id='asd' style='clear:left'></div> "+
                    			js+"</head>");
                    	
                    	closeInputOutput();
                	}
                	else if (header.equals("GET /takepicture HTTP/1.1")) {
                		new Thread(new TakePictureThread(mycontext, this)).start();
                	} 
                	else if (header.equals("GET / HTTP/1.1")) {
                		String firstpage="<head><meta http-equiv=\"Content-type\" value=\"text/html; charset=ISO-8859-2\">First page! <a href='/secondpage'>secondpage</a></head></html>";
                		
                		if (!isAuthorized) {
                			
                			if (sAll.contains("Authorization: Basic") && !isAuthorized) {
                				/*
                				 * this is a basic authentication
                				 */
                				
    	                		String authbase64string=cutter.cutFromString(sAll, "Authorization: Basic ", "\n", 0);
    	                		String authbase64 = " "+new String((Base64.decode(authbase64string, Base64.DEFAULT)))+" ";
    	                		
    	                		String auth_name= ((authbase64.contains(":"))?authbase64.split(":")[0]:"").trim();
    	                		String auth_pass= ((authbase64.contains(":"))?authbase64.split(":")[1]:"").trim();
    	                		
    	                		if (auth_name.equals(authName) && auth_pass.equals(authPass)) {
    	                			send(firstpage);
    	                			
    	                			isAuthorized=true;
    	                		} else {
    	                			askAuth();
    	                		} 
    	                		
                			} else {
                				askAuth();
                			}
                		} else {
                			send(firstpage);	                			
                		}
                	    
                		closeInputOutput();
                    } else {
                    	send("404 error");	
                    	closeInputOutput();
                    }
                	
		        }
        } catch (Exception ex) {
        	Log.e("doInBackground Exception", " "+ex);
        }
        
        Log.e("out", "end");
	}
	
	void send(InputStream fis, String contenttype) {
		try {
		    String header=
		    		"HTTP/1.1 200 OK\n" +
		    		"Content-type: "+contenttype+"\n"+
		    		"Content-Length: "+fis.available()+"\n" +
		    		"\n";
	
		    output.write(header.getBytes());
		    
		    byte[] buffer = new byte[1024];
		    int bytes = 0;

		    while ((bytes = fis.read(buffer)) != -1) {
		    	output.write(buffer, 0, bytes);
		    }
		    
		} catch (Exception ex) {
			Log.e("exxx send", ex+"");
		}
	}

	void send(InputStream fis, String contenttype, OutputStream out, Socket socket) {
		try {
		    String header=
		    		"HTTP/1.1 200 OK\n" +
		    		"Content-type: "+contenttype+"\n"+
		    		"Content-Length: "+fis.available()+"\n" +
		    		"\n";
	
		    out.write(header.getBytes());
		    
		    byte[] buffer = new byte[1024];
		    int bytes = 0;

		    while ((bytes = fis.read(buffer)) != -1) {
		    	out.write(buffer, 0, bytes);
		    }

			out.close();
			socket.close();

		} catch (Exception ex) {
			Log.e("exx send", ex+"");
		}
	}
	
	void send(String s) {
	    String header=
	    		"HTTP/1.1 200 OK\n" +
	    		"Connection: close\n"+
	    		"Content-type: text/html; charset=utf-8\n"+
	    		"Content-Length: "+s.length()+"\n" +
	    		"\n";

	    try {
	    	output.write((header+s).getBytes());
	    } catch (Exception ex) {
	    	Log.e("ex send", ex+"");
	    }
	}
	
	void askAuth() {
	    String header=
	    		"HTTP/1.0 401 Authorization Required\n" +
	    		"Connection: close\n"+
	    		"WWW-Authenticate: Basic realm=\"Lacroix's Android server\"\n"+
	    		"HTTP/1.0 401 Unauthorized";
	    
	    try {
	    	output.write(header.getBytes());
	    } catch (Exception ex) {
	    	Log.e("ex askAuth", ex+"");
	    }
	}

	public String getLocalIpAddress() {
	    try {
	        for (Enumeration<NetworkInterface> en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements();) {
	            NetworkInterface intf = en.nextElement();
	            for (Enumeration<InetAddress> enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements();) {
	                InetAddress inetAddress = enumIpAddr.nextElement();
	                if (!inetAddress.isLoopbackAddress()) {
	                    return inetAddress.getHostAddress().toString();
	                }
	            }
	        }
	    } catch (SocketException ex) {
	        Log.e("ex getLocalIpAddress", ex.toString());
	    }
	    return null;
	}
	
	String getStringFromInput(BufferedReader input) {
        StringBuilder sb = new StringBuilder();
        String sTemp; 
        try {
			while (!(sTemp = input.readLine()).equals(""))  {
				sb.append(sTemp+"\n");
			}
		} catch (IOException e) {
			return "";
		}

        return sb.toString();
	}

	void closeConnections() {
		try {
			closeInputOutput();
			serversocket.close();
		} catch (Exception ex) {
			Log.e("err closeConnections", ex+"");
		}
		
		isRunning=false;
	}
	
	void closeInputOutput() {
    	try {
    		input.close();
    		output.close();
			clientsocket.close();
		} catch (Exception ex) {
			Log.e("ex closeInputOutput", ex+"");
		}
	}

	public String getHost() {
		return host;
	}

	
};

