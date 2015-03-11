// Created by Ramon Viladomat
//( I used iq's smin for blending the ice when melting and fbm to generate normal maps ) 

// disable some of the options if the shader is running slow

//////////////////////////////////
// OPTIONS
#define USE_SMOOTH_MIN
//#define USE_SMOOTH_INNER_SHAPE
#define POOL_DEFORMATION
#define USE_NORMAL_DEFORMATION 
//#define PROCEDURAL_NOISE       

//////////////////////////////////
// CONFIG
#define STEP_REDUCTION 0.6
#define MARCHING_STEPS 180
#define NUM_REFLECTIONS 2
#define PI 3.1415
#define EPSILON 0.001
#define REFRACT_EPSILON 0.03
#define TIME_SCALE 1.0

#define sincos45 0.70710678118654

#define RAFRACTION_FACTOR 0.76
#define INV_RAFRACTION_FACTOR 0.90

#define INTRO_TIME 2.0
#define WAIT_TIME  1.5
#define MELT_TIME  20.0
#define EXIT_TIME  5.0 

float iceTransformFactor = 0.0;

float meltRadius      = 2.0;  
float meltSmooth      = 5.0;  
float meltDeformation = 0.05;  
float meltHeight      = 1.2;  

vec4 meltRandomPhase = vec4(0.0); 

// light compute 
vec3 lightColor = vec3(1.0,1.0,0.8);
vec3 lightPos = vec3(-3.0,4.0,4.0);
vec4 lightVec = vec4(1.0,0.0,0.0,6.0);

///////////
// NOISE //
///////////

//--------------------------------------------------------------------------
float Hash( float n )
{
	return fract(sin(n)*43758.5453);
}

//iq Noise
#ifdef PROCEDURAL_NOISE 

//--------------------------------------------------------------------------
float Noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    return mix(mix(mix( Hash(n+  0.0), Hash(n+  1.0),f.x),
                   mix( Hash(n+ 57.0), Hash(n+ 58.0),f.x),f.y),
               mix(mix( Hash(n+113.0), Hash(n+114.0),f.x),
                   mix( Hash(n+170.0), Hash(n+171.0),f.x),f.y),f.z);
}

#else 

//--------------------------------------------------------------------------
float Noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
	f = f*f*(3.0-2.0*f);
	
	vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;
	vec2 rg = texture2D( iChannel0, (uv+ 0.5)/256.0, -100.0 ).yx;
	return mix( rg.x, rg.y, f.z );
}

#endif

//--------------------------------------------------------------------------
float Fbm(in vec3 p)
{
	float f;
    f  = 0.5000*Noise( p ); p = p*2.02;
    f += 0.2500*Noise( p ); p = p*2.03;
    f += 0.1250*Noise( p ); p = p*2.01;
    f += 0.0625*Noise( p );
	return f*2.0- 1.0; 
}

//////////////////
// UPDATE WORLD //
//////////////////

//--------------------------------------------------------------------------
void UpdateWorld()
{	
	//time resources
	float currentTime = iGlobalTime*TIME_SCALE; 
	float totalTime = INTRO_TIME+WAIT_TIME+MELT_TIME+EXIT_TIME;
	float localTime = mod(currentTime,totalTime); 
	float meltCounter = floor(currentTime/totalTime);
		
	//melting
	float meltTimeFactor = clamp((localTime-(INTRO_TIME+WAIT_TIME))/MELT_TIME,0.0,1.0);
	
	meltRadius      = mix(0.2,4.2,meltTimeFactor); 
	meltSmooth      = mix(8.0,2.0,meltTimeFactor); 
	meltDeformation = mix(0.0,0.2,meltTimeFactor);
	meltHeight      = mix(0.0,3.0,meltTimeFactor);
	
	meltRandomPhase.x = Hash(meltCounter); 
	meltRandomPhase.y = Hash(meltCounter+0.734); 
	meltRandomPhase.z = Hash(meltCounter+1.384); 
	meltRandomPhase.w = Hash(meltCounter+7.821); 
	
	meltRandomPhase *= 2.0*PI; 
	
	//morphing
	float introTimeFactor = mix(1.0,min(localTime/INTRO_TIME,1.0),step(0.5,meltCounter));
	float exitTimeFactor = clamp((localTime-(INTRO_TIME+WAIT_TIME+MELT_TIME))/EXIT_TIME,0.0,1.0);
	
	iceTransformFactor = smoothstep(0.0,1.0,max(1.0 - introTimeFactor,exitTimeFactor));  
}

////////////////
// MORPHOLOGY //
////////////////

