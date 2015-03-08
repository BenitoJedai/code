#define PI	3.14159265359
#define PI2	( PI * 2.0 )

bool cflag = false;
int id;

mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

vec3 hsv(float h, float s, float v)
{
  return mix(vec3(1.0), clamp((abs(fract(
    h + vec3(3.0, 2.0, 1.0) / 3.0) * 6.0 - 3.0) - 1.0), 0.0,1.0), s) * v;
}
 
float sstep(in float a, in float b, in float c)
{
	if (a > c) return 0.0;
    if (b < c) return 1.0;
    return (c-a)/(b-a);
}

float map0(in vec3 p) // ボディー
{   
    
    // postion
    p.y -= -0.3;
   	p.z -= 1.2;
    
    //de
    p.x *= 0.8;
    float a;
    a =  atan(-p.y, p.z);
    //p.yz *=  rotate(a);
    p.yz *= (abs(cos(0.5 * a)) + 0.08);
    a = atan(-p.x, p.y);
	p.xy *=rotate(a);
	return length(p) - 0.25;
    
}

float map1(in vec3 p) // エンジン
{   
    // postion
    p.y -= 0.82;
    p.z -= 0.05;
    // de
    return length(max(abs(p) - vec3(0.15,0.15,0.25), 0.0)) - 0.1;
}

float map2(in vec3 p) //プロペラ
{   
    // position
    p.y -= 0.8;
    p.z -= 0.5;
    // de
    p.xy *= rotate(iGlobalTime);
    vec3 q = p;
    p.x *= 0.2;
    p.x = abs(p.x)-0.05;
    p.yz *= rotate(sign(q.x)*0.5);
    q.z -= -0.02;
    q.z *= 0.6;
    return min(
        max(abs(p.z)-0.01,length(p.xy) -0.05),
        max(-q.z,length(q)-0.08));
}

float map3(in vec3 p) // 主翼
{   
    // postion
    p.y -= 0.3;
    // de
    p.y *= 2.0;
    p.z += 0.2 * abs(p.x);
    p.z *= 0.4;
    float r = 0.15;
    p.x = abs(p.x) -2.0;
    return min(length(p) - r, max(p.x, length(p.zy)- r));
}

float map4(in vec3 p) // フロート
{   
    // postion
    p.y -= -0.4;
    p.z -= -0.1;
    p.x = abs(p.x) - 1.5;
    // de
    p.x *= 0.8;
    float a =  atan(-p.y, p.z);
    p.yz *= (abs(cos(0.5 * a)) + 0.3);
	return length(p) - 0.2;
}

float map5(in vec3 p) // 垂直尾翼
{   
   // return 100.;
    // postion
    p.y -= -0.3;
    p.z -= -1.8;
    // de
    p *= 2.0;
    p.z += 0.2 * p.y;
    p.y *= 0.1;
    p.z *= 0.3;
    return max(abs(p.y-0.06)-0.06, length(p)-0.1);
}


float map6(in vec3 p) // 水平尾翼
{   
    //return 100.;
    // postin
    p.y -=- 0.2;
    p.z -= -1.8;
    // de
    p.z += 0.2 * abs(p.x);
    p.z *= 0.6;
    float r = 0.1;
    p.x = abs(p.x) - 0.3;
    return min(length(p) - r, max(p.x, length(p.zy)- r));
}

float map7(in vec3 p) // エンジンステー
{   
    // position
    p.y -= 0.3;
    p.x = abs(p.x) - 0.4;
    // de
    p.x += 0.6*abs(p.y);
    vec3 q = p;
    p.z += -0.2*p.y;
	p.z = abs(p.z) - 0.2;
    q.z -= -0.2;
    q.z += -1.0*abs(q.y);
	return min(
        max(abs(p.y)-0.5,length(p.zx)-0.05),
        max(abs(q.y)-0.5,length(q.zx)-0.03));
}

