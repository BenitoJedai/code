
// **************************************************************************
// CONSTANTS

#define PI 3.14159
#define TWO_PI 6.28318

#define EPSILON 0.0001
#define BIG_FLOAT 1000000.

// Increase this to 5 to anti-alias and to warm up your GPU
#define NUM_AA_SAMPLES 1.

// **************************************************************************
// DEFINES

#define PLANE_MATL 1.
#define ELLINGTON_BODY_MATL 2.
#define ELLINGTON_SCLERA_MATL 3.
#define ELLINGTON_PUPIL_MATL 4.

// http://www.webonweboff.com/tips/js/event_key_codes.aspx
#define KEY_SHIFT ((16.0 + .5)/256.0)

// **************************************************************************
// GLOBALS

vec3  g_camPointAt   = vec3(0.);
vec3  g_camOrigin    = vec3(0.);

float g_time         = 0.;
vec4  g_debugcolor   = vec4(0.);

vec3  g_ellingtonLookingAt = vec3(0.);

// **************************************************************************
// UTILITIES

float close(float a, float b) { return step(abs(a-b), EPSILON); }

mat3 invert_mat3( mat3 m )
{
    mat3 invm = mat3(1.);
    float invd = 1./(m[0][0]*m[1][1]*m[2][2] - m[0][0]*m[2][1]*m[1][2] +
                     m[1][0]*m[2][1]*m[0][2] - m[1][0]*m[0][1]*m[2][2] +
                     m[2][0]*m[0][1]*m[1][2] - m[2][0]*m[1][1]*m[0][2]);

    // Needs optimization
    invm[0][0] = (m[1][1]*m[2][2] - m[2][1]*m[1][2]) * invd;
    invm[1][0] = - (m[1][0]*m[2][2] - m[2][0]*m[1][2]) * invd;
    invm[2][0] = (m[1][0]*m[2][1] - m[2][0]*m[1][1]) * invd;

    invm[0][1] = - (m[0][1]*m[2][2] - m[2][1]*m[0][2]) * invd;
    invm[1][1] = (m[0][0]*m[2][2] - m[2][0]*m[0][2]) * invd;
    invm[2][1] = - (m[0][0]*m[2][1] - m[2][0]*m[0][1]) * invd;

    invm[0][2] = (m[0][1]*m[1][2] - m[1][1]*m[0][2]) * invd;
    invm[1][2] = - (m[0][0]*m[1][2] - m[1][0]*m[0][2]) * invd;
    invm[2][2] = (m[0][0]*m[1][1] - m[1][0]*m[0][1]) * invd;

    return invm;
}

vec3 orient_to_y( vec3 p, vec3 v )
{
    // assume v is a normalized vector that we will use to
    // orient with the y-axis

    // if v is pointing exactly in the positive or negative
    // y direction, then we can return the positive or negative
    // identity respectively.
    if (close(abs(v.y), 1.) > .5) return sign(v.y) * p;

    vec3 v1 = v;
    vec3 up = vec3(0., 1., 0.); // assuming up vector in world is y
    vec3 v3 = normalize(cross(v1, up));
    vec3 v2 = cross(v3, v1);
    
    // Align the y-axis with the src vector
    mat3 m = mat3(v2, v1, v3);

    // invert the 3x3 matrix and apply it to p
	// TODO - this is overkill! - I know this matrix is 
	// orthogonal, just take the transpose
    return invert_mat3(m) * p;
}

vec2 merge_objs(vec2 a, vec2 b) { return mix(b, a, step(a.x, b.x)); }

float union_df(float a, float b) { return min(a, b); }
float inters_df(float a, float b) { return max(a, b); }
float diff_df(float a, float b) { return max(a, -b); }

// Periodic saw tooth function that repeats with a period of 
// 4 and ranges from [-1, 1].
// The function starts out at 0 for x=0,
//  rises to 1 for x=1,
//  drops to 0 for x=2,
//  continues to -1 for x=3,
//  and then rises back to 0 for x=4
// to complete the period

float sawtooth( float x )
{
    float xmod = mod(x+3.0, 4.);
    return abs(xmod-2.0) - 1.0;
}

// returns "very pseudo" random number between -1 and 1 based on seed
float noise1f( float seed ) { return sin(28.41 * seed); }

