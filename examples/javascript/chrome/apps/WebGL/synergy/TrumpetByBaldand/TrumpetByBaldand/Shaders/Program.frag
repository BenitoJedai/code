// Copyright (c) 2013 Andrew Baldwin (baldand)
// License = Attribution-NonCommercial-ShareAlike (http://creativecommons.org/licenses/by-nc-sa/3.0/deed.en_US)

// Comment these next two lines out in case
// it is too slow or doesn't work (e.g. Chrome/Windows)
#define SHADOW
//#define AO

float rnd(vec2 n)
{
  return fract(sin(dot(n.xy, vec2(12.345,67.891)))*12345.6789);
}

float rnd2(vec3 n)
{
  return fract(sin(dot(n.xyz, vec3(12.345,67.891,40.123)))*12345.6789);
}

vec3 rnd3(vec3 n)
{
	vec3 m = floor(n)*.00001 + fract(n);
	const mat3 p = mat3(13.323122,23.5112,21.71123,21.1212,28.7312,11.9312,21.8112,14.7212,61.3934);
	vec3 mp = (31415.9+m)/fract(p*m);
	return fract(mp);
}

vec3 rotateZ( vec3 p, float a)
{
    float c = cos(a);
    float s = sin(a);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xy,p.z);
    return q;
}

vec3 rotateZ0p2( vec3 p )
{
    mat2  m = mat2(0.9800665778412416,-0.19866933079506122,0.19866933079506122,0.9800665778412416);
    vec3  q = vec3(m*p.xy,p.z);
    return q;
}

vec3 rotateY( vec3 p, float a)
{
    float c = cos(a);
    float s = sin(a);
    mat2  m = mat2(c,-s,s,c);
	vec2 xz = m*p.xz;
    vec3  q = vec3(xz.x,p.y,xz.y);
    return q;
}

float tube( vec3 p, vec3 c, float l, float taper )
{
  float tube = length(p.xy-c.xy);
  float outer = tube - c.z - taper;
  float inner = tube - c.z*.8 - taper;
  return max(max(max(outer,-inner),-p.z),-l+p.z);
}

float barY( vec3 p, vec3 c, float l, float taper )
{
  float tube = length(p.xz-c.xy);
  float outer = tube - c.z - taper;
  return max(max(outer,-p.y),-l+p.y);
}

float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float sdTorusXY( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xy)-t.x,p.z);
  return length(q)-t.y;
}

float sdTorusYZ( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.yz)-t.x,p.x);
  return length(q)-t.y;
}

float valve( vec3 p, vec3 c, float x)
{
    float d = length(p.xz-c.xy);
	float d2 = max(max(d - c.z*.35,p.y-6.-x),-p.y+3.5+x);
	float d3 = max(max(d - c.z*1.,-p.y+6.+x),p.y-6.2-x);
	d -= c.z;
	d = max(max(d,-p.y),p.y-5.5);
	return min(min(d,d2),d3);
}

float saw(float t)
{
	return abs(fract(t*.5)*2.-1.)*2.-1.;
}

