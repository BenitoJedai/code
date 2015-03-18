#define PIXEL_SAMPLES 1
#define LIGHT_SAMPLES 2
#define BSDF_SAMPLES 1
#define CAMERA_LENS_RADIUS			(0.0)
//#define CAMERA_FISHEYE

//render outputs channels
#define RENDER_OUTPUT_BEAUTY
//#define RENDER_OUTPUT_DEPTH
//#define RENDER_OUTPUT_NORMALS
//#define RENDER_OUTPUT_EYE_LIGHT



//light sampling technique *******************
//#define SAMPLE_LIGHT_AREA
#define SAMPLE_LIGHT_SOLIDANGLE
//********************************************

//#define SHOW_MIS_WEIGHTS
#define SHOW_PLANES




//#define TEMP_REGISTER_OPT
#define GAMMA 2.2
const vec3 backgroundColor = vec3( 0.2 );
#define DEPTH_MIN 0.2
#define DEPTH_MAX 16.0

//used macros and constants
#define PI 					3.1415926
#define TWO_PI 				6.2831852
#define FOUR_PI 			12.566370
#define INV_PI 				0.3183099
#define INV_TWO_PI 			0.1591549
#define INV_FOUR_PI 		0.0795775
#define EPSILON 			0.00001 
#define EQUAL_FLT(a,b,eps)	(((a)>((b)-(eps))) && ((a)<((b)+(eps))))
#define IS_ZERO(a) 			EQUAL_FLT(a,0.0,EPSILON)
//********************************************

#define MATERIAL_COUNT 		10
#define BSDF_COUNT 			3
#define BSDF_R_DIFFUSE 		0
#define BSDF_R_GLOSSY 		1
#define BSDF_R_SPECULAR 	2
#define BSDF_R_LIGHT 		3

//***********************************
//sampling types
#define SAMPLING_LIGHT				0
#define SAMPLING_BSDF				1
#define SAMPLING_LIGHT_AND_BSDF_MIS	2
int samplingTechnique;
//***********************************

#define LIGHT_COUNT (4)
#define LIGHT_COUNT_INV (0.25)
#define WALL_COUNT 	(2)

//MIS heuristics *****************************
//#define MIS_HEURISTIC_BALANCE
#define MIS_HEURISTIC_POWER

float misWeightPower( in float a, in float b ) {
    float a2 = a*a;
    float b2 = b*b;
    float a2b2 = a2 + b2;
    return (a2b2 > EPSILON)? a2 / a2b2 : 0.0;
}
float misWeightBalance( in float a, in float b ) {
    float ab = a + b;
    
    return (abs(ab) > EPSILON)? a / ab : 0.0;
}
float misWeight( in float pdfA, in float pdfB ) {
#ifdef MIS_HEURISTIC_POWER
    return misWeightPower(pdfA,pdfB);
#else
    return misWeightBalance(pdfA,pdfB);
#endif
}
//********************************************

// random number generator **********
// taken from iq :)
float seed;	//seed initialized in main
float rnd() { return fract(sin(seed++)*43758.5453123); }
//***********************************

//************************************************************************************
#define BRIGHTNESS(c) (0.2126*c.x + 0.7152*c.y + 0.0722*c.z)
// Color corversion code from: http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
vec3 hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}
//************************************************************************************


//////////////////////////////////////////////////////////////////////////
// Converting PDF from Solid angle to Area
float PdfWtoA( float aPdfW, float aDist2, float aCosThere ){
    if( aDist2 < EPSILON )
        return 0.0;
    return aPdfW * abs(aCosThere) / aDist2;
}

// Converting PDF between from Area to Solid angle
float PdfAtoW( float aPdfA, float aDist2, float aCosThere ){
    float absCosTheta = abs(aCosThere);
    if( absCosTheta < EPSILON )
        return 0.0;
    
    return aPdfA * aDist2 / absCosTheta;
}
//////////////////////////////////////////////////////////////////////////

// Data structures ****************** 
struct Sphere { vec3 pos; float radius; float radiusSq; float area; };
struct LightSample { vec3 pos; vec3 intensity; vec3 normal; float weight; };
struct Plane { vec4 abcd; };
struct Range { float min_; float max_; };
struct Material { vec3 color; float roughness_; int bsdf_; };
struct RayHit { vec3 pos; vec3 normal; vec3 E; int materialId; };
struct Ray { vec3 origin; vec3 dir; };
struct Camera { mat3 rotate; vec3 pos; float fovV; float lensSize; float focusDist; };
//***********************************
    
