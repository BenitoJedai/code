const float EPSILON = 0.0001;
const float E = 2.71828;
const float RT2 = 0.7071067;


float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
	f = f*f*(3.0-2.0*f);
	vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;
	vec2 rg = texture2D( iChannel0, (uv+ 0.5)/256.0, -100.0 ).yx;
	return -1.0+2.0*mix( rg.x, rg.y, f.z );
}

// helper function to create a 3d rotation matrix.
mat3 rotateX(float angle) {
    float ca = cos(angle);
    float sa = sin(angle);
	return mat3(1, 0, 0,  0, ca, -sa,  0, sa, ca);
}

// helper function to create a 3d rotation matrix.
mat3 rotateY(float angle) {
    float ca = cos(angle);
    float sa = sin(angle);
	return mat3(ca, 0, sa,  0, 1, 0,  -sa, 0, ca);
}

/*
	This function returns the distance from a specified point (p)
	to the edge of a sphere centered on c with radius r.

	Think of this as a 3d gradient which is black (0.0) at the edge of the sphere and
	progressively brighter as you move away from the sphere. Inside the sphere, the
	distance is negative up to -r at the center.
*/
float distanceSphere(vec3 p, vec3 c, float r) {
	return length(p - c) - r;
}


// simple function to get the distance from a plane
float plane(vec3 p, vec4 n) {
    return dot(p,n.xyz) + n.w;
}

/*
	This function gives the distance to a diamond cut object.
	h.x specifies the side length, h.y specifies the height
*/
float distanceDiamond(vec3 p, vec2 h) {
    // object is symmetrical about x and z axes
	vec3 q = vec3(abs(p.x), abs(p.y), abs(p.z));
    
    // specify the points that make up the object in the positive octant
    vec3 p1 = vec3(1.0, 0., 0.);
    vec3 p2 = vec3(RT2, 0., RT2);
    vec3 p3 = vec3(0., 1.0, 0.);
    vec3 p4 = vec3(0., 0., 1.0);
    
    // get the plane equations as a vec4 by using cross product on the points
    vec4 s1 = vec4(normalize(cross(p2-p1, p3-p1)), length(p1)+h.x);
    vec4 s2 = vec4(normalize(cross(p4-p2, p3-p2)), length(p2)+h.x);
    
    // get the farthest distance to one of the planes
    // p.y - h.y is the flat top of the diamond on one side
    // the second max gives the distance to one of the sides
    float dist = max(p.y - h.y, max(plane(q, -s1), plane(q, -s2)));
	return dist;
}

/*
	Scene is a function that combines all the other distance functions and modifications
	so that for any given point, it returns the closest distance to something
	in the complete scene.
*/
float scene(vec3 p) {
 	return distanceDiamond(p, vec2(0.2, 0.4));
}

/*
	Gets the normal direction at a given point in space.
	Compute the derivative of each spatial direction.
*/
vec3 getNormal(vec3 p) {
	vec3 n1 = vec3(
        scene(vec3(p.x + EPSILON, p.y, p.z)),
        scene(vec3(p.x, p.y + EPSILON, p.z)),
        scene(vec3(p.x, p.y, p.z + EPSILON))
    );
    return normalize(n1 - scene(p));
}

float diffuse(vec3 normal, vec3 lightRay, float intensity) {
    return clamp(dot(lightRay, normal), 0.0, 1.0);
}

// fragCoord is [0, iResolution.xy]
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // uv is the screen space coordinates stretched by the aspect ratio to give
    // a relative position between 0.0 and 1.0 for both x and y
	vec2 uv = fragCoord.xy / iResolution.xy * 2.0 - 1.0;
    float aspectRatio = iResolution.x / iResolution.y;
    uv.x *= aspectRatio;
    
    // camera position starts off to the side of the scene and is rotated based on the mouse pos
    vec3 cameraPos = vec3(0.0, 0.0, 5.0);
    mat3 mouseRotation = rotateX(iMouse.y / iResolution.y * 3.1415 * 3.0) * rotateY(iMouse.x / iResolution.x * 3.1415 * 3.0);
    
    // rays start from the camera
    vec3 rayOrigin = cameraPos;
    vec3 rayDirection = normalize(vec3(uv, -1.0));
    
    // multiplying the vector by the matrix rotates it in space
    // think of this as rotating the camera around the origin point
    rayOrigin *= mouseRotation;
    rayDirection *= mouseRotation;
    
    float minStep = 0.25;
    float distance = minStep * 2.0;
    vec3 currentPos = rayOrigin;
    const int MAX_STEPS = 100;
    int stepped = 0;
    
    for(int i=0; i<MAX_STEPS; i++) {
     	currentPos += abs(distance - minStep) * rayDirection;
        distance = scene(currentPos);
        if(distance < minStep) {
            stepped = i;
			break;
        }
    }
    
    if(distance > 1.0) {
		fragColor = vec4(0.0, 0.0, 0.0, 1.0);
        return;
    }
    
    vec3 lightPos = vec3(6.0, 4.0, 10.0);
    
    vec3 lightDiff = lightPos - currentPos;
    
    vec3 directionToLight = normalize(lightDiff);
	float distanceToLight = length(lightDiff);
    
    vec3 normal = getNormal(currentPos);
    vec3 reflectAngle = reflect(rayDirection, normal);
    
    vec3 color = vec3(0.9, 0.5, 0.3);
    float diffuse = clamp(dot(directionToLight, normal), 0.0, 1.0);
    float specular = pow(clamp(dot(reflectAngle, directionToLight), 0.0, 1.0), 128.0);
    float ambient = 0.3;
    vec3 final = diffuse * color + vec3(specular) + vec3(ambient);
    //vec3 final = getNormal(currentPos) + 0.5;
    
	fragColor = vec4(final, 1.0);
}