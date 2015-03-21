// For LINUX, a good version is available at :
// https://www.shadertoy.com/view/4djGzV

// Created by sebastien durand - 01/2014
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// Based on :
//     - effie - D.E.A.L (https://www.shadertoy.com/view/4ss3Ws) 
//     - _ben's - Area lights (https://www.shadertoy.com/view/ldfGWs)
// *****************************************************************************






//    ------------------------------------------------
//  !!!             >>>  IMPORTANT  <<<              !!! 
//  !!! Decomment this on Windows, it may works fine !!!
//  !!!   Not sure for Linux (pease comment if ok)   !!!
//    ------------------------------------------------









  #define ANIMATION












//#define PRECALCULATE

// not happy at all if commented !  :'(

// for Linux, a dancing version is available at :
// https://www.shadertoy.com/view/4djGzV


const vec3 LaserColor = vec3(.2,.2,1.);
const float 
	SpecFalloff=3.3335,
	BloomFalloff=5000.0, 
	AttenFalloff=1.;//season to taste (higher # falls quicker)
//const float SpecFalloff=33.5, BloomFalloff=50000.0, AttenFalloff=10.0;//season to taste (higher # falls quicker)

#define TAO 6.28318530718
#define NB_ITER 45
#define MAX_DIST 400.
#define PRECISION .0001


// - LegoMan Constants & Globals ------------------------------------------

#define HEAD 1.
#define SHIRT 2.
#define MIDDLE 3.
#define LEGS 4.
#define ARMS 5.
#define HAND 6.

const float 
	legh = 1., 
	lege=.34, 
	legx=.31;

float laserLength = 0.;

// -- Tools -----------------------------------------------------------

float smin(in float a, in float b, in float k ) {
    float h = clamp( .5+.5*(b-a)/k, 0., 1. );
    return mix( b, a, h ) - k*h*(1.-h);
}

vec2 minObj(in vec2 o1, in vec2 o2) {
    return (o1.x<o2.x) ? o1 : o2;
}

mat2 Rot(in float a) {
	float sa=sin(a), ca=cos(a);
	return mat2(ca, -sa, sa, ca);
}

// -------------------------------------------------------------------

//vec4 boundingSphere;
vec3 armn = normalize(vec3(-.4,-1.,-.5) - vec3(-.25,-.5,0));
vec2 boby2 = normalize(vec2(1,.15));
mat2 handmat = Rot(-.5);

// -- Renderer Constants & Globals -------------------------------------
vec3 sunLight = normalize(vec3(-25,25.,-10));
vec2 fCoord;

// -- Legoman position and color -------------------------------------

struct Legoman {
#ifdef ANIMATION

    #ifdef PRECALCULATE
	mat2 
		rhead,
		rarm_left, rarm_right,
		rleg_left, rleg_right,
		rhand_left, rhand_right,
		rroll;
    #else
	float 
		head,
		arm_left, arm_right,
		leg_left, leg_right,
		hand_left, hand_right,
		roll;
    #endif
#endif
	vec3 pos;
	vec3 c_shirt, c_arms, c_middle, c_legs;
};

	Legoman man = Legoman(
#ifdef ANIMATION
#ifdef PRECALCULATE
        #else
		  -.2, 
		  2.,-.4,.6,-.5, 0.,0.,
		  0.,
        #endif
#endif
		  vec3(0,-1.9,0),
		  vec3(.96,.92,.75),vec3(.39,.17,.1),  vec3(.3,.1,0.), vec3(.69,.54,.39));


// -- Distance functions -----------------------------------------------

float sdCapsule(in vec3 p, in vec3 a, in vec3 b, in float r0, in float r1 ) {
    vec3 pa = p - a, ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0., 1.);
    return length( pa - ba*h ) - mix(r0,r1,h);
}

float roundCylinder(in vec3 p, in float h, in float r1, in float r2) {
    float a = abs(p.y)-(h-r2), b = length(p.xz)-r1;
    return min(min(max(a, b), max(a-r2, b+r2)), length(vec2(b+r2,a))-r2);
}

