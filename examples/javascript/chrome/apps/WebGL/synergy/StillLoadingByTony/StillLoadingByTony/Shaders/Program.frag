#define nWai    50.
#define nNei    20.
#define nGtoD   57.3
#define midWid  -30.
#define nGrad   45.

float getDistance(vec2 p)
{
    return pow((p.x * p.x + p.y * p.y), 0.5);
}

float getGradient(vec2 p)
{
    return atan(abs(p.y / p.x)) * nGtoD;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy;
    uv = vec2( uv.x - 0.5 * iResolution.x, uv.y - 0.5 * iResolution.y);
    
    float nDis = getDistance(uv);
    float nGra = getGradient(uv);
    
    bool isMid = false; float midValue = 0.0;
    int ml = 1;
    if(mod(nGra, nGrad) < midWid || mod(nGra, nGrad) > (nGrad - midWid)) 
    {
        isMid=true; 
    }
    else
    {
        midValue = mod(nGra, nGrad);
        ml = int(nGra / nGrad) + 1;
        if(uv.x < 0. && uv.y > 0.) ml = ml;
        if(uv.x > 0. && uv.y > 0.) ml = 2 + (3 - ml);
        if(uv.x > 0. && uv.y < 0.) ml = 4 + ml;
        if(uv.x < 0. && uv.y < 0.) ml = 6 + (3 - ml);
    }
    
    float nCurrLight = mod(iGlobalTime*3.5, 8.) + 1.; 
    int nCL = int(nCurrLight);

	
    if(nDis < nWai && nDis > nNei && !isMid)
    {
    	vec4 p1 = vec4(smoothstep( 0.78, 0.8, 1.0 - pow(abs(midValue-nGrad*0.5)/(nGrad-midWid), 1.2) ));
   
        float nMiddle = (nWai - nNei) * 0.5 + nNei;
        float sh = 1.0 - abs( nDis - nMiddle ) / (nWai - nNei);
        
        vec4 pp =p1 *  vec4( smoothstep(0.78, 0.8, pow(sh, 0.5) ));
        
        fragColor = pp * vec4(0.3, 0.3, 0.3, 1.);
        
        if(ml == nCL) 
            fragColor = pp * vec4(0.6, 0.6, 0.6, 1.);
    }
    else
  		fragColor = vec4(0., 0., 0., 1.);
 
    

    
}