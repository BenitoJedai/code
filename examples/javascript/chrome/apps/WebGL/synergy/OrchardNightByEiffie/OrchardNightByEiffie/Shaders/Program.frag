// Orchard@Night by eiffie
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

// This is a version of Kali's tree found here. https://www.shadertoy.com/view/Xds3R7

// V2 Removed the artistic "spiral" and replaced it with more artistic "soul sucking darkness"

#define time iGlobalTime
#define size iResolution

mat3  rotAA(vec3 v, float angle){//axis angle rotation
	float c=cos(angle);vec3 s=v*sin(angle);
	return mat3(v.xxx*v,v.yyy*v,v.zzz*v)*(1.0-c)+mat3(c,-s.z,s.y,s.z,c,-s.x,-s.y,s.x,c);
}
const vec3 Julia=vec3(-0.1825,-0.905,-0.2085),ba=vec3(0.0,0.7,0.0);
mat3 rmx;
const float thin=0.018;
float oobdb;

// See http://www.iquilezles.org/www/articles/morenoise/morenoise.htm
float hash(float n) {return fract(sin(n) * 43758.5453123);}
float noyz(vec2 x) {// a dumbed down version of iq's noise
	vec2 p=floor(x),f=fract(x);
	const float tw=117.0;
	float n=p.x+p.y*tw;
	float a=hash(n),b=hash(n+1.0),c=hash(n+tw),d=hash(n+tw+1.0);
	vec2 u=f*f*(3.0-2.0*f);
	return a+(b-a)*u.x+(c-a)*u.y+(a-b-c+d)*u.x*u.y;
}
float fbm(vec2 x){return 0.6*noyz(x)+0.3*noyz(x*1.7)+0.1*noyz(x*3.7);}

float DE(in vec3 p){//tree based on kali's	https://www.shadertoy.com/view/Xds3R7
	p+=(noyz(p.zx*0.7900)-vec3(0.5,0.2,0.5))*0.2000+sin(p.yzx*5.0)*0.037;
	p.xz=abs(vec2(2.0)-mod(p.xz,vec2(4.0)))-vec2(1.0);
	float d=length(p-ba*clamp(dot(p,ba)*oobdb,0.0,1.0))-0.07+p.y*thin,dr=1.0;
	for (int n = 0; n < 9; n++) {
		p.x=abs(p.x);
		p=p*rmx+Julia;
		dr*=0.74074;
		d=min(d,(length(p-ba*clamp(dot(p,ba)*oobdb,0.0,1.0))-0.07+p.y*thin)*dr);//this is iq's tube formula with precalcs
	}
	return d;
}

float CE(in vec3 p0){//texturing the above
	vec3 p=p0;
	p+=(noyz(p.zx*0.7900)-vec3(0.5,0.2,0.5))*0.2000+sin(p.yzx*5.0)*0.037;
	p.xz=abs(vec2(2.0)-mod(p.xz,vec2(4.0)))-vec2(1.0);
	float d=length(p-ba*clamp(dot(p,ba)*oobdb,0.0,1.0))-0.07+p.y*thin,dr=1.0;
	vec4 pTrap=vec4(p,dr);
	for (int n = 0; n < 8; n++) {
		p.x=abs(p.x);
		p=p*rmx+Julia;
		dr*=0.74074;
		float d2=(length(p-ba*clamp(dot(p,ba)*oobdb,0.0,1.0))-0.07+p.y*thin)*dr;
		if(d2<d){
			d=d2;
			pTrap.xyz=p;
			pTrap.w=dr;
		}
	}
	vec2 pt=vec2(pTrap.y*dr,atan(pTrap.z,pTrap.x)*0.01)*700.0;
	return d+(noyz(pt)+noyz(pt*1.7)*0.5)*dr*0.025*abs(p0.y-2.3);
}

mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,normalize(up)));return mat3(rt,cross(rt,fw),fw);
}
float AO( vec3 p, vec3 n ) {//kali's version of iq's?? (I'm using it as very soft shadow/ao.)
	float aodet=0.37,d=aodet,totao = 0.0;
	for( int aoi=1; aoi<5; aoi++ ) {
		totao+=(d-DE(p+n*d))*exp(-1.53*d);
		d+=aodet;
	}
	return clamp(1.-totao, 0., 1.0 );
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	oobdb=1.0/dot(ba,ba);
	rmx=rotAA(normalize(vec3(0.2174,1,0.02174)), 1.62729)*1.35;
	float tim=time*0.125;
	vec3 ro=vec3(tim*0.69+sin(tim*0.57)*2.0,0.35+sin(tim*1.3)*0.1,tim)*3.4;//camera setup
	vec3 L=normalize(vec3(0.5,0.0,0.5));
	vec3 rd=lookat(vec3(1.0+sin(tim*1.6)*0.5,-ro.y*2.0+3.5,0.7),vec3(0.0,1.0,0.0))*normalize(vec3((2.0*fragCoord.xy-size.xy)/size.y,1.0));
	float t=0.0,d=1.0;//distance traveled
	float maxT=((rd.y<0.0)?min(-ro.y/rd.y,20.0):20.0);
	float f=max(dot(rd,L),0.01);
	vec2 pt=vec2(rd.z-rd.x,rd.y)*4.0+vec2(100.0);
	vec3 bcol=vec3(1.0,0.9,0.8)*pow(f,30.0)+vec3(0.5)*pow(f,2.0)*fbm(pt);
	bcol=max(bcol,vec3(1.0-pow(rd.y,4.0))*pow(noyz(vec2(atan(rd.z,rd.x),rd.y)*size.y*0.222),70.0));
	vec3 col=mix(vec3(0.0),bcol,smoothstep(-0.05,0.05,rd.y));
	vec2 trap=vec2(100.0);
	for(int i=1;i<48;i++){//march loop
		if(t>maxT)continue;
		t+=d=DE(ro+rd*t)*0.9+0.001*t;
		if(d<trap.x)trap=vec2(d,t);
	}
	float mind=0.6*trap.y/size.y;
	if(trap.x<mind){
		ro+=rd*trap.y;
		vec3 hcol=vec3(0.41,0.3,0.245);
		vec2 v=vec2(mind,0.0);
		float d=CE(ro);
		float d1=CE(ro-v.xyy),d3=CE(ro-v.yxy),d5=CE(ro-v.yyx);
		float d2=CE(ro+v.xyy),d4=CE(ro+v.yxy),d6=CE(ro+v.yyx);
		vec3 N=normalize(vec3(-d1+d,-d3+d,-d5+d));	
		vec3 N2=normalize(vec3(-d+d2,-d+d4,-d+d6));	
		hcol*=(max(0.0,0.25+dot(N,L)*0.75)+max(0.0,0.25+dot(N2,L)*0.75))*0.3;
		hcol+=clamp(pow(max(0.0,dot(reflect(rd,N),L)),12.0)+pow(max(0.0,dot(reflect(rd,N2),L)),12.0),0.0,0.15)*vec3(1.0,0.9,0.8);	
		hcol*=AO(ro,L);
		col=mix(hcol,col,min(1.0,smoothstep(0.0,mind,trap.x)+trap.y*0.0700));
	}
	fragColor = vec4(clamp(col.rgb,0.0,1.0),1.0);
}