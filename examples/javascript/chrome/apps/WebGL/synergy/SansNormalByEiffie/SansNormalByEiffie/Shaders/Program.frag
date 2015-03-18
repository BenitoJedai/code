//sans normal by eiffie (lighting without finding the surface normal)
//Some of my multisampling stuff requires a number of lighting calcs so I was wondering
//what the minimum number of DE taps would be to calc diffuse and spec terms. 2
//The lighting calc is at the end and they are simply based on DE ratios.

//v2 I decided to add the aforementioned multisampling.

//I have yet to find a scene that didn't benefit from overstepping!
#define AUTO_OVERSTEP
#define LOTSAPOTS 2

float textureIt(vec3 p){
	p*=64.0;
	return clamp(0.5*abs(sin(p.y+sin(p.x+sin(p.z)))),0.0,0.33);
}
float ohfudge;
float DE(in vec3 p0){
	vec2 v=vec2(2.0,2.5);
	p0.xz=mod(p0.xz+v,2.0*v)-v;
	vec3 p=p0;
	float d=100.0,scl=1.0;
	for(int i=0;i<LOTSAPOTS;i++){
		float r=0.5+sin((p.y+0.75)*3.5)*0.25;
		float o0=length(max(abs(vec2((length(p)-0.8)*0.57,p.y+1.0))-vec2(0.0,0.6),0.0))-0.02;
		p.z-=sign(p.z)*1.6;
		float o1=length(max(abs(vec2((length(p.xz)-r)*0.7,p.y))-vec2(0.0,1.0),0.0))-0.02;
		p.x-=sign(p.x)*1.4;
		float d0=max(abs(p.z),abs(p.x))-r*0.707;
		float d1=0.577*(max(abs(p.x+p.z),abs(p.x-p.z))-r);
		float o2=length(max(abs(vec2(max(d0,d1)*0.8,p.y))-vec2(0.0,1.0),0.0))-0.02;
		p.z-=sign(p.z)*1.0;
		p.y+=0.75;
		d=min(d,min(o0,min(o1,o2))*scl);
		p*=5.0;scl*=0.2;
	}
	p0.xz=abs(p0.xz)-v*0.5;
	d-=textureIt(p0)*0.004;
	return abs(d)*ohfudge;
}

float rnd(vec2 p){return fract(sin(dot(p,vec2(317.234,13.241)))*423.1123);}
mat3 lookat(vec3 fw){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,vec3(0.0,1.0,0.0)));return mat3(rt,cross(rt,fw),fw);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 uv=(2.0*fragCoord.xy-iResolution.xy)/iResolution.y;
	float tim=iGlobalTime*0.3;
	vec3 ro=vec3(sin(tim),1.0,cos(tim))*(1.5+iGlobalTime*0.015);
	vec3 rd=vec3(uv,1.0);
	float px=2.5/iResolution.y;
	vec3 fw=-ro;
	if(mod(iGlobalTime,40.0)>20.0){
		tim*=2.0;
		ro=vec3(-tim,0.6+sin(tim)*0.3,2.0+sin((0.5+tim)*1.57));
		tim+=1.5;
		fw=vec3(-tim,0.5+sin(tim)*0.3,2.0+sin((0.5+tim)*1.57))-ro;
	}
	if(mod(iGlobalTime,60.0)>40.0){ro=vec3(-tim,-0.62,2.5);fw=vec3(1.0,0.0,0.0);}
	rd=lookat(fw)*normalize(rd);
	ohfudge=0.9-abs(rd.y)*0.3;
	float t=DE(ro)*rnd(fragCoord.xy),d,pd=10.0,os=0.0,step;
	float tG=-(1.0+ro.y)/rd.y;if(tG<0.0)tG=10.0-tG;
	vec4 hit=vec4(-100.0);//clear stack
	for(int i=0;i<64;i++){
		d=DE(ro+rd*t);
#ifdef AUTO_OVERSTEP
		if(d>=os){		//we have NOT stepped over anything
			os=0.44*d*d/pd;//overstep based on ratio of this step to last
			step=d+os;	//add in the overstep
			pd=d;		//save this step length for next calc
		}else{step=-os;d=1.0;pd=100000.0;os=0.0;}//remove overstep
#else
		step=d;
#endif
		if(d<0.75*px*t)hit=vec4(hit.yzw,t);//enough of a contribution to matter
		t+=step;
		if(hit.x>0.0 || t>tG)break;//the stack is full or marched out of bounds
	}
	
	vec2 so=(ro.xz+rd.xz*tG)*vec2(25.0,50.0);
	vec3 col=vec3(0.7+0.3*abs(sin(so*2.0)+sin(so+sin(so.yx))),0.8);
	
	if(rd.y>0.0)col=vec3(0.05+0.05*sin(atan(rd.x,rd.z)*10.0),0.0,0.0)*rd.y;
	else col*=exp(-tG*0.1)*0.25*sqrt(abs(DE(ro+rd*tG)));
	
	vec3 lightDir=normalize(vec3(0.1,0.2,0.6));
	vec3 in4r=normalize(lightDir-rd);//ideal normal for reflection (half ray)
	vec3 lightCol=vec3(2.0,0.5,0.0);
	vec3 diffuse=vec3(0.5,0.4,0.3);
	for(int i=0;i<4;i++){
		hit=hit.wxyz;
		if(hit.x>0.0){
			vec3 so=ro+rd*hit.x;
			d=DE(so);
			
			//Here are the lighting calcs without a normal.
			vec3 scol=diffuse*clamp(pow(0.5*DE(so+d*lightDir)/d,2.0),0.0,1.0);//self shadow
			scol+=lightCol*clamp(pow(0.5*DE(so+d*in4r)/d,8.0),0.0,1.0);//specular
			
			col=mix(scol*exp(-hit.x*0.1),col,clamp(d/(px*hit.x),0.0,1.0));
		}
	}
	uv=vec2(uv.x+uv.y,uv.x-uv.y);
	float dim=1.0-0.1*abs(mod(iGlobalTime,20.0)-10.0);
	float vig=clamp(dim-0.15*length(uv*uv),0.0,1.0)*4.0;
	fragColor = vec4(col*vig,1.0);
}

