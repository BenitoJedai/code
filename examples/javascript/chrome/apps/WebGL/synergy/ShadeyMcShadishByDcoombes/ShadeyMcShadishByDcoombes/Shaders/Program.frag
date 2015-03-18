//Tinkering with the code found here
//https://www.shadertoy.com/view/Xds3zN

#define 	M_PI   3.14159265358979323846
#define 	DEG2RAD(x)   ((x) * 0.01745329251994329575)

//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdPlane( vec3 p )
{
	return p.y;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdSphere( vec3 p, float s )
{
    return length(p)-s;
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdCapsule( vec3 p, vec3 a, vec3 b, float r )
{
	vec3 pa = p-a, ba = b-a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	return length( pa - ba*h ) - r;
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdCylinder( vec3 p, vec2 h )
{
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}



//////////////////////////////////////////////////////////////////////////////////////////////////////////
float sdCylinder2( vec3 p, vec2 h )
{
//  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  vec2 d = abs(vec2(length(p.xy),p.z)) - h;
 
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}




//////////////////////////////////////////////////////////////////////////////////////////////////////////
mat4 rotXMat(float a)
{ 
mat4 m;
m[0] = vec4(1.0, 0.0, 0.0, 1.0); //Sets the first column    
m[1] = vec4(0.0, cos(a), sin(a), 0.0);    
m[2] = vec4(0.0, -sin(a), cos(a), 0.0);    
m[3] = vec4(0.0, 0.0, 0.0, 1.0);     
    
return m;
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
mat4 rotZMat(float a)
{ 
mat4 m;
m[0] = vec4(cos(a),sin(a), 0.0, 0.0); //Sets the first column    
m[1] = vec4(-sin(a), cos(a), 0.0,0.0);    
m[2] = vec4(0.0, 0.0, 1.0, 0.0);    
m[3] = vec4(0.0, 0.0, 0.0, 1.0);     
    
return m;
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
vec3 opTx( vec3 p, mat4 m )
{
    vec4 q = m*vec4(p,1.0);
    return q.xyz;
}

/////////////////////////////////////////////////////////////////////////////////////////////////
vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////
vec2 map( in vec3 pos )
{
//    vec2 res = opU( vec2( sdPlane(     pos), 1.0 ),
//	                vec2( sdSphere(    pos-vec3( 0.0,0.25, 0.0), 0.25 ), 46.9 ) );
   // res = opU( res, vec2( sdBox(       pos-vec3( 1.0,0.25, 0.0), vec3(0.25) ), 3.0 ) );
    
    
    
    //saucer
    vec2 res = vec2( sdCylinder(  pos-vec3( 0.0,0.30,-0.5), vec2(0.6,0.04) ), 8.0 );
    res = opU( res, vec2(sdCylinder(  pos-vec3( 0.0,0.30,-0.5), vec2(0.2,0.09) ), 8.0  ));
    res = opU( res, vec2( sdSphere(     pos-vec3(  0.0,0.30, -0.5), 0.15 ), 8.0 )  );


    //body
    res = opU( res, vec2( sdCylinder2(  pos-vec3(  0.0,-0.20, 0.2), vec2(0.2,0.5) ), 8.0 ) );
    res = opU( res, vec2( sdCylinder2(  pos-vec3(  0.0,-0.20, 0.4), vec2(0.16,0.6) ), 8.0 ) );
    
 
   //neck   
   mat4 m = rotXMat(DEG2RAD(24.0));
   res = opU( res, vec2( sdBox( opTx( pos-vec3(  -0.0,0.00, -0.12),m), vec3(0.09,0.28,0.2)), 8.0 ) );
 
    
   //engine mounts
    
   m = rotZMat(DEG2RAD(-34.0) );
   res = opU( res, vec2( sdBox( opTx( pos-vec3(  -0.1,0.00, 0.55),m), vec3(0.03,0.4,0.1)), 8.0 ) );
 
 
   m = rotZMat(DEG2RAD(34.0) );
   res = opU( res, vec2( sdBox( opTx( pos-vec3(  0.1,0.00, 0.55),m), vec3(0.03,0.4,0.1)), 8.0 ) );
    
   //engines
    
    res = opU( res, vec2( sdCylinder2(  pos-vec3(  0.35,0.30, 1.0), vec2(0.1,0.6) ), 8.0 ) );
    res = opU( res, vec2( sdCylinder2(  pos-vec3( -0.35,0.30, 1.0), vec2(0.1,0.6) ), 8.0 ) );
    res = opU( res, vec2( sdSphere(     pos-vec3(  0.35,0.30, 0.35), 0.10 ), 46.9 )  );
    res = opU( res, vec2( sdSphere(     pos-vec3( -0.35,0.30, 0.35), 0.10 ), 46.9 )  );

    res = opU( res, vec2( sdCylinder2(  pos-vec3(   0.35,0.30, 1.1), vec2(0.09,0.6) ), 8.0 ) );
    res = opU( res, vec2( sdCylinder2(  pos-vec3(  -0.35,0.30, 1.1), vec2(0.09,0.6) ), 8.0 ) );
 

   
    //sdBox( vec3 p, vec3 b )
    
    
    
    
    return res;
}





//////////////////////////////////////////////////////////////////////////////////////////////////////////
vec2 castRay( in vec3 ro, in vec3 rd )
{
    float tmin = 1.0;
    float tmax = 20.0;
    
#if 0
    float tp1 = (0.0-ro.y)/rd.y; if( tp1>0.0 ) tmax = min( tmax, tp1 );
    float tp2 = (1.6-ro.y)/rd.y; if( tp2>0.0 ) { if( ro.y>1.6 ) tmin = max( tmin, tp2 );
                                                 else           tmax = min( tmax, tp2 ); }
#endif
    
	float precis = 0.002;
    float t = tmin;
    float m = -1.0;
    for( int i=0; i<50; i++ )
    {
	    vec2 res = map( ro+rd*t );
        if( res.x<precis || t>tmax ) break;
        t += res.x;
	    m = res.y;
    }

    if( t>tmax ) m=-1.0;
    return vec2( t, m );
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////
vec3 calcNormal( in vec3 pos )
{
	vec3 eps = vec3( 0.001, 0.0, 0.0 );
	vec3 nor = vec3(
	    map(pos+eps.xyy).x - map(pos-eps.xyy).x,
	    map(pos+eps.yxy).x - map(pos-eps.yxy).x,
	    map(pos+eps.yyx).x - map(pos-eps.yyx).x );
	return normalize(nor);
}



//////////////////////////////////////////////////////////////////////////////////////////////////////////
vec3 render( in vec3 ro, in vec3 rd )
{ 

 vec3 col = vec3(0.0, 0.0, 0.0);
vec2 res = castRay(ro,rd);    
    
float t = res.x;
	float m = res.y;
    if( m>-0.5 )
    {
        vec3 pos = ro + t*rd;
        vec3 nor = calcNormal( pos );
        vec3 ref = reflect( rd, nor );
        
        // material        
		col = 0.45 + 0.3*sin( vec3(0.05,0.08,0.10)*(m-1.0) );
        
        
        if( m<1.5 ) 
        {
            
            float f = mod( floor(5.0*pos.z) + floor(5.0*pos.x), 2.0);
            col = 0.4 + 0.1*f*vec3(1.0);
        }
        
        vec3  lig = normalize( vec3(-0.6, 0.3, -0.5) );
		float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
        float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
        
        amb = amb/4.;
        col = (col*dif)+amb;
        
    }
    
    
    
    
    
return vec3( clamp(col,0.0,1.0) );
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////
mat3 setCamera( in vec3 ro, in vec3 ta, float cr )
{
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(sin(cr), cos(cr),0.0);
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}

    
//////////////////////////////////////////////////////////////////////////////////////////////////////////
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 q = fragCoord.xy/iResolution.xy;
    vec2 p = -1.0+2.0*q;
	p.x *= iResolution.x/iResolution.y;
    vec2 mo = iMouse.xy/iResolution.xy;
		 
	float time = 15.0 + iGlobalTime;

	// camera	
	vec3 ro = vec3( -0.5+3.2*cos(0.1*time + 6.0*mo.x), 1.0 + 2.0*mo.y, 0.5 + 3.2*sin(0.1*time + 6.0*mo.x) );
	vec3 ta = vec3( -0.0, -0.0, 0.5 );
	
	// camera-to-world transformation
    mat3 ca = setCamera( ro, ta, 0.0 );
    
    // ray direction
	vec3 rd = ca * normalize( vec3(p.xy,2.5) );

    // render	
    vec3 col = render( ro, rd );

	col = pow( col, vec3(0.4545) );

    fragColor=vec4( col, 1.0 );
}