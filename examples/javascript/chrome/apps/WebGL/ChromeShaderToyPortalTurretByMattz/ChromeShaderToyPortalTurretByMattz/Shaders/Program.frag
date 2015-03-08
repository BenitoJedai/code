vec3 L = normalize(vec3(-.3, 1., .2));
vec3 n1 = normalize(vec3(2., -1., 0));
vec3 n3 = normalize(vec3(4.,  1., 0));

const float cz = -.8;
const vec3 tgt = vec3(0, cz, 0);
const vec3 cpos = vec3(0, cz, 11.5);

#define precis .01
#define farval 1e5
#define gap .03
#define hip .3
#define dmax 20.0
#define rayiter 70

#define MAT_SHINYBLACK .0
#define MAT_DULLGRAY   1.
#define MAT_MIDGRAY    2.
#define MAT_WHITE      3.
#define MAT_EYE        4.

#define OVERLAY

vec2 opU(in vec2 a, in vec2 b) {
	return a.x < b.x ? a : b;
}

float sdBox(in vec3 p, in vec3 b ) {
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),.0) +
         length(max(d,.0));
}

float sdCylinder(in vec3 p, in vec2 h) {
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),.0) + length(max(d,.0));
}


vec2 baseEllipsoid(in vec3 pos) {
    vec3 abc = vec3(1., 2.5, 1.);
    abc = pos/(abc*abc);
	return vec2((dot(pos * abc, vec3(.5)) - .5)*inversesqrt(dot(abc,abc)), MAT_WHITE);
}

float sdMidCyl(in vec3 pos) {
    return .5*baseEllipsoid(2.*pos*vec3(1.,1.,0)).x;
}


mat3 rotX(in float t) {
    float cx = cos(t), sx = sin(t);
    return mat3(1., 0, 0, 
                0, cx, sx,
                0, -sx, cx);
}

mat3 rotY(in float t) {
    float cy = cos(t), sy = sin(t);
	return mat3(cy, 0, -sy,
                0, 1., 0,
                sy, 0, cy);

}

vec2 ant(inout vec3 pos, in vec3 p1) {
	float u = clamp(dot(pos, p1)/dot(p1,p1), .0, 1.)-1.0;
    pos -= p1;
    return vec2(length(pos-p1*u)-.025, MAT_SHINYBLACK);
}


float sdCone( in vec3 p ){
    p.z -= 0.58;
	vec2 q = vec2( length(p.xy), -p.z );
	return max( max( dot(q,vec2(inversesqrt(2.))), -p.z), p.z-1. );
}

vec2 stick(in vec3 pos, in float l) {
	
    float d;
	d = dot(abs(pos.xz), normalize(vec2(3., -1.)));
    pos.z -= l;
    d = max(d,sdBox(pos, vec3(.15, .05, l)));
    
	return vec2(d, MAT_SHINYBLACK);
	
}

vec2 torus(in vec3 pos, in vec3 x, in vec3 y) {
	
	vec3 n = normalize(cross(x, y));
	
	vec3 pp = length(x)*normalize(pos - n*dot(n, pos));
    
	float d = length(pos-pp)-.05;
	d = max(d, dot(pos, normalize(cross(x, n))));
	d = max(d, dot(pos, normalize(cross(n, y))));

	return vec2(d, MAT_SHINYBLACK);
	
	
}


