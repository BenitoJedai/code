float PI=3.14159265;
vec3 sunCol = vec3(258.0, 28.0, 10.0) / 255.0;
vec3 environmentSphereColor = vec3(0.3001, 0.501, 0.901);
vec3 environmentGroundColor = vec3(0.4001, 0.25, 0.1) * 0.75;

float marchingMultplier = 0.25;

float distFromSphere;
vec3 normal;
vec3 texBlurry;

float material;

vec3 saturate(vec3 a)
{
	return clamp(a, 0.0, 1.0);
}
vec2 saturate(vec2 a)
{
	return clamp(a, 0.0, 1.0);
}
float saturate(float a)
{
	return clamp(a, 0.0, 1.0);
}

vec3 RotateX(vec3 v, float rad)
{
	float cos = cos(rad);
	float sin = sin(rad);
	//if (RIGHT_HANDED_COORD)
	return vec3(v.x, cos * v.y + sin * v.z, -sin * v.y + cos * v.z);
	//else return new float3(x, cos * y - sin * z, sin * y + cos * z);
}
vec3 RotateY(vec3 v, float rad)
{
	float cos = cos(rad);
	float sin = sin(rad);
	//if (RIGHT_HANDED_COORD)
	return vec3(cos * v.x - sin * v.z, v.y, sin * v.x + cos * v.z);
	//else return new float3(cos * x + sin * z, y, -sin * x + cos * z);
}
vec3 RotateZ(vec3 v, float rad)
{
	float cos = cos(rad);
	float sin = sin(rad);
	//if (RIGHT_HANDED_COORD)
	return vec3(cos * v.x + sin * v.y, -sin * v.x + cos * v.y, v.z);
}

	
// polynomial smooth min (k = 0.1);
float smin( float a, float b, float k )
{
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return mix( b, a, h ) - k*h*(1.0-h);
}
// exponential smooth min (k = 32);
/*float smin( float a, float b, float k )
{
    float res = exp( -k*a ) + exp( -k*b );
    return -log( res )/k;
}*/
vec3 GetSunColorReflection(vec3 rayDir, vec3 sunDir)
{
	vec3 localRay = normalize(rayDir);
	float sunIntensity = 1.0 - (dot(localRay, sunDir) * 0.5 + 0.5);
	//sunIntensity = (float)Math.Pow(sunIntensity, 14.0);
	sunIntensity = max(0.0, 0.01 / sunIntensity - 0.025);
	sunIntensity = min(sunIntensity, 40000.0);
	vec3 ground = mix(environmentGroundColor, environmentSphereColor,
					  pow(abs(localRay.y), 0.35)*sign(localRay.y) * 0.5 + 0.5);
	return ground + sunCol * sunIntensity;
}
vec3 GetSunColorStupid(vec3 rayDir, vec3 sunDir)
{
	vec3 localRay = normalize(rayDir);
	float sunIntensity = 1.0 - (dot(localRay, sunDir) * 0.5 + 0.5);
	//sunIntensity = (float)Math.Pow(sunIntensity, 14.0);
	sunIntensity = max(0.0, 0.01 / sunIntensity - 0.025);
	sunIntensity = min(sunIntensity, 40000.0);
	vec3 ground = mix(environmentGroundColor, environmentSphereColor,
					  pow(localRay.y, 0.35)*sign(localRay.y) * 0.5 + 0.5);
	return sunCol * sunIntensity;
}

float IntersectSphereAndRay(vec3 pos, float radius, vec3 posA, vec3 posB, out vec3 intersectA2, out vec3 intersectB2)
{
	// Use dot product along line to find closest point on line
	vec3 eyeVec2 = normalize(posB-posA);
	float dp = dot(eyeVec2, pos - posA);
	vec3 pointOnLine = eyeVec2 * dp + posA;
	// Clamp that point to line end points if outside
	//if ((dp - radius) < 0) pointOnLine = posA;
	//if ((dp + radius) > (posB-posA).Length()) pointOnLine = posB;
	// Distance formula from that point to sphere center, compare with radius.
	float distance = length(pointOnLine - pos);
	float ac = radius*radius - distance*distance;
	float rightLen = 0.0;
	if (ac >= 0.0) rightLen = sqrt(ac);
	intersectA2 = pointOnLine - eyeVec2 * rightLen;
	intersectB2 = pointOnLine + eyeVec2 * rightLen;
	distFromSphere = distance - radius;
	if (distance <= radius) return 1.0;
	return 0.0;
}

