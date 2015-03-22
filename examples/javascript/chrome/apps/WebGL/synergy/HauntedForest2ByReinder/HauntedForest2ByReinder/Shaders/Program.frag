// Created by Reinder Nijhoff 2013

#define RAYCASTSTEPS 12
#define GRIDSIZE 32.
#define GRIDSIZESMALL 10.
#define MAXDISTANCE 300.
#define SPEED 2.6

// comment for faster fog
#define FULL_PROCEDURAL

// uncomment for random trees and crashes in safari
//#define RANDOMTREES

float time = iGlobalTime + 299.;

//
// math functions
//

const mat3 m = mat3( 0.00,  0.80,  0.60,
                    -0.80,  0.36, -0.48,
                    -0.60, -0.48,  0.64 );

float hash( float n ) {
	return fract(sin(n)*43758.5453);
}
vec2 hash2( vec2 n ) {
	return fract(sin(vec2( n.x*n.y, n.x+n.y))*vec2(232.1459123,343.3490423));
}
vec3 hash3( vec2 n ) {
	return fract(sin(vec3(n.x, n.y, n+2.0))*vec3(2.5453123,1.1459123,3.3490423));
}

float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

#ifdef FULL_PROCEDURAL
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
#else
    float a = texture2D( iChannel0, x.xy/256.0 + (p.z+0.0)*120.7123 ).x;
    float b = texture2D( iChannel0, x.xy/256.0 + (p.z+1.0)*120.7123 ).x;
	return mix( a, b, f.z );
#endif	
}

float fbm( vec3 p ) {
    float f;
    f  = 0.5000*noise( p ); p = m*p*2.02;
    f += 0.2500*noise( p ); p = m*p*2.03;
    f += 0.1250*noise( p ); p = m*p*2.01;
    f += 0.0625*noise( p );
    return f;
}

float smin( float a, float b ) {
    float k = 0.06;
	float h = clamp( 0.5 + 0.5*(b-a)/k, 0.0, 1.0 );
	return mix( b, a, h ) - k*h*(1.0-h);
}

vec2 sdSegment2( vec3 a, vec3 b, vec3 p, float ll ) {
	vec3 pa = p - a;
	vec3 ba = b - a;
	float h = clamp( dot(pa,ba)*ll, 0.0, 1.0 );
	
	return vec2( length( pa - ba*h ), h );
}

vec2 distanceToSegmentb( vec3 a, vec3 b, vec3 p ) {
	vec3 pa = p - a;
	vec3 ba = b - a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	
	return vec2( length( pa - ba*h ), h );
}

bool intersectPlane(vec3 ro, vec3 rd, float height, out float dist) {	
	if (rd.y==0.0) {
		return false;
	}
	
	float d = -(ro.y - height)/rd.y;
	d = min(100000.0, d);
	if( d > 0. ) {
		dist = d;
		return true;
	}
	return false;
}


//
// Scene
//

struct Branche {
	vec3 a, b;
	float aradius, bradius;
	vec2 angle;
};

Branche branches[8];
	
