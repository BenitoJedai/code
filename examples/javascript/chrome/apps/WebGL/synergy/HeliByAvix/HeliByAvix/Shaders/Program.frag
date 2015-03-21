#define FARCLIP    55.0

#define MARCHSTEPS 60
#define AOSTEPS    20
#define SHSTEPS    20
#define SHPOWER    1.0

#define PI         3.14
#define PI2        PI*0.5    
#define d90        1.57079633
#define d15        0.261799388

#define AMBCOL     vec3(1.0,1.0,1.0)
#define BACCOL     vec3(1.0,1.0,1.0)
#define DIFCOL     vec3(1.0,1.0,1.0)

#define MAT1       1.0  //terrain
#define MAT2       2.0  //water
#define MAT3       3.0  //trees

#define MAT4       4.0  //body
#define MAT5       5.0  //windows
#define MAT6       6.0  //propelers
#define MAT7       7.0  //legs

/***********************************************/
float rbox(vec3 p, vec3 s, float r) {	
    return length(max(abs(p)-s+vec3(r),0.0))-r;
}
float sminp(float a, float b) {
const float k=0.1;
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}
/***********************************************/
float hash(float n) { 
	return fract(sin(n)*43758.5453123); 
}
/***********************************************/
float noise2(vec2 x) {
    vec2 p = floor(x);
    vec2 f = fract(x);
    f = f*f*(3.0-2.0*f);
	float n = p.x + p.y*57.0;
    return mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
               mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y);
}
/***********************************************/
void oprep(inout vec3 p, float l, float s) {
	float r=1./l;
	float ofs=s+s/(r*2.0);
	float a= mod( atan(p.x, p.z) + PI2*r, PI*r) -PI2*r;
	p.xz=vec2(sin(a),cos(a))*length(p.xz) -ofs;
	p.x+=ofs;
}
void oprepb(inout vec3 p, float l, float s) {
	float r=1./l;
	float ofs=s+s/(r*2.0);
	float a= mod( atan(p.x, p.y) + PI2*r, PI*r) -PI2*r;
	p.xy=vec2(sin(a),cos(a))*length(p.xy) -ofs;
	p.x+=ofs;
}
/***********************************************/
vec2 rot(vec2 k, float t) {
    float ct=cos(t); 
    float st=sin(t);
    return vec2(ct*k.x-st*k.y,st*k.x+ct*k.y);
}
/***********************************************/
vec3 opU(vec3 a, vec3 b) {
	return mix(a, b, step(b.x, a.x));
}
/***********************************************/
vec3 chop(vec3 p) {
    p.x+=sin(PI+iGlobalTime)*2.0;
    p.y-=0.5+sin(iGlobalTime*0.35)*0.75;
    p.xz=rot(p.xz,-d90+sin(PI2+iGlobalTime));
    p.xy=rot(p.xy,d15);

    vec3 q=p;
        q.z+=sin(p.z)*0.5;
        q.y+=sin(p.y)*0.45 -0.05;
        float h=length(q)-0.3;
        float t=rbox(p+vec3(-0.25,-0.1,0.0), vec3(0.2,0.1,0.2),0.025);
        float r=rbox(p+vec3(0.05,-0.1,0.0), vec3(0.08,0.1,0.3),0.025);
    vec3 wind=vec3 (max(min(t,r),h),MAT5,0.0);
    
    h=max(max(h,-t),-r);
          t=rbox(p+vec3(0.5,-0.12,0.0), vec3(0.3,0.05,0.06),0.05);
    h=sminp(h,t);
          t=rbox(p+vec3(0.8,-0.23,0.0), vec3(0.05,0.1,0.04),0.02);
    h=sminp(h,t);
    
        q.xz=-abs(p.xz);
        q.yz+=vec2(0.3,0.2);
          r=rbox(q, vec3(0.2,0.015,0.015),0.0075);
        q+=vec3(0.1,-0.025,-0.2);
          t=rbox(q, vec3(0.015,0.015,0.2),0.0075);
        r=min(r,t);      
        wind=opU(wind,vec3(r,MAT7,0.0));
//        h=min(h,r);      

        q=p;
        q.xz=rot(q.xz,iGlobalTime*9.0);
        oprep(q,1.5,0.1);
        r=rbox(q+vec3(0.0,-0.25,0.0), vec3(0.04,0.01,0.5),0.01);
        q=p;
        q+=vec3(0.8,-0.25,-0.05);
        q.xy=rot(q.xy,iGlobalTime*9.0);
        oprepb(q,1.5,0.0);
        t=rbox(q, vec3(0.03,0.15,0.01),0.01);
    vec3 rotors=vec3(min(r,t),MAT6,0.0);
    
    return opU(rotors,opU(wind,vec3(h, MAT4,0.0)));
}

