

// ------------------------------------------------------- SH, yes 'orangebook' -----------------------------------------------------------------------------

struct SHC
{
    vec3 L00, L1m1, L10, L11, L2m2, L2m1, L20, L21, L22;
};

SHC beach = SHC(
                vec3( 0.6841148,  0.6929004,  0.7069543),
                vec3( 0.3173355,  0.3694407,  0.4406839),
                vec3(-0.1747193, -0.1737154, -0.1657420),
                vec3(-0.4496467, -0.4155184, -0.3416573),
                vec3(-0.1690202, -0.1703022, -0.1525870),
                vec3(-0.0837808, -0.0940454, -0.1027518),
                vec3(-0.0319670, -0.0214051, -0.0147691),
                vec3( 0.1641816,  0.1377558,  0.1010403),
                vec3( 0.3697189,  0.3097930,  0.2029923)
                );

vec3 sh_light(vec3 normal, SHC l)
{
    float x = normal.x;
    float y = normal.y;
    float z = normal.z;
    
    const float C1 = 0.429043;
    const float C2 = 0.511664;
    const float C3 = 0.743125;
    const float C4 = 0.886227;
    const float C5 = 0.247708;
    
    return (
            C1 * l.L22 * (x * x - y * y) +
            C3 * l.L20 * z * z +
            C4 * l.L00 -
            C5 * l.L20 +
            2.0 * C1 * l.L2m2 * x * y +
            2.0 * C1 * l.L21  * x * z +
            2.0 * C1 * l.L2m1 * y * z +
            2.0 * C2 * l.L11  * x +
            2.0 * C2 * l.L1m1 * y +
            2.0 * C2 * l.L10  * z
            );
}
// noise (iq)
float hash( float n )
{
    return fract(sin(n)*43758.5453);
}

vec3 hash3( vec3 p )
{
    vec3 q = vec3( dot(p,vec3(127.1,311.7, 567.324)), 
				   dot(p,vec3(269.5,183.3, 341.693)), 
				   dot(p,vec3(419.2,371.9, 127.143)) );
	return fract(sin(q)*43758.5453);
}

float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0 + 113.0*p.z;
    return mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                   mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
               mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                   mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
}


// ------------------------------------------------------- atmosphere -----------------------------------------------------------------------------
// Credit: Written by GLtracy
// found on shadertoy

// math const
const float PI = 3.14159265359;
const float DEG_TO_RAD = PI / 180.0;
const float MAX = 10000.0;

// scatter const
const float K_R = 0.166;
const float K_M = 0.0025;
const float E = 14.3; 						// light intensity
const vec3  C_R = vec3( 0.3, 0.7, 1.0 ); 	// 1 / wavelength ^ 4
const float G_M = -0.85;					// Mie g

const float R = 1.016 * 6300.0;
const float R_INNER = 2.0 * 0.49840050697085 * 6300.0;

const float SCALE_H = 4.0 / ( R - R_INNER );
const float SCALE_L = 1.0 / ( R - R_INNER );

const int NUM_OUT_SCATTER = 10;
const float FNUM_OUT_SCATTER = 10.0;

const int NUM_IN_SCATTER = 10;
const float FNUM_IN_SCATTER = 10.0;

// angle : pitch, yaw
mat3 rot3xy( vec2 angle ) {
	vec2 c = cos( angle );
	vec2 s = sin( angle );
	
	return mat3(
		c.y      ,  0.0, -s.y,
		s.y * s.x,  c.x,  c.y * s.x,
		s.y * c.x, -s.x,  c.y * c.x
	);
}

// ray direction
vec3 ray_dir( float fov, vec2 size, vec2 pos ) {
	vec2 xy = pos - size * 0.5;

	float cot_half_fov = tan( ( 90.0 - fov * 0.5 ) * DEG_TO_RAD );	
	float z = size.y * 0.5 * cot_half_fov;
	
	return normalize( vec3( xy, -z ) );
}

