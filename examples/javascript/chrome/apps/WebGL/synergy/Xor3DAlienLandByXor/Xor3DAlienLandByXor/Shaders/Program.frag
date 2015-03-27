
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
float worley(vec3 n,float s)
{
    float dis = 1024.0;
    for(int x = -1;x<2;x++)
    {
        for(int y = -1;y<2;y++)
        {
            for(int z = -1;z<2;z++)
        	{
            	vec3 p = floor(n/s)+vec3(x,y,z);
            	float d = length(r(p)+vec3(x,y,z)-fract(n/s));
            	if (dis>d)
            	{
            	 	dis = d;   
            	}
            }
        }
    }
    return dis;
}
float model(vec3 p)
{
 	return (0.8-worley(p,4.0));
}
vec3 color(vec3 p,vec3 n)
{
    float l = dot(normalize(n),vec3(0.0,1.0,0.0))*0.5+0.5;
    float s = pow(worley(p,0.25)*worley(p+iGlobalTime,1.0),2.0);
 	return l*vec3(vec2(s*s*s),s)*2.0;  
}
float dist(vec3 p, vec3 d)
{
    float h = 1.0;
    float r = 0.0;
    float dis = -1.0;
    for(int i = 0;i<60;i++)
    {
	    h = model( p+d*r );
        r += h*3.0;
        if (h < 0.0 || r > 30.0 ) break; 
    }
    if( r < 30.0 ) dis = r;
    return dis;
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
    float t = iGlobalTime*0.1;
    vec3 p = vec3(cos(t),0.0,sin(t))*10.0;//3D Position
    mat3 cm = calcLookAtMatrix(p,vec3(0.0),0.0);//Camera matrix
    vec3 d = normalize( cm * vec3(f.xy,2.0) );//Ray direction
    float r = dist(p,d);//Ray distance
    vec3 c = vec3(0.05,0.08,0.1);//Background Color
    if (r>0.0)
    {
    	c = mix(color(p+d*r,calcNormal(p+d*r)),c,pow(r/30.0,2.0));//Material color and fade
    }
	fragColor = vec4(sqrt(c),1.0);
}