// Checks if spesific pixel is located within these bounds
float pixelInCube(vec2 p, vec4 t){
    return float(p.x > t.x-t.z*.5 && p.x < t.x+t.z*.5 && p.y > t.y-t.w*.5 && p.y < t.y+t.w*.5);
}

// Checks if spesific pixel is located within these bounds
float pixelInCircle(vec2 p, vec3 t){
    float hypT = pow(t.z*.5,2.)+pow(t.z*.5,2.);
    float hypP = pow(p.x-t.x,2.)+pow(p.y-t.y,2.);
    return float(hypP < hypT);
}

// Creates a cone for the light
float shadow_isInBounds(vec2 p, vec3 l, vec4 t, float side){
    
    float ty = t.y - t.w*.5*(float(l.x > t.x)-.5)*2.*side;
    float tx = t.x - t.z*.5*(float(l.y < t.y)-.5)*2.*side;
    
	float rot = atan(ty-l.y,tx-l.x)+45.;
    float lx = l.x + cos(rot)*l.z*side;
    float ly = l.y + sin(rot)*l.z*side;
    
    float angle = (ly-ty)/(lx-tx);
    float 	f = float(p.y > ly+((p.x-lx)*angle) && lx>tx);  
    		f += float(p.y < ly+((p.x-lx)*angle) && lx<tx);
    
	return f;
}

// Culls the "front" of the cone so that only the "shadow" is visible
float shadow_cullLight(float f, vec2 p, vec3 l, vec4 t){
	float ty = t.y - t.w*.5*(float(l.y > t.y)-.5)*2.;
    float tx = t.x - t.z*.5*(float(l.x > t.x)-.5)*2.;
    
    float c = 1.0;
    
    c*= float((p.y < ty && ty > l.y) || (p.y > ty && ty < l.y));
    c*= float((p.x < tx && tx > l.x) || (p.x > tx && tx < l.x));
    return clamp(f-c,0.0,1.0);
}

// Checks if spesific pixel is located within these bounds
float pixelInShadow(vec2 p, vec3 l, vec4 t){
	return shadow_cullLight(shadow_isInBounds(p, l, t, 1.)-shadow_isInBounds(p, l, t, -1.),p,l,t);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec4 color = vec4(.3);
	vec4 cube;
    vec4 T = vec4(fragCoord.xy,iResolution.xy);
    vec3 light = vec3(iMouse.x+float(iMouse.x == 0.0)*T.z*.4,iMouse.y+float(iMouse.y == 0.0)*T.w*.5,20.0);
    float time = iGlobalTime; // Controlls the rotationspeed of the circle
    
    color.rg += pixelInCircle(T.xy,light);
    float circleScale = 1.01;
    // Draw rotating circle
    for(int i = 0; i < 6; i+=1){
    	cube = vec4(cos(float(i)*circleScale+time)*T.w*.3+T.z*.7,sin(float(i)*circleScale+time)*T.w*.3+T.w*.5,40.0,40.0);
        color.r += pixelInCube(T.xy,cube);
        color -= pixelInShadow(T.xy,light,cube)*.95;
    }
    
    // Draw the corner objects
    for(int i = 0; i < 4; i+=1){
    	cube = vec4(mod(floor(float(i)*.5),2.0)*-T.z*.8 + T.z*.9,mod(float(i),2.0)*-T.w*.8 + T.w*.9,40.0,40.0);
        color.r += pixelInCube(T.xy,cube);
        color -= pixelInShadow(T.xy,light,cube)*.95;
    }
    
    // Draw the wall
    cube = vec4(T.z*.2,0.0,10.0,T.w);
    color.r += pixelInCube(T.xy,cube);
    color -= pixelInShadow(T.xy,light,cube)*.95;
    
	fragColor = color;
}