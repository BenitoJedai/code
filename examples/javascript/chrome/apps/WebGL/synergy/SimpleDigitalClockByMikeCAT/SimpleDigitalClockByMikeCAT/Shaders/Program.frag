/**
 * Check if coord is in display of the number.
 * @param coord The position to check (x,y)
 * @param n The number to display
 * @param numpos The position to draw the number (left-x,bottom-y,width,height)
 * @param a The thickness of the number
 * @return the check result (in -> true)
 */
bool isInNumber(vec2 coord,float n,vec4 numpos,float a)
{
	/*
	 *  0
	 * 5 1
	 *  6
	 * 4 2
	 *  3
	 *
	 * n 0123456
	 * 0 TTTTTTF
	 * 1 FTTFFFF
	 * 2 TTFTTFT
	 * 3 TTTTFFT
	 * 4 FTTFFTT
	 * 5 TFTTFTT
	 * 6 TFTTTTT
	 * 7 TTTFFTF
	 * 8 TTTTTTT
	 * 9 TTTTFTT
	 */
	n=floor(n);
	// 0
	if(n!=1.0 && n!=4.0 &&
	numpos.x+a<=coord.x && coord.x<=numpos.x+numpos.z-a &&
	numpos.y+numpos.w-a<=coord.y && coord.y<=numpos.y+numpos.w)return true;
	// 1
	if(n!=5.0 && n!=6.0 &&
	numpos.x+numpos.z-a<=coord.x && coord.x<=numpos.x+numpos.z &&
	numpos.y+numpos.w/2.0+a/2.0<=coord.y && coord.y<=numpos.y+numpos.w-a)return true;
	// 2
	if(n!=2.0 &&
	numpos.x+numpos.z-a<=coord.x && coord.x<=numpos.x+numpos.z &&
	numpos.y+a<=coord.y && coord.y<=numpos.y+numpos.w/2.0-a/2.0)return true;
	// 3
	if(n!=1.0 && n!=4.0 && n!=7.0 &&
	numpos.x+a<=coord.x && coord.x<=numpos.x+numpos.z-a &&
	numpos.y<=coord.y && coord.y<=numpos.y+a)return true;
	// 4
	if(n!=1.0 && n!=3.0 && n!=4.0 && n!=5.0 && n!=7.0 && n!=9.0 &&
	numpos.x<=coord.x && coord.x<=numpos.x+a &&
	numpos.y+a<=coord.y && coord.y<=numpos.y+numpos.w/2.0-a/2.0)return true;
	// 5
	if(n!=1.0 && n!=2.0 && n!=3.0 &&
	numpos.x<=coord.x && coord.x<=numpos.x+a &&
	numpos.y+numpos.w/2.0+a/2.0<=coord.y && coord.y<=numpos.y+numpos.w-a)return true;
	// 6
	if(n!=0.0 && n!=1.0 && n!=7.0 &&
	numpos.x+a<=coord.x && coord.x<=numpos.x+numpos.z-a &&
	numpos.y+numpos.w/2.0-a/2.0<=coord.y && coord.y<=numpos.y+numpos.w/2.0+a/2.0)return true;
	return false;
}

/**
 * Check if coord is in the rect.
 * @param coord The position to check (x,y)
 * @param pos The position of the rect (left-x,bottom-y,width,height)
 * @return the check result (in -> true)
 */
bool isInRect(vec2 coord,vec4 pos)
{
	return pos.x<=coord.x && coord.x<=pos.x+pos.z && pos.y<=coord.y && coord.y<=pos.y+pos.w;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	bool isDraw = false;
	float theTime = iDate.w;
	vec2 uv = fragCoord.xy / iResolution.xy;
	// draw the date
	isDraw = isDraw || isInNumber(uv,iDate.x/1000.0,vec4(0.05,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(iDate.x/100.0,10.0),vec4(0.15,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(iDate.x/10.0,10.0),vec4(0.25,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(iDate.x,10.0),vec4(0.35,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInRect(uv,vec4(0.455,0.575,0.03,0.06));
	isDraw = isDraw || isInNumber(uv,iDate.y/10.0,vec4(0.515,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(iDate.y,10.0),vec4(0.615,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInRect(uv,vec4(0.715,0.575,0.03,0.06));
	isDraw = isDraw || isInNumber(uv,iDate.z/10.0,vec4(0.78,0.575,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(iDate.z,10.0),vec4(0.88,0.575,0.07,0.3),0.015);
	// draw the time
	isDraw = isDraw || isInNumber(uv,theTime/36000.0,vec4(0.25,0.125,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(theTime/3600.0,10.0),vec4(0.35,0.125,0.07,0.3),0.015);
	isDraw = isDraw || isInRect(uv,vec4(0.455,0.185,0.03,0.06));
	isDraw = isDraw || isInRect(uv,vec4(0.455,0.305,0.03,0.06));
	theTime = mod(theTime,3600.0);
	isDraw = isDraw || isInNumber(uv,theTime/600.0,vec4(0.515,0.125,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(theTime/60.0,10.0),vec4(0.615,0.125,0.07,0.3),0.015);
	isDraw = isDraw || isInRect(uv,vec4(0.715,0.185,0.03,0.06));
	isDraw = isDraw || isInRect(uv,vec4(0.715,0.305,0.03,0.06));
	theTime = mod(theTime,60.0);
	isDraw = isDraw || isInNumber(uv,theTime/10.0,vec4(0.78,0.125,0.07,0.3),0.015);
	isDraw = isDraw || isInNumber(uv,mod(theTime,10.0),vec4(0.88,0.125,0.07,0.3),0.015);
	if(isDraw) {
		fragColor = vec4(0.0,0.0,0.0,1.0);
	} else {
		fragColor = vec4(uv,0.5+0.5*sin(iGlobalTime),1.0);
	}
}