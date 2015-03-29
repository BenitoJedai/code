void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec3 resolution = iResolution;
	float time = iGlobalTime;
	
	vec2 center = resolution.xy / 2.0;
	float radius = max(resolution.x, resolution.y) / 4.0;
	
	float indx =  fract(time ) * 500.0;
	
	float fluctuationX0 = 0.0;
	float fluctuationY0 = 0.0;
	float fluctuationX1 = 0.0;
	float fluctuationY1 = 0.0;
	
		
	fluctuationX0 =  100.0 * sin(time * 2.0 + time / 2.0);
	fluctuationY0 =  100.0 * sin(time * 2.0 - time / 2.5);
	fluctuationX1 =  100.0 * cos(time * 2.0 + time / 1.5);
	fluctuationY1 =  100.0 * cos(time * 2.0 - time / 3.0);

	vec2 waveCenter[5];
	waveCenter[0] = vec2(center.x + (resolution.x / 3.0) + fluctuationX0,
					center.y + fluctuationY0);
	waveCenter[1] = vec2(center.x - (resolution.x / 3.0) + fluctuationY0,
					center.y + fluctuationX0);
	waveCenter[2] = vec2(center.x + fluctuationX1,
					center.y + (resolution.y / 3.0)  + fluctuationY1);
	waveCenter[3] = vec2(center.x + fluctuationY1,
					center.y - (resolution.y / 3.0)   + fluctuationX1);
	waveCenter[4] = vec2(center.x + fluctuationX0 + fluctuationX1,
					center.y + fluctuationX1 + fluctuationY1);
	float amplitude[5];
	amplitude[0] = 5.0;
	amplitude[1] = 8.0;
	amplitude[2] = 7.0;
	amplitude[3] = 3.0;
	amplitude[4] = 10.0;
	
	float dst = 0.0;
	float partialClr[5];
	
	for (int i = 0; i < 5; i++) {
		dst = distance(waveCenter[i], fragCoord.xy);
		partialClr[i] = amplitude[i] / dst * sin(dst / 1.0 - time * 10.0);
	}
	
	float mainColor = partialClr[0] + partialClr[1] + partialClr[2] + 
					partialClr[3] + partialClr[4];
	
    fragColor = vec4(vec3(clamp(mainColor, 0.0, 1.0)), 1.0);
}