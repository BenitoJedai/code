//Chaos Trend logo by Luther
//Modified from :
// Coffee & Tablet - @P_Malin

uniform sampler2D backbuffer;

#define PI 3.141592654
#define TWO_PI (2.0*PI)
#define HALF_PI (0.5*PI)
#define TQ_PI (1.5*PI)
//#define ENABLE_MONTE_CARLO
#define ENABLE_REFLECTIONS
#define ENABLE_FOG
#define ENABLE_SPECULAR
#define ENABLE_POINT_LIGHT
#define ENABLE_POINT_LIGHT_FLARE
//#define ENABLE_AMBIENT_OCCLUSION
#define ENABLE_LIGHTING
#define ENABLE_SHADOW
#define ENABLE_VIGNETTE
#define ENABLE_AMBIENT_LIGHT
#define ENABLE_APPLY_COLOUR_CORRECTION
//#define TEST_MATERIAL
 
 float GetShadow( const in vec3 vPos, const in vec3 vLightDir, const in float fLightDistance );
 
 
struct C_Ray
{
	vec3 vOrigin;
	vec3 vDir;
};

struct C_HitInfo
{
	vec3 vPos;
	float fDistance;
	vec3 vObjectId;
};

struct C_Material
{
	vec3 cAlbedo;
	float fR0;
	float fSmoothness;
	vec2 vParam;
};

vec3 RotateX( const in vec3 vPos, const in float fAngle )
{
	float s = sin(fAngle);
	float c = cos(fAngle);

	vec3 vResult = vec3( vPos.x, c * vPos.y + s * vPos.z, -s * vPos.y + c * vPos.z);

	return vResult;
}

vec3 RotateY( const in vec3 vPos, const in float fAngle )
{
	float s = sin(fAngle);
	float c = cos(fAngle);

	vec3 vResult = vec3( c * vPos.x + s * vPos.z, vPos.y, -s * vPos.x + c * vPos.z);

	return vResult;
}
      
vec3 RotateZ( const in vec3 vPos, const in float fAngle )
{
	float s = sin(fAngle);
	float c = cos(fAngle);

	vec3 vResult = vec3( c * vPos.x + s * vPos.y, -s * vPos.x + c * vPos.y, vPos.z);

	return vResult;
}

vec4 DistCombineUnion( const in vec4 v1, const in vec4 v2 )
{
	//if(v1.x < v2.x) return v1; else return v2;
	return mix(v1, v2, step(v2.x, v1.x));
}

float DistCombineUnion( const in float v1, const in float v2 )
{
	//if(v1.x < v2.x) return v1; else return v2;
	return mix(v1, v2, step(v2, v1));
}

vec4 DistCombineIntersect( const in vec4 v1, const in vec4 v2 )
{
	return mix(v2, v1, step(v2.x,v1.x));
}

float DistCombineIntersect( const in float v1, const in float v2 )
{
	return mix(v2, v1, step(v2,v1));
}

vec4 DistCombineSubtract( const in vec4 v1, const in vec4 v2 )
{
	return DistCombineIntersect(v1, vec4(-v2.x, v2.yzw));
}

float DistCombineSubtract( const in float v1, const in float v2 )
{
	return DistCombineIntersect(v1, -v2);
}


float GetDistanceCylinderZ(const in vec3 vPos, const in float r)
{
	return length(vPos.xy) - r;
}

float SelectSegment(const in vec3 vPos, const in float segcount)
{
	
	vec3 vNorm = normalize(vPos);
	float atn = (atan(vNorm.y, vNorm.x) + PI)/  TWO_PI;
	float segment = floor(atn * segcount);
	float half_segment = 0.5 / segcount;
	float seg_norm = mod((segment / segcount) + 0.25 + half_segment, 1.0);
	

	return seg_norm * TWO_PI;//turn it back in to rotation
}

vec4 GetDistanceCylinderMaterialSelectorZ(const in vec3 vPos, const in float r, const in float segcount)
{
	
	//+ PI
	vec3 vNorm = normalize(vPos);
	float atn = (atan(vNorm.y, vNorm.x) + PI)/  TWO_PI;
	//float segment = (segcount-1.0) - mod((floor(atn * (segcount - 0.5) ) + 2.0),segcount);
	float segment = floor(atn * segcount);

	float seg_norm = mod((segment / segcount) + 0.25, 1.0);
	float d = length(vPos.xy) - (r);// + segment * 0.1);
	return vec4(d,seg_norm , vNorm.x, vNorm.y);
}
 


