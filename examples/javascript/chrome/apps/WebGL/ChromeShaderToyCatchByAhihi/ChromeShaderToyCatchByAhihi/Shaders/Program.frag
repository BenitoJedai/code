#define TAU 6.283185307179586

vec2 rect2polar(vec2 p) {
    return vec2(atan(p.y, p.x), length(p));
}

vec2 polar2rect(vec2 p) {
    return vec2(cos(p.x) * p.y, sin(p.x) * p.y);
}

#define NO_MATERIAL 0
#define WHITE_MATERIAL 1
#define RED_MATERIAL 2
#define BLACK_MATERIAL 3
#define LIGHT_MATERIAL 4
#define GROUND_MATERIAL 5

struct ObjectDistance {
    float distance;
    int material;
};

ObjectDistance distanceUnion(ObjectDistance a, ObjectDistance b) {
    if(a.distance < b.distance) {
        return a;
    } else {
     	return b;
    }
}

ObjectDistance distanceDifference(ObjectDistance b, ObjectDistance a) {
    if(-a.distance > b.distance) {
        a.distance *= -1.0;
        return a;
    } else {
        return b;
    }        
}

ObjectDistance sphere(float radius, int material, vec3 p) {
  	return ObjectDistance(length(p) - radius, material);
}

ObjectDistance box(vec3 b, int material, vec3 p)
{
  vec3 d = abs(p) - b;
  return ObjectDistance(
      min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0)),
      material
  );
}

ObjectDistance cylinder(vec2 h, int material, vec3 p)
{
  vec2 d = abs(vec2(length(p.xy), p.z)) - h;
  return ObjectDistance(
      min(max(d.x, d.y), 0.0) + length(max(d, 0.0)),
      material
  );
}

ObjectDistance ground(float y, int material, vec3 p) {
     return ObjectDistance(p.y - y, material);   
}

ObjectDistance halve(float buttonRadius, float spacing, int material, vec3 p) {
    float overshoot = 0.1;
    ObjectDistance od;
    
    od = sphere(1.0, material, p);
    
    od = distanceDifference(
        od,
        box(
            vec3(1.0 + overshoot, 0.5 + overshoot, 1.0 + overshoot),
            material,
            p + vec3(0.0, 0.5 + overshoot - spacing/2.0, 0.0)
        )
    );
    
    od = distanceDifference(
        od,
        cylinder(vec2(buttonRadius + spacing, 0.5), material, p + vec3(0.0, 0.0, 0.6))
    );
    
    return od;
}

ObjectDistance button(float radius, float innerRadius, int material, int innerMaterial, vec3 p) {
    ObjectDistance od;
    
    od = cylinder(vec2(radius, 0.5), material, p + vec3(0.0, 0.0, 0.5));
    
    od = distanceUnion(
    	od,
        cylinder(vec2(innerRadius, 0.5), innerMaterial, p + vec3(0.0, 0.0, 0.52))
    );
    
    return od;
}

ObjectDistance pokeball(vec3 p) {
    float buttonRadius = 0.13;
    float buttonInnerRadius = 0.65 * buttonRadius;
    float spacing = 0.08;
    
    ObjectDistance od;
    
    od = halve(buttonRadius, spacing, RED_MATERIAL, p);
    
    od = distanceUnion(
        od,
        halve(buttonRadius, spacing, WHITE_MATERIAL, p * vec3(1.0, -1.0, 1.0))
    );
    
    od = distanceUnion(
        od,
        sphere(0.97, BLACK_MATERIAL, p)
    );
    
    od = distanceUnion(
        od,
        button(buttonRadius, buttonInnerRadius, WHITE_MATERIAL, LIGHT_MATERIAL, p)
    );
    
    return od;
}

vec3 wobble(vec3 p) {
    float angle = pow(sin(2.0 * iGlobalTime), 3.0) * 0.1 * sin(15.0 * iGlobalTime);
    vec2 xy = polar2rect(rect2polar(p.xy) + vec2(angle, 0.0));
    p.xy = xy;
    float translation = angle;
    p.x -= translation;
    return p;
}

ObjectDistance sceneDistance(vec3 p) {    
	ObjectDistance od;
    
    od = ground(-1.0, GROUND_MATERIAL, p);
    
    od = distanceUnion(
        od,
        pokeball(wobble(p))
    );
            
    return od;
}

#define THRESHOLD 0.001
#define SHADOW_THRESHOLD 0.01
#define MAX_ITERATIONS 256
#define MAX_SHADOW_ITERATIONS 256
#define NORMAL_DELTA 0.001
#define MAX_DEPTH 60.0

struct MarchResult {
    float length;
    float distance;
    int material;
    int iterations;
};
    
MarchResult march(vec3 origin, vec3 direction) {
    MarchResult result = MarchResult(0.0, 0.0, NO_MATERIAL, 0);
    for(int i = 0; i < MAX_ITERATIONS; i++) {
	    ObjectDistance sd = sceneDistance(origin + direction * result.length);
        result.distance = sd.distance;
        result.material = sd.material;
        result.iterations++;
        
        if(result.distance < THRESHOLD || result.length > MAX_DEPTH) {
            break;
        }
        
        result.length += result.distance * (1.0 - 0.5*THRESHOLD);
    }

    if(result.length > MAX_DEPTH) {
        result.material = NO_MATERIAL;
    }
    
    return result;
}