vec2 map(in vec3 pos) {

    // antennae
    vec2 rval = baseEllipsoid(pos);

    vec3 lpos = pos - vec3(-.3, .2, -.2);
    
    rval = opU(rval, ant(lpos, vec3(0, 2.7, 0)));
    
    lpos = pos - vec3(-.5, 2., -.2);
    rval = opU(rval, ant(lpos, vec3(0, 1., 0)));
    rval = opU(rval, ant(lpos, vec3(.2, .2, 0)));
    rval = opU(rval, ant(lpos, vec3(0, .4, 0)));
    	
	pos.x = abs(pos.x);
    
	
	rval.x = max(rval.x, min(dot(pos, n1), sdMidCyl(pos)));
	rval.x = max(rval.x, -sdCone(pos));
    rval.x = max(rval.x, -abs(pos.x)+.5*gap);
	rval.x = max(rval.x, -sdBox(pos - vec3(0, -.15, 0), vec3(.7, .7, .5)));
	//rval.x = max(rval.x, -sdCylinder((pos-vec3(0, 0, .5)).xzy, vec2(.23, .5)));

    rval = opU(rval, vec2(length(pos-vec3(0, 0,.56))-.3, MAT_EYE));
	rval = opU(rval, vec2(sdBox(pos-vec3(0, -.15, 0), vec3(.3, .75, .8)), MAT_DULLGRAY));
    rval = opU(rval, vec2(sdBox(pos+vec3(0, 13.5, .5), vec3(4., 10.1, 4.)), 2.9));
	//rval = opU(rval, vec2(pos.y+3.4, 2.5));
       
	// front leg
    rval = opU(rval, torus(pos-vec3(.45, -1.2, .3), 
                           vec3(-.45, .1, -1.), 
                           vec3( .45, -.25, .9)));

	lpos = rotX(1.8)*rotY(-.2)*(pos+vec3(-.5, 2.5, 0));

	vec2 e = baseEllipsoid(lpos);
    
	e.x = max(e.x, -(e.x+hip));	
	e.x = max(e.x, -sdMidCyl(lpos));
	e.x = max(e.x, dot(lpos, n3));
	e.x = max(e.x, -lpos.x);
	e.x = max(e.x, -lpos.z);
    
    rval = opU(rval, e);
    rval = opU(rval, stick(lpos-vec3(.22, -2.25, -1.45), 0.775));

    // back leg
    rval = opU(rval, torus(pos-vec3(0, -.7, -1.15),
                           vec3(0, .05, -.7),
                           vec3(0, -.35, .55)));
	
	lpos = rotX(-1.8)*(pos+vec3(0, 1.5, .4));
	
	e = baseEllipsoid(lpos);
    
	e.x = max(e.x, -(e.x+hip));	
	e.x = max(e.x, -sdMidCyl(lpos));
	e.x = max(e.x, dot(lpos, n3));
	e.x = max(e.x, lpos.z);

    rval = opU(rval, e);

	rval = opU(rval, stick(vec3(0, -2.32, 2.5)-lpos, 1.3));

	
    // side
    pos.x -= .7;

    e = baseEllipsoid(pos);
		
	e.x = max(e.x, -sdMidCyl(pos)+gap);
	e.x = max(e.x, dot(pos, -n3)+gap);
	e.x = max(e.x, dot(pos, -n1)+gap);
	
	e.x = max(e.x, -sdBox(pos-vec3(1.6, 0, 0), vec3(.75, .1, .5)));
	
    rval = opU(rval, e);

 	e = vec2(sdBox(pos-vec3(.35, -.15, 0), vec3(.3, .65, .4)), MAT_DULLGRAY);
	e = opU(e, vec2(sdBox(vec3(pos.xy, abs(pos.z))-vec3(0, -.2, .3), vec3(.5, .05, .05)), MAT_SHINYBLACK));
    pos.y = abs(pos.y);
	e = opU(e, vec2(sdCylinder((pos-vec3(.28, .2, .5)).xzy, vec2(.13, .2)), MAT_MIDGRAY));
	e.x = max(e.x, -sdCylinder((pos-vec3(.28, .2, .35)).xzy, vec2(.1, .4)));
    
    rval = opU(rval, e);
    
	return rval;
	
}


vec2 castRay( in vec3 ro, in vec3 rd )
{
    vec2 res = vec2(precis*2., -1.);
    float t = .0;
    for( int i=0; i<rayiter; i++ )
    {
        if( abs(res.x)<precis||t>dmax ) continue;//break;
        t += res.x;
	    res = map( ro+rd*t );
	    res.y = res.y;
    }
	if (t > dmax || abs(res.x) > precis) { res.y = -1.; }
    res.x = t;
    return res;
}

vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( .001, 0, 0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float totao = .0;
    float sca = 1.;
    for( int aoi=0; aoi<5; aoi++ )
    {
        float hr = .01 + .04*float(aoi);
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        totao += -(dd-hr)*sca;
        sca *= .8;
    }
    return clamp( 1. - 3.*totao, .0, 1. );
}

#ifdef OVERLAY

float raycyl(vec3 o, vec3 d) { 
   	
    float r = .025;
	float a = dot(d,d) - d.z*d.z;
	float b = 2.*(dot(o,d)-o.z*d.z);
	float c = dot(o,o) - o.z*o.z - r*r;
		
    a *= 2.;
    
	float qd = b*b - 2.*a*c;
 	float sd = sqrt(qd);
	float x1 = (-b + sd)/a;
    float x2 = (-b - sd)/a;
    
    float t = qd < .0 ? farval : min(x1 < .0 ? farval : x1,
			 			             x2 < .0 ? farval : x2);
    
	float u = o.z + t*d.z;

    return (t < farval && u >= .0) ? t : farval;
	
}

#endif


vec3 shade( in vec3 ro, in vec3 rd ) {
	
	vec2 tm = castRay(ro, rd);
	vec3 rcolor;
	
	if (tm.y >= .0) {
		vec3 pos = ro + tm.x * rd;
		vec3 nor = calcNormal(pos);
		float ao = calcAO( pos, nor );
		
		// 0 = shiny black
		// 1 = dark gray
		// 2 = mid gray
		// 3 = shiny white
		// 4 = eye
		
		float r = clamp(mix(.2, 1., tm.y/3.), .0, 1.);
		r*=r;
		float gb = tm.y < 4. ? r : .0;
		
		if (tm.y == 4.) {
			float k = length(pos.xy);
			r *= (1. - 1.*cos(k*30.0));
			float a = atan(pos.y, pos.x);
			r *= (1. - .5*cos(a*16.));
		}
		
		float gamma = abs(tm.y - 1.5) < .75 ? .0 : .5;
		
		vec3 color = vec3(r, vec2(gb));
		
		float c1 = dot(nor, L);

		vec3 diffamb = (.5 + .5 * max(c1, -c1*.2))*color;
		
		vec3 R = 2.*nor*dot(nor,L)-L;
		float spec = gamma*pow(clamp(-dot(R, rd), .0, 1.), 24.);
		rcolor = (diffamb + spec)*ao;

	} else {
		float g = max(.3*dot(L,rd)+.6, .0);
		rcolor = vec3(g*g);
	}
    
#ifdef OVERLAY
	
	if (raycyl(ro, rd) < tm.x) {
		rcolor = mix(rcolor, vec3(1., .3, .3), .5 + .15*sin(94.*iGlobalTime));
	}
    
#endif

	return rcolor;
	
}


void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	
	vec2 uv = (fragCoord.xy - .5*iResolution.xy) * 0.8 / (iResolution.y);
	
	vec3 rz = normalize(tgt - cpos),
        rx = normalize(cross(rz,vec3(0,1.,0))),
        ry = cross(rx,rz);
	
	float thetax = -.05, thetay = -.10;
	
	if (max(iMouse.x, iMouse.y) > 20.0) { 
		thetax = (iMouse.y - .5*iResolution.y) * 3.14/iResolution.y; 
		thetay = (iMouse.x - .5*iResolution.x) * -6.28/iResolution.x; 
	}
	
    mat3 R = mat3(rx,ry,rz)*rotX(thetax)*rotY(thetay);

	vec3 rd = R*normalize(vec3(uv, 1.)),
        ro = tgt + R*vec3(0,0,-length(cpos-tgt));
	
	fragColor.xyz = pow(shade(ro, rd),vec3(.8));
	
}