void initTree( in vec2 pos ) {	
	vec3 sp = vec3( pos.x, -1., pos.y ) + 0.5*vec3( GRIDSIZE, 2., GRIDSIZE );
	vec2 sa = vec2( 0. );

	vec3 r = hash3( pos );
	float sr = max(0.85, r.y);
	float sl = max(0.5, r.x)*30.;

	for( int n=0; n<7; n++ ) {
		vec2 angle = (2.0*(1.-sr)) * (r.xy - vec2(0.5) ) + 0.3*sa;
		float er = max( 0.2, sr*(1.-r.z*0.65) );
		
		sl = max(0.25, r.x)*35.*max(1.5*er, 0.4);
		vec3 ep = sl*vec3( sin(angle.x), cos(angle.x)*cos(angle.y), sin(angle.y) );
				
		ep += sp;
		
		branches[n].a = sp;
		branches[n].b = ep;
		branches[n].aradius = sr; 
		branches[n].bradius = er; 
		branches[n].angle = angle;
		
		Branche prevbranche = branches[n];

#ifdef RANDOMTREES
		if( n>1 && r.y > 0.125 ) {
			prevbranche = branches[n-2];
		} else if( n>0 && r.x > 0.125 ) {
			prevbranche = branches[n-1];
		} 
#else
		if( n == 3 || n == 4 ) {
			prevbranche = branches[ 1 ];
		} else if( n == 1 || n == 2 ) {
			prevbranche = branches[ 0 ];
		} else if( n == 5 ) {
			prevbranche = branches[ 2 ];
		}
#endif
		
		sa = prevbranche.angle;
		sr = prevbranche.bradius;
		sp = prevbranche.b;
		
		
		r = fract( r*11.4362422416543 );
	}
	
	if( r.x < 0.02 ) {
		branches[5].b = vec3( pos.x, -1., pos.y ) + 0.25*vec3( GRIDSIZE, 2., GRIDSIZE );
		branches[5].a = branches[5].b + vec3( 0.04, 8., 0. );
		
		branches[6].a = branches[5].b + vec3( -1.7, 6., 0. );
		branches[6].b = branches[5].b + vec3(  1.4, 6.2, 0. );
		
	}
}

float mapTree( in vec3 pos ) {
	float dist = MAXDISTANCE;
	
	pos.x += 0.07*cos(pos.y*0.5);
	
// unrolling loop for windows
	vec2 d = distanceToSegmentb( branches[0].a, branches[0].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[0].aradius, branches[0].bradius, d.y) );
	d = distanceToSegmentb( branches[1].a, branches[1].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[1].aradius, branches[1].bradius, d.y) );
	d = distanceToSegmentb( branches[2].a, branches[2].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[2].aradius, branches[2].bradius, d.y) );
	d = distanceToSegmentb( branches[3].a, branches[3].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[3].aradius, branches[3].bradius, d.y) );
	d = distanceToSegmentb( branches[4].a, branches[4].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[4].aradius, branches[4].bradius, d.y) );
	d = distanceToSegmentb( branches[5].a, branches[5].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[5].aradius, branches[5].bradius, d.y) );
	d = distanceToSegmentb( branches[6].a, branches[6].b, pos  );
	dist = min(dist, d.x - 1.5*mix( branches[6].aradius, branches[6].bradius, d.y) );

	return dist;	
}


bool raymarchTree( in vec3 ro, in vec3 rd, in float maxdist, out float t ) {
	float precis = 0.02;
    float h = 1.0;
    t = 0.0;
    for( int i=0; i<24; i++ )
    {
        if( abs(h)<precis || t>maxdist ) continue;//break;
        t += h;
	    h = mapTree( ro+rd*t );
    }
	
    return ( t<=maxdist );
}


float mapClouds( in vec3 p ) {
	float d = 1.0-0.3*abs( p.y - 0.3);
	d -= 1.8 * fbm( p*0.35 );

	d = clamp( d, 0.0, 1.0 );
	
	return d;
}

vec3 raymarchClouds( in vec3 ro, in vec3 rd, in vec3 bcol, float tmax ) {
	float sum = 0.;

	float t = 4.5+texture2D( iChannel0, rd.xy*3.1415+vec2(0.1*time) ).x;
	for(int i=0; i<20; i++)	{
		if( t>tmax ) continue;
		vec3 pos = ro + t*rd;
		float c = mapClouds( pos*0.1 );

		c *= 0.25;
		sum = sum + c*(1.0 - sum);
		t += max(0.35,0.095*t);
	}
	return vec3( mix( bcol, vec3( 1.), clamp( sum, 0.0, 1.0 )) );
}

