//Someday my Art will Hang in a Museum. by eiffie
//This is the family friendly version.
//You can view the age restricted one here... https://vimeo.com/115060701

#define time iGlobalTime
#define size iResolution

#define ch_g(x,y) if(i>=bit){ch_d=min(ch_d,max(abs(x)-h,abs(y)));i-=bit;}bit/=2;

float chr(int i,vec2 ch_p){
  const float s=0.5,s2=0.7;
  float h=0.353,ch_d=1.0;
  int bit=32768;
  vec2 dg=s2*vec2(ch_p.x+ch_p.y,ch_p.x-ch_p.y);
  ch_g(dg.x+h,dg.y) 
  ch_g(-dg.y-h,dg.x) 
  ch_g(-dg.y+h,dg.x) 
  ch_g(dg.x-h,dg.y) 
  h=0.25;
  ch_g(ch_p.x+h,ch_p.y-s) 
  ch_g(ch_p.x-h,ch_p.y-s) 
  ch_g(ch_p.y-h,ch_p.x+s) 
  ch_g(ch_p.y-h,ch_p.x) 
  ch_g(ch_p.y-h,ch_p.x-s) 
  ch_g(ch_p.x+h,ch_p.y) 
  ch_g(ch_p.x-h,ch_p.y)
  ch_g(ch_p.y+h,ch_p.x+s)
  ch_g(ch_p.y+h,ch_p.x)
  ch_g(ch_p.y+h,ch_p.x-s) 
  ch_g(ch_p.x+h,ch_p.y+s) 
  ch_g(ch_p.x-h,ch_p.y+s) 
  ch_p.x-=1.25;
  return ch_d;
}
//14 bits and the D's still look like O's :(
#define _A 3828
#define _B 3831
#define _C 3603
#define _D 3735
#define _E 3699
#define _F 3696
#define _G 3703
#define _H 756
#define _I 264
#define _J 135
#define _K 12880
#define _L 531
#define _M 21140
#define _N 25236
#define _O 3735
#define _P 3824
#define _Q 11927
#define _R 12016
#define _S 3687
#define _T 3336
#define _U 663
#define _V 37392
#define _W 671
#define _X 61440
#define _Y 20488
#define _Z 39939

float DEF(vec3 z, float xo, vec2 pf){//art frame
	float d=max(abs(z.x-xo)-pf.x,abs(z.y)-pf.y);
	d=length(max(abs(vec2(d,z.z-3.9))-vec2(0.05),0.0))-0.02;
	return d;
}
vec2 rotate(vec2 v, float angle) {return cos(angle)*v+sin(angle)*vec2(v.y,-v.x);}
float ecyl(vec3 z, vec4 r){
	float f=length(z.xz*r.xy);
	return length(max(vec2(abs(z.y)-r.w,f*(f-r.z)/length(z.xz*r.xy*r.xy)),0.0));//ellipse from iq
}
float smin(float a,float b,float k){float h=clamp(0.5+0.5*(b-a)/k,0.0,1.0);return b+h*(a-b-k+k*h);}//also iq's?
mat2 rm1=mat2(0.87758,0.479425,-0.479425,0.87758);
float spc,rotb;	

