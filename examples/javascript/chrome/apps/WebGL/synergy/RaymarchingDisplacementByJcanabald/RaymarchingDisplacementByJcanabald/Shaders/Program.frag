// Raymarching - based on inigo quilez work

// --------- Worley Noise ------------

#define SIZE 6.0


vec2 hash2D( vec2 p )
{
    float r = 523.0 * sin( dot( p, vec2( 53.3158, 43.6143 ) ) );
    return vec2( fract( 15.32354 * r ), fract( 17.25865 * r ) );
}

vec3 hash3D( vec3 p )
{
    float r = 523.0 * sin( dot( p, vec3( 53.3158, 43.6143, 12.1687 ) ) );
    return vec3( fract( 15.32354 * r ), fract( 17.25865 * r ), fract( 11.1021 * r ) );
}

mat4 getTranslationMatrix( vec3 t )
{
    mat4 tm = mat4( 1.0 );
    
    tm[0].z = t.x;
    tm[1].z = t.y;
    tm[2].z = t.z;
    
    return tm;
}
    
vec3 worley( vec3 p, inout vec3 cp )
{
    // copy p if necessary
    vec3 q = p;
    
    p *= SIZE;
    
    // get the pixel's cell
    vec3 c = floor( p );
    
    // the three closest distances
    vec3 f = vec3( 1e06 );
    
    // look for the closest point
    for( int i=-1; i<=1; i++ )
    for( int j=-1; j<=1; j++ )
    for( int k=-1; k<=1; k++ )
    {
        // get the point at this grid cell
        vec3 g = c + vec3( float( i ), float( j ), float( k ) );
        vec3 o = g + hash3D( g);
        // compute the distance between the current pixel and the grid point
        vec3 r = p - o;
        
        // euclidean^2 distance
        float d = dot( r, r );
        // euclidean
        //d = sqrt( d );
        
        //float d = length(r);
        
        // manhatan distance
        //float d = abs( r.x ) + abs( r.y ) + abs( r.z );
        
        // check if it's the closest point
        if( d < f.x )
        {
            f.z = f.y;
            f.y = f.x;
            f.x = d;
            cp.x = o.x + o.y + o.z;
        }
        else if( d < f.y )
        {
            f.z = f.y;
            f.y = d;
            cp.y = o.x + o.y + o.z;
        }
        else if( d < f.z )
        {
            f.z = d;
            cp.z = o.x + o.y + o.z;
        }
    }
    
    return f;
}


// signed distance field of a plane
float sdPlane( vec3 p )
{
    return p.y;
}

// signed distance field of a sphere
float sdSphere( vec3 p, float s )
{
    return length(p) - s;
}

// sphere with a worley noise displacement
float sdDisplacementSphere( vec3 p, vec3 f, vec3 cp, int fn )
{
    
    float w = f.x;
    
    // different combinations with the distances
    if( fn == 1 ) 		w = 1.0 - f.x;
    else if( fn == 2 ) 	w = f.y - f.x;
    else if( fn == 3 )	w = 0.5*f.x + 0.25*f.y + 0.125*f.z;
    else if( fn == 4 )	w = sqrt( 1.0 - 2.0*( 0.5*f.x - 0.1*f.y ) );

    w = clamp( w, 0.0, 1.0 );
        
    float d1 = sdSphere( p, 1.0 );
    float d2 = 0.06*( 1.0 - w );
    //float d2 = 0.06*( 1.3 + 0.3*sin( 10.0*iGlobalTime ) )*(1.0 - w );
    return d1 + d2;
}

// return the closest point
vec2 computeVisibility( vec2 dm1, vec2 dm2 )
{
    return ( dm1.x < dm2.x ) ? dm1 : dm2;
}

// this function represent the scene. Return a signed distance and
// a float that represents the material
vec2 scene( vec3 pos )
{  
    vec3 cp = vec3( 2.0 );
    vec3 f = worley( pos - vec3(  0.0, 1.0,  0.0  ), cp );
    
    vec2 dm = computeVisibility( 
        vec2( sdPlane( pos ), 1.0 ),
        vec2( sdDisplacementSphere( pos - vec3( 5.0, 1.0, -2.0 ), f, cp, 0 ), 2.0 ) );
    
    dm = computeVisibility( dm, vec2( sdDisplacementSphere( pos - vec3(  2.5, 1.0, -1.0 ), f, cp, 1 ), 3.0 ) );
    dm = computeVisibility( dm, vec2( sdDisplacementSphere( pos - vec3(  0.0, 1.0,  0.0 ), f, cp, 2 ), 4.0 ) );
    dm = computeVisibility( dm, vec2( sdDisplacementSphere( pos - vec3( -2.5, 1.0, -1.0 ), f, cp, 3 ), 3.0 ) );
    dm = computeVisibility( dm, vec2( sdDisplacementSphere( pos - vec3( -5.0, 1.0, -2.0 ), f, cp, 4 ), 2.0 ) );
    //vec2 dm = vec2( sdSphere( pos, 1.0 ), 1.0 );
    return dm;
}

