#define PI	3.14159265359
#define PI2	( PI * 2.0 )

bool Flag = false;
int M;

mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

vec3 hsv(float h, float s, float v)
{
  return mix( vec3( 1.0 ), clamp( ( abs( fract(
    h + vec3( 3.0, 2.0, 1.0 ) / 3.0 ) * 6.0 - 3.0 ) - 1.0 ), 0.0, 1.0 ), s ) * v;
}

float map1(in vec3 p)
{     
    p.yz *= rotate(iGlobalTime * -0.543);
    p.zx *= rotate(iGlobalTime * 0.876);
    // folding -- https://www.shadertoy.com/view/XlX3zB
    float cospin = cos(PI / 5.0), scospin = sqrt(0.75 - cospin * cospin);
    vec3 nc = vec3(-0.5, -cospin ,scospin);
    for(int i = 0; i < 5; i++)
    {
		p.xy = abs(p.xy);
		p -= 2.0 * min(0.0, dot(p, nc)) * nc;
	}
    float time = mod(iGlobalTime, 12.0);
    p.z -=  0.4;
    p.z -=  6.0 * smoothstep(1.0,  3.0, time);
    p.z -= -5.7 * smoothstep(6.0,  9.0, time);
    p.z -= -0.3 * smoothstep(10.0, 12.0, time);
    return abs(p.x) +abs(p.y) + abs(p.z)-0.25;
}

float map2(in vec3 p)
{     
    p.xz = vec2(length(p) - 1.7, sin(atan(p.z, p.x) * 2.5) * 0.6);
    return length(p) - 0.08;
 }

float map3(in vec3 p)
{     
    vec3 q = p;
	p.xz = vec2(length(p) - 1.7, sin(atan(p.z, p.x) * 2.5) * 0.5);
    float de1 = 0.8 * (length(p.xz) - 0.01);
    float de2 = max(p.x-2.0,length(p.yz)-0.01);
    p.y = sin(atan(q.y, length(q.zx)) * 7.0) * 0.3;
    float de3 = 0.7 * (length(p.xy) - 0.01);
    return min(min(de1,de2),de3);
}

float map(in vec3 p)
{
    float de1 = 0.6 * map1(p);
	p.xz *= rotate(iGlobalTime * 0.234);
	p.yz *= rotate(iGlobalTime * 0.123); 
    float de2 = 0.5 * map2(p);
    float de3 = map3(p);
    if (Flag)
    {
        if (de1 < de2 && de1 < de3)
        {
            M = 1;
         } else if (de2 < de3){
            M = 2;
         } else{
            M = 3;   
         }
    }    
    return min(min(de1,de2),de3);
}

vec3 calcColor(in vec3 p)
{
    Flag = true; map(p); Flag = false;
    if (M == 1)
    {
        return hsv(atan(p.z, p.x) / PI2 - 0.02 * iGlobalTime, 0.8, 1.0);
    }
    if (M == 2) 
    {
        return hsv(0.18*floor(0.5 * iGlobalTime), 1.0, 1.0 );
    }
    if (M == 3) 
    {       
    	float t = smoothstep(0.2, 7.5, mod(iGlobalTime, 12.0));
        return mix(vec3(1.0, 0.1, 0.1), vec3(0.2, 0.5, 0.3), t);
    }
    return vec3(1.0);
}

vec3 calcNormal(in vec3 p)
{
	const vec2 e = vec2(0.0001, 0.0);
	return normalize(vec3(
		map(p + e.xyy) - map(p - e.xyy),
		map(p + e.yxy) - map(p - e.yxy),
		map(p + e.yyx) - map(p - e.yyx)));
}

float softshadow(in vec3 ro, in vec3 rd)
{
	float res = 1.0;
    float t = 0.05;
    for(int i = 0; i < 32; i++)
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
	const float maxd = 10.0;
	const float precis = 0.001;
    float h = precis * 2.0;
    float t = 0.0;
	float res = -1.0;
    for(int i = 0; i < 64; i++)
    {
        if(h < precis || t > maxd) break;
	    h = map(ro + rd * t);
        t += h;
    }
    if(t < maxd) res = t;
    return res;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p2d = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
	vec3 col = vec3(0.2, 0.3, 0.7) * ((p2d.y + 1.0) * 0.3);
    col = mix(col, texture2D(iChannel0, p2d * 0.02 - iGlobalTime * 0.0005).xyz, 0.3);
   	vec3 rd = normalize(vec3(p2d, -1.8));
	vec3 ro = vec3(0.0, 0.0, 3.5);
    vec3 li = normalize(vec3(0.5, 0.8, 3.0));
    float t = march(ro, rd);
    if(t > -0.001)
    {
        vec3 p3d = ro + t * rd;
        vec3 n = calcNormal(p3d);
		float dif = clamp((dot(n, li) + 0.5) * 0.7, 0.4, 1.0);
        dif *= clamp(softshadow(p3d, li), 0.4, 1.0);
        col = calcColor(p3d) * dif;
    }
    col = pow(col, vec3(0.8));
   	fragColor = vec4(col, 1.0);
}