//--------------------------------------------------------------------------
// IQ exponential smooth min 
float SmoothMin( float a, float b, float k )
{
    float res = exp( -k*a ) + exp( -k*b );
    return -log( res )/k;
}

//--------------------------------------------------------------------------
vec2 MapIce( in vec3 p , in float mult, in float cryDist)
{ 
	vec3 q = p; 
	q.y += meltHeight;
	
	//basic cube shape
	vec3 iceDimensions = vec3(1.0,1.0,0.5); 
	vec3 d = abs(q) - iceDimensions;
	float dist = (min(max(d.x,max(d.y,d.z)),0.0)+length(max(d,0.0))); 

	//Ice basic deformation 
	dist += meltDeformation*(sin(q.x*2.0+meltRandomPhase.z) + cos(q.y*2.0+meltRandomPhase.w));

	//water pool shape
#ifdef POOL_DEFORMATION
	float angle = atan(p.z,p.x);
	float waterDistort = sin(angle*3.0+meltRandomPhase.x)+0.3*cos(7.0*angle+meltRandomPhase.y);
	float radius = meltRadius *(1.0+0.2*waterDistort);
#else
	float radius = meltRadius+0.1;
#endif
	
	float cyl = max(length(p.xz)-radius,abs(p.y + 1.0)-0.005);
		
	//blending them together
#ifdef USE_SMOOTH_MIN
	float finalDist = SmoothMin(cyl,dist,meltSmooth); 
	
#ifdef USE_SMOOTH_INNER_SHAPE
	finalDist = SmoothMin(finalDist,cryDist+0.5,2.3);
#endif
	
#else 
	float finalDist = min(cyl,dist);
#endif
		
	//make the ice spawn from the logo itself
	finalDist = mix(finalDist,cryDist,iceTransformFactor);
	
	return vec2(mult*finalDist,2.0);
}

//--------------------------------------------------------------------------
vec2 MapBg( in vec3 p)
{
	//background room shape
	vec3 q = p; 
	q.xz = abs(q.xz); 
	return vec2(min(min(p.y + 1.0,5.0 - p.y),min(5.0-q.z,5.0-q.x)),1.0);
}

//--------------------------------------------------------------------------
vec2 MapCry( in vec3 p )
{
	//rotate the logo
	p.xy = vec2(p.x+p.y,p.y-p.x)*sincos45;
	vec3 q = abs(p); 
	
	//distance Rounded Box
	vec4 dim = vec4(0.54,0.54,0.1,0.02);
	float boxDist = length(max(q - dim.xyz,vec3(0.0)))-dim.w;
	
	//distance to Minus Core 
	float sphereDist = length(q.xy + vec2(0.2)) - 0.81;
	return vec2(max(-sphereDist,boxDist),0.0);
}

//--------------------------------------------------------------------------
vec2 Map( in vec3 p, in float mult)
{
	vec2 ret = MapCry(p);
	vec2 bg = MapBg(p); 
	vec2 ice = MapIce(p,mult,ret.x);
	
	//cut the ice on the ground
	ice.x = max(ice.x,-bg.x);
		
	if (ret.x > bg.x) ret = bg; 
	if (ret.x > ice.x) ret = ice; 
	return ret;
}

///////////////
// MATERIALS //
///////////////

//--------------------------------------------------------------------------
vec4 BgMaterial(in vec3 pos, in vec3 normal)
{
	//tiled floor
	vec2 groundtiles = 2.0*(0.5 - abs(0.5-mod(pos.xz,vec2(1.0)))); 
	float groundtileBorder = smoothstep(0.0,0.1,min(groundtiles.x,groundtiles.y));
	vec4 groundColor = mix(vec4(0.15,0.15,0.15,0.02),vec4(0.2,0.2,0.2,0.05),groundtileBorder); 
	
	//Wall Line Light
	pos.y -= 4.0; 
	float lightPower = 2.0*pow(smoothstep(1.2,0.0,length(max(abs(pos.xy)-vec2(4.0,0.05),vec2(0.0)))),5.0); 
	lightPower *= step(EPSILON,-normal.z); 
 	vec4 wallLightColor = vec4(lightPower*lightColor,0.0); 
	
	return groundColor+wallLightColor;
}

//--------------------------------------------------------------------------
vec4 LogoMaterial( in vec3 pos )
{
	vec2 q = vec2(pos.x+pos.y,pos.y-pos.x)*sincos45;
	
	float factorX = step(0.0,q.x); 
	float factorY = step(0.0,q.y); 
	float minusFactorY = 1.0 - factorY; 
	
	vec4 color1 = factorY * vec4(0.2,0.0,0.0,0.2) + minusFactorY * vec4(0.0,0.2,0.0,0.2); 
	vec4 color2 = factorY * vec4(0.0,0.0,0.2,0.2) + minusFactorY * vec4(0.2,0.2,0.0,0.2); 
		
	return factorX * color1 + (1.0 - factorX) * color2; 	
}

