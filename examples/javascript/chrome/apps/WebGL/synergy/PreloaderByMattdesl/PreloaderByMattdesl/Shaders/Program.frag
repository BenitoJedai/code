
//@mattdesl
// preloader page
// - uses colored noise to reduce banding in the gradient
// - simple circles with a slight delay and elastic easing

// Best viewed on resolution 1

#ifndef PI
#define PI 3.141592653589793
#endif

//http://mouaif.wordpress.com/2009/01/05/photoshop-math-with-glsl-shaders/
#define Blend(base, blend, funcf) 	vec3(funcf(base.r, blend.r), funcf(base.g, blend.g), funcf(base.b, blend.b))
#define BlendNormal(base, blend) 		(blend)
#define BlendOverlay(base, blend) 	Blend(base, blend, BlendOverlayf)
#define BlendOverlayf(base, blend) 	(base < 0.5 ? (2.0 * base * blend) : (1.0 - 2.0 * (1.0 - base) * (1.0 - blend)))

const vec3 COLOR_1 = vec3(187.0/255.0, 224.0/255.0, 140.0/255.0);
const vec3 COLOR_2 = vec3(83.0/255.0, 214.0/255.0, 48.0/255.0);

const vec3 COLORB_1 = vec3(232.0/255.0, 88.0/255.0, 38.0/255.0);
const vec3 COLORB_2 = vec3(208.0/255.0, 133.0/255.0, 83.0/255.0);

float time = iGlobalTime;
vec2 resolution = iResolution.xy;

#ifndef HALF_PI
#define HALF_PI 1.5707963267948966
#endif

//https://www.npmjs.org/package/glsl-easings
float elasticInOut(float t) {
  return t < 0.5
    ? 0.5 * sin(+13.0 * HALF_PI * 2.0 * t) * pow(2.0, 10.0 * (2.0 * t - 1.0))
    : 0.5 * sin(-13.0 * HALF_PI * ((2.0 * t - 1.0) + 1.0)) * pow(2.0, -10.0 * (2.0 * t - 1.0)) + 1.0;
}

float elasticOut(float t) {
  return sin(-13.0 * (t + 1.0) * HALF_PI) * pow(2.0, -10.0 * t) + 1.0;
}

float backOut(float t) {
  float f = 1.0 - t;
  return 1.0 - (pow(f, 3.0) - f * sin(f * PI));
}

//https://github.com/mattdesl/glsl-random
highp float random(vec2 co)
{
    highp float a = 12.9898;
    highp float b = 78.233;
    highp float c = 43758.5453;
    highp float dt= dot(co.xy ,vec2(a,b));
    highp float sn= mod(dt,3.14);
    return fract(sin(sn) * c);
}

float circle( vec2 uv, float thick, float delay ) {
	vec2 p = vec2(uv-0.5);
	//p *= mod(p*20.0, 10.0);
	
	float t = sin(time*1.0 + delay)/2.+0.5;
	float ease = elasticInOut(t);
	float anim = 30.0 + ease * 40.0;
	
	
	float size = 0.0 + anim;
	p.x *= resolution.x/resolution.y;
	
	float res = min(resolution.x, resolution.y);
    	float dist = length(p);
	float c = smoothstep(size/res, size/res + 2.0/res, dist);
	
	float s2 = size-thick;
	float c2 = smoothstep(s2/res, s2/res + 2.0/res, dist);
	
	vec2 norm = 2.0 * uv - 1.0;
	float phi = atan(norm.y, norm.x)/PI + time*0.15;
	float a = fract(phi);
	
	float rotation = smoothstep(1.5, 0.5, a);
	
	return mix(0.0, c2-c, rotation);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {

	vec2 uv = ( fragCoord.xy / resolution.xy ) ;
	
	vec3 color = vec3(0.0);
	
	vec2 pos = uv;
	
	pos /= (max(resolution.x, resolution.y)*0.76)/resolution; //scale it
	pos -= vec2(-0. + 100.0/resolution.x, 1.0); //offset

	float dist = length(pos);
	dist = smoothstep(2.25, 0.4, dist);
	
	color = mix(COLOR_2, COLOR_1, dist);
	
	vec3 second = mix(COLORB_1, COLORB_2, dist);
	color = mix(color, second, sin(time*0.15)/2.+.5);
	
	vec3 noise = vec3(random(uv * 1.5), random(uv * 2.5), random(uv));
	color = mix(color, BlendOverlay(color, noise), 0.07);

	color = mix(color, vec3(1.0), circle(uv, 2.0, 0.0));
	//color = mix(color, vec3(1.0), circle(uv, 5.0, 0.09));
	color = mix(color, vec3(1.0), circle(uv, 12.0, 0.05));
	fragColor = vec4( color, 1.0 );
	
}