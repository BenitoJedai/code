#define scale 4.0
#define PIX_SIZE 5.0

float IGN(vec2 p)
{
    vec3 magic = vec3(0.06711056, 0.00583715, 52.9829189);
    return fract( magic.z * fract(dot(p,magic.xy)) );
}


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = floor(fragCoord.xy/PIX_SIZE);
    float k = IGN(uv);
	fragColor = vec4(k,k,k,1.0);
}