//#define fizzer		// :D

#define marchsteps 90

float tnoise(vec3 p) {
	float nz=p.z*64.0;
	vec2 oz=vec2(0.321,0.123);
	vec2 uv1=p.xy+oz*floor(nz);
	vec2 uv2=uv1+oz;
	return mix(texture2D(iChannel0, uv1.xy, -100.).x, texture2D(iChannel0, uv2.xy, -100.).x, fract(nz)-0.5);
}


vec2 opU(vec2 a, vec2 b) {
	return mix(a, b, step(b.x, a.x));
}

float pyramid( vec3 p, float h) {
	vec3 q=abs(p);
	return max(-p.y, (q.x+q.y+q.z-h)/3.0 );
}

float terrain(vec3 p) {
	return p.y - 0.1 - p.z*p.z*0.001 + p.x*atan(p.x)*0.035 + sin(p.x)*sin(p.z)*0.15 ;
}

vec2 map(vec3 p) {
	
	vec2 ret=vec2(pyramid(p,1.0),1.0);
	ret=opU(ret, vec2(pyramid(p-vec3(-2.0,0.0,1.0),1.0),1.0) );
	ret=opU(ret, vec2(pyramid(p-vec3(-0.75,0.0,2.5),1.0),1.0) );
	ret=opU(ret, vec2(pyramid(p-vec3(1.5,0.0,1.5),1.5),1.0) );

	ret=opU(ret, vec2( terrain(p), 2.0));
	
	return ret;
}

vec3 normal(vec3 p ) {
	vec3 e=vec3(0.01,-0.01,0.0);
	return normalize( vec3(	e.xyy*map(p+e.xyy).x +	e.yyx*map(p+e.yyx).x +	e.yxy*map(p+e.yxy).x +	e.xxx*map(p+e.xxx).x));
}

// 		   ray ori	ray dir	 start    max dist steps  hit ori
vec2 march(vec3 ro, vec3 rd, float s, float d, int e, out vec3 ho) {
	vec2 r=vec2(0.0);
	for(int i=0; i<marchsteps; i++) {
		ho=ro+rd*s;
		r=map(ho);
	        if((abs(r.x) < 0.01) || (r.x > d) || (i > e))  { break; }
		s+=r.x;
	}
	if (s >= d) { r.y=0.0; }
	r.x=s;
	return r;
}

float cao(vec3 pos, vec3 nor ){
	float totao = 0.0;
	float sca = 1.0;
	for (int i=0; i<10; i++) {
        	float hr = 0.01 + 0.05*float(i);
        	vec3 aopos =  nor * hr + pos;
        	float dd = map( aopos ).x;
        	totao += -(dd-hr)*sca;
        	sca *= 0.75;
    	}
    return clamp( 1.0 - 4.0*totao, 0.0, 1.0 );
}

float csh( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k ) {
    float res = 1.0;
    float dt = 0.02;
    float t = mint;
    for( int i=0; i<10; i++ ) {
        float h = map( ro + rd*t ).x;
        res = min( res, k*h/t );
        t += max( 0.05, dt );
    }
    return clamp( res, 0.0, 1.0 );
}


vec3 shade(vec3 rgb, vec3 ro, vec3 rd, vec3 nor, vec3 lig, vec3 ho ) {
	float amb =0.2*nor.y;		
	float dif =0.5*clamp(dot(nor, lig), 0.0,1.0);
	float bac =0.1*clamp(dot(nor,-lig), 0.0,1.0);
	float rim =0.3*pow(1.+dot(nor,rd), 5.0);
#ifdef fizzer
	float spe =0.0;
#else	
	float spe =0.5*pow(clamp( dot( lig, reflect(rd,nor) ), 0.0, 1.0 ) ,16.0 );
#endif
	float ao=cao(ho, nor);
	float sh=csh(ho, lig, 0.05, 4.0, 4.0);
	
	vec3 col  = (amb+dif+ao+bac+sh)*vec3(1.);
		 col *= rgb;
		 col += (rim+spe)*vec3(1.);

    return col;
}

