float time = iGlobalTime/20.0;
float sfract(float n)
{
    return smoothstep(0.0,1.0,fract(n));
}
float rand(vec2 n)
{
 	return fract(sin(dot(n,vec2(5.3357,-5.8464)))*256.75+0.325);   
}

float noise(vec2 n)
{
    float h1 = mix(rand(vec2(floor(n.x),floor(n.y))),rand(vec2(ceil(n.x),floor(n.y))),sfract(n.x));
    float h2 = mix(rand(vec2(floor(n.x),ceil(n.y))),rand(vec2(ceil(n.x),ceil(n.y))),sfract(n.x));
    float s1 = mix(h1,h2,sfract(n.y));
    return s1;
}

void doCamera( out vec3 camPos, out vec3 camTar, in float time, in float mouseX )
{
	camPos = vec3(cos(time/4.0)*8.0,-1.0,sin(time/4.0)*8.0);
    camTar = vec3(cos(time/4.0+0.1)*12.0,1.0,sin(time/4.0+0.1)*12.0);
}
vec3 doBackground( void )
{
    return vec3( 0.5, 0.5, 0.8);
}
    
float doModel( vec3 p )
{
    float height = noise(p.xz+vec2(time))*0.3+noise(p.xz*2.0+vec2(0.0,time*0.5))*0.05+noise(p.xz*3.0)*0.05;
    float model = height-p.y;
    return model;
}
vec3 doMaterial( in vec3 pos, in vec3 nor )
{
    float light = noise(pos.xz+vec2(time))*0.3+noise(pos.xz*2.0)*0.05+noise(pos.xz*3.0)*0.05+0.6;
    return vec3(0.1,0.15,0.2)*light;
}
float calcSoftshadow( in vec3 ro, in vec3 rd );

vec3 doLighting( in vec3 pos, in vec3 nor, in vec3 rd, in float dis, in vec3 mal )
{
    vec3 lin = vec3(0.0);

    vec3 lpos = vec3(noise(vec2(time,0.0))*64.0-32.0,noise(vec2(0.0,time))*64.0-32.0,1.0);
    vec3  lig = normalize(vec3(1.0,0.7,0.9));
    float dif = max(dot(nor,lig),0.0)+ max(dot(nor,pos-lpos),0.0);
    float sha = 0.0; if( dif>0.01 ) sha=calcSoftshadow( pos+0.01*nor, lig );
    
    lin += dif*vec3(4.00,4.00,4.00)*sha;
    lin += vec3(0.5,0.5,0.5);

    vec3 col = mal*lin;

	col = mix(doBackground(),col,1.0-clamp(dis*dis/90.0,0.0,1.0));

    return col;
}

float calcIntersection( in vec3 ro, in vec3 rd )
{
	const float maxd = 20.0;           // max trace distance
	const float precis = 0.001;        // precission of the intersection
    float h = precis*2.0;
    float t = 0.0;
	float res = -1.0;
    for( int i=0; i<90; i++ )          // max number of raymarching iterations is 90
    {
        if( h<precis||t>maxd ) break;
	    h = doModel( ro+rd*t );
        t += h*.8;
    }

    if( t<maxd ) res = t;
    return res;
}

vec3 calcNormal( in vec3 pos )
{
    const float eps = 0.002;             // precision of the normal computation

    const vec3 v1 = vec3( 1.0,-1.0,-1.0);
    const vec3 v2 = vec3(-1.0,-1.0, 1.0);
    const vec3 v3 = vec3(-1.0, 1.0,-1.0);
    const vec3 v4 = vec3( 1.0, 1.0, 1.0);

	return normalize( v1*doModel( pos + v1*eps ) + 
					  v2*doModel( pos + v2*eps ) + 
					  v3*doModel( pos + v3*eps ) + 
					  v4*doModel( pos + v4*eps ) );
}

float calcSoftshadow( in vec3 ro, in vec3 rd )
{
    float res = 1.0;
    float t = 0.5;                 // selfintersection avoidance distance
	float h = 1.0;
    for( int i=0; i<40; i++ )         // 40 is the max numnber of raymarching steps
    {
        h = doModel(ro + rd*t);
        res = min( res, 64.0*h/t );   // 64 is the hardness of the shadows
		t += clamp( h, 0.02, 2.0 );   // limit the max and min stepping distances
    }
    return clamp(res,0.0,1.0);
}

mat3 calcLookAtMatrix( in vec3 ro, in vec3 ta, in float roll )
{
    vec3 ww = normalize( ta - ro );
    vec3 uu = normalize( cross(ww,vec3(sin(roll),cos(roll),0.0) ) );
    vec3 vv = normalize( cross(uu,ww));
    return mat3( uu, vv, ww );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = (-iResolution.xy + 2.0*fragCoord.xy)/iResolution.y;
    vec2 m = iMouse.xy/iResolution.xy;
    
    // camera movement
    vec3 ro, ta;
    doCamera( ro, ta, iGlobalTime, m.x );

    // camera matrix
    mat3 camMat = calcLookAtMatrix( ro, ta, 0.0 );  // 0.0 is the camera roll
    
	// create view ray
	vec3 rd = normalize( camMat * vec3(p.xy,2.0) ); // 2.0 is the lens length

	vec3 col = doBackground();

	// raymarch
    float t = calcIntersection( ro, rd );
    if( t>-0.5 )
    {
        // geometry
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal(pos);

        // materials
        vec3 mal = doMaterial( pos, nor );

        col = doLighting( pos, nor, rd, t, mal );
	}
    // gamma
	col = pow( clamp(col,0.0,1.0), vec3(0.4545) );
	   
    fragColor = vec4( col, 1.0 );
}