vec3 art2(vec2 z, float a, float b){//fractal
	z=z*0.83;
	float m=dot(z,z),mold=m,sum=m;
	for (int n = 0; n < 4; n++) {
		if(m>4.0)break;
		z=vec2(z.y*z.y-sqrt(abs(z.x))+a,z.x*z.x-sqrt(abs(z.y))+b);
		m = dot(z,z);
		sum+=1.0/abs(mold-m);
		mold=m;
	}
	return (1.0-sum*0.02)*vec3(0.5+0.5*sin(z),1.0);
} 
float randSeed,GoldenAngle;
float rand(){return randSeed=fract(randSeed+GoldenAngle);}
float hash(vec2 co){return fract(sin(dot(co,vec2(13.434,77.2378)))*1323.3451);}//
float noiz(vec2 p){//from iq
	vec2 c=floor(p),f=fract(p),v=vec2(1.0,0.0);
	return mix(mix(hash(c),hash(c+v.xy),f.x),mix(hash(c+v.yx),hash(c+v.xx),f.x),f.y);
}
float cloud(vec2 p){
	p*=0.05;
	float n=noiz(p);
	p*=-0.16;
	p=sin(p+2.4*sin(p.yx)+p.yx);
	return clamp(n*0.25*(p.x+p.y),0.0,1.0);
}
vec3 art3(vec3 ro,vec3 rd,float a,float b){//landscapes clouds, water, land
	float tW=-ro.y/rd.y,tG=(74.0-190.0*rd.x+6.25*sin((rd.x+rd.z)*50.0)-ro.z)/rd.z;
	if(tG<0.0)tG=100000.0;
	vec3 col=vec3(1.0),sky=vec3(0.6,0.7,0.8),grnd=vec3(0.4,0.6,0.5);
	float lmt=1.0;
	if(rd.y<0.0 && tW<tG){
		ro+=tW*rd;
		rd.y=max(0.0001,-rd.y+sin(ro.z+sin(ro.x))*0.01-0.015*noiz(ro.xz*5.0));
		lmt=max(1.0-tW*0.015,0.17);
	}
	tG=(74.0-190.0*rd.x+6.25*sin((rd.x+rd.z)*50.0)-ro.z)/rd.z;
	if(rd.y>0.0){
		float tS=(90.0-ro.y)/rd.y;
		tW=10000.0;
		col=mix(sky.zxy,sky,rd.y*10.0);
		vec2 p=ro.xz+rd.xz*tS;
		float cld=cloud(p);
		col+=vec3(cld);
	}
	
	ro+=tG*rd;
	float f=40.0,n=noiz(rd.xy*1000.0),y=ro.y*0.009;
	ro*=0.0625;
	for(int i=0;i<3;i++){
		if(y<b*(sin(ro.x+sin(ro.z))+1.82+n*0.5))col=grnd*(0.2+y*f*n);
		ro*=4.0;f*=0.5;b*=0.5;
	}

	return clamp(col,0.0,lmt);
}

vec3 art4(vec2 p, float a, float b){//abstract
	vec3 col=vec3(1.0);
	p*=0.75;
	vec4 v=vec4(rand(),rand(),rand(),rand());
	for(int i=0;i<4;i++){	
		if(a+max(abs(p.x+v.x-0.5)-v.y,abs(p.y+v.z-0.5)-v.w)<0.0)col*=v.xyz;
		if(b+length(p+v.yw-vec2(0.5))-v.z<0.0)col*=v.yzw;
		v=vec4(v.yzw,rand());
	}
	return col;
}

