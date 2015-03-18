//by eiffie - just seeing what my laptop can handle. not much, runs at 1.5 fps :(
//some furry creatures running from a fire. you would too if you were furry!
float rand(vec3 co){// implementation found at: lumina.sourceforge.net/Tutorials/Noise.html
	return fract(sin(dot(co*0.123,vec3(12.9898,78.233,112.166))) * 43758.5453);
}
float noyz(vec3 co){
	vec3 d=smoothstep(0.0,1.0,fract(co));
	co=floor(co);const vec2 v=vec2(1.0,0.0);
	return mix(mix(mix(rand(co),rand(co+v.xyy),d.x),
		mix(rand(co+v.yxy),rand(co+v.xxy),d.x),
		d.y),mix(mix(rand(co+v.yyx),rand(co+v.xyx),d.x),
		mix(rand(co+v.yxx),rand(co+v.xxx),d.x),d.y),d.z);
}
float Limb2(vec3 p, vec3 p0, vec3 p2, vec3 rt, float d, float r){
	vec3 p1=(p2-p0)*0.5;//a simple joint solver
	p1+=p0+normalize(cross(p1,rt))*(d*d-dot(p1,p1));//should be sqrt(d*d...
	vec3 v=p1-p0;v*=clamp(dot(p-p0,v)/dot(v,v),0.0,1.0);
	vec3 v2=p1-p2;v2*=clamp(dot(p-p2,v2)/dot(v2,v2),0.0,1.0);
	return min(distance(p-p0,v),distance(p-p2,v2))-r;
}
float Segment(vec3 p, vec3 p0, vec3 p1, float r){//connect 2 points
	vec3 v=p1-p0;v*=clamp(dot(p-p0,v)/dot(v,v),0.0,1.0);return distance(p-p0,v)-r;
}

vec3 p1=vec3(0.0,-0.15,0.0),p2=vec3(0.0,-0.5,0.05);
vec3 p3=vec3(-0.08,-0.15,0.0),p6=vec3(0.08,-0.15,0.0);
vec3 p9=vec3(-0.05,-0.5,0.05),p12=vec3(0.05,-0.5,0.05);
vec3 rt=vec3(1.0,0.0,0.0);

float DE(vec3 z){
	z.z+=iGlobalTime*0.1;
	int i=int(floor(z.z)+floor(z.x));
	//z.xz=abs(vec2(1.0)-mod(z.xz,2.0))-vec2(0.5)+0.25*vec2(sin(float(i)));//for moon walking
	z.xz=mod(z.xz,1.0)-vec2(0.5,0.7)+0.25*vec2(sin(float(i)));
	float tim=(iGlobalTime+float(i)),arm=sin(float(i))*0.2;
	vec3 p5=vec3(-0.38+abs(arm),arm-0.1-abs(-sin(tim)*0.05),-0.12-cos(tim)*0.1);
	vec3 p8=vec3(0.38-abs(arm),arm-0.1-abs(sin(tim+3.1416)*0.05),-0.12+cos(tim)*0.1);
	vec3 p11=vec3(-0.075,-0.975+max(0.0,cos(tim+3.1416)*0.05),sin(tim)*0.2);
	vec3 p14=vec3(0.075,-0.975+max(0.0,cos(tim)*0.05),-sin(tim)*0.2);
	float d=min(z.y+1.0,min(length(z*vec3(1.5,1.0,1.25))-0.1,Segment(z,p1,p2,0.065)));
	d=min(d,min(Limb2(z,p3,p5,rt,0.3,0.025),Limb2(z,p6,p8,rt,0.3,0.025)));
	return min(d,min(Limb2(z,p9,p11,-rt,0.33,0.025),Limb2(z,p12,p14,-rt,0.33,0.025)))
		-noyz(z*100.0)*0.02;
}
vec3 scene(vec3 ro, vec3 rd){
	float rayLen=0.0,dist=10.0,atm=0.0;
	for(int i=0;i<48;i++){
		rayLen+=dist=min(DE(ro+rayLen*rd)*0.55,0.125);
		atm+=noyz(ro+rayLen*rd-vec3(iGlobalTime))*0.03;
		if(rayLen>6.0 || dist<0.01)break;
	}
	rayLen/=6.0;
	return vec3(rayLen*rayLen+atm)*pow(dot(rd,vec3(0.3,0.1,1.0)),3.0);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = (fragCoord.xy / iResolution.xy-vec2(0.5))*
		vec2(1.0,iResolution.y/iResolution.x);
	vec3 ro=vec3(0.1,-0.25,-3.0),rd=normalize(vec3(uv,0.2));
	vec3 color=scene(ro,rd);
	fragColor = vec4(color,1.0);
}