// returns power perlin noise between -1 and 1 based on seed. Increase
// p to accentuate the contrast between noise samples.
vec2 ppnoise2v( float seed, float p )
{
    float ni = floor( seed );
    float nf = fract( seed );
    nf = nf*nf*(3.0-2.0*nf);

    float x = ni;
    float y = 13. * ni;
    
    nf = pow(nf, p);
    return vec2(mix(noise1f(x + 0.), noise1f(x + 1.),  nf),
                mix(noise1f(y + 0.), noise1f(y + 13.), nf));
}

// **************************************************************************
// DISTANCE FIELDS


float sphere_df( vec3 p, float r ) { return length(p) - r; }
float plane_df( vec3 p, float o ) { return p.y - o; }

#define EVENT_DURATION 15.

vec2 ellington_df( vec3 pos ) 
{
    vec3 bodyp = pos;

    vec2 ellingtonobj = vec2(BIG_FLOAT, ELLINGTON_BODY_MATL);

    // ellington is enjoying himself
    bodyp = orient_to_y(bodyp, normalize(vec3(10. + .8 * cos(7. * g_time), 100., -0.1)));
        
    float event_t  = mod(g_time, EVENT_DURATION);
    float enter_t  = event_t + 0.1;
    float exit_t   = event_t - (EVENT_DURATION - 2.);
    
    // reflect in the yz plane
    bodyp.x += 1.25 * noise1f(.01 * floor(g_time/EVENT_DURATION));
    float bodyside = sign(bodyp.x);
    bodyp.x = abs(bodyp.x);    
    bodyp.y += .1;
    
    // squetch into screen
    float squash_stretch_enter = step(0., enter_t) * sin(14. * enter_t) * exp(-4.8 * enter_t);
    bodyp.y -= 1.7 * squash_stretch_enter - step(0., enter_t) * 5. * sin(14. * enter_t) * exp(-4. * enter_t);
    bodyp.y *= 1. + .8 * squash_stretch_enter;
    bodyp.x *= 1. - .9 * squash_stretch_enter;

    // squetch off screen
    float squash_stretch_exit = step(0., exit_t) * sin(10. * exit_t) * exp(-4. * exit_t);
    bodyp.y -= 1.7 * squash_stretch_exit + step(0., exit_t) * 10. * max(-1., -exit_t * exit_t);
    bodyp.y *= 1. - .5 * squash_stretch_exit;
    bodyp.x *= 1. + .5 * squash_stretch_exit;

    // shape ellington's body
    bodyp.xz *= vec2(.89, 1.);    
    ellingtonobj.x = sphere_df( bodyp, 1. );

    // place ellington's eyes
    vec2 eyeobj = vec2(BIG_FLOAT, ELLINGTON_SCLERA_MATL);
    vec3 eyep = bodyp - vec3(.42, -.05, .85);
    
    // orient the eyes 
    eyep = orient_to_y(eyep, normalize(vec3(-.14, .9, -.3))); 
    eyep.yz *= vec2(.48, .5);
    eyep.y *= -1.; eyep.y += .2;

    // shape ellington's sclera
    eyeobj.x = sphere_df( eyep, .2 );

    // shape ellington's pupils
    vec2 pupilobj = vec2(BIG_FLOAT, ELLINGTON_PUPIL_MATL);    
    vec3 pupildir = vec3(0.2 , 0.01, -.155); // neutral pupil position

    // have his eyes dart around
    // he's got a bit of a lazy eye since I'm too lazy to do this math
    // correctly.
    vec3 lookdir = g_ellingtonLookingAt - vec3(0., -.1, 0.);
    vec3 viewdir = g_camOrigin - g_camPointAt;
    pupildir += .065 * vec3(bodyside, 1., 1.) * (viewdir - lookdir);

    vec3 pupilp = eyep - .2 * normalize( pupildir );
    pupilobj.x = inters_df( sphere_df( pupilp, .06 ), sphere_df( eyep, .21 ) );

    // shape ellington's eye lids and time blinks
    vec2 lidsobj = vec2(BIG_FLOAT, ELLINGTON_BODY_MATL);  
    float bt = mod(g_time, 5.);
    float bdur = .26;
    float bstart = (5.-bdur) * (.5 * noise1f( floor(g_time/5.) ) + .5);
    float blink = -.22 + .43 * (step(bstart, bt) - step(bstart + bdur, bt)) * sin((PI/bdur) * (bt - bstart));
    lidsobj.x  = inters_df( sphere_df( eyep, .212 ),  plane_df( eyep, blink ) );

    // merge ellington all together
    ellingtonobj = merge_objs(ellingtonobj, eyeobj);
    ellingtonobj = merge_objs(ellingtonobj, pupilobj);
    ellingtonobj = merge_objs(ellingtonobj, lidsobj);

    return ellingtonobj;
}

