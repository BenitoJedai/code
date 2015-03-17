/*by mu6k, Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

 A shape changing object. Use mouse to move the camera around the object.

 The original idea was to make a box that bends its shape, but then I though it's
 a lot more interesting if there are more shapes involved. I also implemented a 
 plane itersection for the background. I'll be needing more of that plane intersection
 for another idea I had.

 The plane and background are done 100% seperately from the distance field. This allows
 me to calculate the shadow cast on the plane separately from the long loop and I don't
 run out of instructions.

 This should run at high framerates, even at fullscreen...

 25/05/2013:
 - published
 - added comments

 30/05/2013:
 - added the fix which was suggested by iq

 muuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuusk!*/

float hash(float x)
{
	return fract(sin(x*.0127863)*17143.321);
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

vec2 rotate(vec2 v, float angle)
{
	vec2 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.x = cosa*vo.x - sina*vo.y;
	v.y = sina*vo.x + cosa*vo.y;
	return v;
}

vec2 rotate_z(vec2 v, float angle)
{
	vec2 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.x = cosa*vo.x - sina*vo.y;
	v.y = sina*vo.x + cosa*vo.y;
	return v;
}

vec3 rotate_z(vec3 v, float angle)
{
	vec3 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.x = cosa*vo.x - sina*vo.y;
	v.y = sina*vo.x + cosa*vo.y;
	return v;
}

vec3 rotate_y(vec3 v, float angle)
{
	vec3 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.x = cosa*vo.x - sina*vo.z;
	v.z = sina*vo.x + cosa*vo.z;
	return v;
}

vec3 rotate_x(vec3 v, float angle)
{
	vec3 vo = v; float cosa = cos(angle); float sina = sin(angle);
	v.y = cosa*vo.y - sina*vo.z;
	v.z = sina*vo.y + cosa*vo.z;
	return v;
}
	
vec3 cc(vec3 color, float factor,float factor2) //a wierd color modifier
{
	float w = color.x+color.y+color.z;
	return mix(color,vec3(w)*factor,w*factor2);
}

vec2 material0(vec2 uv) //material used for the infinite plane
{
	//make a checkerboard pattern
	vec2 uv2 = mod(uv,vec2(2.,2.));
	uv2 -= mod(uv,vec2(1.0,1.0));
	float d = uv2.x+uv2.y; 
	d = pow(d-1.0,2.0)*.4;
	
	//sample the texture
	float s = texture2D(iChannel0,uv*.3).y+0.5-d;
	d += s*.2;

	//d - diffuse, s - specular
	return vec2(d,s*s*.5+.1);
}

float dist(vec3 p) //the distance function for raymarching
{
	//warp time to get that crazy bending efect
	float time = p.x*sin(iGlobalTime*2.312)*sin(iGlobalTime)*.9+
		p.z*cos(iGlobalTime*3.12)*sin(iGlobalTime)*.9+
		iGlobalTime+max(mod(iGlobalTime*.1,1.0),mod(-iGlobalTime*.1,1.0));
	
	//rotate the space, bp <- transform(p)
	vec3 bp = rotate_z(p,time*.6);
	bp = rotate_y(bp,time*.7);
	bp = rotate_x(bp,time*.5);
	
	//now we have the distance function for 4 shapes
	float diamond = abs(bp.x)+abs(bp.y)+abs(bp.z)-0.6;//length(p+vec3(.0,.0,.0))-0.5;
	float box = max(abs(bp.x),max(abs(bp.y),abs(bp.z)))-0.35;//length(p+vec3(.0,.0,.0))-0.5;
	float torus = pow((0.4-length(bp.xy)),2.0)+pow(bp.z,2.0)-0.02;
	float sphere = length(bp)-.5;
	
	//these values are used to blend them together
	float change = sin(iGlobalTime)*0.99;
	float change2 = sin(iGlobalTime*.4)*0.99;
	
	//set0 <- mix(sphere,diamond), set1 <- mix(torus,box)
	float set0 = mix(sphere,diamond,smoothstep(-1.0,1.0,change));
	float set1 = mix(torus,box,smoothstep(-1.0,1.0,change));

	//return mix(sphere,diamond,torus,box)
	return mix(set0,set1,smoothstep(-1.0,1.0,change2));
}

vec3 normal(vec3 p,float e) //returns the normal, uses the distance function
{
	float d=dist(p);
	return normalize(vec3(dist(p+vec3(e,0,0))-d,dist(p+vec3(0,e,0))-d,dist(p+vec3(0,0,e))-d));
}

vec3 light; //global variable that holds light direction

vec3 plane(vec3 p, vec3 d) //returns the intersection with a predefined plane
{
	//http://en.wikipedia.org/wiki/Line-plane_intersection
	vec3 n = vec3(.0,1.0,.0);
	vec3 p0 = -n*.8;
	float f=dot(p0-p,n)/dot(n,d);
 	return p+d*f;
}

float sun(vec3 d) //makes a bright dot on the sky
{
	float s = dot(d,light);
	s+=1.0; s*=.5;
	s=pow(s*1.001,100.0);
	return s;
}

vec3 stars(vec3 d) //render stars using 3d noise
{
	d.y=abs(d.y);
	float s = noise(d*364.0)*noise(d*699.0);
	s=pow(s,13.0)*10.0;
	return vec3(s);
}

vec3 backdrop(vec3 p,vec3 d) //render background layer, also used for reflection
{
	float s = sun(d); //sun
	vec3 a = vec3(.3,.4,0.7)*(1.0-abs(d.y))*1.5; //atmosphere
	vec3 n = vec3(.0,1.0,.0); //plane normal
	
	float diffuse = dot(n,light)*.5+.5; //diffuse lighting for the plane
	
	float alpha = dot(d,-n); //this coefficient is used
	if (alpha<.0) alpha=.0;  //to blend the plane with the sky
	
	vec3 pp = plane(p,d); //get the intersection position
	float lpp= length(pp);
	vec2 mat = material0(pp.xz); //get the floor material, 
	//mat.x = diffuse coefficient, mat.y = specular coefficient
	
	//calculate the planes color
	vec3 c = mat.x*vec3(.4,.4,.4)*diffuse + (mat.y*.7)*(a*.2+vec3(sun(reflect(d,n))));
	
	alpha=pow(alpha,.5); //make the scene less foggy
	
	return mix(a+s,c,alpha); //mix the plane with the sky
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy - 0.5;
	uv.x *= iResolution.x/iResolution.y; //fix aspect ratio
	vec3 mouse = vec3(iMouse.xy/iResolution.xy - 0.5,iMouse.z-.5);
	
	mouse.x+=iGlobalTime*.00015;
	
	vec3 p = vec3(sin(mouse.x*7.0)*2.1,0.0,cos(mouse.x*7.0)*2.1); //ray position
	vec3 d = rotate_y(vec3(uv,1.0),3.14159-7.0*mouse.x); 
	d = normalize(d); //ray direction
	
	light = normalize(vec3(sin(iGlobalTime),sin(iGlobalTime*.44)+1.2,sin(iGlobalTime*.24)));

	vec3 c; //color
	
	//first we calculate the background
	
	vec3 back = backdrop(p,d);
	c=back;
	
	if (d.y<.0) //ground
	{
		vec3 p = plane(p,d);
		float alpha=0.3/length(p); //shadow alpha
		float s = 1.0; 
		for (int i=0; i<20; i++) //cast shadows for the shapeshifting object
		{
			float ss = dist(p);
			p+=light*ss;
			ss*=2.0;
			ss = min(ss,1.0);
			s*=ss;
		}
		c = mix(c,c*s,alpha)+stars(d)*.2; //mix + add star reflection
	}
	else
	{
		c+=stars(d); // add stars to the sky
	}
	
	//now we do the raymarch, if the ray hits the object, the color
	//is overwritten, otherwise the background color stays . . .
	
	for (int i=0; i<100; i++) //raymarching
	{
		float di = dist(p); //evaluate the distance fucntion
		p+=d*(di*(hash(p.xy+uv.xy)*0.3+.7))*.4; //move the ray

		if (di>5.0) //too far away from the object, escape from this long loop
		{
			break;
		}
		if (di<.01) //close enough to the object
		{
			vec3 n = normal(p,0.002);
			float ao = dist(p+n)*.5+.5; //self occlusion
						
			// a bit more wierd diffuse lighting, but looks great
			float diffuse = (dot(light,n)*.5+.5); 
			diffuse=pow(diffuse,2.0); 
			
			c = vec3(2.4,.4,.4) * diffuse; //object color
			
			//now we add the beautiful raytraced reflection
			c = mix(c,backdrop(p,reflect(d,n)),(1.0+dot(d,n))*.6+.2); 
			c *= ao; //mix in the self occlusion
			break; //escape the loop
		}
	}
	
	c-=vec3(length(uv)*.1); //vignette
	fragColor = vec4(cc(c*.4+hash(uv.xy+c.xy)*.007,.8,0.9),1.0); //post process
	//fragColor = vec4(c,1.0); //uncomment this to remove post processing
}