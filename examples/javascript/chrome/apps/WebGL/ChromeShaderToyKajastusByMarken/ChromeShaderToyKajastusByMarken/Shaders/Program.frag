float rbox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}

float Map( vec3 p )
{
	p.z -= iGlobalTime*1.25;
	p = mod(p,2.5)-0.5*2.5;
	
	vec3 p2 = p;
		
	float d1 = rbox(p2, vec3(0.25,0.25,2.25), 0.5);
	float d2 = rbox(p2, vec3(0.15,0.15,1.25), 0.5);
	d1 = max(-d2,d1);
	
	p.x += 0.3;
	p.z += 0.5;
	d2 = rbox(p, vec3(0.01,0.5,0.01), 0.02);
	
	p.x -= 0.65;
	p.z -= 0.8;
	float d3 = rbox(p, vec3(0.01,0.5,0.01), 0.02);
    d1 = -log( exp( -6.0*d1 ) + exp( -6.0*min(d2,d3) ) )/6.0;
	
	d2 = rbox(p2, vec3(0.05,5.0,0.05), 0.4);
	float res = max(-d2, d1);
	
	p2 = vec3(sin(-0.3)*p2.x-p2.y, p2.y, p2.z);
	p2.z -= 0.8;
	d2 = rbox(p2, vec3(0.05,5.0,0.05), 0.4);
	
	return max(-d2, res);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p=-1.0+2.0*fragCoord.xy/iResolution.xy;
	p.x *= 1.7;
	
	vec3 ro = vec3( 1.2 );
	vec3 rd = normalize( vec3( p, -1.0 ) );
	
	float h = 1.0;
	float t = 0.0;
	
    
	for( int i = 0; i<64; i++ )
	{
		if( h < 0.001 )// || t > 20.0 )
			break;

		h = Map( rd*t + ro );
		
		t += h;
	}

	fragColor=vec4(t*0.18);
}