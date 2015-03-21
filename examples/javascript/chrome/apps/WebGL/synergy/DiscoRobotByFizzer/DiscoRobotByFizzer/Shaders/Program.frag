
#define APPLY_FAKE_FLOOR_GLOW 1

float wc_scale = 0.7;
float wc_step = 1.3;
float ld = 0.2;
float ad = 0.4;
float aa = 0.1;
float time;

// Finds the entry and exit points of a 2D ray with a circle of radius 1
// centered at the origin.
vec2 intersectCircle(vec2 ro, vec2 rd)
{
	float a = dot(rd, rd);
	float b = 2.0 * dot(rd, ro);
	float ds = b * b - 4.0 * a * (dot(ro, ro) - 1.0);
	
	if(ds < 0.0)
		return vec2(1e3);
	
	return ((-b - sqrt(ds) * vec2(-1.0, 1.0))) / (2.0 * a);
}

mat3 rotateXMat(float a)
{
	return mat3(1.0, 0.0, 0.0, 0.0, cos(a), -sin(a), 0.0, sin(a), cos(a));
}

mat3 rotateYMat(float a)
{
	return mat3(cos(a), 0.0, -sin(a), 0.0, 1.0, 0.0, sin(a), 0.0, cos(a));
}

// Adapted from https://www.shadertoy.com/view/ldlGR7   
vec2 solve( vec2 p, float l1, float l2, float side )
{
	vec2 q = p*( 0.5 + 0.5*(l1*l1-l2*l2)/dot(p,p) );
	
	float s = l1*l1/dot(q,q) - 1.0;
	
	if( s<0.0 ) return vec2(-100.0);
	
	return q + q.yx*vec2(-1.0,1.0)*side*sqrt( s );
}

// Performs a snapping on a vector. This is used in creating a faceted look for a surface.
vec2 qu(vec2 v)
{
	float n = 2.0;
	return floor(v * n + 1e-2) / n;
}

mat3 transpose(mat3 m)
{
	return mat3(vec3(m[0].x, m[1].x, m[2].x),
				vec3(m[0].y, m[1].y, m[2].y),
				vec3(m[0].z, m[1].z, m[2].z));
}

// Returns the parametric distance and surface normal of an intersection between the given
// ray and cylinder in out_t and out_n. If there is no section, out_t and out_n are left untouched.
void cylinder(vec3 ro, vec3 rd, vec3 d, float h0, float h1, float r, mat3 m,
			  inout float out_t, inout vec3 out_n, vec3 ofs)
{
	// Transform the ray. This is for convenience when transforming whole limbs.
	ro = m * ro - ofs;
	rd = m * rd;
	
	// Project the ray's origin onto the cylinder's axis.
	float dd = dot(ro, d);
	
	// Find a local coordinate system for the cylinder.   
	vec3 u = cross(vec3(1.0, 0.0, 0.0), d);
	vec3 v = normalize(cross(u, d));
	u = normalize(cross(v, d));
	
	mat3 cm = mat3(u, v, d);
	
	// Reduce the intersection problem to two dimensions.
	vec2 lro = vec2(dot(ro, u), dot(ro, v));
	vec2 lrd = vec2(dot(rd, u), dot(rd, v));
	
	vec2 t2 = intersectCircle(lro / r, lrd / r);
	
	if(t2.y > 1e2)
		return;
	
	// Find the intersection of the ray with the endcap planes.
	float t0 = (+h0 - dd) / dot(rd, +d);
	float t1 = (-h1 + dd) / dot(rd, -d);
	
	// Sort the endcap plane intersections.
	float mt0t1 = min(t0, t1);
	float xt0t1 = max(t0, t1);
	
	// Find the ray-cylinder interection interval.
	float i0 = max(min(t0, t1), t2.y);
	float i1 = min(max(t0, t1), t2.x);
	
	// If the interval is invalid, then there was no intersection.
	if(i1<=i0)
		return;
	
	vec3 rp = ro + rd * i0;
	
	// Find and transform the surface normal into worldspace.
	vec3 n = transpose(cm) * transpose(m) * normalize(rp - d * clamp(dot(rp, d), h0 + 0.2, h1 - 0.2));
	
	// Snap the normal to make the cylinder appear faceted. The normal is projected
	// onto a cube before snapping in 2D.
	vec3 an = abs(n);
	
	if(an.x > an.y && an.x > an.z)
	{
		n.yz = qu(n.yz / n.x);
		n.x = 1.0;
	}
	else if(an.y > an.x && an.y > an.z)
	{
		n.xz = qu(n.xz / n.y);
		n.y = 1.0;
	}
	else if(an.z > an.y && an.z > an.x)
	{
		n.xy = qu(n.xy / n.z);
		n.z = 1.0;
	}
	
	// Store the hitpoint data if it's valid.
	out_n = mix(cm * n, out_n, step(out_t, i0));
	out_t = min(i0, out_t);
}

