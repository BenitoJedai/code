const float eps = 0.001;

vec3 light = vec3(2.0 * sin(iGlobalTime), 0.25, 1.0 + iGlobalTime / 4.0 + 2.0 * cos(iGlobalTime));

float norm(vec3 v)
{
    return pow(pow(abs(v.x), 12.0) + pow(abs(v.y), 12.0) + pow(abs(v.z), 12.0), 1.0 / 12.0);
}
    
float f(vec3 p)
{
    vec3 cube = p;
    
    cube.y += 0.35;
    
    cube.x = mod(cube.x, 1.0) - 0.5;
    cube.z = mod(cube.z, 1.0) - 0.5;
    
    
    return min
    (
        norm(cube) - 0.25 + sin(16.0 * p.x + 4.0 * iGlobalTime) / 128.0 + cos(16.0 * p.z + 4.0 * iGlobalTime) / 128.0,
        p.y + 0.7
    );
}

vec3 df(vec3 p)
{
    float f0 = f(p);

    return normalize(vec3
    (
        f(p + vec3(eps, 0.0, 0.0)) - f0,
        f(p + vec3(0.0, eps, 0.0)) - f0,
        f(p + vec3(0.0, 0.0, eps)) - f0
    ) / eps);
}

vec3 solve(vec3 start, vec3 dir)
{
    vec3 p;
    float t1;
    float t2 = 0.0;
    
    for(int n = 0; n < 128; ++n)
    {
        p = start + t2 * dir;
        t1 = t2;
        t2 += f(p);
        
        if(t2 - t1 < eps)
        {
            break;
        }
    }
    
    return (t2 - t1 < eps) ? p : vec3(0.0, 0.0, 0.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xx - vec2(0.5, 0.5 * iResolution.y / iResolution.x);
    
    vec3 c = vec3(0, 0, 0);

    vec3 start = vec3(0.0, 0.5, 0.0 + iGlobalTime / 4.0);
    vec3 d = normalize(vec3(uv + vec2(0.0, 0.5), 0.375 + iGlobalTime / 4.0) - start);
    vec3 dir;
    
    dir.x = d.x;
    dir.y = d.y * cos(0.65) - d.z * sin(0.65);
    dir.z = d.y * sin(0.65) + d.z * cos(0.65);
    
    for(float i = 0.0; i < 3.0; ++i)
    {
        vec3 p = solve(start, dir);
        
        if(dot(p, p) > 0.0)
    	{
            vec3 normal = df(p);
            vec3 incident = p - start;
            vec3 reflected = normalize(reflect(incident, normal));
            
            vec3 s = solve(light, normalize(p - light));
            
            if(length(s - p) < 32.0 * eps)
            {
            	c += (normal + vec3(1.0, 0.25, 1.0)) * clamp(dot(normalize(light - p), normal) / length(light - p), 0.0, 1.0) / exp(i);
            }
            
            dir = reflected;
            start = p + dir * 16.0 * eps;
	    }
    }

	fragColor = vec4(c * (sin(fragCoord.y * 2.0) + 2.0) / 3.0, 1.0);
}