// ray intersects sphere
// e = -b +/- sqrt( b^2 - c )
vec2 ray_vs_sphere( vec3 p, vec3 dir, float r ) {
	float b = dot( p, dir );
	float c = dot( p, p ) - r * r;
	
	float d = b * b - c;
	if ( d < 0.0 ) {
		return vec2( MAX, -MAX );
	}
	d = sqrt( d );
	
	return vec2( -b - d, -b + d );
}

// Mie
// g : ( -0.75, -0.999 )
//      3 * ( 1 - g^2 )               1 + c^2
// F = ----------------- * -------------------------------
//      2 * ( 2 + g^2 )     ( 1 + g^2 - 2 * g * c )^(3/2)
float phase_mie( float g, float c, float cc ) {
	float gg = g * g;
	
	float a = ( 1.0 - gg ) * ( 1.0 + cc );

	float b = 1.0 + gg - 2.0 * g * c;
	b *= sqrt( b );
	b *= 2.0 + gg;	
	
	return 1.5 * a / b;
}

// Reyleigh
// g : 0
// F = 3/4 * ( 1 + c^2 )
float phase_reyleigh( float cc ) {
	return 0.75 * ( 1.0 + cc );
}

float density( vec3 p ){
	return exp( -( length( p ) - R_INNER ) * SCALE_H );
}

float optic( vec3 p, vec3 q ) {
	vec3 step = ( q - p ) / FNUM_OUT_SCATTER;
	vec3 v = p + step * 0.5;
	
	float sum = 0.0;
	for ( int i = 0; i < NUM_OUT_SCATTER; i++ ) {
		sum += density( v );
		v += step;
	}
	sum *= length( step ) * SCALE_L;
	
	return sum;
}

vec3 in_scatter( vec3 o, vec3 dir, vec2 e, vec3 l ) {
	float len = ( e.y - e.x ) / FNUM_IN_SCATTER;
	vec3 step = dir * len;
	vec3 p = o + dir * e.x;
	vec3 v = p + dir * ( len * 0.5 );

	vec3 sum = vec3( 0.0 );
    float nsf = 80.0;
	for ( int i = 0; i < NUM_IN_SCATTER; i++ ) {
		vec2 f = ray_vs_sphere( v, l, R );
		vec3 u = v + l * f.y;
		
		float n = ( optic( p, v ) + optic( v, u ) ) * ( PI * 4.0 );
		
        vec3 ns = normalize((o + dir * f.x) - o);
        ns.x += iGlobalTime / 20.0 / float(i + 1);
        float bla = .5 + noise(ns * nsf) * .9;
		sum += density( v ) * exp( -n * ( K_R * C_R + K_M ) ) / bla;
        nsf /=2.;
	

		v += step;
	}
	sum *= len * SCALE_L;
	
	float c  = dot( dir, -l );
	float cc = c * c;
	
	return sum * ( K_R * C_R * phase_reyleigh( cc ) + K_M * phase_mie( G_M, c, cc ) ) * E;
}

// ---------------------------------------------------------Shading Functions----------------------------------------------------------------------

float pow5(float v)
{
    float tmp = v*v;
    return tmp*tmp*v;
}

float distribution(vec3 n, vec3 h, float roughness)
{
    float m_Sq= roughness * roughness;
    float NdotH_Sq= max(dot(n, h), 0.0);
    NdotH_Sq= NdotH_Sq * NdotH_Sq;
    return exp( (NdotH_Sq - 1.0)/(m_Sq*NdotH_Sq) )/ (3.14159265 * m_Sq * NdotH_Sq * NdotH_Sq);
}

float geometry(vec3 n, vec3 h, vec3 v, vec3 l, float roughness)
{
    float NdotL_clamped= max(dot(n, l), 0.0);
    float NdotV_clamped= max(dot(n, v), 0.0);
    float k= roughness * sqrt(2.0/3.14159265);
    float one_minus_k= 1.0 -k;
    return ( NdotL_clamped / (NdotL_clamped * one_minus_k + k) ) * ( NdotV_clamped / (NdotV_clamped * one_minus_k + k) );
}

