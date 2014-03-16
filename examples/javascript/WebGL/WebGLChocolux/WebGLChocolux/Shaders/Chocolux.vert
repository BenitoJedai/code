precision lowp float;

attribute vec2 position;
uniform float t;
varying vec3 s[4];
 
void main()
{
	gl_Position = vec4(position, 0.0, 1.0);
	s[0] = vec3(0);
	s[3] = vec3(sin(abs(t * .0001)), cos(abs(t * .0001)), 0);
	s[1] = s[3].zxy;
	s[2] = s[3].zzx;
}