float head(in vec3 p) {
    float d = max(abs(p.y+.4)-.3, length(p.xz)-.326);
    d = min(d, roundCylinder(p, .425,.51,.1));
    p.y -= .425;
    return min(d, roundCylinder(p, .173, .245,.025));
}

float body(in vec3 p) {
    vec3 vd = abs(p) -  vec3(.75,.6,1.);
    float d = min(max(vd.x,max(vd.y,vd.z)),0.0) + length(max(vd,0.0));
    p.x = abs(p.x);
    d = max(dot(p.xy, boby2)-.7,d);
    p.y -= .4;    
    d = min(d, length(max(abs(p)- vec3(.47,.14,.45),0.0))-.16);
    return max(abs(p.z)-.392, d);
}

float leg(in vec3 p) {
    float d = length(p.zy)-lege;
    d = min(d, length(max(abs(p+vec3(0.,legh*.5,-.08))-vec3(legx,legh*.5,lege-.08),0.)));
    d = min(d, length(max(abs(p+vec3(0.,legh,.02))-vec3(legx,.15,lege+.02),0.)));
    d = max(abs(p.x)-legx, d)-.02;
#ifdef ANIMATION	
    vec3 dd = abs(p+vec3(0.,legh,-.08))-vec3(legx-.1,legh+.2,lege-.18);
    float d2 = min(max(dd.x,max(dd.y,dd.z)),0.) + length(max(dd,0.));
    dd = abs(p+vec3(0.,legh+.1,.02))-vec3(legx-.1,.15,lege-.98);
    d2 = min(d2, min(max(dd.x,max(dd.y,dd.z)),0.0) + length(max(dd,0.)));
    d2 = min(d2, max(-p.z-.05, length(p.xy-vec2(0,-.1))-.24));
    d2 = min(d2, max(-p.z-.05, length(p.xy-vec2(0,-.8))-.24));
    return max(-d2,d);
#else
	return d;
#endif	
}

float arm(in vec3 p) {
    float d = smin(sdCapsule(p, vec3(0), vec3(-.25,-.5,0), .22, .23), 
				   sdCapsule(p, vec3(-.25,-.5,0), vec3(-.4,-1.,-.5), .23, .24),.02); 
    return max(dot(p, armn) - .9, d);
}

float hand(in vec3 p) {
    float d1 = length(p)-.15;
	p.y+=.29;
    float d=length(p.xy);
    return max(abs(p.z+.09)-.2, max(-length(p.xy+vec2(-.1,.25))+.2, max(-d+.18, smin(d1, d-.26,.02))));
}

vec2 legoman(in vec3 p, in Legoman lego) {
	float sa,ca, a;
	bool isLeft = p.x>0.;
	
	// - Head & Shirt --------------------
	vec2 dHead = vec2(head(p+vec3(0,-1.4,0)), HEAD),
    	 dBody = vec2(body(p+vec3(0,-.15,0)), SHIRT);
 
	// - Middle --------------------------
    float middle = length(max(abs(p+vec3(0,.44,0))- vec3(.65,.1,.325),0.0))-.05;
    middle = min(middle,roundCylinder(p.yxz+vec3(.76,0,0),.06,.39,.02));
    vec2 dMiddle = vec2(middle,MIDDLE);   
	// - Legs ----------------------------
	p.x = -abs(p.x);
	vec3 p1 = p;
	p1.y +=.77;
#ifdef ANIMATION
    #ifdef PRECALCULATE    
	p1.yz *= isLeft?lego.rleg_left:lego.rleg_right; //Rot(-lego.leg_left);
	#else
    p1.yz *= Rot(isLeft?-lego.leg_left:-lego.leg_right); //Rot(-lego.leg_left);
	#endif	
#endif	
	p1.x += .38;
	vec2 dLeg = vec2(leg(p1), LEGS);
//   return minObj(minObj(minObj(dHead,dMiddle), dBody),dLeg);

	// - Arms ----------------------------
	p1 = p;
    p1 += vec3(.7,-.55,0);
#ifdef ANIMATION	
    #ifdef PRECALCULATE
	p1.yz *= isLeft?lego.rarm_left:lego.rarm_right;
	#else
    p1.yz *= Rot(isLeft?-lego.arm_left:-lego.arm_right);
    #endif
#endif	
    vec2 dArm = vec2(arm(p1), ARMS);
//    return minObj(minObj(minObj(minObj(dHead,dMiddle), dBody),dLeg),dArm);
	
	// - Hands ---------------------------
	p1 += vec3(.35,.85,.35);
	p1.yz *= handmat;
#ifdef ANIMATION	
    #ifdef PRECALCULATE
	p1.xz *= isLeft?lego.rhand_left:lego.rhand_right;
	#else
    p1.xz *= Rot(isLeft?-lego.hand_left:-lego.hand_right);
    #endif
#endif
    vec2 dHand = vec2(hand(p1), HAND);

	// - Mix -----------------------------
    return minObj(minObj(minObj(minObj(minObj(dHead, dBody),dHand),dArm),dMiddle),dLeg);
}