// ************ SCENE ***************
Plane walls[WALL_COUNT];
Sphere lights[LIGHT_COUNT];

#ifdef SHOW_PLANES
#define PLANE_COUNT (3)
Plane planes[PLANE_COUNT];
Range planeZRanges[PLANE_COUNT];
float planeHalfWidth = 2.3;
#endif
//***********************************

// ************************  INTERSECTION FUNCTIONS **************************
bool raySphereIntersection( Ray ray, in Sphere sph, out float t ) {
    t = -1.0;
	vec3  ce = ray.origin - sph.pos;
	float b = dot( ray.dir, ce );
	float c = dot( ce, ce ) - sph.radiusSq;
	float h = b*b - c;
    if( h > 0.0 ) {
		t = -b - sqrt(h);
	}
	
	return ( t > 0.0 );
}

bool rayPlaneIntersection( Ray ray, Plane plane, out float t ){
    float dotVN = dot( ray.dir, plane.abcd.xyz );
   
    if ( abs( dotVN ) < EPSILON ) {
        return false;
    }
    
	t = -(dot( ray.origin, plane.abcd.xyz ) + plane.abcd.w)/dotVN;
    
    return ( t > 0.0 );
}
// ***************************************************************************
    
void initSamplingTechnique( vec2 fragmentPos, vec2 splitterPos ) {
    if ( fragmentPos.y > splitterPos.y ) {
        if ( fragmentPos.x < splitterPos.x ) {
            samplingTechnique = SAMPLING_BSDF;
        } else {
            samplingTechnique = SAMPLING_LIGHT;
        }
    } else {
        samplingTechnique = SAMPLING_LIGHT_AND_BSDF_MIS;
    }
}

bool isSplitterPixel( vec2 fragmentPos, vec2 splitterPos ) {
    bool res = false;
    if( fragmentPos.y > splitterPos.y ) {
        if ( (abs(fragmentPos.x - splitterPos.x) < 1.0 ) ) {
            res = true;
        } else if ( (abs(fragmentPos.y - splitterPos.y) < 1.0 ) ) {
            res = true;
        }
    } else {
        if ( (abs(fragmentPos.y - splitterPos.y) < 1.0 ) ) {
            res = true;
        }
    }
    
    return res;
}

void initScene() {
    //init lights
    lights[0] = Sphere( vec3( -2.0, 1.4, -5.0 ), 0.05, 0.0025, 0.0314159 );
	lights[1] = Sphere( vec3( -1.1, 1.4, -5.0 ), 0.2, 0.04, 0.5026548 );
	lights[2] = Sphere( vec3( 0.0, 1.4, -5.0 ), 0.4, 0.16, 2.0106193 );
	lights[3] = Sphere( vec3( 1.6, 1.4, -5.0 ), 0.8, 0.64, 8.0424770 );
    
    float moveSize = 0.7;
    float a = 0.0;
    float speed = 2.0;
    for( int i=0; i<LIGHT_COUNT; i++ ){
        float val = a+iGlobalTime*speed;
        
        lights[i].pos += vec3( 0.0, sin(val), cos(val) )*moveSize*(1.0-float(i)*LIGHT_COUNT_INV);
        a += 0.4;
    }
    
    //init walls
    walls[0].abcd = vec4( 0.0, 1.0, 0.0, 1.0 );
    walls[1].abcd = vec4( 0.0, 0.0, 1.0, 6.2 );
    
#ifdef SHOW_PLANES
    //init planes
    vec3 planeNormal = normalize( vec3( 0.0, 1.0, 1.2 ) );
    planes[0].abcd = vec4( planeNormal, 3.8 );
    planeZRanges[0].min_ = -5.8;
    planeZRanges[0].max_ = -5.0;
    
    planeNormal = normalize( vec3( 0.0, 1.0, 0.7 ) );
    planes[1].abcd = vec4( planeNormal, 2.8 );
    planeZRanges[1].min_ = -4.8;
    planeZRanges[1].max_ = -4.0;
    
    planeNormal = normalize( vec3( 0.0, 1.0, 0.3 ) );
    planes[2].abcd = vec4( planeNormal, 1.8 );
    planeZRanges[2].min_ = -3.8;
    planeZRanges[2].max_ = -3.0;
#endif
}

