/*by mu6k, Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

 This started off as test of some noise functions. 
 Well the only proper way to test it is with volumetric rendering. 
 This is not physically accurate, it just looks nice. 
 Use the mouse to rotate and to modify the density.

 02/05/2013:
 - published

 03/05/2013:
 - modified the init of ray variables (more compatible)
 - moved illumination to a separate function
 - added parameters 

 muuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuusk!*/

#define quality 20 // number of steps 20+ is decent for current setup
#define illum_quality 10 // nr of steps for illumination
//#define noise_use_smoothstep //different interpolation for noise functions

//mouse.x rotates the cloud
//mouse.y changes the density of the gas
//note: performance = density * 1.0/quality * 1.0/illum_quality
//note: if you only see a white blob, reduce the quality... there are supposed to be shadows

float hash(float x)
{
	return fract(sin(x*.0127863)*17143.321); //decent hash for noise generation
}

float hash(vec2 x)
{
	return fract(cos(dot(x.xy,vec2(2.31,53.21))*124.123)*412.0); 
}

float hashmix(float x0, float x1, float interp)
{
	x0 = hash(x0);
	x1 = hash(x1);
	#ifdef noise_use_smoothstep
	interp = smoothstep(0.0,1.0,interp);
	#endif
	return mix(x0,x1,interp);
}

float hashmix(vec2 p0, vec2 p1, vec2 interp)
{
	float v0 = hashmix(p0[0]+p0[1]*128.0,p1[0]+p0[1]*128.0,interp[0]);
	float v1 = hashmix(p0[0]+p1[1]*128.0,p1[0]+p1[1]*128.0,interp[0]);
	#ifdef noise_use_smoothstep
	interp = smoothstep(vec2(0.0),vec2(1.0),interp);
	#endif
	return mix(v0,v1,interp[1]);
}

float hashmix(vec3 p0, vec3 p1, vec3 interp)
{
	float v0 = hashmix(p0.xy+vec2(p0.z*143.0,0.0),p1.xy+vec2(p0.z*143.0,0.0),interp.xy);
	float v1 = hashmix(p0.xy+vec2(p1.z*143.0,0.0),p1.xy+vec2(p1.z*143.0,0.0),interp.xy);
	#ifdef noise_use_smoothstep
	interp = smoothstep(vec3(0.0),vec3(1.0),interp);
	#endif
	return mix(v0,v1,interp[2]);
}

float hashmix(vec4 p0, vec4 p1, vec4 interp)
{
	float v0 = hashmix(p0.xyz+vec3(p0.w*17.0,0.0,0.0),p1.xyz+vec3(p0.w*17.0,0.0,0.0),interp.xyz);
	float v1 = hashmix(p0.xyz+vec3(p1.w*17.0,0.0,0.0),p1.xyz+vec3(p1.w*17.0,0.0,0.0),interp.xyz);
	#ifdef noise_use_smoothstep
	interp = smoothstep(vec4(0.0),vec4(1.0),interp);
	#endif
	return mix(v0,v1,interp[3]);
}

float noise(float p) // 1D noise
{
	float pm = mod(p,1.0);
	float pd = p-pm;
	return hashmix(pd,pd+1.0,pm);
}

float noise(vec2 p) // 2D noise
{
	vec2 pm = mod(p,1.0);
	vec2 pd = p-pm;
	return hashmix(pd,(pd+vec2(1.0,1.0)), pm);
}

float noise(vec3 p) // 3D noise
{
	vec3 pm = mod(p,1.0);
	vec3 pd = p-pm;
	return hashmix(pd,(pd+vec3(1.0,1.0,1.0)), pm);
}

float noise(vec4 p) // 4D noise
{
	vec4 pm = mod(p,1.0);
	vec4 pd = p-pm;
	return hashmix(pd,(pd+vec4(1.0,1.0,1.0,1.0)), pm);
}

