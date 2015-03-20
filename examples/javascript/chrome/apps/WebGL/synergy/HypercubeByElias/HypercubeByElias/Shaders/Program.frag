#define STEPS 32
#define PRECISION 0.01
#define DEPTH 10.0
//#define CROSSEYE

vec3 eye = vec3(0,0,-2.5);
vec2 uv; bool hit = false;

float lines, dots, lineWidth = 0.025;
float t = mod(iGlobalTime,1.0);
float s = (sin(iGlobalTime*0.5)+1.0)/2.0*0.15+0.15;
		
// iq's magic distance function
float line(vec3 p,vec3 a,vec3 b){vec3 pa=p-a,ba=b-a;float h=clamp(dot(pa,ba)/dot(ba,ba),0.0,1.0);return length(pa-ba*h)-lineWidth;}

// Rotation
mat3 rotZ(float a){float s=sin(a);float c=cos(a);return mat3(c,-s,0,s,c,0,0,0,1);}
mat3 rotX(float a){float s=sin(a);float c=cos(a);return mat3(1,0,0,0,c,s,0,-s,c);}
mat3 rotY(float a){float s=sin(a);float c=cos(a);return mat3(c,0,-s,0,1,0,s,0,c);}

// Marching
float scene(vec3);
vec3 getNormal(vec3 p){vec2 e=vec2(PRECISION,0);return(normalize(vec3(scene(p+e.xyy)-scene(p-e.xyy),scene(p+e.yxy)-scene(p-e.yxy),scene(p+e.yyx)-scene(p-e.yyx))));}
vec3 march(vec3 ro,vec3 rd){float t=0.0,d;hit=false;for(int i=0;i<STEPS;i++){d=scene(ro+rd*t);if(d<PRECISION){hit=true;break;}if(t>DEPTH){break;}t+=d;}return(ro+rd*t);}
vec3 lookAt(vec3 o,vec3 t,vec2 p){vec2 uv=(2.0*p-iResolution.xy)/iResolution.xx;vec3 d=normalize(t-o),u=vec3(0,1,0),r=cross(u,d);return(normalize(r*uv.x+cross(d,r)*uv.y+d));}

// Vertices
const vec3 lbf = vec3(-0.5,-0.5,-0.5);
const vec3 rbf = vec3( 0.5,-0.5,-0.5);
const vec3 lbb = vec3(-0.5,-0.5, 0.5);
const vec3 rbb = vec3( 0.5,-0.5, 0.5);

const vec3 ltf = vec3(-0.5, 0.5,-0.5);
const vec3 rtf = vec3( 0.5, 0.5,-0.5);
const vec3 ltb = vec3(-0.5, 0.5, 0.5);
const vec3 rtb = vec3( 0.5, 0.5, 0.5);

vec3 lbfi = vec3(-0.5+s,-0.5+s,-0.5+s);
vec3 rbfi = vec3( 0.5-s,-0.5+s,-0.5+s);
vec3 lbbi = vec3(-0.5+s,-0.5+s, 0.5-s);
vec3 rbbi = vec3( 0.5-s,-0.5+s, 0.5-s);

vec3 ltfi = vec3(-0.5+s, 0.5-s,-0.5+s);
vec3 rtfi = vec3( 0.5-s, 0.5-s,-0.5+s);
vec3 ltbi = vec3(-0.5+s, 0.5-s, 0.5-s);
vec3 rtbi = vec3( 0.5-s, 0.5-s, 0.5-s);

vec3 lbf_lbfi = mix(lbf,lbfi,t);
vec3 ltf_ltfi = mix(ltf,ltfi,t);
vec3 lbb_lbbi = mix(lbb,lbbi,t);
vec3 ltb_ltbi = mix(ltb,ltbi,t);

vec3 rbb_lbb = mix(rbb,lbb,t);
vec3 rbf_lbf = mix(rbf,lbf,t);
vec3 rtf_ltf = mix(rtf,ltf,t);
vec3 rtb_ltb = mix(rtb,ltb,t);

vec3 lbfi_rbfi = mix(lbfi,rbfi,t);
vec3 lbbi_rbbi = mix(lbbi,rbbi,t);
vec3 ltfi_rtfi = mix(ltfi,rtfi,t);
vec3 ltbi_rtbi = mix(ltbi,rtbi,t);

