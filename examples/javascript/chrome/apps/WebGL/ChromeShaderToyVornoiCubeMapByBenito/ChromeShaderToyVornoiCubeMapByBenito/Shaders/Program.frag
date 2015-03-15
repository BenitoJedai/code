//#define _DEBUG
#ifdef _DEBUG
const float kEpsilon = 0.01;
#endif
const int kGridSize = 4;


vec3 sampleVoronoiCube(vec3 N, float size)
{
	vec3 aN = abs(N);
	vec2 uv = vec2(0.);
	if (aN.x >= aN.y && aN.x >= aN.z)
	{
		uv = vec2(N.z/N.x, N.y/N.x);
	}
	else if (aN.y>=aN.x && aN.y>=aN.z )
	{
		uv = vec2(N.x/N.y, N.z/N.y);
	}
	else if (aN.z>=aN.x && aN.z>=aN.y )
	{
		uv = vec2(N.x/N.z, N.y/N.z);
	}
	uv = 0.5*(1.+uv);	
	
	float nbPoints = size*size;
	float m = floor(uv.x*size);
	float n = floor(uv.y*size);	
	
	#if defined _DEBUG
	if ( ((uv.x*size-m)<kEpsilon) || ((uv.y*size-n)<kEpsilon))
		return vec3(uv,1.);
	#endif
	
	vec3 voronoiPoint = vec3(0.);;			
	float dist2Max = 1.;
	const float _2PI = 6.28318530718;
	
	float i=0.;
	float j=0.;
	
	for (int ij=0;ij<9;ij++)
	{ 			
		i = floor(float(ij)/3.);
		j = float(ij)-3.*i;
		i--;j--;
			
			vec2 coords = vec2(m+i,n+j);																			
			float phase = _2PI*(size*coords.x+coords.y)/nbPoints;
			vec2 delta = 0.25*vec2(sin(iGlobalTime+phase), cos(iGlobalTime+phase));
			vec2 point = (coords +vec2(0.5) + delta)/size;						
			vec2 dir = uv-point;
			float dist2 = dot(dir,dir);
			
			#if defined _DEBUG //displaying points
			if (dist2<=kEpsilon*kEpsilon)
			{
				return vec3(1.0,1.,1.);
			}
			#endif	
				
			float t = 0.5*(1.+sign(dist2Max-dist2));
			vec3 tmp = vec3(point,dist2);
			dist2Max = mix(dist2Max,dist2,t);
			voronoiPoint = mix(voronoiPoint,tmp,t);							
	}	
	return voronoiPoint;		
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	float head=iGlobalTime*0.25; float pitch= 0.;float roll=iGlobalTime*0.5;
	float ch=cos(head); float cp=cos(pitch); float cr=cos(roll); 
	float sh=sin(head); float sp=sin(pitch); float sr=sin(roll); 
	mat3 rot;
	rot[0] = vec3(cr*ch-sr*sp*sh,sr*ch+cr*sp*sh,-cp*sh);
	rot[1] = vec3(-sr*cp,cr*cp,sp);
	rot[2] = vec3(cr*sh+sr*sp*ch,sr*sh-cr*sp*ch,cp*ch);
	
	// camera
	vec2 q = fragCoord.xy / iResolution.xy;
    vec2 p = -1.0 + 2.0 * q;
    p.x *= iResolution.x/iResolution.y;    
	vec3 ro = rot*vec3(1.0,0.0,0.);
	vec3 ww = normalize(vec3(0.0,0.0,0.0) - ro);
    vec3 uu = normalize(cross( vec3(0.0,1.0,0.0), ww ));
    vec3 vv = normalize(cross(ww,uu));
    vec3 rd = normalize( p.x*uu + p.y*vv + 1.2*ww); //ray direction in world space
	
	vec3 voronoi = sampleVoronoiCube(rd,float(kGridSize));
	fragColor = exp(-11.*float(kGridSize)*voronoi.z)*vec4(voronoi.xy, 5.*sqrt(voronoi.z),1.); 	
		
}