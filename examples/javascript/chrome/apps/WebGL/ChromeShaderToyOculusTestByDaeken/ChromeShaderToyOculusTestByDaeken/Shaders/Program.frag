/* Compiled with Shaderforth: https://github.com/daeken/Shaderforth
import[ noise ]

:globals
	true =>shadertoy
		{
			@vec3 uniform =iResolution @float uniform =iGlobalTime
			iResolution .xy =>resolution
			iGlobalTime =>time
		}
		{ @vec2 uniform =resolution @float uniform =time }
	shadertoy cif
	@float =scene-noise
;

:m noise-2d pnoise-2d ;
:m terrain ( p )
	20 2 p .x 6 / abs 0 1 clamp mix =>factor
		p .xz 2 / noise-2d factor /
		p .y neg
	-
;

:m ring-terrain ( p )
	[
		[
			p [ 0 .6 0 ] - [ 1 p .y - .05 * 1 - 2.5 2 ] / [ 1 .1 ] torus
			p [ 0 4.8 0 ] + 5 sphere
		] 1.25 unionsmooth
		p terrain
	] .2 unionsmooth
;

:m walkway ( p )
	{ ( $rp )
		[ rp .xz p rp - .x.z + 13.7 * sin .5 * rotate-2d rp .y ] .xzy [ .4 .1 .4 ] box
	} p [ 0 1 0 ] + [ 0 0 1 ] repeat
;

:m portal ( p )
	[
		p [ 0 .3 0 ] + [ 1 2.5 1 ] / 1 sphere
		p [ 1 3 .04 ] box
	] intersect
;

:m columns ( p )
	p .x sign neg =>flip
	{ ( $rp )
		[
			[ rp .xz rp .y flip * pi 2 / * pi 8 / + rotate-2d rp .y ] .xzy [ 0 1 0 ] + [ .2 1.5 .2 ] box
			rp [ 0 1.25 0 ] - .4 sphere
		] .75 unionsmooth
	} p [ 2.25 0 2.5 ] repeat
;

:m hell-terrain ( p )
	p [ 0 p .z 4 / 0 ] + [ 0 1 0 1 ] plane scene-noise p .x time 1 + 2 * + sin .1 * + -.3 .6 clamp .4 * +
;

: scene ( p:vec3 -> vec2 )
	{ ( rp )
		[ rp [ .25 .25 .25 ] box 2 ]
	} p [ 2 2 2 ] repeat
	( p .x 100 / round =room
	p [ room 100 * 0 0 ] - =p
	room 1 eq =heaven
	room 2 eq =>hell
		p .xz [ 1.5 1 ] *
		[ 0 time neg 1.5 / heaven mix 0 time heaven hell + mix 1 .3 heaven mix * ] - 
		p .y +
			.75
			3 room -
		room 1 - sign abs mix
	* noise-2d =scene-noise
	[
		room 0 == {
			[
				[ p [ 0 1 0 ] + ring-terrain 1 ]
				[ p walkway 2 ]
				[ p portal 3 ]
			] hitunion
		}
		room 1 == {
			p [ 1 p .z -.04 * + 1 1 ] * [ 0 p .z .05 * 0 ] + =p
			[
				[
					[
						p [ 0 1 0 ] + [ 0 1 0 0 ] plane
						p columns
					] .75 unionsmooth
					p [ 0 0 far .2 + ] - [ 1.5 far far ] box
				] intersect
				4
			]
		}
		{
			[ p hell-terrain 5 ]
		}
	] cond =>temp
	room 1 - sign abs temp .y 3 - sign abs * =>is-portal
	temp [ scene-noise .03 * is-portal * 0 ] + )
;

:m portal-texture ( p )
		[
			##000
			.5 .5 0
			p .z sign 1 + 2 / 1 +
			0
		] material
;

:m marble-texture ( p )
	scene-noise abs =>noise
	##DBDBDB ##4F5052 noise mix =>color
	[ p .y .2 - 0 1 clamp 2 ** color - abs .3 .7 noise 100 * eps + 0 0 ] material
;

:m hell-texture ( p )
	[ ##525252 .05 .5 10 0 0 ] material
;

: get-material ( pos:vec3 dist:float id:float -> material )
	[
		id 1 == {
			pos .y .6 + 0 1 clamp =fade
			[ ##7A3B14 ##878787 fade mix .4 .7 1000 10 fade mix 0 -.3 ] material
		}
		id 2 == { [ ##DBDBDB ##525252 pos .z sign 0 1 clamp mix .3 .7 1000 0 0 ] material }
		id 3 == { pos portal-texture }
		id 4 == { pos marble-texture }
		{ pos hell-texture }
	] cond
;

:m union \min ;
:m unionsmooth ( list k ) list \{ ( a b ) a b k smin-poly } ;
:m hitunion \{ ( $a $b ) a b a .x b .x < select } ;
:m hitunionsmooth ( list k )
	list \{ ( $a $b )
		a .x b .x k smin-poly =smin
		[
			smin
			a .y b .y a .x smin - abs b .x smin - abs < select
		]
	}
;
:m subtract \{ ( d1 d2 ) d1 neg d2 max } ;
:m intersect \max ;
:m hitintersect \{ ( $a $b ) a b a .x b .x > select } ;
:m repeat ( block p c ) p p c mod c 1000 * 0 1 clamp mix 0.5 c * - *block ;

:m sphere ( p s ) p length s - ;
:m torus ( $p t ) [ p .xy length t .x - p .z ] length t .y - ;
: box ( p:vec3 b:vec3 -> float )
	p abs b - =d
	d \max 0 min
	d 0 max length +
;
:m plane ( p n )
	p n .xyz dot n .w +
;

:struct material
	@vec3 =color
	@float =ambient
	@float =diffuse
	@float =specular
	@float =room
	@float =normal-mod
;

:struct hit
	@vec3 =pos
	@vec3 =origin
	@vec3 =dir
	@float =dist
	@float =total-dist
	@material =mat
;

:m tx [ _ 0 0 ] + ;
:m ty [ 0 _ 0 ] + ;
:m tz [ 0 0 _ ] + ;
:m get-normal ( p )
	[
		p eps     tx scene .x
		p eps neg tx scene .x -
		p eps     ty scene .x
		p eps neg ty scene .x -
		p eps     tz scene .x
		p eps neg tz scene .x -
	] normalize
;

:m lightpos [ 0 room 100 * + 5 5 -1 room 1 - abs mix ] ;

:m shadow-dist 0.1 ;

: get-shadow ( ray:vec3 lpos:vec3 -> float )
	lpos ray - normalize =dir
	shadow-dist 10 * =dist
	lpos ray - length =mdist
	far =lowest
	{
		ray dir 2 * dist * + scene =cur
		lowest cur .x min =lowest
		dist cur .x abs shadow-dist max + =dist
		&break cur .x abs shadow-dist < dist mdist >= or when
	} 3 times
	lowest 0 max 0 shadow-dist .8 ** smoothstep
;

: ambient-occlusion ( pos:vec3 dir:vec3 cdist:float -> float )
	pos dir cdist * + =>npos
	npos scene .x far min =>lowest
	0 lowest cdist 1.75 / smoothstep
;

: apply-fog ( color:vec3 cur:hit dist:float miss:float -> vec3 )
		color
		cur .origin 1 miss - * cur .dir sky
		dist cur .total-dist miss mix far fog - - fog / fog-factor * 0 1 clamp
	mix
;

: shade ( cur:hit -> vec3 )
	cur .pos =>pos
	pos .x 100 / round =room
	pos get-normal =normal

	lightpos =lpos
	lpos pos - normalize =ivec
	ivec normal dot 0 max =incidence
	incidence =>diffuse
	pos normal cur .dist ambient-occlusion =>ambient

	cur .mat =>mat

	0 =specular
	{
			ivec cur .origin + normalize normal dot
			0
		max mat .specular pow
		lpos pos - length / =specular
	} mat .specular 0 != incidence 0 > and when

	cur .pos lpos get-shadow =shadow

			mat .color rgb->hsv1
				[ 1 1
					diffuse mat .diffuse * shadow *
					ambient mat .ambient * +
					specular shadow * +
					normal .x abs mat .normal-mod * +
				]
		* hsv1->rgb
	cur cur .origin.pos - length 0 apply-fog
;

:m nullhit
	[ ray ray dir far tdist [ 0 vec3 0 0 0 0 0 ] material ] hit
;

: march ( ray:vec3 dir:vec3 -> hit )
	close =t
	0 =tdist
	2 =>magic-factor
	close magic-factor * =minstep
	far =last
	0 =room
	{
		ray dir t * + scene =cur
		{ nullhit return } cur .x far > when
		{
				{
					t tdist + =tdist
					0 =t
					ray 0 3 get-material =mat
					mat .room =room
					ray [ room 100 * 0 0 ] + =ray
				}
				&break
			cur .y 3 == if
		}
		{
			t cur .x abs minstep + + =t
			{ nullhit return } t far > when
			{
				minstep close magic-factor / 3 * + =minstep
			} cur .x last >= room 0 == and when
			cur .x =last
		} cur .x close close magic-factor / t * + <= if
	} 40 times

	ray dir t cur .x + * + =fray

	[
		fray
		ray
		dir
		cur .x
		tdist
		fray cur .x.y get-material
	] hit
;

:m cut-horizon
	##000 1 dir .y - 0 1 clamp 2 ** mix
;

:m clouds ( dir )
	dir .xy cart->polar [ 3 7 ] * [ 0 time ] + noise-2d .2 .8 clamp .2 - vec3 cut-horizon
;

: sky ( origin:vec3 dir:vec3 -> vec3 )
	origin .x 100 / round =>room
	[
		0	##6010C9 ##111438 dir .y 2.5 * mix
		1	##FCFFD9 ##9CC5FF dir .y 6 * mix
			##AB1529 ##360B11 dir .y 5 * mix
	] room choose
;

2 =>focus
:m far 20 ;
:m fog 14 ;
:m fog-factor 1.5 ;
:m close 0.005 ;

: camera-turn ( p:vec3 -> vec3 )
	time 8 - 0 max 3.5 / =v
	[ p .xz v 0 1 clamp 1.2 ** v 1 - 0 max + neg rotate-2d p .y ] .xzy
;

: Kt ( i:int -> float )
	[
			[ 1.003000 1.020000 1.042000 1.066000 1.094000
			  1.126000 1.162000 1.203000 1.250000 1.310000 ]
		enumerate /{ =>[ k v ] i k int == { v return } } flatten
		{ 1.380000 return }
	] cond
;
:m K[] int Kt ;

: catmull-rom ( r:float -> float )
	11 =>num_segments
	[ 10 r 3.6 ] \* =scaled_val
	0 10 scaled_val floor min max =scaled_val_floor
	scaled_val_floor int =k
    1 =k0p0
    1 K[] 0 K[] - =k0m0
    1 K[] =k0p1
    2 K[] 0 K[] - .5 * =k0m1

    k K[] =p0
    k #1 + K[] k #1 - K[] - .5 * =m0
    k #1 + K[] =p1
    k #2 + K[] k K[] - .5 * =m1

    k #0 == float =k0

    k0 k0p0 * 1 k0 - p0 * + =p0
    k0 k0p1 * 1 k0 - p1 * + =p1
    k0 k0m0 * 1 k0 - m0 * + =m0
    k0 k0m1 * 1 k0 - m1 * + =m1

    scaled_val scaled_val_floor - =t
    1 t - =omt
	    p0 1 2 t * + * m0 t * + omt !* *
	    p1 1 2 omt * + * m1 omt * - t !* *
    +
;

resolution frag->position =pos
pos .x sign neg .1 * =eyeoff
resolution .x.y / =rs
true =>occlude-lens
[ pos .x rs + rs mod rs 2 / - pos .y ] =eyepos
eyepos [ 1 resolution .y.x 2 / / ] / 2 / =eyepos
eyepos !dot =radsq
eyepos radsq 1.5 * catmull-rom * =eyepos
eyepos 2 * [ 1 resolution .y.x 2 / / ] * =eyepos

1 time 10 / - 0 1 clamp 3 ** 100 * =>running
[ 0 .5 -4 running - time 10 - 0 max 4 / sin .1 * + ] [ eyeoff 0 0 ] + camera-turn =origin
[ 0 .5 -1 ] camera-turn =>ct

ct origin - normalize =cd
[ 0 1 0 ] =>cu
cd cu cross =>cs
cs eyepos .x * cu eyepos .y * + cd focus * + normalize =>dir

:m fade-in ( color )
	##fff color time .5 - 2 / 0 1 clamp mix
;

	{
		origin dir march =cur
			{ cur shade fade-in ->fragcolor }
			{
					cur .origin.dir sky =cursky
						cursky
						cursky cur far 1 apply-fog
					cur .total-dist far * 1 min mix
				fade-in ->fragcolor
			}
		cur .dist close 10 * < if
	} { 0 vec3 ->fragcolor }
radsq .195 < occlude-lens not or if
*/

