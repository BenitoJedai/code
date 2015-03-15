// Inspired by iq raymarched toys
// Created by Ramon Viladomat

#define STEP_REDUCTION 0.5
#define PI 3.1415
#define EPSILON 0.002

#define REACTABLE_SCREENWIDTH 0.025

#define REACTABLE_RADIUS 1.5
#define REACTABLE_BORDER_SIZE 0.1

#define REACTABLE_HEIGHT 1.5
#define REACTABLE_BORDER_HEIGHT 0.05

#define REACTABLE_LEG_AXE_DISP 1.13
#define SCREEN_OUTPUT_RADIUS 0.03

#define MODULE_SIZE 0.12
#define MODULE_HEIGHT 0.04
#define MODULE_RADIUS 0.07

#define BPM 128.0

#define WAVE_AMP 0.2
#define WAVE_WIDTH 0.01

#define UI_RADIUS 0.23
#define UI_WIDTH  0.025

vec3 module1Pos = vec3(0.25,0.0,-0.4);
vec3 module2Pos = vec3(0.0,0.0,-1.1);
vec3 module3Pos = vec3(-0.4,0.0,0.4);
vec3 module4Pos = vec3(0.2,0.0,1.0);
vec4 connection = vec4(0.25,-0.4,1.0,0.0);

/////////////////////////
// DISTANCE PRIMITIVES //
/////////////////////////

float distBox( in vec3 p, in vec3 b )
{
  return length(max(abs(p)-b,0.0));
}

float distSquaredCylinder( vec3 p, vec3 d, float r)
{
	return max(length(max((abs(p.xz)-(d.xz-r)),0.0))-r,abs(p.y)-d.y);
}

////////////////
// MORPHOLOGY //
////////////////

vec2 distBody( in vec3 p )
{
	float distXZ = length(p.xz) - REACTABLE_RADIUS;
		
	float screen = max(distXZ, abs(p.y) - REACTABLE_SCREENWIDTH);
	
	float borderXZ = abs(distXZ-REACTABLE_BORDER_SIZE)-REACTABLE_BORDER_SIZE;
	float border = max(borderXZ,abs(abs(p.y + REACTABLE_HEIGHT)-REACTABLE_HEIGHT) - (REACTABLE_SCREENWIDTH+REACTABLE_BORDER_HEIGHT));
	
	vec3 q = p;
	q.xz = abs(q.xz) - REACTABLE_LEG_AXE_DISP;
	q.y += REACTABLE_HEIGHT;
	float legs = distBox(q,vec3(0.05,REACTABLE_HEIGHT,0.05));
		
	//cloth
	vec3 q2 = p;
	q2.y += REACTABLE_HEIGHT;
	q2.y = abs(q2.y);
	float reduction = smoothstep(0.0,REACTABLE_HEIGHT,q2.y);
	float cloth = max(q2.y-REACTABLE_HEIGHT,distXZ + mix(0.15,0.0,reduction));
		
	float dist = min(cloth,min(min(screen,border),legs));
	float innerRadius = step(-EPSILON,distXZ);
	float mat = mix(mix(1.0,2.0,innerRadius),mix(0.0,1.0,innerRadius),step(-REACTABLE_BORDER_HEIGHT,p.y));
		
	return vec2(dist,mat);
}

vec2 distModuleObject(in vec3 p)
{
	p.y -= MODULE_HEIGHT;
	return vec2(distSquaredCylinder(p,vec3(MODULE_SIZE,MODULE_HEIGHT,MODULE_SIZE),MODULE_RADIUS),3.0);
}

vec2 distCubeObject(in vec3 p)
{
	p.y -= MODULE_SIZE;
	return vec2(distBox(p,vec3(MODULE_SIZE,MODULE_SIZE,MODULE_SIZE)),3.0);
}

vec2 map( in vec3 p, out vec4 glows)
{	
	vec2 res = distBody(p);

	vec2 filter = distModuleObject(p-module1Pos);
	vec2 filter2 = distModuleObject(p-module3Pos);
	vec2 cube = distCubeObject(p-module2Pos);
	vec2 cube2 = distCubeObject(p-module4Pos);
	
	if (filter.x < res.x) res = filter; 
	if (filter2.x < res.x) res = filter2; 
	if (cube.x < res.x) res = cube; 
	if (cube2.x < res.x) res = cube2; 
	
	glows = vec4(filter.x,cube.x,filter2.x,cube2.x);
	
	return res;
}

///////////////
// MATERIALS //
///////////////

