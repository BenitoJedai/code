#define PI	3.14159265359
#define PI2	( PI * 2.0 )

bool Flag = false;
int M;

mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

// https://www.shadertoy.com/view/MtsGWH
vec4 boxmap(sampler2D sam, in vec3 p, in vec3 n)
{
    vec3 m = pow(abs(n), vec3(8.0));
	vec4 x = texture2D(sam, p.yz);
	vec4 y = texture2D(sam, p.zx);
	vec4 z = texture2D(sam, p.xy);
	return (x*m.x + y*m.y + z*m.z)/(m.x+m.y+m.z);
}

float dot2(in vec3 v) {return dot(v,v);}
float udTriangle(in vec3 p, in vec3 a, in vec3 b, in vec3 c)
{
    vec3 ba = b - a; vec3 pa = p - a;
    vec3 cb = c - b; vec3 pb = p - b;
    vec3 ac = a - c; vec3 pc = p - c;
    vec3 nor = cross(ba, ac);
    return sqrt(
    (sign(dot(cross(ba,nor),pa)) +
     sign(dot(cross(cb,nor),pb)) +
     sign(dot(cross(ac,nor),pc))<2.0)
     ?
     min( min(
     dot2(ba*clamp(dot(ba,pa)/dot2(ba),0.0,1.0)-pa),
     dot2(cb*clamp(dot(cb,pb)/dot2(cb),0.0,1.0)-pb)),
     dot2(ac*clamp(dot(ac,pc)/dot2(ac),0.0,1.0)-pc))
     :
     dot(nor,pa)*dot(nor,pa)/dot2(nor) );
}

float map1(in vec3 p, in float t)
{   
    t = t * PI2 + iGlobalTime * 6.0;
    p.y +=  -0.2 * smoothstep(0.15, 0.0, length(abs(p.zx - vec2(0.4,0.0)) - vec2(0.0, 0.4))) * step(0.0, p.y);    
    p.z += -0.15 * smoothstep( 0.4, 0.3, length(p.yx)) * step(0.0, p.z);
    p.z +=  0.15 * smoothstep( 0.1, 0.0, length(abs(p.yx) - vec2(0.0, 0.15))) * step(0.0, p.z);
    p.z +=   0.1 * smoothstep( 0.1, 0.0, length(abs(p.yx - vec2(0.4,0.0)) - vec2(0.0, 0.3))) * step(0.0, p.z);
    p.z +=  0.15 * smoothstep( 0.1, 0.0, length(p.yx - vec2(0.35,0.0))) * (1.0 - step(0.0, p.z));
    p.y +=   0.3 * smoothstep( 0.2, 0.0, length((p.zx) - vec2(0.4  + 0.1 * sin(t),      0.4))) * (1.0 - step(0.0, p.y));
    p.y +=   0.3 * smoothstep( 0.2, 0.0, length((p.zx) - vec2(0.4  + 0.1 * sin(t+1.0), -0.4))) * (1.0 - step(0.0, p.y));
    p.y +=   0.3 * smoothstep( 0.2, 0.0, length((p.zx) - vec2(-0.4 + 0.1 * sin(t+2.0),  0.4))) * (1.0 - step(0.0, p.y));
    p.y +=   0.3 * smoothstep( 0.2, 0.0, length((p.zx) - vec2(-0.4+  0.1 * sin(t+3.0), -0.4))) * (1.0 - step(0.0, p.y));
    p.y += 0.05 * sin(t);
    return 0.5 * (length(p) - 1.0);	
}

float map2(in vec3 p)
{   
    return udTriangle(vec3(abs(p.x - 0.1), p.yz),
    	vec3( 0.7, 0.6, 1.0),
    	vec3(-0.1, 0.5, 1.0),
    	vec3( 0.4, 0.3, 1.0));
}

float map3(in vec3 p)
{   
	return dot(p, vec3(0.0, 1.0, 0.0)) + 1.15;
}

