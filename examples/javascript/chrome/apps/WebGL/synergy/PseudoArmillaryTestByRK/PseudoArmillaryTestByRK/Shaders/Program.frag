// Simple Ray marcher over distance field. 
// To be improved on the BTEs and the approximations quality.
// I am mainly using tools originally written by Inigo Quilez.
// See his articles at : http://www.iquilezles.org/www/index.htm

#define RM_USE_ABSORPTION
#define RM_USE_SCATTERING

// Will requires : 
vec2 sceneMap(vec3 p);
vec4 renderMaterial(vec3 pos, vec3 normal, float material, float d, float absorption, float scattering);

// Can require : 
#ifdef RM_USE_ABSORPTION
float absorption(vec3 p, vec3 dir, float d);
#endif

#ifdef RM_USE_SCATTERING
float scattering(vec3 p, vec3 dir, float d);
#endif

// Primitives :
float plane(vec3 p)
{
    return p.y;
}

float sphere(vec3 p, float radius)
{
    return length(p)-radius;
}

float torus(vec3 p, vec2 t)
{
    return length(vec2(length(p.xz)-t.x,p.y))-t.y;
}

float length4(vec2 p)
{
    p = p*p; p = p*p;
    return pow( p.x + p.y, 1.0/4.0 );
}

float torus42(vec3 p, vec2 t)
{
    vec2 q = vec2(length(p.xz)-t.x,p.y);
    return length4(q)-t.y;
}

// Operators :
vec2 opUnion(vec2 d1, vec2 d2)
{
    return (d1.x<d2.x) ? d1 : d2;
}

float opSubstraction(float d1, float d2)
{
    return max(-d1,d2);
}

float opIntersection(float d1, float d2)
{
    return max(d1,d2);
}

// Core functions for the RayMarcher : 
float sceneMapSimple(vec3 p)
{
    vec2 tmp = sceneMap(p);
    return tmp.x;
}

mat3 computeCameraMatrix(in vec3 p, in vec3 target, float roll)
{
    vec3 	vForward = normalize(target-p),
        	vUpAlign = vec3(sin(roll), cos(roll), 0.0),
        	vLeftReal = normalize(cross(vForward, vUpAlign)),
        	vUpReal = normalize(cross(vLeftReal, vForward));
    return mat3(vLeftReal, vUpReal, vForward);
}

vec4 castRay(in vec3 rayOrigin, in vec3 rayDirection, const float dMin, const float dNear, const float dMax)
{
    const int numSteps = 128;
    float d = dMin;
    float m = -1.0;
    float a = 0.0, s = 0.0;
    for(int i=0; i<numSteps; i++)
    {
        vec3 p = rayOrigin+rayDirection*d;
        vec2 res = sceneMap(p);
        if(res.x<dNear || d>dMax)
            break;
        d += res.x;
        m = res.y;
        #ifdef RM_USE_ABSORPTION
        a += absorption(p, rayDirection, res.x);
        #endif
        #ifdef RM_USE_SCATTERING
        s += scattering(p, rayDirection, res.x);
        #endif
    }
    if(d>dMax)
        m = -1.0;
    return vec4(d, m, a, s);
}

vec3 calcNormal(in vec3 pos)
{
    const vec3 eps = vec3( 0.001, 0.0, 0.0 );
    vec3 n = vec3(	sceneMap(pos+eps.xyy).x - sceneMap(pos-eps.xyy).x,
                  	sceneMap(pos+eps.yxy).x - sceneMap(pos-eps.yxy).x,
                  	sceneMap(pos+eps.yyx).x - sceneMap(pos-eps.yyx).x );
    return normalize(n);
}

vec4 renderScene(const ivec2 formatSize, vec3 eyePos, vec3 eyeTarget, const float focalLength, const float dMin, const float dNear, const float dMax)
{
    mat3 camera = computeCameraMatrix(eyePos, eyeTarget, 0.0);
    vec2 o = (gl_FragCoord.xy - vec2(formatSize)/2.0)/max(float(formatSize.x),float(formatSize.y));
    vec3 rayOrigin = vec3(o, 0.0) + eyePos,
        rayDirection = normalize(camera*vec3(o, focalLength));
    vec4 res = castRay(rayOrigin, rayDirection, dMin, dNear, dMax);
    vec3 p = rayOrigin + rayDirection * res.x;
    vec3 n = calcNormal(p);
    return renderMaterial(p, n, res.y, res.x, res.z, res.w);
}