vec2 plane_df( vec3 pos ) 
{
    return vec2(abs( pos.y + 1. ), PLANE_MATL);
}

// **************************************************************************
// SCENE MARCHING

vec2 scene_df( vec3 pos )
{
    vec2 obj = vec2(BIG_FLOAT, -1.);

    obj = merge_objs(obj, ellington_df( pos ));    
    obj = merge_objs(obj, plane_df( pos ));

    return obj;
}

#define DISTMARCH_STEPS 50
#define DISTMARCH_MAXDIST 50.

vec2 dist_march( vec3 ro, vec3 rd, float maxd )
{
    
    float epsilon = 0.001;
    float dist = 10. * epsilon;
    float t = 0.;
    float material = 0.;
    for (int i=0; i < DISTMARCH_STEPS; i++) 
    {
        if ( abs(dist) < epsilon || t > maxd ) continue;
        // advance the distance of the last lookup
        t += dist;
        vec2 dfresult = scene_df( ro + t * rd );
        dist = dfresult.x;
        material = dfresult.y;
    }

    if( t > maxd ) material = -1.0; 
    return vec2( t, material );
}

// **************************************************************************
// SHADOWING & NORMALS

#define SOFTSHADOW_STEPS 30
#define SOFTSHADOW_STEPSIZE .2

float soft_shadow( vec3 ro, 
                      vec3 rd, 
                      float mint, 
                      float maxt, 
                      float k )
{
    float shadow = 1.0;
    float t = mint;

    for( int i=0; i < SOFTSHADOW_STEPS; i++ )
    {
        if( t < maxt )
        {
            float h = scene_df( ro + rd * t ).x;
            shadow = min( shadow, k * h / t );
            t += SOFTSHADOW_STEPSIZE;
        }
    }
    return clamp( shadow, 0.0, 1.0 );

}

vec3 calc_normal( vec3 p )
{
    vec3 epsilon = vec3( 0.001, 0.0, 0.0 );
    vec3 n = vec3(
        scene_df(p + epsilon.xyy).x - scene_df(p - epsilon.xyy).x,
        scene_df(p + epsilon.yxy).x - scene_df(p - epsilon.yxy).x,
        scene_df(p + epsilon.yyx).x - scene_df(p - epsilon.yyx).x );
    return normalize( n );
}

#define AO_NUMSAMPLES 6
#define AO_STEPSIZE .1
#define AO_STEPSCALE .3

float calc_ao( vec3 p, 
              vec3 n )
{
    float ao = 0.0;
    float aoscale = 1.0;

    for( int aoi=0; aoi< AO_NUMSAMPLES ; aoi++ )
    {
        float step = 0.01 + AO_STEPSIZE * float(aoi);
        vec3 aop =  n * step + p;
        
        float d = scene_df( aop ).x;
        ao += -(d-step)*aoscale;
        aoscale *= AO_STEPSCALE;
    }
    
    return clamp( ao, 0.0, 1.0 );
}

// **************************************************************************
// SHADING

struct SurfaceData
{
    float id;
    vec3 point;
    vec3 normal;
    vec3 vdir;
};


vec3 shade_surface(SurfaceData surf)
{

    vec3 surfcol = vec3(1.);
    if (close(surf.id, PLANE_MATL) > .5) {
        surfcol = vec3(0.9, 0.9, 1.);    
    } else if (close(surf.id, ELLINGTON_BODY_MATL) > .5) {
        surfcol = vec3(255., 51., 0.)/255.;
    } else if (close(surf.id, ELLINGTON_SCLERA_MATL) > .5) {
        surfcol = vec3(1., 1., 1.);
    }else if (close(surf.id, ELLINGTON_PUPIL_MATL) > .5) {
        surfcol = vec3(0.05);
    }
    
    // ambient occlusion is amount of occlusion.  So 1 is fully occluded
    // and 0 is not occluded at all.  Makes math easier when mixing 
    // shadowing effects.
    float ao = calc_ao(surf.point, surf.normal);
    vec3 keydir = normalize( vec3(4., 5.,3.) );
    float diff = clamp( dot( surf.normal, keydir ), 0., 1.);
    float amb = .35;

    vec3 rimdir = normalize( vec3(-5., 4., -4.) );
    vec3 hdir = normalize(rimdir + surf.vdir);
    float rim = 3. * clamp( dot(hdir, surf.normal), 0., 1.);

	// shadowing is reverse of occlusion where a shadow value of 1 is
	// no shadow.  confusing... I should fix this.
    float sshad = 1.;
    if ( diff > 0.02 )
    {
        sshad = soft_shadow( surf.point, keydir, 0.02, 10., 7.);
    }
    
    return surfcol * (rim + diff * sshad + amb * (1. - 3.5 * ao)); 

}

