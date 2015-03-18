//Tetrahedronator by eiffie
//This is an attempt at the 3d (or 4d?) version of Triangulator by Nimitz https://www.shadertoy.com/view/lllGRr

//The idea is still simple: find the tetrahedral section of the cube we are in
//then get 4 samples and compute distance using barycentric coords (4d version).
//The implementation is not so simple. Tiling with good tetrahedra that are fairly regular
//is tricky (unless your knighty) and the 4d version of barycentric coords requires 
//a matrix inverse every DE check. So many shortcuts have been taken. 
//It does produce only flat surfaces made of polys but with VERRRY irregular sizes.
 

#define time iGlobalTime
#define size iResolution

//for comparison
//from jessifin (https://www.shadertoy.com/view/lslXDf)
/*vec3 bary(vec2 a, vec2 b, vec2 c, vec2 p) 
{
    vec2 v0 = b - a, v1 = c - a, v2 = p - a;
    float inv_denom = 1.0 / (v0.x * v1.y - v1.x * v0.y)+1e-9;
    float v = (v2.x * v1.y - v1.x * v2.y) * inv_denom;
    float w = (v0.x * v2.y - v2.x * v0.y) * inv_denom;
    float u = 1.0 - v - w;
    return abs(vec3(u,v,w));
}*/

//my own tet weighting function cause why not (actually i didn't want to do the mat inv thingy)
vec4 eiffieCentricCoords(vec3 a, vec3 b, vec3 c, vec3 d, vec3 p) 
{
	b -= a; c -= a; d -= a, p -= a;
	vec3 B = cross(c,d), C=cross(b,d), D=cross(b,c);
	float y = dot(p,B)/(dot(b,B));
	float z = dot(p,C)/(dot(c,C));
	float w = dot(p,D)/(dot(d,D));
	return abs(vec4(1.0-y-z-w,y,z,w));
}

//from iq, this version removes the mix
float smin(float a,float b,float k){float h=clamp(0.5+0.5*(b-a)/k,0.0,1.0);return b+h*(a-b-k+k*h);}

float DO(in vec3 z){//the object's distance estimate
	float bowl=length(max(vec2(abs(length(z.xyz)-0.9),z.y),0.0))-0.1;
	float d=smin(bowl,length(z+vec3(-1.5,0.5,0.0))-0.5,0.5);
	return d;
}

