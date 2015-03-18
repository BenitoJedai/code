// Chain Reaction by eiffie
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

#define REFLECTIONS
#define SHADOWS
////#define TEXTURECUBE
//#define CIRCLE_LINKS
#define TWISTS 4.5

// V2 added shadows and made it compile on my older machine by unrolling the code
#define time iGlobalTime
#define size iResolution
#define TAO 6.2831853
float focalDistance,tim=time*2.0,pixelSize;
const float aperture=0.1,shadowCone=0.5,reflectionCone=0.5,pdt=10.0/TAO,tdp=TAO/10.0;
vec3 L;
const vec3 mcol=vec3(2.0,1.6,1.0);

vec2 Rot2D(vec2 v, float angle) {return cos(angle)*v+sin(angle)*vec2(v.y,-v.x);}

float Link(vec3 p, float a){
	p.xy=Rot2D(p.xy,a);
	p.y+=1.0+sin(a+tim)*0.2;
	p.yz=Rot2D(p.yz,a*TWISTS+tim);
#ifdef CIRCLE_LINKS
	return length(vec2(length(p.xy)-0.225,p.z))-0.0225;
#else
	return length(vec2(length(max(abs(p.xy)-vec2(0.125,0.025),0.0))-0.1,p.z))-0.02;
#endif	
}

float DE(in vec3 p){
	float a=atan(p.x,-p.y)*pdt;
	return min(Link(p,floor(0.5+a)*tdp),Link(p,(floor(a)+0.5)*tdp));
}