float dChaosArrow( vec3 p)
{
  vec3 b = vec3(0.4,2.0,0.2);
  p.y +=0.2;
  
  /*float diffy = -0.3 - p.y;
  b.x += (step(0.3, p.y) * (1.3 - p.y) *0.5) +
  		  step(0.0, diffy)  * (diffy) * 0.5;*/
  if (p.y > 0.3)	  
	  b.x  *= (2.0 - p.y) *1.5;
  else if (p.y <-0.3)  
	  b.x *= 1.1+ (-0.3-p.y)  * 1.14;
  
  
  //p.y += 2.0;
 
  vec3 d = abs(p) - b;

  //return length(max(abs(p)-b,0.0))-0.1;

  
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d ,0.0));
}

vec4 GetDistanceChaosTrendLogo(const in vec3 vPos)
{
	//initialize with material '4' and x/y UV plane
	vec4 vResult = vec4(10000.0, 4, vPos.x, vPos.y);
	//work out which cylinder segment vPos is in	
	float r =  SelectSegment(vPos, 8.0);
	//and rotate the arrow domain accordingly
	vec3 vChaosArrowDomain = RotateZ(vPos, r );
	//shift outwards from centre
	vChaosArrowDomain.y -=3.5;		
	float arrowDist = dChaosArrow(vChaosArrowDomain);					
	vResult.x = DistCombineUnion(vResult.x, arrowDist);	
	//remove inner cylinder
	float cyldist = GetDistanceCylinderZ(vPos, 1.4);	
	vResult.x = DistCombineSubtract(vResult.x, cyldist);	
	return vResult;
}

vec4 GetDistanceScene(const in vec3 vPos)
{
	vec4 vResult = vec4(10000.0, 0.0, 0.0, 0.0);
	vec3 ct_domain = vPos;
	ct_domain.y -= 4.0;
	vec4 vDistChaosTrendLogo = GetDistanceChaosTrendLogo(ct_domain);
	vResult = DistCombineUnion(vResult, vDistChaosTrendLogo);
	
	vec4 vDistFloor = vec4(vPos.y + 1.0, 0.01, vPos.xz);
	vResult = DistCombineUnion(vResult, vDistFloor);
	
	#ifdef ENABLE_TEST_CYLINDER
	vec3 cyldomain = vPos;
	cyldomain.y -= 3.5;
	vec4 cyldist = GetDistanceCylinderMaterialSelectorZ(cyldomain, 0.5, 8.0);		
	vResult = DistCombineUnion(vResult, cyldist);	
	#endif
	return vResult;
}
 
C_Material GetObjectMaterial( const in vec3 vObjId, const in vec3 vPos )
{
	C_Material mat;
	
	/*mat.fR0 = 0.15;
	mat.fSmoothness = 0.1;
	mat.cAlbedo = vec3(1.0,1.0,1.0);   */
	
	
	if(vObjId.x < 0.5)
	{
		// floor
		vec4 cTextureSample = texture2D(iChannel0, vPos.xz * 0.01);                    
		mat.fR0 = 0.02;
		mat.fSmoothness = length(cTextureSample.rgb);
		mat.cAlbedo = cTextureSample.rgb * cTextureSample.rgb; // cheap gamma
	}
	else
	{
		mat.fR0 = 0.2;
		mat.fSmoothness = 0.3;
		mat.cAlbedo = vec3(0.01, 0.01, 0.01);                            
	}
	return mat;
}

 
vec3 GetSkyGradient( const in vec3 vDir )
{
	float fBlend = vDir.y * 0.5 + 0.5;
	return mix(vec3(0.0, 0.0, 0.0), vec3(0.4, 0.6, 1.0), fBlend);
}
 
vec3 GetLightPos()
{
	vec3 vLightPos = vec3(0.0, 12.0, 5.0);
	#ifdef ENABLE_MONTE_CARLO         
	vLightPos += gRandomNormal * 0.2;
	#endif
	return vLightPos;
}
 
vec3 GetLightCol()
{
	return vec3(482.0, 190.0, 200.0);
}