Sphere getLightSphere( int index ) {
    for( int i=0; i<LIGHT_COUNT; i++ ) {
        if( index == i ){
            return lights[i];
        }
    }
    
    return lights[0];
}

#define GET_LIGHT_SPHERE_CONST(i) lights[i]


Material materialLibrary[MATERIAL_COUNT];

#define INIT_MTL(i,bsdf,phongExp,colorVal) materialLibrary[i].bsdf_=bsdf; materialLibrary[i].roughness_=phongExp; materialLibrary[i].color=colorVal;
void initMaterialLibrary()
{
    vec3 white = vec3( 1.0, 1.0, 1.0 );
    vec3 gray = vec3( 0.8, 0.8, 0.8 );
    
    //walls
    INIT_MTL( 0, BSDF_R_DIFFUSE, 0.0, white );
	
    //planes
    INIT_MTL( 1, BSDF_R_GLOSSY, 4096.0, gray );
    INIT_MTL( 2, BSDF_R_GLOSSY, 128.0, gray );
    INIT_MTL( 3, BSDF_R_GLOSSY, 32.0, gray );
    
    //lights
    float mult = 2.0;
    INIT_MTL( 4, BSDF_R_LIGHT, 0.0, hsv2rgb( vec3( 0.9, 0.6, 32.26 ) )*mult );
    INIT_MTL( 5, BSDF_R_LIGHT, 0.0, hsv2rgb( vec3( 0.1, 0.5, 2.0 ) )*mult );
    INIT_MTL( 6, BSDF_R_LIGHT, 0.0, hsv2rgb( vec3( 0.3, 0.6, 0.5 ) )*mult );
    INIT_MTL( 7, BSDF_R_LIGHT, 0.0, hsv2rgb( vec3( 0.5, 0.6, 0.1 ) )*mult );
}

#define GET_MTL(i) if( index == i ) { mtl = materialLibrary[i]; return; }
Material getMaterialFromLibrary( int index ){

    for(int i=0; i<MATERIAL_COUNT; i++ ) { 
    	if( index == i ) {
            return materialLibrary[i];
        }
    }
    
    return materialLibrary[0];
}
#define GET_MTL_CONST(i) materialLibrary[i]

vec3 GetLightIntensity( int lightId ) {
    for(int i=4; i<MATERIAL_COUNT; i++ ) { 
    	if( lightId == i ) {
            return materialLibrary[i].color;
        }
    }
    
    return vec3(0.0);
}

// Geometry functions ***********************************************************
vec2 uniformPointWithinCircle( in float radius, in float Xi1, in float Xi2 ) {
    float r = radius*sqrt(Xi1);
    float theta = Xi2;
	return vec2( r*cos(theta), r*sin(theta) );
}

vec3 uniformDirectionWithinCone( in vec3 d, in float phi, in float sina, in float cosa ) {    
	vec3 w = normalize(d);
    vec3 u = normalize(cross(w.yzx, w));
    vec3 v = cross(w, u);
	return (u*cos(phi) + v*sin(phi)) * sina + w * cosa;
}

vec3 localToWorld( in vec3 localDir, in vec3 normal )
{
    vec3 binormal = normalize( ( abs(normal.x) > abs(normal.z) )?vec3( -normal.y, normal.x, 0.0 ):vec3( 0.0, -normal.z, normal.y ) );
	vec3 tangent = cross( binormal, normal );
    
	return localDir.x*tangent + localDir.y*binormal + localDir.z*normal;
}

vec3 sphericalToCartesian( in float rho, in float phi, in float theta ) {
    float sinTheta = sin(theta);
    return vec3( sinTheta*cos(phi), sinTheta*sin(phi), cos(theta) )*rho;
}

vec3 sampleHemisphereCosWeighted( in vec3 n, in float Xi1, in float Xi2 ) {
    float theta = acos(sqrt(1.0-Xi1));
    float phi = TWO_PI * Xi2;

    return localToWorld( sphericalToCartesian( 1.0, phi, theta ), n );
}

vec3 randomDirection( in float Xi1, in float Xi2 ) {
    float theta = acos(1.0 - 2.0*Xi1);
    float phi = TWO_PI * Xi2;
    
    return sphericalToCartesian( 1.0, phi, theta );
}
//*****************************************************************************


