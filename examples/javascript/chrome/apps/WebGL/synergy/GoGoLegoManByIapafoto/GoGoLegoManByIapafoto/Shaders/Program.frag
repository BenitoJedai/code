// Created by sebastien durand - 01/2014
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

//#define ANTIALIASING
const float AA = 3.; // Anti-Aliasing Level


#define TAO 6.28318530718
#define NB_ITER 60
#define MAX_DIST 400.
#define PRECISION .002


const vec2 
	V01 = vec2(0,1),
	Ve = vec2(.001,0),
	leg1 = vec2(0,-.1),
	leg2 = vec2(0,-.8),
	hand2 = vec2(-.1,.25);	

const vec3
	v0 = vec3(0),
	head0 = vec3(0,-1.4,0),
	body0 = vec3(0,-.15,0),
	middle1 = vec3(0,.44,0),
	middle2 = vec3(.65,.1,.325),
	middle3 = vec3(.76,0,0),
	arm0 = vec3(.7,-.55,0),
	hand0 = vec3(.4,1.,.5),
	bbody = vec3(.47,.14,.45),
	bbody1 = vec3(.75,.6,1.),
	arm1 = vec3(-.25,-.5,0),
	arm2 = vec3(-.4,-1.,-.5),
	hand1 = vec3(.02,.15,0);

const lowp float 
	legh = 1., 
	lege=.34, 
	legx=.31, 
	handa = -.7,
	face_a = 1.1,
	face_r = 27.;

const float 
	face_x = 27.*0.453596121, //face_r*cos(a); // precalcul
	face_y = -27.*0.89120736; //face_r*sin(a); // precalcul


// Global variables
float time;
vec3 sunLight, deltaMan, armn;
vec2 boby2;
mat2 handmat;
vec2 fCoord;
int scene;

float sdCapsule(in vec3 p, in vec3 a, in vec3 b, in float r0, in float r1 ) {
    vec3 pa = p - a, ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0., 1.);
    return length( pa - ba*h ) - mix(r0,r1,h);
}

float smin(in float a, in float b, in float k ) {
    float h = clamp( .5+.5*(b-a)/k, 0., 1. );
    return mix( b, a, h ) - k*h*(1.-h);
}

// h = .5, //  half of height
// r1 = 1., //main rayon
// r2 = .2, // top border
float roundCylinder(in vec3 p, in float h, in float r1, in float r2) {
    float
        a = abs(p.y)-(h-r2),
        b = length(p.xz)-r1;
    return min(min(max(a, b), max(a-r2, b+r2)), length(vec2(b+r2,a))-r2);
}

float head(in vec3 p) {
    float d = max(abs(p.y+.4)-.3, length(p.xz)-.326);
    d = min(d, roundCylinder(p, .425,.51,.1));
    p.y -=.425;
    return min(d, roundCylinder(p, .173, .245,.025));
}

float body(in vec3 p) {
    vec3 vd = abs(p) - bbody1;
    float d = min(max(vd.x,max(vd.y,vd.z)),0.0) + length(max(vd,0.0));
    p.x = abs(p.x);
    d = max(dot(p.xy, boby2)-.7,d);
    p.y -= .4;    
    d = min(d, length(max(abs(p)-bbody,0.0))-.16);
    return max(abs(p.z)-.392, d);
}

float leg(in vec3 p) {
    float d = length(p.zy)-lege;
    d = min(d, length(max(abs(p+vec3(0.,legh*.5,-.08))-vec3(legx,legh*.5,lege-.08),0.)));
    d = min(d, length(max(abs(p+vec3(0.,legh,.02))-vec3(legx,.15,lege+.02),0.)));
    d = max(abs(p.x)-legx, d)-.02;
    vec3 dd = abs(p+vec3(0.,legh,-.08))-vec3(legx-.1,legh+.2,lege-.18);
    float d2 = min(max(dd.x,max(dd.y,dd.z)),0.) + length(max(dd,0.));
    dd = abs(p+vec3(0.,legh+.1,.02))-vec3(legx-.1,.15,lege-.98);
    d2 = min(d2, min(max(dd.x,max(dd.y,dd.z)),0.0) + length(max(dd,0.)));
    d2 = min(d2, max(-p.z-.05, length(p.xy-leg1)-.24));
    d2 = min(d2, max(-p.z-.05, length(p.xy-leg2)-.24));
    return max(-d2,d);
}