vec3 GetAmbientLight(const in vec3 vNormal)
{
	return GetSkyGradient(vNormal);
}
 

 
vec3 GetSceneNormal( const in vec3 vPos )
{
	// tetrahedron normal
	float fDelta = 0.025;

	vec3 vOffset1 = vec3( fDelta, -fDelta, -fDelta);
	vec3 vOffset2 = vec3(-fDelta, -fDelta,  fDelta);
	vec3 vOffset3 = vec3(-fDelta,  fDelta, -fDelta);
	vec3 vOffset4 = vec3( fDelta,  fDelta,  fDelta);

	float f1 = GetDistanceScene( vPos + vOffset1 ).x;
	float f2 = GetDistanceScene( vPos + vOffset2 ).x;
	float f3 = GetDistanceScene( vPos + vOffset3 ).x;
	float f4 = GetDistanceScene( vPos + vOffset4 ).x;

	vec3 vNormal = vOffset1 * f1 + vOffset2 * f2 + vOffset3 * f3 + vOffset4 * f4;

	return normalize( vNormal );
}
 
#define kRaymarchEpsilon 0.01
#define kRaymarchMatIter 32
#define kRaymarchStartDistance 0.1
 
// This is an excellent resource on ray marching -> http://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm
void Raymarch( const in C_Ray ray, out C_HitInfo result, const float fMaxDist, const int maxIter )
{          
	result.fDistance = kRaymarchStartDistance;
	result.vObjectId.x = 0.0;
				    
	for(int i=0;i<=kRaymarchMatIter;i++)                
	{
		result.vPos = ray.vOrigin + ray.vDir * result.fDistance;
		vec4 vSceneDist = GetDistanceScene( result.vPos );
		result.vObjectId = vSceneDist.yzw;
		
		// abs allows backward stepping - should only be necessary for non uniform distance functions
		if((abs(vSceneDist.x) <= kRaymarchEpsilon) || (result.fDistance >= fMaxDist) || (i > maxIter))
		{
			break;
		}                          	
		
		result.fDistance = result.fDistance + vSceneDist.x;      
	}
	
	
	if(result.fDistance >= fMaxDist)
	{
		result.vPos = ray.vOrigin + ray.vDir * result.fDistance;
		result.vObjectId.x = 0.0;
		result.fDistance = 1000.0;
	}
}
 
float GetShadow( const in vec3 vPos, const in vec3 vLightDir, const in float fLightDistance )
{
	C_Ray shadowRay;
	shadowRay.vDir = vLightDir;
	shadowRay.vOrigin = vPos;

	C_HitInfo shadowIntersect;
	Raymarch(shadowRay, shadowIntersect, fLightDistance,25);
													     
	return step(0.0, shadowIntersect.fDistance) * step(fLightDistance, shadowIntersect.fDistance );           
}

#define kFogDensity 0.035
#define kGlareBrightness 0.025
void ApplyAtmosphere(inout vec3 col, const in C_Ray ray, const in C_HitInfo intersection)
{

	vec3 vLightPos = GetLightPos();
	vec3 vToLight = vLightPos - intersection.vPos;
	vec3 vLightDir = normalize(vToLight);
	
	#ifdef ENABLE_FOG
	// fog
	float fFogAmount = exp(intersection.fDistance * -kFogDensity);
	vec3 cFog = GetSkyGradient(ray.vDir);
	col = mix(cFog, col, fFogAmount) ;
	#endif
	
	// glare from light (a bit hacky - use length of closest approach from ray to light)
	#ifdef ENABLE_POINT_LIGHT_FLARE

	float fDot = dot(vToLight, ray.vDir);
	fDot = clamp(fDot, 0.0, intersection.fDistance);
	
	vec3 vClosestPoint = ray.vOrigin + ray.vDir * fDot;
	float fDist = length(vClosestPoint - GetLightPos());
	col += GetLightCol() * kGlareBrightness/ (fDist * fDist);
	#endif      
}
 
// http://en.wikipedia.org/wiki/Schlick's_approximation
float Schlick( const in vec3 vNormal, const in vec3 vView, const in float fR0, const in float fSmoothFactor)
{
	float fDot = dot(vNormal, -vView);
	fDot = min(max((1.0 - fDot), 0.0), 1.0);
	float fDot2 = fDot * fDot;
	float fDot5 = fDot2 * fDot2 * fDot;
	return fR0 + (1.0 - fR0) * fDot5 * fSmoothFactor;
}
 
float GetDiffuseIntensity(const in vec3 vLightDir, const in vec3 vNormal)
{
	return max(0.0, dot(vLightDir, vNormal));
}
 