vec3 rbbi_rbb = mix(rbbi,rbb,t);
vec3 rbfi_rbf = mix(rbfi,rbf,t);
vec3 rtfi_rtf = mix(rtfi,rtf,t);
vec3 rtbi_rtb = mix(rtbi,rtb,t);

float scene(vec3 p)
{
    #ifdef CROSSEYE
    dots = length(p-vec3(0,1.1,0))-0.05;
	p *= rotX(iGlobalTime)*rotZ(0.785*iGlobalTime);
    #else
    p *= rotX(0.785);
    dots = 1e10;
    #endif
    
    lines = 1e10;

	// outside
	lines = min(lines,line(p,lbf_lbfi,rbf_lbf));
	lines = min(lines,line(p,lbb_lbbi,rbb_lbb));
	lines = min(lines,line(p,ltf_ltfi,rtf_ltf));
	lines = min(lines,line(p,ltb_ltbi,rtb_ltb));

	lines = min(lines,line(p,lbf_lbfi,lbb_lbbi));
	lines = min(lines,line(p,ltf_ltfi,ltb_ltbi));
	lines = min(lines,line(p,lbf_lbfi,ltf_ltfi));
	lines = min(lines,line(p,lbb_lbbi,ltb_ltbi));

	lines = min(lines,line(p,rbf_lbf,rbb_lbb));
	lines = min(lines,line(p,rtf_ltf,rtb_ltb));
	lines = min(lines,line(p,rbf_lbf,rtf_ltf));
	lines = min(lines,line(p,rbb_lbb,rtb_ltb));

    // inside
	lines = min(lines,line(p,lbfi_rbfi,lbbi_rbbi));
	lines = min(lines,line(p,ltfi_rtfi,ltbi_rtbi));
	lines = min(lines,line(p,lbfi_rbfi,ltfi_rtfi));
	lines = min(lines,line(p,lbbi_rbbi,ltbi_rtbi));

	lines = min(lines,line(p,lbbi_rbbi,rbbi_rbb));
	lines = min(lines,line(p,lbfi_rbfi,rbfi_rbf));
	lines = min(lines,line(p,ltfi_rtfi,rtfi_rtf));
	lines = min(lines,line(p,ltbi_rtbi,rtbi_rtb));

	lines = min(lines,line(p,rbfi_rbf,rtfi_rtf));
	lines = min(lines,line(p,rbbi_rbb,rtbi_rtb));
	lines = min(lines,line(p,rbfi_rbf,rbbi_rbb));
	lines = min(lines,line(p,rtfi_rtf,rtbi_rtb));

    // connections
	lines = min(lines,line(p,rtbi_rtb,rtb_ltb));
	lines = min(lines,line(p,rbfi_rbf,rbf_lbf));
	lines = min(lines,line(p,rbbi_rbb,rbb_lbb));
	lines = min(lines,line(p,rtfi_rtf,rtf_ltf));
	
	lines = min(lines,line(p,ltfi_rtfi,ltf_ltfi));
	lines = min(lines,line(p,ltbi_rtbi,ltb_ltbi));
	lines = min(lines,line(p,lbfi_rbfi,lbf_lbfi));
	lines = min(lines,line(p,lbbi_rbbi,lbb_lbbi));
	
	return min(lines, dots);
}

vec3 processColor(vec3 p)
{	
	vec3 n = getNormal(p);
	vec3 l = normalize(eye-p);
	
	float d = 1e10;
	float diff = max(dot(n,l),0.0);
	float spec = pow(diff,1.0);
    
    if (dots < d && dots < lines) { return vec3(0.0); }

	return vec3(100,0,10)*diff*max(0.5-spec,0.01);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{	
	uv = (2.0 * fragCoord.xy - iResolution.xy) / iResolution.xx;
    
	#ifdef CROSSEYE
	vec3 p = march(eye,lookAt(eye,vec3(uv.x < 0.0 ? 1.2: -1.2,0,0)));
    #else
    eye *= rotX(iGlobalTime)*rotZ(iGlobalTime);
    vec3 p = march(eye,lookAt(eye,vec3(0),fragCoord));
    #endif
    
	vec3 col = processColor(p);
	
	if (hit == false) { col = vec3(1.0-length(uv)*vec3(1.2,1.2,1)*0.5); }

	fragColor = vec4(col,1.0);
}