vec3 getmat(vec2 o, vec3 uvw) {
	vec3 col=vec3(0.0);
	if (o.y==1.0) { 
			col=vec3(0.6,0.37,0.18); 
			col=mix(col, texture2D(iChannel1, uvw.xy*4.0, -100.).xyz, 0.5 );
			col=mix(col,vec3(1.0)*tnoise(uvw)*.6, 0.25 );
	}
	if (o.y==2.0) { 
			col=vec3(0.6,0.37,0.18); 
			col=mix(col,vec3(1.0)*tnoise(uvw), 0.25 );
			
	}
//	if (o.y==3.0) { col=vec3(0.0,0.0,1.0); }
	return col;	
}

vec3 fog(vec3 col, float d, vec3 p ) {
	float n=smoothstep(-0.15,0.4,terrain(p));
	float fog=exp(-0.05 * d*d);
#ifdef fizzer
	return mix(vec3(0.85,0.85,0.85)*0.2, col, n*fog);
#else
	return mix(vec3(0.85,0.85,0.85), col, n*fog);
#endif
}



vec3 getbg(vec3 rd) {
#ifdef fizzer
	float v=pow(1.0-max(rd.y,0.0),10.);
	vec3 sky=mix( vec3(0.6,0.8,0.9), vec3( 1.0, 1.0, 1.0), v)*0.5;
#else
	float v=pow(1.0-max(rd.y,0.0),6.);
	vec3 sky=mix( vec3(0.6,0.8,0.9), vec3( 1.0, 1.0, 1.0), v);
#endif
	//sun
	
	//this sun looks really crappy

	if(rd.z<0.0) {
		float a=atan(rd.x,rd.y);
		float r = length(rd.xy) *4.0;
		
  	float b = 0.9 * sin(.0 * r - 3.0*iGlobalTime - 6.0*a);
  	b = 0.4 / r + cos(10.0 * a + b ) / (350.0 * r);
	b = b * smoothstep(0.5, 1.2,b);

#ifdef fizzer
	vec3 sun=vec3(1.0,1.0,0.7)*b;
#else		
	vec3 sun=vec3(0., 0., 2.2) - vec3(b,b,b);
  	sun.xy=1.-sun.xy;
#endif
		
	sky=mix(sky,sun,b);
		
	}

	
	return clamp(sky, 0.0, 1.0);
}

vec2 rot(vec2 k, float t) {
    return vec2(cos(t)*k.x-sin(t)*k.y,sin(t)*k.x+cos(t)*k.y);
}


void animate(inout vec3 ta, inout vec3 ro, inout vec3 lig) {
	float t=iGlobalTime*0.25;

	ta.x -= sin(t)*1.7;
	ta.z += cos(t)*0.8;

	ro.z += cos (t)*2.;
	ro.xz=rot(ro.xz, t );
	
	ro.y+=sin(t)*0.5;
	
	lig.z -= cos(t)*2.;
	lig.xz=rot(lig.xz, -t );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	
	vec2 q = fragCoord.xy / iResolution.xy;
    vec2 p = -1.0 + 2.0 * q;
    p.x *= iResolution.x/iResolution.y;

	vec3 ta = vec3(0.0, 0.0, 1.0);
	vec3 ro = vec3(0.0, 1.2, -3.5);
	vec3 lig=normalize(vec3(0.0, 1.2, 3.5));
	animate(ta,ro,lig);
	vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(0.0,1.0,0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
	vec3 rd = normalize( p.x*uu + p.y*vv + 2.5*ww );

	
	
	vec3 ho;
	vec2 o=march(ro,rd,0.0,30.0,marchsteps, ho);
	
	vec3 col=vec3(1.0);

	if (o.y<0.5) {
		col=getbg(rd);
		o.x=(iResolution.y-fragCoord.y)*0.3;
	} else {
		vec3 nor=normal(ho);	
		col=getmat(o,ho);
		col=shade(col, ro, rd, nor, lig, ho);
		
		col=fog(col,o.x, ho);
	}
	
	col*=exp(.01*o.x); col*=1.1;

	fragColor=vec4( col, 1.0);
}		 