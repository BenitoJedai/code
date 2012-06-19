#ifdef GL_ES
precision highp float;
#endif


uniform float time;
uniform vec2 resolution;
uniform vec2 aspect;
 
void main( void ) {
 
	vec2 position = -aspect.xy + 2.0 * gl_FragCoord.xy / resolution.xy * aspect.xy;
    float angle = 0.0 ;
    float radius = sqrt(position.x*position.x + position.y*position.y) ;
    if (position.x != 0.0 && position.y != 0.0){
        angle = degrees(atan(position.y,position.x)) ;
    }
    float amod = mod(angle+30.0*time-120.0*log(radius), 30.0) ;
    if (amod<15.0){
        gl_FragColor = vec4( 0.0, 0.0, 0.0, 1.0 );
    } else{
        gl_FragColor = vec4( 1.0, 1.0, 1.0, 1.0 );                    
    }
}