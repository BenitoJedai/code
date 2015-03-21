// Built from the basics of'Clouds' Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// Edited by NeoNusso into "Cyclonic Sphere"

#define time (iGlobalTime + 23.0)
const vec3 backc_top=vec3(0.1,0.2,0.2);
const vec3 backc_bot=vec3(0.0,0.0,0.0);
const vec3 mainc=vec3(0.2,0.0,0.2);
const float timeScale = 0.2;
const float  PI = 3.14159265;
/*math**********************************************/
float hash( float n )
{
    return fract(sin(n)*43758.5453);
}
float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
	

    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*57.0 + 113.0*p.z;

    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return res;
}
float fbm( vec3 p )
{
	const mat3 m = mat3( 0.00,  0.90,  0.60,
              -0.90,  0.36, -0.48,
              -0.60, -0.48,  0.34 );
    float f;
    f  = 1.300*noise( p ); p = m*p*2.02;
    f += 0.7500*noise( p ); p = m*p*2.03;
    f += 0.3000*noise( p ); p = m*p*2.01;
    f += 0.0800*noise( p ); p = m*p*2.01;
    return f;
}
/*shape**********************************************/
// from http://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm
float lengthSphere(vec3 v,float r)
{
	return length(v) - r;
}
vec3 opTwistY( vec3 p, float rad_twist)
{
    float c = cos(rad_twist*p.y);
    float s = sin(rad_twist*p.y);
    mat2  m = mat2(c,-s,s,c);
	vec2  xz = m*p.xz;
    return  vec3(xz.x, p.y,xz.y );
}
//*Raymarching Functions***************************************************************/
//====================
// distanceFunction
//====================
float distanceFunction(vec3 p)
{
	return lengthSphere(p,3.6 ) ;
}
//Normal
vec3 getNormal(vec3 p, float t)
{
	float e = 0.001*t;
    vec3  eps = vec3(e,0.0,0.0);
    return -normalize( vec3(
    	distanceFunction(p+eps.xyy) - distanceFunction(p-eps.xyy),
		distanceFunction(p+eps.yxy) - distanceFunction(p-eps.yxy),
		distanceFunction(p+eps.yyx) - distanceFunction(p-eps.yyx)
		));
}
//====================
// 3dmap
//====================
vec4 map3d( in vec3 p )
{
	float d = 0.2 - abs(p.y)*pow(length(p.xz),pow(abs(cos(time*timeScale)),0.5))*0.2;

	float f= fbm(p - vec3(.5,0.8,0.0)*iGlobalTime*4.0);
	d += 4.0 * f;
	d = clamp( d, 0.0, 1.0 );
	
	return vec4( mix( vec3(0.4,0.1,0.1), 
				  vec3(1.0,1.0,1.0),
				  d * 0.9), d);
}
//*Shading Functions*************************************************/
vec3 phong(
  in vec3 pt,
  in vec3 prp,
  in vec3 normal,
  in vec3 light,
  in vec3 color,
  in float spec,
  in vec3 ambLight)
{
	vec3 lightv=normalize(light-pt);
	float diffuse=dot(normal,lightv);
	vec3 refl=-reflect(lightv,normal);
	vec3 viewv=normalize(prp-pt);
	float rim = max(0.0, 1.0-dot(viewv,normal));
	float specular=pow(max(dot(refl,viewv),0.0),spec);
	return (max(diffuse*0.5,0.0))*color+ambLight+specular;
}
//*Render Functions*************************************************/
float raymarching(
  in vec3 camPos,
  in vec3 rayDir,
  in int maxite,
  in float precis,
  in float startf,
  in float maxd,
  out int objfound)
{ 
	const vec3 e=vec3(0.1,0,0.0);
	float s=startf;
	vec3 c,p,n;
	float f=startf;
	objfound=1;
	for(int i=0;i<256;i++){
	if (abs(s)<precis||f>maxd||i>maxite) break;
		f+=s;
		p=camPos+rayDir*f;
		s=distanceFunction(p);
	}
	if (f>maxd) objfound=-1;
	return f;
}
//RenderOpaque
vec4 render(
	in vec3 camPos,
	in vec3 rayDir,
	in int maxite,
	in float precis,
	in float startf,
	in float maxd,
	in vec3 background,
	in vec3 light,
	in float spec,
	in vec3 ambLight)
{ 

	int objfound=-1;
	float f=raymarching(camPos,rayDir,maxite,precis,startf,maxd,objfound);
	if (objfound>0){
		vec3 p=camPos+rayDir*f;
		vec3 n = getNormal(p, f);
		return 
		vec4(
			phong(p,camPos,n,light,mainc,spec,ambLight),
			f);
	}
	f=maxd;
	return vec4(background,f);
}

//RenderVolume
vec4 renderRaymarchAccumulate( in vec3 camPos, in vec3 rayDir , vec4 base, float dstep)
{
	vec4 sum = vec4(0, 0, 0, 0);
	float f = 0.0;
	vec3 p = vec3(0.0, 0.0, 0.0);
	for(int i=0; i<120; i++)
	{
		if (f > base.w || sum.a > 0.8 ) continue;
		p = camPos + f*rayDir;
		vec4 col = map3d( opTwistY( p,0.5*cos(time*timeScale))  );
		col.a *= dstep;
		col.rgb *= col.a;
		sum = sum + col*(1.0 - sum.a);	
    	f += max(0.1,dstep*f);
	}
	sum.xyz /= (0.003+sum.w);

	return clamp( sum, 0.0, 1.0 );
}
//*Camera Functions*************************************************/
vec3 camera(
  in vec3 camPos,
  in vec3 camAt,
  in vec3 camUp,
  in float focus,
  in vec2 pixel)
{
	vec2 vPos=-1.0+2.0*pixel/iResolution.xy;
	vec3 camDir=normalize(camAt-camPos);
	vec3 u=normalize(cross(camUp,camDir));
	vec3 v=cross(camDir,u);
	vec3 scrCoord=camPos+camDir*focus+vPos.x*u*iResolution.x/iResolution.y+vPos.y*v;
	return normalize(scrCoord-camPos);
}
vec3 prp_mouse(){
	 float mx=iMouse.x/iResolution.x*PI*2.0;
	 float my=-((iMouse.y+1.0)/iResolution.y*0.07 - 0.035)*PI;
	 return vec3(cos(my)*cos(mx),sin(my),cos(my)*sin(mx))*12.0; //Trackball style camera pos
}
//*Main***************************************************************/
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	const vec3 camUp  =vec3(0,1,0);
	const vec3 camAt  =vec3(0.0,0.0,0.0);
	const float focus=1.5;
	vec3 camPos = camAt+prp_mouse()*2.0;
	vec3 rayDir= camera(camPos,camAt,camUp,focus,fragCoord);
	vec3 light= camPos+vec3(0.0,15.0,0.0);

	const float maxe=0.01;
	const float startf=0.1;
	const float spec=8.0;
	const vec3 ambi=vec3(0.0,0.2,0.2);
	
	float latitude = 0.5+0.5+rayDir.y;
	vec3 back =mix(backc_bot,backc_top,latitude*latitude);

	vec4 c1=render(camPos,rayDir,16,maxe,startf,40.0,back,light,spec,ambi);
	vec4 res = renderRaymarchAccumulate( camPos, rayDir, c1,0.03 );
	
	vec3 col = mix( c1.xyz, res.xyz, res.w*1.2);
	fragColor=vec4(col.xyz,1.0);
}