float arm(in vec3 p) {
    float d = smin(sdCapsule(p, v0, arm1, .22, .23), 
				   sdCapsule(p, arm1, arm2, .23, .24),.02); 
    return max(dot(p, armn) - .9, d);
}

float hand(in vec3 p) {
    p.yz *= handmat;
    float d1 = length(p-hand1)-.15;
    p.zy+=.08;
    float d = length(p.xy);
    d = max(-d+.18, smin(d1, d-.26,.02));
    d = max(-length(p.xy+hand2)+.2,d);
    return max(abs(p.z)-.2, d);
}

vec2 minObj(in vec2 o1, in vec2 o2) {
    return (o1.x<o2.x) ? o1 : o2;
}

ivec2 getId(in vec3 p) {
    float k = 5.;
    return (ivec2((k*100.+p.x)/k, (k*100.+p.z)/k)-100);
}

vec2 legoman(in vec3 p, in ivec2 id) {

	float a, bodyA;
	vec3 p0 = p;
	float sa,ca, anim=0.;

	if (scene!=1) {
		anim = -1.1+cos(float(-id.y)*.7 + 6.*iGlobalTime);
	} else { // walking
		anim = (p0.x<0.?1.:-1.)*cos(6.*iGlobalTime-4.);
	} 
	
	if (scene==1) {
		p += deltaMan;
	}
	else if(scene==2) {
		//anim = cos(6.*iGlobalTime);
  		bodyA = .12*anim;
		sa=sin(bodyA); 
		ca=cos(bodyA);
		p.yz *= mat2(ca, -sa, sa, ca);
	}	
	
	vec2 dHead = vec2(head(p+head0),1.);
    vec2 dBody = vec2(body(p+body0),2.);
   
    float middle = length(max(abs(p+middle1)- middle2,0.0))-.05;
    middle = min(middle,roundCylinder(p.yxz+middle3,.06,.39,.02));
    vec2 dMiddle = vec2(middle,3.);   
    p.x = -abs(p.x);
           
	vec3 p1 = p;
	p1.y +=.77;

	if (scene==1) { // id.x==0 && id.y==0) {
        a = -.4*anim;
		sa=sin(a); 
		ca=cos(a);
		p1.yz *= mat2(ca, -sa, sa, ca);
	} else if (scene == 2) {
		sa=sin(-2.*bodyA); 
		ca=cos(-2.*bodyA);
		p1.yz *= mat2(ca, -sa, sa, ca);
	}
	
	vec2 dLeg = vec2(leg(p1+vec3(.38,.77-.77,0)),4.);

    p += arm0;

    if (scene!=0 || id.x==0 && id.y==0) {
        a = -.5 + anim;
		sa=sin(a);
		ca=cos(a);
        p.yz *= mat2(ca, -sa, sa, ca);
	}

    vec2 dArm = vec2(arm(p),5.);
    vec2 dHand = vec2(hand(p+hand0),6.);

    return minObj(minObj(minObj(minObj(minObj(dHead, dBody),dHand),dArm),dMiddle),dLeg);
}

vec2 DE(in vec3 p) {
    float k = 5.;
    ivec2 id = getId(p);
    p.xz = mod(p.xz, k)-0.5*k;
    return minObj(legoman(p, id), vec2(p.y+1.93,10.));
}

vec3 N(vec3 p) {
    return normalize(vec3(
        DE(p+Ve.xyy).x - DE(p-Ve.xyy).x,
        DE(p+Ve.yxy).x - DE(p-Ve.yxy).x,
        DE(p+Ve.yyx).x - DE(p-Ve.yyx).x
    ));
}

