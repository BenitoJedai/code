// Ben Weston 2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.


// Divide the view into multiple viewports
// Set global variables to replace iResolution and fragCoord for the local viewport
// Returns index of which panel is being drawn for this pixel
// in the range [0,numPanels.x*numPanels.y)
vec2 view_Resolution;
vec2 view_FragCoord;
int SideMenu( ivec2 numPanels, in vec2 fragCoord, out vec4 fragColor )
{
	// arrange so that the main view and the side views have the same aspect ratio
	vec2 dims = vec2(
						iResolution.x/float(numPanels.x+numPanels.y), // main view is sv.y times bigger on both axes!
						iResolution.y/float(numPanels.y)
						);


	// which one is selected?
	ivec2 viewIndex = ivec2(floor(iMouse.xy/dims));

	int selectedPanel = 0;
	if ( viewIndex.x < numPanels.x )
	{
		selectedPanel = viewIndex.y+viewIndex.x*numPanels.y;
	}
	

	// figure out which one we're drawing
	viewIndex = ivec2(floor(fragCoord.xy/dims));

	int index;
	vec4 viewport;
	if ( viewIndex.x < numPanels.x )
	{
		viewport.xy = vec2(viewIndex)*dims;
		viewport.zw = dims;
		index = viewIndex.y+viewIndex.x*numPanels.y;
	}
	else
	{
		// main view, determined by where the last click was
		viewport.x = float(numPanels.x)*dims.x;
		viewport.y = 0.0;
		viewport.zw = dims*float(numPanels.y);
		index = selectedPanel;
	}
	
	
	// highlight currently selected
	if ( index == selectedPanel && viewIndex.x < numPanels.x &&
		( fragCoord.x-viewport.x < 2.0 || viewport.x+viewport.z-fragCoord.x < 2.0 ||
		  fragCoord.y-viewport.y < 2.0 || viewport.y+viewport.w-fragCoord.y < 2.0 ) )
	{
		fragColor = vec4(1,1,0,1);
		return -1;
	}
	
	// compute viewport-relative coordinates
	view_FragCoord = fragCoord.xy - viewport.xy;
	view_Resolution = viewport.zw;
	
	return index;
}


// --

const float tau = 6.28318530717958647692;

// Gamma correction
#define GAMMA (2.2)

vec3 ToLinear( in vec3 col )
{
	// simulate a monitor, converting colour values into light values
	return pow( col, vec3(GAMMA) );
}

vec3 ToGamma( in vec3 col )
{
	// convert back into colour values, so the correct light will come out of the monitor
	return pow( col, vec3(1.0/GAMMA) );
}

vec2 Noise( in vec3 x )
{
    vec3 p = floor(x);
    vec3 f = fract(x);
	f = f*f*(3.0-2.0*f);
//	vec3 f2 = f*f; f = f*f2*(10.0-15.0*f+6.0*f2);

	vec2 uv = (p.xy+vec2(37.0,17.0)*p.z) + f.xy;

#if (1)
	vec4 rg = texture2D( iChannel0, (uv+0.5)/256.0, -100.0 );
#else
	// on some hardware interpolation lacks precision
	vec4 rg = mix( mix(
				texture2D( iChannel0, (floor(uv)+0.5)/256.0, -100.0 ),
				texture2D( iChannel0, (floor(uv)+vec2(1,0)+0.5)/256.0, -100.0 ),
				fract(uv.x) ),
				  mix(
				texture2D( iChannel0, (floor(uv)+vec2(0,1)+0.5)/256.0, -100.0 ),
				texture2D( iChannel0, (floor(uv)+1.5)/256.0, -100.0 ),
				fract(uv.x) ),
				fract(uv.y) );
#endif			  

	return mix( rg.yw, rg.xz, f.z );
}


// Set up a camera looking at the scene.
// origin - camera is positioned relative to, and looking at, this point
// distance - how far camera is from origin
// rotation - about x & y axes, by left-hand screw rule, relative to camera looking along +z
// zoom - the relative length of the lens
vec3 localRay;
void CamPolar( out vec3 pos, out vec3 ray, in vec3 origin, in vec2 rotation, in float distance, in float zoom )
{
	// get rotation coefficients
	vec2 c = vec2(cos(rotation.x),cos(rotation.y));
	vec4 s;
	s.xy = vec2(sin(rotation.x),sin(rotation.y)); // worth testing if this is faster as sin or sqrt(1.0-cos);
	s.zw = -s.xy;

	// ray in view space
	ray.xy = view_FragCoord.xy - view_Resolution.xy*.5;
	ray.z = view_Resolution.y*zoom;
	ray = normalize(ray);
	localRay = ray;
	
	// rotate ray
	ray.yz = ray.yz*c.xx + ray.zy*s.zx;
	ray.xz = ray.xz*c.yy + ray.zx*s.yw;
	
	// position camera
	pos = origin - distance*vec3(c.x*s.y,s.z,c.x*c.y);
}

