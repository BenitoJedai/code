#define PI				3.14159265359
#define PIH				(PI/2.0)
#define PI2				(PI*2.0)
#define DRAW_RANGE		64.0
vec3	MOD_OFFSET		= vec3(16.0, 32.0, 16.0);
vec3	MOD_INV_OFFSET	= 1.0 / MOD_OFFSET;
#define OFFSET_Y		4.0


mat3 RotX(float a)								{float s = sin(a); float c = cos(a); return mat3(1,0,0,0,c,s,0,-s,c);}
mat3 RotY(float a)								{float s = sin(a); float c = cos(a); return mat3(c,0,-s,0,1,0,s,0,c);}
mat3 RotZ(float a)								{float s = sin(a); float c = cos(a); return mat3(c,s,0,-s,c,0,0,0,1);}
float Sub(float from, float what)				{return max(from, -what);}
float Add(float a, float b)						{return min(a, b);}
float Intersect(float a, float b)				{return max(a, b);}


float PropulsionTime(float s, float offset)		{return iGlobalTime*0.5 + sin((iGlobalTime+offset*PI)*2.0 + PI*0.1)*s;}
float BreathScale(float y, float offset)		{return sin(y * PI * 0.8 + (iGlobalTime+offset*PI) * 2.0) * 0.5 + 0.5;}


vec4 Rnd4(float i)								{return sin(vec4(123.8456, 64.146543, 992.12343, 1235.01023) * i);}

float ModRand(vec3 p)
{
	p = (p + MOD_OFFSET*8.0) / MOD_OFFSET;
	p = sin(floor(p)*946.1235) * 0.5 + 0.5;
	return sin(dot(p, vec3(123.1123, 234.123, 67.112))) * 0.5 + 0.5;
}


float JellyBody(vec3 p, float inOffset)
{
	p.y					*= 0.7 + inOffset*0.2;
	
	// Z plugged into X on purpose, this makes the shape not perfect from both sides
	p.z					+= (sin(p.x * PI * 4.0)*0.5 + 0.5) * 0.06;
	p.x					+= (cos(p.z * PI * 4.0)*0.5 + 0.5) * 0.06;
	
	float breath		= pow(0.2 + BreathScale(p.y, inOffset)*0.8, 0.2);
	p.xz				*= 0.5 + breath*0.5;
	
	float sphereOuter	= (length(p) - 1.0);
	float sphereInner	= (length(p) - 0.95);
	float hallowSphere	= Sub(sphereOuter, sphereInner);
	float bodyLength	= (p.y+0.5 + (1.0-clamp(breath-0.5,0.0,1.0)) * 0.4);
	float halfSpere		= Sub(hallowSphere, bodyLength);

	return halfSpere;
}


float JellyTentacle(vec3 p, float inTentacleLength, float inAnimSpeed, float inOffset)
{	
	float tentacleScalePos	= clamp(abs(p.y) / inTentacleLength, 0.0, 1.0);
	
	float tentacleMod		= pow(tentacleScalePos, 1.5) * PI2 - PropulsionTime(0.15, inOffset) * inAnimSpeed;
	float tentacleModifierX	= cos(tentacleMod)*0.4;
	float tentacleModifierY	= cos(tentacleMod + 12.02343)*0.4;
	p.x						+=(tentacleScalePos * tentacleModifierX) * 2.0;
	p.z						+=(tentacleScalePos * tentacleModifierY) * 2.0;
	
	float tentacleThickness	= mix(0.15, 0.01, tentacleScalePos);
	
	p.y						= abs(p.y + inTentacleLength*0.5) - inTentacleLength * 0.5;

	float cylinder			= max(length(p.xz) - tentacleThickness, p.y);

	return cylinder;
}