float softshadow(in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k) {
    float res = 1.0, h, t = mint;
    for( int i=0; i<28; i++ ) {
      //  if (t < maxt) {
            h = DE( ro + rd*t ).x;
            res = min( res, k*h/t );
            t += 0.14;
      //  }
    }
    return clamp(res, 0., 1.);
}

float calcAO(in vec3 pos, in vec3 nor) {
    float dd, hr=.01, totao=.0, sca=1.;
    for(int aoi=0; aoi<5; aoi++ ) {
        dd = DE(nor * hr + pos).x;
        totao += -(dd-hr)*sca;
        sca *= .7;
        hr += .05;
    }
    return clamp(1.-4.*totao, 0., 1.);
}


vec3 mandelbrot(in vec2 uv) {
	float k = .5+.5*cos(iGlobalTime);
    uv *= mix(.02, 2., k);
	uv.x-=(1.-k)*1.8;
    vec2 z = vec2(0);
    vec3 c = vec3(0);
    for(float i=0.;i<14.;i++) {
        if(length(z) >= 4.) continue;
        z = vec2(z.x*z.x-z.y*z.y, 2.*z.y*z.x) + uv;
        if(length(z) >= 2.0) {
            c.r = i*.05;
            c.b = sin(i*.2);
        }
    }
    return sqrt(c);
}

vec3 getTexture(in vec3 p, in float m) {
    ivec2 id = getId(p);

	vec3 p0 = p;
    float k = 5.;
    p.xz = mod(p.xz, k)-0.5*k;
	if (scene==1) {
		p += deltaMan;
	} else if (scene == 2) {
		
		float anim = -1.1+cos(float(-id.y)*.7 + 6.*iGlobalTime);
		
  		float bodyA = .12*anim;
		float sa=sin(bodyA); 
		float ca=cos(bodyA);
		p.yz *= mat2(ca, -sa, sa, ca);
	}	
    vec3 c;   
  
    if (m==1.) {
		c = vec3(1.,1.,0);
		float g = mod(iGlobalTime, TAO*3.);
		if (id.x==0 && id.y==0 && g > 2.5*TAO) {
			float a = .8*cos(2.*g+1.57);
			p.xz*= mat2(cos(a), -sin(a), sin(a), cos(a));
		}
		if (p.z<0.) {
			// Draw face
			vec2 p2 = p.xy;
			p2.y -= 1.46;
			p2 *= 100.;
			float px = abs(p2.x);
			float e = 4.-.08*px;
			float v = 
					(px<face_x && p2.y<-e) ? abs(length(p2)-face_r)-e : 
					(p2.y<-e) ? length(vec2(px,p2.y)-vec2(face_x,face_y))-e :
					length(vec2(px,p2.y)-vec2(face_x,-face_y*.1))-1.8*e; 
			v = clamp(v, 0., 1.);
			c = mix(vec3(0), c, v);
		}
    }
    else if (m==2.) {
        c = (id.x==0 && id.y==0) ? mandelbrot(p.xy - vec2(.14,.15)) : vec3(1,0,0);
       
	} else if (m==10.) {
		if (scene!=1) time = 0.;
		float d = .3*sin(2.2+time);
		c = vec3(.75-.25*(mod(floor(p0.x),2.)+mod(floor(p0.z+d-time*.18),2.)));
	 	//c = vec3(.5+.5*smin(mod(floor(p0.x),2.),mod(floor(p0.z+d-time*.18),2.),1.));
	} else {
        c = m == 6. ? vec3(1.,1.,0)  :
			m == 3. ? vec3(.2,.2,.4) :
			m == 4. ? vec3(.1,.1,.2) :
			          vec3(1.,1.,1.);
		
    }
    if (m==10. || !(id.x==0 && id.y==0)) {
		// black & white
        float a = (c.r+c.g+c.b)*.33;
		c = vec3(1.,.95,.85)*a;
    }

	return c;
}