float dSphere(vec3 p, float rad)
{
	//vec3 center = vec3(0, 0, 0.0);
	//p -= center;
	//rad += sin(p.y * 32.0 - iGlobalTime * 8.0) * 0.01;
	return length(p) - rad;
}
float dSphereWave(vec3 p, float rad)
{
	//vec3 center = vec3(0, 0, 0.0);
	//p -= center;
	rad -= 0.05;
	rad += sin(p.y * 8.0 - iGlobalTime * 2.0) * 0.03;
	return length(p) - rad;
}

float dBox(vec3 pos, vec3 b)
{
	return length(max(abs(pos)-(b),0.0));
}

float dBoxSigned(vec3 p)
{
	float b = 1.0;
	vec3 b2 = vec3(6.0, 2.0, 2.0);
	vec3 center = vec3(0, -2.0, 0.0);
	vec3 d = abs(p - center) - b2;//*abs(cos(p.y + 0.5));
	return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float dFloor(vec3 p)
{
	return p.y + 1.0;
}

float sdColumn( vec3 p, vec3 c )
{
	float cyl = length(p.xz-c.xy)-c.z;// + abs(p.y);
	cyl -= cos(p.y*2.0)*0.045;
	float a = atan(p.x - c.x, p.z - c.y);
	a /= 2.0*PI;
	float subs = 48.0;
	a *= subs;
	//cyl *= pow(sin(a), 0.5) * 0.925 + 1.0;
	cyl += abs(sin(a)) * 0.015;

	cyl = max(cyl, p.y - 2.4);
	cyl = min(cyl, dBox(p + vec3(0.0, 1.0, 0.0), vec3(0.3, 0.2, 0.3)));
	cyl = min(cyl, dBox(p + vec3(0.0, -2.3, 0.0), vec3(0.3, 0.15, 0.3)));
	return cyl;
}

float sdCapsule( vec3 p, vec3 a, vec3 b, float r )
{
    vec3 pa = p - a, ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h ) - r;
}

float sdTorus( vec3 p, vec2 t )
{
	p.y += 1.0;
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	return length(q)-t.y;
}
float length8(vec2 v)
{
	return pow((pow(v.x,8.0) + pow(v.y, 8.0)), (1.0/8.0));
}
float length8(vec3 v)
{
	return pow((pow(v.x,8.0) + pow(v.y, 8.0) + pow(v.z, 8.0)), (1.0/8.0));
}
float sdTorus82( vec3 p, vec2 t, vec3 center, float subs )
{
	p -= center;
	float a = atan(p.x, p.z);
	a = pow(abs(sin(a*subs)), 0.25);
	//a = mod(a,PI*2.0) - 0.5;
	//a = a *0.025 + 0.975;
	a = a *0.2 + 0.8;
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	return length8(q)-t.y*a;
}
float sdTorusArch( vec3 p, vec2 t, vec3 center, float subs )
{
	p -= center;
	float a = atan(p.y, p.z);
	a = pow(abs(sin(a*subs)), 0.25);
	//a = mod(a,PI*2.0) - 0.5;
	//a = a *0.025 + 0.975;
	a = a *0.25 + 0.75;
	vec2 q = vec2(length(p.yz)-t.x,p.x);
	return length8(q)-t.y*a;
}
float sdTorusDome( vec3 p, vec2 t, vec3 center, float subs )
{
	p -= center;
	float a = atan(p.x, p.z);
	a = pow(abs(sin(a*subs)), 0.15);
	//a = mod(a,PI*2.0) - 0.5;
	//a = a *0.025 + 0.975;
	a = a *0.25 + 0.75;
	vec2 q = vec2(length(p.xz)-t.x,p.y);
	return length8(q)-t.y*a;
}

