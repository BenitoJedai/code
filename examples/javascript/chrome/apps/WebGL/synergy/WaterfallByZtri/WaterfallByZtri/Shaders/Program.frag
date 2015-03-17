float time;

vec3 rotate(vec3 r, float v){ return vec3(r.x*cos(v)+r.z*sin(v),r.y,r.z*cos(v)-r.x*sin(v));}


vec3 blob(float i){ 
	
	float t   = max(0.0,time+(time*(mod(i,10.0)*0.1)));
	vec3  s   = vec3(sin(i*1.02),(mod(t*0.2+i*0.01+sin(i)*30.0,1.0)-0.5)*13.0,sin(i*0.1));
		  s  *= 1000.0;
	float f   = s.y+1000.0;
	float f1  = min(f,0.0)-abs(sin(abs(max(f,0.0)*0.0009)))*clamp(1.0-f/3000.0,0.0,1.0)*3000.0*mod(i*0.2,1.0);
	float f2  = s.y-f1;
	vec3  m  = s + s*f2*0.0004;
	vec3  ss = vec3(m.x,f1,m.z);
	return ss;
	}



void mainImage( out vec4 fragColor, in vec2 fragCoord ){

	time        = iGlobalTime*0.3;

	float BALL_SIZE = 0.9999;
	float BLEND_DEPTH = 200.0;
	float BLEND_BALL = 0.004;
	
    vec2  uv     = fragCoord.xy/(iResolution.xx*0.5)-vec2(1.0,iResolution.y/iResolution.x);
	float rot    = sin(time)*0.5+iMouse.x*0.01;
    vec3  ray1   = rotate(-normalize(vec3(uv.x,uv.y-0.3,1.0)),rot);
    vec3  campos = rotate(vec3(0.0,-3000.0,7000.0),rot);
	vec3  blobpos= vec3(0.0);
		
		
    float i = 0.0;
    float cl = 0.0;  
	float ca = 0.0;
    vec3  cp = vec3(0.0);
	float cd = 99999.9;    

	float ii = 0.0;
	
    for(int i=0;i<100;i++){
        
		blobpos = blob(ii);
		
        vec3  vect  = blobpos-campos;
        float vectm = length(vect);     
        vec3  vectn = vect/vectm;
		
        float d = dot(ray1,vectn);
		float c = BALL_SIZE/d;
		float w = (smoothstep(-BLEND_DEPTH,0.0,cd-vectm) * smoothstep(c-BLEND_BALL,c,d));
		
		cd = mix(cd,vectm,w);
		cp = mix(cp,blobpos,w);
		cl = mix(cl,d,w);

		ii ++;
		
	};
    

	float a = dot(ray1, cp-campos );
	vec3  contact = campos+ray1*a;
	vec3  norm = normalize(cp-contact);	
	float dist = length(cp-contact);	

	float cut   = clamp(smoothstep(0.80,0.99,cl)-pow(smoothstep(400.0,0.0,dist),1.0),0.0,1.0);
	float angle = dot(norm,vec3(0.0,0.8,0.2));
	
	float sha  = smoothstep(-0.3,-0.5,angle)*0.5;
	float hil  = smoothstep(0.6,0.8,angle);
	float sha2 = smoothstep(1.0,0.0,max(0.0,-contact.y))*smoothstep(0.40,0.98,cl);
	
	vec3 backcol = vec3(0.25,0.35,0.4)-sha2*0.15+smoothstep(0.0,0.4,ray1.y-0.1)*0.4;
	
	vec3 blobcol = vec3(0.3,0.4,0.45);
		 blobcol = mix(blobcol,vec3(0.0,0.0,0.05),sha); 
		 blobcol = mix(blobcol,vec3(1.0),hil); 

	fragColor = vec4(sqrt(mix(backcol,blobcol,cut))-dot(uv,uv)*0.4,1.0);
    
    
}