float DEEif(vec2 p){
	float x=abs(abs(p.x)-9.0);
	vec2 ap=vec2(abs(p.x-3.0),p.y);
	x=min(x,abs(abs(ap.x-9.0)-6.0));
	float y=clamp(p.y,-3.0,3.0)*2.0-p.y;
	p.x=abs(p.x);
	if(abs(p.x)<18.0){
		if(abs(p.x)<12.0){
			if(abs(p.x)<6.0){
				if(p.y<-3.0)y=10.0;
			}else y=10.0;
		}
	}else y=10.0;
	return min(max(abs(x),abs(p.y)-6.0),abs(y));
}
float st,ct;
vec2 rotate(vec2 v) {return ct*v+st*vec2(v.y,-v.x);}
vec2 invrot(vec2 v) {return st*v+ct*vec2(v.y,-v.x);}
vec3 art5(vec2 p){//nude
	vec3 col=vec3(0.5+p.y,0.0,0.5);
	vec2 ap=invrot(floor(p*32.0));
	float dE=DEEif(vec2(mod(ap.x+time*20.0,64.0)-32.0,ap.y-5.0));
	p=rotate(floor(p*32.0));
	p.y+=5.0;p.x=mod(p.x-time*10.0,64.0)-32.0;
	float d=length(max(abs(p)-vec2(10.0,4.0),0.0))-5.0;
	if(d<1.0){//body
		if(d<0.0){
			if(d<-1.0){
				float h=0.5*smoothstep(0.0,0.05,hash(p));
				col=vec3(1.0,h,h);
			}else col=vec3(1.0,1.0,0.5);
		}else col=vec3(0.0);
	}
	p+=vec2(sin(time*4.0),cos(time*4.0));
	ap=p-vec2(6.0,2.0);ap.x=abs(ap.x)-6.0;
	d=length(ap)-2.0;
	if(d<1.0){//ears
		if(d<0.0)col=vec3(0.5);else col=vec3(0.0);
	}
	ap=p;ap.x=abs(abs(ap.x)-8.0)-3.0;
	ap.y+=9.0;
	d=length(ap)-2.0;
	if(d<1.0){//feet
		if(d<0.0)col=vec3(0.5);else col=vec3(0.0);
	}
	d=length(p*vec2(0.6,1.0)-vec2(4.0,-3.0))-5.0;
	if(d<1.0){//head
		if(d<0.0){
			col=vec3(0.5);
			ap=p-vec2(7.0,-3.0);ap.x=abs(ap.x);
			d=length(ap-vec2(5.0,-1.0))-2.0;
			if(d<0.0)col=vec3(1.0,0.5,0.5);
			d=length(ap-vec2(4.0,2.0))-1.0;
			if(d<0.0 || (ap.x<3.0 && ap.y>-3.0 && ap.y<-1.0))col=vec3(0.0);
		}else col=vec3(0.0);
	}
	if(dE<1.0)col=vec3(0.0,1.0,0.0);
	return col;
}
mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);vec3 rt=normalize(cross(fw,up));return mat3(rt,cross(rt,fw),fw);
}
float pic(vec2 p, vec2 pf){return max(abs(p.x)-pf.x,abs(p.y)-pf.y);}
#define CH(a,b,c,d,e,f,g,h,i,j,k,l,m) ch[0]=a;ch[1]=b;ch[2]=c;ch[3]=d;ch[4]=e;ch[5]=f;ch[6]=g;ch[7]=h;ch[8]=i;ch[9]=j;ch[10]=k;ch[11]=l;ch[12]=m;
vec3 art1(vec3 ro,vec3 rd){//non nude version :)
	vec3 col=vec3(clamp(0.5+0.5*rd.y,0.0,1.0));
	vec2 p=rd.xy*3.0;
	p.x+=1.0;
	int ch[13];
	p.x=-p.x;CH(_I,_M,_A,_G,_E,0,_R,_E,_M,_O,_V,_E,_D)
	float d=1.0;
	for(int i=0;i<13;i++){
		d=min(d,chr(ch[i],p*vec2(-10.0,5.0)));
		p.x+=0.14;
	}
	col=mix(vec3(0.0),col,smoothstep(0.0,0.15,d));
	return col;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	GoldenAngle=2.0-0.5*(1.0+sqrt(5.0));
	float tim=min(time,48.75),tim2=tim;
	st=0.0;ct=1.0;
	vec2 up=vec2(0.0,1.0);
	if(time>50.25){st=sin(time*4.0);ct=cos(time*4.0);tim2+=(time-50.25)*15.0;}
	if(time>62.83){up=vec2(st,ct);}
	vec3 ro=vec3(-30.0+tim,-0.75,-0.5+tim2*0.01);
	vec2 uv=(2.0*fragCoord.xy-size.xy)/size.y;
	vec3 rd=lookat(vec3(0.2*cos(tim+sin(tim*0.1)),0.1,1.0),vec3(up,0.0))*normalize(vec3(uv,1.5));
	vec3 col=vec3(0.85),N,L=normalize(vec3(0.1,0.5,-0.6)),LC=vec3(1.0,0.7,0.4);
	float px=2.0/size.y,spec=0.2;
	float tW=(4.0*sign(rd.z)-ro.z)/rd.z;
	vec2 p0=ro.xy+rd.xy*tW,p=p0,pf=vec2(1.0,2.0),pfs=pf;
	float d=pic(p,pf),d2,ds=d,a=2.28,b=0.05,x,xs=0.0;
	int ch[13];
	CH(0,_P,_H,_O,_T,_O,_G,_R,_A,_P,_H,_Y,0)
	p.x-=4.0;
	d2=pic(p,pf);
	rotb=0.28;
	if(d2<d){a=-1.68;rotb=0.36,b=-0.23;d=d2;xs=4.0;ds=d2;}else p.x+=4.0;
	if(d<0.0 || time>75.0){
		spec=1.0;
		vec3 sro=vec3(0.0,0.0,-3.0);//5.25*sin(a),0.4,5.25*cos(a));
		vec3 srd=lookat(-sro,vec3(0.0,1.0,0.0))*normalize(vec3(p*0.5,1.0));//vec3(b,0.5,0.0))*normalize(vec3(p*0.5,2.0));
		if(time>75.0){sro=vec3(5.0,0.4,(76.0-time)*8.0);srd=lookat(vec3(-1.0,-0.075,0.0),vec3(0.0,1.0,0.0))*normalize(vec3(uv,2.0));rotb=0.35*sin(time*8.0);}
		col=art1(sro,srd);
		if(time>75.0){
			col*=clamp(time-75.0,0.0,1.0);
			fragColor=vec4(col,1.0);
			return;
		}
	}
	x=9.0;
	p.x=p0.x-x;a=0.54;b=0.33;pf=vec2(1.75);
	d=pic(p,pf);
	p.x-=5.0;
	d2=pic(p,pf);
	if(d2<d){d=d2;a=-0.18;b=-0.53;x+=5.0;}else p.x+=5.0;
	if(d<ds){xs=x;pfs=pf;ds=d;if(d==d2){ CH(0,_G,_E,_N,_E,_R,_A,_T,_I,_V,_E,0,0) }else{ CH(0,0,0,_F,_R,_A,_C,_T,_A,_L,0,0,0) }}
	if(d<0.0){
		spec=1.0;
		col=art2(p,a,b);
		if(b==0.33)col=col.rbg;
		//col=clamp(col,0.1,0.9);
	}
	x=-5.0;
	p.x=p0.x-x;a=2.0;b=0.0;
	d=pic(p,pf);
	p.x+=5.0;
	d2=pic(p,pf);
	if(d2<d){d=d2;a=0.0;b=2.0;d2=50.0;x-=5.0;}else{p.x-=5.0;d2=6.0;}
	if(d<ds){xs=x;pfs=pf;ds=d;if(a==0.0){ CH(0,0,_A,_B,_S,_T,_R,_A,_C,_T,0,0,0) }else{ CH(0,0,0,_C,_U,_B,_I,_S,_M,0,0,0,0) }}
	if(d<0.0){
		spec=1.0;
		randSeed=hash(floor(p*d2));
		col=art4(p,a,b);
		if(b==2.0)col=mix(col,col.gbr,dot(col,col.brg));
	}
	x=-17.0;
	p.x=p0.x-x;a=2.0;b=0.021;pf=vec2(3.0,2.5);
	d=pic(p,pf);
	p.x+=7.0;
	d2=pic(p,pf);
	if(d2<d){d=d2;a=0.0;b=0.21;d2=500.0;x-=7.0;}else{p.x-=10.0;d2=6.0;}
	if(d<ds){xs=x;pfs=pf;ds=d;if(a!=0.0){ CH(0,0,0,_R,_E,_A,_L,_I,_S,_M,0,0,0) }else{ CH(_E,_X,_P,_R,_E,_S,_S,_I,_O,_N,_I,_S,_M) }}
	if(d<0.0){
		spec=0.5;
		col=art3(vec3(0.0,1.0,0.0),normalize(vec3(p*0.125,2.0)),a,b);
	}
	
	x=18.0;
	p.x=p0.x-x;pf=vec2(1.0,0.6);
	d=pic(p,pf);
	
	if(d<ds){xs=x;pfs=pf;ds=d;CH(_D,_E,_G,_E,_N,_E,_R,_A,_T,_I,_V,_E,0) }
	if(d<0.0){
		if(time<45.0)col=vec3(0.1);
		else{spec=0.0;col=art5(p);}
	}
	
	//nameplate
	p.x=p0.x-xs;p.y+=pfs.y+0.4;
	d=max(abs(p.x)-1.0,abs(p.y)-0.25);
	if(d<0.0 || time<3.0){
		spec=1.0;d2=1.0;
		vec3 scol=vec3(0.6,0.4,0.2);
		p.x-=0.8;
		if(time<3.0){scol=col;d=-1.0;p=uv;p.x=-p.x;CH(0,0,0,_S,_O,_M,_E,_D,_A,_Y,0,0,0)}
		for(int i=0;i<13;i++){
			d2=min(d2,chr(ch[i],p*vec2(-10.0,5.0)));
			p.x+=0.14;
		}
		scol=mix(vec3(0.0),scol,smoothstep(0.0,30.0*px,d2));
		col=mix(col,scol,clamp(-d*15.0,0.0,1.0));
	}
	
	N=vec3(0.0,0.0,-sign(rd.z));
	col*=(0.5+dot(N,L)*0.5);
	
	col+=spec*LC*pow(max(0.0,dot(reflect(rd,N),L)),4.0);	
	
	float t=(3.9*sign(rd.z)-ro.z)/rd.z,dm=10.0,tm;
	for(int i=0;i<3;i++){
		t+=d=DEF(ro+rd*t,xs,pfs);
		if(d<dm){dm=d;tm=t;}
		if(d<0.00001 || t>tW)break;
	}
	if(dm<px*t){
		col=mix(vec3(0.0),col,clamp(dm/(px*tm),0.0,1.0));
	}
	col*=min(time*0.1,1.0)*min((75.0-time)*0.1,1.0);
	fragColor = vec4(col,1.0);
}