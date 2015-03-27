float hash( vec3 p )
{
	float h = dot(p,vec3(127.1,311.7,201.3));
	
    return fract(sin(h)*43758.5453123);
}

float noise( in vec3 p )
{
    vec3 i = floor( p );
    vec3 f = fract( p );
	
	vec3 u = f*f*(3.0-2.0*f);

    return mix(mix( mix( hash( i + vec3(0,0,0) ), 
                         hash( i + vec3(1,0,0) ), u.x),
                    mix( hash( i + vec3(0,1,0) ), 
                         hash( i + vec3(1,1,0) ), u.x), u.y),
               mix( mix( hash( i + vec3(0,0,1) ), 
                         hash( i + vec3(1,0,1) ), u.x),
                    mix( hash( i + vec3(0,1,1) ), 
                         hash( i + vec3(1,1,1) ), u.x), u.y), u.z );
}

float noise(vec2 p)
{
    return noise(vec3(p.xy, 1));
}

float fbm(vec2 p)
{
    const mat2 m = mat2(0.8, 0.6, -0.6, 0.8);
 
    float f = 0.0;
    f += 0.5000 * noise(p); p = m * p * 2.02;
    f += 0.2500 * noise(p); p = m * p * 2.03;
    f += 0.1250 * noise(p); p = m * p * 2.01;
    f += 0.0625 * noise(p);
    return f / 0.9375;
}

// ...

const vec2 brick_size = vec2(0.2, 0.1);

void masks(vec2 p, out float mortar, out float mortar_grain, out float brick_grain)
{
    vec2 b = p / brick_size;
    ivec2 bi = ivec2(b);
    
    b.x += 0.5 * float(bi.y - (bi.y / 2) * 2);
    bi = ivec2(b);

    vec2 bf = fract(b);
    
    bf.x += 0.05 * fbm(50.0 * p);
    bf.y += 0.1 * fbm(30.0 * p);	

    mortar = 1.0 - smoothstep(0.025, 0.0255, bf.x) * smoothstep(0.975, 0.965, bf.x) * smoothstep(0.05, 0.051, bf.y) * smoothstep(0.90, 0.89, bf.y);
    mortar_grain = 0.5 * fbm(300.0*p);
    brick_grain = smoothstep(0.7, 0.9, fbm(100.0*p*vec2(1.0, 1.3)));
}

float height(vec2 p)
{
    float mortar, mortar_grain, brick_grain;
    masks(p, mortar, mortar_grain, brick_grain);
    return 0.01 * mortar * mortar_grain  + (1.0 - mortar) * (1.0 - 0.3 * brick_grain + 0.003 * fbm(110.0*p));
}

vec3 normal(vec2 p)
{
    float d = 0.001;
    vec2 dx = vec2(d, 0.0);
    vec2 dy = vec2(0.0, d);
    float dh_dx = (height(p + dx) - height(p)) / d;
    float dh_dy = (height(p + dy) - height(p)) / d;
    return normalize(vec3(-dh_dx, -dh_dy, 1.0));
}

vec3 diffuse(vec2 p)
{
    vec2 b = p / brick_size;
    ivec2 bi = ivec2(b);
    
    b.x += 0.5 * float(bi.y - (bi.y / 2) * 2);
    bi = ivec2(b);

    vec2 bf = fract(b);
  
    float mortar_mask, mortar_grain_mask, brick_grain_mask;
    masks(p, mortar_mask, mortar_grain_mask, brick_grain_mask);
    
    vec3 mortar_color = vec3(0.7, 0.7, 0.7) - 0.1 * vec3(fbm(20.0*p));
	vec3 mortar_grain_color = 0.1 * vec3(0.6, 0.5, 0.3);
    
    mortar_color = mortar_grain_mask * mortar_grain_color + (1.0 - mortar_grain_mask) * mortar_color;
    
    vec3 brick_color = vec3(0.5, 0.1, 0.1);
    brick_color += vec3(0.1 * noise(vec2(bi)));
    brick_color -= vec3(smoothstep(0.0, 1.0, 0.7*fbm(20.0*(p)) - 0.3));

	vec3 brick_grain_color = vec3(0.6, 0.3, 0.2);
    brick_color = brick_grain_mask * brick_grain_color + (1.0 - brick_grain_mask) * brick_color;
    
  	return mortar_color * mortar_mask + (1.0 - mortar_mask) * brick_color;
}

vec3 light(vec3 p, vec3 n, vec3 c, vec3 l, float i)
{
    vec3 d = l - p;
    float a = 1.0 / (1.0 + length(d));
    float f = dot(normalize(d), n);
    float w = 0.5; 
    return c * a * i * max(0.0, (f + w) / (1.0 + w));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x / iResolution.y;

    vec3 l = vec3(0.5 + 0.5 * sin(2.0 * iGlobalTime), 0.5 + 0.5 * sin(3.0 * iGlobalTime), 1.0 + sin(iGlobalTime));

    vec3 color;
    //color = diffuse(q.xy);
    //color = normal(p);
    color = light(vec3(p, 0.0), normal(p), diffuse(p), l, 1.1);
    fragColor = vec4(color, 1.0);
}