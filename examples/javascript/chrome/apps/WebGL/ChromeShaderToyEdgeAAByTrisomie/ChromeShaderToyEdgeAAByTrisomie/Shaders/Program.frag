// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// A list of usefull distance function to simple primitives, and an example on how to 
// do some interesting boolean operations, repetition and displacement.
//
// More info here: http://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm

// Tweaked by T21 to enable single pass distance based Anti-aliasing.
const float KEY_A  = 65.5/256.0;
const float KEY_T  = 84.5/256.0;

float sdPlane( vec3 p )
{
	return p.y;
}

float sdSphere( vec3 p, float s )
{
    return length(p)-s;
}

float udRoundBox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}

float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float sdCapsule( vec3 p, vec3 a, vec3 b, float r )
{
	vec3 pa = p - a;
	vec3 ba = b - a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	
	return length( pa - ba*h ) - r;
}

float length2( vec2 p )
{
	return sqrt( p.x*p.x + p.y*p.y );
}

float length6( vec2 p )
{
	p = p*p*p; p = p*p;
	return pow( p.x + p.y, 1.0/6.0 );
}

float length8( vec2 p )
{
	p = p*p; p = p*p; p = p*p;
	return pow( p.x + p.y, 1.0/8.0 );
}

float sdTorus82( vec3 p, vec2 t )
{
  vec2 q = vec2(length2(p.xz)-t.x,p.y);
  return length8(q)-t.y;
}

float sdTorus88( vec3 p, vec2 t )
{
  vec2 q = vec2(length8(p.xz)-t.x,p.y);
  return length8(q)-t.y;
}

float sdCylinder6( vec3 p, vec2 h )
{
  return max( length6(p.xz)-h.x, abs(p.y)-h.y );
}

//----------------------------------------------------------------------
vec2 opU( vec2 d1, vec2 d2 ) { return (d1.x<d2.x) ? d1 : d2; }


//----------------------------------------------------------------------

vec2 map( in vec3 pos )
{
    vec2 res = opU( vec2( sdPlane(     pos), 1.0 ),
	                vec2( sdSphere(    pos-vec3( 0.0,0.25, 0.0), 0.25 ), 646.9 ) );
    res = opU( res, vec2( udRoundBox(  pos-vec3( 1.0,0.3, 1.0), vec3(0.15), 0.1 ), 541.0 ) );
	res = opU( res, vec2( sdTorus(     pos-vec3( 0.0,0.25, 1.0), vec2(0.20,0.05) ), 425.0 ) );
    res = opU( res, vec2( sdCapsule(   pos,vec3(-1.3,0.20,-0.1), vec3(-1.0,0.20,0.2), 0.1  ), 331.9 ) );
	res = opU( res, vec2( sdTorus82(   pos-vec3( 0.0,0.25, 2.0), vec2(0.20,0.05) ),250.0 ) );
	res = opU( res, vec2( sdTorus88(   pos-vec3(-1.0,0.25, 2.0), vec2(0.20,0.05) ),143.0 ) );

    return res;
}

float softshadow( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<30; i++ )
    {
		if( t>maxt ) break;

		float h = map( ro + rd*t ).x;
        res = min( res, k*h/t );
        t += 0.02;
    }
    return clamp( res, 0.0, 1.0 );

}

vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float totao = 0.0;
    float sca = 1.0;
    for( int aoi=0; aoi<5; aoi++ )
    {
        float hr = 0.01 + 0.05*float(aoi);
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        totao += -(dd-hr)*sca;
        sca *= 0.75;
    }
    return clamp( 1.0 - 4.0*totao, 0.0, 1.0 );
}

vec3 GetColor(in vec3 ro, in vec3 rd, in float t, float m)
{
       vec3 pos = ro + t*rd;
        vec3 nor = calcNormal( pos );

		//col = vec3(0.6) + 0.4*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
		vec3 col = vec3(0.6) + 0.4*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
		
        float ao = calcAO( pos, nor );

		vec3 lig = normalize( vec3(-0.6, 0.7, -0.5) );
		float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);

		float sh = 1.0;
		if( dif>0.02 ) { sh = softshadow( pos, lig, 0.02, 10.0, 7.0 ); dif *= sh; }

		vec3 brdf = vec3(0.0);
		brdf += 0.20*amb*vec3(0.10,0.11,0.13)*ao;
        brdf += 0.20*bac*vec3(0.15,0.15,0.15)*ao;
        brdf += 1.20*dif*vec3(1.00,0.90,0.70);

		float pp = clamp( dot( reflect(rd,nor), lig ), 0.0, 1.0 );
		float spe = sh*pow(pp,16.0);
		float fre = ao*pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );

		return col*brdf + vec3(1.0)*col*spe + 0.2*fre*(0.5+0.5*col);	
}

vec4 Blend(in vec4 fg, in vec4 bg) {
	float a = 1.- fg.a;
	return fg + bg * a;
}

vec3 castRay(in vec3 ro, in vec3 rd, in float maxd, in float aa)
{
	vec4 col = vec4(0.0);	// Forground (pre-multiplied)
	float c = 1.;			// Keep track if we are moving closer or away from a surface
	
	float precis = 0.001;
    float h = 0.0, mh = 1e20;
    float t = 0.0, mt = 0.0;
    float m = -1.0, mm = -1.0;
	
    for(int i=0; i<100; i++ )
    {
        t += h;
	    vec2 res = map( ro+rd*t );
        h = res.x;
	    m = res.y;
		
		if(abs(h)<precis || t>maxd ) break;
		
		if(h < mh) { // closer
			c = 1.0;
			mh = h;
			mt = t;
			mm = m;
		} else if(c > 0.0) { // away
			c = 0.0;
			float s = (mt+1.0)/iResolution.y;
			if(mh < s) {
				float a = 1. - (mh / s);
				vec3 rgb = GetColor(ro, rd, mt, mm);
				col = Blend(col, vec4(rgb*a, a));
			}
		}
    }
	
	if(texture2D(iChannel3, vec2(KEY_A,0.75)).x > 0.) {
 		col = vec4(0.0);
	}
	
	if(texture2D(iChannel3, vec2(KEY_T,0.75)).x > 0.) {
		if(m > 1.) col.a = 1.;
		return vec3(pow(col.a, 2.2));
	}
	
	col = Blend(col, vec4(GetColor(ro, rd, t, m), 1.));
	
	col.rgb /= col.a;	
	col.rgb *= exp( -0.01*t*t );
	
    return col.rgb;
}



vec3 render( in vec3 ro, in vec3 rd )
{ 
    vec3 col = castRay(ro,rd,20.0, 1. - iMouse.z);
	return vec3( clamp(col,0.0,1.0) );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float time = 15.0 + iGlobalTime;

	// camera	
	vec3 ro = vec3( -0.5+3.2*cos(0.1*time + 6.0*mo.x), 1.0 + 2.0*mo.y, 0.5 + 3.2*sin(0.1*time + 6.0*mo.x) );
	vec3 ta = vec3( -0.5, -0.4, 0.5 );
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );

	
    vec3 col = render( ro, rd );

	col = sqrt( col );

    fragColor=vec4( col, 1.0 );
}