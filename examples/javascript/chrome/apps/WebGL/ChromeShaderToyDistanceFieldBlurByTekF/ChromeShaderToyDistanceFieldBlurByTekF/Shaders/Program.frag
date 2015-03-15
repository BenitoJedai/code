// Ben Weston 2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

const float tau = 6.28318530717958647692;


// Gamma correction
#define GAMMA (2.2)

vec3 ToLinear( in vec3 col )
{
	// simulate a monitor, converting colour values into light values
	return pow( col, vec3(GAMMA) );
}

vec3 ToGamma( in vec3 col )
{
	// convert back into colour values, so the correct light will come out of the monitor
	return pow( col, vec3(1.0/GAMMA) );
}

// Set up a camera looking at the scene.
// origin - camera is positioned relative to, and looking at, this point
// distance - how far camera is from origin
// rotation - about x & y axes, by left-hand screw rule, relative to camera looking along +z
// zoom - the relative length of the lens
void CamPolar( out vec3 pos, out vec3 ray, in vec3 origin, in vec2 rotation, in float distance, in float zoom, in vec2 fragCoord )
{
	// get rotation coefficients
	vec2 c = vec2(cos(rotation.x),cos(rotation.y));
	vec4 s;
	s.xy = vec2(sin(rotation.x),sin(rotation.y)); // worth testing if this is faster as sin or sqrt(1.0-cos);
	s.zw = -s.xy;

	// ray in view space
	ray.xy = fragCoord.xy - iResolution.xy*.5;
	ray.z = iResolution.y*zoom;
	ray = normalize(ray);
	
	// rotate ray
	ray.yz = ray.yz*c.xx + ray.zy*s.zx;
	ray.xz = ray.xz*c.yy + ray.zx*s.yw;
	
	// position camera
	pos = origin - distance*vec3(c.x*s.y,s.z,c.x*c.y);
}

vec2 Noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
	f = f*f*(3.0-2.0*f);
//	vec3 f2 = f*f; f = f*f2*(10.0-15.0*f+6.0*f2);

	vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;

#if (1)
	vec4 rg = texture2D( iChannel0, (uv+0.5)/256.0, -100.0 );
#else
	// on some hardware interpolation lacks precision
	vec4 rg = mix( mix(
				texture2D( iChannel0, (floor(uv)+0.5)/256.0, -100.0 ),
				texture2D( iChannel0, (floor(uv)+vec2(1,0)+0.5)/256.0, -100.0 ),
				fract(uv.x) ),
				  mix(
				texture2D( iChannel0, (floor(uv)+vec2(0,1)+0.5)/256.0, -100.0 ),
				texture2D( iChannel0, (floor(uv)+1.5)/256.0, -100.0 ),
				fract(uv.x) ),
				fract(uv.y) );
#endif			  

	return mix( rg.yw, rg.xz, f.z );
}


float DistanceField( vec3 pos )
{
	return min ( pos.y+1.0, length(vec3(pos.xy,(fract(pos.z/3.0+.5)-.5)*3.0))-1.0 )
			+ .1*min(.5,Noise(pos*5.0+iGlobalTime*vec3(0,0,0)).x)
			;
}


// approximate a smoothed normal
vec3 GetNormal( vec3 pos, float blur )
{
	vec2 delta = vec2(0,blur+.001);
	vec3 grad;
	grad.x = DistanceField( pos+delta.yxx )-DistanceField( pos-delta.yxx );
	grad.y = DistanceField( pos+delta.xyx )-DistanceField( pos-delta.xyx );
	grad.z = DistanceField( pos+delta.xxy )-DistanceField( pos-delta.xxy );
	return normalize(grad);
}


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	float T = iGlobalTime;
	
	vec3 pos, ray;
	CamPolar( pos, ray, vec3(0), vec2(.2,-.25) + vec2(.1*sin(T/19.0),-.2*cos(T/17.0)), 8.0, 2.0, fragCoord );
	
	float aperture = .8;
	float focalPlane = sin((T-sin(T))*.7)*3.4 + length(pos);
	float focus = focalPlane; // todo: correct for screen-pos so flat not curved plane
	
	vec4 result = vec4(0);
	float t = 0.0;
	float oh = 0.0;
	for ( int i=0; i < 100; i++ )
	{
		if ( result.a > .99 )
			break; //continue; // some hardware prefers continue to break
		
		float blur = aperture*abs(t/focus-1.0);

		vec3 p = pos+ray*t;
		float h = DistanceField( p ) + blur*.5; // shrink the "solid" part to be centre of the blur
		
		if ( h < blur )
		{
			//add blurred contribution for this sample
			vec3 n = GetNormal( p, blur );
			vec4 sample;
			
			sample.rgb = mix( vec3(.2,.5,.5), vec3(.7,.5,.3), clamp((p.y+.95)/blur,0.0,1.0) );

			// lighting - this is too hard-edged, should blur
			sample.rgb *= (.3+max(dot(n,normalize(vec3(-2,3,-1))),.0));
			
			sample.a = pow(clamp(1.0-h/blur,0.0,1.0),2.0);

			// modulate by the length of the step
			sample.a = pow(sample.a,(h+oh)/2.0);

			result += vec4(sample.rgb,1)*sample.a*(1.0-result.a);
		}
		
		t = t+h;
		oh = h;
	}
	
	result.rgb *= 1.0/(result.a+.0001);
	
	result.rgb = mix( vec3(.6,.7,.8), result.rgb, min(1.0,result.a/.99) );

	fragColor.rgb = ToGamma(result.rgb);
}