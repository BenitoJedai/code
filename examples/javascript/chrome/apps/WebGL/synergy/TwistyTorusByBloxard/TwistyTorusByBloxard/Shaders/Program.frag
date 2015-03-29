// Created by Per Bloksgaard/2014
// Thanks to iq for letting me use his ray-marching, softshadow and ao code.

#define PI 3.14159265358979

float distPlane(in vec3 p)
{
	return p.y;
}

vec2 distTorus(in vec3 p, in vec2 t)
{
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	float a = (atan(q.x,q.y)+PI)/PI*4.0;
	float b = (atan(p.z,p.x)+PI)/PI*8.0;
	float s = clamp(abs(sin(iGlobalTime*0.05))*1.8-0.7,0.0,1.0);
	float c = a * s;
	float d = b * s;	
	float m;
	if (fract(d)>0.5)
	{
		m = -s;
		if (fract(c)>0.5)
		{
			m = b*(1.0-s);
		}
	}
	else
	{
		m = a*(1.0-s);
		if (fract(c)>0.5)
		{
			m = -s;
		}
	}
	return vec2((length(q)-t.y)*0.5,m);
}

vec2 maxOfTwo(in vec2 a, in vec2 b)
{
	return (a.x<b.x)?a:b;
}

vec3 doTwist(in vec3 p)
{
	float f = sin(iGlobalTime)*12.0;
	float c = cos(f*p.y);
	float s = sin(f*p.y);
	mat2  m = mat2(c,-s,s,c);
	return vec3(p.y,m*p.xz);
}

vec2 map(in vec3 pos)
{
	return maxOfTwo( vec2(distPlane(pos),-2.0), distTorus(doTwist(pos-vec3(0.0,0.25,0.0)),vec2(0.20,0.05)) );
}

vec2 castRay(in vec3 ro, in vec3 rd, in float maxd)
{
	float precis = 0.0008;
	float h = precis*2.0;
	float t = 0.0;
	float m = -1.0;
	for(int i=0; i<50; i++)
	{
		if(abs(h)<precis||t>maxd)
		{
			continue;
		}
		t += h;
		vec2 res = map( ro+rd*t );
		h = res.x;
		m = res.y;
	}
	return vec2(t, m);
}

float softshadow(in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k)
{
	float res = 1.0;
	float t = mint;
	for(int i=0; i<30; i++)
	{
		if(t<maxt)
		{
			float h = map(ro + rd*t).x;
			res = min(res, k*h/t);
			t += 0.02;
		}
	}
	return clamp(res,0.0,1.0);
}

vec3 calcNormal(in vec3 pos)
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	map(pos+eps.yyx).x - map(pos-eps.yyx).x);
	return normalize(nor);
}

float calcAO(in vec3 pos, in vec3 nor)
{
	float totao = 0.0;
	float sca = 1.0;
	for(int aoi=0; aoi<5; aoi++)
	{
		float hr = 0.01+0.05*float(aoi);
		vec3 aopos = nor*hr+pos;
		float dd = map(aopos).x;
		totao += -(dd-hr)*sca;
		sca *= 0.75;
	}
	return clamp(1.0-4.0*totao,0.0,1.0);
}

vec3 render(in vec3 o, in vec3 d, in vec3 lig)
{ 
	vec3 col = vec3(0.0);
	vec2 res = castRay(o,d,7.0);
	float t = res.x;
	float m = res.y;
	vec3 pos = o + d*t;
	vec3 nor;
	if(m<-1.0)
	{
		nor = vec3(0.0,1.0,0.0);
		t = 7.0;	
		if (d.y<0.0)
		{
			t = -o.y/d.y;
			pos = o + d*t;
			m = (fract(pos.x)>0.5)?0.0:1.0;
			m = (fract(pos.z)>0.5)?1.0-m:m;
			m -= 1.0;
		}
	}
	else
	{
		nor = calcNormal(pos);
	}
	float factor = clamp(1.0+m,0.0,1.0);
	float ao = calcAO(pos,nor);
	col = vec3(0.5)+0.5*sin(vec3(PI,PI*0.25,PI*0.5)*m);
	float dif = clamp(dot(nor,lig),0.0,1.0);
	float bac = clamp(dot(nor,normalize(vec3(-lig.x,0.0,-lig.z))),0.0,1.0)*clamp(1.0-pos.y,0.0,1.0);
	float sh = softshadow(pos,lig,0.02,7.0,4.0); 
	dif *= sh; 
	vec3 brdf = dif*vec3(1.0);
	float pp = clamp(dot(reflect(d,nor),lig ),0.0,1.0);
	float fre = ao*pow(clamp(1.0+dot(nor,d),0.0,1.0),2.0);
	col = col*brdf+fre*(0.5+0.5*col);
	col *= factor;
	col += vec3(1.5)*sh*pow(pp,8.0);
	t = clamp(t-3.2,0.0,10.0);
	return mix(vec3(0.8,0.9,1.0),col,exp(-0.4*t*t));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 r = fragCoord.xy/iResolution.xy;
	vec2 s = -1.0+2.0*r;
	s.x *= iResolution.x/iResolution.y;
	float u = iGlobalTime*0.5;
	float v = -PI*0.4-cos(iGlobalTime*0.7)*PI*0.10;
	float f = 0.5+abs(cos(iGlobalTime*0.1)*0.5);
	float time = -iGlobalTime*0.5;
	vec3 target = vec3(0.0,0.25,0.0);
	vec3 origin = vec3(cos(u)*sin(v)*f,cos(v)*f,sin(u)*sin(v)*f);
	origin += target;
	vec3 camForward = normalize(target-origin);
	vec3 wldUp = vec3(0.0,1.0,0.0);
	vec3 camRight = normalize(cross(camForward,wldUp));
	vec3 camUp = normalize(cross(camRight,camForward));
	vec3 d = normalize(s.x*camRight+s.y*camUp+camForward*2.1);
	vec3 c = render(origin,d,normalize(origin));
	fragColor = vec4(c,1.0);
}
