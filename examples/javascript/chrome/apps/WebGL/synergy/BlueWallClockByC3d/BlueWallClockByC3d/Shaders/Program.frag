#define time iGlobalTime
#define resolution (iResolution.xy)
#define mouse (iMouse.xy)
#define seconds (iDate.w)
#define minutes (seconds/60.0)
#define hours (minutes/60.0)


const vec3 light = vec3(5.5, 10.0, 5.5);

const int raytraceDepth = 5;
const float attenuation = 0.5;
const float max_cube_size = 1.0;
const float todeg = 3.14159265358 / 180.0;

struct Ray
{
    vec3 org;
    vec3 dir;
};

struct Sphere
{
    vec3 c;
    float r;
    vec4 col;
};

struct Plane
{
    vec3 p;
    vec3 n;
    vec4 col1, col2;
};


struct Cube
{
    vec3 c;
    vec3 h_side;
    vec4 col;
};

struct Intersection
{
    float t;
    vec3  p;                    // hit point
    vec3  n;                    // normal
    int   hit;
    vec4  col;
};

vec3 random(vec3 p)
{
    return fract(1e4*sin(vec3(1e3, 2.2223e3, 3.117e3) *
                         (p + 2.1*p.yzx + 3.2*p.zxy)));
}

void sphere_intersect(Sphere s,  Ray ray, inout Intersection isect)
{
    // rs = ray.org - sphere.c
    vec3 rs = ray.org - s.c;
    float B = dot(rs, ray.dir);
    float C = dot(rs, rs) - (s.r * s.r);
    float D = B * B - C;

    if (D > 0.0)
    {
        float t = -B - sqrt(D);
        if (t > 0.0 && t < isect.t)
        {
            isect.t = t;
            isect.hit = 1;

            // calculate normal.
            vec3 p = vec3(ray.org.x + ray.dir.x * t,
                          ray.org.y + ray.dir.y * t,
                          ray.org.z + ray.dir.z * t);
            vec3 n = p - s.c;
            n = normalize(n);
            isect.n = n;
            isect.p = p;
            isect.col = s.col;
        }
    }
}

float dpmod(float x) { return mod(x, 1.0) - 0.5; }

void plane_intersect(Plane pl, Ray ray, inout Intersection isect)
{
    float d = -dot(pl.p, pl.n);
    float v = dot(ray.dir, pl.n);


    // Check if the plane is parallel to the ray.
    if (abs(v) < 1.0e-6)
        return;

    float t = -(dot(ray.org, pl.n) + d) / v;

    if ( (t > 0.0) && (t < isect.t) )
    {
        isect.hit = 1;
        isect.t   = t;
        isect.n   = pl.n;

        vec3 p = vec3(ray.org.x + t * ray.dir.x,
                      ray.org.y + t * ray.dir.y,
                      ray.org.z + t * ray.dir.z);
        isect.p = p;
        float offset = 0.2;
        vec3 dp = p + offset;

        if (dpmod(dp.x) * dpmod(dp.y) * dpmod(dp.z) < 0.0)
            isect.col = pl.col1;
        else
            isect.col = pl.col2;
    }
}


void cube_intersect(Cube c,  Ray ray, inout Intersection isect)
{
    vec3 vertex1 = c.c - c.h_side;
    vec3 vertex2 = c.c + c.h_side;
    vec3 ro = ray.org;
    vec3 rd = ray.dir;

    float t;
    float tNear = isect.t;
    vec3 normal = vec3(1);
    vec3 interp = ro;

#define CHECK_CUBE_COORD(A, B, C) if(rd.A == 0.0) { if(ro.A < vertex1.A || ro.A > vertex2.A) return; } else { if (rd.A > 0.0) t = (vertex1.A - ro.A) / rd.A; else t = (vertex2.A - ro.A) / rd.A; if(t >= 0.0 && t < tNear) { vec3 ip = ro + rd * t; if (ip.B >= vertex1.B && ip.B <= vertex2.B && ip.C >= vertex1.C && ip.C <= vertex2.C) { tNear = t; normal.A = -sign(rd.A); normal.B = 0.0; normal.C = 0.0; interp = ip; } } } 

    CHECK_CUBE_COORD(x, y, z);
    CHECK_CUBE_COORD(y, x, z);
    CHECK_CUBE_COORD(z, x, y);

    t = tNear;
    if (t > 0.0 && t < isect.t)
    {
        isect.t = t;
        isect.hit = 1;
        isect.p = interp;
        isect.col = c.col;
        isect.n = normal;
    }
}
		

void cube_compute(vec3 iip, out Cube cu)
{
    cu.c = iip + (0.1 * random(1.113 * iip) - 0.05);
    cu.h_side = vec3(0.08) + 0.05 * random(2.117*iip).z;
    vec3 col = vec3(0.1, 0.6, 0.2)*random(0.997*iip) + vec3(0.6, 1.4, 1.9);
    cu.col = vec4(col, 4.4);
}

void cubes_intersect(float z, Ray ray, inout Intersection isect)
{
    ray.org.x += time;
    vec3 ro = ray.org;
    vec3 rd = ray.dir;
    float t0 = isect.t;

    // Compute intersection with plane at z
    if (abs(rd.z) < 1e-5)
        return;
    float t = (z - ro.z)/rd.z;
    if (t < 0.0 || t >= t0)
        return;

    // Find the cube for that intersection
    vec3 ip = ro + t * rd;
    vec3 iip = floor(5.0 * ip + 0.5) * 0.2;
    if (abs(iip.y - 2.0) > 2.0)
        return;

    // Check cube at given z.
    Cube cu;
    cube_compute(iip, cu);
    cube_intersect(cu, ray, isect);
    if (isect.t < t0)
        isect.p.x -= time;
}