float DistanceField( vec3 pos );

vec3 Normal( vec3 pos )
{
	const vec2 delta = vec2(0,.001);
	vec3 grad;
	grad.x = DistanceField( pos+delta.yxx )-DistanceField( pos-delta.yxx );
	grad.y = DistanceField( pos+delta.xyx )-DistanceField( pos-delta.xyx );
	grad.z = DistanceField( pos+delta.xxy )-DistanceField( pos-delta.xxy );
	return normalize(grad);
}

float Trace( vec3 pos, vec3 ray )
{
	const vec2 interval = vec2(0.0,10.0); // could do ray traced bounding shape to get tighter region
	
	float h = 1.0;
	float t = interval.x;
	for ( int i=0; i < 100; i++ )
	{
		if ( t > interval.y || h < .01 )
			break;
		h = DistanceField( pos+ray*t );
		t += h;
	}
	
	if ( t > interval.y || t < interval.x || h > .1 )
		return 0.0;
	
	return t;
}



// --- scene ---

int type;

float DistanceField( vec3 pos )
{
	if ( type >= 5 )
	{
		vec3 a = max( abs(pos)-.354, vec3(0) );
		return (dot( a, vec3(1) )-.5)/sqrt(3.0);
	}
	else
	{
		//return length(pos)-1.0;// + Noise(sqrt(pow(pos,vec3(2.0))+1.0)*10.0).x*.1;
		
		vec4 p;
		vec2 d = vec2(-1,1)/sqrt(3.0);
		p.x = dot(pos,d.yyy);
		p.y = dot(pos,d.yxx);
		p.z = dot(pos,d.xyx);
		p.w = dot(pos,d.xxy);
		
		//p += cos(p*15.0+iGlobalTime)*.1;
		//p += cos(p.yzwx*tau*2.0+iGlobalTime)*.1;
		
		//val += dot(cos(p*8.0+cos(p.yzwx*12.0)+sin(p.yzwx*12.0)+iGlobalTime)*.1,vec4(1,1,1,1));
		
		float time = iGlobalTime*.3;
		time += sin(time);
		
		p.x += smoothstep(.0,1.,p.x)*sin(length(p.yzw)*17.0+time)*.1;
		p.y += smoothstep(.0,1.,p.y)*sin(length(p.zwx)*17.0+time)*.1;
		p.z += smoothstep(.0,1.,p.z)*sin(length(p.wxy)*17.0+time)*.1;
		p.w += smoothstep(.0,1.,p.w)*sin(length(p.xyz)*17.0+time)*.1;
		
		float val = length(p)-1.0;
		
		/*p.yzwx += cos(p*tau*1.0+iGlobalTime)*.1;
		p.zwxy += cos(p*tau*1.0+iGlobalTime+tau/3.0)*.1;
		p.wxyz += cos(p*tau*1.0+iGlobalTime+tau*2.0/3.0)*.1;*/
		
		return val*.5;
	}
}


const vec3 ambient = vec3(0);
const vec3 lightCol0 = vec3(1,.9,.8);
const vec3 lightCol1 = vec3(.5,.7,1);
const vec3 lightCol2 = vec3(.1,.05,.02);
const vec3 lightDir0 = vec3(-1,3,-2)*0.26726; //normalize(vec3(1,2,3)) silly compiler
const vec3 lightDir1 = vec3(0,1,0);
const vec3 lightDir2 = vec3(0,-1,0);
vec3 Lighting( vec3 p, vec3 n )
{
	// build a spherical harmonic matrix using directional lights
	const vec3 sh0 = lightCol1*lightDir1.x+lightCol2*lightDir2.x;
	const vec3 sh1 = lightCol1*lightDir1.y+lightCol2*lightDir2.y;
	const vec3 sh2 = lightCol1*lightDir1.z+lightCol2*lightDir2.z;
	const vec3 sh3 = lightCol1+lightCol2+ambient;

	return .5*(
				n.x*sh0 +
				n.y*sh1 +
				n.z*sh2 +
				sh3)
			+ pow(max(.0,dot(n,lightDir0)),1.3)*lightCol0;
}

