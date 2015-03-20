#define PI	3.14159265359
#define PI2	( PI * 2.0 )

mat2 rotate(in float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

float cross(in vec2 a, in vec2 b ) 
{
    return a.x * b.y - b.x * a.y;
}

// from https://www.shadertoy.com/view/XsX3zf
float deBezier(in vec2 p, in vec2 b0, in vec2 b1, in vec2 b2) 
{
  b0 -= p; b1 -= p; b2 -= p;	
  float a=cross(b0,b2), b=2.0*cross(b1,b0), d=2.0*cross(b2,b1);
  float f=b*d-a*a;
  vec2 d21=b2-b1, d10=b1-b0, d20=b2-b0;
  vec2 gf=2.0*(b*d21+d*d10+a*d20);
  gf=vec2(gf.y,-gf.x);
  vec2 pp=-f*gf/dot(gf,gf);
  vec2 d0p=b0-pp;
  float ap=cross(d0p,d20), bp=2.0*cross(d10,d0p);
  float t=clamp((ap+bp)/(2.0*a+b+d), 0.0 ,1.0);
  return length(mix(mix(b0,b1,t),mix(b1,b2,t),t));

}

float deStreamline(in vec2 p, in vec2 control, in vec2 offset, in float size)
{
    vec2 controlR = vec2(size + offset.x, 0.0);
    vec2 controlL = vec2(-(size + offset.y), 0.0);
    vec2 deltaR = controlR - control;
    vec2 deltaL = controlL - control;
    float t = 1.0 - 2.0 * offset.x / (controlR.x - control.x);
    vec2 jointR = control + deltaR * t;
    t = 1.0-2.0 * offset.y / (control.x - controlL.x);
    vec2 jointL = control + deltaL * t;    
    return min(min(min(
        deBezier(p, jointR, controlR, jointR*vec2(1.0,-1.0)),
    	deBezier(p, jointL, controlL, jointL*vec2(1.0,-1.0))),
		deBezier(p, jointL, control, jointR)),
    	deBezier(p, jointL * vec2(1.0, -1.0), control * vec2(1.0, -1.0), jointR * vec2(1.0, -1.0)));
}

float deExclamationMark(in vec3 p)
{
    // bounding box
    vec3 bb = vec3(1.0, 3.2, 1.0);
    if (any(greaterThan(abs(p), bb))) return length(max(abs(p) - bb, 0.0)) + 0.2;
    vec2 control = vec2(0.7, 0.7);
    vec2 offset = vec2(0.2, 0.2);
    float size = 1.55;
    p.xz *= rotate(atan(p.z,p.x));
    return min(deStreamline(0.8 * p.yx, control, offset, size), length(p - vec3(0.0,-2.7, 0.0)) - 0.3);
}

float deQuestionMark(in vec3 p)
{
    // bounding box
    vec3 bb = vec3(1.5, 3.2, 0.4);
    if (any(greaterThan(abs(p), bb))) return length(max(abs(p) - bb, 0.0)) + 0.2;
    vec2 v = vec2(0.0, -1.2);
    vec2 h = vec2(0.0, 0.7);
    vec2 a0 = v + h;
    vec2 a1 = v * rotate(PI2 / 6.0) + h;
    vec2 a2 = v * rotate(PI2 * 2.0 / 6.0) + h;
    vec2 a3 = v * rotate(PI2 * 3.0 / 6.0) + h;
    vec2 a4 = v * rotate(PI2 * 4.0 / 6.0) + h;
    vec2 a5 = v * rotate(PI2 * 5.0 / 6.0) + h;
    //vec2 b0 = a0 + 0.5 * (a1 - a0);
    vec2 b1 = a1 + 0.5 * (a2 - a1);
    vec2 b2 = a2 + 0.5 * (a3 - a2);
    vec2 b3 = a3 + 0.5 * (a4 - a3);
    vec2 b4 = a4 + 0.5 * (a5 - a4);
    vec2 b5 = a5 + 0.5 * (a0 - a5);    
    float de = 100.0;
    //de = min(de, deBezier(p, b0, a1, b1));
    de = min(de, deBezier(p.xy, b1, a2, b2));
    de = min(de, deBezier(p.xy, b2, a3, b3));
    de = min(de, deBezier(p.xy, b3, a4, b4));
    de = min(de, deBezier(p.xy, b4, a5, b5));
	de = min(de, deBezier(p.xy, b5, a0, vec2(0.0,-1.8)));
	return min(length(vec2(de,p.z))-0.3, length(p - vec3(0.0,-2.7, 0.0)) - 0.3);
}

float map(in vec3 p)
{
    p.x += 6.0;
    p.y -= 0.4;
    p.zx *= rotate(iGlobalTime * -0.5);
    float s = 6.0;
    float a = PI / s - atan(p.z, p.x);
    float n = PI2 / s;
    a = floor(a / n) * n;
    p.zx *= rotate(a);
    p.x -= 8.0 ;
    return min(
        deExclamationMark(p + vec3(1.2, 0.0, 0.0)),
        deQuestionMark(p - vec3(1.2, 0.0, 0.0)));
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
	const float maxd = 20.0;
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
	vec2 p = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
	vec3 col = vec3(0.15 + p.y * 0.2);
   	vec3 rd = normalize(vec3(p, -1.8));
	vec3 ro = vec3(0.0, 0.0, 8.0);
    vec3 li = normalize(vec3(0.5, 0.8, 3.0));
    float t = march(ro, rd);
    if(t > -0.001)
    {
        vec3 pos = ro + t * rd;
        vec3 n = calcNormal(pos);
		float dif = clamp((dot(n, li) + 0.5) * 0.7, 0.3, 1.0);
   		col = vec3(0.8) * dif;
    }
   	fragColor = vec4(col, 1.0);
}
