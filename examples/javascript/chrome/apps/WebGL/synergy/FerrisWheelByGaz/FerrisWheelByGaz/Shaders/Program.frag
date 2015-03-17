#define PI	3.14159265359
#define PI2	PI * 2.0
    
bool Flag = false;
int material;


mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

vec3 hsv(float h, float s, float v)
{
  return mix( vec3( 1.0 ), clamp( ( abs( fract(
    h + vec3( 3.0, 2.0, 1.0 ) / 3.0 ) * 6.0 - 3.0 ) - 1.0 ), 0.0, 1.0 ), s ) * v;
}
    
float udBox( vec3 p, vec3 b )
{
  return length(max(abs(p)-b,0.0));
}    
    
float map1(in vec3 p, in float a)
{
    p.xy *= rotate(-a);
    p.xy *= rotate(-iGlobalTime * 0.5);
    p.y += 0.15;
    return  0.5 * udBox(p, vec3(0.2, 0.15, 0.2));    
}    
    
float map2(in vec3 p)
{
    p.z = abs(p.z) - 0.25;
    return max(p.y, length(p.xz) - 0.05);
}    
    
float map(in vec3 p)
{
    p.xy *= rotate(iGlobalTime*0.5);
    // https://www.shadertoy.com/view/Mlf3Wj
    float sep = 6.0;
    float a = PI / sep - atan(p.x, p.y);
    float n = PI2 / sep;
    a = floor(a / n) * n;
    p.xy *= rotate(a);   
    p.y -= 1.0;
    float de1 = map1(p, a);
    float de2 = map2(p);    
    if (Flag)
    {
        if (de1 < de2)
        {
             material = 1;
        } else {
            material = 2;
        }
    }        
    return min(de1, de2);    
}

vec3 calcNormal(in vec3 p)
{
	const vec2 e = vec2(0.0001, 0.0);
	return normalize(vec3(
		map(p + e.xyy) - map(p - e.xyy),
		map(p + e.yxy) - map(p - e.yxy),
		map(p + e.yyx) - map(p - e.yyx)));
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

vec3 doColor(in vec3 p)
{
    Flag = true; map(p); Flag = false;
    if (material == 1) 
    {
        return hsv(0.05, 0.6, 1.0 );
    }
    return hsv(0.5, 0.3, 0.7 );
}


vec3 transform(in vec3 p)
{
    p.zx *= rotate(iGlobalTime * 0.1 + 0.2);
    return p;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
	vec3 col = vec3(0.3 + p.y * 0.15);
    vec3 rd = normalize(vec3(p, -1.8));
	vec3 ro = vec3(0.0, 0.0, 3.0);
    vec3 li = normalize(vec3(0.5, 0.8, 3.0));
    ro = transform(ro);
	rd = transform(rd);
	li = transform(li);
    float t = march(ro, rd);
    if(t > -0.001)
    {
        vec3 pos = ro + t * rd;
        vec3 n = calcNormal(pos);
		float dif = clamp((dot(n, li) + 0.5) * 0.7, 0.3, 1.0);
        col = doColor(pos) * dif;
	}
   	fragColor = vec4(col, 1.0);
}