// Returns a pyramid-like periodic signal.
float pyramid(float x)
{
	x = fract(x);
	return min(x * 2.0, (1.0 - x) * 2.0);
}

// Returns a semicircular periodic signal.
float circ(float x)
{
	x = fract(x) * 2.0 - 1.0;
	return sqrt(1.0 - x * x);
}

vec3 floorTexture(vec2 p)
{
	// Create a subtle shadow under the robot.
	float l = 1.0 - smoothstep(0.0, 30.0, length(p)) - (1.0 - smoothstep(0.0, 1.0, length(p))) * 0.3;
	
	p.x += time * wc_scale * 2.0 * wc_step;
	
	// Add a small tile edge-shadowing. 
	l *= pow(1.0 - distance(fract(p * 10.0), vec2(0.5)), 0.2);
	
	p = floor(p * 10.0) / 10.0;
	
	// Create the footstep ripples.
	float tm = floor(time * 10.0) / 10.0;
	
	float wc0 = floor(tm * wc_scale);
	float wc1 = floor(tm * wc_scale + 0.5);
	
	float rt0 = wc0 / wc_scale;
	float rt1 = (wc1 - 0.5) / wc_scale;
	
	vec2 footp0 = vec2(wc0 * wc_step * 2.0 + wc_step * 0.5, ld);
	vec2 footp1 = vec2(wc1 * wc_step * 2.0 - wc_step * 0.5, -ld);
	
	float d0 = distance(p, footp0);
	float d1 = distance(p, footp1);
	
	float r0 = (tm - rt0) / wc_scale * 0.5;
	float r1 = (tm - rt1) / wc_scale * 0.5;
	
	float s0 = 0.05;
	float s1 = 0.05;
	
	float footm0 = (smoothstep(0.0, s0, r0 * 2.0 - d0) - smoothstep(s0 * 2.0, s0 * 3.0, r0 * 2.0 - d0)) *
		(1.0 - smoothstep(0.0, 1.0, r0));
	
	float footm1 = (smoothstep(0.0, s1, r1 * 2.0 - d1) - smoothstep(s1 * 2.0, s1 * 3.0, r1 * 2.0 - d1)) *
		(1.0 - smoothstep(0.0, 1.0, r1));
	
	vec3 ff = l * vec3(footm0 + footm1) * vec3(1.2, 1.0, 0.4) * 0.1;
	
	vec2 cp = floor(p);
	vec2 fp = fract(p);
	
	// Add a large tile edge-shadowing. 
	l *= pow(1.0 - distance(fp, vec2(0.5)) * 1.3, 0.2);
	
	return ff + l * mix(vec3(0.8, 0.2, 0.6) * 0.1, vec3(1.0,0.6,0.2) * 2.0,
						pow(0.5 + 0.5 * cos(cp.x * 592.0 + time) * sin(cp.y + cp.x * 0.2), 20.0));
}