// computes the normal using finite differences
vec3 calcNormal( vec3 pos )
{
    float eps = 0.01; // precission
    float gradX = scene( pos + vec3(eps, 0.0, 0.0) ).x - scene( pos - vec3(eps, 0.0, 0.0)).x;
    float gradY = scene( pos + vec3(0.0, eps, 0.0) ).x - scene( pos - vec3(0.0, eps, 0.0)).x;
    float gradZ = scene( pos + vec3(0.0, 0.0, eps) ).x - scene( pos - vec3(0.0, 0.0, eps)).x;
    return normalize( vec3( gradX, gradY, gradZ ) );
}

vec3 calcBackground( vec2 p )
{
    return vec3( 1.0 ) * ( 1.0 - 0.13 * length( p - vec2( 0.0 ) ) );
}

vec2 intersect( vec3 ro, vec3 rd )
{
    float e = 0.001;	// precission
    float tmin = 0.0;	// minimum distance
    float tmax = 32.0;	// maximum distance
    float t = 0.0;		// intersection distance
    float m = -1.0;		// material representation
    
    // raymarching loop
    for( int i=0; i<100; i++ )
    {
        vec2 dm = scene( ro + t*rd );
        if( dm.x < e || t > tmax ) break;
        t += dm.x;
        m = dm.y;
    }
    
    if( t > tmax ) m = -1.0;
    return vec2( t, m );
}

// function that computes a hard shadow based on the position and light direction
float shadow( vec3 pos, vec3 l )
{
    float tmin = 0.2;
    float tmax = 6.0;
    
    float t = tmin;
    for( int i=0; i<64; i++ )
    {
        vec2 dm = scene( pos + t*l );
        if( dm.x < 0.001 ) return 0.0;
        t += dm.x;
    }
    
    return 1.0;
}

// function that computes a soft shadow based on the position and light direction
float softShadow( vec3 pos, vec3 l )
{
    float tmin = 0.2;
    float tmax = 6.0;
    float k = 8.0;		// small value produces more penumbra
    float sh = 1.0;		// initially not shadow
    
    float t = tmin;
    for( int i=0; i<64; i++ )
    {
        float d = max( scene( pos + t*l ).x, 0.0 );
        sh = min( sh, k*d / t );
        t += clamp( d, 0.02, 0.1 );
        if( d < 0.001 ) break;
    }
    
    return clamp( sh, 0.0, 1.0 );
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    
    // the coordinate of the pixel between [-1, 1]
	vec2 p = -1.0 + 2.0*fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x/iResolution.y;
    
    // The ray equation r = ro + t*rd
    // TODO: compute a real camera
    vec3 ro = vec3( 0.0, 2.5, 3.6 );							// camara position
    
    vec3 ta = vec3( 0.0, 0.5, 0.0 ); 							// target position
    vec3 ww = normalize( ta - ro );								// forward vector
    vec3 uu = normalize( cross( vec3( 0.0, 1.0, 0.0 ), ww ) );	// right vector
    vec3 vv = normalize( cross( ww, uu ) );						// up vector
    vec3 rd = normalize( p.x*uu + p.y*vv + 1.5*ww );

    
    // intersection
    vec2 t = intersect( ro, rd );

    // background
    vec3 col = calcBackground( p );
    
    // check the intersection in order to get the color of the pixel
    if( t.y > -0.5 )
    {
        vec3 pos = ro + t.x*rd;
        vec3 n = calcNormal( pos );
        
        vec3 l = normalize( vec3( 1.5*sin(iGlobalTime), 0.8, 1.0 +-0.6*cos(iGlobalTime) ) );
        vec3 ref = reflect( rd, n );
        
        
        float sh = softShadow( pos + 0.001*n, l );
        
        float con = 1.0;
        float amb = 0.5 + 0.5*n.y;
        float dif = max( dot( n, l ), 0.0 ) * sh;
        float spe = pow( clamp( dot( ref, l ), 0.0, 1.0 ), 16.0 );
        
        
        col  = 0.10 * con * vec3(0.80,0.90,1.00);
        col += 0.70 * dif * vec3(1.00,0.97,0.85);
        col += 0.60 * spe * vec3(0.80,0.90,1.00) * dif;
        col += 0.20 * amb * vec3( 0.5, 0.4, 0.4 );
        
        if ( t.y < 1.5 ) 		col *= 1.8*vec3( 0.3, 0.35, 0.40 ); 
        else if( t.y < 2.5 )	col *= vec3( 1.5, 0.9, 1.0 );
        else if( t.y < 3.5 )	col *= vec3( 1.5, 0.75, 1.0 );
        else if( t.y < 4.5 )	col *= vec3( 1.5, 0.6, 1.0 );
        
    }
    
	fragColor = vec4(col,1.0);
}