float sdHexPrism( vec3 p, vec2 h )
{
	p.y += 0.5;
    vec3 q = abs(p);
    return max(q.y-h.y,max(q.x+q.z*0.57735,q.z*1.1547)-(h.x*(2.35 - p.y)));
}
float sdHexPrismGem( vec3 p, vec2 h )
{
    vec3 q = abs(p);
    return max(q.y-h.y,max(q.x+q.z*0.57735,q.z*1.1547)-(h.x*(0.5 - abs(p.y))*3.0));
}

float dGem(vec3 p)
{
	float final = sdHexPrism(p, vec2(0.25, 1.0));
	return final;
}

float matMin(float a, float b, float matNum)
{
	float final = smin(a, b, 0.2);
	if (a < b)
	{
		//material = 0.0;
		return final;
	}
	else
	{
		material = matNum;
		return final;
	}
}

float dTiles(vec3 p)
{
	float subs = 16.0;
	float final = length(p) - 2.2;
	float a = atan(p.x, p.z);
	a /= 2.0*PI;
	a *= subs;
	a = abs((fract(a) - 0.5))*2.0;	// triangle wave from 0.0 to 1.0
	a -= 0.15;
	a *= 6.0;
	a = max(0.0, a);
	a = min(0.75, a);

	float b = atan(length(p.xz), p.y);
	b /= 2.0*PI;
	b *= subs;
	b = abs((fract(b) - 0.5))*2.0;	// triangle wave from 0.0 to 1.0
	b -= 0.15;
	b *= 6.0;
	b = max(0.0, b);
	b = min(0.75, b);
	
	a = a*b;

	a = a *0.2 + 0.8;
	b = b *0.2 + 0.8;
	
	final = final - a;
	final = max(final, 0.5-p.y);
	return final/1.414;
}

float GemCut(vec3 p)
{
	float size = 0.5;
	float f = length(p) - size;
	if (f <= 1.0)
	{
		marchingMultplier = 0.65;
		f = max(f, p.y - size * 0.25);
		
		f = max(f, p.y + p.x - size * 0.7);
		f = max(f, p.y - p.x - size * 0.7);
		f = max(f, p.y + p.z - size * 0.7);
		f = max(f, p.y - p.z - size * 0.7);
		
		f = max(f, -p.y + p.x - size * 0.6);
		f = max(f, -p.y - p.x - size * 0.6);
		f = max(f, -p.y + p.z - size * 0.6);
		f = max(f, -p.y - p.z - size * 0.6);

		f = max(f, p.y + p.x + p.z - size * 0.95);
		f = max(f, p.y - p.x + p.z - size * 0.95);
		f = max(f, p.y - p.x - p.z - size * 0.95);
		f = max(f, p.y + p.x - p.z - size * 0.95);

		f = max(f, -p.y + p.x + p.z - size * 0.85);
		f = max(f, -p.y - p.x + p.z - size * 0.85);
		f = max(f, -p.y - p.x - p.z - size * 0.85);
		f = max(f, -p.y + p.x - p.z - size * 0.85);
	} else marchingMultplier = 1.0;
	return f;
}