// BSDF functions *************************************************************
float brdfEvalBrdfPhong( in  vec3 N, in vec3 E, in vec3 L, in float roughness ){
    vec3 R = reflect( E*(-1.0), N );
    float dotLR = dot( L, R );
    dotLR = max( 0.0, dotLR );
    return pow( dotLR, roughness + 1.0 ) * (roughness + 1.0) * (INV_PI);
}

vec3 brdfEvalDirPhong( in vec3 N, in vec3 E, in float roughness, in float r1, in float r2, out float pdf ) {
    vec3 R = reflect( E*(-1.0), N );
    float phi = r2*TWO_PI;
    float theta = acos( pow( r1, 1.0/( roughness+1.0 ) ) );
    vec3 dir = localToWorld( sphericalToCartesian( 1.0, phi, theta ), R );
    pdf = brdfEvalBrdfPhong( N, E, dir, roughness );
    return normalize(dir);
}

float brdfEvalBrdfDiffuse( in vec3 N, in vec3 L ){
    return clamp( dot( N, L ), 0.0, 1.0 )*INV_PI;
}

float brdfEvalPdfDiffuse( in vec3 N, in vec3 L ){
    return clamp( dot( N, L ), 0.0, 1.0 )*INV_PI;
}

vec3 brdfEvalDirDiffuse( in vec3 N, in float r1, in float r2, out float pdf ){
    vec3 dir = sampleHemisphereCosWeighted( N, r1, r2 );
    pdf = brdfEvalPdfDiffuse( N, dir );
    return dir;
}
//*****************************************************************************

///////////////////////////////////////////////////////////////////////
void initCamera( in vec3 pos, in vec3 frontDir, in vec3 upDir, in float fovV, in float lensSize, in float focusDist, out Camera dst ) {
	vec3 back = normalize( -frontDir );
	vec3 right = normalize( cross( upDir, back ) );
	vec3 up = cross( back, right );
    dst.rotate[0] = right;
    dst.rotate[1] = up;
    dst.rotate[2] = back;
    dst.fovV = fovV;
    dst.pos = pos;
    dst.lensSize = lensSize;
    dst.focusDist = focusDist;
}

Ray genRay( in Camera camera, in vec2 pixel, in float Xi1, in float Xi2 )
{
#ifdef CAMERA_FISHEYE
    vec2 uv = pixel/iResolution.xy;
    
    uv = (uv*2.0 - 1.0)*vec2(iResolution.x/iResolution.y,1.);
    
    float fov = camera.fovV;
    float angle = fov/4.0;
    float a = sin(angle);
    
    uv *= a;
    
    if( length(uv) > 1.0 ) {
        Ray ray;
        ray.origin = vec3( 0.0, 0.0, 0.0 );
        ray.dir = vec3( 0.0, 0.0, 0.0 );
        return ray;
    }
    
    vec3 cameraDirInv = vec3( 0.0, 0.0, 1.0 );
    vec3 normal;
    normal.x = -uv.x;
    normal.y = -uv.y;
    normal.z = sqrt( 1.0 - (uv.x*uv.x + uv.y*uv.y) );
    
    Ray ray;
	ray.origin = camera.pos;
	ray.dir = camera.rotate*reflect( cameraDirInv, normal );

	return ray;
#else
	vec2 iPlaneSize=2.*tan(0.5*camera.fovV)*vec2(iResolution.x/iResolution.y,1.);
	vec2 ixy=(pixel/iResolution.xy - 0.5)*iPlaneSize;
    
    Ray ray;
    if( camera.lensSize < EPSILON ) {
        ray.origin = camera.pos;
		ray.dir = camera.rotate*normalize(vec3(ixy.x,ixy.y,-1.0));
    } else {
	    vec2 uv = uniformPointWithinCircle( camera.lensSize, rnd(), rnd() );
    	vec3 newPos = camera.pos + camera.rotate[0]*uv.x + camera.rotate[1]*uv.y;
    	vec3 focusPoint = camera.pos - camera.focusDist*camera.rotate[2];
    	vec3 newBack = normalize(newPos - focusPoint);
    	vec3 newRight = normalize( cross( camera.rotate[1], newBack ) );
		vec3 newUp = cross( newBack, newRight );
    	mat3 newRotate;
    	newRotate[0] = newRight;
    	newRotate[1] = newUp;
    	newRotate[2] = newBack;
   
		ray.origin = newPos;
		ray.dir = newRotate*normalize(vec3(ixy.x,ixy.y,-1.0));
    }

	return ray;
#endif
}


