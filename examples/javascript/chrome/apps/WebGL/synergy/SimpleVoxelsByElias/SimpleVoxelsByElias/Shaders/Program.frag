#define DPRECISION 0.01
#define VPRECISION 0.1
#define DEPTH 10.0
#define STEPS int(50.0*(1.0/VPRECISION))

// voxel size
float vSize = 0.1;

vec3 eye = vec3(0,1,2);
vec3 light = vec3(0,1,0);

vec2 uv; float map(vec3);
bool hit = false;

float t = iGlobalTime;
float sphere;
float box;
float line;

// Rotation
mat3 rotZ(float a){float s=sin(a);float c=cos(a);return mat3(c,-s,0,s,c,0,0,0,1);}
mat3 rotX(float a){float s=sin(a);float c=cos(a);return mat3(1,0,0,0,c,s,0,-s,c);}
mat3 rotY(float a){float s=sin(a);float c=cos(a);return mat3(c,0,-s,0,1,0,s,0,c);}

// Ditsance functions
float sdBox(vec3 p,vec3 b){vec3 d=abs(p)-b;return min(max(d.x,max(d.y,d.z)),0.0)+length(max(d,0.0));}
float sdSphere(vec3 p,float r){return length(p)-r;}
float sdTorus(vec3 p,vec2 t){vec2 q=vec2(length(p.xz)-t.x,p.y);return length(q)-t.y;}

// Marching
vec3 getNormal(vec3 p){vec2 e=vec2(DPRECISION,0);return(normalize(vec3(map(p+e.xyy)-map(p-e.xyy),map(p+e.yxy)-map(p-e.yxy),map(p+e.yyx)-map(p-e.yyx))));}
vec3 lookAt(vec3 o,vec3 t){vec3 d=normalize(t-o),u=vec3(0,1,0),r=cross(u,d);return(normalize(r*uv.x+cross(d,r)*uv.y+d));}
vec3 voxalize(vec3 p){return floor((p+vSize/2.0)/vSize)*vSize;}
vec3 repeat(vec3 p,float s){return mod(p,s)-s/2.0;}

float map(vec3 p)
{
	
    if (uv.x>line)p = voxalize(p);
	
	box = max(-sdSphere(p,0.7),sdBox(p,vec3(0.5)));
	sphere = sdSphere(p,0.4);
	
	return min(sphere,box);
}

vec3 getColor(vec3 p)
{	
	vec3 n = getNormal(p);
	vec3 l = normalize(light-p);
	vec3 col = vec3(0);

	float d = 1e10;
	float dist = min(1.0,0.5/length(light-p));
	float diff = max(dot(n,l),0.0);
	float spec = pow(diff,10.0);
	
	if (box<d) { col = vec3(0,2,3); d = box; }
	if (sphere<d) { col = vec3(3,2,0); d = sphere; }
	
	col = dist*(col+diff+spec);
	col = col*(1.0/exp(length(p)));
	
	return col;
}

vec3 march(vec3 ro, vec3 rd)
{
	float t = 0.0, d;
	
	for(int i=0;i<STEPS;i++)
	{
		d = map(ro+rd*t);
		
		if (d<DPRECISION){hit=true;}
		if (hit==true||t>DEPTH){break;}
		
		t += d*VPRECISION;
	}
	
	return ro+rd*t;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	eye *= rotY(t);
	uv = (2.0*fragCoord.xy-iResolution.xy)/iResolution.xx;
    
    line = (iMouse.x/iResolution.x-0.5)*2.0;
    if (line == -1.0) { line = 0.0; }
	
	vec3 p = march(eye,lookAt(eye,vec3(0)));
	vec3 col = 1.0-vec3(length(uv))*0.5;
	
	if (hit == true) { col = getColor(p); }
    if (abs(uv.x-line)<0.005) { col = vec3(0); }
    
	fragColor = vec4(col,1.0);
}