vec3 scene(vec2 p)
{
	vec3 col = vec3(0.0);
	
	// Camera rotation.
	mat3 cam = rotateXMat(cos(time * 0.2) * 0.5) * rotateYMat(time * 0.3);

	vec3 ro = cam * (vec3(0.0, 0.7, 2.9 + cos(time * 2.0) * 0.3));
	vec3 rd = cam * (vec3(p, -1.0));
	
	vec2 hipp = vec2(0.0, cos(time * 10.0) * 0.1);
	float theight = 1.0;
	vec3 tdir = normalize(vec3(-0.1, 1.0, sin(time * 4.0) * 0.1));
	vec3 n;
	
	float fl = -1.5;
	float t = 1e3;
	
	// Floor.
	{
		float i = (fl - ro.y) / rd.y;
		if(i > 0.0)
			t = min(t, i);
	}
	
	// First leg.
	{
		float wc = fract(time * wc_scale);
		float l0 = 0.8;
		float l1 = 1.0;
		vec2 e = vec2((0.6 - pyramid(wc)) * wc_step, fl + circ(wc * 2.0) * 0.3 * step(0.5, wc)) - hipp;
		vec2 m = solve(e, l0, l1, 1.0);
		
		cylinder(ro, rd, normalize(vec3(m, 0.0)), 0.0, l0, 0.1, mat3(1), t, n, vec3(hipp, ld));
		cylinder(ro, rd, normalize(vec3(e - m, 0.0)), 0.0, l1, 0.1, mat3(1), t, n, vec3(hipp + m, ld));
	}
	
	// Second leg.
	{
		float wc = fract(time * wc_scale + 0.5);
		float l0 = 0.8;
		float l1 = 1.0;
		vec2 e = vec2((0.6 - pyramid(wc)) * wc_step, fl + circ(wc * 2.0) * 0.3 * step(0.5, wc)) - hipp;
		vec2 m = solve(e, l0, l1, 1.0);
		
		cylinder(ro, rd, normalize(vec3(m, 0.0)), 0.0, l0, 0.1, mat3(1), t, n, vec3(hipp, -ld));
		cylinder(ro, rd, normalize(vec3(e - m, 0.0)), 0.0, l1, 0.1, mat3(1), t, n, vec3(hipp + m, -ld));
	}
	
	// First arm.
	{
		float wc = fract(time * wc_scale);
		float l0 = 0.5;
		float l1 = 0.7;
		vec2 e = vec2((sin(wc * 3.1415926 * 2.0) * 0.5) + 0.1, -0.9) - hipp;
		vec2 m = solve(e, l0, l1, -1.0);
		vec3 ofs = vec3(hipp, ad) + tdir * theight;
		mat3 x = rotateYMat(aa);
		
		cylinder(ro, rd, normalize(vec3(m, 0.0)), 0.0, l0, 0.08, x, t, n, ofs);
		cylinder(ro, rd, normalize(vec3(e - m, 0.0)), 0.0, l1, 0.08, x, t, n, ofs + vec3(m, 0.0));
	}
	
	// Second arm.
	{
		float wc = fract(time * wc_scale + 0.5);
		float l0 = 0.5;
		float l1 = 0.7;
		vec2 e = vec2((sin(wc * 3.1415926 * 2.0) * 0.5) + 0.1, -0.9) - hipp;
		vec2 m = solve(e, l0, l1, -1.0);
		vec3 ofs = vec3(hipp, -ad) + tdir * theight;
		mat3 x = rotateYMat(-aa);
		
		cylinder(ro, rd, normalize(vec3(m, 0.0)), 0.0, l0, 0.08, x, t, n, ofs);
		cylinder(ro, rd, normalize(vec3(e - m, 0.0)), 0.0, l1, 0.08, x, t, n, ofs + vec3(m, 0.0));
	}
	
	// Torso.
	cylinder(ro, rd, tdir, 0.0, theight, 0.2, mat3(1), t, n, vec3(hipp, 0.0));
	
	// Shoulders.
	cylinder(ro, rd, tdir, 0.2, theight + 0.1, 0.32, mat3(1), t, n, vec3(hipp, 0.0));
	
	// Head.
	cylinder(ro, rd, tdir, theight + 0.1, theight + 0.5, 0.2, mat3(1), t, n, vec3(hipp, 0.0));
	
	if(t>1e2)
		return 0.75 * mix(vec3(0.0), vec3(0.18, 0.1, 0.11) * 0.5, rd.y + 0.2) *
				(vec3(1.0) + 0.1 );
	
	// Perform the shading.
	vec3 rp = ro + rd * t;
	
	if(rp.y < fl + 1e-2)
	{
		col = floorTexture(rp.xz);

#if APPLY_FAKE_FLOOR_GLOW
		// (fake)glow
		mat3 tcam = transpose(cam);
		
		for(int iy = 0; iy < 8; iy += 1)
			for(int ix = 0; ix < 8; ix += 1)
			{
				vec3 op=vec3(0.5 + float(ix - 4) * 2.0 - mod(time * wc_scale * 2.0 * wc_step, 2.0),
							 	fl, 0.5 + float(iy - 4) * 2.0);
				vec3 vp=tcam * (op - ro);
				if(vp.z < 0.0)
				{
					vec2 pp=vp.xy / -vp.z;
					col += vec3(1.0 - smoothstep(0.0,0.3,pow(distance(p,pp), 0.7))) *
									pow(floorTexture(op.xz) * 1.6,vec3(1.5));
				}
			}
#endif
		
	}
	else
	{
		// Apply reflection.
		vec3 r = normalize(reflect(rd, n));
		
		col =  vec3(0.01);
		
		{
			float i = (fl - rp.y) / r.y;
			if(i > 0.0)
				col += floorTexture(rp.xz + r.xz * i) * 1.2;
		}
	}
	
	col *= 2.0;
	
	vec3 rpn = normalize(rp - vec3(sin(time * 1.4) * 3.0, 2.0, cos(time) * 3.0));
	
	float u = atan(rpn.z, rpn.x);
	float v = acos(rpn.y);
	
	float fu = fract(u * 8.0 / 3.1415926);
	float fv = fract(v * 6.0);
	
	float iu = floor(u * 8.0 / 3.1415926);
	float iv = floor(v * 6.0);
	
	col += (1.0 - smoothstep(0.3, 0.32, distance(vec2(fu, fv), vec2(0.5)))) * 0.1 *
		mix(vec3(1.0,1.0,0.4) * 0.02, vec3(1.0,0.5,0.4),
			pow(0.5 + 0.5 * cos(iu + iv * 10.0 + time * 3.0), 50.0)) * step(iv, 16.0);
	
	return col;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	time = iGlobalTime;
	vec2 uv = fragCoord.xy / iResolution.xy;
	fragColor.rgb = scene((uv * 2.0 - vec2(1.0)) * vec2(iResolution.x / iResolution.y, 1.0)) * 1.3;
}
