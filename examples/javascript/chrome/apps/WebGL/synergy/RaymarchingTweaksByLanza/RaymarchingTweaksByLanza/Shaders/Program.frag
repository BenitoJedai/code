const float EPS = 0.03; // large epsilon to smooth edges and lower artifacts
const int MAXI = 150;	 
const float MAXD = 30.; 

vec3 hsv(float h,float s,float v) { 
	return mix(vec3(1.),clamp((abs(fract(h+vec3(3.,2.,1.)/3.)*6.-3.)-1.),0.,1.),s)*v;
}

vec3 repeater(vec3 p) {
	vec3 repeat = vec3(1.1);
	return mod(p+ + vec3(.0,.0,-iGlobalTime),repeat) - 0.5 * repeat;
}

float scenedist(vec3 p){ 

	float s2 = length(p)-2.;


	float s3 = length(max(abs(repeater(p)) -vec3(.2), .0)) - 0.2; 
	
	return max(s2, s3);
}

vec3 getNormal(vec3 pos){ 
	vec2 eps = vec2(0.0, EPS);
	return normalize(vec3( 
			scenedist(pos + eps.yxx) - scenedist(pos - eps.yxx),
			scenedist(pos + eps.xyx) - scenedist(pos - eps.xyx),
			scenedist(pos + eps.xxy) - scenedist(pos - eps.xxy)));
}

vec3 renderworld(vec2 uv){ 
	
	vec3 camPos = vec3(3.*sin(iMouse.x/iResolution.x*2.),-3.*cos(iMouse.x/iResolution.x*2.),2.);
	vec3 camTarg = vec3(0.);
	vec3 camUp = normalize(vec3(0.,0.,1.));
	
	// camera points to target from camera
	// remember that all directions must be normalised, or shit goes craycray
	vec3 camDir = normalize(camTarg - camPos);
	// right is perpendicular to up and forwards
	vec3 camRight = normalize(cross(camUp,camDir));
	// change UP to be relative to camera
	camUp = normalize(cross(camDir,camRight));
	
	// This pixel will cast ray in the camera direction, but a bit up/down and sidewise
	vec3 rayDir = normalize(camDir+uv.x*camRight+uv.y*camUp);
	
	// the first distance we will jump
	float dist = scenedist(camPos);
	// must maintain the total or we will not know where we hit when we do
	float total = dist;
	

	vec3 c2 = hsv(.63,1.,smoothstep(-1.5,2.5,uv.y)); 

	
	// now we march along the ray a lot
	for(int i = 0;i<MAXI;i++){
		dist = scenedist(camPos+rayDir*total); // distance to closest thing (safe jump distance)
		total += dist;						   // add it to our progress
		if(dist<EPS || dist>MAXD){continue;}   // quit if we hit something or are lost
	}
	vec3 dest = camPos+rayDir*total; // this is where we ended up
	vec3 c;							 // this will be our pixel colour
	if(dist<EPS){					 // if we score a hit
	
	float lum = dot(getNormal(dest), normalize(camPos));
	c = vec3(lum * 0.5, lum *0.2, lum*0.5) * sin( 100.* length(repeater(dest)))+vec3(pow(lum, 25.));         // we make colours
		
	}
	else
	{
		c = c2;// no hit, we are lost in the sky
	}
	
		
	return mix(c, c2, clamp(0.8-(dest.z+1.0)/2.0, 0.0, 1.0)); 
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = (fragCoord.xy / iResolution.xy - 0.5) * vec2(2.0,2.0/(iResolution.x/iResolution.y)); 
	
	vec3 c = renderworld(uv);
	
	fragColor = vec4(c, 1.0);
}