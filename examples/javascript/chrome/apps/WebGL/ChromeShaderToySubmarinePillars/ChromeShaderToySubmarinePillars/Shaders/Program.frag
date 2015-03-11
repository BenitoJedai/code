mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

vec3 hsv(float h, float s, float v)
{
  return mix( vec3( 1.0 ), clamp( ( abs( fract(
    h + vec3( 3.0, 2.0, 1.0 ) / 3.0 ) * 6.0 - 3.0 ) - 1.0 ), 0.0, 1.0 ), s ) * v;
}

float map(in vec3 p)
{
    p *= 0.5;
    vec3 q = p;
    float time = iGlobalTime * -0.5;
	p.z += 1.5 * sin(p.x * 5.8+ time)+ 0.5 * sin(p.x * 5.2+ time);
    p.x += 1.5 * cos(q.z * 3.2+ time)+ 0.2 * cos(q.z * 4.3+ time);
    p.xz *= rotate(atan(-p.x, p.z));
    float t = pow(1.0 - sqrt(1.0 - pow(abs(p.y), 2.0)), 2.5);
    float de = 0.3 * mix(p.z - 0.2, p.z - 1.0, t);
    return max(abs(p.y) - 1.15, de);
}

vec3 calcNormal(in vec3 p)
{
	const vec2 e = vec2(0.001, 0.0);
	return normalize(vec3(
		map(p + e.xyy) - map(p - e.xyy),
		map(p + e.yxy) - map(p - e.yxy),
		map(p + e.yyx) - map(p - e.yyx)));
}

float march(in vec3 ro, in vec3 rd)
{
	const float maxd = 100.0;
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
    vec3 col = vec3(0.0);
    vec3 rd = normalize(vec3(p, -1.8));
	vec3 ro = vec3(0.0, 0.0, 8.5);
    vec3 li = normalize(vec3(0.5, 0.8, 3.0));
    float t = march(ro, rd);
    if(t > -0.001)
    {
        vec3 p3d = ro + t * rd;
        vec3 n = calcNormal(p3d);
		float dif = clamp((dot(n, li) + 0.5) * 0.7, 0.4, 1.0);
        col = hsv(0.6, 0.7, 0.9) * dif;
		col *= exp(-0.005 * p3d.z * p3d.z);
        col = pow(col, vec3(0.8));
  	}
   	fragColor = vec4(col, 1.0);
}