float factorObj( in vec2 p, in vec2 from, in vec2 to, float power, vec2 wavemix)
{
	//Wave
	vec2 v = to - from; 
	float len = length(v);
	v /= len;
	vec2 vv = vec2(-v.y,v.x);
	vec2 lp = p - from; 
	vec2 localCoord = vec2(dot(lp,v),dot(lp,vv));
	
	float xBound = localCoord.x / len;
	float outOfBounds = (1.0 - step(1.0,xBound)) * step(0.0,xBound);
	
	float wave1 = (2.0*(texture2D(iChannel0, vec2(xBound, 0.75)).x - 0.5));
	float wave2 = (2.0*(texture2D(iChannel0, vec2(xBound*0.5, 0.75)).x - 0.5));
	float wave = WAVE_AMP*((wave1*wavemix.x)+(wave2*wavemix.y))*power*sin(3.1415*xBound);
	
	float factorWave = mix(0.0,0.5*(1.0 - smoothstep(0.0,WAVE_WIDTH,abs(localCoord.y+wave))),outOfBounds);
	
	//UI
	
	float angSin = normalize(localCoord).y;
	float distUI1 = abs((length(localCoord))-UI_RADIUS)-(UI_WIDTH*mix(1.0,0.2,step(0.0,localCoord.y)));
	float factorUI = 0.5*step(0.0,-distUI1) * step(0.3,abs(angSin)); 

	float factorAmpUI = 0.5*(1.0 - step(0.0,length(localCoord - vec2(0.0,UI_RADIUS)) - UI_WIDTH));
	
	return factorWave + factorUI + factorAmpUI;
}



vec4 colorScreen( in vec2 pos ) 
{	
	//Beat Waves
	float modfactor = 60.0/BPM;
	
	float distToOutput = length(pos); 
	float maxradius = 0.5*REACTABLE_RADIUS;
	float actualWaveRadius = maxradius*mod(iChannelTime[0],modfactor)/modfactor;
	
	float beatWeight = 0.4+0.6*(1.0 - step(0.5,mod(iChannelTime[0],4.0*modfactor)/modfactor));
	
	float waveValue = 1.0 - smoothstep(0.0,0.05,abs(distToOutput-actualWaveRadius));
	float factor = beatWeight * (maxradius-actualWaveRadius) * waveValue; 
	
	//OUTPUT
	factor = mix(1.0,factor,step(SCREEN_OUTPUT_RADIUS,distToOutput));

	float obj1 = factorObj(pos,module1Pos.xz,vec2(0.0), connection.z,vec2(1.0,0.0));
	float obj2 = factorObj(pos,module2Pos.xz,connection.xy,1.0,vec2(1.0,0.0));
	float obj3 = factorObj(pos,module3Pos.xz,vec2(0.0),1.0,vec2(0.5,0.5));
	float obj4 = factorObj(pos,module4Pos.xz,module3Pos.xz,1.0,vec2(0.0,1.0));
	
	factor = factor + obj1 + obj2 + obj3 + obj4;
			
	//FINAL SCREEN MIX
	return mix(vec4(0.00,0.00,0.22,0.05),vec4(0.50,0.50,0.50,0.2), factor);
}

vec4 calcColor( in vec3 pos, in vec3 nor, float material )
{
	vec4 materialColor = vec4(0.0);
	
		 if(material < 0.5) materialColor = colorScreen(pos.xz);
	else if(material < 1.5) materialColor = vec4(0.0,0.0,0.0,0.05);
	else if(material < 2.5) materialColor = vec4(0.15,0.15,0.15,0.2);
	else if(material < 3.5) materialColor = vec4(0.2,0.2,0.2,0.2);
		
	return materialColor;
}

///////////
// LOGIC //
///////////

void ComputeLogic(in vec3 ro, in vec3 rd)
{
	//Intersect with the screen and move Cube
	module2Pos = ro+rd*(-ro.y/rd.y);
	float len = min(length(module2Pos.xz),(REACTABLE_RADIUS - sqrt(2.0*MODULE_SIZE*MODULE_SIZE)));
	module2Pos = normalize(module2Pos)*len;
		
	float lenToFilter = length(module1Pos.xz - module2Pos.xz);
	float lenToFilter2 = length(module3Pos.xz - module2Pos.xz);
	float lenFilter = length(module1Pos.xz);
	float lenFilter2 = length(module3Pos.xz);
	
	connection.z = step(lenFilter,len) * step(lenToFilter,len);
	connection.w = step(lenFilter2,len) * step(lenToFilter2,len);	
	
	connection.xy = mix(mix(vec2(0.0),module1Pos.xz,connection.z),module3Pos.xz,connection.w);
}

//////////////////////
// MAIN RAY/SHADING //
//////////////////////

vec2 intersect( in vec3 ro, in vec3 rd, out vec4 glows)
{
	glows = vec4(9999.0);
	vec2 res = vec2( 2.0*EPSILON, -1.0);
    float t = 0.0;
    for( int i=0; i<150; i++ )
    {
		if( abs(res.x)<EPSILON ) continue;
		vec4 thisglows;
		res = map( ro+rd*t, thisglows);
		glows = min(glows,thisglows);
		t += res.x*STEP_REDUCTION;
    }
	if( abs(res.x)>EPSILON ) res.y = -1.0;
	res.x = t;
    return res;
}

vec3 calcNormal( in vec3 pos )
{
	vec4 dummy;
    vec3 eps = vec3(EPSILON,0.0,0.0);
	return normalize( vec3( map(pos+eps.xyy,dummy).x - map(pos-eps.xyy,dummy).x, map(pos+eps.yxy,dummy).x - map(pos-eps.yxy,dummy).x, map(pos+eps.yyx,dummy).x - map(pos-eps.yyx,dummy).x) );
}

