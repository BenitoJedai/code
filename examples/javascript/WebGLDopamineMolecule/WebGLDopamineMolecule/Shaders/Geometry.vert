  attribute vec3 aPos; // Normals = Pos
  uniform mat4 mvMatrix;
  uniform mat4 prMatrix;
  uniform vec3 color;
  uniform float scale;
  varying vec3 col;
  const vec4 dirDif = vec4(0., 0., 1., 0.);
  const vec4 dirHalf = vec4(-.4034, .259, .8776, 0.);
void main(void) {
   gl_Position = prMatrix * mvMatrix * vec4(scale * aPos, 1.);
   vec4 rotNorm = mvMatrix * vec4(aPos, .0);
   float i = max( 0., dot(rotNorm, dirDif) );
   col = i * color;
   i = pow( max( 0., dot(rotNorm, dirHalf) ), 30.);
   col += vec3(i, i, i);
}