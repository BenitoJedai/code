#define PI	3.14159265359
#define PI2	PI * 2.0


mat2 rotate(in float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

float hash(in vec3 p){
	p  = fract(p * vec3(.16532,.17369,.15787));
    p += dot(p.xyz, p.yzx + 19.19);
    return fract(p.x * p.y * p.z);
}

float smin(in float a, in float b, in float k)
{   
    float res = exp(-k * a) + exp(-k * b);
    return -log(res) / k;   
}

vec2 TE1(in vec2 p)
{
	p.y -= 1.5 * pow(p.x, 2.0) + 0.2;
    return p;
}

vec2 TE2(in vec2 p)
{
    vec2 q = p;
    p.x = abs(p.x) - 0.3; 	
 	p.x += 0.4 * p.y;
    p.x = abs(p.x - 0.5) - 0.25; 	
	p.x += 0.4 * p.y;
	p.x = abs(p.x); 	
    p.y += clamp(3.0 * pow(p.x - 0.2, 2.0), 0.0, 0.5) + 0.1;
    p.y -= abs(0.2 * q.x);
    return p;
}

vec2 TE3(in vec2 p)
{
    p.x =abs(p.x);
    p.x += 0.8 * pow(p.y + 0.2, 2.0);
    p.x += 0.7 * p.y;
    p.x -= 1.6;
    return p;
}

vec2 TE4(in vec2 p, in float t)
{
    t = t * PI2 + iGlobalTime * 2.5;
    p.x -= 0.2 * sin(t) * p.y * p.y;
    float a = PI / 2.0 + 0.4 * sin(t);
    p *= rotate(-a + PI / 2.0);
    vec2 v = vec2(cos(a), sin(a));
    p -= 2.0 * min(0.0, dot(p, v)) * v;
    return p;    
}

float DEBat(in vec3 p, in float t)
{
    vec2 te1 = TE1(p.xy);
    vec2 te2 = TE2(p.xy);
    vec2 te3 = TE3(p.xy);
    p.zx = TE4(p.zx, t);
	float de1 = max(abs(p.z) - 0.005, max(te1.y, max(-te2.y, min(1.0, te3.x))));
    float de2 = min(min(
       max(te3.x, length(vec2(te1.y, p.z)) - 0.01),
       max(-te2.y, length(vec2(te2.x, p.z)) - 0.01)),
       max(te1.y, max(-te2.y, length(vec2(te3.x, p.z)) - 0.01)));   
    p.y *= 0.7 * pow(p.y, 0.7);
    p.y -= 0.1;
    p.y -=  0.15 * smoothstep(0.1, 0.0, length(abs(p.zx) - vec2(0.0, 0.15))) * step(0.0, p.y);       
    float de3 = length(p) - 0.12;
    return smin(smin(de1, de2, 30.0), de3, 30.0);
}

float DE(in vec3 p)
{   
    p *= 0.8;
    p.yz *= rotate(iGlobalTime * 0.3123);
    p.zx *= rotate(iGlobalTime * 0.5123);
    p.x += 0.5 * sin(iGlobalTime);
    // Sparse grid (https://www.shadertoy.com/view/XlfGDs)
    const float c = 4.5;
	vec3 ip = floor(p / c);
    p = mod(p, c) - c / 2.0;
    float rnd = hash(ip);
   	float de = 1.0;
    if (length(ip) - 3.0 < 0.0)
    if (rnd > 0.6)
    {
        p.yz *= rotate(PI * 0.4);
        de = min(de, DEBat(p, rnd));
    }
    return de;
}

vec3 calcNormal(in vec3 p)
{
	const vec2 e = vec2(0.0001, 0.0);
	return normalize(vec3(
		DE(p + e.xyy) - DE(p - e.xyy),
		DE(p + e.yxy) - DE(p - e.yxy),
		DE(p + e.yyx) - DE(p - e.yyx)));
}

float march(in vec3 ro, in vec3 rd)
{
	const float maxd = 32.0;
	const float precis = 0.005;
    float h = precis * 2.0;
    float t = 0.0;
	float res = -1.0;
    for(int i = 0; i < 64; i++)
    {
        if(h < precis || t > maxd) break;
	    h = DE(ro + rd * t);
        t += h;
    }
    if(t < maxd) res = t;
    return res;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p2 = (fragCoord.xy * 2.0 - iResolution.xy) / iResolution.y;
    vec3 col = 0.6 * length(p2) * texture2D(iChannel0, vec2(length(p2), atan(p2.y, p2.x) / PI2) * 2.0).rgb;
  	vec3 rd = normalize(vec3(p2, -1.5));
	vec3 ro = vec3(0.0, 0.0, 10.0);
    vec3 li = normalize(vec3(0.5, 0.8, 3.0));
    float t = march(ro, rd);
    if(t > -0.001)
    {
        vec3 p3 = ro + t * rd;
        vec3 n = calcNormal(p3);
		float dif = clamp((dot(n, li) + 0.5) * 0.7, 0.3, 1.0);
        col = vec3(0.15) * dif;
 	}
    fragColor = vec4(col, 1.0);
}