float map(in vec3 p)
{   
    float s = 5.0;
    vec3 q = p;
    vec2 f = floor(q.xz / s);
    vec4 rand = texture2D(iChannel1, f * 0.05);
    q.xz = mod(q.xz, s) - s / 2.0 + sin(rand.xy * PI2 + iGlobalTime * 0.3) * 0.5;
    q.xz *= rotate(-PI / 4.0);
    float de1 = map1(q, rand.z);
    float de2 = map2(q);
    float de3 = map3(p);
    if (Flag)
    {
        if (de1 > de3 && de2 > de3)
        {
            M = 3;
         } else if (de1 < de2){
            M = 1;
         } else {
            M = 2;   
         }
    }    
    return min(min(de1, de2), de3);
}

vec3 calcNormal(in vec3 p)
{
	const vec2 e = vec2(0.001, 0.0);
	return normalize(vec3(
		map(p + e.xyy) - map(p - e.xyy),
		map(p + e.yxy) - map(p - e.yxy),
		map(p + e.yyx) - map(p - e.yyx)));
}

float softshadow(in vec3 ro, in vec3 rd)
{
	float res = 1.0;
    float t = 0.02;
    for(int i = 0; i < 64; i++)
    {
		float h = map(ro + rd * t);
        res = min(res, 8.0 * h / t);
        t += clamp(h, 0.02, 0.1);
        if(h < 0.001 || t > 1.5) break;
    }
    return clamp(res, 0.0, 1.0);
}

float march(in vec3 ro, in vec3 rd)
{
	const float maxd = 50.0;
	const float precis = 0.02;
    float h = precis * 2.0;
    float t = 0.0;
	float res = -1.0;
    for(int i = 0; i < 128; i++)
    {
        if(h < precis || t > maxd) break;
	    h = map(ro + rd * t);
        t += h;
    }
    if(t < maxd) res = t;
    return res;
}

vec3 sky(in vec3 p)
{
    vec3 col = vec3(0.1, 0.15, 0.6) + 0.3 * pow(max(0.0, 1.0 - 3.0 * p.y), 2.0);
    float f = boxmap(iChannel2, p * vec3(0.6, 2.0, 0.6), p).x;
    f = smoothstep(0.3, 1.0, f);
    return mix(col, vec3(0.9), clamp (f * p.y * 5.0, 0.0, 1.0));      
}

vec3 material(in vec3 p)
{
    Flag = true; map(p); Flag = false;
    if (M == 1)
    {
        vec3 col = boxmap(iChannel1, p * 1.2, p).rgb;
        return mix(col, vec3(0.7, 0.4, 0.3), 0.8);
    }
    if (M == 2) 
    {
        return vec3(0.03);
    }
    if (M == 3) 
    {
     	vec3 col = texture2D(iChannel0, (p.xz * 0.3 + iGlobalTime) * rotate(PI / 4.0)).rgb;
        return mix(col, vec3(0.2, 0.7, 0.3), 0.6);
    }
    return vec3(0.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p2d = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
    vec3 ta = vec3(0.5);
    vec3 ro = vec3(5.0, 1.8, 2.0);
    float time = mod(iGlobalTime, 30.0);
    ro.xz *= rotate(-0.4 * PI * smoothstep(0.0, 15.0, time));
    ro.y  += 2.0 * smoothstep(5.0, 20.0, time);
    ro.xz += 18.0 * step(19.0, time);
    ro.xz += -25.0 * smoothstep(20.0, 28.0, time);
	vec3 cw = normalize(ta - ro);
	vec3 cp = vec3(0.0, 1.0, 0.0);
	vec3 cu = normalize(cross(cw, cp));
	vec3 cv = normalize(cross(cu, cw));
	vec3 rd = normalize(p2d.x * cu + p2d.y * cv + 2.5 * cw);
    vec3 lig = normalize(vec3(0.5, 0.8, 3.0));  
    vec3 col = sky(rd);
    float t = march(ro, rd);
    if(t > -0.01)
    {
        vec3 p3d = ro + t * rd;
        vec3 n = calcNormal(p3d);
		float dif = clamp((dot(n, lig) + 0.5) * 0.7, 0.2, 1.0);
        dif *= clamp(softshadow(p3d, lig), 0.2, 1.0);
        col = material(p3d) * dif;
		col *= exp(-0.0005 * p3d.z * p3d.z);
        col = pow(col, vec3(0.5));
	}
   	fragColor = vec4(col, 1.0);
}
