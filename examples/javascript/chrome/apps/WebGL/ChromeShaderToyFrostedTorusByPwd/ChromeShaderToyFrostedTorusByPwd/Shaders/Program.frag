#define pi 3.14159265
#define R(p, a) p=cos(a)*p+sin(a)*vec2(p.y, -p.x)
#define hsv(h,s,v) mix(vec3(1.), clamp((abs(fract(h+vec3(1., 1., 3.)/3.)*6.-3.)-1.), 0., 1.), s)*v

vec3 csb( vec3 f, float c, float s, float b) {
	
	return mix(vec3(.5), mix(vec3(dot(vec3(.2125, .7154, .0721), f*b)), f*b, s), c);
	
}

float sn(vec3 p) {
	
	const vec4 v = vec4(17.*17.,34.,1.,7.);
	const vec2 c = vec2(1./6.,1./3.);
	const vec4 d = vec4(0.,.5,1.,2.);
	const float n = 1./v.w;
	
	vec3 i = floor(p + dot(p, c.yyy));
	vec3 x0 = p - i + dot(i, c.xxx);
	
	vec3 l = step(x0.xyz, x0.yzx);
	vec3 g = 1.-l;
	
	vec3 i1 = g.xyz * l.zxy;
	vec3 i2 = max(g.xyz, l.zxy);
	
	vec4 r = i.z + vec4(0., i1.z, i2.z, 1.);
	r = mod((r*v.y+v.z)*r, v.x);
	r += i.y + vec4(0., i1.y, i2.y, 1.);
	r = mod((r*v.y+v.z)*r, v.x);
	r += i.x + vec4(0., i1.x, i2.x, 1.);
	r = mod((r*v.y+v.z)*r, v.x);
	r = floor(r);
	
	vec3 x1 = x0 - i1 + 1. * c.x;
	vec3 x2 = x0 - i2 + 2. * c.x;
	vec3 x3 = x0 - 1. + 3.  * c.x;
	
	vec3 ns = n * d.wyz - d.xzx;
	r -= v.w*v.w*floor(r*ns.z*ns.z);
	vec4 a = floor(r*ns.z);
	vec4 b = floor(r - v.w*a);
	
	vec4 x = a*ns.x + ns.y;
	vec4 y = b*ns.x + ns.y;
	vec4 h = 1. - abs(x) - abs(y);
	
	vec4 b0 = vec4(x.xy, y.xy);
	vec4 b1 = vec4(x.zw, y.zw);
	
	vec4 s0 = floor(b0)*2.+1.;
	vec4 s1 = floor(b1)*2.+1.;
	vec4 sh = floor(h);
	
	vec4 a0 = b0.xzyw + s0.xzyw*sh.xxyy;
	vec4 a1 = b1.xzyw + s1.xzyw*sh.zzww;
	vec4 t = vec4(
		dot(vec3(a0.xy, h.x), x0),
		dot(vec3(a0.zw, h.y), x1),
		dot(vec3(a1.xy, h.z), x2),
		dot(vec3(a1.zw, h.w), x3)
	);
	
	vec4 s = vec4(
		dot(x0, x0),
		dot(x1, x1),
		dot(x2, x2),
		dot(x3, x3)
	);
	
	s = max(.6 - s, 0.);
	s *= s;
	s *= s;
	
	return 48. * dot(s, t);
	
}

float fsn(vec3 p) {
	
	return sn(p*.06125)*.5 + sn(p*.125)*.25 + sn(p*.25)*.125;
		
}

vec3 cellpos;
vec3 signvec;
vec3 subpos;
float fsign;

float cf(vec3 pos) {
	
	cellpos=pos-floor(pos);
	
	signvec=2.0*step(0.5,cellpos)-1.0;
	fsign=signvec.x*signvec.y*signvec.z;
	
	subpos=abs(abs(cellpos-0.5)-0.25);
	
	return fsign*(max(max(subpos.x,subpos.y),subpos.z)-0.25);
	
}
	

float torus(vec3 p) {
		
	return length(vec2(length(p.xz) -4.24, p.y)) -1.19;
		
}

float m;
float f(vec3 p) {

	m = 0.;
	R(p.yz, pi/6.);
	vec3 q=p;
	R(p.xz, iGlobalTime);
	R(p.yz, .5* iGlobalTime);

	float d = torus(p) +fsn(p*100.0)*0.015;
	float md = mix( cf(p), d, .72 + sin(iGlobalTime) * .185 );
	float nd = dot(q+vec3(0.,2., 0.), vec3(0., 1.,0.));
	m = step(nd, md);
	return min(md,nd);
	
}

vec3 g(vec3 p) {
	vec2 e = vec2(.0001, .0);
	return normalize(vec3(f(p+e.xyy) - f(p-e.xyy),f(p+e.yxy) - f(p-e.yxy),f(p+e.yyx) - f(p-e.yyx)));
}


float ao(vec3 p, vec3 n, float d) {

	float s = sign(d);
	float o = s*.5+.5;
	for (float i = 5.0; i > 0.; --i) {
		o -= (i*d - f(p+n*i*d*s)) / exp2(i);
	}
	return o;
	
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 wh = vec2( iResolution.x, iResolution.y );
	vec3 p = vec3(0.,2.,4.);
	vec3 d = vec3((fragCoord.xy/(0.5*wh)-1.)*vec2(wh.x/wh.y,1.0), 0.)-p;
	d = normalize(d);
	p.z += 8.0 + sin(iGlobalTime) * 2.0;
	p.y += 2.0;	
	float b=0.,r=0.,l=0.,a,s;
	int j;
	
	for ( int i = 0; i <= 63; ++i) {
		 
		  l = f(p)*0.9;
		  r += l;
		  p += l*d;
		  l = abs(l);
		  if (l<0.005*r) break;
		  j = i;
		
	}
	
	if (j == 63) {
		
		fragColor = vec4(1.0);
		return;
		
	}
	
	float m = m;

	vec3 n = g(p);
	float e = 1.+dot(n,d);
		
	vec3 lp = normalize(vec3(sin(iGlobalTime)*10.0, 1.0+abs(sin(iGlobalTime*0.5))*20.0, 10.0*cos(iGlobalTime))-p);
	float x = pow(max(dot(reflect(lp, n), d), 0.0), 16.0);
	
	d = refract(d, n, 0.6);
	a = ao(p, n, 0.51);
	s = ao(p, d, -0.435);

	s = 1.-clamp(s+0.0,0.,1.);
	s*=s;
	float c = s*a+0.4;
	vec3 cA = c*c*mix(hsv(0.15,0.25,.95), hsv(0.05,0.17,.95), e)*clamp(max(dot(n,lp),0.),0.9,1.0) +vec3(x*a);
	vec3 cB = vec3(a);
	
	fragColor.xyz = clamp(csb(mix(cA, cB, m), 1.2, 1.0, 1.0).xyz, 0., 1.);
	fragColor.w = 1.;	
}