float trace(vec3 ro, vec3 rd, out vec3 intersection, out float dist, out bool isSky) {
	isSky = true; // sky
	dist = MAXDISTANCE;
	float distcheck;
		
	if( intersectPlane( ro,  rd, 0., distcheck) && distcheck < MAXDISTANCE ) {
		dist = distcheck;
		isSky = false;
	}
	
	// trace grid
	vec2 map = floor( ro.xz / GRIDSIZE ) * GRIDSIZE;
	float deltaDistX = GRIDSIZE*sqrt(1. + (rd.z * rd.z) / (rd.x * rd.x));
	float deltaDistY = GRIDSIZE*sqrt(1. + (rd.x * rd.x) / (rd.z * rd.z));
	float stepX, stepY, sideDistX, sideDistY;
	
	//calculate step and initial sideDist
	if (rd.x < 0.) {
		stepX = -GRIDSIZE;
		sideDistX = (ro.x - map.x) * deltaDistX / GRIDSIZE;
	} else {
		stepX = GRIDSIZE;
		sideDistX = (map.x + GRIDSIZE - ro.x) * deltaDistX / GRIDSIZE;
	}
	if (rd.z < 0.) {
		stepY = -GRIDSIZE;
		sideDistY = (ro.z - map.y) * deltaDistY / GRIDSIZE;
	} else {
		stepY = GRIDSIZE;
		sideDistY = (map.y + GRIDSIZE - ro.z) * deltaDistY / GRIDSIZE;
	}
	
	bool hit = false; 
	
	for( int i=0; i<RAYCASTSTEPS; i++ ) {
		float maxdist = distance( ro.xz, map );
		if( hit || maxdist-GRIDSIZE > dist ) continue;

		vec2 offset = (hash2( map+vec2(43.12,1.23) ) - vec2(0.5) )*(GRIDSIZESMALL);
		initTree( map+offset );
		
		
		if( raymarchTree( ro, rd, maxdist+GRIDSIZE, distcheck ) && distcheck < dist ) {
			dist = distcheck;
			isSky = false;
			hit = true;
		}
						
		if (sideDistX < sideDistY) {
			sideDistX += deltaDistX;
			map.x += stepX;
		} else {
			sideDistY += deltaDistY;
			map.y += stepY;
		}		
	}
	
	intersection = ro+rd*dist;
	
	if( isSky ) return 1.;
	else return clamp( dist/(MAXDISTANCE - 30. ), 0., 1.);
}


void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 q = fragCoord.xy/iResolution.xy;
	vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
	
	// camera	
	vec3 ce = vec3( cos( 0.232*time) * 10.+1.2, 7.+3.*cos(0.3*time), GRIDSIZE*(time/SPEED) );
	vec3 ro = ce;
	vec3 ta = ro + vec3( -sin( 0.232*time) * 10., -2.0+cos(0.23*time), 10.0 );
	
	float roll = -0.15*sin(0.5*time);
	
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( sin(roll), cos(roll),0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 1.5*cw );
	
	// raytrace
	bool sky;
	vec3 intersection;
	float dist;
	
	float a = trace(ro, rd, intersection, dist, sky);

	// ground and tree color, based on xz coord
	vec3 col = 0.12*(1.-a) * texture2D( iChannel1, intersection.xz*0.01 ).rgb;
	col = mix( col, vec3( 1. ), a );
	
	// sky color
	if( sky) {
		col =  mix( col, 2.5*vec3( 0.7, 0.7, 1.0), 2.*rd.y );
	}
	
	col +=  vec3( 0.25 ) *(1.- clamp( intersection.y*0.1, 0.0, 1. ) );	
	col = raymarchClouds( ro, rd, col, dist ).xyz;

	
	//-----------------------------------------------------
	// postprocessing
    //-----------------------------------------------------
    // gamma
	col = pow( col, vec3(0.45) );

    // desat
    col = mix( col, vec3(dot(col,vec3(0.333))), 0.2 );

    // tint
	col *= vec3( 1.0, 1.0, 1.0*0.9);

	// vigneting
    col *= 0.2 + 0.8*pow( 16.0*q.x*q.y*(1.0-q.x)*(1.0-q.y), 0.1 );


	fragColor = vec4( col,1.0);
}