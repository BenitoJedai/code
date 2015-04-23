///////////////////
// Shadertoy (1) //
///////////////////

// Precision
#ifdef GL_ES
precision highp float;
#endif

// Rendering parameters
// Note: set IPD to 0.0 for classic (non-VR) rendering
#define QUALITY_HIGH

// Uniform variables
vec3 resolution = iResolution;
float time = iGlobalTime;
vec3 headPosition;
mat3 headRotate;
vec3 lightPosition;
float edgePower;

///////////
// Unity //
///////////

// VR parameters
#define FOV		(96.0 * PI / 180.0)
#define IPD		0.03
#define EYES_Y	0.06
#define EYES_Z	0.03

// Rendering parameters
#define RAY_STEP_MAX		20.0
#define RAY_LENGTH_MAX		10.0
#define EDGE_LENGTH			0.1
#ifdef QUALITY_HIGH
	#define EDGE_FULL
	#define SHADOW
#endif
#define BUMP_RESOLUTION		200.0
#define BUMP_INTENSITY		0.3
#define AMBIENT_BLOCK		0.2
#define AMBIENT_EDGE		2.5
#define SPECULAR_POWER		2.0
#define SPECULAR_INTENSITY	0.3
#define FADE_POWER			1.5
#define GAMMA				0.8

// Math constants
#define DELTA	0.002
#define PI		3.14159265359

// PRNG (unpredictable)
float randUnpredictable (in vec3 seed) {
	seed = fract (seed * vec3 (5.3983, 5.4427, 6.9371));
	seed += dot (seed.yzx, seed.xyz + vec3 (21.5351, 14.3137, 15.3219));
	return fract (seed.x * seed.y * seed.z * 95.4337);
}

// PRNG (predictable)
float randPredictable (in vec3 seed) {
	return fract (11.0 * sin (3.0 * seed.x + 5.0 * seed.y + 7.0 * seed.z));
}

// Check whether there is a block at a given voxel edge
float block (in vec3 p, in vec3 n) {
	vec3 block = floor (p + 0.5 + n * 0.5);
	vec3 blockEven = mod (block, 2.0);
	float blockSum = blockEven.x + blockEven.y + blockEven.z;
	return max (step (blockSum, 1.5), step (blockSum, 2.5) * step (0.5, randPredictable (block))) *
		step (4.5, mod (block.x, 32.0)) *
		step (2.5, mod (block.y, 16.0)) *
		step (4.5, mod (block.z, 32.0));
}

// Cast a ray
vec3 hit (in vec3 p, in vec3 ray, in float rayLengthMax, out float rayLength, out vec3 n) {

	// Launch the ray
	vec3 q = p;
	vec3 raySign = sign (ray);
	vec3 rayInv = 1.0 / ray;
	vec3 rayLengthNext = (0.5 * raySign - fract (p + 0.5) + 0.5) * rayInv;
	for (float rayStep = 0.0; rayStep < RAY_STEP_MAX; ++rayStep) {

		// Reach the edge of the voxel
		rayLength = min (rayLengthNext.x, min (rayLengthNext.y, rayLengthNext.z));
		n = step (rayLengthNext.xyz, rayLengthNext.yzx) * step (rayLengthNext.xyz, rayLengthNext.zxy) * raySign;
		q = p + rayLength * ray;

		// Check whether we hit a block
		if (block (q, n) > 0.5 || rayLength > rayLengthMax) {
			break;
		}

		// Next voxel
		rayLengthNext += n * rayInv;
	}

	// Return the hit point
	return q;
}

// HSV to RGB
vec3 rgb (in vec3 hsv) {
	return hsv.z * (1.0 + hsv.y * clamp (abs (fract (hsv.xxx + vec3 (0.0, 2.0 / 3.0, 1.0 / 3.0)) * 6.0 - 3.0) - 2.0, -1.0, 0.0));
}

