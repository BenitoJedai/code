#define PI					3.14159265359
#define PIH 				(PI/2.0)
#define PIQ 				(PI/4.0)
#define PI2 				(PI*2.0)
#define MARCH_SCALE			0.65
#define MARCH_STEPS			72
#define PRECISION			0.001
#define GLOW_SPHERE_SIZE	0.1
#define LIGHT_COLOR			vec3(1.0, 0.9, 0.6)*1.6

float GetTime()									{return iGlobalTime * 0.1;}
vec3 GetRayDir(vec2 inTC, vec2 inAspectRatio)	{return normalize(vec3((-0.5 + inTC)*1.2 * inAspectRatio, 1.0));}
mat3 RotX(float a)								{float s = sin(a); float c = cos(a); return mat3(1,0,0,0,c,s,0,-s,c);}
mat3 RotY(float a)								{float s = sin(a); float c = cos(a); return mat3(c,0,-s,0,1,0,s,0,c);}
mat3 tm(mat3 m)
{
	return mat3(
		m[0][0], m[1][0], m[2][0],
		m[0][1], m[1][1], m[2][1],
		m[0][2], m[1][2], m[2][2]
	);
}


// Any fraction  in range of (0.0, 0.5] will suffice. 1.0/3.0 = 6 instantiations
vec3 InstantiateRotY(vec3 p, float inPiFrac)
{
	float rad		= mod(atan(p.x, p.z) +  PIH*inPiFrac, PI*inPiFrac) - PIH*inPiFrac;
	p.xz			= vec2(sin(rad), cos(rad)) * length(p.xz);
	return p;
}


float Ground(vec3 p)
{
	return length(p.y-0.01);
}


float GlowSphere(vec3 p)
{
	p.y -= 2.0;
	return length(p) - GLOW_SPHERE_SIZE;
}

float Leaf(vec3 p, float offset)
{
	p.y = clamp(p.y, 0.0, 2.5); // set range
	p.z -= offset*0.95 - 0.03;	// offset
	p.y	= mod(p.y, 0.1)-0.05;	// repeat
	p.z -= abs(p.y + 0.05)*1.2; // stretch
	
	return length(p) - 0.02;
}

float Branch(vec3 p, float rnd)
{
	float hMax		= 2.5;
	float hScalePos	= clamp(p.y / hMax, 0.0, 1.0);
	float h			= abs(p.y-hMax*0.5) - hMax*0.5;
	
	p.x				+= 0.12;
	// Bend-Rotate Body
	p				= RotY(hScalePos * PI * 1.5) * p;
	
	// Stretch
	float animS		= mix(0.1, 1.0, sin(rnd * 123.1211)*0.5+0.5);
	float anim		=  -sin(hScalePos * PI*4.0 + GetTime()*PI*animS ) * 0.1 * pow(hScalePos, 8.0);
	p.x				+= sin(hScalePos * PI * 0.8) * hScalePos + anim;
	
	// Twist
	p				= RotY(pow(hScalePos, 3.0) * PI2*2.0) * p;
	
	// Y - axis rotate-instantiation
	p				= InstantiateRotY(p, 1.0/3.0);
		
	float wl		= mix(0.2, 0.004, hScalePos);
	float branch 	= max(max(p.x-wl, p.z-wl), h);
	float leaf		= Leaf(p, wl);
	return min(branch, leaf);
}

float Branches(vec3 p)
{
	float depthCurr	= 1000.0;
	depthCurr = min(depthCurr, Branch(p, 1.0));
	depthCurr = min(depthCurr, Branch(RotY(PI2 * (1.0/3.0)) * p, 2.0));
	depthCurr = min(depthCurr, Branch(RotY(PI2 * (2.0/3.0)) * p, 3.0));
	return depthCurr;
}


#define OBJ(inOBJ, inMatID) depthPrev=depthCurr; depthCurr=min(depthCurr, inOBJ); if(depthCurr < depthPrev) matID = inMatID;
vec2 Scene(vec3 p)
{
	float depthCurr	= 1000.0;
	float depthPrev	= 1000.0;
	float matID		= -1.0;
		
	OBJ(Ground(p), 0.0);
	OBJ(Branches(p), 1.0);
		
	return vec2(depthCurr, matID);
}

float SceneGlow(vec3 p)
{		
	return GlowSphere(p);
}