bool raySceneIntersection( in Ray ray, in float distMin, out RayHit hit, out int objId, out float dist ) {
    float nearest_dist = 100000.0;
    hit.E = ray.dir*(-1.0);
    
    //check lights
    for( int i=0; i<LIGHT_COUNT; i++ ){
        float dist;
        if( raySphereIntersection( ray, lights[i], dist ) && (dist>distMin) && ( dist < nearest_dist ) ) {
            nearest_dist = dist;
            
            hit.pos = ray.origin + ray.dir*nearest_dist;
    		hit.normal = normalize(hit.pos - lights[i].pos);
    		hit.materialId = 4 + i;
            objId = i;
        }
    }
    
    //check walls
    for( int i=0; i<WALL_COUNT; i++ ){
        float dist;
        if( rayPlaneIntersection( ray, walls[i], dist ) && (dist>distMin) && (dist < nearest_dist ) ){
            nearest_dist = dist;
            
            hit.pos = ray.origin + ray.dir*nearest_dist;
    		hit.normal = walls[i].abcd.xyz;
    		hit.materialId = 0;
            objId = LIGHT_COUNT + i;
        }
    }
    
#ifdef SHOW_PLANES
    //check planes
    for( int i=0; i<PLANE_COUNT; i++ ){
        float dist;
        if( rayPlaneIntersection( ray, planes[i], dist ) && (dist>distMin) && (dist < nearest_dist ) ){
            vec3 hitPos = ray.origin + ray.dir*dist;
            if( (hitPos.z < planeZRanges[i].max_ ) && (hitPos.z > planeZRanges[i].min_) && (hitPos.x < planeHalfWidth ) && (hitPos.x > -planeHalfWidth ) ) {
                nearest_dist = dist;

                hit.pos = hitPos;
                hit.normal = planes[i].abcd.xyz;
                hit.materialId = 1+i;
                objId = LIGHT_COUNT + WALL_COUNT + i;
            }        
        }
    }
#endif
    
    if ( nearest_dist < 99999.0 ) {
        dist = nearest_dist;
    }
    
    return ( dist > 0.0 );
}

vec3 sampleBSDF( 	in RayHit hit,
                	in int bsdf,
                	in float surfaceRoughness,
                	in bool useMIS ) {
    vec3 light = vec3( 0.0 );
    
    for( int i=0; i<BSDF_SAMPLES; i++ ) {
        vec3 bsdfDir;
        float bsdfPdfW;

        //Generate direction proportional to bsdf
        if( bsdf == BSDF_R_GLOSSY ) {
            bsdfDir = brdfEvalDirPhong( hit.normal, hit.E, surfaceRoughness, rnd(), rnd(), bsdfPdfW );
        } else {
            bsdfDir = brdfEvalDirDiffuse( hit.normal, rnd(), rnd(), bsdfPdfW );
        }

        if( (dot( bsdfDir, hit.normal ) > 0.0) && (bsdfPdfW > EPSILON) ){
            //calculate light visibility
            float lightDist;
            int lightId;
            RayHit newHit;
            Ray shadowRay = Ray( hit.pos, bsdfDir );
            
            if( raySceneIntersection( shadowRay, EPSILON, newHit, lightId, lightDist ) && (newHit.materialId >=4) ) {
                 //Read light info
                float weight = 1.0;
                
                if ( useMIS ) {
        			Sphere lightSphere = getLightSphere( lightId );
                    float lightPdfW;
                    
#ifdef SAMPLE_LIGHT_SOLIDANGLE
                    vec3 lightDir = lightSphere.pos - hit.pos;
    				float d2 = dot(lightDir, lightDir);
                    float sin_a_max_2 = lightSphere.radiusSq / d2;
    				float cos_a_max = sqrt( 1.0 - min( 1.0, sin_a_max_2 ) );
                    lightPdfW = 1.0/(TWO_PI * (1.0 - cos_a_max));	//1.0/SolidAngle
#else
                    float lightPdfA = 1.0/lightSphere.area;
                    lightPdfA *= LIGHT_COUNT_INV;
                    //lightPdfA *= float(LIGHT_SAMPLES)/float(LIGHT_SAMPLES + BSDF_SAMPLES);
                    float cosTheta1 = max( 0.0, dot( newHit.normal, -bsdfDir ) );
                    lightPdfW = PdfAtoW( lightPdfA, lightDist*lightDist, cosTheta1 );
#endif

                    weight = misWeight( bsdfPdfW, lightPdfW );
                }
                
                light += GetLightIntensity( newHit.materialId )*weight;
            }
        }
    }

    return light*(1.0/float(BSDF_SAMPLES));
}

