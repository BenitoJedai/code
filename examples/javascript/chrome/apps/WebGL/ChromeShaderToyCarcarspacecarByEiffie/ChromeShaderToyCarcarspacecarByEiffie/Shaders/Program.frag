// carcarspacecar by eiffie
// This is the morph from the shiney toy car to the space car in kali's shader.
// It is part of a larger animation that wouldn't compile :(

//from iq, this version removes the mix
float smin(float a,float b,float k){float h=clamp(0.5+0.5*(b-a)/k,0.0,1.0);return b+h*(a-b-k+k*h);}

mat2 trmx,mrmx;//the wheel spinners
float minL,cc3,cc4;//car config
vec4 trpc,cc;//color and stuff
bool bColoring=false;

float DE(in vec3 p0){//carcarspacecar by eiffie
	vec3 p=p0;
	p.y+=1.24;
	float d=length(max(vec3(abs(p.x)-0.35,length(p.yz)-1.92,-p.y+1.4),0.0))-0.05;
	d=max(d,p.z-1.0);
	p=p0+vec3(0.0,-0.22,0.39);
	p.xz=abs(p.xz);
	float w1=0.24,w2=cc.z,dL=length(p+vec3(-0.3,0.0,-1.18-p0.z*0.17))-0.05;
	p.xz-=vec2(cc.w,1.0);
	if(p0.z<0.0){
		w1=cc.x;w2=cc.y;
		p.xy=mrmx*p.xy;
	}else p.xz=mrmx*p.xz;
	p.x=abs(p.x);
	float r=length(p.yz);
	d=smin(d,length(vec2(max(max(p.x-w2,0.0),-p.y-0.08),r-w1))-0.02,0.25);
	float d1=length(vec2(max(p.x-w2-0.01,0.0),r-w1+0.05))-0.04;
	if(p0.z<0.0)p.yz=p.yz*trmx;
	else p.yz=trmx*p.yz;
	float d2=min(min(abs(p.z+p.y),abs(p.z-p.y)),min(abs(p.z),abs(p.y)));//8 blades
	d2=max(r-w1+cc3,max(d2-0.003,p.x-w2+0.04));
	d2=min(d2,dL);
	minL=min(minL,d2);//catch the minimum distance to the glowing things
	if(bColoring){
		if(d2<d && d2<d1){trpc+=vec4(1.0,0.6,0.5,256.0);}//spokes/turbines
		else if(d1<d){trpc+=vec4(vec3(clamp(1.0-(r-w1+0.09)*100.0,0.0,1.0)),256.0);}
		else {//the car's body
			if(p0.z<-1.04 || (abs(p0.y-0.58)>0.05-p0.z*0.09 || p0.z>0.25) && length(max(abs(p0.xz+vec2(0.0,-0.27))-vec2(0.18,0.39),0.0))>0.1)trpc+=vec4(1.0,0.9,0.4,16.0);
			else trpc+=vec4(0.1,0.2,0.3,2.0);//the windshield
		}
	}
	return min(d,min(d1,d2)); 
}
void setConfig(){
	float t=mod(iGlobalTime,10.0);
	t=t-5.0;
	if(t>4.0)t=5.0-t;
	t=clamp(t,0.0,1.0);
	cc3=mix(0.06,-0.03,t);
	cc4=mix(0.0,-0.5,t);
	cc=mix(vec4(0.24,0.07,0.1,0.55),vec4(0.33,0.04,0.22,0.72),t);
}
mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,normalize(up)));return mat3(rt,cross(rt,fw),fw);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	setConfig();
	float tim=iGlobalTime*5.0,a=cc4*3.0;
	trmx=mat2(cos(tim),sin(tim),-sin(tim),cos(tim));//the turbine spinner
	mrmx=mat2(cos(a),sin(a),-sin(a),cos(a));
	tim=iGlobalTime*0.5;
	vec3 ro=vec3(cos(tim),0.1+sin(tim*0.7),sin(tim))*(2.5-abs(sin(tim*0.7)));
	vec3 rd=lookat(-ro,vec3(0.0,1.0,0.0))*normalize(vec3((2.0*fragCoord.xy-iResolution.xy)/iResolution.y,1.0));
	vec3 col=vec3(0.4,0.5,0.6)+rd*0.1,L=normalize(vec3(0.5,0.5,-0.5));
	float t=0.0,d=1.0;
	minL=100.0;
	for(int i=0;i<32;i++){
		t+=d=DE(ro+rd*t);
	}
	if(d<0.1){
		vec3 p=ro+rd*t;
		vec2 v=vec2(2.0/iResolution.y,0.0);
		trpc=vec4(0.0);bColoring=true;
		vec3 N=normalize(vec3(-DE(p-v.xyy)+DE(p+v.xyy),-DE(p-v.yxy)+DE(p+v.yxy),-DE(p-v.yyx)+DE(p+v.yyx)));
		col=trpc.rgb*0.1666*max(0.2,0.25+0.75*dot(N,L));
		col*=mix(vec3(0.5,0.5,1.0),vec3(1.0,1.0,0.5),abs(dot(rd,N)));
		col+=vec3(1.0,0.5,0.0)*pow(max(0.0,dot(reflect(rd,N),L)),trpc.a);
	}
	col+=vec3(1.0,0.5,0.2)/(1.0+minL*minL*10000.0);//the glow
	fragColor = vec4(clamp(col,0.0,1.0),1.0);
}