float CircleOfConfusion(float t){//calculates the radius of the circle of confusion at length t
	return max(abs(focalDistance-t)*aperture,pixelSize*(1.0+t));
}
mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,up));return mat3(rt,cross(rt,fw),fw);
}
float linstep(float a, float b, float t){return clamp((t-a)/(b-a),0.,1.);}// i got this from knighty and/or darkbeam
//random seed and generator
float randSeed;
float randStep(){//a simple pseudo random number generator based on iq's hash
	return  (0.8+0.2*fract(sin(++randSeed)*43758.5453123));
}
#ifdef SHADOWS
float FuzzyShadow(vec3 ro, vec3 rd, float lightDist, float coneGrad, float rCoC){
	float t=0.0,d,s=1.0,r;
	ro+=rd*rCoC*2.0;
	//for(int i=0;i<4;i++){
		r=rCoC+t*coneGrad;d=DE(ro+rd*t)+r*0.5;s*=linstep(-r,r,d);t+=abs(d)*randStep();
		r=rCoC+t*coneGrad;d=DE(ro+rd*t)+r*0.5;s*=linstep(-r,r,d);t+=abs(d)*randStep();
		r=rCoC+t*coneGrad;d=DE(ro+rd*t)+r*0.5;s*=linstep(-r,r,d);t+=abs(d)*randStep();
		r=rCoC+t*coneGrad;d=DE(ro+rd*t)+r*0.5;s*=linstep(-r,r,d);t+=abs(d)*randStep();
	//}
	return clamp(s,0.0,1.0);
}
#endif
vec3 Background(vec3 rd){
#ifdef TEXTURECUBE
	return textureCube(iChannel0,rd).rgb;
#else
	float s=max(0.0,dot(rd,L));
	return vec3(1.0,0.5,0.25)*(s+pow(s,10.0))+rd*0.05;
#endif
}
#ifdef REFLECTIONS
vec3 FuzzyReflection(vec3 ro, vec3 rd, float coneGrad, float rCoC){
	float t=0.0,d,r;
	ro+=rd*rCoC*2.0;
	vec4 col=vec4(0.0);
	//for(int i=0;i<3;i++){//had to unroll this to get it to compile correctly?!?!
		r=rCoC+t*coneGrad;d=DE(ro);
		if(d<r){
			vec2 v=vec2(r*0.1,0.0);//use normal deltas based on CoC radius
			vec3 N=normalize(vec3(DE(ro+v.xyy)-d,DE(ro+v.yxy)-d,DE(ro+v.yyx)-d));
			if(N!=N)N=-rd;
			vec3 scol=mcol*(0.1+Background(reflect(rd,N)))*(0.75+0.5*dot(N,L));
			float alpha=(1.0-col.w)* linstep(-r,r,-d);
			col+=vec4(scol*alpha,alpha);
		}
		d=max(d,r*0.5)*randStep();ro+=d*rd;t+=d;
		//unrolled
		r=rCoC+t*coneGrad;d=DE(ro);
		if(d<r){
			vec2 v=vec2(r*0.1,0.0);//use normal deltas based on CoC radius
			vec3 N=normalize(vec3(DE(ro+v.xyy)-d,DE(ro+v.yxy)-d,DE(ro+v.yyx)-d));
			if(N!=N)N=-rd;
			vec3 scol=mcol*(0.1+Background(reflect(rd,N)))*(0.75+0.5*dot(N,L));
			float alpha=(1.0-col.w)* linstep(-r,r,-d);
			col+=vec4(scol*alpha,alpha);
		}
		d=max(d,r*0.5)*randStep();ro+=d*rd;t+=d;
		r=rCoC+t*coneGrad;d=DE(ro);
		if(d<r){
			vec2 v=vec2(r*0.1,0.0);//use normal deltas based on CoC radius
			vec3 N=normalize(vec3(DE(ro+v.xyy)-d,DE(ro+v.yxy)-d,DE(ro+v.yyx)-d));
			if(N!=N)N=-rd;
			vec3 scol=mcol*(0.1+Background(reflect(rd,N)))*(0.75+0.5*dot(N,L));
			float alpha=(1.0-col.w)* linstep(-r,r,-d);
			col+=vec4(scol*alpha,alpha);
		}
		d=max(d,r*0.5)*randStep();ro+=d*rd;t+=d;
	//}
	return col.rgb+Background(rd)*(1.0-clamp(col.w,0.0,1.0));
}
#endif
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	randSeed=fract(cos((fragCoord.x+fragCoord.y*117.0+time*10.0)*473.7192451));
	pixelSize=2.0/size.y;
	vec3 ro=vec3(0.0,0.0,-2.75);
	vec3 rd=lookat(vec3(cos(tim)*0.2,-sin(tim)*0.2,0.0)-ro,vec3(0.0,1.0,0.0))*normalize(vec3((2.0*fragCoord.xy-size.xy)/size.y,2.0));
	focalDistance=length(ro);
	L=normalize(vec3(0.5,0.6,0.4));
	vec4 col=vec4(0.0);//color accumulator
	float t=2.5;//distance traveled
	ro+=rd*t;//move close to object
	for(int i=0;i<12;i++){//march loop
		if(col.w>0.9 || t>4.0)continue;//bail if we hit a surface or go out of bounds
		float rCoC=CircleOfConfusion(t);//calc the radius of CoC
		float d=DE(ro);
		if(d<rCoC){//if we are inside add its contribution
			vec2 v=vec2(rCoC*0.1,0.0);//use normal deltas based on CoC radius
			vec3 N=normalize(vec3(-d+DE(ro+v.xyy),-d+DE(ro+v.yxy),-d+DE(ro+v.yyx)));
			if(N!=N)N=-rd;
			vec3 refl=reflect(rd,N);
			vec3 scol=mcol*(0.1+Background(refl));
#ifdef SHADOWS
			scol*=FuzzyShadow(ro,L,3.0,shadowCone,rCoC);
#else
			scol*=(0.75+0.5*dot(N,L));
#endif
#ifdef REFLECTIONS
			scol+=0.5*FuzzyReflection(ro,refl,reflectionCone,rCoC);
#endif
			float alpha=(1.0-col.w)*linstep(-rCoC,rCoC,-d);//calculate the mix like cloud density
			col+=vec4(scol*alpha,alpha);//blend in the new color	
		}
		d=max(d,pixelSize)*randStep();//add in noise to reduce banding and create fuzz
		ro+=d*rd;//march
		t+=d;
	}//mix in background color
	col.rgb=mix(Background(rd),col.rgb,clamp(col.w,0.0,1.0));

	fragColor = vec4(clamp(col.rgb,0.0,1.0),1.0);
}