float fresnel(float f0, vec3 n, vec3 l)
{
    return f0 + (1.0-f0) * pow(1.0- dot(n, l), 5.0);
}

float diffuseEnergyRatio(float f0, vec3 n, vec3 l)
{
    return 1.0 - fresnel(f0, n, l);
}
// ==============================================================================================================================================
// quaternion functions 

vec3 quatRotateVec(vec4 q, vec3 v)
{
    vec3 uv = cross(q.xyz, v);
    vec3 uuv = cross(q.xyz, uv);
    return v + ((uv * q.w) + uuv) * 2.0;
}

vec4 angleAxis(float a, vec3 v)
{
	return vec4(v * sin(a * 0.5), cos(a * 0.5));
}

vec4 quatMult(vec4 q, in vec4 p)
{
	return vec4(
		p.w * q.x + p.x * q.w + p.y * q.z - p.z * q.y,
		p.w * q.y + p.y * q.w + p.z * q.x - p.x * q.z,
		p.w * q.z + p.z * q.w + p.x * q.y - p.y * q.x,
        p.w * q.w - p.x * q.x - p.y * q.y - p.z * q.z
	);
}

vec4 quatConjugate(const vec4 q)
{
    return vec4(-q.x, -q.y, -q.z, q.w);
}

    
vec4 quatInverse(const vec4 q)
{
    return quatConjugate(q) / dot(q, q);
}

vec4 quatNormalize(const vec4 q)
{
	return q/length(q);
}

vec4 quatRotate(const vec4 q, const float angle, const vec3 v)
{
	return quatMult(vec4(v*sin(angle*0.5), cos(angle * 0.5)), q);
}



// -----------------------------------------------------Distance Function / Tools ---------------------------------------------------------------------

float smax(float a, float b, float k)
{
	return log(exp(k*a)+exp(k*b))/k;
}
/*
float smin(float a, float b, float k)
{
	return -(log(exp(k*-a)+exp(k*-b))/k);
}
*/


float rmf(vec3 p, float frequency, float lacunarity, int octaveCount)
{
    p *= frequency;
    float signal = 0.0;
    float value  = 0.0;
    float weight = 1.0;
    float h = 1.0;
    float f = 1.0;

    for (int curOctave=0; curOctave < 6; curOctave++) 
    {
        signal = noise(p);
        signal = pow(1.0 - abs(signal), 2.0) * weight;
        weight = clamp(0.0, 1.0, signal * 16.0);
        value += (signal * pow(f, -1.0));
        f *= lacunarity;
        p *= lacunarity;
    }
    
    return (value * 1.25) - 1.0;
}

float sdHexPrism(vec3 p, vec2 h, float k)
{
    vec3 q = abs(p);
    return max(q.z-h.y,max((q.x*0.866025*k+q.y*0.5),q.y)-h.x);
}

float sdCone( in vec3 p, in vec3 c )
{
    vec2 q = vec2( length(p.xz), p.y);
    vec2 v = vec2( c.z*c.y/c.x, -c.z );

    vec2 w = v - q;

    vec2 vv = vec2( dot(v,v), v.x*v.x );
    vec2 qv = vec2( dot(v,w), v.x*w.x );

    vec2 d = max(qv,0.0)*qv/vv;

    return sqrt( dot(w,w) - max(d.x,d.y) )* sign(max(q.y*v.x-q.x*v.y,w.y));
}

float sdBox(vec3 p, vec3 b)
{
    vec3 d = abs(p) - b;
    return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}



mat3 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    return mat3(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c);
}

