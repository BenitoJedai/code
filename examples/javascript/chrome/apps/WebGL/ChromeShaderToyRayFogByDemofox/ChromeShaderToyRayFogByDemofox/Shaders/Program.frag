// TODO: try different amplitudes and frequencies


/*
  Written by Alan Wolfe
  http://demofox.org/
  http://blog.demofox.org/
*/

//=======================================================================================

#define FLT_MAX 3.402823466e+38

//=======================================================================================
struct SMaterial
{
	vec3 m_diffuseColor;
	float m_specular;
	vec3 m_specularColor;
};

//=======================================================================================
struct SCollisionInfo
{
	int			m_Id;
	bool		m_foundHit;
	bool 		m_fromInside;
	float 		m_collisionTime;
	vec3		m_intersectionPoint;
	vec3		m_surfaceNormal;
	SMaterial 	m_material;
};

//=======================================================================================
struct SSphere
{
	int			m_Id;
	vec3   		m_center;
	float  		m_radius;
	SMaterial	m_material;
};
	
//=======================================================================================
struct SAxisAlignedBox
{
	int			m_Id;
	vec3		m_pos;
	vec3		m_scale;
	SMaterial	m_material;
};
	
//=======================================================================================
struct SPointLight
{
	vec3		m_pos;
	vec3		m_color;
};
	
//=======================================================================================
struct SDirLight
{
	vec3		m_reverseDir;
	vec3		m_color;
};
	
//=======================================================================================
struct SShadingStackItem
{
	vec3		m_addColor;
	vec4		m_fogColorAndAmount;
};
	
//=======================================================================================
// Scene parameters
//=======================================================================================
	
//----- settings
#define DO_SHADOWS true // setting this to false will speed up rendering
	
//----- camera
vec2 mouse = iMouse.xy / iResolution.xy;

vec3 getCameraPos ()
{
	return iMouse.z > 0.0 ? vec3(mouse.x * -8.0 + 4.0, 1.0, mouse.y*8.0 - 8.0) : vec3(0,1.0,-4.0);
}

vec3 getCameraAt ()
{
	vec3 pos = getCameraPos();
	
	return pos + vec3(0.0,-1.0,4.0);
}

vec3 cameraPos	= getCameraPos();
vec3 cameraAt 	= getCameraAt();

vec3 cameraFwd  = normalize(cameraAt - cameraPos);
vec3 cameraLeft  = normalize(vec3(-1.0,0.0,0.0));
vec3 cameraUp   = normalize(cross(cameraLeft, cameraFwd));

float cameraViewWidth	= 6.0;
float cameraViewHeight	= cameraViewWidth * iResolution.y / iResolution.x;
float cameraDistance	= 6.0;  // intuitively backwards!

//----- shading stack
SShadingStackItem shadingStack;

//----- lights
vec3 lightAmbient				= vec3(0.1,0.1,0.1);

vec3 fogColor = vec3(0.5,0.5,0.5);

SDirLight lightDir1 =
	SDirLight
	(
		normalize(vec3(-1.0,1.0,-1.0)),
		vec3(1.0,1.0,1.0)
	);

SPointLight lightPoint1 =
	SPointLight
	(
		vec3(sin(1.57 + iGlobalTime*1.3),0.3,cos(1.57 + iGlobalTime*1.3)),
		vec3(0.7,0.3,0.7)
	);

//----- primitives
SSphere lightPoint1Sphere =
	SSphere
	(
		1,						//id
		lightPoint1.m_pos,		//center
		0.06,					//radius
		SMaterial
		(
			lightPoint1.m_color,//diffuse color
			1.0,				//specular amount
			vec3(0.0,0.0,0.0)	//specular color
		)
	);	

