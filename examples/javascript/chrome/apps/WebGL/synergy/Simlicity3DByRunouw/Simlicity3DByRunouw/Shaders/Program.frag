// 3d flythrough of the fractal in https://www.shadertoy.com/view/lslGWr

const int MAX_RAY_STEPS = 64;

vec2 mouse = iMouse.xy / iResolution.xy - vec2(.5);

float field(in vec3 p) {
	float strength = 7. + .03;
	float accum = 0.;
	float prev = 0.;
	float tw = 0.;
	for (int i = 0; i < 10; ++i) {
		float mag = dot(p, p);
		p = abs(p) / mag + vec3(-0.5 * (mouse.x + 1.0), -0.4 * (mouse.y + 1.0), -1.5);
		float w = exp(-float(i) / 5.);
		accum += w * exp(-strength * pow(abs(mag - prev), 2.3));
		tw += w;
		prev = mag;
	}
	return max(0., accum / tw);
}

vec2 rotate2d(vec2 v, float a) {
	float sinA = sin(a);
	float cosA = cos(a);
	return vec2(v.x * cosA - v.y * sinA, v.y * cosA + v.x * sinA);	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 screenPos = (fragCoord.xy / iResolution.xy) * 2.0 - 1.0;
	vec3 cameraDir = vec3(0.0, 0.0, 1.4);
	vec3 cameraPlaneU = vec3(1.0, 0.0, 0.0);
	vec3 cameraPlaneV = vec3(0.0, 1.0, 0.0) * iResolution.y / iResolution.x;
	vec3 rayDir = cameraDir + screenPos.x * cameraPlaneU + screenPos.y * cameraPlaneV;
	vec3 rayPos = vec3(80.0, 12.0 * sin(iGlobalTime / 4.7), 0.0);
		
    
    rayDir.y -= .2 * sin(iGlobalTime / 4.7);
    rayDir = normalize(rayDir);
    
	rayPos.xz = rotate2d(rayPos.xz, iGlobalTime / 2.0);
	rayDir.xz = rotate2d(rayDir.xz, iGlobalTime / 2.0 + 3.14 / 2.0);
    
    
    float dis = 0.0;
    
    vec3 col = vec3(0);
    for(int i=0;i<MAX_RAY_STEPS;i++){
        
        
        float val = field(rayPos * .053);
        float t = max(5.0 * val - .9, 0.0);
        
        col += sqrt(dis) * .1 * vec3(0.5 * t * t * t, .6 * t * t, .7 * t);
        
        dis += 1.0 / float(MAX_RAY_STEPS);
        
        rayPos += rayDir * 1.0/ (val + .35);
    }
    
    vec2 q = screenPos;
    
    col = min(col, 1.0) - .34 * (log(col + 1.0));
    
    fragColor = vec4(sqrt(col.rgb), 1.0);
}