float GetBlinnPhongIntensity(const in C_Ray ray, const in C_Material mat, const in vec3 vLightDir, const in vec3 vNormal)
{            
	vec3 vHalf = normalize(vLightDir - ray.vDir);
	float fNdotH = max(0.0, dot(vHalf, vNormal));

	float fSpecPower = exp2(4.0 + 6.0 * mat.fSmoothness);
	float fSpecIntensity = (fSpecPower + 2.0) * 0.125;

	return pow(fNdotH, fSpecPower) * fSpecIntensity;
}
 
// use distance field to evaluate ambient occlusion
float GetAmbientOcclusion(const in C_Ray ray, const in C_HitInfo intersection, const in vec3 vNormal)
{
	vec3 vPos = intersection.vPos;
	
	float fAmbientOcclusion = 1.0;
	
	float fDist = 0.0;
	for(int i=0; i<=5; i++)
	{
		fDist += 0.1;
	
		vec4 vSceneDist = GetDistanceScene(vPos + vNormal * fDist);
	
		fAmbientOcclusion *= 1.0 - max(0.0, (fDist - vSceneDist.x) * 0.2 / fDist );                                    
	}
	
	return fAmbientOcclusion;
}

vec3 GetObjectLighting(const in C_Ray ray, const in C_HitInfo intersection, const in C_Material material, const in vec3 vNormal, const in vec3 cReflection)
{
	vec3 cScene ;
	vec3 vSpecularReflection = vec3(0.0);
	vec3 vDiffuseReflection = vec3(0.0);
	
	#ifdef ENABLE_AMBIENT_OCCLUSION
	float fAmbientOcclusion = GetAmbientOcclusion(ray, intersection, vNormal);	
	#else
	const float fAmbientOcclusion = 1.0;
	#endif
	#ifdef ENABLE_AMBIENT_LIGHT
	vec3 vAmbientLight = GetAmbientLight(vNormal) * fAmbientOcclusion;
	vDiffuseReflection += vAmbientLight;
	#endif
	
	vSpecularReflection += cReflection * fAmbientOcclusion;
		
	#ifdef ENABLE_POINT_LIGHT
	vec3 vLightPos = GetLightPos();
	vec3 vToLight = vLightPos - intersection.vPos;
	vec3 vLightDir = normalize(vToLight);
	float fLightDistance = length(vToLight);
	
	float fAttenuation = 1.0 / (fLightDistance * fLightDistance);
	#endif
	
	#ifdef ENABLE_SHADOW
	float fShadowBias = 0.1;              
	float fShadowFactor = GetShadow( intersection.vPos + vLightDir * fShadowBias, vLightDir, fLightDistance - fShadowBias );
	#else
	float fShadowFactor = 1.0;
	#endif
	#ifdef ENABLE_LIGHTING	
	vec3 vIncidentLight = GetLightCol() * fShadowFactor * fAttenuation;
	
	vDiffuseReflection += GetDiffuseIntensity( vLightDir, vNormal ) * vIncidentLight;                                                                                  
	vSpecularReflection += GetBlinnPhongIntensity( ray, material, vLightDir, vNormal ) * vIncidentLight;

	
	vDiffuseReflection *= material.cAlbedo;
	#else
	vDiffuseReflection = material.cAlbedo;// * fShadowFactor;
	#endif
	
	#ifdef ENABLE_SPECULAR
	float fFresnel = Schlick(vNormal, ray.vDir, material.fR0, material.fSmoothness * 0.9 + 0.1);
	cScene = mix(vDiffuseReflection , vSpecularReflection, fFresnel);
	#else
	cScene = vDiffuseReflection;
	#endif
	
	return cScene;
}
 
vec3 GetSceneColourSimple( const in C_Ray ray )
{
	C_HitInfo intersection;
	Raymarch(ray, intersection, 16.0, 32);
			     
	vec3 cScene;
       
	if(intersection.vObjectId.x < 0.5)
	{
		cScene = GetSkyGradient(ray.vDir);
	}
	else
	{
		C_Material material = GetObjectMaterial(intersection.vObjectId, intersection.vPos);
		vec3 vNormal = GetSceneNormal(intersection.vPos);
      
		// use sky gradient instead of reflection
		vec3 cReflection = GetSkyGradient(reflect(ray.vDir, vNormal));
      
		// apply lighting
		cScene = GetObjectLighting(ray, intersection, material, vNormal, cReflection );
	}
       
	ApplyAtmosphere(cScene, ray, intersection);
       
	return cScene;
}
 
