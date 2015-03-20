// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


vec4 ot=vec4(1.);
float g=1.2;

const int MaxIter = 14;
float igt = iGlobalTime;
float zoom=1.3;
vec3 dim=vec3(1.5, .25 ,1.3);

float a1 = 1.15;
float a2 = -.53;
float a3 = 2.15;
vec2 vr1 = vec2(cos(a1),sin(a1));
vec2 vr2 = vec2(cos(a2),sin(a2));
vec2 vr3 = vec2(cos(a3),sin(a3));

vec2 cmul( vec2 a, vec2 b )  { return vec2( a.x*b.x - a.y*b.y, a.x*b.y + a.y*b.x ); }


float capsule( vec3 p)
{
   	p.y -= clamp( p.y, 0.0, 4.0 );
    return length( p ) - .7;
}

float sphere( vec3 p, float r)
{
   	p.y -= .7*p.y;//1.4*r;
    return length( p ) - r;
}


float branches(in vec3 p,in float l, in float dr, in int max)
{ 
    l= min(l,capsule(p));
    float b = sphere(p,9./dr);if(b>1.5)return min(b,l);
    for(int i=0;i<MaxIter;i++) {
        if(i>max)continue;
        p.y-=2.5;   		
        p.xz = abs(p.xz);
        p.xz = cmul(vr1,p.xz);
        p.xy = cmul(vr2,p.xy);	
        p =  p * g;
        dr *= g;
        l= min (l ,capsule(p) / dr);
    }
	return l;
}

float map(vec3 p)
{ 
    //p.y+=.2-.2*sin(p.x)*cos(.2*p.z);

    vec2 p0 =mod(floor((10.+p.xz)/20.),4.);
   
    p.xz=mod(10.+p.xz,20.)-10.;   
    float dr = 1.0;  	
    float l=p.y;
   
    if(p.y>.15){   
		for(int i=0;i<5;i++){
            l=branches(p,l,dr,MaxIter-i-int(p0.x+p0.y));
            p.xz = cmul(vr3,p.xz);  
            p.y-=4.5;
            if(p.y<0.)break;
            p*=g;dr*=g;   
        }
    }			
    return l;   
}

vec4 branchesOt(in vec3 p,in vec4 ot, in int max)
{ 
   
 
    for(int i=0;i<MaxIter;i++) {
        if(i>max)continue;
        p.y-=2.5;   		
        p.xz = abs(p.xz);
        p.xz = cmul(vr1,p.xz);
        p.xy = cmul(vr2,p.xy);	
        p =  p * g;
        ot=min(ot,vec4(abs(p),dot(p,p)/float(i+1)));
    }
	return ot;
}

vec4 mapOt(vec3 p)
{ 
    //p.y+=.2-.2*sin(p.x)*cos(.2*p.z);

    vec2 p0 =mod(floor((10.+p.xz)/20.),4.);
   
    p.xz=mod(10.+p.xz,20.)-10.;   
    float dr = 1.0;
    ot = vec4(1.);    	
   
    if(p.y>.15){   
		for(int i=0;i<5;i++){
            ot=branchesOt(p,ot,MaxIter-i-int(p0.x+p0.y));
            p.xz = cmul(vr3,p.xz);  
            p.y-=4.5;
            p*=g;dr*=g;   
        }
    }			
    return ot;   
}

float trace( in vec3 ro, in vec3 rd )
{
    float maxd = 120.;
    float precis = 0.001;
      
    float dt=precis*2.0;
    float t = 0.0;
    for( int i=0; i<128; i++ )
    {
		if( t>maxd ||  dt<precis*(.1+t)) continue;//break;//              
        t += dt;
		dt = map( ro+rd*t );
    }

    if( t>maxd ) t=-1.0;
    return t;
}

vec3 calcNormal( in vec3 pos )
{
	vec3  eps = vec3(.0001,0.0,0.0);
	vec3 nor;
	nor.x = map(pos+eps.xyy) - map(pos-eps.xyy);
	nor.y = map(pos+eps.yxy) - map(pos-eps.yxy);
	nor.z = map(pos+eps.yyx) - map(pos-eps.yyx);
	return normalize(nor);
}

float softShadow( in vec3 ro, in vec3 rd, in float tmin, in float tmax)
{
	float res = 1.0;
        float precis = 0.001;
	float t = tmin;
	for (int i = 0; i < 16; i++)
	{
		float dt = map( ro + rd*t );
		res = min(res, 8.0*dt/t);
		t += dt;
		if( dt<precis || t>tmax ) break;
	}
	return clamp(res*t/tmax, 0.0, 1.0);	
}


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = -1.0 + 2.0*fragCoord.xy / iResolution.xy;
	p.x *= iResolution.x/iResolution.y;
	
	vec2 m = vec2(-0.5)*6.28;
	m = (iMouse.xy/iResolution.xy-.5)*6.28;
	m+=.5*vec2(cos(0.15*igt),cos(0.09*igt))+.3;      
	
    // camera

	
	vec3 ta = vec3(.5*igt,8.,2.);
	vec3 ro =ta  + zoom*4.*(5.+sin(.2*igt))*vec3( cos(m.x)*cos(m.y), 1.+sin(m.y), sin(m.x)*cos(m.y));
	
	
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(0.,1.,0.0);
	vec3 cu = normalize(cross(cw,cp));
	vec3 cv = normalize(cross(cu,cw));
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.5*cw );


    // trace

   
	
	vec3 col = vec3(0.8,0.8,1.);
	float t = trace( ro, rd );
	if( t>0.0 )
	{
		vec3 pos = ro + t*rd;
		vec3 nor = calcNormal( pos );
        ot = mapOt(pos);
		
		// lighting
        
        vec3  light1 = normalize(vec3( 0.4, 0.9,0.4  ));
		float key = clamp( dot( light1, nor ), 0.0, 1.0 );
		if(pos.y<.15){
            key*=softShadow(pos,light1,.1,15.);
            ot=texture2D(iChannel0,(pos.xz / 16.0));
            ot.a=0.;
        }
		
		float amb = (0.4+0.6*nor.y);
		float ao = pow( clamp(1.-.5*ot.a,0.2,1.0), 1.2 );		
        vec3 brdf = vec3(ao)*(.4*amb+key);

        // material				
		vec3 rgb =ot.rgb*vec3(.9,.7,.2);
		
		// color
        col = rgb*brdf;
		col = mix(vec3(0.8,0.8,1.),col,exp(-0.01*t));

	}

	fragColor=vec4(col,1.0);
}