//float closest;
struct point{
	vec3 position;
	vec3 normal;
	vec3 color;
	float distanceFromCamera;
};
struct polygon{
	vec3 A;
	vec3 B;
	vec3 C;	
	vec3 color;
};
struct sphere{
	vec3 pos;
	float size;
};
vec3 color;
vec3 camera;
vec3 ray;
point closestPoint;
void resolveRayPolygonIntersection(polygon poly){
	
	vec3 e1=poly.B-poly.A;
	vec3 e2=poly.C-poly.A;
	vec3 pvec=cross(ray, e2);
	float det=dot(e1, pvec);
	
	float invDet=1.0/det;
	vec3 tvec=camera-poly.A;
	float u=dot(tvec,pvec)*invDet;
	if(u<0.0||u>1.0) return;
	vec3 qvec=cross(tvec,e1);
	float v=dot(ray,qvec)*invDet;
	if(v<0.0||v>1.0||(u+v)>1.0) return;
	float t=dot(e2,qvec)*invDet;
	if(t>0.0)
	if(t<closestPoint.distanceFromCamera){
		closestPoint.distanceFromCamera=t;
		closestPoint.normal=normalize(cross(e1, e2));
		closestPoint.color=poly.color;
	}
}
bool resolveRaySphereIntersection(sphere ball){
	vec3 OC=ball.pos-camera;
	float P=dot(OC,ray);
	if(P<0.) return false;
	float d=sqrt(pow(length(OC),2.0)-pow(P,2.0));
	if(d>ball.size) return false;
	return true;
}
float resolveRayLightIntersection(sphere sun){
	vec3 OC=sun.pos-camera;
	float P=dot(OC,ray);
	float d=sqrt(pow(length(OC),2.0)-pow(P,2.0));
	return 1./d;
	
}
mat3 rotate_x( float angle)
{
	return mat3(		1	,		0,			0	,
						0	,cos(angle),-sin(angle) ,
						0	,sin(angle),cos(angle)	); 
}
mat3 rotate_y( float angle)
{
	return mat3(cos(angle)	,		0,	sin(angle)	,
						0	,		1,			0	,
				-sin(angle)	,		0,	cos(angle)	); 
}
sphere sun;
void resolveRay(float howManiethReflection){
	if(closestPoint.distanceFromCamera<100000.){
		camera+=ray*(closestPoint.distanceFromCamera)-normalize(closestPoint.normal);
		ray=normalize(reflect(ray,closestPoint.normal));
		float sunlight=length(sun.pos-camera)/min(max(0.4/resolveRayLightIntersection(sun),0.01),1.0);
		color+=vec3(closestPoint.color/(0.11*sunlight*(howManiethReflection*1.1+1.)));
		//ray=normalize(ray)*vec3(0.77);

	}
}
float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}
void scene(){
	sphere ball;
	ball.pos=vec3(0.,-3.,10.);
	ball.size=17.;
	if(resolveRaySphereIntersection(ball))
	for(float reflection=0.; reflection<=6.; reflection++){
		
	polygon test2;
	test2.A=vec3(9., -10., 10.);
	test2.B=vec3(4., -10., -10.);
	test2.C=vec3(9., 10., 10.);
	test2.color=vec3(.61,.8,.51);
	resolveRayPolygonIntersection(test2);
	polygon test3;
	test3.C=vec3(-12., -10., 10.);
	test3.B=vec3(-7., -10., -10.);
	test3.A=vec3(-1., 30., 0.);
	test3.color=vec3(.8,.5,.5);
	resolveRayPolygonIntersection(test3);
		
	for(float i=3.14*2000.0; i<3.14*4000.0; i+=3.14*400.0){
			polygon ympyraPala;
			ympyraPala.A=vec3(4.0*cos(i), -1.0, 4.0*sin(i));
			ympyraPala.B=vec3(4.0*cos(i+3.14*400.), -1.0, 4.0*sin(i+3.14*400.));
			ympyraPala.C=vec3(0.0, 4.+1.*sin(iGlobalTime*3.782562261), 0.0);
			ympyraPala.color=vec3(.31,.0,.0);
			resolveRayPolygonIntersection(ympyraPala);
	}
	for(float i=3.14*2000.0; i<3.14*4000.0; i+=3.14*400.0){
			polygon ympyraPala;
			ympyraPala.A=vec3(5.0*cos(i+3.14*400.), -3.0+1.*sin(iGlobalTime*3.782562261-1.), 5.0*sin(i+3.14*400.));
			ympyraPala.B=vec3(4.0*cos(i+3.14*400.), -1.0, 4.0*sin(i+3.14*400.));
			ympyraPala.C=vec3(4.0*cos(i), -1.0, 4.0*sin(i));
			ympyraPala.color=vec3(.31,.0,.0);
			resolveRayPolygonIntersection(ympyraPala);
			ympyraPala.C=vec3(5.0*cos(i+3.14*400.), -3.0+1.*sin(iGlobalTime*3.782562261-1.), 5.0*sin(i+3.14*400.));
			ympyraPala.B=vec3(5.0*cos(i), -3.0+1.*sin(iGlobalTime*3.782562261-1.0), 5.0*sin(i));
			ympyraPala.A=vec3(4.0*cos(i), -1.0, 4.0*sin(i));
			ympyraPala.color=vec3(.31,.0,.0);
			resolveRayPolygonIntersection(ympyraPala);
	}
	polygon RR;
	RR.A=vec3(-10., -6., -10.);
	RR.B=vec3(10., -6., -10.);
	RR.C=vec3(-10., -6., 10.);
	RR.color=vec3(.104,.1,.31);
	resolveRayPolygonIntersection(RR);
	RR.A=vec3(10., -6., -10.);
	RR.B=vec3(10., -6., 10.);
	RR.C=vec3(-10., -6., 10.);
	RR.color=vec3(.104,.1,.31);
	resolveRayPolygonIntersection(RR);
		
	for(float j=-1.0; j<4.0; j++)
		for(float i=-2.0; i<2.0; i++){
			polygon tasoPalaA;
			tasoPalaA.C=vec3(4.0*i, -4.0+sin((i+j+iGlobalTime)*.71)*2., 4.0*j);
			tasoPalaA.B=vec3(4.0*i+4.0, -4.0+sin((i+j+iGlobalTime)*.71)*2., 4.0*j);
			tasoPalaA.A=vec3(4.0*i, -4.0+sin((i+j+iGlobalTime)*.71)*2., -4.0+4.0*j);
			tasoPalaA.color=vec3(.41,.41,.41);
			resolveRayPolygonIntersection(tasoPalaA);
			polygon tasoPalaB;
			tasoPalaB.C=vec3(4.0*i+4.0, -4.0+sin((i+j+iGlobalTime)*.71)*2., 4.0*j);
			tasoPalaB.B=vec3(4.0*i+4.0, -4.0+sin((i+j+iGlobalTime)*.71)*2., -4.0+4.0*j);
			tasoPalaB.A=vec3(4.0*i, -4.0+sin((i+j+iGlobalTime)*.71)*2., -4.0+4.0*j);
			tasoPalaB.color=vec3(.41,.41,.41);
			resolveRayPolygonIntersection(tasoPalaB);
		}
		resolveRay(reflection);
		color+=vec3(min(resolveRayLightIntersection(sun),0.0));
	closestPoint.distanceFromCamera=100000.;
	}
}
void mainImage( out vec4 fragColor, in vec2 fragCoord ){
	vec2 uv = fragCoord.xy / iResolution.xy;
	//float aspectratio = iResolution.x / iResolution.y;
	vec3 noise=texture2D(iChannel0, uv+vec2(iGlobalTime)).xyz*rand(uv+vec2(iGlobalTime))*vec3(0.052);
	
	
	float aspectCorrection = (iResolution.x/iResolution.y);
	vec2 coordinate_entered = 2.0 * uv - 1.0;
	vec2 coord = vec2(aspectCorrection,1.0) *coordinate_entered;
	
	camera = vec3(-4.0*sin(iGlobalTime),3.0,22.0);
	ray = normalize(vec3(coord, -1.5));
	ray*=rotate_y((sin(iGlobalTime)*2.)*3.14/180.);
	sun.pos=vec3(0.,0.0,-5.*sin(iGlobalTime*0.166421));
	//camera=vec3(-4.0*sin(iGlobalTime),3.0,15.0);
	//ray=normalize(vec3((-1.+2.*uv)*vec2(aspectratio,1.),-1.));
	closestPoint.distanceFromCamera=100000.;
	color=vec3(0.);
	float angle=3.14*(sin(iGlobalTime*0.08631)*10.0)/180.0;
	ray*=rotate_y(angle);
	scene();

	fragColor = vec4(color+noise+vec3(-0.5),1.0);
}