float softShadow( in vec3 ro, in vec3 rd, float mint, float k )
{
	vec4 dummy;
    float res = 1.0;
    float t = mint;
    for( int i=0; i<45; i++ )
    {
        float h = map(ro + rd*t,dummy).x;
        res = min( res, k*h/t );
        t += h*STEP_REDUCTION;
    }
    return clamp(res,0.0,1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x/iResolution.y;
	
	// Compute Camera
	float introFactor = smoothstep(0.0,4.0,iGlobalTime);
	
	float camAngle = mix(0.0,1.2*PI,introFactor);
	float camDist = mix(4.0,2.45,introFactor); 
	
    vec3 camPosition = vec3(camDist*sin(camAngle), 1.5, camDist*cos(camAngle));
    vec3 camTarget	 = vec3( 0.0, -0.50, 0.0 );
    vec3 camFront 	 = normalize( camTarget - camPosition );
    vec3 camRight 	 = normalize( cross(camFront,vec3(0.0,1.0,0.0) ) );
    vec3 camUp 		 = normalize( cross(camRight,camFront));
    vec3 rayDir 	 = normalize( p.x*camRight + p.y*camUp + 2.0*camFront );

	// update logic
	vec2 mousePos = -1.0 + 2.0 * iMouse.xy/iResolution.xy;
	mousePos.x *= iResolution.x/iResolution.y;
	
	vec3 mouseRay = normalize( mousePos.x*camRight + mousePos.y*camUp + 2.0*camFront );
	
	if (introFactor > 1.0-EPSILON)
	{
		ComputeLogic(camPosition,mouseRay);
	}
	
	// light compute
	vec3 lightPos1 = vec3(2.5,2.5,2.0);
	vec3 lightColor1 = vec3(1.0,1.0,1.0);
	
	// Start Ray
    vec3 finalcolor = vec3(0.0);
	vec4 glows = vec4(0.0);
	float attenuation = 1.0;
	for( int reflectCount=0; reflectCount<2; reflectCount++ )
	{
		// Compute color for single ray
    	vec2 tmat = intersect(camPosition,rayDir,glows);

		//glow ( use the ray min distance in order to compute real material emision )
		vec4 glowamount = pow(max(1.0 - glows,0.0),vec4(30.0));
		
		float beat1 = pow(texture2D( iChannel0, vec2( 0.01, 0.25 ) ).x * texture2D( iChannel0, vec2( 0.07, 0.25 ) ).x,4.0);
		float beat2 = 2.0*pow(texture2D( iChannel0, vec2( 0.15, 0.25 ) ).x * texture2D( iChannel0, vec2( 0.30, 0.25 ) ).x,2.0);
		
		beat1 = 0.2+0.8*clamp(beat1,0.0,1.0);
		beat2 = 0.2+0.8*clamp(beat2,0.0,1.0);
		
		vec3 glow = ((glowamount.x*connection.z)+glowamount.y)*beat1*vec3(0.0,0.7,0.0) +
					glowamount.w*beat2*vec3(0.7,0.0,0.0) +
					glowamount.z*vec3(0.7*beat2,0.7*mix(0.0,beat1,connection.w),0.0);
		
		finalcolor += attenuation*glow;				
		
		if (tmat.y > -0.5)
		{
			// results extraction
			vec3 position 	= camPosition + tmat.x*rayDir;
			vec3 normal 	= normalize(calcNormal(position));
			vec3 reflDir 	= reflect(rayDir,normal);
			
			// lights and materials 
			vec4 materialColor 	= calcColor( position, normal, tmat.y );
			
			float ambient  		= 0.7 + 0.3*normal.y;
			vec3 ambientColor 	= ambient*materialColor.rgb;
			
			//light 1
			vec3 lightDir1 	= normalize(lightPos1 - position);
			
			float diffuse1  = max(dot(normal,lightDir1),0.0);
			float specular1 = pow(clamp(dot(lightDir1,reflDir),0.0,1.0),3.0);
			float shadow1   = softShadow( position, lightDir1, 0.01, 10.0 );
			
			vec3 diffuseColor1 = diffuse1*lightColor1*materialColor.rgb;
			vec3 specularColor1 = specular1*materialColor.w*lightColor1;
			vec3 ilumColor1 = shadow1*(diffuseColor1 + specularColor1);
			
			// mixing lights
			finalcolor += attenuation*mix(ambientColor,ilumColor1,0.8);
			
			// prepare next ray for reflections 
			rayDir = reflDir;
			attenuation *= 2.0*materialColor.w;
			camPosition = position + EPSILON*normal;
		}
		else
		{
			finalcolor += attenuation*vec3(0.2,0.2,0.2);
			attenuation = 0.0;
		}
			
	}
	
	// desaturation, gamma correction and simple vignette
	finalcolor = pow(mix( finalcolor, vec3(dot(finalcolor,vec3(0.33))), 0.3 ), vec3(0.45));
	finalcolor *= mix(1.0,0.6,length(p)*0.5);
	
    fragColor = vec4( finalcolor,1.0 );
}