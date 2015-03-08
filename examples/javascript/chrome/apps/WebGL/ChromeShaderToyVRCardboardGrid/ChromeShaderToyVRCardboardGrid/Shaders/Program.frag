const float i_MAXD = 100.;
const int i_STEPS = 50;

float t = iGlobalTime;
vec3 eps = vec3(.02, 0., 0.);


float smin( float a, float b ) {
	float k = 1.;
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}

float sdSphere( vec3 p, float s ) {
  return length(p)-s;
}

float sdGrid(vec3 p, vec3 q) {		
	float w = 0.1;	
  	return smin(
			smin(
				length(p.xy)-w,
				length(p.xz)-w
				),
			length(p.yz)-w
		);
}

vec2 field(in vec3 q) {		
	float grid = 5. + .1*length(q);
		
	float c = .2*cos(.05*q.y);
    float s = .2*sin(.05*q.z);
	mat2 m = mat2(c,-s,s,c);
    vec3 p = mix(vec3(m*q.xy,q.z), q, .4 + .23*sin(.11*t));
	vec3 w = .2*p;
	
	vec3 pp = 4.*vec3(sin(1.+.94*t), cos(1.12*t), 1.5+sin(2.+0.73*t)*cos(.81*t));
	float sp = sdSphere(p + pp, 1.3);
	
	vec3 g = mod(p, grid) - .5*grid;	
	return vec2(smin(sdGrid(g, q), sp), 0.);
}

vec3 normal(vec3 p) {
	return normalize(vec3(
		field(p+eps.xyz).x - field(p-eps.xyz).x,
		field(p+eps.yxz).x - field(p-eps.yzx).x,
		field(p+eps.yzx).x - field(p-eps.yzx).x
		));
}

int istep = 0;

vec4 intersect(in vec3 ro, in vec3 rd) {	
    	float k = 0.;
    	vec2 r = vec2(0.1);
    	int j = 0;
    	for(int i=0; i<i_STEPS; i++ ) {
			if (istep > i_STEPS) continue;
        	if(abs(r.x) < eps.x || k>i_MAXD) continue;        				
	    	r = field(ro+rd*k);
	    	k += r.x;
			j += 1;
			istep += 1;
    	}

    	if(k>i_MAXD) r.y=-1.0;
    	return vec4( k, j, r.yx );
}

vec3 FresnelSchlick(vec3 SpecularColor,vec3 E,vec3 H) {
    return SpecularColor + (1.0 - SpecularColor) * pow(1.0 - clamp(dot(E, H), 0., 1.), 5.);
}


vec3 shade(vec4 h, vec3 p, vec3 rd, vec3 n) {
	vec3 color = vec3(0.);
	if (h.z >= 0.) {	
		vec3 L = (-rd);
		float D = clamp(dot(L, n), 0., 1.);
		vec3 H = normalize(L - rd);
		vec3 R = reflect(rd, n);
			
		vec3 tex = textureCube(iChannel0, R).xyz;
		vec3 dcolor = 0.1+0.3*tex;
		vec3 scolor = vec3(0.4, 0.3, 0.2 );
		
		// L = light
		// N = normal
		// R = reflected ray
		// V = viewer, -1* ray dir
		// H = halfway vector H = .5*(L + V)
		
		float spec = 64.;		
			
		color = dcolor*1.*(D + .01);
		color += FresnelSchlick(scolor, L, H) * ((spec + 2.) / 8. ) * pow(clamp(dot(n, H), 0., 1.), spec) * D;		
		
		color *= smoothstep(0., 1., 6./h.x);	
	}		
	
	return color;
}

// Stolen somewhere
vec2 HmdWarp(vec2 uv) {
	// screen space transform(Side by Side)
	uv = vec2((mod(uv.x,1.0)-0.5)*2.0+0.2*sign(uv.x), uv.y);

	// HMD Parameters
	vec2 ScaleIn = vec2(1.0);
	vec2 LensCenter = vec2(0.0,0.0);
	vec4 HmdWarpParam = vec4(1.0,0.22, 0.240, 0.00);
	vec2 Scale = vec2(1.1);

	// Distortion
	vec2 theta  = (uv - LensCenter) * ScaleIn; // Scales to [-1, 1]
	float  rSq    = theta.x * theta.x + theta.y * theta.y;
	vec2 rvector= theta * (HmdWarpParam.x + HmdWarpParam.y * rSq
			       + HmdWarpParam.z * rSq * rSq
			       + HmdWarpParam.w * rSq * rSq * rSq);
	return LensCenter + Scale * rvector;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;	
	vec3 mouse = vec3(2.*iMouse.xy / iResolution.xy - 1., 0.);
	mouse.y *= -1.;
	
	vec2 xy = HmdWarp(2.*uv - 1.);
	
	vec3 ct = vec3(.5*cos(t), .5*sin(t), 0.) + 5.*mouse;
	vec3 cp = vec3(0., 0., -13.);
	vec3 cd = normalize(ct-cp);		
	
	vec3 side = cross(vec3(0., 1., 0.), cd);
	vec3 up = cross(cd, side);
	vec3 rd = normalize(cd + 1.65*(xy.x*side + xy.y*up)); // FOV
	
    //-----------------------------------------------------	
	
	vec4 h = intersect(cp, rd);			
	vec3 p = cp + h.x*rd;
	vec3 color = vec3(0.);	
	if (h.z >= 0.) {
		vec3 n = normal(p-rd*0.01);
		vec3 R = reflect(rd, n);
		color = shade(h, p, rd, n);
	}	
	
	color = pow( clamp(color,0.0,1.0), vec3(0.45) );
	color = pow( color, vec3(1.1)) * sqrt( 64.*uv.x*uv.y*(1.-uv.x)*(1.-uv.y) );
	fragColor = vec4(color, 0.);
}