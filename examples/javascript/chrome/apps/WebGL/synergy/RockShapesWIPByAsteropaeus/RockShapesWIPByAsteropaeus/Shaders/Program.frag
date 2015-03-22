/*

Lets try out this distance field raymarching stuff, in the style of iq.

Extremely unfinished and messy, but the basic idea - using randomly generated BSP splits to
generate interesting shapes that resemble rock formations - seems to show some potential, if I
can get rid of all the artifacts.

The real fun would be if I could get a nice displacement working on top of all this.

A great deal of optimization should be possible once I clean up some of this horrible mess.

Daniel "Asteropaeus" Dresser

*/


const float heightCap = 0.5;

float hash( vec2 p )
{
	float h = dot(p,vec2(127.1,311.7));
	
    return -1.0 + 2.0*fract(sin(h)*43758.5453123);
}

vec2 getCell( vec3 p, out vec3 normals[6], out vec3 centers[6], out float hardnesses[6] )
{
	hardnesses[0] = hardnesses[1] = hardnesses[2] = hardnesses[3] = 0.002;
	normals[0] = vec3(1,0,0);
	normals[1] = vec3(1,0,0);
	normals[2] = vec3(0,1,0);
	normals[3] = vec3(0,1,0);
	centers[0] = vec3(-1,0,0);
	centers[1] = vec3(1,0,0);
	centers[2] = vec3(0,-1,0);
	centers[3] = vec3(0,1,0);
		
	vec3 splitAxis = vec3( 1.0, 0.0, 0.0 );
	//vec3 splitCenter = vec3(0.0, 0.0001 * iMouse.x / iResolution.x, 0.0);
    //vec3 splitCenter = vec3(0.000007);
    vec3 splitCenter = vec3(0.000027 + 0.000001 * floor(iGlobalTime / 20.0));
	float splitSize = 0.9;
	vec2 rockId = splitCenter.xy;
	
	float curAxis = 1.0;
	for( float i = 0.0; i < 10.0; i += 1.0 )
	{
		curAxis *= -1.0;
        
		splitAxis = vec3( normalize( curAxis * vec2(splitAxis.y, -splitAxis.x ) + 0.0 * 0.35 * vec2( hash( splitCenter.xy ), hash( splitCenter.xy + vec2( 0.1, 0.0 ) ) ) ),
                         0.2 * hash( splitCenter.xy + vec2( 3.1, 3.0 ) ) );

		   
		float axisDot = dot( p - splitCenter, splitAxis);
		float dist = abs( axisDot );
		
		
		vec3 newSplitCenter = vec3( splitCenter.xy + sign( axisDot ) * splitAxis.xy * splitSize * ( 1.0 + 0.3 * hash( splitCenter.xy + vec2(0.7, sign(axisDot)))), 0.0 );
		//if( hash( newSplitCenter + vec2(0.2,0.7 ) ) > 0.3 * max( 1.0, 7.0 - i ) )
        
		
		float splitPick = hash( splitCenter.xy + vec2(0.2,0.7 ) ) > 0.0 ? 1.0 : -1.0;
        float hardness = hash( splitCenter.xy + vec2(0.2,0.7 ) ) > -( 1.0 - 1.9 * pow( 8.0 * length( rockId - newSplitCenter.xy ), 2.0 ) ) ? 1.0 : 0.0;
		if( axisDot * splitPick < 0.0 && hardness == 0.0 )
		//if( axisDot > 0.0 )
		{
			rockId = newSplitCenter.xy;
			hardnesses[0] = hardnesses[1] = hardnesses[2] = hardnesses[3] = 0.02;
		}
        hardness = 0.02 * ( 1.0 - hardness ) + 0.002;
		if( curAxis > 0.0 )
		{
			if( axisDot > 0.0 )
			{
                if( dot(centers[0] - splitCenter, splitAxis) < 0.0 )
                {
                    normals[0] = splitAxis;
                    centers[0] = splitCenter;
                    hardnesses[0] = hardness;
                }
			}
			else
			{
                if( dot(centers[1] - splitCenter, splitAxis) > 0.0 )
                {
					normals[1] = splitAxis;
					centers[1] = splitCenter;
    	            hardnesses[1] = hardness;   
                }
			}
		}
		else
		{
			if( axisDot > 0.0 )
			{
                if( dot(centers[2] - splitCenter, splitAxis) < 0.0 )
                {
                    normals[2] = splitAxis;
                    centers[2] = splitCenter;
                    hardnesses[2] = hardness;
                }
			}
			else
			{
                if( dot(centers[3] - splitCenter, splitAxis) > 0.0 )
                {
					normals[3] = splitAxis;
					centers[3] = splitCenter;
                	hardnesses[3] = hardness;
                }
			}
		}
		
		splitCenter = newSplitCenter;
		splitSize *= sqrt(0.5) * ( 1.0 + 0.0 * hash( splitCenter.xy + vec2(0.5,0.5) ) );
	}
    
    float eccentricity = 2.0 * (0.5 - length( rockId ));
    normals[4] = normalize( vec3( eccentricity * 0.4 * hash(rockId + vec2(6.0)), 0.1 + eccentricity * 0.6 * hash(rockId + vec2(5.0)), 1.0 ) );
    centers[4] = vec3( rockId.xy, eccentricity * eccentricity * eccentricity* 0.15 + 0.2 * hash(rockId + vec2(4.0)) );
    
    float extraChunkOffset = 0.1 * hash(splitCenter.xy + vec2(14.0));
    centers[5] = vec3( splitCenter.xy, dot(centers[4] - splitCenter, normals[4]) / normals[4].z + extraChunkOffset );
    normals[5] = normalize( vec3( hash(splitCenter.xy + vec2(15.0)), hash(splitCenter.xy + vec2(16.0)), 2.0 ) );
                      
    if( extraChunkOffset < 0.0 )
    {
        vec3 swap = normals[4];
        normals[4] = normals[5];
        normals[5] = swap;
        swap = centers[4];
        centers[4] = centers[5];
        centers[5] = swap;
    }
    //normals[4] = vec3( 0.0, 0.0, 1.0 );
    //normals[4] = vec3( 0.0, 0.0, 1.0 );
    //float softnessBoost = 0.05 * pow( 0.5 * ( hash( splitCenter.xy + vec2( 12.0, 11.0 ) ) + 1.0), 3.0 );
    hardnesses[0] = max( hardnesses[0], 0.03 * pow( 0.5 * ( hash( splitCenter.xy + vec2( 12.0, 11.0 ) ) + 1.0), 3.0 ) );
    hardnesses[1] = max( hardnesses[0], 0.03 * pow( 0.5 * ( hash( splitCenter.xy + vec2( 12.0, 12.0 ) ) + 1.0), 3.0 ) );
    hardnesses[2] = max( hardnesses[0], 0.03 * pow( 0.5 * ( hash( splitCenter.xy + vec2( 12.0, 13.0 ) ) + 1.0), 3.0 ) );
    hardnesses[3] = max( hardnesses[0], 0.03 * pow( 0.5 * ( hash( splitCenter.xy + vec2( 12.0, 14.0 ) ) + 1.0), 3.0 ) );
    //0.03 * ( 1.0 - closestHardness1 ) + 0.002 + softnessBoost;
    
    return length( splitCenter ) > 1.00 ? vec2(1e38) : rockId;
}