/***********************************************/
vec3 DE(vec3 p) {

float rw=0.8;                //river width
float sw=0.55;              //shore to side transition speed
float h=p.y+0.8;
float ss=0.0;

float mat=MAT1;
vec3 q=p;

        p.z+=iGlobalTime*2.0;

        float crv=sin(p.z*0.5)*2.0+0.5;         //river path center
        float lrv=crv+rw + noise2(p.xz);  //left shore path
        float rrv=crv-rw + noise2(p.zx);  //right shore path

        //river        
        if ( p.x<lrv && rrv<p.x ) {  //water surface
            mat=MAT2;
            ss=texture2D(iChannel0, p.xz).x*0.01; 
            h+=ss;
        } else { //terrain

            float t=texture2D(iChannel0, p.xz*0.001).x*1.25; //hills 
            float g=texture2D(iChannel0, vec2(p.x*0.2,p.z*0.5*0.2)).x*0.05;  //greens
            t+=g;
            //soft shore transition
            if ( p.x>lrv ) sw=t*clamp( (p.x - lrv  )*sw ,0.0,3.0); else
                           sw=t*clamp( abs(p.x - rrv)*sw ,0.0,3.0);
            h-=sw;               
            if (g>0.01) { mat=MAT3; ss=g; } else ss=sw;
            
        }

    return opU(chop(q),vec3(h,mat,ss));
}
/***********************************************/
#define suncolor vec3(0.8,0.8,0.2)  //vec3(0.8,0.7,0.9)
vec3 sky(vec3 rd, vec3 sky){
    float sa=max(dot(rd,sky),0.0);
    float v=pow(1.0-max(rd.y,0.0),8.0);
    vec3 s=mix(vec3(0.7,0.8,0.9),vec3(0.9,0.9,0.9),v);
        s=s+suncolor*sa*sa*0.2;
        s=s+suncolor*min(pow(sa,550.0)*1.5,0.3);
    return clamp(s,0.0,1.0);
}
/***********************************************/
vec3 clouds(vec3 ro, vec3 rd, vec3 col) {
    float v=(250.0-ro.y)/rd.y;
    rd.xz*=v;
    rd.xz+=ro.xz;
    rd.xz*=0.1;
    float f=noise2(rd.xz*0.05+iGlobalTime*0.2)*2.2;
    col=mix(col,vec3(0.75,0.75,0.73), clamp(f*rd.y,0.0,1.0));
    return col;
}
/***********************************************/
vec3 normal(vec3 p) {
	vec3 e=vec3(0.01,-0.01,0.0);
	return normalize( vec3(	e.xyy*DE(p+e.xyy).x +	e.yyx*DE(p+e.yyx).x +	e.yxy*DE(p+e.yxy).x +	e.xxx*DE(p+e.xxx).x));
}
/***********************************************/
float calcAO(vec3 p, vec3 n ){
	float ao = 0.0;
	float sca = 1.0;
	for (int i=0; i<AOSTEPS; i++) {
        	float h = 0.01 + 1.2*pow(float(i)/float(AOSTEPS),1.5);
        	float dd = DE( p+n*h ).x;
        	ao += -(dd-h)*sca;
        	sca *= 0.65;
		if( ao>0.99 ) break;
    	}
   return clamp( 1.0 - 1.0*ao, 0.0, 1.0 );
 //  return clamp(ao,0.0,1.0);
}
/***********************************************/
float calcSh( vec3 ro, vec3 rd, float s, float e, float k ) {
	float res = 1.0;
    for( int i=0; i<SHSTEPS; i++ ) {
    	if( s>e ) break;
        float h = DE( ro + rd*s ).x;
        res = min( res, k*h/s );
    	s += 0.02*SHPOWER;
		if( res<0.001 ) break;
    }
    return clamp( res, 0.0, 1.0 );
}
/***********************************************/
void rot2( inout vec3 p, vec3 r) {
	float sa=sin(r.y); float sb=sin(r.x); float sc=sin(r.z);
	float ca=cos(r.y); float cb=cos(r.x); float cc=cos(r.z);
	p*=mat3( cb*cc, cc*sa*sb-ca*sc, ca*cc*sb+sa*sc,	cb*sc, ca*cc+sa*sb*sc, -cc*sa+ca*sb*sc,	-sb, cb*sa, ca*cb );
}
/***********************************************/