vec3 cc(vec3 color, float factor,float factor2) // color modifier
{
	float w = color.x+color.y+color.z;
	return mix(color,vec3(w)*factor,w*factor2);
}

vec3 ldir = normalize(vec3(-1.0,-1.0,-1.0)); //light direction

float density(vec3 p) //density function for the cloud
{
	vec4 xp = vec4(p*0.4,iGlobalTime*0.1+noise(p));
	float nv=pow(pow(noise(xp),2.0)*2.1,	2.5)*(10.0-length(p.xyz));
	nv = max(0.0,nv); //negative density is illegal.
	nv = min(1.0,nv); //high density causes artifacts
	return nv;
}

float illumination(vec3 p,float density_coef)
{
	vec3 l = ldir;
	float il = 1.0;
	float ill = 1.0;
		
	for(int i=0; i<int(illum_quality); i++) //illumination
	{
		il-=density(p-l*hash(p.xy+vec2(il,p.z))*0.5)*density_coef;
		p-=l;
		if (il <= 0.0)
		{
			il=0.0;
			break; //light can't reach this point in the cloud
		}
		if (il == ill)
		{
			break; //we already know the amount of light that reaches this point
			//(well not exactly but it increases performance A LOT)
		}
		ill = il;
	}
	
	return il;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy - 0.5;
	uv.x = uv.x*iResolution.x/iResolution.y;
	vec2 m = iMouse.xy / iResolution.xy - 0.5;
	
	
	float rot = m.x*3.14159*2.0;
	
	vec3 d = vec3(sin(rot),.0,cos(rot)); //ray direction
	vec3 p = -20.0*d; //ray position
	
	vec3 t = cross(d,vec3(0.0,1.0,0.0)); 
	d.x = d.x+uv.x*t.x; //perspective projection
	d.y = uv.y;
	d.z = d.z +uv.x*t.z;
	
	d=normalize(d);
	
	p+=d*10.0; //move the ray forward by 10.0 units 
	//because cloud is a inside a 10.0 radius sphere
	//and initial ray position is 20.0 units away
	
	float illum_acc = 0.0;
	float dense_acc = 0.0;
	float density_coef = m.y*.35+0.23;
	float quality_coef = 20.0/float(quality);
	
	for (int i=0; i<quality; i++) //max 20 step raycast
	{
		float ld0=length(p);
		
		p+=d*quality_coef;
		float ld1=length(p);
		
		if(ld1>ld0&&ld1>10.0) 
		{
			break; //break condition: ray is moving away from the sphere
			//and is not inside the sphere
		}
		
		float nv = density(p+d*hash(uv+vec2(iGlobalTime,dense_acc))*0.25)*density_coef*quality_coef;
		//evaluate the density function
		
		vec3 sp = p;
		dense_acc+=nv;
		
		if (dense_acc>1.0)
		{
			dense_acc=1.0; //break condition: following steps do not contribute 
			break; //to the color because it's occluded by the gas
		}
		
		float il = illumination(p,density_coef);
		
		illum_acc+=max(0.0,nv*il*(1.0-dense_acc)); 
		//nv - alpha of current point
		//il - illumination of current point
		//1.0-dense_acc - how much is visible of this point
	}

	vec3 illum_color = vec3(1.1,0.85,0.3)*illum_acc*1.55;
	
	float sun = dot(d,-ldir); sun=.5*sun+.5; sun = pow(sun,100.0);
	vec3 sky_color = vec3(0.3,0.5,0.8);
	
	vec3 dense_color = sky_color*0.51; //color of the dark part of the cloud
	
	sky_color=sky_color*(1.0-uv.y*0.2)+vec3(sun);

	vec3 color = mix(sky_color,dense_color+illum_color,smoothstep(0.0,1.0,dense_acc)); color-=length(uv)*0.2;
	color+=hash(color.xy+uv)*0.01; //kill all color banding
	color =cc(color,0.5,0.4); 
	
	fragColor = vec4(color,1.0);
}