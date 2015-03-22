//////////////////////////////////////////////////////////////
// ShaderToy HLSL translation
//////////////////////////////////////////////////////////////

#define float4 vec4
#define float3 vec3
#define float2 vec2
#define const

float saturate(float color)
{
	return clamp(color, 0.0, 1.0);
}

float2 saturate(float2 color)
{
	return clamp(color, 0.0, 1.0);
}

float3 saturate(float3 color)
{
	return clamp(color, 0.0, 1.0);
}

float4 saturate(float4 color)
{
	return clamp(color, 0.0, 1.0);
}



//////////////////////////////////////////////////////////////
// Main
//////////////////////////////////////////////////////////////
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 glUV = fragCoord.xy / iResolution.xy;	
	float4 cvSplashData = float4(iResolution.x, iResolution.y, iGlobalTime, 0.0);	
	float2 InUV = glUV * 2.0 - 1.0;	
	
	//////////////////////////////////////////////////////////////
	// End of ShaderToy Input Compat
	//////////////////////////////////////////////////////////////
	
	// Constants
	const float TimeElapsed		= cvSplashData.z;
	const float Brightness		= sin(TimeElapsed) * 0.2;
	const float2 Resolution		= float2(cvSplashData.x, cvSplashData.y);
	const float AspectRatio		= Resolution.x / Resolution.y;
	const float3 InnerColor		= float3( 0.50, 0.50, 0.50 );
	const float3 OuterColor		= float3( 0.05, 0.05, 0.90 );
	const float3 WaveColor		= float3( 0.70, 0.70, 1.00 );
		
	// Input
	float2 uv				= (InUV + 1.0) / 2.0;

	// Output
	float4 outColor			= float4(0.0);

	// Positioning 
	float2 outerPos			= -0.5 + uv;
	outerPos.x				*= AspectRatio;

	float2 innerPos			= InUV * ( 2.0 - Brightness );
	innerPos.x				*= AspectRatio;

	// "logic" 
	float innerWidth		= length(outerPos);	
	float circleRadius		= 0.24 + Brightness * 0.1;
	float invCircleRadius 	= 1.0 / circleRadius;	
	float circleFade		= pow(length(2.0 * outerPos), 0.5);
	float invCircleFade		= 1.0 - circleFade;
	float circleIntensity	= pow(invCircleFade * max(1.1 - circleFade, 0.0), 2.0) * 60.0;
  	float circleWidth		= dot(innerPos,innerPos);
	float circleGlow		= ((1.0 - sqrt(abs(1.0 - circleWidth))) / circleWidth) + Brightness * 0.5;
	float outerGlow			= min( max( 1.0 - innerWidth * ( 1.0 - Brightness ), 0.0 ), 1.0 );
	float waveIntensity		= 0.0;
	
	// Inner circle logic
	if( innerWidth < circleRadius )
	{
		circleIntensity		*= pow(innerWidth * invCircleRadius, 24.0);
		
		float waveWidth		= 0.05;
		float2 waveUV		= InUV;

		for(float i = 0.0; i < 3.0; i++) 
		{		
			waveUV.y		+= 0.4 * (0.20 * cos((waveUV.x * 2.0) + (i / 7.0) + (TimeElapsed * 1.5)));
			waveWidth		= abs(1.0 / (200.0 * waveUV.y));
			waveIntensity 	+= waveWidth;
		}		
	}	
	
	// Compose outColor
	outColor.rgb	= outerGlow * OuterColor;	
	outColor.rgb	+= circleIntensity * InnerColor * 2.0;	
	outColor.rgb	+= circleGlow * InnerColor * (0.6 + Brightness * 1.2);
	outColor.rgb	+= WaveColor * waveIntensity;
	outColor.a		= 1.0;

	// Fade in
	outColor.rgb	= saturate(outColor.rgb);
	outColor.rgb	*= min(TimeElapsed / 2.0, 1.0);

	//////////////////////////////////////////////////////////////
	// Start of ShaderToy Output Compat
	//////////////////////////////////////////////////////////////

	fragColor = outColor;
}