// Main function
void _main (out vec4 _gl_FragColor, in vec2 _gl_FragCoord) {

	// Define the ray corresponding to this fragment
	float rayStereo = 0.5 * sign (_gl_FragCoord.x - resolution.x * 0.5) * sign (IPD);
	vec3 rayOrigin = headPosition + headRotate * vec3 (rayStereo * IPD, EYES_Y, EYES_Z);
	vec3 rayDirection = headRotate * normalize (vec3 ((2.0 * _gl_FragCoord.x - (1.0 + rayStereo) * resolution.x), 2.0 * _gl_FragCoord.y - resolution.y, 0.5 * resolution.x / tan (0.5 * FOV)));

	// Cast a ray
	float hitDistance;
	vec3 hitNormal;
	vec3 hitPosition = hit (rayOrigin, rayDirection, RAY_LENGTH_MAX, hitDistance, hitNormal);
	vec3 hitUV = hitPosition * abs (hitNormal.yzx + hitNormal.zxy);

	// Basic edge detection
	vec3 edgeDistance = fract (hitUV + 0.5) - 0.5;
	vec3 edgeDirection = sign (edgeDistance);
	edgeDistance = abs (edgeDistance);

	#ifdef EDGE_FULL
	vec3 hitNormalAbs = abs (hitNormal);
	vec2 edgeSmooth = vec2 (dot (edgeDistance, hitNormalAbs.yzx), dot (edgeDistance, hitNormalAbs.zxy));
	float edgeIntensity = (1.0 - block (hitPosition + edgeDirection * hitNormalAbs.yzx, hitNormal)) * smoothstep (0.5 - EDGE_LENGTH, 0.5 - EDGE_LENGTH * 0.5, edgeSmooth.x);
	edgeIntensity = max (edgeIntensity, (1.0 - block (hitPosition + edgeDirection * hitNormalAbs.zxy, hitNormal)) * smoothstep (0.5 - EDGE_LENGTH, 0.5 - EDGE_LENGTH * 0.5, edgeSmooth.y));
	edgeIntensity = max (edgeIntensity, (1.0 - block (hitPosition + edgeDirection, hitNormal)) * smoothstep (0.5 - EDGE_LENGTH, 0.5 - EDGE_LENGTH * 0.5, min (edgeSmooth.x, edgeSmooth.y)));
	#else
	float edgeIntensity = 1.0 - block (hitPosition + step (edgeDistance.yzx, edgeDistance.xyz) * step (edgeDistance.zxy, edgeDistance.xyz) * edgeDirection, hitNormal);
	edgeIntensity *= smoothstep (0.5 - EDGE_LENGTH, 0.5 - EDGE_LENGTH * 0.5, max (edgeDistance.x, max (edgeDistance.y, edgeDistance.z)));
	#endif

	// Set the object color
	vec3 color = cos ((hitPosition + hitNormal * 0.5) * 0.05);
	color = rgb (vec3 (color.x + color.y + color.z + edgeIntensity * 0.05, 1.0, 1.0));

	// Lighting
	vec3 lightDirection = hitPosition - lightPosition;
	float lightDistance = length (lightDirection);
	lightDirection /= lightDistance;

	float lightIntensity = min (1.0, 1.0 / lightDistance);
	#ifdef SHADOW
	float lightHitDistance;
	vec3 lightHitNormal;
	hit (hitPosition - hitNormal * DELTA, -lightDirection, lightDistance, lightHitDistance, lightHitNormal);
	lightIntensity *= step (lightDistance, lightHitDistance);
	#endif

	// Bump mapping
	hitNormal = normalize (hitNormal + (1.0 - edgeIntensity) * randUnpredictable (floor (hitUV * BUMP_RESOLUTION) / BUMP_RESOLUTION) * BUMP_INTENSITY);

	// Shading
	float ambient = mix (AMBIENT_BLOCK, AMBIENT_EDGE, edgeIntensity) * edgePower;
	float diffuse = max (0.0, dot (hitNormal, lightDirection));
	float specular = pow (max (0.0, dot (reflect (rayDirection, hitNormal), lightDirection)), SPECULAR_POWER) * SPECULAR_INTENSITY;
	color = (ambient + diffuse * lightIntensity) * color + specular * lightIntensity;
	color *= pow (max (0.0, 1.0 - hitDistance / RAY_LENGTH_MAX), FADE_POWER);

	// Light source
	lightDirection = lightPosition - rayOrigin;
	if (dot (rayDirection, lightDirection) > 0.0) {
		lightDistance = length (lightDirection);
		if (lightDistance < hitDistance) {
			vec3 lightNormal = cross (rayDirection, lightDirection);
			color += smoothstep (0.001, 0.0, dot (lightNormal, lightNormal));
		}
	}

	// Adjust the gamma
	color = pow (color, vec3 (GAMMA));

	// Set the fragment color
	_gl_FragColor = vec4 (color, 1.0);
}

///////////////////
// Shadertoy (2) //
///////////////////

// Main function
void mainImage (out vec4 fragColor, in vec2 fragCoord) {

	// Set the position of the head
	headPosition = vec3 (32.0 * cos (iGlobalTime * 0.1), 9.0 + 9.0 * cos (iGlobalTime * 0.5), 2.0 + 2.0 * cos (iGlobalTime));

	// Set the orientation of the head
	float yawAngle = 4.0 * PI * iMouse.x / iResolution.x;
	float pitchAngle = -4.0 * PI * iMouse.y / iResolution.y;

	float cosYaw = cos (yawAngle);
	float sinYaw = sin (yawAngle);
	float cosPitch = cos (pitchAngle);
	float sinPitch = sin (pitchAngle);

	headRotate [0] = vec3 (cosYaw, 0.0, -sinYaw);
	headRotate [1] = vec3 (sinYaw * sinPitch, cosPitch, cosYaw * sinPitch);
	headRotate [2] = vec3 (sinYaw * cosPitch, -sinPitch, cosYaw * cosPitch);

	// Lighting
	lightPosition = headPosition + headRotate * vec3 (0.2 * sin (iGlobalTime * 2.0), 0.2 * sin (iGlobalTime * 3.0), 0.2 * sin (iGlobalTime) + 0.5);
	edgePower = 1.0; // TODO: implement a nice effect for the Shadertoy version :)

	// Set the fragment color
	_main (fragColor, fragCoord);
}