struct material {
	vec3 color;
	float ambient;
	float diffuse;
	float specular;
	float room;
	float normal_mod;
};
struct hit {
	vec3 pos;
	vec3 origin;
	vec3 dir;
	float dist;
	float total_dist;
	material mat;
};
float scene_noise;
material get_material(vec3 pos, float dist, float id) {
	material temp_1;
	if(id == 1.) {
		float fade = clamp(pos.y + .6, 0., 1.);
		temp_1 = material(mix(vec3(.47843, .23137, .078431), vec3(.52941, .52941, .52941), fade), .4, .7, mix(1000., 10., fade), 0., -.3);
	} else if(id == 2.) {
		temp_1 = material(mix(vec3(.85882, .85882, .85882), vec3(.32157, .32157, .32157), clamp(sign(pos.z), 0., 1.)), .3, .7, 1000., 0., 0.);
	} else if(id == 3.) {
		temp_1 = material(vec3(0., 0., 0.), .5, .5, 0., (sign(pos.z) + 1.) / 2. + 1., 0.);
	} else if(id == 4.) {
		temp_1 = material(abs(pow(clamp(pos.y - .2, 0., 1.), 2.) - mix(vec3(.85882, .85882, .85882), vec3(.30980, .31373, .32157), abs(scene_noise))), .3, .7, abs(scene_noise) * 100. + .00001, 0., 0.);
	} else {
		temp_1 = material(vec3(.32157, .32157, .32157), .05, .5, 10., 0., 0.);
	}
	return temp_1;
}
float Kt(int i) {
	if(i == 0) {
		return 1.003000;
	} else if(i == 1) {
		return 1.020000;
	} else if(i == 2) {
		return 1.042000;
	} else if(i == 3) {
		return 1.066000;
	} else if(i == 4) {
		return 1.094000;
	} else if(i == 5) {
		return 1.126000;
	} else if(i == 6) {
		return 1.162000;
	} else if(i == 7) {
		return 1.203000;
	} else if(i == 8) {
		return 1.250000;
	} else if(i == 9) {
		return 1.310000;
	} else {
		return 1.380000;
	}
}
vec3 sky(vec3 origin, vec3 dir) {
	return (floor(origin.x / 100. + .5) == 0. ? mix(vec3(.37647, .062745, .78824), vec3(.066667, .078431, .21961), dir.y * 2.5) : (floor(origin.x / 100. + .5) == 1. ? mix(vec3(.98824, 1., .85098), vec3(.61176, .77255, 1.), dir.y * 6.) : mix(vec3(.67059, .082353, .16078), vec3(.21176, .043137, .066667), dir.y * 5.)));
}
vec3 apply_fog(vec3 color, hit cur, float dist, float miss) {
	return mix(color, sky(cur.origin * (1. - miss), cur.dir), clamp((mix(dist, cur.total_dist, miss) - 6.) / 14. * 1.5, 0., 1.));
}
float pnoise_2d(vec2 v) {
	vec4 Pi = mod(floor(v.xyxy) + vec4(0., 0., 1., 1.), 289.);
	vec4 Pf = fract(v.xyxy) - vec4(0., 0., 1., 1.);
	vec4 gx = fract(mod(((mod((Pi.xzxz * 34. + 1.) * Pi.xzxz, 289.) + Pi.yyww) * 34. + 1.) * (mod((Pi.xzxz * 34. + 1.) * Pi.xzxz, 289.) + Pi.yyww), 289.) / 41.) * 2. - 1.;
	vec4 gy = abs(gx) - .5;
	gx -= floor(gx + .5);
	vec4 norm = inversesqrt(sqrt(gx * gx + gy * gy));
	vec2 fade_xy = ((Pf.xy * 6. - 15.) * Pf.xy + 10.) * pow(Pf.xy, vec2(2.));
	vec2 n_x = mix(vec2(dot(vec2(gx.x, gy.x) * norm.x, Pf.xy), dot(vec2(gx.z, gy.z) * norm.y, Pf.xw)), vec2(dot(vec2(gx.y, gy.y) * norm.z, Pf.zy), dot(vec2(gx.w, gy.w) * norm.w, Pf.zw)), fade_xy.x);
	return mix(n_x.x, n_x.y, fade_xy.y) * 2.3;
}
float box(vec3 p, vec3 b) {
	vec3 d = abs(p) - b;
	return min(max(max(d.x, d.y), d.z), 0.) + length(max(d, 0.));
}
vec2 rotate_2d(vec2 c, float a) {
	float ca = cos(a);
	float sa = sin(a);
	return vec2(c.x * ca - c.y * sa, c.y * ca + c.x * sa);
}
vec3 hsv1_rgb(vec3 hsv) {
	return mix(vec3(1., 1., 1.), clamp(abs(mod(hsv.x * 6. + vec3(0., 4., 2.), 6.) - 3.) - 1., 0., 1.), hsv.y) * hsv.z;
}
float smin_poly(float a, float b, float k) {
	float h = clamp(.5 + .5 * (b - a) / k, 0., 1.);
	return mix(b, a, h) - k * h * (1. - h);
}
vec2 scene(vec3 p) {
	return vec2(box(mix(p, mod(p, vec3(2., 2., 2.)), vec3(1., 1., 1.)) - vec3(1.0, 1.0, 1.0), vec3(.25, .25, .25)), 2.);
}
float get_shadow(vec3 ray, vec3 lpos) {
	vec3 dir = normalize(lpos - ray);
	float dist = 1.0;
	float mdist = length(lpos - ray);
	float lowest = 20.;
	for(int temp_5 = 0; temp_5 < 3; ++temp_5) {
		vec2 cur = scene(ray + dir * 2. * dist);
		lowest = min(lowest, cur.x);
		dist += max(abs(cur.x), .1);
		if(abs(cur.x) < .1 || dist >= mdist) {
			break;
		}
	}
	return smoothstep(max(lowest, 0.), 0., .15849);
}
hit march(vec3 ray, vec3 dir) {
	vec2 cur;
	float t = .005;
	float tdist = 0.;
	float minstep = .010;
	float last = 20.;
	float room = 0.;
	for(int temp_6 = 0; temp_6 < 40; ++temp_6) {
		cur = scene(ray + dir * t);
		if(cur.x > 20.) {
			return hit(ray, ray, dir, 20., tdist, material(vec3(0.), 0., 0., 0., 0., 0.));
		}
		if(cur.x <= (.005 + .0025 * t)) {
			if(cur.y == 3.) {
				tdist += t;
				t = 0.;
				material mat = get_material(ray, 0., 3.);
				room = mat.room;
				ray += vec3(room * 100., 0., 0.);
			} else {
				break;
			}
		} else {
			t += abs(cur.x) + minstep;
			if(t > 20.) {
				return hit(ray, ray, dir, 20., tdist, material(vec3(0.), 0., 0., 0., 0., 0.));
			}
			if(cur.x >= last && room == 0.) {
				minstep += .0075;
			}
			last = cur.x;
		}
	}
	vec3 fray = ray + dir * (t + cur.x);
	return hit(fray, ray, dir, cur.x, tdist, get_material(fray, cur.x, cur.y));
}
float ambient_occlusion(vec3 pos, vec3 dir, float cdist) {
	return smoothstep(0., min(scene(pos + dir * cdist).x, 20.), cdist / 1.75);
}
vec3 camera_turn(vec3 p) {
	float v = max(iGlobalTime - 8., 0.) / 3.5;
	return vec3(rotate_2d(p.xz, -(pow(clamp(v, 0., 1.), 1.2) + max(v - 1., 0.))), p.y).xzy;
}
vec3 rgb_hsv1(vec3 rgb) {
	vec4 K = vec4(0., -.33333, .66667, -1.);
	vec4 p = (rgb.g < rgb.b ? vec4(rgb.bg, K.wz) : vec4(rgb.gb, K.xy));
	vec4 q = (rgb.r < p.x ? vec4(p.xyw, rgb.r) : vec4(rgb.r, p.yzx));
	float d = q.x - min(q.w, q.y);
	return vec3(abs(q.z + (q.w - q.y) / (6. * d + .00001)), d / (q.x + .00001), q.x);
}
vec3 shade(hit cur) {
	float room = floor(cur.pos.x / 100. + .5);
	vec3 normal = normalize(vec3(scene(cur.pos + vec3(.00001, 0., 0.)).x - scene(cur.pos + vec3(-.00001, 0., 0.)).x, scene(cur.pos + vec3(0., .00001, 0.)).x - scene(cur.pos + vec3(0., -.00001, 0.)).x, scene(cur.pos + vec3(0., 0., .00001)).x - scene(cur.pos + vec3(0., 0., -.00001)).x));
	vec3 lpos = vec3(room * 100., 5., mix(5., -1., abs(room - 1.)));
	vec3 ivec = normalize(lpos - cur.pos);
	float incidence = max(dot(ivec, normal), 0.);
	float specular = 0.;
	if(cur.mat.specular != 0. && incidence > 0.) {
		specular = pow(max(dot(normalize(ivec + cur.origin), normal), 0.), cur.mat.specular) / length(lpos - cur.pos);
	}
	float shadow = get_shadow(cur.pos, lpos);
	return apply_fog(hsv1_rgb(rgb_hsv1(cur.mat.color) * vec3(1., 1., incidence * cur.mat.diffuse * shadow + ambient_occlusion(cur.pos, normal, cur.dist) * cur.mat.ambient + specular * shadow + abs(normal.x) * cur.mat.normal_mod)), cur, length(cur.origin - cur.pos), 0.);
}
float catmull_rom(float r) {
	float scaled_val = 10. * r * 3.6;
	float scaled_val_floor = max(0., min(10., floor(scaled_val)));
	int k = int(scaled_val_floor);
	float k0p0 = 1.;
	float k0m0 = Kt(1) - Kt(0);
	float k0p1 = Kt(1);
	float k0m1 = (Kt(2) - Kt(0)) * .5;
	float p0 = Kt(int(k));
	float m0 = (Kt(int(k + 1)) - Kt(int(k - 1))) * .5;
	float p1 = Kt(int(k + 1));
	float m1 = (Kt(int(k + 2)) - Kt(int(k))) * .5;
	float k0 = float(k == 0);
	p0 = k0 * k0p0 + (1. - k0) * p0;
	p1 = k0 * k0p1 + (1. - k0) * p1;
	m0 = k0 * k0m0 + (1. - k0) * m0;
	m1 = k0 * k0m1 + (1. - k0) * m1;
	float t = scaled_val - scaled_val_floor;
	float omt = 1. - t;
	return (p0 * (1. + 2. * t) + m0 * t) * omt * omt + (p1 * (1. + 2. * omt) - m1 * omt) * t * t;
}
void mainImage(out vec4 fragColor, in vec2 fragCoord) {
	vec2 pos = (fragCoord.xy / iResolution.xy * 2. - 1.) * vec2(iResolution.x / iResolution.y, 1.);
	float eyeoff = -sign(pos.x) * .1;
	float rs = iResolution.x / iResolution.y;
	vec2 eyepos = vec2(mod(pos.x + rs, rs) - rs / 2., pos.y);
	eyepos = eyepos / vec2(1., iResolution.y / (iResolution.x / 2.)) / 2.;
	float radsq = dot(eyepos, eyepos);
	eyepos *= catmull_rom(radsq * 1.5);
	eyepos = eyepos * 2. * vec2(1., iResolution.y / (iResolution.x / 2.));
	vec3 origin = camera_turn(vec3(0., .5, -4. - pow(clamp(1. - iGlobalTime / 10., 0., 1.), 3.) * 100. + sin(max(iGlobalTime - 10., 0.) / 4.) * .1) + vec3(eyeoff, 0., 0.));
	vec3 cd = normalize(camera_turn(vec3(0., .5, -1.)) - origin);
	if(radsq < .195 || false) {
		hit cur = march(origin, normalize(cross(cd, vec3(0., 1., 0.)) * eyepos.x + vec3(0., 1., 0.) * eyepos.y + cd * 2.));
		if(cur.dist < .050) {
			fragColor = vec4(mix(vec3(1., 1., 1.), shade(cur), clamp((iGlobalTime - .5) / 2., 0., 1.)), 1.);
		} else {
			vec3 cursky = sky(cur.origin, cur.dir);
			fragColor = vec4(mix(vec3(1., 1., 1.), mix(cursky, apply_fog(cursky, cur, 20., 1.), min(cur.total_dist * 20., 1.)), clamp((iGlobalTime - .5) / 2., 0., 1.)), 1.);
		}
	} else {
		fragColor = vec4(vec3(0.), 1.);
	}
}