float sdFractThing(vec3 z)
{
	float r;
    mat3 rm1 = rotationMatrix(vec3(1.0, 0.0, 0.0), 0.8);
    mat3 rm2 = rotationMatrix(vec3(1.0, 1.0, 0.0), 0.5);    
    const float Scale = 1.1;
    float Offset = 0.8;
    int m=0;
	for (int n=0; n < 4; n++) 
    {
		z *= rm1;
		
		if(z.x+z.y<0.0) z.xy = -z.yx;
		if(z.x+z.z<0.0) z.xz = -z.zx;
		if(z.y+z.z<0.0) z.zy = -z.yz;
		
		z = z*Scale - Offset*(Scale-1.0);
		z *= rm2;
		
		r = dot(z, z);
		//if (n<ColorIterations) orbitTrap = min(orbitTrap, abs(vec4(z,r)));
		
		m++;
	}
	
	return (sdHexPrism(z, vec2(0.5), 1.0)-rmf(z, 4.0, 2.0, 1)*0.1) * pow(Scale, -float(m));
}
vec3 DomainRotateSymmetry( const in vec3 vPos, const in float fSteps )
{
	float angle = atan( vPos.x, vPos.z );
	
	float fScale = fSteps / 6.283185307179;
	float steppedAngle = (floor(angle * fScale + 0.5)) / fScale;
	
	float s = sin(-steppedAngle);
	float c = cos(-steppedAngle);
	
	vec3 vResult = vec3( c * vPos.x + s * vPos.z, 
			     vPos.y,
			     -s * vPos.x + c * vPos.z);
	
	return vResult;
}

float sdTorus( vec3 p, vec2 t )
{
  return length( vec2(length(p.xz)-t.x,p.y) )-t.y;
}
float sdCappedCylinder( vec3 p, vec2 h )
{
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}
// ----------------------------------------------------------EDIT ME----------------------------------------------------------------------

float smin( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}


float cc, ss;

vec4 formula (vec4 p) 
{
	//p.y-=t*.25;
    p.y=abs(3.-mod(p.y, 6.));
    for (int i=0; i<6; i++) {
		p.xyz = abs(p.xyz)-vec3(.0,1.,.0);
		p=p*1.6/clamp(dot(p.xyz,p.xyz),.2,1.)-vec4(0.4,1.5,0.4,0.);
		p.xz*=mat2(cc,ss,-ss,cc);
	}
	return p;
}

vec2 de(vec3 pos) 
{
	float aa=smoothstep(0.,1.,clamp(cos(-pos.y*.4)*1.5,0.,1.))*3.14159;
    cc=cos(aa);
    ss=sin(aa);
	vec3 tpos=pos;
	vec4 p=vec4(tpos,1.);
	float y=max(0.,.3-abs(pos.y-3.3))/.3;
    p=formula(p);
    float fr=max(abs(p.z/p.w)-.01,length(p.zx)/p.w-.002);
    float d=fr;
    
    return vec2(d, pow(dot(p.xy, p.zw)*0.004, 2.666));
}

float sdf(vec3 p, out float m)
{
    const vec3 c = vec3(6.0);
    vec3 w = mod(p, c)-0.5*c;
    vec3 hashid=hash3(floor(p/12.0));
    w = rotationMatrix(hashid, iGlobalTime*0.05)*w;
    
    p = rotationMatrix(vec3(0.0, 1.0, 0.0), 0.1*iGlobalTime) * p;
	float sd = sdCappedCylinder(p, vec2(2.0, 0.16));
    sd = max(sd, -sdCappedCylinder(p, vec2(1.86, 0.2)));
    sd = max(sd, -sdCappedCylinder(p-vec3(0.0, 0.0, 0.0), vec2(1.96, 0.12)));
    sd = min(sd, sdBox(p, vec3(0.16, 0.16, 1.86)));
    sd = min(sd, sdBox(p, vec3(1.86, 0.16, 0.16)));
    vec2 fr = de(p*0.25-vec3(0.0, 7.97, 0.0));
    float fractal = fr.x;
    m = min(1.0, fr.y);
    fractal = max(fractal, sdCappedCylinder(p, vec2(2.0, 2.0)));
    fractal = min(fractal, length(p)-0.5);
    sd = min(sd, fractal);
    sd = min(sd, length(p-vec3(0.0, 2.3, 0.0))-0.55);
    sd = min(sd, sdBox(p-vec3(0.0, -2.7, 0.0), vec3(0.65)));
    
    float astroids = (length(w)-0.5*hash(dot(hashid, hashid))); 
    if (astroids < sd)
    {
        sd = astroids-rmf(w, 32.0, 0.5, 8)*0.01;
        m = 2.0;
    }
    return sd;
}