vec3 GasMat(vec3 p, float lightDist)
{
	float dist	= clamp(length(p.xz) * 0.3333, 0.0, 1.0);
	float fade	= pow(1.0 / (1.0 + lightDist), 2.0) * 3.0;
	return  mix(vec3(0.8, 1.0, 0.2), vec3(1.0, 0.5, 0.25), dist) * fade;
}

vec3 GetMaterial(vec3 ro, vec3 rd, vec3 p, vec3 n, vec2 res, vec3 inBackgroundColor)
{
	vec3 color			= inBackgroundColor;
	vec3 lightPos		= vec3(0.0, 2.0, 0.0);
	vec3 lightDir		= lightPos-p;
	float lightDist		= length(lightDir);
	lightDir			= lightDir / lightDist;
	float directLight	= dot(n, lightDir);
		
	if(res.y == 0.0)
	{
		color				= GasMat(p, lightDist);
	}
	
	if(res.y == 1.0)
	{
		// "sss"
		float sss			= mix( pow(directLight*0.5 + 0.5, 2.1), clamp(1.0-directLight, 0.0, 1.0), 0.4);
		color				=vec3(0.8, 0.6, 0.5) * sss * pow(1.0 / (1.0 + max(lightDist, 0.0)), 1.6) * 0.5;
		
		float lightFacing	= pow(max(directLight, 0.0), 2.0);
		color				+= vec3(0.6,0.7,0.45) * lightFacing * pow(1.0 / (1.0 + max(lightDist, 0.0)), 4.0) * 1.5;
		
		// Fade into the ground "gas"
		color				= mix(GasMat(p, lightDist), color * LIGHT_COLOR, clamp(p.y, 0.0, 1.0));
	}
	
	return color;
}

vec2 March(vec3 p, vec3 o)
{
	vec2 r		= vec2(1000.0, -1.0);
	float z		= 0.0;
	float matID	= -1.0;
	
	for(int i=0; i<MARCH_STEPS; i++)
	{
		r = Scene(p + o*z);
		if(r.x < PRECISION)
			continue;
		z		+=r.x*MARCH_SCALE;
		matID	= r.y;
	}

	
	return vec2(z, matID);
}


vec2 GlowMarch(vec3 p, vec3 o)
{
	float d		= 1000.0;
	float z		= 0.0;
	float acc	= 0.0;
	
	for(int i=0; i<15; i++)
	{
		d	= SceneGlow(p + o*z);
		acc	+= max(1.0 - d, 0.0);
		z	+= max(0.0, d);
	}
	
	return vec2(acc / 16.0, z);
}

vec3 DistFieldNormal(vec3 p)
{	
	vec3 n;
	vec2 dn = vec2(0.01, 0.0);
	n.x	= Scene(p + dn.xyy).x - Scene(p - dn.xyy).x;
	n.y	= Scene(p + dn.yxy).x - Scene(p - dn.yxy).x;
	n.z	= Scene(p + dn.yyx).x - Scene(p - dn.yyx).x;
	return normalize(n);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
	vec2 uv = fragCoord.xy / iResolution.xy;
	
	// Construct simple ray
	vec2 aspect		= vec2(iResolution.x/iResolution.y, 1.0);
	vec3 ro			= vec3(0, 1.2, -3);
	vec3 rd			= GetRayDir(uv, aspect);
	
	
	// Transform camera orientation
	mat3 ymat		= RotY(PI2 * GetTime()*-0.03);
	mat3 xmat		= RotX(PI2 * 0.10);
	mat3 mat		= ymat * xmat;
	
	ro				= mat * ro;
	rd				= mat * rd;
		
	// Background color
	vec3 color		= vec3(0.0, 0.0, 0.0);
	
	vec2 res			= March(ro, rd);
	vec3 p;
	vec3 n;
	if(res.x > 0.0)
	{
		p		= ro + rd*res.x;
		n		= DistFieldNormal(p);
		color	= GetMaterial(ro, rd, p, n, res, color);
	}

	vec2 gres	= GlowMarch(ro, rd);
	float glowi	= 1.0;
	if(res.y == 1.0)
	{
		float v = (tm(ymat) * p).z;
		glowi = smoothstep(0.0, 0.5, max(v, 0.0));
	}
			
	color		+=gres.x * glowi * LIGHT_COLOR;
		
	fragColor = vec4(color, 1.0);
}