vec3 Sky( vec3 ray )
{
	//return mix( vec3(1.05), vec3(0,.3,.7), abs(ray.y) );
	return mix( vec3(.1,.05,.02), 1.5*pow( vec3( .01, .1, .5 ), vec3(abs(ray.y)) ), smoothstep( -.1, .1, ray.y ) );
}

vec4 Albedo( vec3 pos, vec3 normal )
{
	int pattern = type;
	if ( pattern >= 5 ) pattern -= 5;
	
	if ( pattern == 0 )
	{
		vec3 p = pos;
		p.yz = p.yz*vec2(.5,sqrt(3.0)*.5)+p.zy*vec2(sqrt(3.0)*.5,-.5);
		p *= 32.0*vec3(1,2,1);
		float f = abs(Noise(p)*2.0-1.0).x; p *= .5;
		f = mix( f, abs(Noise(p)*2.0-1.0).x, .5 ); p *= .5;
		f = mix( f, abs(Noise(p)*2.0-1.0).x, .5 ); p *= .5;
		f = mix( f, abs(Noise(p)*2.0-1.0).x, .5 ); p *= .5;
		f = mix( f, abs(Noise(p)*2.0-1.0).x, .5 ); p *= .5;
		
		vec3 col = mix( vec3(1), vec3(0), pow(1.0-f,12.0) );
		
		return vec4( col, .01);
	}
	
	if ( pattern == 1 )
		return vec4(1,0,0,.01);
	
	if ( pattern == 2 )
		return vec4(0,.2,0,.1);
	
	if ( pattern == 3 )
	{
		vec3 p = pos;
		p.yz = p.yz*vec2(.5,sqrt(3.0)*.5)+p.zy*vec2(sqrt(3.0)*.5,-.5);
		p *= 50.0;
		float f =   pow(Noise(p).x*2.0-1.0,2.0); p *= .5;
		f = mix( f, pow(Noise(p).x*2.0-1.0,2.0), .5 ); p *= .5;
		f = mix( f, pow(Noise(p).x*2.0-1.0,2.0), .5 ); p *= .5;
		f = mix( f, pow(Noise(p).x*2.0-1.0,2.0), .5 ); p *= .5;
		f = mix( f, pow(Noise(p).x*2.0-1.0,2.0), .5 ); p *= .5;
		f = sqrt(f);
		
		vec3 col = mix( vec3(.8-f*.5), vec3(0,.1,0)+f*vec3(.15,.15,.05), smoothstep( .6,.7, f+normal.y ) );
		
		return vec4( col, 0 );
	}
	
	return vec4(vec3(.01),.01);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec4 oFragColor;
	type = SideMenu( ivec2(2,5), fragCoord, oFragColor );
	if ( type < 0 )
		return; // menu rendering, e.g. border colours
	
	vec3 pos, ray;
	CamPolar( pos, ray, vec3(0), vec2(.2*sin(iGlobalTime*.3)+.2,iGlobalTime*.1), 3.0, 1.0 );
	
	vec3 col = Sky(ray);

	float t = Trace( pos, ray );
	pos += t*ray;
	
	if ( t > 0.0 )
	{
		vec3 normal = Normal(pos);
		vec4 albedo = Albedo(pos, normal);
		col = albedo.xyz*Lighting(pos, normal);

		// rim lighting/vague specular
		vec3 reflectedRay = reflect(ray,normal);
		vec3 reflection = Sky(reflectedRay);
		//reflection += pow(Lighting(pos,reflectedRay),vec3(4.0))*0.0;
		vec3 h = normalize( lightDir0-ray );

		reflection += pow( max(dot(h,normal),.0), 3000.0 ) * 3000.0/32.0;

		float fresnel = mix( albedo.w, min(1.0,albedo.w/.01), pow(1.0-abs(dot(ray,normal)),5.0) );
		col = mix( col, reflection, fresnel );
	}
	
	// vignetting
	col *= smoothstep( .4, .95, localRay.z );

	fragColor = vec4(ToGamma(col),1.0);
}