vec4 getColorAndRoughness(vec3 p, vec3 N, float ambo)
{
    return vec4(1.0);
}

vec3 grad(vec3 p, float eps)
{
    float m;
    vec3 f = vec3(sdf(p, m));
    vec3 g = vec3(sdf(p+vec3(eps, 0.0, 0.0), m),
                  sdf(p+vec3(0.0, eps, 0.0), m),
                  sdf(p+vec3(0.0, 0.0, eps), m));
    return (g-f) / eps;
}

// ---------------------------------------------------------------------------------------------------------------------------------------

float ambientOcclusion(const vec3 p, vec3 N)
{
    const float k = 4.0;
    float amboDelta = 0.04;
    float ambo = 0.0;
    float t = 1.0;
    float m;
    for (int i=1; i<=4; i++)
    {
        ambo += t * (float(i)*amboDelta - sdf(p+N*amboDelta*float(i), m));
        t *= 0.5;
    }
    return 1.0 - min(1.0, k*ambo);
}

float softShadow(vec3 ro, vec3 rd, float mint, float maxt, float k) 
{
	float sh = 1.0;
	float t = mint;
	float h = 0.0;
    float m;
	for (int i = 0; i < 32; i++) 
	{
		if (t > maxt)
			break;
		h = sdf(ro+rd*t, m);
		sh = min(sh, k*h/t);
		t += h;
	}
	return max(0.1, sh);
}

vec3 setupRayDirection(vec2 v, float camFov)
{
    float fov_y_scale = tan(camFov/2.0);
    vec3 raydir = vec3(v.x*fov_y_scale, v.y*fov_y_scale, -1.0);
    return normalize(raydir);
}

const float starLimit = 0.975;
const vec3 starColor = vec3(1.0, 0.9, 0.8);


