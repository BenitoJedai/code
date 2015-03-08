 #define MAX_ITERATIONS 140
 

	// rotations from euler angles
    mat4 fromEuler(vec3 ang) {
        vec2 a1 = vec2(sin(ang.x),cos(ang.x));
        vec2 a2 = vec2(sin(ang.y),cos(ang.y));
        vec2 a3 = vec2(sin(ang.z),cos(ang.z));
        mat4 m;
        m[0] = vec4(a1.y*a3.y+a1.x*a2.x*a3.x,a1.y*a2.x*a3.x+a3.y*a1.x,-a2.y*a3.x,0.0);
        m[1] = vec4(-a2.y*a1.x,a1.y*a2.y,a2.x,0.0);
        m[2] = vec4(a3.y*a1.x*a2.x+a1.y*a3.x,a1.x*a3.x-a1.y*a3.y*a2.x,a2.y*a3.y,0.0);
        m[3] = vec4(0.0,0.0,0.0,1.0);
        return m;
    }           

    // rotate (operate) vec3 from mat4
    vec3 rotate(vec3 v, mat4 m) {
       return vec3(dot(v,m[0].xyz),dot(v,m[1].xyz),dot(v,m[2].xyz));
    } 

    // p: point
    // Distance function (some objects)
    vec3 map( in vec3 p ) {
        float sr = 1.0;  // sphere radius
        vec3 box = vec3(3.5,1.7,0.3);
        float rbox = 0.1;
        float id = 1.0;  // 3 = floor, 2 wall, 1 sphere

        // Sphere
        float distS = length(p + vec3( sin(iGlobalTime * 0.21) * 3.0,0.3,cos(iGlobalTime * 0.21) * 3.0))-sr;
        
        // First wall
        float distQ = length(max(abs(p)-box,0.0))-rbox;
        // Cuboid moved 2.0 to the right (2on wall)
        float distQ2 = length(max(abs(p + vec3(-2.0,.0,2.0)) - vec3(0.3,1.7,3.5), 0.0))-rbox;

        // Window in the wall
        vec3 d = abs(p) - vec3(0.5,0.8,1.0);
        float distQ3 = min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));

        // External wall (parcel limit) without ceiling
        vec3 d2 = vec3(10.0,2.0,10.0) - abs(p);
        float distQ4 = max( min( d2.x,  d2.z ), -d2.y );

        // Column
        float distCol = length(max(abs(p + vec3(2.5,.0,4.5)) - vec3(0.3,1.7,0.3), 0.0))-rbox;


        float distTot = min( min( distS, distQ), distQ2 );
        distTot = max( -distQ3,distTot );
        distTot = min( distCol,distTot );
        distTot = min( distTot, distQ4 );

        if (distS > distTot) {
            id = 2.0;
        }

        float distP = p.y + 1.6;

        if (distP < distTot) {
            id = 3.0;
        }

        distTot = min(distP, distTot);

        return vec3(distTot, id, 0.0 );
    }


	// texture
    float fbm( vec3 p, vec3 n )
    {
        return texture2D( iChannel0, (p.xy + p.zy) / 4.0).x;
    }

	// bump mapping
    vec3 doBumpMap( in vec3 pos, in vec3 nor )
    {
        float e = 0.0015;
        float b = 0.1;
        
        float ref = fbm( pos, nor );
        vec3 gra = b*vec3( fbm( vec3(pos.x+e, pos.y, pos.z),nor)-ref,
                            fbm( vec3(pos.x, pos.y+e, pos.z),nor)-ref,
                            fbm( vec3(pos.x, pos.y, pos.z+e),nor)-ref )/e;
        
        vec3 tgrad = gra - nor * dot ( nor , gra );
        return normalize ( nor - tgrad );
    }


    // p: point to calculate gradient
	// id: object id
    // Calculate normal by gradient
    vec3 calcNormal( in vec3 p, in float id ) {
        vec3 eps = vec3(0.002,0.0,0.0);

        vec3 nor = normalize( vec3(
            map( p + eps.xyy).x - map(p - eps.xyy).x,
            map( p + eps.yxy).x - map(p - eps.yxy).x,
            map( p + eps.yyx).x - map(p - eps.yyx).x));

        if (id == 2.0) {
            return doBumpMap(p,nor);
        }

        return nor;
    }


    // shadow
    // ro: ray origin
    // rd: ray destination
    float softShadow( in vec3 ro, in vec3 rd, float mint, float maxt, float k )
    {
        float res = 1.0;
        float t = mint;
        for( int i=0; i < 100; i++ )
        {
            vec3 vh = map(ro + rd*t);
            float h=vh.x;
            if( h<0.00 )
                return 0.0;
            res = min( res, (k*h/t) );
            t += h;
            if (t > maxt)
                break;
        }

        return res;
    }

	// ambientOclussion
    float ambientOclussion( in vec3 p, in vec3 n) {
        float step = 0.1;
        float res = 1.0;
        vec4 vstep = vec4( n*step, step );
        vec4 np = vec4(p,0.0) + vstep;
        for (int i=0; i < 5; i++) {
            res -= (np.w - map( np.xyz ).x) * 0.25;
            np += vstep;
        }

        return max(res,0.0);
    }

    // p: point to colorize
	// id: object id of point
    vec3 colorize( in vec3 p, in float id ) {
        vec3 n = calcNormal( p, id);

        // light pos2 (rotate)
        vec3 lighto2 = vec3(800.0 * sin(iGlobalTime*0.04),1000.0,800.0 * cos(iGlobalTime * 0.04));
        vec3 tolight2 = normalize(lighto2 - p);
        vec3 colorLight2 = vec3(0.8,0.9,0.9);


        // text col
        float ambientOc = ambientOclussion( p, n );
        vec3 direct2 = colorLight2 * 1.0 * max(dot(n,tolight2),0.0) / (ambientOc);// * ambientOc);

        float shadow2 = softShadow(p, tolight2, 0.02, 50.0, 5.0) * ambientOc;

        vec3 texture;
        if (id == 2.0)
            texture =  max( texture2D(iChannel0, (p.xy + p.zy) / 4.0 ).xyz, vec3(.4,.4,.4));
        else if (id == 1.0)
            texture = vec3(.2,.5,.6);
        else
            texture = vec3(.2,.5,.2);

        vec3 ambient = vec3(.5,.5,.5);


        return  (texture * ambient * ambientOc) + (texture * direct2 * shadow2);  // One light

    }


    // ro: ray origin
    // rd: ray direction (normalizada)
    vec3 rayMarch( in vec3 ro, in vec3 rd ) {

        float dist = 0.0;
        vec3 vdist;
        vec3 np = ro;
        for( int i = 0; i < MAX_ITERATIONS; i++ ) {
            
            vdist = map(np);
            dist = vdist.x;
            if (dist < 0.01)
                break;
            np += rd * dist;

        }

        if (dist < 0.01) {
            return colorize(np, vdist.y);
        }

        return vec3( 0.6, .6, 0.8);   

    }


    void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
        // Obtenemos xy de fragment y normalizamos (haciendo cuadrados los píxeles si hace falta)
        vec2 uv = fragCoord.xy / iResolution.xy;
        vec2 p = -1.0 + 2.0 * uv;
        p.x *= iResolution.x/iResolution.y;


        // cam calculation (camo = origin, camd = destination/target)
        vec3 camo = vec3( 9.0*sin( iGlobalTime * -0.2), 1.0, 9.0*cos(iGlobalTime*-0.2 ));//vec3(iCamX, iCamY, iCamZ);
        vec3 camd = camo;
        camd = normalize(camd);
        vec3 up = vec3(0.0,1.0,0.0);
        vec3 left = normalize(cross(camd, up));
        camd = normalize( left*p.x + up*p.y - 2.0*camd );


		// Calculate and assign color
        fragColor = vec4( rayMarch( camo, camd), 1 );

    }

