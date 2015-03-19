// since (orthodox) easter in .ro was on May 5th i thought of trying an easter egg! :)
// based on iq's work (apple + clover demos). 
// modded by nervus (nervus@nervus.org)

#ifdef GL_ES
precision highp float;
#endif

mat3 m = mat3( 0.00,  0.80,  0.60,
              -0.80,  0.36, -0.48,
              -0.60, -0.48,  0.64 );

float hash( float n )
{
    return fract(sin(n)*43758.5453);
}


float noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);

    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*57.0 + 113.0*p.z;

    float res = mix(mix(mix( hash(n+  0.0), hash(n+  1.0),f.x),
                        mix( hash(n+ 57.0), hash(n+ 58.0),f.x),f.y),
                    mix(mix( hash(n+113.0), hash(n+114.0),f.x),
                        mix( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
    return res;
}

float fbm( vec3 p )
{
    float f = 0.0;

    f += 0.5000*noise( p ); p = m*p*2.02;
    f += 0.2500*noise( p ); p = m*p*2.03;
    f += 0.1250*noise( p ); p = m*p*2.01;
    f += 0.0625*noise( p );

    return f/0.9375;
}

float maxcomp(in vec3 p ) { return max(p.x,max(p.y,p.z));}
float sdBox( vec3 p, vec3 b )
{
  vec3  di = abs(p) - b;
  float mc = maxcomp(di);
  return min(mc,length(max(di,0.0)));
}

float eggShape( in vec3 p )
{

    p.y *= 0.8;
    float f = pow(1.9*dot(p.xz, p.xz), 0.65);
    p.y += 0.15 * f;
    return  (length(p) - 1.0);
}

float tableShape( in vec3 p )
{
    return p.y + 1.2;
}


vec2 map( in vec3 p )
{
    vec2 dist1 = vec2(eggShape(p), 1.0);
    vec2 dist2 = vec2(tableShape(p), 2.0);
    
    if (dist2.x < dist1.x)
        dist1 = dist2;

    return dist1;
}

vec2 intersect( in vec3 ro, in vec3 rd )
{
    float t=0.0;
    float dt = 0.06;
    float nh = 0.0;
    float lh = 0.0;
    float lm = -1.0;
    
    for(int i=0;i<100;i++)
    {
        vec2 ma = map(ro+rd*t);
        nh = ma.x;
        if(nh>0.0)
        {
            lh=nh;
            t+=dt;
        }
        lm=ma.y;
    }
    
    if( nh>0.0 )
        return vec2(-1.0);
    
    t = t - dt*nh/(nh-lh);
    return vec2(t,lm);
}

vec3 calcNormal(in vec3 pos)
{
    vec3  eps = vec3(.001,0.0,0.0);
    vec3 nor;
    nor.x = map(pos+eps.xyy).x - map(pos-eps.xyy).x;
    nor.y = map(pos+eps.yxy).x - map(pos-eps.yxy).x;
    nor.z = map(pos+eps.yyx).x - map(pos-eps.yyx).x;
    return normalize(nor);
}

float softshadow( in vec3 ro, in vec3 rd, float mint, float maxt, float k )
{
    float res = 1.0;
    float dt = 0.1;
    float t = mint;
    for( int i=0; i<30; i++ )
    {
        float h = map(ro + rd*t).x;
        if( h>0.001 )
            res = min( res, k*h/t );
        else
            res = 0.0;
        t += dt;
    }
    return res;
}

vec3 eggColor( in vec3 pos, in vec3 nor, out vec2 spe )
{
    spe.x = 1.0;
    spe.y = 1.0;
    
    float a = atan(pos.y,pos.x);
    float r = length(pos.xy);
    
    // red
    vec3 col = vec3(1.0,0.0,0.0);
    float f = smoothstep( 0.0, 1.0, fbm(pos*4.0) );
    col *= 0.8+0.2*f;
    
    // frekles
    f = smoothstep( 0.0, 1.0, fbm(pos*48.0) );
    f = smoothstep( 0.7,0.9,f);
    col = mix( col, vec3(0.9,0.9,0.6), f*0.5 );
    
    
    // stripes
    f = fbm( vec3(a*7.0 + pos.z,3.0*pos.y,pos.x)*2.0);
    f = smoothstep( 0.2,1.0,f);
    f *= smoothstep(0.4,1.2,pos.y + 0.75*(noise(4.0*pos.zyx)-0.5) );
    col = mix( col, vec3(0.4,0.2,0.0), 0.5*f );
    spe.x *= 1.0-0.35*f;
    spe.y = 1.0-0.5*f;
        
    float ao = 0.5 + 0.5 * nor.y;
    col *= ao*1.2;
    
    vec2 sc = vec2(cos(a), sin(a));
    a = atan(sc.y, sc.x);
    float sss = 0.5 + 0.5*sin(4.0*a);
    float ttt = 0.2 + 0.4*pow(sss,0.3);
    ttt += 0.1*pow(0.5+0.5*cos(8.0*a), 1.0);
    float fff = 0.0;
    if (r<ttt)
        fff = col.r;
    col = mix(col, vec3(0.0,1.0,0.0), 1.0*(1.0-r)*fff);
        
    spe.x *= 1.0-0.35*fff;
    spe.y = 1.0-0.5*fff;
    
    return col;
}

vec3 tableColor( in vec3 pos, in vec3 nor, out vec2 spe )
{
    spe.x = 1.0;
    spe.y = 1.0;
    vec3 col = vec3(0.5,0.4,0.3)*1.7;
    
    float f = fbm( 4.0*pos*vec3(6.0,0.0,0.5) );
    col = mix( col, vec3(0.3,0.2,0.1)*1.7, f );
    spe.y = 1.0 + 4.0*f;
    
    f = fbm( 2.0*pos );
    col *= 0.7+0.3*f;
    
    // frekles
    f = smoothstep( 0.0, 1.0, fbm(pos*48.0) );
    f = smoothstep( 0.7,0.9,f);
    col = mix( col, vec3(0.2), f*0.75 );
    
    // fake ao
    f = smoothstep( 0.1, 1.55, length(pos.xz) );
    col *= f*f*1.4;
    col.x += 0.1*(1.0-f);
    return col;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 p = -1.0 + 2.0 * fragCoord.xy / iResolution.xy;
    p.x *= iResolution.x/iResolution.y;


    // light
    vec3 light = normalize(vec3(1.0,0.8,-0.6));

    float ctime = iGlobalTime;
    // camera
    vec3 ro = 1.1*vec3(2.5*cos(1.5*ctime),(0.5*cos(ctime*.23)+0.5),2.5*sin(1.5*ctime));

    vec3 ww = normalize(vec3(0.0) - ro);
    vec3 uu = normalize(cross( vec3(0.0,1.0,0.0), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec3 rd = normalize( p.x*uu + p.y*vv + 1.5*ww );

    vec3 col = vec3(0.0);
    vec2 tmat = intersect(ro,rd);
    if( tmat.x>0.0 )
    {
        vec3 pos = ro + tmat.x*rd;
        vec3 nor = calcNormal(pos);
        vec3 ref = reflect(rd,nor);
        vec3 lig = normalize(vec3(1.0,0.8,-0.6));
        
        float con = 1.0;
        float amb = 0.5 + 0.5*nor.y;
        float dif = max(dot(nor,lig),0.0);
        float bac = max(0.2 + 0.8*dot(nor,vec3(-lig.x,lig.y,-lig.z)),0.0);
        float rim = pow(1.0+dot(nor,rd),3.0);
        float spe = pow(clamp(dot(lig,ref),0.0,1.0),16.0);
        
        // shadow
        float sh = softshadow( pos, lig, 0.06, 4.0, 4.0 );
        
        // lights
        col  = 0.10*con*vec3(0.80,0.90,1.00);
        col += 0.70*dif*vec3(1.00,0.97,0.85)*vec3(sh, (sh+sh*sh)*0.5, sh*sh );
        col += 0.15*bac*vec3(1.00,0.97,0.85);
        col += 0.20*amb*vec3(0.10,0.15,0.20);

        // color
        vec2 pro;
        if( tmat.y<1.5 )
            col *= eggColor(pos,nor,pro);
        else
            col *= tableColor(pos,nor,pro);
        
        // rim and spec
        col += 0.60*rim*vec3(1.0,0.97,0.85)*amb*amb;
        col += 0.60*pow(spe,pro.y)*vec3(1.0)*pro.x*sh;
        
        col = 0.3*col + 0.7*sqrt(col);
    }


    fragColor = vec4(col,1.0);
}
