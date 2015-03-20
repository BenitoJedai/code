////////////////////////////////////////
// XBE
// Joy Division tribute


//////////////////////////////////////
// Noise from IQ
vec2 hash( vec2 p )
{
	p = vec2( dot(p,vec2(127.1,311.7)),
			 dot(p,vec2(269.5,183.3)) );
	return -1.0 + 2.0*fract(sin(p)*43758.5453123);
}

float noise( in vec2 p )
{
	const float K1 = 0.366025404;
	const float K2 = 0.211324865;
	
	vec2 i = floor( p + (p.x+p.y)*K1 );
	
	vec2 a = p - i + (i.x+i.y)*K2;
	vec2 o = (a.x>a.y) ? vec2(1.0,0.0) : vec2(0.0,1.0);
	vec2 b = a - o + K2;
	vec2 c = a - 1.0 + 2.0*K2;
	
	vec3 h = max( 0.5-vec3(dot(a,a), dot(b,b), dot(c,c) ), 0.0 );
	
	vec3 n = h*h*h*h*vec3( dot(a,hash(i+0.0)), dot(b,hash(i+o)), dot(c,hash(i+1.0)));
	
	return dot( n, vec3(70.0) );
}

//////////////////////////////////////
// Musgrave's noise function

float multifractal(vec2 point)
{
	float value = 1.0;
	float rmd = 0.0;
	float pwHL = pow(2., -0.5);
	float pwr = pwHL;

	for (int i=0; i<4; i++)
	{
		value *= pwr*noise(2.*point) + 0.65;
		point *= 2.;
		pwr *= pwHL;
	}

	return value;
}


//////////////////////////////////////
/// Ray-Primitive intersections

struct Inter {
	vec3 p;		//pos
	float d;	//distance
    float dn;  // noise
};

void intPlane(vec3 ro, vec3 rd, vec3 p, vec3 n, inout Inter i)
{
	float d = -1.;
	float dpn = dot(n,rd);
	if (abs(dpn)>0.00001)
	{
		d = (dot(n, p-ro)) / dpn;
		if (d>0.)
		{
            vec3 ip = ro+d*rd;
            float vx = abs(ip.x);
            float no = 0.45*multifractal(0.925*ip.xz+0.05*iGlobalTime)*exp(-16.*vx*vx);
            float dn = ip.y - no;
            if ((dn<0.01)&&(i.d<0.)&&(vx<0.5))
            {
                i.p = ip;
                i.d = d;
                i.dn = abs(dn);
            }
 		}
	}
}

////////////////////////////////////
// Raytracing

vec3 raytrace( vec3 ro, vec3 rd)
{
	Inter i;
	i.p = vec3(0.,0.,0.);
	i.d = -1.;
    i.dn = -1.;
	//
	vec3 col = vec3(0.16);
	vec3 p = vec3(0.,0.,-1.0);
	vec3 n = vec3(0.,0.,1.);
    for (int k=0; k<64; k++)
    {
		intPlane( ro, rd, p, n, i);
        if (i.d>0.) break;
        p.z += 0.03125;
    }
	//
	if (i.d>0.)
    {
        float d = i.dn*128.*clamp(abs(2.25-ro.y),0.75,1.);
        col += vec3( i.dn<0.2?smoothstep(1.,0.,d):0. );
    }
    //
    return clamp(col,0.,1.);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    
    float T1 = -3.14159*(0.5 + 0.15*cos(0.33*iGlobalTime));
	// camera	
	vec3 ro = vec3( 2.*cos(T1), 1.25 + 0.5*sin(0.13*iGlobalTime), 1.75*sin(T1) );
	vec3 ta = vec3( 0., 0., -0.25 );
	// camera tx
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );

    // Render planes
    vec3 col = raytrace( ro, rd);
    
	// Vignetting
	vec2 r = -1.0 + 2.0*(q);
	float vb = max(abs(r.x), abs(r.y));
	col *= (0.05 + 0.95*(1.0-exp(-(1.0-vb)*30.0)));
	fragColor=vec4( col, 1.0 );
}