float rez,hrez;//the tesellation scale, and half it 
vec4 wt;
float DE(in vec3 p){
	vec3 c=floor(p/rez)*rez+vec3(hrez);//tile the space into cubes and find the center
	float d0=DO(c);//get the approx distance and return if far away
	if(d0>rez*4.0)return d0-rez*1.5;
	
	//find 4 points in space surrounding point p, we know one will be c the center of the cube
	//we will find the three other corners of the cube that enclose our point
	vec3 o1=vec3(1.0),o2=vec3(-1.0),o3;//offsets for the other "vertices"
	vec3 tp=p-c,abp=abs(tp),sp=sign(tp);//get our position within the cube
	if(abp.x>abp.y && abp.x>abp.z){//now tile the cube into 12 tetrahedrons (very irregular) and find the one we are in
		o1.x=sp.x;o2.x=sp.x;//the first 2 vertices we only need to know which side of the cube it is on
		o3=vec3(sp.x,vec2(1.0,-1.0)*sign(tp.y-tp.z));//the third vertex is chosen from a diagonal
	}else if(abp.y>abp.z){//we are on one of the "Y" sides of the cube
		o1.y=sp.y;o2.y=sp.y;//which y side is determined by the sign of y obviously
		if(tp.x>tp.z)o3=vec3(1.0,sp.y,-1.0);//now spilt this pyramid shape in 2 diagonally
		else o3=vec3(-1.0,sp.y,1.0);
	}else{//the "Z" sides, same same
		o1.z=sp.z;o2.z=sp.z;
		o3=vec3(vec2(1.0,-1.0)*sign(tp.x-tp.y),sp.z);
	}
	o1*=hrez;o2*=hrez;o3*=hrez;//scale the offsets to the tetrahedron's vertices
	//eiffie centric coords are just like barycentric only they work like sh!@!T!
	wt=eiffieCentricCoords(c,c+o1,c+o2,c+o3,p);
	//now use these points to determine the weighted avg distance
	vec4 pd=vec4(d0,DO(c+o1),DO(c+o2),DO(c+o3));//get the distances
	pd*=wt;//apply the weights
	return pd.x+pd.y+pd.z+pd.w; //and wala! ...crappy tesellation
}
float meshy=0.0;
vec3 scene( vec3 ro, vec3 rd, vec2 fragCoord ){
	float t=0.0,d,dm=100.0,tm,px=1.0/size.y,ff=0.75+0.25*cos(time*0.1);
	for(int i=0;i<48;i++){
		d=DE(ro+rd*t)*ff;
		if(d<dm){dm=d;tm=t;if(d<0.00001)break;}
		t+=d+px*t;
		if(t>5.0)break;
	}
	vec3 col=vec3(fragCoord.y/size.y*0.2);
	px*=tm;
	if(dm<px){
		ro+=rd*tm;
		float d=DE(ro);
		float w=min(wt.x,min(wt.y,min(wt.z,wt.w))),dif=0.5,spec=0.0;
		if(meshy<0.8){
			vec2 v=vec2(px*0.66,0.0);
			vec3 dn=vec3(DE(ro-v.xyy),DE(ro-v.yxy),DE(ro-v.yyx));
			vec3 dp=vec3(DE(ro+v.xyy),DE(ro+v.yxy),DE(ro+v.yyx));
			vec3 nor=(dp-dn)/(length(dp-vec3(d))+length(vec3(d)-dn));
			vec3 litDir=normalize(vec3(0.7,0.4,0.7));
			dif=0.5+0.5*dot(nor,litDir);
			spec=0.25*pow(max(0.0,dot(reflect(rd,nor),litDir)),4.0);
			vec3 scol=vec3(0.5);//vec3(0.6,0.3,0.3+dot(nor,rd)*0.1);
			scol=clamp(dif*scol+spec*vec3(1.0),0.0,1.0);
			col=mix(scol,col,clamp(dm/px+meshy*2.0,0.0,1.0));
		}
		col=mix(vec3(0.0,0.5,0.0),col,smoothstep(0.0,12.0*px,1.0-meshy+w));
	}
	return col;
}
mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,up));return mat3(rt,cross(rt,fw),fw);
}
vec2 rotate(vec2 v, float angle) {return cos(angle)*v+sin(angle)*vec2(v.y,-v.x);}