vec3 GetSceneColour( const in C_Ray ray )
{                                                           
	C_HitInfo intersection;
	Raymarch(ray, intersection, 30.0, 256);
		     
	vec3 cScene;
	
	if(intersection.vObjectId.x < 0.001)
	{
		cScene = GetSkyGradient(ray.vDir);
	}
	else
	{
		C_Material material = GetObjectMaterial(intersection.vObjectId, intersection.vPos);
		
		vec3 vNormal = GetSceneNormal(intersection.vPos);
	
		vec3 cReflection;
		#ifdef ENABLE_REFLECTIONS	
		{
			// get colour from reflected ray
			float fSepration = 0.05;
			C_Ray reflectRay;
			reflectRay.vDir = reflect(ray.vDir, vNormal);
			reflectRay.vOrigin = intersection.vPos + reflectRay.vDir * fSepration;
									       
			cReflection = GetSceneColourSimple(reflectRay);                                                                          
		}
		#else
		cReflection = GetSkyGradient(reflect(ray.vDir, vNormal));                               
		#endif
		// apply lighting
		cScene = GetObjectLighting(ray, intersection, material, vNormal, cReflection );
	}
	
	ApplyAtmosphere(cScene, ray, intersection);
	
	return cScene;
}
 
void GetCameraRay( const in vec3 vPos, const in vec3 vForwards, const in vec3 vWorldUp, in vec2 fragCoord, out C_Ray ray)
{
	vec2 vPixelCoord = fragCoord.xy;
	#ifdef ENABLE_MONTE_CARLO
	vPixelCoord += gPixelRandom.zw;
	#endif
	vec2 vUV = ( vPixelCoord / iResolution.xy );
	vec2 vViewCoord = vUV * 2.0 - 1.0;

	vViewCoord *= 0.75;
	
	float fRatio = iResolution.x / iResolution.y;

	vViewCoord.y /= fRatio;                            

	ray.vOrigin = vPos;

	vec3 vRight = normalize(cross(vForwards, vWorldUp));
	vec3 vUp = cross(vRight, vForwards);
	     
	ray.vDir = normalize( vRight * vViewCoord.x + vUp * vViewCoord.y + vForwards);         
}
 
void GetCameraRayLookat( const in vec3 vPos, const in vec3 vInterest, in vec2 fragCoord, out C_Ray ray)
{
	vec3 vForwards = normalize(vInterest - vPos);
	vec3 vUp = vec3(0.0, 1.0, 0.0);

	GetCameraRay(vPos, vForwards, vUp, fragCoord, ray);
}
 
vec3 OrbitPoint( const in float fHeading, const in float fElevation )
{
	return vec3(sin(fHeading) * cos(fElevation), sin(fElevation), cos(fHeading) * cos(fElevation));
}
 
vec3 Tonemap( const in vec3 cCol )
{
	// simple Reinhard tonemapping operator      
	return cCol / (1.1 + cCol);
}
 
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	C_Ray ray;
	
	vec2 mouse = iMouse.xy / iResolution.xy;
	mouse.x += iGlobalTime * 0.01 ;
	
	vec3 vCameraPos = OrbitPoint(-mouse.x * 14.0 + PI, 
								  mouse.y * PI * 0.2 + PI * 0.025) * 17.0 - vec3(0.0, 0.9, 0.0);
	
	GetCameraRayLookat( vCameraPos, vec3(0.0, 5.0, 0.0), fragCoord, ray);
	//GetCameraRayLookat(vec3(0.0, 0.0, -5.0), vec3(0.0, 0.0, 0.0), ray);
	
	vec3 cScene = GetSceneColour( ray );	
	#ifdef ENABLE_APPLY_COLOUR_CORRECTION
	float fExposure = 2.5;
	cScene = cScene * fExposure;
	vec3 cCurr = Tonemap(cScene );
	#else
	vec3 cCurr = cScene;
	#endif

	vec3 cFinal = cCurr;

	
	float fAlpha = 1.0;
	#ifdef ENABLE_VIGNETTE
	vec2 npos = (fragCoord.xy / iResolution.xy) - vec2(0.5, 0.5);
	float vignette = 1.2 - (length(npos) * 0.7);
	cFinal.rgb *= vignette * vignette;
	#endif
	fragColor = vec4( cFinal, fAlpha );		
}
