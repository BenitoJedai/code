// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

//Based on https://www.shadertoy.com/view/4ds3zn by IQ 



vec4 ot=vec4(1.);
float g=1.18;

const int MaxIter = 16;
float igt = iGlobalTime;
float zoom=1.5;
vec3 dim=vec3(1., .3 ,1.);
mat3 tr;


//from Tree in grass by alleycatsphinx : https://www.shadertoy.com/view/Xd2GDy 
vec3 foldY(vec3 P, float c)
{
	float r = length(P.xz);
	float a = atan(P.z, P.x);
	//float c = 3.14159265358979 / n;

	a = mod(a, 2.0 * c) - c; 

	P.x = r * cos(a);
	P.z = r * sin(a);

	return P;
}



// Rotation function included in MathUtils.frag of Syntopia's Fragmentarium 
mat3 rotationMat(vec3 v, float angle)
{
	float c = cos(angle);
	float s = sin(angle);
	
	return mat3(c + (1.0 - c) * v.x * v.x, (1.0 - c) * v.x * v.y - s * v.z, (1.0 - c) * v.x * v.z + s * v.y,
		(1.0 - c) * v.x * v.y + s * v.z, c + (1.0 - c) * v.y * v.y, (1.0 - c) * v.y * v.z - s * v.x,
		(1.0 - c) * v.x * v.z - s * v.y, (1.0 - c) * v.y * v.z + s * v.x, c + (1.0 - c) * v.z * v.z
		);
}


float map(vec3 p)
{ 
    float l= length(p*dim)-1.;
	float l1 = l;
    float dr = 1.0;
    ot = vec4(1.);
				
	for(int i=0;i<MaxIter;i++) {
		if(p.y<-3.)continue;
		if(i-(i/4)*5==0)
			p = foldY(p, 1.047);
        else{
			p.yz = abs(p.yz);				
            p = tr * p * g -vec3(1.2,1.,0.);		
			dr *= g;
			ot=min(ot,vec4(abs(p),dot(p,p)));
			}
		l1=(length(p*dim)-1.) / dr;
		if(l1<l){
			l=l1;
			ot.y*=1.1;
		}
	}
			
    return l;    
}


float trace( in vec3 ro, in vec3 rd )
{
	float maxd = 20.;
	float precis = 0.001;
      
    float h=precis*2.0;
    float t = 0.0;
    for( int i=0; i<150; i++ )
    {
	if( t>maxd ||  h<precis*(.1+t)) continue;//break;//        
        
        t += h;
		h = map( ro+rd*t );
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

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = -1.0 + 2.0*fragCoord.xy / iResolution.xy;
        p.x *= iResolution.x/iResolution.y;
	
	vec2 m = vec2(-0.5)*6.28;
	if( iMouse.z>0.0 )m = (iMouse.xy/iResolution.xy-.5)*6.28;
	m+=.5*vec2(cos(0.15*igt),cos(0.09*igt))+.3;      
	
    // camera


	vec3 ta = vec3(0.,2.1,0.);
	vec3 ro =  -zoom*10.*vec3( cos(m.x)*cos(m.y), sin(m.y), sin(m.x)*cos(m.y));
	
	
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(0.,1.,0.0);
	vec3 cu = normalize(cross(cw,cp));
	vec3 cv = normalize(cross(cu,cw));
	vec3 rd = normalize( p.x*cu + p.y*cv + 2.0*cw );


    // trace

        tr = rotationMat(normalize(vec3(-1.,-1.7,-0.5)), -.85);
	
	vec3 col = vec3(0.8,0.8,1.);
	float t = trace( ro, rd );
	if( t>0.0 )
	{
		vec3 pos = ro + t*rd;
		vec3 nor = calcNormal( pos );
		
		// lighting
                vec3  light1 = vec3(  0.577, 0.577, -0.577 );
                vec3  light2 = vec3( -0.707, -0.707,0.0  );
		float key = clamp( dot( light1, nor ), 0.0, 1.0 );
		float bac = clamp( 0.2 + 0.8*dot( light2, nor ), 0.0, 1.0 );
		float amb = (0.7+0.3*nor.y);
		float ao = pow( clamp(ot.w*6.0,0.2,1.0), 1.2 );		
                vec3 brdf = vec3(ao)*(.4*amb+key+.2*bac);

                // material				
		vec3 rgb =ot.rgb*vec3(.6,.5,.2);
		
		// color
		col = mix(vec3(0.8,0.8,1.),rgb*brdf,exp(-0.04*t));
fragColor=vec4(col,1.0);
	}

	else discard;
}