SSphere sphere1 =
	SSphere
	(
		2,						//id
		vec3(0.0,0.0,0.0),		//center
		0.2,					//radius
		SMaterial
		(
			vec3(0.0,1.0,0.0),	//diffuse color
			10.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

SSphere sphere2 =
	SSphere
	(
		3,						//id
		vec3(sin(iGlobalTime*1.3),sin(3.14 + iGlobalTime * 1.4)*0.25,cos(iGlobalTime*1.3)),	//center
		0.15,					//radius
		SMaterial
		(
			vec3(0.0,1.0,1.0),	//diffuse color
			3.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

SAxisAlignedBox orbitBox = 
	SAxisAlignedBox
	(
		4,						//Id
		vec3(sin(2.0 + iGlobalTime*1.3),-0.2,cos(2.0 + iGlobalTime*1.3)),	//center
		vec3(0.5,0.5,0.5),	//scale
		SMaterial
		(
			vec3(1.0,0.0,0.0),	//diffuse color
			20.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

SAxisAlignedBox floorBox = 
	SAxisAlignedBox
	(
		5,						//Id
		vec3(0.0,-1.6,0.0),		//center
		vec3(10.0,0.1,10.0),	//scale
		SMaterial
		(
			vec3(1.0,1.0,1.0),	//diffuse color
			20.0,				//specular amount
			vec3(0.0,0.0,0.0)	//specular color
		)
	);

SAxisAlignedBox backBox1 = 
	SAxisAlignedBox
	(
		6,						//Id
		vec3(0.0,0.0,3.0),		//center
		vec3(10.0,3.0,0.1),		//scale
		SMaterial
		(
			vec3(0.0,0.0,0.8),  //diffuse color
			20.0,				//specular amount
			vec3(0.0,0.0,0.0)	//specular color
		)
	);

SSphere floorSphere1 =
	SSphere
	(
		7,						//id
		vec3(1.5, -1.0, 0.0),	//center
		0.2,					//radius
		SMaterial
		(
			vec3(0.8,0.8,0.0),	//diffuse color
			3.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

SSphere floorSphere2 =
	SSphere
	(
		8,						//id
		vec3(1.5, -1.0, 1.0),	//center
		0.2,					//radius
		SMaterial
		(
			vec3(0.8,0.8,0.0),	//diffuse color
			3.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

SSphere floorSphere3 =
	SSphere
	(
		9,						//id
		vec3(1.5, -1.0, 2.0),	//center
		0.2,					//radius
		SMaterial
		(
			vec3(0.8,0.8,0.0),	//diffuse color
			3.0,				//specular amount
			vec3(1.0,1.0,1.0)	//specular color
		)
	);

//----- macro lists

// sphere primitive list
#define SPHEREPRIMLIST PRIM(sphere1) PRIM(sphere2) PRIM(floorSphere1) PRIM(floorSphere2) PRIM(floorSphere3)

// sphere primitive list with light primitives
#define SPHEREPRIMLISTWITHLIGHTS SPHEREPRIMLIST PRIM(lightPoint1Sphere)

// box primitive list
#define BOXPRIMLIST PRIM(orbitBox) PRIM(floorBox) PRIM(backBox1)

// point light list
#define POINTLIGHTLIST LIGHT(lightPoint1)

// directional light list
#define DIRLIGHTLIST LIGHT(lightDir1)

//=======================================================================================
bool RayIntersectSphere (inout SSphere sphere, inout SCollisionInfo info, in vec3 rayPos, in vec3 rayDir, in int ignorePrimitiveId)
{
	if (ignorePrimitiveId == sphere.m_Id)
		return false;

	//get the vector from the center of this circle to where the ray begins.
	vec3 m = rayPos - sphere.m_center;

    //get the dot product of the above vector and the ray's vector
	float b = dot(m, rayDir);

	float c = dot(m, m) - sphere.m_radius * sphere.m_radius;

	//exit if r's origin outside s (c > 0) and r pointing away from s (b > 0)
	if(c > 0.0 && b > 0.0)
		return false;

	//calculate discriminant
	float discr = b * b - c;


	//a negative discriminant corresponds to ray missing sphere
	if(discr < 0.0)
		return false;

	//not inside til proven otherwise
	bool fromInside = false;

	//ray now found to intersect sphere, compute smallest t value of intersection
	float collisionTime = -b - sqrt(discr);

	//if t is negative, ray started inside sphere so clamp t to zero and remember that we hit from the inside
	if(collisionTime < 0.0)
	{
		collisionTime = -b + sqrt(discr);
		fromInside = true;
	}

	//enforce a max distance if we should
	if(info.m_collisionTime >= 0.0 && collisionTime > info.m_collisionTime)
		return false;

	// set all the info params since we are garaunteed a hit at this point
	info.m_fromInside = fromInside;
	info.m_collisionTime = collisionTime;
	info.m_material = sphere.m_material;

	//compute the point of intersection
	info.m_intersectionPoint = rayPos + rayDir * info.m_collisionTime;

	// calculate the normal
	info.m_surfaceNormal = info.m_intersectionPoint - sphere.m_center;
	info.m_surfaceNormal = normalize(info.m_surfaceNormal);

	// we found a hit!
	info.m_foundHit = true;
	info.m_Id = sphere.m_Id;
	return true;
}

//=======================================================================================
bool RayIntersectAABox (inout SAxisAlignedBox box, inout SCollisionInfo info, in vec3 rayPos, in vec3 rayDir, in int ignorePrimitiveId)
{
	if (ignorePrimitiveId == box.m_Id)
		return false;
	
	float rayMinTime = 0.0;
	float rayMaxTime = FLT_MAX;
	
	//enforce a max distance
	if(info.m_collisionTime >= 0.0)
	{
		rayMaxTime = info.m_collisionTime;
	}	
	
	// find the intersection of the intersection times of each axis to see if / where the
	// ray hits.
	for(int axis = 0; axis < 3; ++axis)
	{
		//calculate the min and max of the box on this axis
		float axisMin = box.m_pos[axis] - box.m_scale[axis] * 0.5;
		float axisMax = axisMin + box.m_scale[axis];

		//if the ray is paralel with this axis
		if(abs(rayDir[axis]) < 0.0001)
		{
			//if the ray isn't in the box, bail out we know there's no intersection
			if(rayPos[axis] < axisMin || rayPos[axis] > axisMax)
				return false;
		}
		else
		{
			//figure out the intersection times of the ray with the 2 values of this axis
			float axisMinTime = (axisMin - rayPos[axis]) / rayDir[axis];
			float axisMaxTime = (axisMax - rayPos[axis]) / rayDir[axis];

			//make sure min < max
			if(axisMinTime > axisMaxTime)
			{
				float temp = axisMinTime;
				axisMinTime = axisMaxTime;
				axisMaxTime = temp;
			}

			//union this time slice with our running total time slice
			if(axisMinTime > rayMinTime)
				rayMinTime = axisMinTime;

			if(axisMaxTime < rayMaxTime)
				rayMaxTime = axisMaxTime;

			//if our time slice shrinks to below zero of a time window, we don't intersect
			if(rayMinTime > rayMaxTime)
				return false;
		}
	}
	
	//if we got here, we do intersect, return our collision info
	info.m_fromInside = (rayMinTime == 0.0);
	if(info.m_fromInside)
		info.m_collisionTime = rayMaxTime;
	else
		info.m_collisionTime = rayMinTime;
	info.m_material = box.m_material;
	
	info.m_intersectionPoint = rayPos + rayDir * info.m_collisionTime;

	// figure out the surface normal by figuring out which axis we are closest to
	float closestDist = FLT_MAX;
	for(int axis = 0; axis < 3; ++axis)
	{
		float distFromPos= abs(box.m_pos[axis] - info.m_intersectionPoint[axis]);
		float distFromEdge = abs(distFromPos - (box.m_scale[axis] * 0.5));

		if(distFromEdge < closestDist)
		{
			closestDist = distFromEdge;
			info.m_surfaceNormal = vec3(0.0,0.0,0.0);
			if(info.m_intersectionPoint[axis] < box.m_pos[axis])
				info.m_surfaceNormal[axis] = -1.0;
			else
				info.m_surfaceNormal[axis] =  1.0;
		}
	}

	// we found a hit!
	info.m_foundHit = true;
	info.m_Id = box.m_Id;
	return true;	
}

//=======================================================================================
bool PointCanSeePoint(in vec3 startPos, in vec3 targetPos, in int ignorePrimitiveId)
{
	// see if we can hit the target point from the starting point
	SCollisionInfo collisionInfo =
		SCollisionInfo
		(
			0,
			false,
			false,
			-1.0,
			vec3(0.0,0.0,0.0),
			vec3(0.0,0.0,0.0),
			SMaterial(
				vec3(0.0,0.0,0.0),
				1.0,
				vec3(0.0,0.0,0.0)
			)
		);	
	
	vec3 rayDir = targetPos - startPos;
	collisionInfo.m_collisionTime = length(rayDir);
	rayDir = normalize(rayDir);

	// run intersection against all non light primitives. return false on first hit found
	return true
	#define PRIM(x) && !RayIntersectSphere(x, collisionInfo, startPos, rayDir, ignorePrimitiveId)
	SPHEREPRIMLIST
	#undef PRIM
	#define PRIM(x) && !RayIntersectAABox(x, collisionInfo, startPos, rayDir, ignorePrimitiveId)
	BOXPRIMLIST
	#undef PRIM
	;
}

//=======================================================================================
void ApplyPointLight (inout vec3 pixelColor, in SCollisionInfo collisionInfo, in SPointLight light, in vec3 rayDir)
{
	if (DO_SHADOWS == false || PointCanSeePoint(collisionInfo.m_intersectionPoint, light.m_pos, collisionInfo.m_Id))
	{
		// diffuse
		vec3 hitToLight = normalize(light.m_pos - collisionInfo.m_intersectionPoint);
		float dp = dot(collisionInfo.m_surfaceNormal, hitToLight);
		if(dp > 0.0)
			pixelColor += collisionInfo.m_material.m_diffuseColor * dp * light.m_color;
				
		// specular
		vec3 reflection = reflect(hitToLight, collisionInfo.m_surfaceNormal);
		dp = dot(rayDir, reflection);
		if (dp > 0.0)
			pixelColor += collisionInfo.m_material.m_specularColor * pow(dp, collisionInfo.m_material.m_specular) * light.m_color;
	}
}

//=======================================================================================
void ApplyDirLight (inout vec3 pixelColor, in SCollisionInfo collisionInfo, in SDirLight light, in vec3 rayDir)
{
	if (DO_SHADOWS == false || PointCanSeePoint(collisionInfo.m_intersectionPoint, collisionInfo.m_intersectionPoint + light.m_reverseDir * 1000.0, collisionInfo.m_Id))
	{
		// diffuse
		float dp = dot(collisionInfo.m_surfaceNormal, light.m_reverseDir);
		if(dp > 0.0)
			pixelColor += collisionInfo.m_material.m_diffuseColor * dp * light.m_color;
		
		// specular
		vec3 reflection = reflect(light.m_reverseDir, collisionInfo.m_surfaceNormal);
		dp = dot(rayDir, reflection);
		if (dp > 0.0)
			pixelColor += collisionInfo.m_material.m_specularColor * pow(dp, collisionInfo.m_material.m_specular) * light.m_color;			
	}
}

//=======================================================================================
float DefiniteIntegral (in float x, in float amplitude, in float frequency, in float motionFactor)
{
	// Fog density on an axis:
	// (1 + sin(x*F)) * A
	//
	// indefinite integral:
	// (x - cos(F * x)/F) * A
	//
	// ... plus a constant (but when subtracting, the constant disappears)
	//
	x += iGlobalTime * motionFactor;
	return (x - cos(frequency * x)/ frequency) * amplitude;
}

//=======================================================================================
float AreaUnderCurveUnitLength (in float a, in float b, in float amplitude, in float frequency, in float motionFactor)
{
	// we calculate the definite integral at a and b and get the area under the curve
	// but we are only doing it on one axis, so the "width" of our area bounding shape is
	// not correct.  So, we divide it by the length from a to b so that the area is as
	// if the length is 1 (normalized... also this has the effect of making sure it's positive
	// so it works from left OR right viewing).  The caller can then multiply the shape
	// by the actual length of the ray in the fog to "stretch" it across the ray like it
	// really is.
	return (DefiniteIntegral(a, amplitude, frequency, motionFactor) - DefiniteIntegral(b, amplitude, frequency, motionFactor)) / (a - b);
}

//=======================================================================================
float FogAmount (in vec3 src, in vec3 dest, in float fogMod)
{
	float len = length(dest - src);
	
	// calculate base fog amount (constant density over distance)	
	float amount = len * 0.1;
	
	// calculate definite integrals across axes to get moving fog adjustments
	float adjust = 0.0;
	adjust += AreaUnderCurveUnitLength(dest.x, src.x, 0.01, 0.6, 2.0);
	adjust += AreaUnderCurveUnitLength(dest.y, src.y, 0.01, 1.2, 1.4);
	adjust += AreaUnderCurveUnitLength(dest.z, src.z, 0.01, 0.9, 2.2);
	adjust *= len;
	
	return min(amount+adjust+fogMod, 1.0);
}

//=======================================================================================
void TraceRay (in vec3 rayPos, in vec3 rayDir, in float fogMod)
{
	int lastHitPrimitiveId = 0;
	
	vec3 rayToCameraDir = rayDir;
	
	vec3 pixelColor = vec3(0.0,0.0,0.0);	
	SCollisionInfo collisionInfo =
		SCollisionInfo
		(
			0,
			false,
			false,
			-1.0,
			vec3(0.0,0.0,0.0),
			vec3(0.0,0.0,0.0),
			SMaterial(
				vec3(0.0,0.0,0.0),
				1.0,
				vec3(0.0,0.0,0.0)
			)
		);

	// run intersection against all objects, including light objects		
	#define PRIM(x) RayIntersectSphere(x, collisionInfo, rayPos, rayDir, lastHitPrimitiveId);
	SPHEREPRIMLISTWITHLIGHTS
	#undef PRIM
			
	// run intersections against all boxes
	#define PRIM(x) RayIntersectAABox(x, collisionInfo, rayPos, rayDir, lastHitPrimitiveId);
	BOXPRIMLIST
	#undef PRIM

	if (collisionInfo.m_foundHit)
	{	
			
		// do texture sampling for the floorbox
		if (collisionInfo.m_Id == floorBox.m_Id)
		{
			collisionInfo.m_material.m_diffuseColor = 
			texture2D(iChannel0, collisionInfo.m_intersectionPoint.xz * 0.25).xyz;
		}
			
		// point lights
		#define LIGHT(light) ApplyPointLight(pixelColor, collisionInfo, light, rayDir);
		POINTLIGHTLIST
		#undef LIGHT
				
		// directional lights
		#define LIGHT(light) ApplyDirLight(pixelColor, collisionInfo, light, rayDir);
		DIRLIGHTLIST				
		#undef LIGHT

		// ambient light
		pixelColor += lightAmbient * collisionInfo.m_material.m_diffuseColor;
					
		vec4 fog = vec4(fogColor, FogAmount(rayPos, collisionInfo.m_intersectionPoint, fogMod));

		shadingStack.m_addColor = pixelColor;
		shadingStack.m_fogColorAndAmount = fog;
	}
	// no hit means all fog
	else
		shadingStack.m_fogColorAndAmount = vec4(fogColor,1.0);
}

//=======================================================================================
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 rawPercent = (fragCoord.xy / iResolution.xy);
	vec2 percent = rawPercent - vec2(0.5,0.5);
	
	vec3 rayPos;
	vec3 rayTarget;
	
	rayTarget = (cameraFwd * cameraDistance)
			  + (cameraLeft * percent.x * cameraViewWidth)
	          + (cameraUp * percent.y * cameraViewHeight);
		
	rayPos = cameraPos;
	
	vec3 rayDir = normalize(rayTarget);

	float fogMod = mod( fragCoord.x + fragCoord.y, 2.0 ) / 255.0;
	
	TraceRay(rayPos, rayDir, fogMod);
	
	vec3 pixelColor = mix(shadingStack.m_addColor, shadingStack.m_fogColorAndAmount.xyz, shadingStack.m_fogColorAndAmount.w);
	
	fragColor = vec4(pixelColor, 1.0);
}