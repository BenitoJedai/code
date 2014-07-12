attribute vec2 position;
attribute vec2 texcoord;
uniform float h;
varying vec2 tc;
 
void main()
{
	gl_Position=vec4(position,0.0,1.0);
	tc=vec2(position.x,position.y*h);
}