float atrium(vec3 p)
{
	vec3 c = vec3(1.0, 1.0, 1.0)* 4.0;
	float c2 = 5.2;
	vec3 q = mod(p,c)-0.5*c;
	float q2 = mod(p.x,c2)-0.5*c2;
	vec3 p2 = vec3(q.x, p.y, q.z);
	vec3 p3 = vec3(q2, p.y, p.z);

	float final = -sdCapsule(p, vec3(0.0,-0.5,0.0), vec3(0.0,2.25,0.0), 3.0);
	// This if condition is for a culling speedup and a cool bevel effect on the ceiling tiles.
	if (final < 0.01) final = max(final, -dTiles(p + vec3(0.0, -2.25, 0.0)));
	final = min(final, sdTorus82(p, vec2(2.75, 0.25), vec3(0.0, -0.795, 0.0), 12.0));
	final = max(final, -sdCapsule(p, vec3(-6.0,0.0,0.0), vec3(6.0,0.0,0.0), 2.0));
	//final = max(final, -sdCapsule(p, vec3(0.0,0.0,-16.0), vec3(0.0,0.0,16.0), 2.0));
	final = max(final, -dBoxSigned(p));
	final = max(final, -sdCapsule(p, vec3(0.0,0.0,0.0), vec3(0.0,5.5,0.0), 0.5));
	final = max(final, p.y - 5.3);
	//final = max(final, sdCapsule(p, vec3(0.0,-0.5,0.0), vec3(0.0,0.5,0.0), 3.05));
	//final = max(final, -dSphere(p2, 0.08));
	final = min(final, sdColumn(p2, vec3(0.0, 0.0, 0.25)));

	final = min(final, sdTorus82(p, vec2(3.0, 0.25), vec3(0.0, 2.7, 0.0), 8.0));
	final = min(final, sdTorus82(p, vec2(0.75, 0.25), vec3(0.0, -1.0, 0.0), 6.0));
	final = min(final, sdTorusArch(p3, vec2(2.125, 0.3), vec3(0.0, -0.1, 0.0), 6.0));
	//final = min(final, sdTorusArch(p, vec2(2.125, 0.3), vec3(2.6, -0.1, 0.0), 6.0));
	//final = min(final, sdTorusArch(p, vec2(2.125, 0.3), vec3(-2.6, -0.1, 0.0), 6.0));
	final = min(final, dFloor(p));
	material = 0.0;
	final = matMin(final, sdHexPrism(p, vec2(0.25, 1.0)), 2.0);

	//final = min(final, dGem(p));
	//final = matMin(final, sdHexPrismGem(p - vec3(0, 1.0, 0), vec2(0.25, 0.5)), 1.0);
	final = matMin(final, GemCut(p - vec3(0, 0.8, 0)), 1.0);

	//vec4 texX = texture2D(iChannel1, p.yz*0.2);
	//vec4 texY = texture2D(iChannel1, p.xz*0.2);
	//vec4 texZ = texture2D(iChannel1, p.xy*0.2);
	//vec4 noise = texX + texY + texZ;
	//final += noise.x * 0.05;

	return final;
}