//Sample spherical area light with 'Solid angle sampling' technique
void sampleLightW( in Sphere lightSphere, vec3 x, float Xi1, float Xi2, out vec3 Wi, out float pWi ) { 
   	vec3 dirToLightCenter = lightSphere.pos - x;
    float distToLightCenter2 = dot(dirToLightCenter, dirToLightCenter);
    float sin_a_max_2 = lightSphere.radiusSq / distToLightCenter2;
    float cos_a_max = sqrt( 1.0 - clamp( sin_a_max_2, 0.0, 1.0 ) );
    
    float omega = TWO_PI * (1.0 - cos_a_max);	//solid angle
    float cosa = mix( cos_a_max, 1.0, Xi1 );
    float sina = sqrt(1.0 - cosa*cosa);

    Wi = uniformDirectionWithinCone( dirToLightCenter, TWO_PI*Xi2, sina, cosa );
    pWi = 1.0/omega;
}

//Sample spherical area light with 'Area sampling' technique
void sampleLightA( in Sphere lightSphere, vec3 x, float Xi1, float Xi2, out vec3 Wi, out float pWi ) { 
    //pick point on light surface
    vec3 lightN = randomDirection( Xi1, Xi2 );
    vec3 lightP = lightSphere.pos + lightN*lightSphere.radius;
    float pdfA = 1.0/lightSphere.area;;

    Wi = lightP - x;
    float distTolightP2 = dot( Wi, Wi );
    Wi *= 1.0/sqrt(distTolightP2);
    float cosThetaAtLight = dot( lightN, -Wi );

    if( cosThetaAtLight < 0.0 ) {
        pWi = 0.0;
    } else {
    	pWi = PdfAtoW( pdfA, distTolightP2, cosThetaAtLight );
    }
}

vec3 salmpleLight( in RayHit hit, in int bsdf, in float surfaceRoughness, in bool useMIS ) {
    vec3 Li;				//incomming radiance
    vec3 Lo = vec3( 0.0 );	//outgoing radiance
    vec3 Wi;				//direction to light
	float lightPdfW;		//pdf of choosing Wi with 'light sampling' technique
    float brdfPdfW;			//pdf of choosing Wi with 'bsdf sampling' technique
    
    for( int i=0; i<LIGHT_SAMPLES; i++ ) {
        //select light to sample
        float lightPickProbability;
        int lightId;

        lightPickProbability = LIGHT_COUNT_INV;
    	lightId = int( rnd()*float(LIGHT_COUNT) );

        //Read light info
        Sphere lightSphere = getLightSphere( lightId );
        Li = GetLightIntensity( 4+lightId );

#ifdef SAMPLE_LIGHT_SOLIDANGLE
        sampleLightW( lightSphere, hit.pos, rnd(), rnd(), Wi, lightPdfW );	
#else
        sampleLightA( lightSphere, hit.pos, rnd(), rnd(), Wi, lightPdfW );
#endif
        lightPdfW *= lightPickProbability;

        if ( (dot(Wi,hit.normal) > 0.0) && (lightPdfW > EPSILON) ) {
            Ray shadowRay = Ray( hit.pos, Wi );
            float dist;
            raySphereIntersection( shadowRay, lightSphere, dist );

            RayHit newHit;
            float newDist;
            int newId;

            if( raySceneIntersection( shadowRay, EPSILON, newHit, newId, newDist ) && EQUAL_FLT(newDist,dist,EPSILON) ) {
                
                float brdf;
                if( bsdf == BSDF_R_GLOSSY ) {
                    brdf = brdfEvalBrdfPhong( hit.normal, hit.E, Wi, surfaceRoughness );
                    brdfPdfW = brdf;	//sampling Pdf matches brdf
                } else {
                    brdf = brdfEvalBrdfDiffuse( hit.normal, Wi );
                    brdfPdfW = brdf;	//sampling Pdf matches brdf
                }

                //not used here
                //float cosTheta = dot( wi, hit.normal );

                float weight = 1.0;
                if( useMIS ) {
                    weight = misWeight( lightPdfW, brdfPdfW );
                }
                
                Lo += ( Li * brdf * weight ) / lightPdfW;
            }
        }
    }
    
    return Lo*(1.0/float(LIGHT_SAMPLES));
}
    