vec2 trumpet(vec3 pos, float time)
{
	if (pos.x<-4.) return vec2(-(pos.x+3.),0.);
	if (pos.x>4.) return vec2(pos.x-3.,0.);
	if (pos.y<-5.) return vec2(-pos.y-4.,0.);
	if (pos.y>4.) return vec2(pos.y-3.,0.);
	
	float zo = 10.0;
	float bz = -pos.z+zo;
	float bellTaper = mix(.0,1.0/(1.+.5*bz),smoothstep(-8.,0.,-bz));
	float tube1 = tube(pos-vec3(0.,0.,-5.), vec3(0.,0.,.4), 16., bellTaper);
	
	float torus = max(sdTorusYZ(rotateZ0p2(pos-vec3(.4,-2.,-5.)), vec2(2.0406776898823855,.4)),pos.z+4.9);
	float v1 = valve(pos-vec3(0.8,-4.8,0.),vec3(0.,0.,.4),clamp(saw(time*3.5),-.4,0.));
	float v2 = valve(pos-vec3(0.8,-4.8,1.1),vec3(0.,0.,.4),clamp(saw(time*2.5),-.4,0.));
	float v3 = valve(pos-vec3(0.8,-4.8,2.2),vec3(0.,0.,.4),clamp(saw(time*5.5),-.4,0.));
	float bottomfront = tube(pos-vec3(1.6,-3.2,2.8),vec3(0.,0.,.4), 4.1, 0.);	
	float bottomfrontjoin = max(max(sdTorus(pos-vec3(1.0,-3.2,2.8),vec2(.6,.4)),pos.z-2.8),-pos.x+1.);
	float frontbottomend = max(max(sdTorusYZ(pos-vec3(1.6,-1.8,6.9),vec2(1.4,.4)),pos.y),-pos.z+6.9);
	float toptube = tube(pos-vec3(1.6,-.4,-8.),vec3(0.,0.,.4), 14.9, 0.);
	float valves = min(min(v1,v2),min(min(toptube,v3),min(frontbottomend,bottomfrontjoin)));
	float bottomback = tube(pos-vec3(.8,-4.,-5.), vec3(0.,0.,.4), 11.,0.);
	float loop1 = max(sdTorusYZ(pos-vec3(.8,-3.4,6.),vec2(.6,.4)),6.-pos.z);
	float looptop1 = tube(pos-vec3(.8,-2.8,2.3),vec3(0.,0.,.4), 3.7, 0.);
	
	float loop2end = max(sdTorusYZ(pos-vec3(1.6,-3.6,-3.),vec2(.6,.4)),3.+pos.z);
	float loop2top = tube(pos-vec3(1.6,-3.0,-3.),vec3(0.,0.,.4), 2.4, 0.);
	float loop2topjoin =max(max(sdTorus(pos-vec3(1.,-3.0,-.6),vec2(.6,.4)),-pos.z-.6),-pos.x+1.);
	float loop2bot = tube(pos-vec3(1.6,-4.2,-3.),vec3(0.,0.,.4), 2.4, 0.);
	float loop2botjoin =max(max(sdTorus(pos-vec3(1.,-4.2,-.6),vec2(.6,.4)),-pos.z-.6),-pos.x+1.);
	float loop2 = min(min(loop2end,min(loop2bot,loop2botjoin)),min(loop2topjoin,loop2top));
	float loop3 = max(sdTorusXY(pos-vec3(1.2,-3.6,1.1),vec2(.6,.4)),1.-pos.x);
	
	float rim = sdTorusXY(pos-vec3(0.,0.,0.95+zo), vec2(2.45,.08));
	float tubes = min(min(min(min(valves,loop3),looptop1),min(bottomback,loop1)),min(min(tube1,bottomfront),min(torus,loop2)));

	float connect = barY(pos-vec3(1.7,-3.,6.),vec3(0.,0.,.05),2.5,.05*smoothstep(-2.4,-2.5,pos.y)+.05*smoothstep(-1.2,-1.1,pos.y));
	
	vec2 body = vec2(min(min(rim,connect),tubes),1.);
	
	float mouthpiece = tube(pos-vec3(1.6,-.4,-10.),vec3(0.,0.,.4), 16.9, smoothstep(-8.7,-9.5,pos.z)*.35-.1*smoothstep(-7.8,-8.,pos.z));

	vec2 res = body;
	if (mouthpiece<body.x) {
		res.x = mouthpiece;
		res.y = 2.;
	}
	return res;
}

float note(vec3 pos)
{
	float notebottom = length(pos)-1.;
	float notestem = max(max(length(pos.xz-vec2(0.,.8))-.2,-pos.y),pos.y-5.);
	return min(notebottom,notestem);
}

vec2 music(vec3 pos, float time) 
{
	vec3 ptz = vec3(pos.x,pos.y+(2.+.1*pos.z)*saw(.5+pos.z*.1)-.1*pos.z+1.,pos.z);
	vec3 p = vec3(ptz.xy,mod(min(max(ptz.z-2.*fract(3.*time),10.),100.),2.)-1.);
	return vec2(.3333*note(3.*p),3.);
}

vec2 map(vec3 pos, float time)
{
	//return max(2.*sin(pos.x*.5)-pos.y+40.,-length(pos-vec3(50.,50.,0))-10.);
	// Does pos + ray intersect with the sphere area?
	vec2 res = vec2(pos.y+4.85,0.);
	vec2 t = trumpet(pos, time);
	
	vec2 n = music(pos,time);
	
	if (t.x<res.x) res = t;
	if (n.x<res.x) res = n;
	return res;
}

vec3 normal(vec3 pos, float time)
{
	vec3 eps = vec3(0.001,0.,0.);
	float dx = map(pos+eps.xyy,time).x;
	float dy = map(pos+eps.yxy,time).x;
	float dz = map(pos+eps.yyx,time).x;
	float mdx = map(pos-eps.xyy,time).x;
	float mdy = map(pos-eps.yxy,time).x;
	float mdz = map(pos-eps.yyx,time).x;
	return normalize(vec3(dx-mdx,dy-mdy,dz-mdz));
}