void mainImage( out vec4 fragColor, in vec2 fragCoord ) 
{
    // coordinate system with origin in the center and y-axis goes up
    // y-range is [-1.0, 1.0] and x-range is based on aspect ratio.
    vec2 uv = fragCoord.xy / iResolution.xy;
    vec2 p = (-iResolution.xy + 2.0 * gl_FragCoord.xy) / iResolution.y;
    vec2 m = 2.0*(iMouse.xy * 2.0 - 1.0) * vec2(iResolution.x / iResolution.y, 1.0);
    //p.x += sin(time*10.0)*0.01*sin(uv.y*resolution.y*2.0);
    //p.y += sin(time*100.0)*0.01*sin(uv.x*resolution.y*2.0);
    
    //vec3 lightPos = vec3(sin(time), 0.0, 0.3);
    vec3 lightDir = normalize(vec3(-0.4496467, 0.4155184, 0.3416573));
    vec3 lightColor = vec3(1.0, 1.0, 1.0);
    vec3 rayOrigin = vec3(-1.95, 0.0, 0.0);
    mat3 rm = rotationMatrix(vec3(0.0, 1.0, 1.0), 0.6);
    //rm *= rotationMatrix(vec3(1.0, 0.0, 0.0), -iMouse.x*2.0);
    
    rayOrigin = vec3(-3.0, 4.0, 1.0);
    rm = rotationMatrix(normalize(vec3(0.0, 1.0, 1.0)), 2.0);
    
    vec3 rayDir = rm * setupRayDirection(p, radians(100.0));
    
    vec3 rayPos = rayOrigin;
    float sd;
    float travel = 0.0;
    
    vec3 sp = vec3(0.0);
    bool hit = false;
    float coneR;
    float mat =0.0;
    for (int i=0; i<256; i++)
    {
        coneR = travel * tan(0.25*radians(100.0)/iResolution.y);
        sd = sdf(rayPos, mat);
        
        if (abs(sd) < coneR)
        {
            hit = true;
            break;
        }
        
        rayPos += rayDir * sd *0.8;
        travel += sd *0.8;
        if (travel > 18.0)
        {
            break;
        }
    }

    if (hit == true)
    {
        //gl_FragColor = vec4(travel*0.1);return;
        vec3 N = normalize(grad(rayPos, coneR));
        vec3 P = rayPos;
        
        float ambo = ambientOcclusion(P, N);
        float shadow = softShadow(P, lightDir, 0.04, 0.01, 6.0);
        
        // constants for now 
	    float refractiveIndex = clamp(15.0*abs(mat), 0.0, 15.0);
	    float u_roughness = clamp(mat*0.25, 0.0, 1.0);
	    vec3 u_diffuseColor = mix(vec3(0.25, 0.3, 0.3)*2.0, vec3(0.3, 0.6, 0.6), clamp(mat, 0.0, 1.0));
        if (mat >= 2.0)
        {
            u_diffuseColor = vec3(1.0);
            u_roughness = 0.1;
            refractiveIndex = 3.0;
        }
        float u_fresnel0 = pow((1.0 - refractiveIndex)/(1.0 + refractiveIndex), 2.0);
	    
        // surface params
        vec3 halfVec=  normalize(lightDir - rayDir);
            
        float NdotL = dot(N, lightDir);
        float NdotV = dot(N, -rayDir);
        float NdotL_clamped= max(NdotL, 0.0);
        float NdotV_clamped= max(NdotV, 0.0);
        float brdf_spec= fresnel(u_fresnel0, halfVec, lightDir) 
            * geometry(N, halfVec, -rayDir, lightDir, u_roughness) 
            * distribution(N, halfVec, u_roughness) / (4.0 * NdotL_clamped * NdotV_clamped);
        vec3 color_spec= NdotL_clamped * brdf_spec * lightColor;
        vec3 color_diff= NdotL_clamped * diffuseEnergyRatio(u_fresnel0, N, lightDir) * u_diffuseColor * lightColor;
        vec3 color = max(vec3(0.0), color_spec)+color_diff+clamp(-N.z+0.25, 0.0, 1.0)*vec3(0.8, 0.8, 1.0)*u_diffuseColor; // + sh_light(rotationMatrix(vec3(0.0, 1.0, 0.0), 1.9)*N, beach)*0.15;
        gl_FragColor = vec4(ambo*shadow*color, 1.0);
        //gl_FragColor.r += mod((atan(rayPos.x, rayPos.z)+3.14) / 6.28, 0.0625) * 16.0;
    }
    else
    {
        vec3 ro = rayOrigin + vec3(0.0, 0.0, 6500.0);
        vec2 e = ray_vs_sphere( ro, rayDir, R );
	    if ( e.x > e.y || e.x < 0.0 ) {
		    float n = noise(vec3(uv * 1000.0, 0.0));
            if (n > starLimit) {
        		gl_FragColor = vec4(starColor * smoothstep(starLimit, 1.0, n), 1.0);
            }
            else {
            	gl_FragColor = vec4(vec3(0.0), 1.0);
            }
	
            return;
	    }
	
	    vec2 f = ray_vs_sphere( ro, rayDir, R_INNER );
        //e.y = f.x;
	    e.y = min( e.y, f.x );
        
	    vec3 I = in_scatter( ro, rayDir, e, lightDir );
        
        gl_FragColor = vec4(I, 1.0);
    }
    
    float distanceFromCenter = length( uv - vec2(0.5, 0.5) );
    float vignetteAmount;
    vignetteAmount = 1.0 - distanceFromCenter;
    vignetteAmount = smoothstep(0.1, 1.0, vignetteAmount);    
    vec3 color = gl_FragColor.xyz;
    color = pow(color, vec3(0.8));
    color += 0.05*sin(uv.y*iResolution.y*2.0);
    color += 0.009 * sin(iGlobalTime*16.0);
    color *=  vignetteAmount*1.0;
    
	fragColor.xyz = color;
}