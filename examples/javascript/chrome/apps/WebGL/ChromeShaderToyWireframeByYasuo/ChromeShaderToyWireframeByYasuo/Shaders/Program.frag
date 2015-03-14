
void circle(vec2 p, vec2 offset, float size, vec3 color, inout vec3 i){
    float l = length(p - offset);
    if(l < size){
        i = color;
    }
}

void rect(vec2 p, vec2 offset, float size, vec3 color, inout vec3 i){
    vec2 q = (p - offset) / size;
    if(abs(q.x) < 1.0 && abs(q.y) < 1.0){
        i = color;
    }
}

float WEIGHT = 3.0 / iResolution.x;
float line(vec2 p, vec2 p0, vec2 p1, float w) {
    vec2 d = p1 - p0;
    float t = clamp(dot(d,p-p0) / dot(d,d), 0.0,1.0);
    vec2 proj = p0 + d * t/1.0+(sin(iGlobalTime*0.5)*0.002);
    float dist = length(p - proj);
    dist = 1.0/dist*WEIGHT*(w+(sin(iGlobalTime*0.5)*0.1));
    return min(dist*dist,1.0);
}

void hud(vec2 p, vec2 offset, float size, vec3 color, inout vec3 i, float sp){
    vec2 q = (p - offset) / size;
    if(abs(q.x) < 1.0+abs(sin(iGlobalTime*sp)*19.0) && abs(q.y) < 0.9){
        i = color;
    }
}

mat4 matRotateX(float rad)
{
	return mat4(1,       0,        0,0,
				0,cos(rad),-sin(rad),0,
				0,sin(rad), cos(rad),0,
				0,       0,        0,1);
}

mat4 matRotateY(float rad)
{
	return mat4( cos(rad),0,-sin(rad),0,
				 0,       1,        0,0,
				 sin(rad),0, cos(rad),0,
				 0,       0,        0,1);
}

mat4 matRotateZ(float rad)
{
	return mat4(cos(rad),-sin(rad),0,0,
				sin(rad), cos(rad),0,0,
				       0,        0,1,0,
					   0,        0,0,1);
}

mat4 translate(vec3 t)
{
	return mat4(1,0,0,t.x,
				0,1,0,t.y,
				0,0,1,t.z,
				0,0,0,1);
}

mat4 scale(vec3 s)
{
	return mat4(s.x,  0,  0,0,
				  0,s.y,  0,0,
				  0,  0,s.z,0,
				  0,  0,  0,1);
}

mat4 perspectiveLH(float vw, float vh, float z_near, float z_far)
{
	return mat4(2.0*z_near/vw,         	   0,                           0,0,
				            0, 2.0*z_near/vh,                           0,0,
				            0,             0, 	     z_far/(z_far-z_near),1,
				            0,             0, z_near*z_far/(z_near-z_far),0);
}

mat4 lookAtLH(vec3 aUp, vec3 aFrom, vec3 aAt)
{
	vec3 aX = vec3(0.0);
	vec3 aY = vec3(0.0);

	vec3 aZ = vec3(aAt.x,aAt.y,aAt.z);
	aZ = normalize(aZ-aFrom);

	aX = normalize(cross(aUp,aZ));
	aY = cross(aZ,aX);

	return mat4(          aX.x,           aY.x,           aZ.x,0,
				          aX.y,           aY.y,           aZ.y,0,
				          aX.z,           aY.z,           aZ.z,0,
				-dot(aFrom,aX), -dot(aFrom,aY), -dot(aFrom,aZ),1);
}