float JellyFish(vec3 p, float inOffset)
{
	
	vec4 rnd	= Rnd4(1.0 + inOffset);

	float d		= 1000.0;
	
	d			= min(d, JellyBody(p, inOffset));	
	d			= min(d, JellyTentacle(p + vec3(rnd.x, 0, rnd.y)* 0.2, 6.0+rnd.z*4.0, 1.0+rnd.w*0.5, inOffset));
	d			= min(d, JellyTentacle(p + vec3(rnd.w, 0, rnd.x)* 0.2, 6.0+rnd.y*4.0, 1.0+rnd.z*0.5, inOffset));
	d			= min(d, JellyTentacle(p + vec3(rnd.z, 0, rnd.w)* 0.2, 6.0+rnd.x*4.0, 1.0+rnd.y*0.5, inOffset));
	d			= min(d, JellyTentacle(p + vec3(rnd.y, 0, rnd.z)* 0.2, 6.0+rnd.w*4.0, 1.0+rnd.x*0.5, inOffset));

	return d;	
}




vec2 Scene(vec3 p)
{
	// Create an offset
	float rnd		= ModRand(p);
	
	p.y				-=PropulsionTime(0.3, rnd * PI);
	
	// Instantiate
	p				= mod(p, MOD_OFFSET)-MOD_OFFSET*0.5;
	p.y				+=rnd * OFFSET_Y;
	
	float d			= JellyFish(p, rnd);

	return vec2(d, length(p.xz));
}



vec2 GetDepth(vec3 p, vec3 o)
{
	vec2 d	= vec2(1000.0, 0.0);
	float z = 0.0;
	
	for(int i=0; i<46; i++)
	{
		d = Scene(p + o*z);
		if(d.x < 0.01)
			continue;
		z += max(0.01, d.x*0.8);
	}
	
	if(z > DRAW_RANGE)
		z = 0.0;
	
	return vec2(z, d.y);
}


vec3 Normal(vec3 p, float d)
{	
	vec3 n;
	vec2 dn = vec2(d, 0.0);
	n.x	= Scene(p + dn.xyy).x - Scene(p - dn.xyy).x;
	n.y	= Scene(p + dn.yxy).x - Scene(p - dn.yxy).x;
	n.z	= Scene(p + dn.yyx).x - Scene(p - dn.yyx).x;
	return normalize(n);
}


vec3 GetRayDir(vec2 inTC, vec2 inAspectRatio)
{
	return normalize(vec3((-0.5 + inTC)*0.8 * inAspectRatio, 1.0));
}



void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
	
	
	// Construct simple ray
	vec2 aspect		= vec2(iResolution.x/iResolution.y, 1.0);
	vec3 rayOrigin	= vec3(-8.0, -8.0, -0.0);
	vec3 rayDir		= GetRayDir(uv, aspect);
	
	
	// Transform camera orientation
	mat3 ymat		= RotY(PI2 * 0.55);
	mat3 xmat		= RotX(PI2 * 0.5);
	mat3 zmat		= RotZ(PI2 * 0.6);
	mat3 mat		= zmat * ymat * xmat;
	rayOrigin		= mat * rayOrigin;
	rayDir			= mat * rayDir;
	
	// Light
	vec3 lightDir	= rayOrigin;
	
	// Background color
	float bgTrans	= pow(mix(uv.x,uv.y, 0.5), 0.5);
	vec3 color		= vec3(0.0,0.11,0.2) * bgTrans;
	
	vec2 result		= GetDepth(rayOrigin, rayDir);
	if(result.x > 0.0)
	{
		vec3 p				= rayOrigin + rayDir * result.x;
		vec3 n				= Normal(p, 0.01);
		float liFacing		= max(dot(n, normalize(lightDir)), 0.0);
		float liUp			= n.y;
		
		float expScale		= 0.0;//pow(clamp(result.y-0.2, 0.0, 1.0), 4.0);
		float depthFog		= pow(1.0 - result.x / DRAW_RANGE, 2.0);
		
		// Fake skin / sub surface scatting
		vec3 color1			= vec3(0.95,0.6,0.25);
		vec3 color2			= vec3(0.95,0.2,0.1);
		float highLight 	= 1.0 - liFacing;
		vec3 jellyColor		= mix(color2, color1, highLight);
		vec3 colorFacing	= mix(color, jellyColor * depthFog, pow(depthFog, 0.6));
		vec3 colorUp		= vec3(liUp);
		color				= colorFacing + colorUp*pow(depthFog, 2.0) * 0.8;
	}
	
	fragColor = vec4(color, 1.0);
}