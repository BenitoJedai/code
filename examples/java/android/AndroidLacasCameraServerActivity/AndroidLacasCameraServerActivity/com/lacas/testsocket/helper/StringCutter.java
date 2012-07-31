package com.lacas.testsocket.helper;


public class StringCutter {

	public String cutFromString(String s, String val1, String val2, int start) {
        int a1, b1;

        a1 = s.indexOf(val1,start) + val1.length();
        b1 = s.indexOf(val2,a1);

        if (a1 == val1.length()-1) return "";

        return s.substring(a1, b1);
    }

    public String[] cutFromStringArray(String s, String val1, String val2, int start)
    {
    	String sAllImg="";
    	int a1 = 0;
    	int b1 = 0;

    	do {
    		a1 = s.indexOf(val1, start) + val1.length();
    		b1 = s.indexOf(val2, a1);

    		if (a1 == val1.length()-1) {
    		} else {
    			sAllImg+=s.substring(a1, b1)+"\n";
    		}

    		if (a1 == val1.length()-1)
    			break;
    		if (b1 == 0)
    			break;

    		start = b1;
    	} while (true);

    	return sAllImg.split("\n");
    }
   
    
}
