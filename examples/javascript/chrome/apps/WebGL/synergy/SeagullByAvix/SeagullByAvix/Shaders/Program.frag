float udBox( vec3 p, vec3 b ) {
  return length(max(abs(p)-b,0.0)) ;
}
float sdCone(vec3 p, vec3 c){
    vec2 q = vec2( length(p.xz), p.y );
    return max( max( dot(q,c.xy), p.y), -p.y-c.z );
}
float sdTriPrism(vec3 p, vec2 h){
    vec3 q = abs(p);
    return max(q.z-h.y,max(q.x*0.866025+p.y*0.5,-p.y)-h.x*0.5);
}
/***********************************************/
float sminp(float a, float b, float k) {
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}
/***********************************************/
vec2 rot(vec2 k, float t) {
    return vec2(cos(t)*k.x-sin(t)*k.y,sin(t)*k.x+cos(t)*k.y);
}
/***********************************************/
float body(vec3 p) {
    //head
    vec3 q=p;
    q.y-=0.47;
    q.z-=0.1;
    float h=length(q)-0.13 - cos(p.y)*0.05;
    //body
    p.z-=0.1;
    p.y+=0.1;
    p.y*=0.35+sin(p.z*0.5)*0.5;
    float b=length(p)-0.25;
    //beak
    q.y-=0.45;
    q.z-=0.11;
    float n=sdCone(q,vec3(0.8,0.11,0.3))+sin(q.z*3.2)*0.2;
    //tail
    q.y+=2.1;
    q.z+=0.15;
    float t=sdTriPrism(q,vec2(0.5,0.01))-sin(q.y+0.05)*0.1;
    
    return sminp(t, sminp(n, sminp(h,b,0.05), 0.02), 0.1);
}
/***********************************************/
float wing(vec3 p) {
    float sgt=sin(2.0+iGlobalTime*2.0);
    float a=( 0.25+sgt*0.4 );
    p.xz=rot(p.xz,a);
    float bf=-0.4*(a*0.4- ((cos(2.0+iGlobalTime*2.0)*0.4 )+0.3) );

    float c = cos(-p.x*bf);
    float s = sin(-p.x*bf);
    mat2  m = mat2(c,-s,s,c);
    p.xz*=m;

    p.y-=1.;
    p.y+=sin(1.6+p.x*0.3);
    p.x-=1.35;
    p.y+=clamp( sin(p.x*0.3) ,0.0, 1.0);
    p.y*=  pow(abs(1.4-p.x),-0.20) ;

    return udBox(p, vec3(1.4,0.30,0.02));// -d;
}
/***********************************************/
float hash(float n) { 
	return fract(sin(n)*43758.5453123); 
}

float noise3(vec3 x) {
    vec3 p = floor(x);
    vec3 f = fract(x);
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + p.z*113.0;
    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return res;
}

/***********************************************/
vec2 opU(vec2 a, vec2 b) {
	return mix(a, b, step(b.x, a.x));
}
/***********************************************/
vec2 DE(vec3 p) {
    vec3 q=p;
//floor
    q.x*=0.3;
    q.z+=iGlobalTime*3.0;
    q.y+=iGlobalTime*1.2;
    vec2 water=vec2( p.y+2.0-noise3(q)*0.2 , 1.0);
    
//bird
    p.y+=sin(1.0+iGlobalTime*2.0)*0.3;

    p.zy=rot(p.zy,1.57);

    float l=wing(vec3( p.x-0.13,p.y,p.z+0.05));
    float r=wing(vec3(-p.x-0.13,p.y,p.z+0.05));
    float b=body(p);
    vec2 bird=vec2( sminp(b, sminp(l,r,0.1), 0.1), 2.0);

	return opU(bird,water);
}

/***********************************************/
float calcSS(vec3 ro, vec3 rd, float t, float k ) {
    float res = 1.0;
    for( int i=0; i<10; i++ ) {
    	float h = DE(ro + rd*t).x;
        res = min( res, k*h/t );
        t += h;
	}
    return clamp(res,0.0,1.0);
}
/***********************************************/
vec3 normal(vec3 p) {
	vec3 e=vec3(0.01,-0.01,0.0);
	return normalize( vec3(	e.xyy*DE(p+e.xyy).x +	e.yyx*DE(p+e.yyx).x +	e.yxy*DE(p+e.yxy).x +	e.xxx*DE(p+e.xxx).x));
}
/***********************************************/
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    vec2 p = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x/iResolution.y;	
	vec3 ta = vec3(0.0, 0.0, 0.0);
	vec3 ro =vec3(0.0, 6.0, -6.0);          //5
	vec3 lig=normalize(vec3(1.0, 5.0, 0.0));
	
    ro.xz=rot(ro.xz,iGlobalTime*0.2);

	vec3 cf = normalize( ta - ro );
    vec3 cr = normalize( cross(cf,vec3(0.0,1.0,0.0) ) );
    vec3 cu = normalize( cross(cr,cf));
	vec3 rd = normalize( p.x*cr + p.y*cu + 2.5*cf );

	vec3 col=vec3(0.6,0.7,1.0);
	/* trace */
	vec2 r=vec2(0.0);	
	float f=0.0;
	vec3 ww;
	for(int i=0; i<90; i++) {
		ww=ro+rd*f;
		r=DE(ww);		
		if( r.x<0.0 ) break;
		f+=r.x/2.0 ;
	}
	/* draw */

	if( f<30.0 ) {
		vec3 nor=normal(ww);

        float rim=0.0;
        float spe=0.0;

		if (r.y==1.0) {
		    
	        float rim =0.3*pow(1.+dot(nor,rd), 5.0);
            float spe =0.5*pow(clamp( dot( lig, reflect(rd,nor) ), 0.0, 1.0 ) ,16.0 );
		    }
		if (r.y==2.0) col=vec3(0.8) ;
		
			float amb =0.2*ww.y;
			float dif =0.7*clamp(dot(lig, nor), 0.0,1.0);
			float bac =0.2*clamp(dot(nor,-lig), 0.0,1.0);		
		    float sh=calcSS(ww, lig, 0.01, 1.0);

		col*=(amb+dif+bac+sh);

	    col += (rim+spe)*vec3(1.);		
	}
	
	col*=exp(0.02*f); col*=0.9;	
	
	fragColor = vec4( col, 1.0 );
}
