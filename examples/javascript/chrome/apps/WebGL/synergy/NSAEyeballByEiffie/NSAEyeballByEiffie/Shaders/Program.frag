//NSA's Eyeball by eiffie
#define time iGlobalTime*0.15
#define size iResolution
#define tex iChannel0
vec3 atmColor=vec3(0.0);
float bflash,t=0.0;
float Rect(in vec3 z, vec3 radii){return max(abs(z.x)-radii.x,max(abs(z.y)-radii.y,abs(z.z)-radii.z));}
float RCyl(in vec3 z, vec3 radii){return length(max(vec2(abs(z.y)-radii.y,length(z.xz)-radii.x),0.0))-radii.z;}
float RRect(in vec3 z, vec4 radii){return length(max(abs(z.xyz)-radii.xyz,0.0))-radii.w;}
vec3 Tile3D(vec3 p, float a){p+=vec3(a);p-=4.0*a*floor(p/(4.0*a));p-=2.0*max(p-2.0*a,0.0);return p-vec3(a);}

float DEBoard(vec3 z){
	float t2=t*t*0.0002;
	float flash=0.1+sin(time*40.0+z.x*3.0+z.y*5.0+z.z*7.0)*0.05+t2;
	float d0=RCyl(z.xzy+vec3(-0.2900,0.1300,-0.1800),vec3(0.0,0.26,0.09));
	if(d0<(bflash+t2))atmColor+=vec3(0.1,0.2,0.4)*((bflash+t2)-d0);
	float d=Rect(z,vec3(1.0,0.025,0.5));
	z.xz=clamp(z.xz,-vec2(0.3300,0.1600),vec2(0.3300,0.1600))*2.0-z.xz;
	z+=vec3(0.03,-0.2400,0.0);
	float d2=RCyl(z,vec3(0.0,0.0900,0.1));
	if(d2<flash)atmColor+=vec3(0.9,0.3,0.0)*(flash-d2);
	float dS=min(min(d0,d),d2);
	return dS;
}
mat3 rmx;
vec3 pp;
float pd;
float DEPhone(vec3 z0)
{
	vec3 z=rmx*(z0-pp)*4.0;
	float d=RRect(z,vec4(0.5,1.0,0.0,0.15));
	float d4=Rect(z+vec3(0.0,0.0,0.15),vec3(0.5,0.8,0.01));
	if(d4<0.3 && abs(z.x)<0.5 && abs(z.y)<0.8){
		vec3 col=texture2D(tex,-z.yx/vec2(1.6,1.0)+vec2(0.5)).rgb;
		atmColor+=(0.3-d4)*mix(max(col-col.brg,0.05),col,pd);
	}
	d=max(d,-d4);
	return d*0.25;
}

float scale=1.1200;
vec3 offset=vec3(0.7200,0.2800,1.0000)*20.0;
float psni=pow(scale,-2.0);
float DE(in vec3 z0){
	vec3 z=Tile3D(z0,4.0);
	z = abs(z);
	if (z.x<z.y)z.xy = z.yx;
	if (z.x<z.z)z.xz = z.zx;
	if (z.y<z.z)z.yz = z.zy;
	z = z*scale - offset*(scale-1.0);
	if(z.z<-0.5*offset.z*(scale-1.0))z.z+=offset.z*(scale-1.0);
	z = abs(z);
	if (z.x<z.y)z.xy = z.yx;
	if (z.x<z.z)z.xz = z.zx;
	if (z.y<z.z)z.yz = z.zy;
	z = z*scale - offset*(scale-1.0);
	if(z.z<-0.5*offset.z*(scale-1.0))z.z+=offset.z*(scale-1.0);
	return min(DEBoard(z)*psni,DEPhone(z0));
}
mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,normalize(up)));return mat3(rt,cross(rt,fw),fw);
}
vec3 path(float d){
	d*=6.0;
	return vec3(4.0+sin(d*0.23+sin(d)),4.0+sin(d*0.3),d*1.5+sin(d*0.3)*2.0);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	float GoldenAngle=2.0-0.5*(1.0+sqrt(5.0));
	float rnd=fract(sin(dot(fragCoord.xy,vec2(13.434,77.2378)))*41323.34526);
	bflash=0.25+0.25*sin(time*10.0);
	vec3 ro=path(time);
	pp=path(time+0.08);
	float a=sin(time*2.0);
	rmx=lookat(path(time+0.05)-ro,vec3(sin(a),cos(a),cos(a*1.3)*0.25));
	vec3 rd=rmx*normalize(vec3((2.0*fragCoord.xy-size.xy)/size.y,2.0));
	rmx[0]=-rmx[0];
	pd=dot(vec3(0.0,0.0,1.0)*rmx,rd);
	pd*=pd;
	float d=1.0;
	for(int i=0;i<96;i++){
		if(d<0.0002)continue;
		t+=d=DE(ro+rd*t)*(0.7+0.2*rnd);
		rnd=fract(rnd+GoldenAngle);
	}
	vec3 col=atmColor;
	fragColor = vec4(col,1.0);
}