//--------------------------------------------------------------------------
vec4 CalcColor( in vec3 pos, in vec3 nor, float material )
{
	vec4 materialColor = vec4(0.0);
		
	if (material < 0.5) materialColor = LogoMaterial( pos ); 
	else if (material < 1.5) materialColor = BgMaterial( pos, nor );
	else materialColor = vec4(0.0,0.1,0.2,0.4);  
		
	return materialColor;
}

//////////////////////
// MAIN RAY/SHADING //
//////////////////////

//--------------------------------------------------------------------------
vec2 Intersect( in vec3 ro, in vec3 rd, in float mult)
{
	vec2 res = vec2( 2.0*EPSILON, -1.0);
    float t = 0.0;
    for( int i=0; i<MARCHING_STEPS; i++ )
    {
		if( res.x<EPSILON ) continue;
		res = Map( ro+rd*t, mult);
		t += res.x*STEP_REDUCTION;
    }
	if( res.x>EPSILON ) res.y = -1.0;
	res.x = t;
    return res;
}

//--------------------------------------------------------------------------
vec3 CalcNormal( in vec3 pos, in float mult)
{
    vec2 eps = vec2(EPSILON,0.0);
	return normalize( 
		vec3( Map(pos+eps.xyy,mult).x - Map(pos-eps.xyy,mult).x, 
			  Map(pos+eps.yxy,mult).x - Map(pos-eps.yxy,mult).x, 
			  Map(pos+eps.yyx,mult).x - Map(pos-eps.yyx,mult).x) 
	);
}

//--------------------------------------------------------------------------
float SoftShadow( in vec3 ro, in vec3 rd, float maxDist, float mint, float k )
{
    float res = 1.0;
    float t = mint;
    for( int i=0; i<40; i++ )
    {
		if( t > maxDist ) continue; 
        float h = Map(ro + rd*t,1.0).x;
        res = min( res, k*h/t );
        t += h*STEP_REDUCTION;
    }
    return clamp(res,0.0,1.0);
}

//--------------------------------------------------------------------------
//IQ ray-marched ambient occlusion algorithm 
float AmbientOcclusion( in vec3 pos, in vec3 nor )
{
	float totao = 0.0;
    float sca = 1.0;
    for( int aoi=0; aoi<8; aoi++ )
    {
        float hr = 0.01 + 1.2*pow(float(aoi)/8.0,1.5);
        vec3 aopos =  nor * hr + pos;
        float dd = Map( aopos, 1.0 ).x;
        totao += -(dd-hr)*sca;
        sca *= 0.85;
    }
    return clamp( 1.0 - 0.6*totao, 0.0, 1.0 );
}

//--------------------------------------------------------------------------
vec3 GetLightDir(in vec3 position)
{
	//segment light
	float lambda = dot(position-lightPos,lightVec.xyz); 
	vec3 closestLightPos = clamp(lambda,0.0,lightVec.w)*lightVec.xyz+lightPos;
	
	return closestLightPos - position; 
}

//--------------------------------------------------------------------------
vec3 CalcNormalModification(in vec3 position, in vec3 normal, in float materialId)
{
#ifdef USE_NORMAL_DEFORMATION
	
	vec3 dirX = cross(normal,vec3(0.0,0.0,1.0));
	vec3 dirXalt = cross(normal, vec3(0.0,1.0,0.0)); 
	dirX = mix(dirXalt,dirX,step(0.5,length(dirX)));
	vec3 dirZ = cross(dirX,normal); 	
	
	float isIce = step(1.5,materialId); 
	float isDistortion = step(0.5,materialId);  
	
	position.y += meltHeight*isIce; 
	float dist1 = 0.1*Fbm(vec3(sin(3.0*Fbm(position*2.0)))); 
	float dist2 = 0.05*Fbm(position*3.0);
	float distort = mix(dist2,dist1,isIce);
	
	return isDistortion*(dirX + dirZ)*distort; 
#else 
	return vec3(0.0); 
#endif
}

//--------------------------------------------------------------------------
vec4 Shade(in vec3 position, in vec3 normal, in float materialId)
{
	if (materialId > -0.5)
	{
		// lights and materials 
		vec4 materialColor 	= CalcColor( position, normal, materialId );
		
		float ambient  		= 0.7 + 0.3*normal.y;
		vec3 ambientColor 	= ambient*materialColor.rgb;
				
		//light
		vec3 lightDir 	= normalize(GetLightDir(position));	
		float diffuse  = max(dot(normal,lightDir),0.0);
			
		vec3 diffuseColor = diffuse*lightColor*materialColor.rgb;
		return vec4(mix(ambientColor,diffuseColor,0.8),materialColor.w);
	}
	else
	{
		return vec4(0.0,0.1,0.15,0.0);
	}
}