vec3 cube(vec2 world, mat4 proj, mat4 view, float time, vec3 p, vec3 s, float rotX, float rotY, float rotZ, vec3 cl)
{
	vec3 col = vec3(0.0);
	vec4 pos = vec4(p,1.0);

	vec4 v1 = proj*view*((vec4(-1.0,-1.0,1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v2 = proj*view*((vec4(1.0,-1.0,1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v3 = proj*view*((vec4(1.0,1.0,1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v4 = proj*view*((vec4(-1.0,1.0,1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v5 = proj*view*((vec4(-1.0,-1.0,-1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v6 = proj*view*((vec4(1.0,-1.0,-1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v7 = proj*view*((vec4(1.0,1.0,-1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v8 = proj*view*((vec4(-1.0,1.0,-1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));

	col += cl*line(world,vec2(v1.x,v1.y),vec2(v2.x,v2.y),0.5);
	col += cl*line(world,vec2(v2.x,v2.y),vec2(v3.x,v3.y),0.5);
	col += cl*line(world,vec2(v3.x,v3.y),vec2(v4.x,v4.y),0.5);
	col += cl*line(world,vec2(v4.x,v4.y),vec2(v1.x,v1.y),0.5);
	col += cl*line(world,vec2(v5.x,v5.y),vec2(v6.x,v6.y),0.5);
	col += cl*line(world,vec2(v6.x,v6.y),vec2(v7.x,v7.y),0.5);
	col += cl*line(world,vec2(v7.x,v7.y),vec2(v8.x,v8.y),0.5);
	col += cl*line(world,vec2(v8.x,v8.y),vec2(v5.x,v5.y),0.5);

	col += cl*line(world,vec2(v8.x,v8.y),vec2(v4.x,v4.y),0.5);
	col += cl*line(world,vec2(v7.x,v7.y),vec2(v3.x,v3.y),0.5);
	col += cl*line(world,vec2(v5.x,v5.y),vec2(v1.x,v1.y),0.5);
	col += cl*line(world,vec2(v6.x,v6.y),vec2(v2.x,v2.y),0.5);

	return col;
}

vec3 airplane(vec2 world, mat4 proj, mat4 view, float time, vec3 p, vec3 s, float rotX, float rotY, float rotZ, vec3 cl)
{
	vec3 col = vec3(0.0);
	vec4 pos = vec4(p,1.0);

	vec4 v1 = proj*view*((vec4(-0.215379,-0.018131,0.254635,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v2 = proj*view*((vec4(-0.130827,-0.030620,-0.229209,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v3 = proj*view*((vec4(-0.605928,0.226597,0.656825,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v4 = proj*view*((vec4(-0.457512,-0.151435,0.173292,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v5 = proj*view*((vec4(-0.301224,-0.276743,0.205711,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v6 = proj*view*((vec4(-0.762492,-0.313925,0.985007,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v7 = proj*view*((vec4(0.219197,-0.018172,0.253044,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v8 = proj*view*((vec4(0.605928,0.226597,0.656825,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v9 = proj*view*((vec4(0.134193,-0.030673,-0.229209,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v10 = proj*view*((vec4(0.457512,-0.151435,0.173292,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v11 = proj*view*((vec4(0.001113,0.122996,0.372016,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v12 = proj*view*((vec4(0.012398,0.313925,0.213513,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v13 = proj*view*((vec4(0.001113,-0.050108,-0.985007,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v14 = proj*view*((vec4(0.301224,-0.276743,0.205711,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v15 = proj*view*((vec4(0.762492,-0.313925,0.985007,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));

	col += cl*line(world,vec2(v2.x,v2.y),vec2(v3.x,v3.y),0.5);
	col += cl*line(world,vec2(v2.x,v2.y),vec2(v12.x,v12.y),0.5);

	col += cl*line(world,vec2(v3.x,v3.y),vec2(v4.x,v4.y),0.5);
	col += cl*line(world,vec2(v4.x,v4.y),vec2(v5.x,v5.y),0.5);
	col += cl*line(world,vec2(v4.x,v4.y),vec2(v6.x,v6.y),0.5);
	col += cl*line(world,vec2(v6.x,v6.y),vec2(v5.x,v5.y),0.5);
	col += cl*line(world,vec2(v5.x,v5.y),vec2(v13.x,v13.y),0.5);

	col += cl*line(world,vec2(v8.x,v8.y),vec2(v9.x,v9.y),0.5);
	col += cl*line(world,vec2(v8.x,v8.y),vec2(v10.x,v10.y),0.5);
	col += cl*line(world,vec2(v10.x,v10.y),vec2(v15.x,v15.y),0.5);
	col += cl*line(world,vec2(v10.x,v10.y),vec2(v14.x,v14.y),0.5);

	col += cl*line(world,vec2(v2.x,v2.y),vec2(v13.x,v13.y),0.5);
	col += cl*line(world,vec2(v4.x,v4.y),vec2(v13.x,v13.y),0.5);			
	col += cl*line(world,vec2(v9.x,v9.y),vec2(v13.x,v13.y),0.5);
	col += cl*line(world,vec2(v10.x,v10.y),vec2(v13.x,v13.y),0.5);

	col += cl*line(world,vec2(v3.x,v3.y),vec2(v12.x,v12.y),0.5);				
	col += cl*line(world,vec2(v8.x,v8.y),vec2(v12.x,v12.y),0.5);

	col += cl*line(world,vec2(v12.x,v12.y),vec2(v11.x,v11.y),0.5);

	col += cl*line(world,vec2(v11.x,v11.y),vec2(v3.x,v3.y),0.5);
	col += cl*line(world,vec2(v11.x,v11.y),vec2(v8.x,v8.y),0.5);

	col += cl*line(world,vec2(v11.x,v11.y),vec2(v7.x,v7.y),0.5);
	col += cl*line(world,vec2(v11.x,v11.y),vec2(v1.x,v1.y),0.5);

	col += cl*line(world,vec2(v9.x,v9.y),vec2(v12.x,v12.y),0.5);

	col += cl*line(world,vec2(v8.x,v8.y),vec2(v7.x,v7.y),0.5);
	col += cl*line(world,vec2(v3.x,v3.y),vec2(v1.x,v1.y),0.5);

	col += cl*line(world,vec2(v1.x,v1.y),vec2(v6.x,v6.y),0.5);
	col += cl*line(world,vec2(v7.x,v7.y),vec2(v15.x,v15.y),0.5);

	col += cl*line(world,vec2(v12.x,v12.y),vec2(v13.x,v13.y),0.5);
	col += cl*line(world,vec2(v13.x,v13.y),vec2(v14.x,v14.y),0.5);
	col += cl*line(world,vec2(v14.x,v14.y),vec2(v15.x,v15.y),0.5);

	return col;
}

vec3 mountain(vec2 world, mat4 proj, mat4 view, float time, vec3 p, vec3 s, float rotX, float rotY, float rotZ, vec3 cl)
{
	vec3 col = vec3(0.0);
	float pi = 3.14159265358979;

	vec4 pos = vec4(p,1.0);
	vec4 v1 = proj*view*((vec4(1.0,0.0,0.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v2 = proj*view*((vec4(-1.0,0.0,0.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v3 = proj*view*((vec4(0.0,0.0,-2.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));
	vec4 v4 = proj*view*((vec4(0.0,2.0,-1.0,1.0)-pos)*matRotateX(rotX)*matRotateY(rotY)*matRotateZ(rotZ)*scale(s));

	col += cl*line(world,vec2(v1.x,v1.y),vec2(v2.x,v2.y),0.5);
	col += cl*line(world,vec2(v2.x,v2.y),vec2(v3.x,v3.y),0.5);
	col += cl*line(world,vec2(v3.x,v3.y),vec2(v1.x,v1.y),0.5);
	col += cl*line(world,vec2(v1.x,v1.y),vec2(v4.x,v4.y),0.5);
	col += cl*line(world,vec2(v2.x,v2.y),vec2(v4.x,v4.y),0.5);
	col += cl*line(world,vec2(v3.x,v3.y),vec2(v4.x,v4.y),0.5);
	return col;
}

vec3 _vUP = vec3(0, 1, 0);
vec3 _vAT = vec3(0, -2, 0);
vec3 _mEye = vec3(1, 0, 15);
vec3 _mEyeA = vec3(0, 0, 20);

float pi = atan(1.)*4.;
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	// set up world position
	vec2 worldCenter = (fragCoord.xy * 2.0 - iResolution.xy) / min(iResolution.x, iResolution.y);
	float cLength = length(worldCenter);

	// add post effect
	worldCenter += (worldCenter/cLength)*cos(cLength*12.0-iGlobalTime*4.0)*0.03; // ripple

	vec3 col = vec3(0.0);

	_mEyeA.xz = vec2(-cos(iGlobalTime*0.5)*200.,-sin(iGlobalTime*0.5)*200.);
	_mEyeA.y = 45.0;

	_mEye = _mEye*0.7;
	_mEye.x += _mEyeA.x * 0.5;
	_mEye.y += _mEyeA.y * 0.5;
	_mEye.z += _mEyeA.z * 0.5;

	// pipeline
	mat4 proj = perspectiveLH(1.0, 1.0, 0.12-(_mEyeA.x*0.0003), 1000.0);
	mat4 view = lookAtLH(_vUP, _mEye, _vAT);

	float loopZ = mod(iGlobalTime*3.0, 30.0);
	col += airplane(worldCenter,proj,view,iGlobalTime,vec3(0.0,0.0,loopZ-15.0),vec3(1.8,1.8,1.8),0.0,0.0,sin(iGlobalTime*0.5),vec3(1.0,1.0,1.0));

	col += cube(worldCenter,proj,view,iGlobalTime,vec3(6.0,0.0,0.0),vec3(1.2,1.2,1.2),iGlobalTime*.5-(pi/2.),0.0,0.0,vec3(1.0,1.0,1.0));
	col += cube(worldCenter,proj,view,iGlobalTime,vec3(-4.0,0.0,6.0),vec3(1.2,2.2,1.2),0.0,0.0,0.0,vec3(1.0,1.0,1.0));

	col += mountain(worldCenter,proj,view,iGlobalTime,vec3(4.0,0.0,9.0),vec3(2.2,1.2,1.2),0.0,0.0,0.0,vec3(1.0,1.0,1.0));
	col += mountain(worldCenter,proj,view,iGlobalTime,vec3(-4.0,0.0,-6.5),vec3(2.2,2.1,1.2),0.0,0.0,0.0,vec3(1.0,1.0,1.0));

	// floor
	vec3 gridScale = vec3(3.5,3.5,3.5);
	for(float i = -2.; i <= 2.; i ++)
	{
		vec4 t1 = proj*view*(vec4(-256.0*0.02,-64.0*0.02,i*128.0*0.02,1.0)*scale(gridScale));
		vec4 t2 = proj*view*(vec4(256.0*0.02,-64.0*0.02,i*128.0*0.02,1.0)*scale(gridScale));
		vec4 t3 = proj*view*(vec4(i*128.0*0.02,-64.0*0.02,-256.0*0.02,1.0)*scale(gridScale));
		vec4 t4 = proj*view*(vec4(i*128.0*0.02,-64.0*0.02,256.0*0.02,1.0)*scale(gridScale));

		col += vec3(1.0,1.0,1.0)*line(worldCenter,vec2(t1.x,t1.y),vec2(t2.x,t2.y),0.5);
		col += vec3(1.0,1.0,1.0)*line(worldCenter,vec2(t3.x,t3.y),vec2(t4.x,t4.y),0.5);
	}

	// if you don't want posteffect.
	//worldCenter = (gl_FragCoord.xy * 2.0 - iResolution) / min(iResolution.x, iResolution.y);

	hud(worldCenter,vec2(-1.9+abs(sin(iGlobalTime*1.0)*0.57),0.85),0.03,vec3(1.0,1.0,1.0),col,1.0);
	hud(worldCenter,vec2(-1.9+abs(sin(iGlobalTime*1.1)*0.57),0.75),0.03,vec3(1.0,1.0,1.0),col,1.1);
	hud(worldCenter,vec2(-1.9+abs(sin(iGlobalTime*1.2)*0.57),0.65),0.03,vec3(1.0,1.0,1.0),col,1.2);


	fragColor = vec4(vec3(col), 1.0);
}