// Other tools : 
float softShadow(vec3 rayOrigin, vec3 lightPos, float dNearLight, float kShadowSoftness)
{
    const int maxStep = 128;
    const float dNearIntersect = 0.0000001;
    vec3 rayDirection = lightPos - rayOrigin;
    float dMax = length(rayDirection) - dNearLight;
    rayDirection = normalize(rayDirection);
    float res = 1.0;
    float d=0.0;
    for(int k=0; k<maxStep; k++)
    {
        float closest = sceneMapSimple(rayOrigin + rayDirection*d);
        if(closest<dNearIntersect)
            return 0.0;
        res = min(res, kShadowSoftness*closest/d);
        d += closest;
        if(d>=dMax)
            break;
    }
    return res;
}

// Scene settings :
const vec3 	eyeTarget = vec3(0, 1.4, 5),
    		objectCenter = vec3(0,1.8,5.0);
const float focalLength = 0.5;
const vec3 	lightPos = vec3(2.8, 3, 4.3),
            lightDir = vec3(-2.0, -0.5, 0.7),
        	lightCol = vec3(1, 1, 1.5);
const float lightSpan = 0.7, // cosine
    		lightRadiance = 10.0,
    		thetaRing2 = 0.2,
    		thetaRing3 = 0.4;
mat3 rotMat2, rotMat3;

float test(vec3 p)
{
    return length(max(abs(p-vec3(0,0,5))-0.5,0.0));
}

vec2 sceneMap(in vec3 p)
{
    return	opUnion( vec2(plane(p), 1), 
            opUnion( vec2(sphere(p-lightPos, 0.01), 1024),
            opUnion( vec2(sphere(p-objectCenter, 0.5), 2),
            opUnion( vec2(torus42(p-objectCenter, vec2(1.0, 0.1)), 3),
            opUnion( vec2(torus42(rotMat2*(p-objectCenter), vec2(1.3, 0.1)), 4),
                     vec2(torus42(rotMat3*(p-objectCenter), vec2(1.6, 0.1)), 5)
                     )))));
}


float absorption(vec3 p, vec3 dir, float d)
{
    return d*exp(-p.y*5.0)/2.0;
}

float scattering(vec3 p, vec3 dir, float d)
{
    vec3 u = normalize(p - lightPos);
    float dLight = distance(p, lightPos);
    return  d*0.2*exp(-p.y*0.1)/(dLight*dLight) * (max(dot(u, normalize(lightDir)), lightSpan)-lightSpan)/(1.0-lightSpan);
}

vec4 renderMaterial(vec3 p, vec3 normal, float material, float dist, float absorption, float scattering)
{
    vec4 col = vec4(0, 0, 0, 1);

    if(material>0.0) // Valid object intersection
    {
        if(material<=1.0) //floor
            col = mod(floor(5.0*p.z) + floor(5.0*p.x), 2.0)*vec4(0.3,0.3,0.3,0)+vec4(0.3,0.3,0.3,1);
        else // other
            col = vec4(material/5.0,1.0-material/5.0, material*material/25.0-p.y/3.0, 1.0);

        // Simple shading :
        vec3 u = p - lightPos;
        float l = length(u);
        u = u/l;
        float s = max(dot(-u, normal),0.0) * max((max(dot(u, normalize(lightDir)), lightSpan)-lightSpan)/(1.0-lightSpan), 0.0) / max(l*l, 1.0) * lightRadiance;
        col = col * max(s,0.0001) * vec4(lightCol,1);

        // Light source : 
        col = (material>=1024.0) ? vec4(1,1,1,1) : col;
    }

    col.rgb = col.rgb * softShadow(p, lightPos, 1.0, 32.0) * max(1.0 - absorption,0.0) + scattering * lightRadiance * lightCol/max(1.0,dist*dist);

    return col;
}

void mainImage(out vec4 fragColor, in vec2 fragCoord )
{
    // Setup the animation of the scene :
	rotMat2 = mat3( 1, 0, 0,
                    0, cos(thetaRing2*iGlobalTime), sin(thetaRing2*iGlobalTime),
                    0, -sin(thetaRing2*iGlobalTime), cos(thetaRing2*iGlobalTime));

	rotMat3 = mat3(	cos(thetaRing3*iGlobalTime), sin(thetaRing3*iGlobalTime), 0,
                    -sin(thetaRing3*iGlobalTime), cos(thetaRing3*iGlobalTime), 0,
                     0, 0, 1);
    vec3 eyePos = eyeTarget + vec3(4.0*cos(0.1*iGlobalTime), 1.5, 4.0*sin(0.1*iGlobalTime));
    
    // Render :
    vec4 c = renderScene(ivec2(iResolution.xy), eyePos, eyeTarget, focalLength, 0.5, 0.0001, 100.0);
    fragColor = vec4(pow(c.rgb, vec3(1, 1, 1)/2.2), 1.0);
}
