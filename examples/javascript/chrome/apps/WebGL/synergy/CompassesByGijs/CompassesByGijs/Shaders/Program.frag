const float c     = 5.;

      vec2  Ratio = iResolution.xy/iResolution.y;
      float time  = iGlobalTime/3.;

vec2 Scale(vec2 p){
    return (p/iResolution.xy*2.-1.)*Ratio;
}

vec2 MinPole  = Scale(abs(iMouse.zw)) * float(iMouse.z != 0.0);
vec2 PlusPole = Scale(iMouse.xy);


void mainImage( out vec4 fragColor, in vec2 fragCoord ){
    vec2 scaledp = Scale(fragCoord);
    
    //debug standard color
    vec3 col = vec3(abs(scaledp.x),abs(scaledp.y),1.);
    
    //Compass drawing
    	//Compass position and scaling etc..
        vec2 blocked = mod(scaledp*c+.5,1.)*2.-1.;//position of the pixel within the compass
        vec2 middle  = floor(scaledp*c+.5)/c;//middle of compass in the field

    	//forces
        vec2 delta1 = PlusPole-middle;
        vec2 force1 =  delta1/dot(delta1,delta1);
        vec2 delta2 = MinPole-middle;
        vec2 force2 = -delta2/dot(delta2,delta2);
        vec2 forcer = normalize(force1+force2);

    	//distances to use for drawing
        float d = abs(-blocked.x*forcer.y+blocked.y*forcer.x)/length(forcer);//distance from compass-point to force-direction-ray
        float bd = length(blocked);//distance from compass-point to the middle of the compass;

    	//colouring
        if(bd<.95){//inside of compass
            if(d<.1){//compasss needle
                if(dot(forcer,blocked)>0.){//compass point is on the force's direction side
                    col = vec3(.8,.3,.3);//red
                }else{
                    col = vec3(.9);//white
                }
            }else{
                col = vec3(.2);
            }
        }else if(bd>1.){//outside of compasses
            col = vec3(.1);
        }else{//compass border
            col = vec3(0.);
        }
    
    //magnet drawing
    	//calculating the distance from a point to the magnet
        vec2 delta = PlusPole-MinPole;
        float dotdelta = dot(delta,delta);     
        float t3 =  clamp(dot(scaledp-MinPole,delta)/dotdelta,0.,1.);
        float dm =  distance(MinPole + delta*t3, scaledp); 
    	//colouring
        if(dm<.02){//inside magnet
            if(distance(scaledp,PlusPole)>distance(scaledp,MinPole)){//on which side of the magnet
                col = vec3(.8,.3,.3);
            }else{
                col = vec3(.9);
            }
        }
    
	fragColor = vec4(col,1.0);
}