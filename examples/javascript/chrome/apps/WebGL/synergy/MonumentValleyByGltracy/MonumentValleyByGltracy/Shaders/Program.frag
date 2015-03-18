// ray marching
const int max_iterations = 128;
const float stop_threshold = 0.01;
const float grad_step = 0.05;
const float clip_far = 1000.0;

// math
const float PI = 3.14159265359;
const float DEG_TO_RAD = PI / 180.0;

mat3 roty( float angle ) {
	float c = cos( angle );
	float s = sin( angle );
	
	return mat3(
		c  , 0.0, -s  ,
		0.0, 1.0, 0.0,
		s  , 0.0, c  
	);
}

mat3 rotzx( vec2 angle ) {
	vec2 c = cos( angle );
	vec2 s = sin( angle );
	
	return
	mat3(
		c.y, s.y, 0.0,
		-s.y, c.y, 0.0,
		0.0, 0.0, 1.0
	) *
	mat3(
		1.0, 0.0, 0.0,
		0.0, c.x, s.x ,
		0.0, -s.x, c.x
	);
}

// distance function
float dist_sphere( vec3 pos, float r ) {
	return length( pos ) - r;
}

float dist_box( vec3 pos, vec3 size ) {
	return length( max( abs( pos ) - size, 0.0 ) );
}

float dist_cone( vec3 p, float r, float h )
{
	vec2 c = normalize( vec2( h, r ) );
    float q = length(p.xy);
    return max( dot(c,vec2(q,p.z)), -(p.z + h) );
}

float dist_capsule( vec3 p, vec3 a, vec3 b, float r )
{
    vec3 pa = p - a, ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h ) - r;
}

vec2 princess( vec3 p ) {
	p = vec3( p.x,abs(p.y),p.z );
	
	// hat
	float d0 = dist_cone( roty( radians( 70.0  ) ) * ( p - vec3( -3.4, 0.0, 2.04 ) ), 0.97, 3.3 );
	// skirt
	float d1 = dist_cone( roty( radians( -10.0 ) ) * ( p - vec3( 0.03, 0.0, -0.1 ) ), 1.6, 2.6 );
	// head
	float d2 = dist_sphere( p + vec3( 0.0, 0.0, -0.8 ), 1.0 );
	// neck
	float d3 = dist_capsule( p, vec3( 0.0, 0.0, -0.5 ), vec3( 0.0, 0.0, 1.0 ), 0.18 );
	// legs
	float d4 = dist_capsule( p + vec3( 0.0, -0.4, 0.0 ), vec3( 0.0, 0.0, -4.6 ), vec3( 0.0, 0.0, -2.0 ), 0.15 );
	// feet
	float d5 = dist_cone( roty( -90.0 * DEG_TO_RAD ) * ( p + vec3( -0.53, -0.4, 4.58 ) ), 0.16, 0.5 );

	float g0 = min( min( d0, d1 ), min( d4, d5 ) );

	float d = g0;
	float id = 1.0;
	
	if ( d > d3 ) { d = d3; id = 0.0; }
	if ( d > d2 ) { d = d2; id = step( 0.2, p.x ); }
	
	return vec2( d, id );
}

// distance
vec2 dist_field( vec3 p ) {
	return princess( p + vec3( 0.0, 0.0, -0.85 ) );
}

// gradient
vec3 gradient( vec3 pos ) {
	const vec3 dx = vec3( grad_step, 0.0, 0.0 );
	const vec3 dy = vec3( 0.0, grad_step, 0.0 );
	const vec3 dz = vec3( 0.0, 0.0, grad_step );
	return normalize (
		vec3(
			dist_field( pos + dx ).x - dist_field( pos - dx ).x,
			dist_field( pos + dy ).x - dist_field( pos - dy ).x,
			dist_field( pos + dz ).x - dist_field( pos - dz ).x			
		)
	);
}

// ray marching
vec2 ray_marching( vec3 origin, vec3 dir, float start, float end ) {
	float depth = start;
	for ( int i = 0; i < max_iterations; i++ ) {
		vec2 hit = dist_field( origin + dir * depth );
		if ( hit.x < stop_threshold ) {
			return hit;
		}
		depth += hit.x;
		if ( depth >= end) {
			break;
		}
	}
	return vec2( end, -1.0 );
}

// othogonal ray direction
vec3 ray_dir( float fov, vec2 size, vec2 pos ) {
	vec2 xy = pos - size * 0.5;

	float cot_half_fov = tan( ( 90.0 - fov * 0.5 ) * DEG_TO_RAD );	
	float z = size.y * 0.5 * cot_half_fov;
	
	return normalize( vec3( xy, -z ) );
}

vec3 EvalPixel( vec2 pix ) {
	// default ray dir
	vec3 dir = ray_dir( 45.0, iResolution.xy, pix );
	
	// default ray origin
	vec3 eye = vec3( 0.0, 0.0, 13.0 );

	// rotate camera
	mat3 rot = rotzx( vec2( 70.0 * DEG_TO_RAD, 0.7 * iGlobalTime ) );
	dir = rot * dir;
	eye = rot * eye;
	
	// ray marching
	vec2 hit = ray_marching( eye, dir, 0.0, clip_far );
	if ( hit.x >= clip_far ) {
		return mix( vec3( 0.0, 0.3, 0.4 ), vec3( 0.17, 0.7, 0.7 ), pix.y / iResolution.y );
	}
	
	// shading
	return vec3( hit.y );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec3 color = vec3( 0.0 );

#if 1
	color += EvalPixel( fragCoord.xy                    );
	color += EvalPixel( fragCoord.xy + vec2( 0.5, 0.0 ) );
	color += EvalPixel( fragCoord.xy + vec2( 0.0, 0.5 ) );
	color += EvalPixel( fragCoord.xy + vec2( 0.5, 0.5 ) );
	
	color *= 0.25;
#else
	color = EvalPixel( fragCoord.xy );
#endif	
	
	fragColor = vec4( color, 1.0 );
}