float cellMap( vec3 p, vec3 normals[6], vec3 centers[6], float hardnesses[6], vec2 rockId, float flip )
{
    if( rockId.x == 1e38 ) return 1e38;
    float closestDist1 = 1e38;
    float closestDist2 = 1e38;
    float closestHardness1 = 1e38;
    float closestHardness2 = 1e38;


    float dir = -1.0;
    for( int i = 0; i < 4; i += 1 )
    {
        dir *= -1.0;
        float dist = dir * dot( p - centers[i], normals[i]);
        if( dist < closestDist1 )
        {
            closestDist2 = closestDist1;
            closestHardness2 = closestHardness1;
            closestDist1 = dist;
            closestHardness1 = hardnesses[i];
        }
        else if( dist < closestDist2 )
        {
            closestDist2 = dist;
            closestHardness2 = hardnesses[i];
        }
    }

    /*float softnessBoost = 0.1 * pow( 0.5 * ( hash( rockId + vec2( 12.0, 11.0 ) ) + 1.0), 3.0 );
    float softness1 = 0.03 * ( 1.0 - closestHardness1 ) + 0.002 + softnessBoost;
    float softness2 = 0.03 * ( 1.0 - closestHardness2 ) + 0.002 + softnessBoost;*/
    float softness1 = closestHardness1;
    float softness2 = closestHardness2;
    
    

    if( softness2 > softness1 )
    {
    	float swap = softness1;
        softness1 = softness2;
        softness2 = swap;
        
        swap = closestDist1;
        closestDist1 = closestDist2;
        closestDist2 = swap;
    }


    softness1 = max( softness1, closestDist1 );
    softness2 = max( softness2, closestDist2 );

    float softness = min( softness1, softness2 );
    float verticalDist1 = -dot( p - centers[4], normals[4] );
    float verticalDist2 = -dot( p - centers[5], normals[5] );
    float pickSide = ( verticalDist2 <  verticalDist1 || verticalDist1 > 0.0 ) ? 1.0 : -1.0;
    float verticalDist = pickSide == flip ? verticalDist1 :
    	min( -verticalDist1, verticalDist2 );
    //float verticalDist = verticalDist1;
    

    float bigRadAxis1 = softness1 - closestDist1;
    float bigRadAxis2 = softness1 - verticalDist;
    float bigRadAxis1Clamp = max( 0.0, bigRadAxis1);
    float bigRadAxis2Clamp = max( 0.0, bigRadAxis2);
    float bigRad = sqrt( bigRadAxis1Clamp * bigRadAxis1Clamp + bigRadAxis2Clamp * bigRadAxis2Clamp );
    bigRad = bigRad > 0.0 ? bigRad : max( bigRadAxis1, bigRadAxis2 );
    
    float smallRadAxis1 = softness2 - closestDist2;
    float smallRadAxis2 = softness2 - ( softness1 - bigRad );
    float smallRadAxis1Clamp = max( 0.0, smallRadAxis1 );
    float smallRadAxis2Clamp = max( 0.0, smallRadAxis2 );
    float smallRad = sqrt( smallRadAxis1Clamp * smallRadAxis1Clamp + smallRadAxis2Clamp * smallRadAxis2Clamp );
    smallRad = smallRad > 0.0 ? smallRad : max( smallRadAxis1, smallRadAxis2 );
    //smallRad = min( smallRad, 0.0 );

    //return bigRad - softness1;
    return smallRad - softness2;
}

