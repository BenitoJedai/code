vec3 rotateX(float a, vec3 v) {
   return vec3(v.x, cos(a) * v.y + sin(a) * v.z, cos(a) * v.z - sin(a) * v.y);
}

vec3 rotateY(float a, vec3 v) {
   return vec3(cos(a) * v.x + sin(a) * v.z, v.y, cos(a) * v.z - sin(a) * v.x);
}

float map(in vec3 p) {
        
    vec3 q = rotateY(iGlobalTime, p);
 	float d1 = length(q) - 1.0;
    
    d1 += 0.1*sin(10.0*q.x)*sin(10.0*q.y + iGlobalTime )*sin(10.0*q.z);
    
 	float d2 = p.y + 1.0;
        
    float k = 1.0;
    float h = clamp(0.5 + 0.5 *(d1-d2)/k, 0.0, 1.0);
        
    return mix(d1, d2, h) - k*h*(1.0-h);
}

vec3 calcNormal(in vec3 p) {
    vec2 e = vec2(0.0001, 0.0);
     
    return normalize(vec3( map(p+e.xyy) - map(p-e.xyy),
                           map(p+e.yxy) - map(p-e.yxy),
                           map(p+e.xyx) - map(p-e.xyx)));
	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 p = -1.0 + 2.0 * uv;
    
    p.x *= iResolution.x / iResolution.y;
    
    vec3 ro = vec3(0.0, 0.0, 3.0);
    vec3 rd = normalize(vec3(p, -1.0));
    
    vec2 angs = vec2(1.0-iMouse.y * 0.003, 1.0-iMouse.x * 0.01);
	
	//ro = rotateY(angs.y, ro) + vec3(0.0, 0.6, 0.0);
	//rd = rotateY(angs.y, rotateX(angs.x, rd));

    vec3 col = vec3(0.0);
    
    float tmax = 20.0;
	float h = 1.0;
    float t = 0.0;
    for (int i=0; i<100; i++) {
    	
        if (h < 0.0001 || t > tmax) break;
        
        h = map(ro + t*rd);   
        t += h;
    }
    
    vec3 lig = vec3(0.5773);

    if (t<tmax) {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal(pos);
    	col = vec3(1.0);
        col = vec3(1.0, 0.8, 0.5)*clamp(dot(nor, lig), 0.0, 1.0);
        col += vec3(0.2, 0.3, 0.4)*clamp(nor.y, 0.0, 1.0);
        col += 0.1;
        col *= exp(-0.1*t);
    }
    
    fragColor = vec4(col, 1.0);
}