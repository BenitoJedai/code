float det(vec2 a, vec2 b) { return a.x*b.y-b.x*a.y; }

vec2 closestPointInSegment( vec2 a, vec2 b )
{
  vec2 ba = b - a;
  return a + ba*clamp( -dot(a,ba)/dot(ba,ba), 0.0, 1.0 );
}

// From: http://research.microsoft.com/en-us/um/people/hoppe/ravg.pdf
vec2 get_distance_vector(vec2 b0, vec2 b1, vec2 b2) {
	
  float a=det(b0,b2), b=2.0*det(b1,b0), d=2.0*det(b2,b1); // 𝛼,𝛽,𝛿(𝑝)
  
  if( abs(2.0*a+b+d) < 1000.0 ) return closestPointInSegment(b0,b2);
	
  float f=b*d-a*a; // 𝑓(𝑝)
  vec2 d21=b2-b1, d10=b1-b0, d20=b2-b0;
  vec2 gf=2.0*(b*d21+d*d10+a*d20);
  gf=vec2(gf.y,-gf.x); // ∇𝑓(𝑝)
  vec2 pp=-f*gf/dot(gf,gf); // 𝑝′
  vec2 d0p=b0-pp; // 𝑝′ to origin
  float ap=det(d0p,d20), bp=2.0*det(d10,d0p); // 𝛼,𝛽(𝑝′)
  // (note that 2*ap+bp+dp=2*a+b+d=4*area(b0,b1,b2))
  float t=clamp((ap+bp)/(2.0*a+b+d), 0.0 ,1.0); // 𝑡̅
  return mix(mix(b0,b1,t),mix(b1,b2,t),t); // 𝑣𝑖= 𝑏(𝑡̅)

}

float approx_distance(vec2 p, vec2 b0, vec2 b1, vec2 b2) {
  return length(get_distance_vector(b0-p, b1-p, b2-p));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 xy = fragCoord.xy;
	
	// can we scale?

	// xy.x *= 0.5;

	float zoom = 0.33  + 0.33 * sin(iGlobalTime);
//float zoom = fZoom;
// define dby host
	xy.x *= 1.0 / zoom;
	xy.y *= 1.0 / zoom;

	vec2 b0 = vec2(0.25, .5) * iResolution.xy;
	// vec2 b1 = vec2(0.5, .75 + .1*sin(iGlobalTime)) * iResolution.xy;
	vec2 b1 = iMouse.xy;
	vec2 b2 = vec2(.75, .5) * iResolution.xy;
	vec2 mid = .5*(b0+b2) + vec2(0.0,0.01);
	
	float d = approx_distance(xy, b0, b1, b2);
	//float thickness = 1.0;
	float thickness = 8.0;
	
	float a;
	
	if(d < thickness) {
	  a = 1.0;
	} else {
	  // Anti-alias the edge.
	  a = 1.0 - smoothstep(d, thickness, thickness+0.5);
	}
	

    // http://webcache.googleusercontent.com/search?q=cache:fYNtgTVCVwQJ:www.openscenegraph.org/projects/osg/browser/OpenSceneGraph-Data/trunk/shaders/volume_n_iso.frag%3Frev%3D8959%26format%3Draw+&cd=2&hl=en&ct=clnk&gl=ee

// error CreateShader {{ infoLog = ERROR: 0:75: '/' :  wrong operand types  no operation '/' exists that takes 
// a left-hand operand of type 'highp float' and a right operand of type 'const int' (or there is no acceptable conversion)
// http://www.lab4games.net/zz85/blog/2014/09/08/rendering-lines-and-bezier-curves-in-three-js-and-webgl/

// clip viewport on the right
if (fragCoord.x > (iResolution.x * zoom)) 
	// keep the previous bytes, from the previous program 
	discard;

if (fragCoord.y > (iResolution.y * zoom)) 
	// keep the previous bytes, from the previous program 
	discard;


if (fragCoord.x > (iResolution.x * 0.5)) 
{
	float aa = 1.0 - a;

	// how should we blend?
	fragColor = vec4(
		// are we supposed to be able to read pixels from last run?
		fragColor.r * aa + a * a,
		fragColor.g * aa + a * a,
		fragColor.b * aa + a * a, 
		fragColor.a * aa + a * a
	);
	return;
}

if (fragCoord.x < (iResolution.x * 0.25)) 
{
	// clear out bg
	fragColor = vec4(
		a,
		a,
		a, 
		0.5
	);
	return;
}



	// fragColor = vec4(a,1.0,1.0, 1.0);
	fragColor = vec4(
		a,
		1.0,
		1.0, 
		0.5
	);

	//fragColor = vec4(a,a,a, a);
}