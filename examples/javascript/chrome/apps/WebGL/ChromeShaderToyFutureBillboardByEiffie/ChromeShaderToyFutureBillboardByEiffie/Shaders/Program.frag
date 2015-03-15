// DE Edge Detection by eiffie (I'm sure it has been done before but here is my take on it.)
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// Sometimes it is nice to know where the sharp edges of a distance estimate are to
// reduce aliasing etc. Here the edges are all that is drawn (so lots of aliasing).
const int steps=64;
const float HitDistance=0.002, maxDepth=25.0;
const vec2 ve=vec2(0.02,0.0);

float Rect(in vec3 z, vec3 radii){return max(abs(z.x)-radii.x,max(abs(z.y)-radii.y,abs(z.z)-radii.z));}

void rotate(inout vec2 v,float a){v=cos(a)*v+sin(a)*vec2(v.y,-v.x);}

const vec3 c4=vec3(0.1,1.0,0.1),c3=vec3(0.24,0.04,2.48);
const vec2 b=vec2(3.0);
float k,time;
vec3 txc=vec3(0.0);
bool bColoring=false;

float DE(vec3 z0)
{//another discontinuous de
	vec3 z=z0,c=vec3(0.08,0.9,0.08);
	vec2 h=floor(z.xz*0.3333333);
	z.xz=abs(b-mod(z.xz,2.0*b));
	float d=min(Rect(z,c4),min(Rect(z+vec3(0.0,1.0,0.0),c3),Rect(z+vec3(0.0,1.0,0.0),c3.zyx))),a=0.25;
	k=h.x+h.y;
	rotate(z.xz,6.283185*0.3333333);
	for(int i=0;i<5;i++){
		rotate(z.yz,-0.5+sin(time*40.0+a+k)*0.333);
		d=min(d,Rect(z,c));
		a*=2.0;
		z.y-=c.y;
		c=c*0.75;
	}
	rotate(z.yz,-0.5+sin(time*40.0+a+k)*0.333);
	float d2=Rect(z,vec3(c.x*20.0,c.yz));
	d=min(d,d2);
	if(bColoring){
		if(d==d2 && z.z>0.0){
			if(h.x>h.y){
				txc=2.0*texture2D(iChannel0,z.xy*vec2(-1.4,-2.4)+0.5).rgb;
			}else{
				txc=2.0*texture2D(iChannel1,z.xy*vec2(-1.4,-2.4)+0.5).rgb;
			}
		}
	}
	return d;
}

vec3 scene(vec3 ro, vec3 rd){
	float t=0.0,d=10.0;
	for(int i=0;i<steps;i++){
		if(t>=maxDepth || d<HitDistance)continue;
		t+=d=DE(ro+t*rd);
	}
	if(d>=HitDistance)return vec3(rd.y);
	ro+=rd*t;
	bColoring=true;
	d=DE(ro);
	bColoring=false;
	float d1=DE(ro-ve.xyy),d2=DE(ro+ve.xyy);
	float d3=DE(ro-ve.yxy),d4=DE(ro+ve.yxy);
	float d5=DE(ro-ve.yyx),d6=DE(ro+ve.yyx);//these can be used for the normal as well so no extra work
	//the farther the initial estimate is from the average of the deltas the more of an edge it is
	d=abs(d-0.5*(d2+d1))+abs(d-0.5*(d4+d3))+abs(d-0.5*(d6+d5));//edge finder
	k*=sin(time);
	return (txc+abs(vec3(cos(k),sin(k),sin(time*2.3)))*d*200.0)/t;
}

mat3 lookat(vec3 fw,vec3 up){
	fw=normalize(fw);
	vec3 rt=normalize(cross(fw,normalize(up)));up=cross(rt,fw);
	return mat3(rt,up,fw);
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	time=iGlobalTime*0.1;
	float r=10.0+sin(time*2.0)*5.0;
	vec3 ro=vec3(cos(-time*2.0)*r,1.0+sin(time*3.0)*1.5,sin(-time*2.0)*r);
	vec3 rd=normalize(lookat(-ro,vec3(0.0,1.0+cos(time*5.0)*0.25,0.25*sin(time*5.0)))*vec3((-iResolution.xy+2.0*(fragCoord.xy))/iResolution.y,2.5));
	fragColor = vec4(scene(ro,rd),1.0);
}
