#define ID_NONE         -1.0
#define ID_RING_0        0.10
#define ID_RING_1        0.11
#define ID_RING_2        0.12
#define ID_RING_3        0.13

const vec4 RING_0     = vec4(5.0, 0.0, 0.0, ID_RING_0);
const vec4 RING_1     = vec4(5.1, 0.0, 0.0, ID_RING_1);
const vec4 RING_2     = vec4(5.2, 0.0, 0.0, ID_RING_2);
const vec4 RING_3     = vec4(5.3, 0.0, 0.0, ID_RING_3);

const vec3 lightPos = vec3(-15.0, 0.0, -15.0);
const vec3 lightColor = vec3(1.0, 1.0, 1.0);

float sdBox(in vec3 p, in vec3 box) {
    vec3 d = abs(p) - box;
    return min(max(d.x, max(d.y, d.z)), 0.0) + length(max(d, 0.0));
}

float sdSphere(vec3 p, float s) {
    return length(p) - s;
}

vec3 rotateY(in vec3 p, float an) {
    float c = cos(an);
    float s = sin(an);
    return vec3(c * p.x + s * p.z, p.y, -s * p.x + c * p.z);
}

vec3 rotateZ(in vec3 p, float an) {
    float c = cos(an);
    float s = sin(an);
    return vec3(c * p.x - s * p.y, s * p.x + c * p.y, p.z);
}

vec2 map(in vec3 p) {
    // hit object ID is stored in res.x, distance to object is in res.y

    float sz = 0.2;
    float time = iGlobalTime;
    float time2 = sin(iGlobalTime * 0.25) * 8.0;

    float d1 = sdBox(rotateZ(rotateY(p, 0.75 + time), 0.0 - time2), vec3(sz, RING_0.x, RING_0.x));
    float d2 = sdSphere(p, RING_0.x);
    vec2 res = vec2(ID_RING_0, max(-d2, max(d1, sdSphere(p, RING_0.x))));

    d1 = sdBox(rotateZ(rotateY(p, -0.75 + time), -0.75 - time2), vec3(sz, RING_1.x, RING_1.x));
    d2 = sdSphere(p, RING_1.x);
    vec2 obj = vec2(ID_RING_1, max(-d2, max(d1, sdSphere(p, RING_1.x))));
    if (obj.y < res.y) res = obj;

    d1 = sdBox(rotateZ(rotateY(p, 0.0 + time), -1.5 - time2), vec3(sz, RING_2.x, RING_2.x));
    d2 = sdSphere(p, RING_2.x);
    obj = vec2(ID_RING_2, max(-d2, max(d1, sdSphere(p, RING_2.x))));
    if (obj.y < res.y) res = obj;

    d1 = sdBox(rotateZ(rotateY(p, -0.75 + time), 0.75 - time2), vec3(sz, RING_3.x, RING_3.x));
    d2 = sdSphere(p, RING_3.x);
    obj = vec2(ID_RING_3, max(-d2, max(d1, sdSphere(p, RING_3.x))));
    if (obj.y < res.y) res = obj;
           
    return res;
}

vec2 raymarchScene(in vec3 ro, in vec3 rd, in float tmin, in float tmax) {
    vec3 res = vec3(ID_NONE);
    float t = tmin;
    for (int i = 0; i < 120; i++) {
        vec3 p = ro + rd * t;
        res = vec3(map(p), t);
        float d = res.y;
        if (d < (0.001 * t) || t > tmax)
            break;
        t += 0.75 * d;
     }
     return res.xz;
}

vec3 getNormal(in vec3 p) {
    vec2 eps = vec2(0.00015, 0.0);
    return normalize(vec3(map(p + eps.xyy).y - map(p - eps.xyy).y,
                          map(p + eps.yxy).y - map(p - eps.yxy).y,
                          map(p + eps.yyx).y - map(p - eps.yyx).y));
}

float raymarchAO(in vec3 ro, in vec3 rd, float tmin) {
    float ao = 0.0;
    for (float i = 0.0; i < 5.0; i++) {
        float t = tmin + pow(i / 5.0, 2.0);
        vec3 p = ro + rd * t;
        float d = map(p).y;
        ao += max(0.0, t - 0.5 * d - 0.05);
    }
    return 1.0 - 0.5 * ao;
}

float raymarchShadows(in vec3 ro, in vec3 rd, float tmin, float tmax) {
    float sh = 1.0;
    float t = tmin;
    for(int i = 0; i < 40; i++) {
        vec3 p = ro + rd * t;
        float d = map(p).y;
        sh = min(sh, 4.0 * d / t);
        t += 0.75 * d;
        if (d < (0.001 * t) || t > tmax)
            break;
    }
    return sh;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = (-iResolution.xy + 2.0 * fragCoord.xy) / iResolution.y;
    vec3 ro = vec3(0.0, 0.05, -20.0);
    vec3 rd = normalize(vec3(p.xy, 2.5));

    // background
    vec3 col = vec3(0.0);

    float tmin = 0.1;
    float tmax = 50.0;
    vec2 obj = raymarchScene(ro, rd, tmin, tmax);
    float id = obj.x;
    float t = obj.y;
    if (t < tmax) {
        vec3 pos = ro + rd * t;
        if (id == ID_RING_0) col = vec3(0.2);
        if (id == ID_RING_1) col = vec3(0.0, 0.15, 0.5);
        if (id == ID_RING_2) col = vec3(0.0, 0.5, 0.15);
        if (id == ID_RING_3) col = vec3(0.5, 0.075, 0.075);

        vec3 nor = getNormal(pos);
        float occ = clamp(raymarchAO(pos, nor, tmin), 0.0, 1.0);

        // point light
        vec3 lDir = normalize(lightPos - pos);
        float lDist = length(lightPos - pos);
        float dif = 0.1 + max(0.0, dot(nor, lDir));
        vec3 h = normalize(-rd + lDir);
        float spe = pow(clamp(dot(h, nor), 0.0, 1.0), 16.0);
        vec3 lightColor = dif * lightColor * (10.0 / lDist);
        lightColor += 5.0 * dif * spe * vec3(1.0, 1.0, 1.0);

        float sha = clamp(raymarchShadows(pos, lDir, tmin, tmax), 0.15, 1.0);
        col *= lightColor * sha * occ;
    }

    // gamma correction
    vec3 gamma = vec3(1.0 / 2.2);
    fragColor = vec4(pow(col, gamma), 1.0);
}