vec3 ManRef(vec3 p) {
	p *= 10.;
#ifdef ANIMATION	
     #ifdef PRECALCULATE
	p.yz *= man.rroll;//Rot(man.leg_right);
	#else
    p.yz *= Rot(man.roll);//Rot(man.leg_right);
	#endif
#endif
	return p + man.pos;
}

// -- Legoman colors ----------------------------------------- 

vec3 getTexture(in vec3 p, in float m, in Legoman lego) {
	p = ManRef(p);
	vec3 c;   
  
    if (m == HEAD) {
		c = vec3(1.,1.,0);
#ifdef ANIMATION	
    #ifdef PRECALCULATE
		p.xz*= lego.rhead;
	#else
        p.xz*= Rot(lego.head);
	#endif		
#endif		
		if (p.z<0.) { // draw face			
			vec2 p2 = p.xy;
			p2.y -= 1.46;
			p2 *= 100.; // scale because 
			float face_r = 27.;
			float face_x = face_r*0.453596121, //face_r*cos(a); // precalcul
				  face_y = -face_r*0.89120736; //face_r*sin(a); // precalcul
			float px = abs(p2.x);
			float e = 4.-.08*px;
			float v = (px<face_x && p2.y<-e) ? abs(length(p2)-face_r)-e : 
					  (p2.y<-e) ? length(vec2(px,p2.y)-vec2(face_x,face_y))-e :
					  length(vec2(px,p2.y)-vec2(face_x,-face_y*.1))-1.8*e; 
			v = clamp(v, 0., 1.);
			c = mix(vec3(0), c, v);
		}
    } else {
        c = m == HAND   ? vec3(1.,1.,0.) :
			m == SHIRT  ? lego.c_shirt :
			m == MIDDLE ? lego.c_middle :
			m == LEGS   ? lego.c_legs : lego.c_arms;
    }
	return c;
}

// Standard Ray-Marching stuff --------------------------------------------

float DEL(vec3 p){ // distance estimated light
	p = ManRef(p);
	p += vec3(.7,-.55,0);
#ifdef ANIMATION	
    #ifdef PRECALCULATE    
	p.yz *= man.rarm_right;
    #else
	p.yz *= Rot(-man.arm_right);
    #endif
#endif	
	p += vec3(.35,.85,.35);
	p.yz *= handmat;
#ifdef ANIMATION	
     #ifdef PRECALCULATE  
	p.xz *= man.rhand_right;
	#else
    p.xz *= Rot(-man.hand_right);
    #endif
#endif	
	return .1*sdCapsule(p, vec3(0,-.25,0),vec3(0,-.25,-laserLength),.13, .13);
}

vec2 DE(in vec3 p) {
	vec2 res = legoman( ManRef(p), man);
	return vec2(.1*res.x, res.y);
}

// ---------------------------

vec4 cp; //closest point to light along ray (well kinda, very inaccurate if ray does not come close)

vec2 map( in vec3 pos ) {
	float bump = 0.0;
	if(pos.y<0.005) bump = texture2D( iChannel0, pos.xz * 6.0 ).x * 0.002;
	vec2 res = vec2( pos.y + bump, 10.0 );
	res = minObj(res, DE(pos));

	float dL = DEL(pos);
	if(dL<cp.w) cp=vec4(pos,dL);//catch the position nearest the light as we march by (accurate only when close)
	if(dL<res.x)res=vec2(dL,-2.0);
	return res;
}

