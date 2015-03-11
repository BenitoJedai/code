// can we drag drop into live app?
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    
	fragColor = vec4(
		uv,
		0.5 + 0.5 * sin(iGlobalTime),
		// 0.5

// first program will want to paint all bytes
		1.0
		// fragCoord.x / iResolution.x
	);
}