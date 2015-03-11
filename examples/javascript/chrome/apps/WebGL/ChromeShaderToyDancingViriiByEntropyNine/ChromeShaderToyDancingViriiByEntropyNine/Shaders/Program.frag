//#define DOF_EFFECT
//uncomment to enable DOF (actually works faster!)

mat3 rot(vec3 v, float angle)
{
	float c = cos(angle);
	float s = sin(angle);
	
	return mat3(c + (1.0 - c) * v.x * v.x, (1.0 - c) * v.x * v.y - s * v.z, (1.0 - c) * v.x * v.z + s * v.y,
		(1.0 - c) * v.x * v.y + s * v.z, c + (1.0 - c) * v.y * v.y, (1.0 - c) * v.y * v.z - s * v.x,
		(1.0 - c) * v.x * v.z - s * v.y, (1.0 - c) * v.y * v.z + s * v.x, c + (1.0 - c) * v.z * v.z
		);
}

vec2 f=vec2(0.5,2.);

float surfkifs(vec3 p,float sca) {
	float time = iGlobalTime*1.2;
	vec2 c=vec2(1.,1.);
	const int iter=24;
	float sc=1.16+sca*.025;
	vec3 j=vec3(-1,-1,-1.5);
	//vec3 rotv=normalize(vec3(0.2,-0.2,-1)+.2*vec3(sin(time),-sin(time),cos(time)));
	vec3 rotv=normalize(vec3(-0.08+sin(time)*.02,-0.2,-.5));
	float rota=radians(50.);
	mat3 rotm=rot(normalize(rotv),rota);
	p.z=abs(p.z)-4.;
	for (int i=0; i<iter; i++) {
		p.xy=abs(p.xy+f.xy)-f.xy;
		p*=rotm;
		p*=sc;
		p+=j;
		
	}
	return length(p)*pow(sc,float(-iter));
}

float nucleo(vec3 p, float s) {
	float d=1000.;
	for (float n=0.; n<10.; n++) {
		float t=mod((iGlobalTime+n)*2.,4000.);
		vec4 r=texture2D(iChannel1,vec2(floor(t/64.),mod(t,64.))/64.);
		d=min(d,length(p+(r.xyz-vec3(.5))*s*.7)-s*(.2+r.w*.3));
	}
	return d;
}


float sampleMusic()
{
	return (
		texture2D( iChannel0, vec2( 0.01, 0.15 ) ).x + 
		texture2D( iChannel0, vec2( 0.07, 0.15 ) ).x + 
		texture2D( iChannel0, vec2( 0.15, 0.15 ) ).x + 
		texture2D( iChannel0, vec2( 0.30, 0.15 ) ).x);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	float sca=.5+sampleMusic();
	float time = iGlobalTime*.9;
	vec2 coord = fragCoord.xy / iResolution.xy *2. - vec2(1.);
	coord.y *= iResolution.y / iResolution.x;
	float dist=29.+sin(time*2.)*5.;
	vec3 target = vec3(-f,0);
	vec3 from = target+dist*normalize(vec3(sin(time),cos(time),.5+sin(time)));
	vec3 up=vec3(1,sin(time)*.5,1);
    vec3 edir = normalize(target - from);
    vec3 udir = normalize(up-dot(edir,up)*edir);
    vec3 rdir = normalize(cross(edir,udir));
	float fov=1.1*sin(iGlobalTime * 0.1)+sca*.09;
	vec3 dir=normalize((coord.x*rdir+coord.y*udir)*fov+edir);
	vec3 p=from;
	float steps;
	float totdist;
	float intens=1.;
	float maxdist=dist+15.;
	vec3 col=vec3(0.);
	for (int r=0; r<110; r++) {
		float d1=surfkifs(p,sca);
		float d2=nucleo(p+vec3(f,0),max(5.,sca*3.));
		float d=min(d1,d2);
		#ifdef DOF_EFFECT
			totdist+=max(max(0.5-time*0.5,0.02*pow(totdist*.06,3.)),abs(d));
		#else
			totdist+=max(max(0.5-time*0.5,0.03),abs(d));
		#endif
		if (totdist>maxdist) break;
		p=from+totdist*dir;
		steps++;
		intens=max(0.,maxdist-totdist+3.)/maxdist;
		col+=(d==d1?vec3(2.1*sin(iGlobalTime),.2*sin(iGlobalTime),1)*pow(intens,2.5):vec3(1.3*sin(iGlobalTime),0.2,.1*sin(iGlobalTime))*(.05+sca*.2)*intens);
	}
	col=col*0.035+vec3(.5)*(max(0.,length(coord)-.6));
	fragColor = vec4(col,1.0);	
	
}