vec2 castRay( in vec3 ro, in vec3 rd, in float maxd ) {//same as iq's but adding a light trap
	cp=vec4(1000.0);//reset the light trap
	vec2 res=vec2(100.0);
	float t = PRECISION*10.0;
	for( int i=0; i<NB_ITER; i++ )
	{
		if( res.x<PRECISION||t>maxd ) break;
		res = map( ro+rd*t );
		t += res.x;
	}
	if( t>maxd ) res.y=-1.0;
	return vec2( t, res.y );
}

vec3 findLightDir(vec3 p, float d){ //find the light direction given a point and estimated distance
	vec2 v=vec2(d,0.0);
	return normalize(vec3(DEL(p-v.xyy)-DEL(p+v.xyy),DEL(p-v.yxy)-DEL(p+v.yxy),DEL(p-v.yyx)-DEL(p+v.yyx)));
}

float specTrowbridgeReitz( float HoN, float a, float aP ) {
	float a2 = a*a;
	return (a2*aP*aP)/pow(HoN*HoN*(a2-1.)+1.,2.);
}

float visSchlickSmithMod( float NoL, float NoV, float r ) {
	float k = pow(r*.5+.5,2.) * .5;
	return 1./(4.*(NoL*(1.-k)+k)*NoV*(1.-k)+k );
}

float fresSchlickSmith( float HoV, float f0 ) {
	return f0+(1.-f0)*pow(1.-HoV, 5.);
}

float findSpecLight( vec3 pos, vec3 N, vec3 V, float f0, float roughness )
{//calculates the specular portion of light by finding the point on the light closest to the reflected ray
 //and using that as the light direction
	float res = 0.;
	if (laserLength>.1){
	castRay(pos, reflect( -V, N ), 3.0);//find the closest point to the light along the reflected ray
	float distLight=max(DEL(pos),length(cp.xyz-pos));//find the distance to that point
	vec3 NL=findLightDir(cp.xyz,cp.w); //the direction to light from closest point on reflected ray
	vec3 closestPoint=cp.xyz+NL*cp.w; //an estimate of the light surface point nearest the reflected ray
	vec3 l=normalize(closestPoint-pos); //the direction to this point

	//pretty much the same as original from here
	float NoV		= clamp( dot( N, V ),0.,1.);	
	vec3 h			= normalize( V + l );
	float HoV		= dot( h, V );
	float NoL		= clamp( dot( N, l ),0.,1.);	
	float alpha		= roughness * roughness;
	float alphaPrime	= clamp( 1./(1.+distLight*SpecFalloff) + alpha, 0., 1. );//not sure at all about this??
	float specD		= specTrowbridgeReitz( clamp(dot(h,N),0.,1.), alpha, alphaPrime );
	float specF		= fresSchlickSmith( HoV, f0 );
	float specV		= visSchlickSmithMod( NoL, HoV, roughness );
	res = specD*specF*specV*NoL;
	}
	return res;
}
	
vec3 areaLights( vec3 pos, vec3 nor, vec3 rd, vec3 col)
{
	vec3 albedo		= pow(col, vec3(2.2));
	float roughness = 0.7 - clamp( 0.5 - dot( albedo, albedo ), 0.05, 0.95 );
	float dist	= DEL(pos);
	vec3 L		= findLightDir(pos,dist); //the direction to light
	float NdotL	= max(0.0,dot(nor,L));
	vec2 vShad	= castRay(pos, L, 0.5);
	float shad	= ((vShad.y>-0.5)?1.0-NdotL:1.0);
	float spec	= findSpecLight( pos, nor, -rd, .3, roughness );
	float atten	= 1.0/(1.0+dist*dist*AttenFalloff);
	vec3 color	= albedo * 0.63 * NdotL * atten * shad + spec;
	return pow(color,vec3(1./2.2));
}

const vec2 eps = vec2(0.001, 0. );
vec3 calcNormal(in vec3 pos) {
	return normalize(vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x ));
}

