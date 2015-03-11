mat2 rotate (float angle) {
 	return mat2(cos(angle), -sin(angle),
                sin(angle), cos(angle));   
}

float volume (vec3 p) {
    float meta = 0.0;
    float r = 1.0;
    float g = 2.0;
    for (int i = 0; i < 8; ++i) {
        vec3 mn = vec3(0.0);
        mn.x = sin(iGlobalTime + float(i)*128.0) * 2.0;
        mn.y = sin(iGlobalTime - float(i)*64.0) * 2.0;
        mn.z = cos(iGlobalTime - float(i)*16.0) * 2.0;
        meta -= 0.75*0.75 / pow(length(mn-p), g);
    }
    
    return min(p.y + 3.25, meta + 1.0);
}

vec3 normal (vec3 p) {
 	vec2 eps = vec2(0.001, 0.0);
    return normalize(vec3(
    	volume(p + eps.xyy) - volume(p - eps.xyy),
        volume(p + eps.yxy) - volume(p - eps.yxy),
        volume(p + eps.yyx) - volume(p - eps.yyx)
    ));
}

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
	uv = uv * 2.0 - 1.0;
    uv.x *= iResolution.x / iResolution.y;
    
    vec3 sun = normalize(vec3(0.0, -1.0, 0.0));
    vec3 fog = vec3(0.8, 0.8, 0.8);
    vec3 color = fog;
   	vec3 ro = vec3(uv, -5.0);
    vec3 rd = normalize(vec3(uv * 1.25, 1.0));
  
    ro.z = -5.0 * cos(iGlobalTime * 0.5);
    ro.x = -5.0 * sin(iGlobalTime * 0.5);
    rd.xz *= rotate(-iGlobalTime * 0.5);
    
    float h = 0.0;
    for (int i = 0; i < 150; ++i) {
     	vec3 p = ro+h*rd;
        float tr = volume(p);
        h += max(0.01, tr);
       
        if (h > 14.0) break;
        if (tr < 0.0) {
            vec3 n = normal(p);
            float shade = clamp(dot(n, -sun), 0.0, 1.0);
            float shade2 = clamp(dot(n, normalize(vec3(-1.0, -1.0, 0.0))), 0.0, 1.0);
        	color = vec3(1.0, 0.9, 0.7)*mix(0.125, 1.0, shade);
            color += vec3(0.2, 0.2, 0.5)*0.25 * shade2;
            
            if (p.y < -3.0 + 1e-6) {
                
                color -= mix(0.0, 0.25, rand(floor(p.xz*0.5)));
                
             	float sh = 0.1;
                for (int i = 0; i < 16; ++i) {
                 	vec3 pp = p + sh * vec3(0.0, 1.0, 0.0);
                    float si = volume(pp);
                    sh += max(0.01, si);
                    if (sh > 4.0) break;
                    if (si < 0.0) {
                     	color *= 1.0 - exp(-max(0.0, length(pp-p)-0.125) * 3.5);
                        break;   
                    }
                }
            }
            
           	color = mix(color, fog, 1.0-exp(-length(p)*0.25));
            break;
        }
    }
    
    vec2 uv2 = fragCoord.xy / iResolution.xy;
    uv2 = uv2 * 2.0 - 1.0;
    color *= smoothstep(1.65, 1.65 - 0.75, length(uv2));
    
    fragColor = vec4(sqrt(color), 1.0);
}