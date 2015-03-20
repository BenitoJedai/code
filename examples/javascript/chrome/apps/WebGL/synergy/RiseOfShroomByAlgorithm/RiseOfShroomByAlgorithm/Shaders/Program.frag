/* created by okanovic dragan (abstract algorithm) */

// mushroom texture sampler
//----------------------------------------------------------------------
#define WW 0.0
#define OO 1.0
#define cc 2.0
#define _i 3.0

#define MKNum(a,b,c,d,e,f,g,h) (a+4.0*(b+4.0*(c+4.0*(d+4.0*(e+4.0*(f+4.0*(g+h*4.0)))))))

// as suggested by iq and movAX13h
float sample(in vec2 uv) {
	ivec2 p = ivec2(0.0);
	p.x = int( uv.x*16.);
	p.y = int( uv.y*16.);
    p.x = (p.x>7) ? 15-p.x : p.x;

    float rr=0.0;

    if(p.y== 0) rr=MKNum( _i,_i,_i,_i,_i,WW,WW,WW);
    if(p.y== 1) rr=MKNum( _i,_i,_i,WW,WW,WW,OO,cc);
    if(p.y== 2) rr=MKNum( _i,_i,WW,WW,OO,OO,OO,cc);
    if(p.y== 3) rr=MKNum( _i,WW,WW,cc,OO,OO,cc,cc);
    if(p.y== 4) rr=MKNum( _i,WW,OO,cc,cc,cc,cc,cc);
    if(p.y== 5) rr=MKNum( WW,WW,OO,OO,cc,cc,OO,OO);
    if(p.y== 6) rr=MKNum( WW,OO,OO,OO,cc,OO,OO,OO);
    if(p.y== 7) rr=MKNum( WW,OO,OO,OO,cc,OO,OO,OO);
    if(p.y== 8) rr=MKNum( WW,OO,OO,cc,cc,OO,OO,OO);
    if(p.y== 9) rr=MKNum( WW,cc,cc,cc,cc,cc,OO,OO);
    if(p.y==10) rr=MKNum( WW,cc,cc,WW,WW,WW,WW,WW);
    if(p.y==11) rr=MKNum( WW,WW,WW,WW,_i,_i,WW,_i);
    if(p.y==12) rr=MKNum( _i,WW,WW,_i,_i,_i,WW,_i);
    if(p.y==13) rr=MKNum( _i,_i,WW,_i,_i,_i,_i,_i);
    if(p.y==14) rr=MKNum( _i,_i,WW,WW,_i,_i,_i,_i);
    if(p.y==15) rr=MKNum( _i,_i,_i,WW,WW,WW,WW,WW);

    return mod( floor(rr / pow(4.0,float(p.x))), 4.0 )/3.0;
}

// distance field formulas by inigo quilez
//----------------------------------------------------------------------
float sdPlane( vec3 p ) {
	return p.y;
}
float sdCube( vec3 p, float a ) {
  return length(max(abs(p) - vec3(a),0.0));
}
vec2 opU( vec2 d1, vec2 d2 ) {
	return (d1.x<d2.x) ? d1 : d2;
}
float smin( float a, float b) {
	float k=3.1;
	float res = exp( -k*a ) + exp( -k*b );
    return -log( res )/k;
	k = 0.9;
	float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}
vec2 map( in vec3 pos ) {
	float d= smin( sdPlane(pos), sdCube(pos-vec3( 0.0, 0.5, 0.0), 0.5 ) );
	return vec2(d,1.);
}
//----------------------------------------------------------------------
vec3 castRay( in vec3 ro, in vec3 rd, in float maxd ) {
	float precis = 0.001;	// when to call a hit
    float h=precis*2.0;		// howmuch to move along the ray
    float t = 0.0;			// moved already
    vec2 m = vec2(-1.0);	// color/uv, depends on use, here - uv
    vec3 pos = vec3(0.0);	// 3d position
    for( int i=0; i<60; i++ )
    {
        if( abs(h)<precis||t>maxd ) break;	// voila
        t += h;					//  move
        pos = ro+rd*t;			// update current 3d position
	    vec2 res = map( pos );	// get de
        h = res.x;				// get de
	    m = vec2(res.y);		// uv/color
    }
    // corrections of uv
    if( abs(pos.x)<0.5 && pos.y>0.99 && abs(pos.z)<0.5) { m= vec2(.5+pos.x, .5+pos.z); }
    else {	m = vec2(pos.x/10.5+1.5, pos.z/10.5+1.5);	}
    // it was a good day :)
    return vec3( t, m );
}
// approximate shadow :: http://www.iquilezles.org/www/articles/rmshadows/rmshadows.htm
float softshadow( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<60; i++ )
    {
		if( t<maxt )
		{
	        float h = map( ro + rd*t ).x;
	        res = min( res, k*h/t );
	        t += 0.02;
		}
    }
    return clamp( res, 0.0, 1.0 );
}
// approximate normal :: http://code4k.blogspot.com/2009/10/potatro-and-raymarching-story-of.html
vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}
// code by inigo quilez
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
// brdf thingy, mostly inigo's code
vec3 render( in vec3 ro, in vec3 rd )
{ 
    vec3 col = vec3(0.0);
    vec3 res = castRay(ro,rd,20.0);
    float t = res.x;
	vec2 uv = res.yz;
    vec3 pos = ro + t*rd;
    vec3 nor = calcNormal( pos );
    float ao = calcAO( pos, nor );

    // sampling the mushroom "texture" and coloring
	col = (uv.x>1.) ? vec3( (uv-vec2(1.))*clamp(0.8+0.3*sin(iGlobalTime), 0.0, 1.0), 1.) : vec3(sample(uv));

	vec3 lig = normalize( vec3(-0.6, 0.7, -0.5) );	// light direction (0., 1., 0.)
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

	col = col*brdf + vec3(1.0)*col*spe + 0.2*fre*(0.5+0.5*col);

	col *= exp( -0.01*t*t );

	return vec3( clamp(col,0.0,1.0) );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
	
	float time = 5.0 + 4.*iGlobalTime + 20.0*iMouse.x/iResolution.x;

	// camera	
	vec3 ro = vec3( 2.*cos(0.2*time),
					2.0 + sin(0.2*time),
					2.*sin(0.2*time) );		// camera position aka ray origin

	vec3 ta = vec3( 0.0, 0.5, 0.0 );		// camera look-at position
	
	// camera tx
	vec3 eye = normalize( ta-ro );						// eye vector
	vec3 cp = vec3( 0.0, 1.0, 0.0 );					// "up" vector
	vec3 hor = normalize( cross(eye, cp) );				// horizontal vector
	vec3 up = normalize( cross(hor, eye) );				// up vector
	vec3 rd = normalize( p.x*hor + p.y*up + 2.5*eye );	// ray direction
	
	// main thing
	vec3 col = sqrt(render( ro, rd ));
	
	// vignette
    col *= 0.2 + 0.8*pow( 16.0*q.x*q.y*(1.0-q.x)*(1.0-q.y), 0.1 );

	// ta-da!
    fragColor=vec4( col, 1.0 );
}