float SoftShadow(in vec3 ro, in vec3 rd) {
    float res = 1.0, h, t = .02;
    for( int i=0; i<15; i++ ) {
		h = DE( ro + rd*t ).x;
		res = min( res, 10.*h/t );
		t += .06;
    }
    return clamp(res, 0., 1.);
}

float CalcAO(in vec3 pos, in vec3 nor) {
    float dd, hr=.01, totao=.0, sca=1.;
    for(int aoi=0; aoi<4; aoi++ ) {
        dd = DE(nor * hr + pos).x;
        totao += -(dd-hr)*sca;
        sca *= .8;
        hr += .03;
    }
    return clamp(1.-4.*totao, 0., 1.);
}

vec3 render( in vec3 ro, in vec3 rd ) {
	vec2 res = castRay(ro,rd,MAX_DIST);
	vec3 pos, nor, c0, col = LaserColor/(1.+cp.w*cp.w*BloomFalloff);
	if( res.y>-0.5 ) {
		pos = ro + res.x * rd;
		nor = calcNormal(pos); // TODO le faire direct sur le bon objet
		if (res.y>0.5&&res.y<10.) {
			c0 =  getTexture(pos, res.y, man); 
		} else {
			c0 = .5*texture2D( iChannel0, pos.xz/*+vec2(0,-120.*iGlobalTime)*/).xxx;
			//c*=vec3(.4,1.2,.4);
		}
		c0;
		float sh = 1.,
          ao = CalcAO(pos, nor ),
          amb = clamp(.5+.5*nor.y, .0, 1.),
          dif = clamp(dot( nor, sunLight ), 0., 1.);
		sh = SoftShadow( pos, sunLight); 
		dif *= (.1+.8*sh); 

		vec3 brdf =
			ao*.2*(amb)+// + bac*.15) +
			1.2*dif*vec3(1.,.9,.7);
	
		float
			pp = clamp(dot(reflect(rd,nor), sunLight),0.,1.),
			spe = 4.*sh*pow(pp,16.),
			fre = ao*pow( clamp(1.+dot(nor,rd),0.,1.), 2.);
	
		vec3 c = c0*(brdf + spe) + fre*(.5*c0+.5);//*exp(-.01*res.x*res.x);
   	//	return sqrt(c);
		col = .2*sqrt(c)+ max(col,LaserColor*areaLights( pos, nor, rd, c0 ));
	}//else if(res.y<-1.5)col=vec3(1.0);
	return clamp(col,0.0,1.0);
}

mat3 LookAt(in vec3 ro, in vec3 up){
    vec3 fw=normalize(ro),
    	 rt=normalize(cross(fw,up));
    return mat3(rt, cross(rt,fw),fw);
}

vec3 RD(in vec3 ro, in vec3 cp) {
    return LookAt(cp-ro, vec3(0,1,0))*normalize(vec3((2.*fCoord-iResolution.xy)/iResolution.y, 12.0));
} 

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 q = fragCoord.xy/iResolution.xy;
	float time = 9.0 + iGlobalTime*.2;
	float time2 = time*16.;

		// - Animation -----------------------------------------------
#ifdef ANIMATION
	float c2 = cos(2.*time2);
	float c4 = cos(4.*time2);
    
#ifdef PRECALCULATE    
    #else
	man.head = .2*c4;
	man.hand_left = .6*c2;
	man.hand_right = man.hand_left+.5;	
	man.arm_left  = 1.2+.5*c2;
	man.arm_right  = man.arm_left -.5; 
	man.leg_left  = .8*c2;
	man.leg_right  = -man.leg_left;
	man.roll =-.1;
#endif
    man.pos = vec3(0,-2.2+.4*c4,0);
#endif
	//------------------------------------------------------------
	laserLength = clamp(mod(iGlobalTime*15., 60.)-20., 0., 3.);

	fCoord = fragCoord.xy;
	vec3 ro =4.*vec3(1.2*cos(iMouse.x*.01+time*2.),iMouse.y/200. + 1.2+0.6*cos(time*6.),1.2*sin(iMouse.x*.01+time*2.));
	vec3 rd = RD(ro,vec3(-.2, .19, 0));		
	vec3 col = render( ro, rd );

	fragColor = vec4((q.y>.15&&q.y<.85)?col:vec3(0), 1);
}
