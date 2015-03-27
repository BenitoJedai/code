
//---------------------------------------------------------
// MarbleFantasies.glsl    by Antony Holzer   3/2015
// original:  https://www.shadertoy.com/view/MtX3Ws
// info:      3d marble with transparent satiny inner structure
// changes:   added sphere antialiasing, extracted constants
//            texture will be generated   
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// tags:      raytrace, 3d, sphere, noise, antialiasing, transparency, satiny
//---------------------------------------------------------

#ifdef GL_ES
  precision mediump float;
#endif

//uniform float time;
uniform vec2 mouse;
uniform vec2 resolution;

#define time iGlobalTime
#define mouse iMouse
#define resolution iResolution

//---------------------------------------------------------
// here some values to play for you ...

#define MAP_OCTAVE 7
#define SPHERE_RADIUS 1.3
#define BACK_COLOR vec3(0.4, 0.05, 0.05)
#define INNER_COLOR vec3(0.2, 0.2, 0.6)
#define GLAS_COLOR vec3(0.5, 0.5, 0.9)
#define SHOW_REFLECTION 
#define ANIMATE_PATTERN
#define ROTATE_SCENE

//---------------------------------------------------------

vec2 csqr( vec2 a )  { return vec2( a.x*a.x - a.y*a.y, 2.*a.x*a.y  ); }

mat2 rot(float a)    { return mat2(cos(a),sin(a),-sin(a),cos(a)); }

float map(in vec3 p) 
{
    float res = 0.;
    vec3 c = p;
    for (int i = 0; i < MAP_OCTAVE; ++i) 
    {
        p =.7*abs(p)/dot(p,p) -.7;
        p.yz += csqr(p.yz);
        p=p.zxy;
        res += exp(-19.0 * abs(dot(p,c)));
    }
    return res;
}

vec2 iSphere( in vec3 ro, in vec3 rd, in vec4 sph )//from iq
{
	vec3 oc = ro - sph.xyz;
	float b = dot( oc, rd );
	float c = dot( oc, oc ) - sph.w*sph.w;
	float h = b*b - c;
	if( h<0.0 ) return vec2(-1.0);
	h = sqrt(h);
	return vec2(-b-h, -b+h );
}

vec3 raymarch( in vec3 ro, vec3 rd, vec2 tminmax )
{
    float t = tminmax.x;
    float dt = 0.02;
    #ifdef ANIMATE_PATTERN
      dt = 0.02 + 0.01*cos(time*0.5);  //animated
    #endif
    vec3 col= vec3(0.);
    float c = 0.;
    for( int i=0; i<64; i++ )
    {
        t+=dt*exp(-2.*c);
        if(t>tminmax.y)break;
        vec3 pos = ro+t*rd;
        c = 0.45 * map(ro+t*rd);               
        col = 0.98*col + 0.08*vec3(c*c, c, c*c*c);  //green	
        col = 0.99*col + 0.08*vec3(c*c*c, c*c, c);  //blue
        col = 0.99*col + 0.08*vec3(c, c*c*c, c*c);  //red
    }    
    return col;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    //float time = iGlobalTime;
    vec2 q = fragCoord.xy / resolution.xy;
    vec2 p = -1.0 + 2.0 * q;
    p.x *= resolution.x / resolution.y;
    vec2 m = vec2(-0.5);
//    if( mouse.z > 0.0 )   // mouse pressed?
	m = mouse.xy / resolution.xy * 3.14;
//    m += mouse.xy * 3.14;    // use on glslsandbox.com

    // camera
    vec3 ro = vec3(4.);
    ro.yz *= rot(m.y);
    ro.xz *= rot(m.x);
    #ifdef ROTATE_SCENE
      ro.xz *= rot(0.07*time);
    #endif
    vec3 ta = vec3( 0.0);
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(0.0,1.0,0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
    vec3 rd = normalize( p.x*uu + p.y*vv + 4.0*ww );
    
    vec2 tmm = iSphere( ro, rd, vec4(0.,0.,0.,SPHERE_RADIUS) );

    // raymarch
    vec3 col = raymarch(ro,rd,tmm);  // get sphere Color

    const float taa = 6.3;     // antialiasing limit
    float aa = (2.2*(tmm.x - taa));
    if ((tmm.x < 0.0) || (tmm.x > taa))
    {
        vec3 bgCol = BACK_COLOR * map(rd);   // background color
        if (tmm.x > taa)
    	{
	    //bgCol = vec3(9.0, 5.0, 0.0);   // for testing antialiasing
            col = mix( col, bgCol, aa);
        }
        else col = bgCol;
    }

    if (tmm.x > 0.0)             // add sphere reflection ?
    {	        
        vec3 nor = reflect(rd, (ro+tmm.x*rd) * 0.5);  // sphere normal
        float fre = pow(0.3+ clamp(dot(nor,rd), 0.0, 1.0), 3. )*1.3;
        // add reflecting color
        col += GLAS_COLOR * fre * 0.1 *(1.0-aa);   // add some solid glas
        #ifdef SHOW_REFLECTION
	  col += BACK_COLOR * map(nor) * fre * 0.3 *(1.0-aa);
	#endif
    }
	
    // shade
    col = 0.5 *(log(1.0 + col));
    col = clamp(col, 0., 1.0);
    fragColor = vec4( col, 1.0 );
}