void animate(inout vec3 ro) {
 ro.xz=rot(ro.xz,iGlobalTime*0.1);   
    ro.z+=sin(iGlobalTime*0.2)*5.0;
    ro.y+=5.0+abs(sin(iGlobalTime))*0.35;
}
/***********************************************/
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    vec2 p = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x/iResolution.y;	
	vec3 ta = vec3(0.0, 0.0, 0.0);
	vec3 ro =vec3(0.0, 0.0, -13.0);     //-10.0
	vec3 lig=normalize(vec3(0.0, 0.5, 5.0));
	vec3 sun=normalize(vec3(0.0, 0.5, 5.0));

animate(ro);
	
	vec2 mp=iMouse.xy/iResolution.xy;
	rot2(ro,vec3(mp.x,mp.y,0.0));
	rot2(lig,vec3(mp.x,-mp.y,0.0));

	vec3 cf = normalize( ta - ro );
    vec3 cr = normalize( cross(cf,vec3(0.0,1.0,0.0) ) );
    vec3 cu = normalize( cross(cr,cf));
	vec3 rd = normalize( p.x*cr + p.y*cu + 2.5*cf );

	vec3 col=vec3(1.0);
	/* trace */
	vec3 r=vec3(0.0);	
	float d=0.0;
	vec3 ww;
	for(int i=0; i<MARCHSTEPS; i++) {
		ww=ro+rd*d;
		r=DE(ww);		
        if( abs(r.x)<0.01 || r.x>FARCLIP ) break;
        d+=r.x;
	}
    r.x=d;
	/* draw */
	if( r.x<FARCLIP ) {
	    
	    vec2 rs=vec2(1.0,1.0);  //rim and spec

		if (r.y==MAT1) { col=smoothstep(vec3(0.0,0.0,0.06),vec3(0.63,0.63,0.605), vec3(r.z*3.1)); rs=vec2(0.3,0.6); }
		if (r.y==MAT2) { col=vec3(0.148,0.157,0.331); rs=vec2(1.0,0.6); }
		if (r.y==MAT3) { col=smoothstep(vec3(0.235,0.114,0.105), vec3(1.0,0.713,1.623), vec3(r.z*10.0));  rs=vec2(0.0,0.5); }

		if (r.y==MAT4) { col=vec3(0.95,0.34,0.21);  rs=vec2(0.2,1.0); }
		if (r.y==MAT5) { col=vec3(0.6,0.7,0.95);  rs=vec2(0.2,1.0); }
		if (r.y==MAT6) { col=vec3(0.1,0.2,0.1);  rs=vec2(0.2,1.0); }
		if (r.y==MAT7) { col=vec3(0.91,0.95,0.98);  rs=vec2(0.2,1.0); }

		vec3 nor=normal(ww);

    	float amb= 1.0;		
    	float dif= clamp(dot(nor, lig), 0.0,1.0);
    	float bac= clamp(dot(nor,-lig), 0.0,1.0);
    	float rim= pow(1.+dot(nor,rd), 3.0);
    	float spe= pow(clamp( dot( lig, reflect(rd,nor) ), 0.0, 1.0 ) ,16.0 );
    	float ao= calcAO(ww, nor);
    	float sh= calcSh(ww, lig, 0.01, 2.0, 4.0);

	    col *= 0.5*amb*AMBCOL*ao + 0.4*dif*DIFCOL*sh + 0.05*bac*BACCOL;
	    col += 0.3*rim*amb * rs.x;
    	col += 0.5*pow(spe,1.0)*sh * rs.y;

     
	}else {
        col=sky(rd,sun);
        col=clouds(ro,rd,col);
	}

    	
		
//	col*=exp(.01*-d); col*=0.9;	
    
	fragColor = vec4( col, 1.0 );
}