// **************************************************************************
// CAMERA & GLOBALS

struct CameraData
{
    vec3 origin;
    vec3 dir;
    vec2 st;
};

CameraData setup_camera( vec2 aaoffset, vec2 fragCoord )
{

    float invar = iResolution.y / iResolution.x;
    vec2 st = (fragCoord.xy + aaoffset) / iResolution.xy - .5;
    st.y *= invar;

    // calculate the ray origin and ray direction that represents
    // mapping the image plane towards the scene for a pinhole
	// camera
    vec3 iu = vec3(0., 1., 0.);

    vec3 iz = normalize( g_camPointAt - g_camOrigin );
    vec3 ix = normalize( cross(iz, iu) );
    vec3 iy = cross(ix, iz);

    vec3 dir = normalize( st.x*ix + st.y*iy + 1.0 * iz );

    return CameraData(g_camOrigin, dir, st);

}

void animate_globals()
{
    // remap the mouse click ([-1, 1], [-1/ar, 1/ar])
    float invar = iResolution.y/iResolution.x;
    vec2 click = iMouse.xy / iResolution.xy - .5;    
    click *= 2.;  click.y *= invar;
    
    g_time = max(0., iGlobalTime - 2.);

    // camera position
    g_camOrigin = vec3(0.0, 0.5, 5.0);    
    g_ellingtonLookingAt = vec3(0.0, 0.5, 5.0);

    float shift_pressed = texture2D( iChannel0, vec2(KEY_SHIFT, .25)).x;
    float seed = 100. * sawtooth(.01 * g_time);
    g_ellingtonLookingAt.xy += mix( vec2(1., invar) * ppnoise2v(.7 * seed, 4.),
                                    click,
                                    shift_pressed );

    g_camPointAt   = vec3(0., .6, 0.);

}

// **************************************************************************
// MAIN

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{   
    
    // ----------------------------------
    // Animate globals

    animate_globals();


    vec3 scenecol = vec3(0.);
    float denom = TWO_PI/max(1., NUM_AA_SAMPLES-1.);
	// The first sample does not have any aa offset, but the subsequent
	// samples are offset onto a circle that's a pixel wide.
    for (float aa = 0.; aa < NUM_AA_SAMPLES; aa += 1.) 
    {

        vec2 aaoffset = step(.5, aa) * .5 * vec2( cos((aa-1.) * denom ),
                                                  sin((aa-1.) * denom ) );

		// ----------------------------------
		// SETUP CAMERA
		
        CameraData cam = setup_camera( aaoffset, fragCoord);

        // ----------------------------------
        // SCENE MARCHING

        vec2 scenemarch = dist_march( cam.origin, 
                                      cam.dir, 
                                      DISTMARCH_MAXDIST );
        
        // ----------------------------------
        // SHADING
        if (scenemarch.y > 0.)
        {
            vec3 mp = cam.origin + scenemarch.x * cam.dir;
            vec3 mn = calc_normal( mp );

            SurfaceData currSurf = SurfaceData(scenemarch.y,
                                               mp, mn, normalize(mp - cam.origin));

            // fall off exponentially into the distance (as if there is a spot light
            // on the point of interest).
            scenecol += shade_surface( currSurf ) * exp( -0.01*scenemarch.x*scenemarch.x );
        }
    
    }

    scenecol /= NUM_AA_SAMPLES;

    // ----------------------------------
    // POST PROCESSING

    // Gamma correct
    scenecol = pow(scenecol, vec3(.45));

    // Contrast adjust - cute trick learned from iq
    // scenecol = mix( scenecol, vec3(dot(scenecol,vec3(0.333))), 0. );

    // color tint
    // scenecol = .5 * scenecol + .5 * scenecol * vec3(1., 1., 1.);
    
    if (g_debugcolor.a > 0.) 
    {
        fragColor.rgb = g_debugcolor.rgb;
    } else {
        fragColor.rgb = scenecol;
    }

    fragColor.a = 1.;
}