vec3 model(vec3 rayOrigin, vec3 rayDirection,float time)
{
	float t = 0.;
	vec3 p;
	float d = 0.;
	bool nothit = true;
	vec2 r;
	for (int i=0;i<200;i++) {
		if (nothit) {
			t += d*.5;
			p = rayOrigin + t * rayDirection;
			r = map(p,time);
			d = r.x;
			nothit = d>t*.001 && t<10000.;
		}
	}
	t += d*.5;
	p = rayOrigin + t * rayDirection;
	vec3 n = normal(p,time);
	float lh = abs(fract(iGlobalTime*.1)*2.-1.);
	lh = 79.*lh*lh*(3.-2.*lh);
	vec3 lightpos = vec3(25.,25.,25.);
	vec3 lightdist = lightpos - p;
	float light = 2.+dot(lightdist,n)*1./length(lightdist);
#ifdef AO
	// AO
	float at = 0.4;
	float dsum = d;
	vec3 ap;
	for (int j=0;j<4;j++) {
		ap = p + at * n;
		float m = map(ap,time).x;
		dsum += m/(at*at);
		at += 0.1;
	}
	float ao = clamp(dsum*.1,0.,1.);
	light = light*ao;
#endif
#ifdef SHADOW
	// March for shadow
	vec3 s;
	float st;
	float sd=0.;
	float sh=1.;
	st=.5;//+.5*rnd2(p+.0123+fract(iGlobalTime*.11298923));
	vec3 shadowRay = normalize(lightpos-p);
	nothit = true;
	for (int i=0;i<40;i++) {
		if (nothit) {
			st += sd*.5;
			s = p + st * shadowRay;
			sd = map(s,time).x;
			sh = min(sh,sd);
			nothit = sd>0.00001;
		}
	}
	light = 5.0*light * clamp(sh,0.1,1.);
#endif
	vec3 m;
	m=.5+.2*abs(fract(p)*2.-1.);
	m=vec3(1.,.9,.4);
	if (r.y==0.) {
		m=vec3(.9);
	} else if (r.y==2.) {
		m=.3+vec3(m.x+m.y+m.z)*.333;
	} else if (r.y==3.) {
		m=vec3(0.1);
	}
	vec3 c = vec3(clamp(1.*light,0.,10.))*vec3(m);
	return c; 
}

vec3 camera(in vec2 sensorCoordinate, in vec3 cameraPosition, in vec3 cameraLookingAt, in vec3 cameraUp)
{
	vec2 uv = 1.-sensorCoordinate;
	vec3 sensorPosition = cameraPosition;
	vec3 direction = normalize(cameraLookingAt - sensorPosition);
	vec3 lensPosition = sensorPosition + 2.*direction;
	const vec2 lensSize = vec2(1.);
    vec2 sensorSize = vec2(iResolution.x/iResolution.y,1.0);
	vec2 offset = sensorSize * (uv - 0.5);
	vec3 right = cross(cameraUp,direction);
	vec3 rayOrigin = sensorPosition + offset.y*cameraUp + offset.x*right;
	vec3 rayDirection = normalize(lensPosition - rayOrigin);
	// Render the scene for this camera pixel
	float rt = 0.;//fract(iGlobalTime);
	vec3 colour = vec3(0.);
	for (int m = 0;m<1;m++) {
		colour += 1.*max(model(rayOrigin, rayDirection,iGlobalTime),vec3(0.));
	}
	// Post-process for display
	vec3 toneMapped = colour/(1.+colour);
	// Random RGB dither noise to avoid any gradient lines
	vec3 dither = vec3(rnd3(vec3(uv.xy,iGlobalTime)))/255.;
	// Return final colour
	return toneMapped + dither;
}

vec3 world(vec2 fragCoord)
{
	// Position camera with interaction
	float anim = .5+.5*sin(iGlobalTime*.1-.5);
	float rotspeed = 10.*(anim+iMouse.x/iResolution.x);
	float radius = (anim+iMouse.y/iResolution.y)*40.;//10.+5.*sin(iGlobalTime*.2);
	//vec3 cameraPos = vec3(iGlobalTime*(10.+iGlobalTime*.1),100.*iMouse.y/iResolution.y,100.0*iMouse.x/iResolution.x);
	//vec3 cameraTarge = cameraPos + vec3(10.,0.,0.);
	//vec3 cameraTarget = vec3(radius*sin(rotspeed),0.+100.*iMouse.y/iResolution.y-50.,radius*cos(rotspeed));
	//vec3 cameraPos = vec3(iGlobalTime,0.,0.);
	vec3 cameraTarget = vec3(0.,-2.5,0.);
	vec3 cameraPos = vec3(radius*sin(rotspeed),-2.5,radius*cos(rotspeed));
	vec3 cameraUp = vec3(0.,1.,0.);
	vec2 uv = fragCoord.xy / iResolution.xy;
	return camera(uv,cameraPos,cameraTarget,cameraUp);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	fragColor = vec4(world(fragCoord),1.);
}