float DistanceToObject(vec3 p)
{
	float tubeWidth = 0.15;
	//p = RotateY(p, p.y);
	p = RotateY(p, p.y);
	vec3 rp = p;
	vec3 lp = p;
	rp.x += (sin(rp.y+iGlobalTime)*0.5+0.5);
	lp.x += -(sin(rp.y+iGlobalTime)*0.5+0.5);
	float final = sdCapsule(lp, vec3(1.0,-5.5,0.0), vec3(1.0,5.5,0.0), tubeWidth);
	final = min(final, sdCapsule(rp, vec3(-1.0,-5.5,0.0), vec3(-1.0,5.5,0.0), tubeWidth));
	float cl = 0.5;
	float ql = mod(rp.y,cl)-0.5*cl;
	vec3 pl = vec3(rp.x, ql, rp.z);
	vec3 pr = vec3(lp.x, ql, lp.z);
	final = matMin(final, sdCapsule(pl, vec3(-1.0,0.0,0.0), vec3(0.0,0.0,0.0), tubeWidth), 1.0);
	final = matMin(final, sdCapsule(pr, vec3(-0.0,0.0,0.0), vec3(1.0,0.0,0.0), tubeWidth), 2.0);

	return final;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy/iResolution.xy * 2.0 - 1.0;// - 0.5;

	// Camera up vector.
	vec3 camUp=vec3(0,1,0); // vuv

	// Camera lookat.
	vec3 camLookat=vec3(0,1.0,0);	// vrp

	float mx=iMouse.x/iResolution.x*PI*2.0 + iGlobalTime * 0.5;
	float my=-iMouse.y/iResolution.y*10.0 + sin(iGlobalTime * 0.93)*0.32+0.02;//*PI/2.01;
	vec3 camPos=vec3(cos(my)*cos(mx),sin(my),cos(my)*sin(mx))*(3.75); 	// prp

	// Camera setup.
	vec3 camVec=normalize(camLookat - camPos);//vpn
	vec3 sideNorm=normalize(cross(camUp, camVec));	// u
	vec3 upNorm=cross(camVec, sideNorm);//v
	vec3 worldFacing=(camPos + camVec);//vcv
	vec3 worldPix = worldFacing + uv.x * sideNorm * (iResolution.x/iResolution.y) + uv.y * upNorm;//scrCoord
	vec3 relVec = normalize(worldPix - camPos);//scp

	float sunSpeed = -0.77;
	vec3 sunDir = normalize(vec3(sin(iGlobalTime*sunSpeed)*2.0, -3.0, sin(iGlobalTime*sunSpeed)*2.0));

	float dist = 0.02;
	float t = 0.1;
	float maxDepth = 40.0;
	vec3 pos = vec3(0,0,0);
	// ray marching time
	for (int i = 0; i < 150; i++)
	{
		if ((t > maxDepth) || (abs(dist) < 0.001)) continue;	// break DOESN'T WORK!!! ARRRGGG!
		material = 0.0;
		pos = camPos + relVec * t;
		dist = DistanceToObject(pos);
		t += dist * 0.5;	// because deformations mess up distance function.
	}
	float finalMaterial = material;

	//vec3 finalColor = vec3(0.0,0.0,0.0);// GetSunColorReflection(relVec, -sunDir) + vec3(0.1, 0.1, 0.1);
	vec3 finalColor = GetSunColorStupid(relVec, -sunDir) + vec3(0.1, 0.1, 0.1);

	//finalColor += texture2D(iChannel0, vec2(pos.x+iGlobalTime, pos.y*0.01+0.0)).xyz;
	vec3 smallVec = vec3(0.0025, 0, 0);
	vec3 normal = vec3(dist - DistanceToObject(pos - smallVec.xyy),
					   dist - DistanceToObject(pos - smallVec.yxy),
					   dist - DistanceToObject(pos - smallVec.yyx));
	normal = normalize(normal);
	float ambient = DistanceToObject(pos + normal * 1.0)*0.5;
	ambient += DistanceToObject(pos + normal * 0.1)*5.0;
	ambient = max(0.1, pow(ambient, 0.5));	// tone down ambient with a pow and min clamp it.
	vec4 texX = texture2D(iChannel0, pos.yz*0.75);
	vec4 texY = texture2D(iChannel0, pos.xz*0.75);
	vec4 texZ = texture2D(iChannel0, pos.xy*0.75);
	vec4 tex = mix(texX, texZ, abs(normal.z));
	tex = mix(tex, texY, abs(normal.y));//.zxyw;
	//tex = tex * tex;
	tex.xyz = mix(tex.xyz, vec3(1.0,1.0,1.0), 0.97);
	float wave = sin(pos.y+iGlobalTime)*0.5+0.5;
	wave = 1.0 - saturate(pow(wave, 0.25));
	if (finalMaterial == 1.0)
	{

		tex.xyz = mix(vec3(1.0, 0.2, 0.2), tex.xyz, wave);
		tex.xyz += vec3(1.0, 0.5, 0.5) * wave*16.0;
	//	tex.xyz += wave;
	}
	if (finalMaterial == 2.0)
	{
		tex.xyz = mix(vec3(0.2, 1.1, 0.2), tex.xyz, wave);
		tex.xyz += vec3(1.0, 0.5, 0.5) * wave*16.0;
	}
	vec3 ref = reflect(relVec, normal);
	if (t <= maxDepth)
	{
		vec3 envLight = mix(environmentGroundColor, environmentSphereColor * 1.0, (normal.y * 0.5 + 0.5));
			// calculate the reflection vector for highlights

		vec3 sunRef = GetSunColorReflection(ref, -sunDir)* sunCol * max(0.0, dot(normal, -sunDir));
		vec3 sunDirect = max(0.0, dot(-sunDir, normal)) * sunCol * 1.0;
		finalColor += (envLight) * tex.xyz;
		finalColor *= vec3(1.0,1.0,1.0) * ambient;
		finalColor += (sunDirect) * tex.xyz;// * ambient;
		finalColor += sunRef;
		//finalColor = mix(finalColor, vec3(0.015,0.015,0.015), pow(saturate(distance(pos, camPos)*0.075), 0.7) );
	}

	fragColor = vec4(sqrt(clamp(finalColor*1.0, 0.0, 1.0)),1.0);
}
