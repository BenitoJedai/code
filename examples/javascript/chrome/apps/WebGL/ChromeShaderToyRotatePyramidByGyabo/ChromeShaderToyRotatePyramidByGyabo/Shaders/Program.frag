#ifdef GL_ES
precision mediump float;
#endif

#define EPSM  0.9999

//iq's tools.
float hash( float n )
{
    return fract(sin(n)*43758.5453123);
}

float hexp( vec2 p, vec2 h )
{
    vec2 q = abs(p);
    return max(q.x-h.y,max(q.x+q.y*0.57735,q.y*1.1547)-h.x);
}

float plane(vec3 p, float D) {
	return D - dot(abs(p), normalize(vec3(0.0, 1.0, 0.0)));
}

vec2 rot(vec2 p, float a) {
	return vec2(
		cos(a) * p.x - sin(a) * p.y,
		sin(a) * p.x + cos(a) * p.y);
}

vec3 map(vec3 p) {
	vec3 pp = p;
	
	//ground
	float k = plane(pp, 0.7);
	
	//PYRAMID
	pp = mod(-abs(p), 1.0) - 0.5;
	
	float tim = iGlobalTime * 4.0 + sin(iGlobalTime * 3.0) * 2.0;
	for(int e = 0 ; e < 6; e++) {
		//rotate
		if( floor(float(e) / 2.0) > 0.0 ) tim = -tim;
		vec3 ppp = pp;
		ppp.xz = rot(ppp.xz, tim * 0.3 + ceil(p.z) / (1.5));
		k = min(k, max(ppp.y - 0.07 * float(e), hexp(ppp.xz, vec2(0.48 - 0.08 * float(e)))) );
	}
	return vec3(pp.xz, k);
}
	
void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec2 uv    = -1.0 + 2.0 * ( fragCoord.xy / iResolution.xy );
	
	//ray
	vec3 dir   = normalize(vec3(uv * vec2(iResolution.x / iResolution.y, 1.0), 1.0)).xyz;
	
	//rotate
	dir.xz = rot(dir.xz, iGlobalTime * 0.07);
	dir.zy = rot(dir.zy, iGlobalTime * 0.002);
	//dir.xy = rot(dir.xy, time * 0.005);
	
	//camera 
	vec3 pos   = vec3(0.0);
	pos.z     += iGlobalTime * 0.3;
	pos.x     += iGlobalTime * 0.1;
	
	//col
	vec3  col  = vec3(0.0);
	float t    = 0.01;

	//raymarch
	vec3  gm   = vec3(0.0);
	for(int i  = 0 ; i < 48; i++) {
		gm = map(pos + dir * t * EPSM);
		t += gm.z;
	}
	vec3  IP   = pos + dir * t;
	
	//fake shadow and fake ao : http://pouet.net/topic.php?which=7535&page=1
	vec3  L    = vec3(-0.5, 0.2, -0.7); //norm...
	float Sef  = 0.05;
	float S1   = max(map(IP + Sef * L).z, 0.005);
	col        = max(vec3(S1), 0.0);
	
	//sun
	vec3 sun   = mix(vec3(1, 2, 3), vec3(3, 2, 1), t * 0.5) * 0.04;
	col        = sqrt(col) + dir.zxy * 0.05+ t * 0.07 + sun;
	fragColor = vec4(col, 1.0);

}