//--------------------------------------------------------------------------
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	UpdateWorld();
	
    vec2 puv = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
    vec2 p = vec2(puv.x * iResolution.x/iResolution.y,puv.y);
	
	// Compute Camera	
	vec2 mousePos = -1.0 + 2.0 * iMouse.xy/iResolution.xy;	
	float camAngle = mousePos.x*PI;
	
    vec3 camPosition = 4.0 * vec3(sin(camAngle), 0.0, cos(camAngle));
	camPosition.y = 2.0+2.0*mousePos.y;
	
    vec3 camTarget	 = vec3( 0.0, 0.0, 0.0 );
    vec3 camFront 	 = normalize( camTarget - camPosition );
    vec3 camRight 	 = normalize( cross(camFront,vec3(0.0,1.0,0.0) ) );
    vec3 camUp 		 = normalize( cross(camRight,camFront));
    vec3 rayDir 	 = normalize( p.x*camRight + p.y*camUp + 2.0*camFront );
	
	// Start Ray
    vec3 finalcolor = vec3(0.0);
	float attenuation = 1.0;
	for( int reflectCount=0; reflectCount<NUM_REFLECTIONS; reflectCount++ )
	{		
		// Compute color for single ray
    	vec2 tmat = Intersect(camPosition,rayDir,1.0);			
		
		// results extraction
		vec3 position 	= camPosition + tmat.x*rayDir;
		vec3 normal 	= normalize(CalcNormal(position,1.0));
		normal += CalcNormalModification(position,normal,tmat.y);
		normal = normalize(normal);
		
		vec3 reflDir 	= reflect(rayDir,normal);
		
		vec4 ilumColor = Shade(position,normal, tmat.y);
		vec3 innerColor = vec3(0.0);
		
		if (tmat.y < -0.5)
		{
			finalcolor += attenuation*ilumColor.xyz; 
		}
		else
		{
			// refraction 		
			if (tmat.y > 1.5)
			{
				vec3 refrDir = refract(rayDir, normal, RAFRACTION_FACTOR);
				vec3 innerStartPos = position+REFRACT_EPSILON*refrDir; 
				vec2 refrMat = Intersect(innerStartPos,refrDir,-1.0); 
				
				vec3 innerPos = innerStartPos+refrMat.x*refrDir;
				vec3 innerNormal = normalize(CalcNormal(innerPos,-1.0));	
				
				if (refrMat.y < 0.5)
				{
					innerColor = Shade(innerPos,innerNormal,0.0).xyz;
				}
				else
				{
					//getting refracted out of ice again
					vec3 outerRefrDir = refract(refrDir, innerNormal, INV_RAFRACTION_FACTOR);
					vec3 outerStartPos = innerPos+REFRACT_EPSILON*outerRefrDir; 
					vec2 outerRefrMat = Intersect(outerStartPos,outerRefrDir,1.0); 
					
					vec3 outerPos = outerStartPos+outerRefrMat.x*outerRefrDir;
					vec3 outerNormal = normalize(CalcNormal(outerPos,1.0));
					
					innerColor = Shade(outerPos,outerNormal,outerRefrMat.y).xyz;
				}
				
				innerColor *= mix(vec3(0.0,0.5,1.0),vec3(1.0),smoothstep(1.0,0.0,refrMat.x));
			}
			
			//shadow and ambient occlusion
			vec3 lightDir = GetLightDir(position); 
			float lightDist = length(lightDir); 
			float shadow = SoftShadow( position, lightDir/lightDist, lightDist, 0.01, 10.0 );
			float ao = AmbientOcclusion(position,normal);	
						
			finalcolor += attenuation*(0.7+0.3*ao*shadow)*(ilumColor.xyz+innerColor);
			
			// prepare next ray for reflections 
			attenuation *= 2.0*ilumColor.w;
			rayDir = reflDir;
			camPosition = position + EPSILON*normal;
		}
	}
	
	// desaturation, gamma correction and simple vignette
	finalcolor = pow(mix( finalcolor, vec3(dot(finalcolor,vec3(0.33))), 0.3 ), vec3(0.45));
	finalcolor *= mix(1.0,0.0,smoothstep(0.7,2.0,length(puv)));
	
    fragColor = vec4( finalcolor,1.0 );
}