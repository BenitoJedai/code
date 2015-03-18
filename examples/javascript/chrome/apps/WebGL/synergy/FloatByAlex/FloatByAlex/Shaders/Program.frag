#define MAX_STEPS 64
#define EPSILON .001

#define M_PI 3.14159265358979

const vec3 LightSource = vec3(2., 1.5, 0);

vec3 WaterColor = vec3(0.4, 0.9, 1);

const float WaterHeight = 0.0;
const float MaxWaveAmplitude = 0.04;

const float HeightPool = 1.;
const float HalfSizePool = 3.;
const float DepthPool = 2.;

struct MaterialInfo {
	vec3 Kd;
	float Shininess;
};

float cyclicTime = mod(iGlobalTime, 30.);

float WaveAmplitude() {
	return MaxWaveAmplitude * exp(-cyclicTime / 10.);
}

float WaterWave(vec3 a) {
	return WaveAmplitude() * sin((2. * a.x * a.x + 2. * a.z * a.z) - 10. * cyclicTime);
}

float BallOscillation() {
	return sin(5. * cyclicTime + 4.) * exp(-cyclicTime / 6.) + 0.3;
}

float PoolBottom(vec3 a) {
	return a.y + DepthPool + .01;
}

float BackWall(vec3 a) {
	return a.z + HalfSizePool + .01;
}

float LeftWall(vec3 a) {
	return a.x + HalfSizePool + .01;
}

float WaterSurface(vec3 a) {
	vec3 sz = vec3(HalfSizePool, 0, HalfSizePool);
	return length(max(abs(a + vec3(0, WaterWave(a), 0)) - sz, 0.));
}

float Pool(vec3 a) {
	return min(PoolBottom(a), min(LeftWall(a), BackWall(a)));
}

float Ball(vec3 a) {
	return length(a + vec3(0., BallOscillation(), 0.)) - 0.75;
}

float Scene(vec3 a) {
	return min(WaterSurface(a), min(Ball(a), Pool(a)));
}

bool IsWaterSurface(vec3 a)
{
	float closest = Ball(a);
	float sample = Pool(a);
	if (sample < closest) {
		closest = sample;
	}	
	sample = WaterSurface(a);
	if (sample < closest) {
		return true;
	}
	return false;
}

bool IsWater(vec3 pos)
{
	return (pos.y < (WaterHeight - MaxWaveAmplitude));
}

vec3 PoolColor(vec3 pos) {		
	if ((pos.y > HeightPool) || (pos.x > HalfSizePool) || (pos.z > HalfSizePool)) 
		return vec3(0.0);
	float tileSize = 0.2;
	float thickness = 0.015;
	vec3 thick = mod(pos, tileSize);
	if ((thick.x > 0.) && (thick.x < thickness) || (thick.y > 0.) && (thick.y < thickness) || (thick.z > 0.) && (thick.z < thickness))
		return vec3(1);
	return vec3(sin(floor((pos.x + 1.) / tileSize)) * cos(floor((pos.y + 1.) / tileSize)) * sin(floor((pos.z + 1.) / tileSize)) + 3.);
}

MaterialInfo Material(vec3 a) {
	MaterialInfo m = MaterialInfo(vec3(.5, .56, 1.), 50.);
	float closest = Ball(a);

	float sample = WaterSurface(a);
	if (sample < closest) {
		closest = sample;
		m.Kd = WaterColor;
		m.Shininess = 120.;
	}
	sample = Pool(a);
	if (sample < closest) {
		closest = sample;
		m.Kd = PoolColor(a);		
		m.Shininess = 0.;
	}
	return m;
}

vec3 Normal(vec3 a) {
	vec2 e = vec2(.001, 0.);
	float s = Scene(a);
	return normalize(vec3(
		Scene(a+e.xyy) - s,
		Scene(a+e.yxy) - s,
		Scene(a+e.yyx) - s));
}

float Occlusion(vec3 at, vec3 normal) {
	float b = 0.;
	for (int i = 1; i <= 4; ++i) {
		float L = .06 * float(i);
		float d = Scene(at + normal * L);		
		b += max(0., L - d);
	}
	return min(b, 1.);
}

vec3 LookAt(vec3 pos, vec3 at, vec3 rDir) {
	vec3 f = normalize(at - pos);
	vec3 r = cross(f, vec3(0., 1., 0.));
	vec3 u = cross(r, f);
	return mat3(r, u, -f) * rDir;
}

float Trace(vec3 rPos, vec3 rDir, float distMin) {
	float L = distMin;
	for (int i = 0; i < MAX_STEPS; ++i) {
		float d = Scene(rPos + rDir * L);
		L += d;
		if (d < EPSILON * L) break;
	}
	return L;
}

vec3 Lighting(vec3 at, vec3 normal, vec3 eye, MaterialInfo m, vec3 lColor, vec3 lPos) {
	vec3 lDir = lPos - at;
	
	vec3 lDirN = normalize(lDir);
	float t = Trace(at, lDirN, EPSILON*2.);
	if (t < length(lDir)) {
		vec3 pos = at + lDirN * t;
		if(!IsWaterSurface(pos))
			return vec3(0.);
	}
	vec3 color = m.Kd * lColor * max(0., dot(normal, normalize(lDir)));
	
	if (m.Shininess > 0.) {
		vec3 h = normalize(normalize(lDir) + normalize(eye - at));
		color += lColor * pow(max(0., dot(normal, h)), m.Shininess) * (m.Shininess + 8.) / 25.;
	}
	return color / dot(lDir, lDir);
}

vec3 Shade(vec3 rpos, vec3 rdir, float t)
{
	vec3 pos = rpos + rdir * t;
	vec3 nor = Normal(pos);
	
	bool waterSurface = IsWaterSurface(pos);
	bool water = IsWater(pos);
	vec3 waterSurfaceLight = vec3(0);;
	if (waterSurface)
	{
		vec3 refractionDir = refract(normalize(rdir), nor, 0.9);

		waterSurfaceLight = Lighting(pos, nor, rpos, Material(pos), vec3(1.), LightSource);

		float wt = Trace(pos, refractionDir, 0.03);		
		pos += refractionDir * wt;
		nor = Normal(pos);
	}
	MaterialInfo mat = Material(pos);

	vec3 color = .11 * (1. - Occlusion(pos, nor)) * mat.Kd;

	color += Lighting(pos, nor, rpos, mat, vec3(1.), LightSource);
	
	if (water || waterSurface) {
		color *= WaterColor;
		if (waterSurface)
			color += waterSurfaceLight;
	}
	return color;
}

vec3 Camera(vec2 px) {
	vec2 uv = px.xy / iResolution.xy * 2. - 1.;	
	uv.x *= iResolution.x / iResolution.y;
	vec3 rayStart = vec3(3.5, 1.7, 6.);
	vec3 rayDirection = LookAt(rayStart, vec3(0, -1, 0), normalize(vec3(uv, -2.)));
	
	float path = Trace(rayStart, rayDirection, 0.);	
	return Shade(rayStart, rayDirection, path);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
	vec3 col = Camera(fragCoord.xy);
	fragColor = vec4(col, 0.);
}