float map8(in vec3 p) // フロートステー
{   
    // postion
    p.y -= -0.4;
    p.z -= -0.3;
    p.x = abs(p.x) - 1.5;
    // de
	p.z += -0.1*p.y;
	p.z = abs(p.z) - 0.2;
    p.z += -0.1*p.y;
    vec3 q = p;
    q.x +=  0.6*q.y;
	return min(
        max(abs(p.y-0.3)-0.3,length(p.zx)-0.05),
        max(abs(q.y-0.3)-0.3,length(q.zx)-0.03));
}

float map(in vec3 p)
{   
    
    //return 100.0;
    p *= 0.7; //　I spent 2 months to find this line
    // bounding box
    vec3 bb = vec3(2.8,1.5,2.5);
    if (any(greaterThan(abs(p), bb))) return length(max(abs(p) - bb, 0.0)) + 0.2;
    const int n = 9;
    float res = 100.0;
    float de[n];
    de[0] = map0(p);
    de[1] = map1(p);
    de[2] = map2(p);
    de[3] = map3(p);
    de[4] = map4(p);
    de[5] = map5(p);
    de[6] = map6(p);
    de[7] = map7(p);
    de[8] = map8(p);
    for(int i = 0; i < n; i++) res = min(res, de[i]);
    if (cflag)
    {
		id = n - 1;        
   		for(int i = 0; i < n - 1; i++)
    	{
            bool f = true;
            for(int j = 1; j < n; j++)
            {
                if (de[i] > de[j])  f = false;
            }
            if (f) {id = i; break;}
        }	     
    }    
    return res;
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
	const float maxd = 30.0;
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

vec3 sky(in vec3 p)
{
    vec3 col = hsv(0.4, 0.8, 0.2);
    col = mix(col, hsv(0.6, 0.3, 0.15), smoothstep(0.0, 2.0, length(p)));
    return col;
}

vec3 coloring(in vec3 p)
{
    cflag = true; map(p); cflag = false;
    vec3 col0 = hsv(0.0, 0.9, 0.8);
    if (id == 0) 
    {
        return col0;
    }
    if (id == 1)
    {
        return col0;
    }
    if (id == 2) 
    {
        return hsv(0.2, 0.8, 0.6);
    }
    if (id == 3) 
    {
        return col0;
    }
    if (id == 4) 
    {
        return col0;
    }
    if (id == 5) 
    {
        return col0;
    }
    if (id == 6) 
    {
        return col0;
    }
    if (id == 7) 
    {
        return col0;
    }
    if (id == 8) 
    {
        return col0;
    }
    return vec3(0.0);
}

mat3 lookat(in vec3 fw, in vec3 up)
{
	fw = normalize(fw);
    vec3 rt = normalize(cross(fw, up));
    return mat3(rt, cross(rt, fw), fw);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p2d = (2.0 * fragCoord - iResolution.xy) / iResolution.y;   
    float time = 0.3 * iGlobalTime;
    float a = 0.0;
    float h = -0.3;
    float d = 6.0;
    a = (a + 0.5)*PI;
    //vec3 ro = vec3(cos(a), h, sin(a))* d;
	vec3 ro = vec3(cos(time), 0.6, sin(time))* 6.0;
    vec3 rd = normalize(vec3(p2d, 2.0));
	rd = lookat(-ro, vec3(0.0, 1.0, 0.0)) * rd;
    vec3 lig = normalize(ro + vec3(0.5, 0.8, 3.0)); 
    vec3 col = sky(vec3(p2d, 0.0));
    float t = march(ro, rd);
    if(t > -0.01)
    {
        vec3 p3d = ro + t * rd;
        vec3 n = calcNormal(p3d);
		float dif = clamp((dot(n, lig) + 0.5) * 0.7, 0.2, 1.0);
        dif *= clamp(softshadow(p3d, lig), 0.2, 1.0);
        col = coloring(p3d) * dif;
		col *= exp(-0.0005 * p3d.z * p3d.z);
        col = pow(col, vec3(0.8));
	}
   	fragColor = vec4(col, 1.0);
}