// this is wrong, but looks very cool!
float marchGlitchyShadow(vec3 lightPos, vec3 surfacePos, float k) {
    vec3 origin = lightPos;
    vec3 target = surfacePos;
    
    vec3 travel = target - origin;
    vec3 forward = normalize(travel);
    float maxLength = length(travel);
    
    float length = 0.0;
    float distance = 0.0;
    int iterations = 0;
    for(int i = 0; i < MAX_SHADOW_ITERATIONS; i++) {
        if(length >= maxLength) {
         	return 1.0;   
        }
        
        ObjectDistance od = sceneDistance(origin + forward * length);
        distance = od.distance;
        
        if(abs(distance) < THRESHOLD) {
            return 0.0;
        }
        
        length += distance;
        iterations++;
    }

    return 1.0;
}

float marchShadow(vec3 lightPos, vec3 surfacePos, float k) {
    vec3 origin = lightPos;
    vec3 target = surfacePos;
    
    vec3 travel = target - origin;
    vec3 forward = normalize(travel);
    float maxLength = length(travel) * 0.9;
    
    float length = 0.0;
    float distance = 0.0;
    float light = 1.0;
    int iterations = 0;
    for(int i = 0; i < MAX_SHADOW_ITERATIONS; i++) {
        if(length >= maxLength - SHADOW_THRESHOLD) {
         	break;
        }
        
        ObjectDistance od = sceneDistance(origin + forward * length);
        distance = od.distance;
        
        if(distance < SHADOW_THRESHOLD) {
            return 0.0;
        }
        
        light = min(light, k * distance / length);
        length += distance * 0.999;
        
        iterations++;
    }

    //return 1.0 - float(iterations) / float(MAX_SHADOW_ITERATIONS);
    return light;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 pxPos = 2.0*(0.5 * iResolution.xy - fragCoord.xy) / iResolution.xx;
    
    vec2 camXZ = polar2rect(vec2(-TAU/4.0 + 0.3 * iGlobalTime, 3.0));
  	vec3 camPos = vec3(camXZ.x, 1.0 + 0.5 * sin(1.0 * iGlobalTime), camXZ.y);
    
    vec3 camLook = vec3(0.0, 0.0, 0.0);
    
    vec3 camUp = vec3(0.0, 1.0, 0.0); 
    vec3 camForward = normalize(camLook - camPos);
    vec3 camLeft = normalize(cross(camUp, camForward));
    vec3 camUp2 = cross(camForward, camLeft);
    vec3 camPosForward = camPos + camForward;
    vec3 screenPos = camPosForward - pxPos.x * camLeft - pxPos.y * camUp2;
    vec3 rayForward = normalize(screenPos - camPos);
    
    MarchResult mr = march(camPos, rayForward);
    	
    vec3 rayEnd = camPos + mr.length * rayForward;
    vec3 color;
    vec3 bgColor = vec3(0.1);
    
    /*if(mr.distance < 0.0) {
       	color = vec3(0.0, 1.0, 1.0);
    } else */if(mr.material == NO_MATERIAL) {
        color = bgColor;
    } else {
        vec3 baseColor;
        
        if(mr.material == WHITE_MATERIAL) {
            baseColor = vec3(1.0);
        } else if(mr.material == RED_MATERIAL) {
            baseColor = vec3(1.0, 0.0, 0.0);
        } else if(mr.material == BLACK_MATERIAL) {
            baseColor = vec3(0.2);
        } else if(mr.material == LIGHT_MATERIAL) {
         	baseColor = vec3(1.0, 0.7, 0.7);   
        } else if(mr.material == GROUND_MATERIAL) {
            float tile = mod(floor(rayEnd.x) + floor(rayEnd.z), 2.0);
            
            if(tile < 1.0) {
	         	baseColor = vec3(0.2);
            } else {
                baseColor = vec3(0.3);
            }
        }
        
        float deltaTwice = 2.0 * NORMAL_DELTA;
        vec3 dx = vec3(NORMAL_DELTA, 0.0, 0.0);
        vec3 dy = vec3(0.0, NORMAL_DELTA, 0.0);
        vec3 dz = vec3(0.0, 0.0, NORMAL_DELTA);
        vec3 normal = normalize(vec3(
            (sceneDistance(rayEnd + dx).distance - sceneDistance(rayEnd - dx).distance) / deltaTwice,
            (sceneDistance(rayEnd + dy).distance - sceneDistance(rayEnd - dy).distance) / deltaTwice,
            (sceneDistance(rayEnd + dz).distance - sceneDistance(rayEnd - dz).distance) / deltaTwice
        ));

       	vec2 lightXZ = polar2rect(vec2(-0.5 * iGlobalTime, 3.0));
        vec3 lightPos = vec3(lightXZ.x, 5.0, lightXZ.y);

        float ambient = 0.2;
        float diffuse = max(0.0, dot(normal, normalize(lightPos - rayEnd)));
        float specular = pow(diffuse, 16.0);
		float shadow = 1.0;
        shadow = marchShadow(lightPos, rayEnd, 32.0);
        //shadow = marchGlitchyShadow(lightPos, rayEnd, 8.0);

        color = ((ambient + shadow * diffuse) * baseColor + specular) * (1.0 - mr.length * 0.01);
        //color = vec3(rayIterations / MAX_TRACE_ITERATIONS, 0.0, shadow);

    }
        
	//color = mix(vec3(0.0), vec3(0.0, 1.0, 0.0), float(mr.iterations)/float(MAX_ITERATIONS));
    
    fragColor = vec4(color, 1.0);
}