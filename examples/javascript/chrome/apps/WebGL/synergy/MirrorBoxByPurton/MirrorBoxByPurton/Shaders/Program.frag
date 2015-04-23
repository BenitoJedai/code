#define REFLECTION_COUNT 4

float map(in vec3 pos)
{
    float negCircle = length(pos) - 1.37;
    float cube = length(max(abs(pos) - 0.95, 0.0)) - 0.05;
    float shape = mix(max(-negCircle, cube), cube, 0.5 + sin(iGlobalTime * 0.4) * 0.5);;
    
    float roof = dot(pos, vec3(0.0, -1.0, 0.0)) + 2.0 + sin(pos.x * 35.0) * 0.005;
    float ground = dot(pos, vec3(0.0, 1.0, 0.0)) + 1.0;
    float backWall = dot(pos, vec3(0.1, 0.0, -1.0)) + 6.5;
    float frontWall = dot(pos, vec3(0.1, 0.0, 1.0)) + 6.5;
    
    vec3 repeatPos = mod(pos + vec3(0.0, -0.05, 0.0), 2.0) - 0.5 * 2.0;
    float pillars = length(max(abs(repeatPos) - 0.15, 0.0)) - 0.05;
    pillars = max(-(dot(pos, vec3(0.0, -1.0, 0.0)) + 0.5), pillars);
    
    return min(min(min(min(min(shape, roof), ground), backWall), frontWall), pillars);
}

vec3 castRay(in vec3 ro, in vec3 rd, in float mint, in float maxt)
{
    float t = mint;
    
    for (int i = 0; i < 96; i++)
    {
        vec3 p = ro + rd*t;
        float dist = map(p);
        
        if (dist <= 0.0 || t > maxt)
            break;
        
        t += max(dist, 0.0001);
    }
    
    return ro + rd*min(t, maxt);
}

vec3 getNormal(in vec3 pos)
{
    vec2 eps = vec2(0.001, 0.0);
    vec3 normal = vec3(
        map(pos + eps.xyy) - map(pos - eps.xyy),
        map(pos + eps.yxy) - map(pos - eps.yxy),
        map(pos + eps.yyx) - map(pos - eps.yyx));
    return normalize(normal);
}

float getAO(in vec3 hitp, in vec3 normal)
{
    float dist = 0.02;
    vec3 spos = hitp + normal * dist;
    float sdist = map(spos);
    return clamp(sdist / dist, 0.0, 1.0);
}

vec4 shade(in vec3 hitp, in vec3 normal, in vec3 rd, in vec3 lightPos)
{
    vec3 lightDir = normalize(lightPos - hitp);
    vec3 lightHit = castRay(hitp, lightDir, 0.01, distance(hitp, lightPos));
    
    float ao = getAO(hitp, normal);
    float shadow = pow(distance(hitp, lightHit) / distance(hitp, lightPos), 64.0);
    float diffuse = clamp(dot(normal, lightDir), 0.0, 1.0);
    float attenuation = 1.0 / pow(distance(hitp, lightPos), 2.0);
    float specular = pow(clamp(dot(normalize(lightDir - rd), normal), 0.0, 1.0), 64.0);
    
    vec4 diffuseColor = vec4(1.0) * diffuse * attenuation;
    vec4 specularColor = vec4(1.0) * specular * diffuse * attenuation;
    
    return (diffuseColor + specularColor) * min(ao, shadow);
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
    vec2 uv = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0 + 2.0*uv;
    p.y *= iResolution.y/iResolution.x;
    
    vec3 ro = vec3(0.1 * cos(iGlobalTime * 0.4), 0.05 * sin(iGlobalTime * 0.25), -3.0);
    vec3 rd = normalize(vec3(p.x, p.y, 0.75));
    vec3 lightPos = vec3(cos(iGlobalTime * 0.1) * 3.0, 0.0, sin(iGlobalTime * 0.4) * 3.0);
    
    vec3 hitp = castRay(ro, rd, 1.0, 32.0);
    vec3 normal = getNormal(hitp);
    
    vec4 color = vec4(0.0);
    float refMult = 1.0;
    color += shade(hitp, normal, rd, lightPos);
    
    for (int i = 0; i < REFLECTION_COUNT; i++)
    {
        refMult *= 0.75;
        vec3 refDir = reflect(rd, normal);
        vec3 refHitp = castRay(hitp, refDir, 0.01, 32.0);
        vec3 refNormal = getNormal(refHitp);
        color += shade(refHitp, refNormal, refDir, lightPos) * refMult;
        
        rd = refDir;
        hitp = refHitp;
        normal = refNormal;
    }
    
    fragColor = sqrt(color);
}
