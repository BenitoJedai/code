#define light normalize(vec3(cos(iGlobalTime*0.1),1.0,0.2))
float r(float n)
{
 	return fract(cos(n*29.42)*43.42);
}
vec3 r(vec3 n)
{
    float n1 = dot(n,vec3(43.25,40.85,36.31));
    float n2 = dot(n,vec3(37.73,34.90,28.46));
    float n3 = dot(n,vec3(27.25,29.85,42.84));
 	return vec3(r(n1),r(n2-256.0),r(n3+256.0)); 
}
float model(vec3 p)
{
    float saf = min(length(p+vec3(0.0,cos(iGlobalTime)*0.5,0.0))-1.0,p.y+2.0);//Sphere and floor
    float cac = min(length(max(abs(p+vec3(2.5,1.0,1.0))-1.0,0.0)),
    (length(p.xz+vec2(-1.0,-2.0))*2.0+p.y)*0.35);//Cube and cone
 	return min(saf,cac);
}
float shadow(vec3 p, vec3 n, vec3 d);
    
vec3 color(vec3 p,vec3 n)
{
    float l = shadow(p,n,-light)*0.9+0.1;//Calculate lighting
    vec3 c = pow(vec3(r(floor(p/2.0+0.5)).x),vec3(1.0,1.0,4.0));//Get color
 	return c*l;  
}
float dist(vec3 p, vec3 d)
{
    float h = 1.0;
    float r = 0.0;
    float dis = -1.0;
    for(int i = 0;i<80;i++)
    {
	    h = model( p+d*r );
        r += h;
        if (h < 0.0 || r > 30.0 ) break; 
    }
    if( r < 30.0 ) dis = r;
    return dis;
}
float shadow(vec3 p, vec3 n, vec3 d)
{
    float h = 1.0;
    float r = -20.0;
    float c = 10.0;
    for(int i = 0;i<40;i++)
    {
	    h = model( p+d*r );
        c = min(h,c);
        r += h;
        if (h < 0.0 || r > -0.1 ) break;
    }
    if (r > -0.1) c = 1.0;
    return clamp(c,0.0,max(dot(n,-d),0.0));
}
mat3 calcLookAtMatrix(vec3 ro, vec3 ta, float roll)//Function by Iq
{
    vec3 ww = normalize( ta - ro );
    vec3 uu = ( cross(ww,vec3(sin(roll),cos(roll),0.0) ) );
    vec3 vv = ( cross(uu,ww));
    return mat3( uu, vv, ww );
}

vec3 calcNormal(vec3 pos )//Also by Iq
{
    const float eps = 0.002;

    const vec3 v1 = vec3( 1.0,-1.0,-1.0);
    const vec3 v2 = vec3(-1.0,-1.0, 1.0);
    const vec3 v3 = vec3(-1.0, 1.0,-1.0);
    const vec3 v4 = vec3( 1.0, 1.0, 1.0);

	return normalize( v1*model( pos + v1*eps ) + 
					  v2*model( pos + v2*eps ) + 
					  v3*model( pos + v3*eps ) + 
					  v4*model( pos + v4*eps ) );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 f = (-iResolution.xy + 2.0*fragCoord.xy)/iResolution.y;//2D Position
    float t = iGlobalTime*0.1+iMouse.x/iResolution.x*4.0;
    vec3 p = vec3(cos(t),0.1,sin(t))*4.0;//3D Position
    mat3 cm = calcLookAtMatrix(p,vec3(0.0),0.0);//Camera matrix
    vec3 d = normalize( cm * vec3(f.xy,2.0) );//Ray direction
    float r = dist(p,d);//Ray distance
    vec3 c = pow(vec3(dot(d,light)*0.5+0.6),vec3(12.0,16.0,32.0));//Background Color
    if (r>0.0)
    {
    	c = mix(color(p+d*r,calcNormal(p+d*r)),c,pow(r/30.0,2.0));//Material color and fade
    }
	fragColor = vec4(sqrt(c),1.0);
}