int imod(int a, int b)
{
	return a - a / b * b;
}
int xor(int a, int b)
{
	int result = 0;
	int x = 1;
	for(int i = 0; i <= 8; ++i)
    {
        if (imod(a,2) != imod(b,2))
            result += x;
        a /= 2;
        b /= 2;
        x *= 2;
	}
	return result;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 xy = uv * vec2(320.0,200.0);
	float z;
    vec2 dist;
    for(int i = 0; i < 256; ++i)
    {
		z = float(i) / 255.0;
		dist = (xy - vec2(160.0,100.0)) * z;		
		z = mod(z + iGlobalTime/4.0, 1.0);
		dist.x += sin(iGlobalTime*3.14)*15.0;		
		int zz = int(z * 2.0);
		if (zz == 0) dist.x -= 10.0;
		else dist.x += 10.0;		
		if ( (abs(dist.x) >= 25.0 && (imod(int(z*8.0),2)==0)) || abs(dist.y) >= 16.0)
            break;
	}
	int texel = xor(xor(int(dist.x), int(dist.y)), int(mod(z, 0.25)*255.0));
    texel = imod(texel, 16);
    float c = float(texel) / 16.0;
	fragColor = vec4(c,c,c,1.0);
	//fragColor = vec4(z,z,z,1.0);
}