vec3 Render(in vec3 p, in vec3 rd, in float t, in float m) {
    vec3  col = getTexture(p, m),
    	  nor = N(p);
	float sh = 1.,
          ao = calcAO(p, nor ),
          amb = clamp(.5+.5*nor.y, .0, 1.),
          dif = clamp(dot( nor, sunLight ), 0., 1.),
          bac = clamp(dot( nor, normalize(vec3(-sunLight.x,0.,-sunLight.z))), 0., 1.)*clamp( 1.0-p.y,0.0,1.0);

	if( dif>.02 ) { sh = softshadow( p, sunLight, .02, 10., 12.); dif *= (.1+sh); }
	
	vec3 brdf = vec3(0.0);
	brdf += .2*ao*amb*vec3(0.10,0.11,0.13);
	brdf += .2*ao*bac*vec3(0.15);
	brdf += 1.2*dif*vec3(1.,.9,.7);
	
	float pp = /*1.1**/clamp( dot(reflect(rd,nor), sunLight ), 0.0, 1.);
	float spe = 1.2*sh*pow(pp,16.0);
	float fre = .2*ao*pow(clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );
	
	col = col*(brdf + spe) + .2*fre*(0.5+0.5*col);
    return sqrt(col);
}


mat3 lookat(in vec3 ro, in vec3 up){
    vec3 fw=normalize(ro),
    	 rt=normalize(cross(fw,up));
    return mat3(rt, cross(rt,fw),fw);
}

vec3 RD(in vec3 ro, in vec3 cp) {
    return lookat(cp-ro, V01.xyx)*normalize(vec3((2.*fCoord-iResolution.xy)/iResolution.y, 12.0));
} 

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	
// - Precalcul global variables ------------------------------
	time = 3.14+12.*iGlobalTime;
	sunLight = normalize(vec3(-10.25,30.33,-7.7));
	deltaMan = vec3(0,.05*sin(1.72+time),0);
	armn = normalize(arm2 - arm1);
	boby2 = normalize(vec2(1,.15));
	handmat = mat2(cos(handa), -sin(handa), sin(handa), cos(handa));
	
	float tAnim = mod(iGlobalTime, 3.14*9.);  
	scene = tAnim > 3.14*9. ? 1:
	        tAnim > 3.14*7. ? 2:
	        tAnim > 3.14*6. ? 0 : 1;
	
//------------------------------------------------------------
	
    vec2 
		obj, 
		mouse = (iMouse.xy/iResolution.xy)*6.28,
		q = fragCoord.xy/iResolution.xy;

    vec3 
		ro = 45.*vec3(-cos(mouse.x), max(.8,mouse.x-2.+sin(mouse.x)*cos(mouse.y)), -.5-sin(mouse.y)),
    	rd, cp = V01.xxx;
	
    vec3 ctot = vec3(0);
	
#ifdef ANTIALIASING 
	for (float i=0.;i<AA;i++) {
		fCoord = fragCoord.xy+.4*vec2(cos(6.28*i/AA),sin(6.28*i/AA));	
#else
		fCoord = fragCoord.xy;
#endif
    // Camera origin (o) and direction (d)
        rd = RD(ro, cp);

        // Ray marching
		float m=0.;
        float t=0.,d=1.;
		
        for(int i=0;i<NB_ITER;i++){
            if(abs(d)<PRECISION || t>MAX_DIST)continue;
            obj = DE(ro+rd*t);
            t+=d=obj.x *.85;
            if (abs(d)<PRECISION) {
                m=obj.y;
            }
        }
 
        // Render colors
        if(t<MAX_DIST){// if we hit a surface color it
            ctot += Render(ro + rd*t, rd,t, m);
        }
#ifdef ANTIALIASING 		
    }
	ctot /=AA;	
#endif 
	
	ctot *= pow(16.*q.x*q.y*(1.-q.x)*(1.-q.y), .11); // vigneting
	fragColor = vec4(ctot,1.0);

}