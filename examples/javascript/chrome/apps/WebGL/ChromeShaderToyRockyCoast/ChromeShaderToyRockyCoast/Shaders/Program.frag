// https://www.shadertoy.com/view/ltlGDX
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// Created by S.Guillitte 



float time=-iGlobalTime;
float dh = 0.;

                                 
mat2 m2 = mat2(0.8,  0.6, -0.6,  0.8);

float noise(in vec2 p){

    float res=0.;
    float f=1.;
	for( int i=0; i< 3; i++ ) 
	{		
        p=m2*p*f+.6;     
        f*=1.2;
        res+=sin(p.x+sin(2.*p.y));
	}        	
	return res/3.;
}


float fbmabs( vec2 p ) {
	p *=.5;
	float f=1.;   
	float r = 0.0;	
    for(int i = 0;i<8;i++){	
		r += abs(noise( p*f ))/f;       
	    f *=2.;
        p-=vec2(-.01,.04)*r;
	}
	return r;
}

float sea( vec2 p ) 
{
	float f=1.;   
	float r = 0.0;	
    for(int i = 0;i<8;i++){	
		r += (1.-abs(noise( p*f +.9*time)))/f;       
	    f *=2.;
        p-=vec2(-.01,.04)*r;
	}
	return r/4.+.5;
}



float rocks(vec2 p){
   
    return 1.5*fbmabs(p);   
}

float map( vec3 p)
{
	float d1 =p.y-.1*p.z+.2-.4*fbmabs(p.xz);
    float d2 =p.y-.4*sea(p.xz);
    dh = d2-d1;
    float d = min(d1,d2);
	return d;	
       	
}

vec3 normalRocks(in vec2 p)
{
	const vec2 e = vec2(0.004, 0.0);
	return normalize(vec3(
		rocks(p + e.xy) - rocks(p - e.xy),
        .05,//.008,
		rocks(p + e.yx) - rocks(p - e.yx)
		));
}

vec3 normalSea(in vec2 p)
{
	const vec2 e = vec2(0.002, 0.0);
	return normalize(vec3(
		sea(p + e.xy) - sea(p - e.xy),
        .004,
		sea(p + e.yx) - sea(p - e.yx)
		));
}

vec3 sky(in vec2 p)
{	
	return sin(vec3(1.7,1.5,1)+1. + .45*fbmabs(p*4.+.02*time));
}

float march(in vec3 ro, in vec3 rd)
{
	const float maxd = 15.0;
	const float precis = 0.001;
    float h = precis * 2.0;
    float t = 0.0;
	float res = -1.0;
    for(int i = 0; i < 128; i++)
    {
        if(h < precis*t || t > maxd) break;
	    h = map(ro + rd * t);
        t += h;
    }
    if(t < maxd) res = t;
    return res;
}

vec3 transform(in vec3 p)
{
    
    p.zx = p.xz;
    p.z=-p.z;
    return p;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.y;
	vec3 col = vec3(0.);
   	vec3 rd = normalize(vec3(p, -2.));
	vec3 ro = vec3(0.0, 2.0, -2.+.2*time);
    vec3 li = normalize(vec3(-2., 2., -4.));
    ro = transform(ro);
	rd = transform(rd);
    
    
    
    float t = march(ro, rd);
    if(t > -0.001)
    {
        if(dh<0.)t-=dh;
        vec3 pos = ro + t * rd;
        
        float k=rocks(pos.xz);
        
        vec3 nor = normalRocks(pos.xz);
        float r = max(dot(nor, li),0.05)/2.;
        if(dh<0.&&dh>-.1)r+=.5*exp(20.*dh);
        
        vec3 col1 =vec3(r*k*k, r*k, r*.8);
        if(dh<0.05){
        	vec3 nor = normalSea(pos.xz);
        	nor = reflect(rd, nor);
        	col1 += pow(max(dot(li, nor), 0.0), 5.0)*vec3(.8);
        	col1 +=.2* sky(nor.yz);
        }
	    col = .1+col1;
        
	}
    else //sky
        col = sky(rd.yz);
    
   	fragColor = vec4(col, 1.0);
}