vec3 calculateDirectLight( in RayHit hit, in int bsdf, in float surfaceRoughness ) {    
    vec3 light = vec3(0.0);
	if( (bsdf == BSDF_R_LIGHT) || ( bsdf == BSDF_R_DIFFUSE ) || ( bsdf == BSDF_R_GLOSSY ) ) {       
        if( samplingTechnique == SAMPLING_LIGHT ) {
			light += salmpleLight( hit, bsdf, surfaceRoughness, false );
        } else if( samplingTechnique == SAMPLING_BSDF ) {
            light += sampleBSDF( hit, bsdf, surfaceRoughness, false );
        } else {
            light += sampleBSDF( hit, bsdf, surfaceRoughness, true );
            light += salmpleLight( hit, bsdf, surfaceRoughness, true );
        }
	} else if( bsdf == BSDF_R_GLOSSY ) {
		//not implemented yet
	} else if( bsdf == BSDF_R_SPECULAR ) {
		//not implemented yet
	}
    
	return light;
}

vec3 Radiance( in Ray ray ) {
    vec3 res;
   	
    RayHit hit;
    //Calculate nearest intersection
    float dist;
    int objId;
    if( raySceneIntersection( ray, 0.0, hit, objId, dist ) ) {
        
#ifdef RENDER_OUTPUT_DEPTH
    	float distNormalized = clamp( dist, DEPTH_MIN, DEPTH_MAX )/(DEPTH_MAX-DEPTH_MIN);
        return hsv2rgb( vec3(distNormalized, 0.7, 0.7) );
#endif
        
#ifdef RENDER_OUTPUT_NORMALS
    	return hit.normal;
#endif
        
#ifdef RENDER_OUTPUT_EYE_LIGHT
    	return vec3(1.0)*dot(hit.normal,hit.E);
#endif
        
#ifdef RENDER_OUTPUT_BEAUTY
    	Material mtl = getMaterialFromLibrary( hit.materialId );

        vec3 f, Le;

        if( mtl.bsdf_ == BSDF_R_LIGHT ) {
            Le = mtl.color;
            f = vec3( 1.0, 1.0, 1.0 );
        } else {
            Le = vec3( 0.0 );
            f = mtl.color;
        }

        return Le + f * calculateDirectLight( hit, mtl.bsdf_, mtl.roughness_ );
#endif
    }

    return backgroundColor;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    seed = /*iGlobalTime +*/ iResolution.y * fragCoord.x / iResolution.x + fragCoord.y / iResolution.y;
    
	float sinTime = sin(iGlobalTime*0.2);
    vec3 cameraPos = vec3( 0.0, 1.0 + sin(iGlobalTime*0.3), 0.0 + sin(iGlobalTime*0.4) );
    vec3 cameraTarget = vec3( sin(iGlobalTime*0.4)*0.3, 0.0, -5.0 );
    
    Camera camera;
    initCamera( cameraPos, cameraTarget - cameraPos, vec3( 0.0, 1.0, 0.0 ), radians(70.0), CAMERA_LENS_RADIUS, 8.0, camera );
    initScene();
    initMaterialLibrary();
    initSamplingTechnique( fragCoord.xy, iMouse.xy );
	
    Ray ray;
	vec3 accumulatedColor = vec3( 0.0 );
	for(int si=0; si<PIXEL_SAMPLES; ++si ){
        vec2 screenCoord = fragCoord.xy + vec2( (1.0/float(PIXEL_SAMPLES))*(float(si)+rnd()), rnd() );
        ray = genRay( camera, screenCoord, rnd(), rnd() );
        
        accumulatedColor += Radiance( ray );
	}
	
	//devide to sample count
	accumulatedColor = accumulatedColor*(1.0/float(PIXEL_SAMPLES));
	
	//gamma correction
    accumulatedColor = pow( accumulatedColor, vec3( 1.0 / GAMMA ) );
    
    // show slider position
    if( isSplitterPixel( fragCoord.xy, iMouse.xy ) ) {
        accumulatedColor = vec3( 1.0 );
    }
	
	fragColor = vec4( accumulatedColor,1.0 );
}