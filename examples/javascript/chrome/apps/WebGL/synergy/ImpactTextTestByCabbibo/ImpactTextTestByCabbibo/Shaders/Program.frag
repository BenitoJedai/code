

float impactLU[ 58 ];



// box rendering for title at end
float inBox( vec2 p , vec2 loc , float boxSize ){
 	
    if( 
        p.x < loc.x + boxSize / 2. &&
        p.x > loc.x - boxSize / 2. &&
        p.y < loc.y + boxSize / 2. &&
        p.y > loc.y - boxSize / 2. 
    ){
        
     return 1.;  
        
    }
    

   
    return 0.;
    
    
}

vec2 getTextLookup( float lu ){
    
    float posNeg = abs( lu ) / lu;
    
    float x = floor( abs( lu ) / 100. );
    float y = abs( lu ) - (x * 100.);
    
    y = floor( y / 10. );
    y *= ((abs( lu ) - (x * 100.) - (y * 10. )) -.5) * 2.;
    
    return vec2( x * posNeg , y  );
    
}


float impact( vec2 p , float stillness ){
  

    float f = 0.;
    
    for( int i = 0; i < 58; i++ ){
    	
        for( int j = 0; j < 4; j++ ){
            
            float size = (-3.+( 20. * float( j ))) * stillness + 10.;
            vec2 lu = getTextLookup( impactLU[ i ] )* size;
            f += inBox( p , vec2( iResolution / 2. ) + lu , size );
            
            
        }
 
    }
    
    return f/4.;
    
}



void mainImage( out vec4 fragColor, in vec2 fragCoord ){
    
    // CREATING 'IMPACT' 
    
    
    
    // 1000 and 100 are the x positions
    // 10 is y position
    // 1 is y sign
    // I
    impactLU[0] = -1621.;
	impactLU[1] = -1611.;
	impactLU[2] = -1600.;
	impactLU[3] = -1610.;
	impactLU[4] = -1620.;
    
    
    // M
    impactLU[5] = -1221.;
	impactLU[6] = -1211.;
	impactLU[7] = -1201.;
	impactLU[8] = -1210.;
	impactLU[9] = -1220.;
    
    impactLU[10] = -1021.;
	impactLU[11] = -1011.;
	impactLU[12] = -1001.;
	impactLU[13] = -1010.;
	impactLU[14] = -1020.;
    
    impactLU[15] = -821.;
	impactLU[16] = -811.;
	impactLU[17] = -801.;
	impactLU[18] = -810.;
	impactLU[19] = -820.;
    
    // P
    impactLU[20] = -421.;
	impactLU[21] = -411.;
	impactLU[22] = -401.;
	impactLU[23] = -410.;
	impactLU[24] = -420.;

	impactLU[25] = -221.;
	impactLU[26] = -211.;
	impactLU[27] = -201.;

  
	// A
    impactLU[28] = 221.;
	impactLU[29] = 211.;
	impactLU[30] = 201.;
	impactLU[31] = 210.;
	impactLU[32] = 220.;
    
    impactLU[33] = 321.;
    
    impactLU[34] = 421. ;
	impactLU[35] = 411. ;
	impactLU[36] = 401. ;
	impactLU[37] = 410. ;
	impactLU[38] = 420. ;
    
    
    // extra hooks for p and m...
    impactLU[39] = -321.;
    impactLU[40] = -1121.;
    impactLU[41] = -921.;
    
    
 	// C
    
  	impactLU[42] = 821.;
	impactLU[43] = 811.;
	impactLU[44] = 801.;
	impactLU[45] = 810.;
	impactLU[46] = 820.;
    
  	impactLU[47] = 921. ;
	impactLU[48] = 1021.;

  	impactLU[49] = 920. ;
	impactLU[50] = 1020.;
    
      
  	// T
    
  	impactLU[51] = 1521.;
	impactLU[52] = 1511.;
	impactLU[53] = 1501.;
	impactLU[54] = 1510.;
	impactLU[55] = 1520.;
    
  	impactLU[56] = 1421.;
	impactLU[57] = 1621.;


    
    

	



    vec2 p = (-iResolution.xy + 2.0*fragCoord.xy)/iResolution.y;
    vec2 m = iMouse.xy/iResolution.xy;
    
    
   
    
    vec3 col = vec3( 1.);
 
        
    col = vec3( 1. );

    float imp = impact( fragCoord.xy , max( 0.2 , sin( iGlobalTime )) -.2 );
    float textFade = pow( max( 0. , 0.1 ) , 2. );
    col = vec3( imp ) + vec3( textFade );      
       
    

	fragColor = vec4(col,1.0);
    
    
}
