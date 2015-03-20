// credits go to iq and his awesome articles about distance maps:
// http://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm
// http://www.iquilezles.org/www/articles/rmshadows/rmshadows.htm

// --- GLOBALS ---

const float e = 2.71828182845904523536028747135266249;

// sun
const vec3 sun = vec3(-0.2, 1.0, 0.5);

const vec3 amb = vec3(0.01);
const vec3 bg = vec3(0.01, 0.01, 0.014);

// --- FUNCTIONS ---

// http://www.java-gaming.org/index.php?topic=28018.0
float Rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

vec2 Map(in vec3 p) {
    float id = 0.0;
    float d = 1000.0;
    
    // sphere
    float s = length(p - vec3(0.0, 0.5, 0.0)) - 0.6;
    // block
    float b = length(max(abs(p - vec3(0.0, 0.5, 0.0)) - vec3(0.4), 0.0)) - 0.1;
    
    // main shape
    float m = max(b, -s);
    if (m < d) {
        d = m;
        id = 1.0;
    }
    
    // sphere 2
    float x = length(p - vec3(0.0 + pow(sin(iGlobalTime * 0.6), 3.0), 0.5, 0.0 + pow(sin(iGlobalTime * 0.6 + 0.7853), 3.0))) - 0.3
        + 0.01 * sin(p.x * 50.0) * sin(p.y * 50.0) * sin(p.z * 50.0);
    if (x < d) {
        d = x;
        id = 3.0;
    }
    
    // ground
    float g = p.y;
    if (g < d) {
        d = g;
        id = 2.0;
    }
    
    return vec2(d, id);
}

vec3 MapNormal(in vec3 p) {
    vec3 e = vec3(0.0001, 0.0, 0.0);
    vec3 n;
    
    n.x = Map(p + e.xyy).x - Map(p - e.xyy).x;
    n.y = Map(p + e.yxy).x - Map(p - e.yxy).x;
    n.z = Map(p + e.yyx).x - Map(p - e.yyx).x;
    
    return normalize(n);
}

vec2 Shoot(in vec3 ro, in vec3 rd) {
    float t = 0.2;
    for (int i = 0; i < 256; ++i) {
        // map
        vec2 m = Map(ro + rd * t);
        if (m.x < 0.0001)
            // we hit something
            return vec2(t, m.y);
        t += m.x;
    }
    return vec2(-1.0, 0.0);
}

float Shadow(in vec3 p, in vec3 n, in vec2 uv) {
    vec3 ro = p + n * 0.001;
    vec3 rd = sun;
    
    float res = 1.0;
    float t = Rand(uv) * 0.04 + 0.002;
    
    for (int i = 0; i < 64; ++i) {
        // map
        float m = Map(ro + rd * t).x;
        res = min( res, 10.0 * m / t );
        if(res < 0.0001)
            break;
        t += clamp(m, 0.01, 0.02);
    }
    return clamp(res, 0.0, 1.0);
}

vec3 Draw(in vec3 ro, in vec3 rd, in vec2 uv) {
    // background
    vec3 c = bg;
    
    // intersect the ray with geometry
    vec2 res = Shoot(ro, rd);
    
    if (res.y > 0.5) {
        // hit point
        vec3 p = ro + rd * res.x;
        
        // normal
        vec3 n = MapNormal(p);
        
        vec3 r = reflect(sun, n);
        float sp = 0.0;
        float s = 1.0;
        
        if (res.y < 1.5) {
            // main shape
            c = vec3(1.0, 0.2, 0.2);
            sp = 0.8;
            s = 4.0;
        }
        
        else if (res.y < 2.5) {
            // ground
            c = vec3(0.4);
        }
        
        else if (res.y < 3.5) {
            // sphere 2
            c = vec3(0.1, 0.3, 1.0);
            sp = 1.0;
            s = 20.0;
        }
        
        // light
        float dif = max(dot(n, sun), 0.0);
        float shd = 0.0;
        if (dif > 0.0)
            shd = Shadow(p, n, uv);
        float spe = sp * max(pow(max(dot(r, rd), 0.0), s), 0.0) * dif;
        
        c = clamp((spe + dif * c) * shd + amb * c * smoothstep(-1.5, 1.0, n.y), 0.0, 1.0);
        
        // fog
        float f = pow(e, -pow(res.x*0.3, 2.0));
        c = mix(bg, c, f);
    }
    
    return c;
}

// --- MAIN ---

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    // uv coords
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 uv2 = (uv * 2.0 - 1.0) * vec2(iResolution.x / iResolution.y, 1.0);
    
    // create a ray
    float d = 2.0;
    vec3 ro = vec3(cos(iGlobalTime * 0.1) * d, 1.1, sin(iGlobalTime * 0.1) * d);
    vec3 ta = vec3(0.0, 0.5, 0.0);
    
    vec3 ww = normalize(ta - ro);
    vec3 uu = normalize(cross(ww, vec3(0.0, 1.0, 0.0)));
    vec3 vv = normalize(cross(uu, ww));
    
    vec3 rd = normalize(uv2.x * uu + uv2.y * vv + 2.0 * ww);
    
    // get color
    vec3 color = Draw(ro, rd, uv);
    
    // gamma
    color = sqrt(color);
    
    // output
	fragColor = vec4(color, 1.0);
}