Sphere sphere[4];
Cube cube[1];
Plane plane[2];

void Intersect(Ray r, inout Intersection i)
{
    sphere_intersect(sphere[0], r, i);
    sphere_intersect(sphere[1], r, i);
    sphere_intersect(sphere[2], r, i);
    sphere_intersect(sphere[3], r, i);

    cube_intersect(cube[0], r, i);

    plane_intersect(plane[0], r, i);

    cubes_intersect(0.0, r, i);
}

vec4 computeLightShadow(in Intersection isect)
{
    int i, j;
    int ntheta = 16;
    int nphi   = 16;
    float eps  = 0.001;

    // Slightly move ray org towards ray dir to avoid numerical problem.
    vec3 p = vec3(isect.p.x + eps * isect.n.x,
                  isect.p.y + eps * isect.n.y,
                  isect.p.z + eps * isect.n.z);
    vec4 result;

    Ray ray;
    ray.org = p;
    ray.dir = normalize(light - p);

    Intersection lisect;
    lisect.hit = 0;
    lisect.t = 1e30;
    lisect.n = lisect.p = vec3(0);
    lisect.col = vec4(0);
    Intersect(ray, lisect);
    if (lisect.hit != 0)
    {
        result = vec4(0.1, 0.1, 0.1, 10.0);
    }
    else
    {
        float shade = max(0.0, dot(isect.n, ray.dir));
        shade = pow(shade,2.0) + shade * 0.5;
        result = vec4(shade, shade, shade, 0);
    }

#if 0
	// Second light
    ray.dir = normalize(light2 - p);
    lisect.hit = 0;
    lisect.t = 1e30;
    lisect.n = lisect.p = vec3(0);
    lisect.col = vec4(0);
    Intersect(ray, lisect);
    if (lisect.hit != 0)
    {
        result += vec4(0.1, 0.1, 0.1, 10.0);
    }
    else
    {
        float shade = max(0.0, dot(isect.n, ray.dir));
        shade = pow(shade,2.0) + shade * 0.5;
        result += vec4(shade, shade, shade, 0);
    }
#endif

    return result;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 position = fragCoord.xy / resolution.xy - 0.5;
    position.x *= resolution.x / resolution.y;
	float angle = mouse.x * 0.1 - 30.1;
    float radius = 20.5 - 0.05 * mouse.y;

    float ss = sin(angle * todeg);
    float cc = cos(angle * todeg);
    vec3 org = vec3(ss*radius,2.0,cc*radius);
    vec3 dir = normalize(vec3(position.x*cc-ss,position.y, -position.x*ss-cc));

    vec3 center = vec3(-1.0, sin(0.1*time)*1.0+1.5, 2.75);
    sphere[0].c   = center;
    sphere[0].r   = 0.5;
    sphere[0].col = vec4(1,0.5,0.3, 0.5);

    const float sixtieth = 1.0 / 60.0;
    const float secminfac = 2.0 * 3.1415965358 * sixtieth;
    const float hourfac = secminfac * 5.0;

    float seca = seconds * secminfac;
    sphere[1].c   = center + 0.6 * vec3(sin(seca), cos(seca), 1.0);
    sphere[1].r   = 0.2;
    sphere[1].col = vec4(0.3,1,0.3, 2.5);

    float mina = minutes * secminfac;
    sphere[2].c   = center + 0.7 * vec3(sin(mina), cos(mina), 0.0);
    sphere[2].r   = 0.3;
    sphere[2].col = vec4(0.43,0.3,1, 0.0);

    float hrsa = hours * hourfac;
    sphere[3].c   = center + 0.5 * vec3(sin(hrsa), cos(hrsa), 0.5);
    sphere[3].r   = 0.2;
    sphere[3].col = vec4(0.8,0.0,0.0, 0.1);

    cube[0].c = vec3(-1.0, 0.5, 1.5);
    cube[0].h_side = vec3(0.5, 3.0, 1.0);
    cube[0].col = vec4(1.0, 0.8, 0.7, 3.1);

    plane[0].p = vec3(0,-0.5, 0);
    plane[0].n = vec3(0, 1.0, 0);
    plane[0].col1 = vec4(0.6,0.8,1, 0.0);
    plane[0].col2 = vec4(0.0,0.0,0.3, 0.5);

    plane[1].p = vec3(0,0, -2.5);
    plane[1].n = vec3(0.0, 0, 1.0);
    plane[1].col1 = vec4(0,0,0.3, 0.0);
    plane[1].col2 = vec4(0.0,0.3,0.0, 0.0);

    Ray r;
    r.org = org;
    r.dir = dir;
    vec4 col = vec4(0,0,0,1);
    float eps  = 0.0001;
    vec4 bcol = vec4(1,1,1,1);
    for (int j = 0; j < raytraceDepth; j++)
    {
        Intersection i;
        i.hit = 0;
        i.t = 1e30;
        i.n = i.p = vec3(0);
        i.col = vec4(0);

        Intersect(r, i);
        if (i.hit != 0)
        {
            vec4 shadow = computeLightShadow(i);
            col += bcol * i.col * shadow;
            bcol *= i.col / (1.0 + i.col.a * i.t + shadow.a);
        }
        else
        {
            if (j == 0)
                discard;
            break;
        }

        r.org = vec3(i.p.x + eps * i.n.x,
                     i.p.y + eps * i.n.y,
                     i.p.z + eps * i.n.z);
        r.dir = reflect(r.dir, vec3(i.n.x, i.n.y, i.n.z));
    }
    col.a = 1.0;
    fragColor = col;
}