float getOcclusion( vec3 pos, vec3 nor, float stepLength, vec2 self )
{
    const float lookupRad = 0.06;
    const int lookupSamples = 16;
    const int steps = 4;
    float ret = 0.0;
    vec2 prevId = vec2( 0.0 );
    for( int l = 0; l < lookupSamples; l++ )
    {
        float hardnesses[6];
        vec3 normals[6];
        vec3 centers[6];
        float angle = float(l) / float(lookupSamples) * 2.0 * 3.14159265;
        vec2 rockId = getCell( pos + lookupRad * vec3( sin( angle ), cos(angle), 0.0 ), normals, centers, hardnesses );
        if( rockId == prevId ) continue;
        prevId = rockId;
        float flip = rockId != self ? 1.0 : -1.0;

        float accum = 0.0;
        for( int i = 0; i < steps; i++ )
        {
            float dist = float(i) * stepLength;
            float cellDist = cellMap( pos + nor * dist, normals, centers, hardnesses, rockId, flip );
            accum += max( 0.0, (dist - cellDist) / dist );
        }
        ret = max( ret, accum );

    	/*float step = 8.;
    	float ao = 0.;
    	float dist;
    	for (int i = 1; i <= 3; i++) {
        dist = step * float(i);
		ao += max(0., (dist - map(p + n * dist).y) / dist);*/
    }
    //return 1. - ao * 0.1;
    return 0.2 * ret;
}


