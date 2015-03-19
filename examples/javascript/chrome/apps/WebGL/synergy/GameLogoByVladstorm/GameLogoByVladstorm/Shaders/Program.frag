//TODO length squared ? 
//also there is another aproach to set a position and use distance then 
//here:https://www.shadertoy.com/view/Msf3z7

vec2 cur = vec2(0.);
vec3 cl = vec3(0.);

float wave = 0.;
float cshift = 0.1;
bool bSmall = false;

float focus = .5;
float size = 1.;

//float focus_c = .0;
//float size_c = 0.;


//https://www.shadertoy.com/view/4sXSWs strength= 16.0
vec3 filmGrain(vec2 uv, float strength ){       
    float x = (uv.x + 4.0 ) * (uv.y + 4.0 ) * (iGlobalTime * 10.0);
	return  vec3(mod((mod(x, 13.0) + 1.0) * (mod(x, 123.0) + 1.0), 0.01)-0.005) * strength;

}

float hash(vec2 p){
    return fract(sin(dot(p,vec2(127.1,311.7))) * 43758.5453123);
}



vec3 circleGlow( vec2 pos ){ //, float size, float focus

    //c = pow(c, focus);
    //vec3 col = vec3(c );    
    
    //float c = 20. / length(pos*size_c);
    //vec3 col = vec3(
    //	pow(c, focus_c - .5),
    //    pow(c, focus_c ),
    //    pow(c, focus_c + .5)
    //);

    //float c = 20. / length(pos*size_c);

    vec3 col = vec3(
    	pow(20. / length((pos+vec2(cshift,0.))*size), focus - .5),
        pow(20. / length(pos*size), focus ),
        pow(20. / length((pos-vec2(cshift,0.))*size), focus + .5)
    );
    
	return col;
}


vec3 bm(vec3 c1, vec3 c2){
	return 1.- (1.-c1)*(1.-c2);
}

void C(vec2 fragCoord, bool s){
	cur.x--;
    if(!s) return;
    //if(s){
        //48 - is the length
        vec2 pos = vec2((fragCoord.xy - iResolution.xy/2. + (cur+vec2(48.2/2., -2.5))*10.) / 15.);
        
        
        //focus_c = floor( focus *  sin(fragCoord.y - iResolution.y) *1.)/1.;
        //size_c = size*filmGrain(cur, 1.0).x*1000.; //floor( size *  sin(fragCoord.x - iResolution.x) *1.)/1. ;
        
        //float var = clamp(hash(cur)*10., .1, 1.); //variations
        float var = hash(cur)*.5;
        //float var = hash2(cur)*.5;

        
        float t = var + iGlobalTime;
        
        if(bSmall) {
            wave = 4.*abs( sin(t)*sin(t*3.) );
            cshift = .12;
        }else{
        	wave = .4*sin(t)*sin(t*3.);
        	cshift = .0; //.1?
        }
        
        size = 200.*( 1.-.3+ wave); //br =.4
        focus = 2.*(.82 + .02*sin(t*40.)*sin(30.+t*20.)); 
        
        vec3 c = circleGlow(pos);
        cl = bm(cl, c);
        //cl = cl*c;         
        //cl = cl+c;         
        //cl = max(cl*.5, circleGlow( pos ));

    //}
} 

//next line
void NL(){
    cur.y++;
    cur.x=0.;
}


#define X C(fragCoord,true);
#define _ C(fragCoord,false);

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{

	
    //size =200.*(1.-iMouse.x/iResolution.x); //size
    //focus = 2.*(iMouse.y/iResolution.y); // focus

    //prev pulse
    //size = 200.*(1.-.3+ .4*sin(iGlobalTime)*sin(iGlobalTime*3.));
    //focus = 2.*(.82 + .02*sin(iGlobalTime*40.)*sin(30.+iGlobalTime*20.)); //
    
    //flicker - pulse
    //y = mod(iGlobalTime, 1.);
    

cur = vec2(0.);
cl = vec3(.0);

// arc
cur.y-=10.;
bSmall = true;
					_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;X;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;X;X;X;_;_;_;X;X;X;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;X;X;X;_;_;_;_;_;_;_;_;_;_;_;X;X;X;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;_;_;_;X;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;X;_;_;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;NL();
					_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;NL();
					NL();
					NL();
					NL();
					  
					//printed mars    
					bSmall = false;
					X;X;_;_;X;X;_;_;X;X;X;_;X;_;_;X;_;X;X;X;_;X;X;X;_;X;X;_;_;_;_;X;_;_;_;X;_;_;X;_;_;X;X;_;_;X;X;_;NL();
					_;_;X;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;_;_;_;_;_;_;_;_;_;_;_;X;_;_;_;_;NL();
					X;X;_;_;X;X;_;_;_;X;_;_;X;X;_;X;_;_;X;_;_;X;X;X;_;_;_;X;_;_;_;X;X;_;X;X;_;X;X;X;_;X;X;_;_;X;X;_;NL();
					X;_;_;_;_;X;_;_;_;X;_;_;X;_;X;X;_;_;X;_;_;X;_;_;_;_;_;X;_;_;_;X;_;X;_;X;_;X;_;X;_;_;X;_;_;_;_;X;NL();
					X;_;_;_;_;_;X;_;X;X;X;_;X;_;_;X;_;_;X;_;_;X;X;X;_;X;X;_;_;_;_;X;_;X;_;X;_;X;_;X;_;_;_;X;_;X;X;_;NL();
					NL();    
					NL();
					NL();
					    
					//chapter one    
					bSmall = true;
					_;_;X;X;X;_;X;_;X;_;_;X;_;_;X;X;X;_;X;X;X;_;X;X;X;_;X;X;X;_;_;_;_;_;X;X;X;_;X;X;X;_;X;X;X;_;_;NL();
					_;_;X;_;_;_;X;X;X;_;X;X;X;_;X;X;X;_;_;X;_;_;X;X;_;_;X;_;_;_;_;_;_;_;X;_;X;_;X;_;X;_;X;X;_;_;_;NL();
					_;_;X;X;X;_;X;_;X;_;X;_;X;_;X;_;_;_;_;X;_;_;X;X;X;_;X;_;_;_;_;_;_;_;X;X;X;_;X;_;X;_;X;X;X;_;_;NL();

    
	vec2 uv = fragCoord.xy / iResolution.xy;
    vec3 fg = filmGrain(uv,  20.0);
    cl = bm(cl, fg);//1.- (1.-cl)*(1.-fg); 
    fragColor = vec4(cl, 1.);
}
