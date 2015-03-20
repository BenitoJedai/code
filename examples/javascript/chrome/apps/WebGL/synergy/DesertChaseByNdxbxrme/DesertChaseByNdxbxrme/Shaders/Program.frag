//my first attempt at raymarching
//heavily based on https://www.shadertoy.com/view/Xds3zN by IQ
//give me a shout if you have any suggestions/improvements
//ndxbxrme

float pn(vec3 p) {
	//noise function by CPU https://www.shadertoy.com/view/4sfGRH
    vec3 i = floor(p); 
	vec4 a = dot(i, vec3(1., 57., 21.)) + vec4(0., 57., 21., 78.);
    vec3 f = cos((p-i)*3.141592653589793)*(-.5) + .5;  
	a = mix(sin(cos(a)*a), sin(cos(1.+a)*(1.+a)), f.x);
    a.xy = mix(a.xz, a.yw, f.y);   
	return mix(a.x, a.y, f.z);
}

float getNoise(vec2 pos, float pulse) {
	vec3 q = vec3(pos * 2., pos.x-pos.y + iGlobalTime * 0.3);
	float b = (pulse * 1.6) + pn(q * 2.) + 2.8;
	b +=  .25 * pn(q * 4.);
	b +=  .25  * pn(q * 8.);
	b +=  .5  * pn(vec3(pos, pos.x-pos.y + iGlobalTime * 0.3) * 12.23);
	b = pow(b,0.5);	
	return b;
}

vec2 rotate(vec2 uv, float d)
{
	vec2 tuv = uv;		
	uv.x = tuv.x*cos(d)-tuv.y*sin(d);
	uv.y = tuv.x*sin(d)+tuv.y*cos(d);
	return uv;
}

vec3 sun(vec2 pos, float size, float pulse)
{
	//scale the scene
	pos *= size;
	//make a circle
	float r = length(pos);
    float f = 0.23 / r;
	//tighten it a little
    f *= smoothstep(0.2, 0.8, f * .5);
	//bring the noise
	float b = getNoise(pos, pulse);
	//mix circle and noise
	b = mix(0.0, b, f);
	b *= f;
	b *= 0.5;
	//draw the sun
 	return vec3(b, 0.67 * b  * (0.7 + sin(iGlobalTime * 7.) * 0.01) , 0.0);
}

vec3 binaryStar(vec2 uv, float size, float pulse)
{
	//scale the scene
	uv *= size;
	//rotate the scene
	///uv = rotate(uv, sin(iGlobalTime * .25));
	//draw the suns
	return sun(uv + vec2(0.,0.), 1.0, pulse) 
		+ sun(uv + vec2(sin(iGlobalTime)*0.4, cos(iGlobalTime)*0.1), 3.0-cos(iGlobalTime) * 1.4, pulse);
}

vec3 sky(vec3 ro, vec3 rd)
{
	rd.y -= 0.2;
	float c = pow(rd.x,2.0) * 0.2 + pow(rd.y,2.0);
  	vec3 col = vec3(c * 0.25,c * 0.8,c * 0.8);	
	col += binaryStar(rd.xy, 1.0, 1.0);
	return col;
}

float sdPlane( vec3 p )
{
	return p.y;
}

float sdSphere( vec3 p, float s )
{
	p.y = pow(p.y, 13.0);
    return length(p)-s;
}

float opS( float d1, float d2 )
{
    return max(-d2,d1);
}

vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}

vec3 opRep( vec3 p, vec3 c )
{
    return mod(p,c)-0.5*c;
}

float dunes(in vec3 pos)
{
	return 0.3 * sin(pos.z + pn(pos))* sin(pos.x) * 1.5;
}

vec2 map(in vec3 pos)
{
	 
	vec2 res = opU(vec2(sdPlane(pos) + dunes(pos) , 1.0),
				   vec2( 
					   sdSphere(    
						   pos-vec3( 0.0,dunes(pos)-.6, 0.0 - iGlobalTime + pn(pos)),
						   0.025 ),
					46.9 ) );
	return res;
}

vec2 castRay( in vec3 ro, in vec3 rd, in float maxd )
{
	float precis = 0.001;
    float h=precis*2.0;
    float t = 0.0;
    float m = -1.0;
    for( int i=0; i<60; i++ )
    {
        if( abs(h)<precis||t>maxd ) continue;//break;
        t += h;
	    vec2 res = map( ro+rd*t );
        h = res.x;
	    m = res.y;
    }

    if( t>maxd ) m=-1.0;
    return vec2( t, m );
}

float softshadow( in vec3 ro, in vec3 rd, in float mint, in float maxt, in float k )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i<30; i++ )
    {
		if( t<maxt )
		{
        float h = map( ro + rd*t ).x;
        res = min( res, k*h/t );
        t += 0.02;
		}
    }
    return clamp( res, 0.0, 1.0 );

}

vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}

float calcAO( in vec3 pos, in vec3 nor )
{
	float totao = 0.0;
    float sca = 1.0;
    for( int aoi=0; aoi<5; aoi++ )
    {
        float hr = 0.01 + 0.05*float(aoi);
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        totao += -(dd-hr)*sca;
        sca *= 0.75;
    }
    return clamp( 1.0 - 4.0*totao, 0.0, 1.0 );
}

vec3 render(in vec3 ro, in vec3 rd)
{
	vec3 col = sky(ro,rd);
	vec2 res = castRay(ro, rd, 20.0);	
	float t = res.x;
	float m = res.y;
	if(m>-0.5)
	{
		vec3 pos = ro + t*rd;
        vec3 nor = calcNormal( pos );

		//col = vec3(0.6) + 0.4*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
		vec3 newcol = vec3(0.6) + 0.4*sin( vec3(0.05,0.08,0.10)*(m-1.0) ) + getNoise(pos.xx * 5.2, 1.0) * 0.25;
		
        float ao = calcAO( pos, nor );

		vec3 lig = normalize( vec3(-sin(iGlobalTime)*0.8, 0.8, -4.7) );
		float amb = clamp( 0.5+0.2*nor.y, 0.0, 1.0 );
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);

		float sh = 1.0;
		if( dif>0.001 ) { sh = softshadow( pos, lig, 0.02, 10.0, 7.0 ); dif *= sh; }

		vec3 brdf = vec3(0.0);
		brdf += 0.20*amb*vec3(0.10,0.11,0.13)*ao;
        brdf += 0.20*bac*vec3(0.15,0.15,0.15)*ao;
        brdf += 1.20*dif*vec3(1.00,0.90,0.70);

		float pp = clamp( dot( reflect(rd,nor), lig ), 0.0, 0.2 );
		float spe = sh*pow(pp,32.0);
		float fre = ao*pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );

		float fog = exp(-0.05 * res.x*res.x);
		newcol = newcol*brdf + vec3(1.0)*newcol*spe + 0.2*fre*(0.5+0.5*newcol) ;	
		col = mix(col, newcol, fog);
	}
	//col *= exp( -0.01*t*t );
	return clamp(col, 0.0, 1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
	vec3 ro = vec3( sin(iGlobalTime)*0.2, 0.05, 2.5 - iGlobalTime);
	vec3 ta = vec3( -0.0, -0.0, 0.0 - iGlobalTime);
	vec3 cw = normalize( ta-ro );
	vec3 cp = vec3( 0.0, 1.0, 0.0 );
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );

	
    vec3 col = render( ro, rd );
	
	col = sqrt( col );

    fragColor=vec4( col, 1.0 );
}