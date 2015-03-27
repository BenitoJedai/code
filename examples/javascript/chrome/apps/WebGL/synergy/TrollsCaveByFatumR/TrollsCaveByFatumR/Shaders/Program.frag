#define OCTAVES  8
#define HIGH_QUALITY (0)
#define DARK_FORCE (0)

#if HIGH_QUALITY
#define MAX_STEPS  128
#else
#define MAX_STEPS  64
#endif
#define THRESHOLD .0001


const float fogDensity = .15;


float rand(in vec2 co){
   return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float rand2(in vec2 co){
   return fract(cos(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float rand3(in vec3 co){
   return fract(sin(dot(co.xyz ,vec3(12.9898,78.233,213.576))) * 43758.5453);
}

float valueNoiseSimple3D(in vec3 vl) {
    const vec2 helper = vec2(0., 1.);
    vec3 grid = floor(vl);
    vec3 interp = smoothstep(vec3(0.), vec3(1.), fract(vl));
    
    float interpY0 = mix(mix(rand3(grid),
                         	 rand3(grid + helper.yxx),
                         	 interp.x),
                        mix(rand3(grid + helper.xyx),
                         	rand3(grid + helper.yyx),
                         	interp.x),
                        interp.y);
    
    
    float interpY1 = mix(mix(rand3(grid + helper.xxy),
                         	 rand3(grid + helper.yxy),
                         	interp.x),
                        mix(rand3(grid + helper.xyy),
                         	rand3(grid + helper.yyy),
                         	interp.x),
                        interp.y);
    
    return -1. + 2.*mix(interpY0, interpY1, interp.z);
}

float fractalNoise(in vec3 vl) {
    const float persistance = 2.;
    const float persistanceA = 2.;
    float amplitude = .5;
    float rez = 0.0;
    float rez2 = 0.0;
    vec3 p = vl;
    
    for (int i = 0; i < OCTAVES / 2; i++) {
        rez += amplitude * valueNoiseSimple3D(p);
        amplitude /= persistanceA;
        p *= persistance;
    }
    
    float h = smoothstep(0., 1., vl.y*.5 + .5 );
    if (h > 0.01) { // small optimization, since Hermit polynom has low front at the start
        // God is in the details
        for (int i = OCTAVES / 2; i < OCTAVES; i++) {
            rez2 += amplitude * valueNoiseSimple3D(p);
            amplitude /= persistanceA;
            p *= persistance;
        }
        rez += mix(0., rez2, h);
    }
    
    return rez;
}

vec2 helix(vec3 p, float r, float width) {
    return vec2(r*cos(p.z/ width), r*sin(p.z/ width));
}

float scene(in vec3 a) {
   
   float zVal = fractalNoise(a.xyz);

   return -length(a.xy - helix(a, .25, 1.5)) + 1.
#if HIGH_QUALITY
       + zVal * 1.5;
#else
       + zVal * 1.1;
#endif

}

vec3 snormal(in vec3 a) {
   const vec2 e = vec2(.0001, 0.);
   float w = scene(a);

   return normalize(vec3(
       scene(a+e.xyy) - w,
       scene(a+e.yxy) - w,
       scene(a+e.yyx) - w ));

}

float trace(in vec3 O, in vec3 D, out float hill) {
    float L = 0.;
    float d = 0.;
    
#if DARK_FORCE
    L = 2.5;
#endif
    
    for (int i = 0; i < MAX_STEPS; ++i) {
#if HIGH_QUALITY
        d = scene(O + D*L) * .25;
#else      
        d = scene(O + D*L) * .45;
#endif        
        L += d;
        if (d < THRESHOLD*L)
            break;
    }

    hill = d;
    return L;
}

float occluded(in vec3 p, in float len, in vec3 dir) {
    return max(0., len - scene(p + len * dir));
}

float occlusion(in vec3 p, in vec3 normal) {
    vec3 rotZccw = vec3(-normal.y, normal.x, normal.z);
    vec3 rotZcw = vec3(normal.y, -normal.x, normal.z);
    
    vec3 rotXccw = vec3(normal.x, normal.z, -normal.y);
    vec3 rotXcw = vec3(normal.x, -normal.z, normal.y);
    
    vec3 rotYccw = vec3(normal.z, normal.y, -normal.x);
    vec3 rotYcw = vec3(-normal.z, normal.y, normal.x);
    
    float rez = 0.;
    const float dst = .15;

   	rez+= occluded(p, dst, normal);
    
    rez+= occluded(p, dst, rotXccw);
    rez+= occluded(p, dst, rotXcw);

    rez+= occluded(p, dst, rotYccw);
    rez+= occluded(p, dst, rotYcw);

    rez+= occluded(p, dst, rotZccw);
    rez+= occluded(p, dst, rotZcw);

    return (1. - min(rez, 1.));
}

vec3 enlight(in vec3 p, in vec3 normal, in vec3 eye, in vec3 lightPos) {
    
    float normDir = .5 * dot(normal, vec3(0., 1., 0.));
    float down = .5 - normDir;
    float up = normDir + .5;
    
    vec3 dir = lightPos - p;
    vec3 eyeDir = eye - p;
    vec3 I = normalize(dir);

    float mxB = valueNoiseSimple3D( (p.zxy - sin(p.z) * 0.1)*20. + valueNoiseSimple3D(
        													p * 55. +
        													valueNoiseSimple3D(
                                                                p*500. +
                                                                valueNoiseSimple3D(
                                                                    p*1000.)
                                                            )
    													));
    
    /*
    float mxB = valueNoiseSimple3D( p.zxy*20. + valueNoiseSimple3D(
        													p * 55. +
        													valueNoiseSimple3D(
                                                                p*500. +
                                                                valueNoiseSimple3D(p*1000.)
                                                            )
    													));
    */
    mxB = abs(mxB);
    
    vec3 grass = mix(vec3(0.258823529, 0.317647059, 0.062745098),
                     vec3(0.835294118, 0.898039216, 0.611764706),
                     mxB
    );
    
    vec3 cave = vec3(0.850980392,
                     0.592156863,
                     0.294117647
    );
    
    vec3 cave2 = vec3(0.505882353,
                      0.31372549,
                      0.156862745
                     );
    
    float val1 = (p.x + p.y* 3. + p.z) * 100. +
                                  2. * valueNoiseSimple3D(p * 80.);
    float val2 = 1.5 * valueNoiseSimple3D(p * 160.);
    float val3 = .75 * valueNoiseSimple3D(p * 320.);
    
   
    vec3 ceilClr = mix(cave2, cave, abs(sin(val1 + val2 + val3)));
    vec3 color = mix(vec3(.0), ceilClr, down);
    color = mix(cave, color, .5 + abs(normDir));
    color = mix(color, grass, up);
    
    vec3 ambient = color;
    vec3 diffuse = max(dot(normal, I), 0.) * color.rgb;

    diffuse = clamp(diffuse, 0., 1.) * 0.75;

    return diffuse + ambient * occlusion(p, normal);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 centered_uv = uv * 2. - 1.;
    centered_uv.x *= iResolution.x / iResolution.y;

    float timeOffset = iGlobalTime / 2.;
    
    vec3 O = vec3(0., -0.2, 1. - timeOffset);
    O.xy += helix(O, .25, 1.5);
    
    vec3 D = normalize(vec3(centered_uv, -3.0));

    float hill;
    float path = trace(O, D, hill);
    vec3 coord = O + path * D;
    
    vec3 resColor;
    vec3 skyBlueColor = vec3(0.529411765, 0.807843137, 0.980392157);
    vec3 bgColor = mix(vec3(1.), skyBlueColor, clamp(centered_uv.y, 0., 1.));

    vec3 lightPos = vec3(0., 0., 1. - timeOffset);
    lightPos.xy += O.xy;
    vec3 normal = snormal(coord);

    resColor = enlight(coord, normal, O, lightPos);
    resColor = mix(resColor, bgColor, min(hill, 1.));

    // Calc some fog
    float fogFactor = exp(-pow(abs(fogDensity * (coord.z - .75 + timeOffset)), 6.0));
    fogFactor = clamp(fogFactor, 0.0, 1.0);

    resColor = mix(bgColor, resColor, fogFactor);

	fragColor = vec4(resColor,1.0);
}