vec2 ch_p;
float ch_d;
void ch(int i){
	const float ch_sv=0.4,ch_sh=0.2;
	if(i > 127){i-=128;ch_d=min(ch_d,max(abs(ch_p.x),abs(ch_p.y)-ch_sv));}
	if(i > 63){i-=64;ch_d=min(ch_d,max(abs(ch_p.x-ch_sh),abs(ch_p.y-ch_sh)-ch_sh));}
	if(i > 31){i-=32;ch_d=min(ch_d,max(abs(ch_p.x-ch_sh),abs(ch_p.y+ch_sh)-ch_sh));}
	if(i > 15){i-=16;ch_d=min(ch_d,max(abs(ch_p.x+ch_sh),abs(ch_p.y-ch_sh)-ch_sh));}
	if(i > 7){i-=8;ch_d=min(ch_d,max(abs(ch_p.x+ch_sh),abs(ch_p.y+ch_sh)-ch_sh));}
	if(i > 3){i-=4;ch_d=min(ch_d,max(abs(ch_p.x)-ch_sh,abs(ch_p.y-ch_sv)));}
	if(i>1){i-=2;ch_d=min(ch_d,max(abs(ch_p.x)-ch_sh,abs(ch_p.y)));}
	if(i>0)ch_d=min(ch_d,max(abs(ch_p.x)-ch_sh,abs(ch_p.y+ch_sv)));
	ch_p.x-=ch_sv*1.5;
}
float cursor(vec2 uv, vec2 ms){
	float d=max(abs(uv.x-ms.x)*abs(uv.y-ms.y),max(abs(uv.x-ms.x)-0.015,abs(uv.y-ms.y)-0.02));
	return smoothstep(0.0,0.01,sqrt(d));
}
vec2 mstsk(float t){
	if(t<0.0)return vec2(0.9,0.5);
	if(t==0.0 || t==4.0)return vec2(0.375,0.025);
	if(t==1.0)return vec2(0.9,0.6);
	if(t==2.0 || t==6.0)return vec2(0.77,0.5);
	if(t==3.0)return vec2(0.9,0.9);
	if(t==5.0)return vec2(0.9,0.7);
	return vec2(0.9,0.5);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	rez=0.01;
	float clk=1.0,tim=time,maxRez=0.15,minRez=0.01;
	vec2 slider=vec2(0.375,0.5), ms=slider;
	float tm=mod(tim,5.0),tsk=mod(floor(tim/5.0),8.0);
	if(tsk>1.0 && tsk<8.0)rez=maxRez;
	if(tsk==4.0)meshy=1.0;
	ms=mstsk(tsk);
	if(tm<1.0){//moving mouse to task
		ms=mix(mstsk(tsk-1.0)+vec2(-0.3,1.0)*tm,ms,tm);	
		if(tsk==5.0)meshy=1.0;
	}else{//perform task
		tm-=1.0;tm*=0.25;
		if(ms.y<0.03){ms.x+=sin(tm*6.283)*0.3;slider.x=ms.x;}
		else if(ms.x==0.77){ms.y+=sin(tm*6.283)*0.4;slider.y=ms.y;}
		//if(tm<0.04)clk=0.0;
		if(tsk==1.0)rez=minRez+tm*(maxRez-minRez);
		else if(tsk==7.0)rez=maxRez-tm*(maxRez-minRez);
		if(tsk==3.0)meshy=tm;
		if(tsk==5.0)meshy=1.0-tm;
		clk=sin(time*50.0);
	}
	vec2 uv=fragCoord.xy/size.xy;
	vec3 color;
	if(uv.x<0.75 && uv.y>0.05){
		
		hrez=0.5*rez;
		float a=-slider.x*3.0,b=slider.y*3.0;
		vec3 ro=vec3(0.0,0.0,1.5),rd=vec3((vec2(2.5)*fragCoord.xy-size.xy)/size.y,1.5);
		ro.xz=rotate(ro.xz,a);
		ro.xy=rotate(ro.xy,b);
		rd=lookat(vec3(0.0,-1.0,0.0)-ro,vec3(0.0,1.0,0.0))*normalize(rd);
		color=scene(ro,rd,fragCoord);
	}else{
		color=vec3(0.4);
	  	if(uv.x>0.79 && uv.y<0.95 && uv.y>0.45){
			float sz=30.0;
			ch_d=100.0;
            const int _A=126, _C=29,_D=125,_E=31,_F=30,_H=122,_I=128,_L=25,_M=252,_P=94, _S=55,_T=132,_U=121;
			ch_p=(uv-vec2(0.86,0.9))*sz;
			ch(_M);ch(_E);ch(_S);ch(_H);
			ch_p=(uv-vec2(0.83,0.8))*sz;
			ch(_D);ch(_I);ch(_F);ch(_F);ch(_U);ch(_S);ch(_E);
			ch_p=(uv-vec2(0.82,0.7))*sz;
			ch(_S);ch(_P);ch(_E);ch(_C);ch(_U);ch(_L);ch(_A);ch(_A);
			ch_p=(uv-vec2(0.81,0.6))*sz;
			ch(_T);ch(_E);ch(_S);ch(_E);ch(_L);ch(_L);ch(_A);ch(_T);ch(_E);
			ch_p=(uv-vec2(0.84,0.5))*sz;
			ch(_S);ch(_M);ch(_D);ch(_D);ch(_T);ch(_H);
			float y=mod(uv.y+0.04,0.1);
			if(abs(uv.x-0.89)<0.1 && y<0.08)color=vec3(0.5+y*4.0);
			color=mix(vec3(0.0),color,smoothstep(0.0,0.1,ch_d));
	  	}else if(uv.y<0.045 && uv.x<0.75){
			color=vec3(0.4+uv.y*5.0+max(0.0,0.4-abs(uv.x-slider.x)*60.0));
		}else if(uv.y>0.05 && uv.x>0.755 && uv.x<0.785){
			color=vec3(0.6+(0.755-uv.x)*5.0+max(0.0,0.4-abs(uv.y-slider.y)*40.0));
		}		
	}
	color=mix(vec3(0.0),color,cursor(uv,ms));
	uv-=vec2(0.004);
	if(clk>0.5)color=mix(vec3(1.0),color,cursor(uv,ms));
	fragColor = vec4(color,1.0);	
}