float trace( vec3 camPos, vec3 camDir, float t, float reflT, vec3 reflDir, out vec3 hit, out float hardnesses[6], out vec3 normals[6], out vec3 centers[6], out vec2 rockId )
{
    float marchThroughBorder = 0.0001;
    float status = 1.0;
    for( float j = 0.0; j < 40.0; j += 1.0 )
    {
        float curCellStartT = t;
        //march += camDir * 0.005;
        t += marchThroughBorder;
        if( reflT < t )
        {
            /*camPos = camPos + camDir * reflT;
            camDir = reflDir;
            t = marchThroughBorder;
            status = -1.0;
            reflT = 1e38;*/
            hit = camPos + camDir * reflT;
            return -1.0;
        }

        vec3 march = camPos + camDir * t;


        if( ( abs( march.x ) > 1.0 && march.x * camDir.x > 0.0 ) ||
            ( abs( march.y ) > 1.0 && march.y * camDir.y > 0.0 ) || march.z > heightCap) break;

        rockId = getCell( march, normals, centers, hardnesses );


        float sideDot0 = dot( camDir, normals[0] );
        float sideDot1 = dot( camDir, normals[1] );
        float sideDot2 = dot( camDir, normals[2] );
        float sideDot3 = dot( camDir, normals[3] );
        float sideDot4 = dot( camDir, normals[4] );
        float sideDot5 = dot( camDir, normals[5] );
        float cellExitDist = reflT;
        if( sideDot0 < 0.0 ) cellExitDist = min( cellExitDist, dot( normals[0], centers[0] - camPos) / sideDot0 ); 
        if( sideDot1 > 0.0 ) cellExitDist = min( cellExitDist, dot( normals[1], centers[1] - camPos) / sideDot1 );
        if( sideDot2 < 0.0 ) cellExitDist = min( cellExitDist, dot( normals[2], centers[2] - camPos) / sideDot2 );
        if( sideDot3 > 0.0 ) cellExitDist = min( cellExitDist, dot( normals[3], centers[3] - camPos) / sideDot3 );
        t = max( t, min( sideDot4 > 0.0 ? 0.0 : dot( normals[4], centers[4] - camPos) / sideDot4,
                        sideDot5 > 0.0 ? 0.0 : dot( normals[5], centers[5] - camPos) / sideDot5 ) );
        //hitColor = normals[3] * 0.5 + vec3(0.5);
        //hitColor = vec3( cellExitDist * 0.1 );
        //break;

        float dist = 1e38;
        float mapTol = 0.001;
        for( float k = 0.0; k < 20.0; k += 1.0 )
        {
            dist = cellMap( camPos + camDir * t, normals, centers, hardnesses, rockId, 1.0 );
            if( dist < mapTol || t + dist > cellExitDist ) break;
            t += dist;
        }

        if( dist < mapTol && t < reflT)
        {
            /*vec2 checkRockId = getCell( camPos + camDir * t, normals, centers, hardnesses );
            if( checkRockId != rockId )
            {
                //reflectionMult *= vec3( 1.0, 0.0, 0.0 );
                //t = curCellStartT * 0.75 + t * 0.25;
                //t = curCellStartT + 0.1;
                continue;

            }*/
            
            hit = camPos + camDir * t;
            //hitColor = reflectionMult * vec3( 0.8 );
            return status;
            //break;
        }

        t = cellExitDist;
    }
    
    return 0.0;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = (2.0 * fragCoord.xy / iResolution.xy - vec2(1)) * vec2( 1.0, 1.0 );
    
    float camAngle = ( iMouse.z < 0.0 ? 0.0 : iMouse.x - iMouse.z ) / iResolution.x * 4.0 * 3.1415926 - 0.1 + iGlobalTime / 20.0 * 2.0 * 3.14159265;
    float camDistance = max( 16.0 * ( iMouse.w < 0.0 ? 0.0 : iMouse.y - iMouse.w ) / iResolution.y + 2.1, 0.0001 );
    //float camAngle = abs( iMouse.x ) / iResolution.x * 4.0 * 3.1415926 - 2.6 + 2.0;
    //float camDistance = max( 16.0 * abs (iMouse.y ) / iResolution.y - 1.0, 0.0001 ) ;
    vec3 camPos = vec3( sin( camAngle ) * camDistance, cos( camAngle ) * camDistance, 0.35 );
    vec3 camForward = normalize( vec3( 0.0, 0.0, 0.0 ) - camPos );
    vec3 camRight = normalize( cross( vec3( 0.0, 0.0, 1.0 ), camForward ) );
    vec3 camUp = normalize( cross( camForward, camRight ) );
    
    vec3 camDir = normalize( camForward + 0.3 * iResolution.x / iResolution.y * camRight * uv.x + 0.3 * camUp * uv.y );
	
    float waterHitT = -camPos.z / camDir.z;
    vec3 waterHit = camPos + waterHitT * camDir;
    vec3 waterNorm = normalize( vec3( 0.0, 0.03 * sin( 120.0 * waterHit.y + 2.0 * iGlobalTime ) + 0.1 * sin( 10.0 * waterHit.y + iGlobalTime ), 1.0 ) );
    vec3 reflDir = reflect( camDir, waterNorm );
    
    
    
    /*float shortestStartIntersection = (1.0 - camPos.x ) / camDir.x;
    shortestStartIntersection = min( shortestStartIntersection, (-1.0 - camPos.x ) / camDir.x );
    shortestStartIntersection = min( shortestStartIntersection, (1.0 - camPos.y ) / camDir.y );
    shortestStartIntersection = min( shortestStartIntersection, (-1.0 - camPos.y ) / camDir.y );*/
    

    vec3 reflectionMult = vec3( 1.0 );
    
    
    vec3 hitColor = vec3( -1.0 );
    /*if( camDir.z > 0.0 )
    {
        hitColor = vec3( 0.3, 0.3, 0.8 );
    }
    else*/
    {
        float t = max( (-sign( camDir.x ) - camPos.x ) / camDir.x, (-sign( camDir.y) - camPos.y ) / camDir.y );
    	t = max( t, (heightCap - camPos.z ) / camDir.z );
        
        //float waterDist = ( -camPos.z ) / camDir.z;
        
        vec3 cellTracingCamDir = camDir;
        vec3 cellTracingCamPos = camPos;
        float status = 1.0;
        vec3 hit = waterHit;
        if( waterHitT < t )
        {
            //cellTracingCamPos = waterHit;
            //cellTracingCamDir = reflDir;
            //t = max( (-sign( cellTracingCamDir.x ) - cellTracingCamPos.x ) / cellTracingCamDir.x, (-sign( cellTracingCamDir.y) - cellTracingCamPos.y ) / cellTracingCamDir.y );
            // //reflectionMult = vec3( 0.5 );
            //hit = waterHit;
            status = -1.0;
            //waterHitT = 1e38;
        }

        
        //shortestStartIntersection = 1.0;
        //vec3 march = camPos + camDir * ( shortestStartIntersection + marchThroughBorder );

        float hardnesses[6];
        vec3 normals[6];
        vec3 centers[6];
        vec2 rockId;

        
        if( status > 0.0 )
        {
			status *= trace( cellTracingCamPos, cellTracingCamDir, t, waterHitT, reflDir, hit, hardnesses, normals, centers, rockId );
        }
        /*if( dot( hit - cellTracingCamPos, cellTracingCamDir ) > waterHitT )
        {
			hit = cellTracingCamPos + waterHitT * cellTracingCamDir;
        	status = -1.0;
        }*/
    
		vec3 sunDir = normalize( vec3( 1.0, 1.0, 1.0 ) );
        //if( hitColor.x >= 0.0 )
        //if( status != 0.0 )
        {
            
            //vec3 march = camPos + camDir * t;
            vec3 nor;
            if( status == 1.0 )
            {
                vec3 eps = vec3( 0.001, 0.0, 0.0 );
                nor = normalize( vec3(
                    cellMap( hit + eps.xyy, normals, centers, hardnesses, rockId, 1.0 ) - cellMap( hit - eps.xyy, normals, centers, hardnesses, rockId, 1.0 ),
                    cellMap( hit + eps.yxy, normals, centers, hardnesses, rockId, 1.0 ) - cellMap( hit - eps.yxy, normals, centers, hardnesses, rockId, 1.0 ),
                    cellMap( hit + eps.yyx, normals, centers, hardnesses, rockId, 1.0 ) - cellMap( hit - eps.yyx, normals, centers, hardnesses, rockId, 1.0 )
                ) );
            }
            else
            {
                nor = vec3( 0.0, 0.0, 1.0 );
                rockId = vec2( 0.0 );
            }

            //nor = vec3( 0.0, 0.0, 1.0 );

            
            float test = hit.x;
            float occlMult = 1.0 - getOcclusion( hit, nor, 0.01, rockId );
            occlMult = occlMult > 0.0 ? occlMult : 1.0;
            hitColor = mix( vec3( 0.2, 0.2, 0.3 ), vec3 ( 0.2, 0.2, 0.19 ), -nor.z * 0.5 + 0.5 ) * max( 0.0, occlMult );
            
            float shadow = max( 0.0, trace( hit, sunDir, 0.01, 1e38, vec3( 0.0 ), hit, hardnesses, normals, centers, rockId ) );
            //float shadow = 0.0;

            hitColor += vec3( 0.65, 0.6, 0.45 ) * max( 0.0, dot( normalize(nor), sunDir ) ) * ( 1.0 - shadow );
            
            //hitColor.z = sin( test );
            //hitColor *= status < 0.0 ? 0.5 : 1.0;
        }
        /*else
        {
            vec3 halfAng = normalize( sunDir - camDir );
            hitColor = vec3( 0.1, 0.1, 0.5 ) + pow( max( 0.0, dot( halfAng, waterNorm ) ), 8.0 );
        }*/
    }
    //if( hitColor == vec3( -1.0 ) ) hitColor = vec3( 0.1, 0.1, 0.5 );

	//vec3 material = vec3( 0.7 + 0.3 * hash(rockId + vec2(1.0)) );
	//material.xy = vec2( 0.0 );
	//float mask = clamp( edgemask * 1000.0, 0.0, 1.0 );
	//fragColor = vec4( mix( vec3( 0.4, 0.35, 0.25 ), mix( 1.0, 0.4 + 0.6 * pow( clamp( edgemask, 0.0, 1.0 ), 0.4), 20.0 * softness ) * material, mask ), 1.0);
    const float gamma = 1.0 / 2.4;
    fragColor = vec4( pow( hitColor.x, gamma ), pow( hitColor.y, gamma ), pow( hitColor.z, gamma ), 1.0 );
	//fragColor += 1.0 * vec4( closestDist1 / softness1, closestDist2 / softness2, 0.0, 1.0);
	//fragColor = vec4( 0.0 );
	//fragColor += 0.5 * vec4( clamp( closestDist1 / softness1, 0.0, 1.0), clamp( closestDist2 / softness2, 0.0, 1.0 ), 0.0, 1.0);
	//fragColor = vec4( vec3( 0.0 ) * ( 1.0 - binary_edge ) + binary_edge * vec3( 0.7 + 0.3 * hash(rockId + vec2(1.0)) ), 1.0);
	//fragColor = vec4( vec3( 0.5 ) * min( 1.0, 1.0 - edgemask * 1000.0 ), 1.0 );
    //fragColor = vec4( iMouse.z, -iMouse.z * 0.001, iMouse.z * 0.01, 1.0 );
}