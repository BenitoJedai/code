 attribute vec3 aPos;
  attribute vec3 aNorm;
  uniform mat4 mvMatrix;
  uniform mat4 prMatrix;
  varying vec4 color;
  const vec3 dirDif = vec3(0., 0., 1.);
  const vec3 dirHalf = vec3(-.4034, .259, .8776);
void main(void) {
   gl_Position = prMatrix * mvMatrix * vec4(aPos, 1.);
   vec3 rotNorm = (mvMatrix * vec4(aNorm, .0)).xyz;
   float i = max( 0., dot(rotNorm, dirDif) );
   color = vec4(.9*i, .5*i, 0., 1.);
   i = pow( max( 0., dot(rotNorm, dirHalf) ), 120.);
   color += vec4(i, i, i, 0.);
}