#ifdef GL_ES
precision highp float;
#endif
  varying vec3 col;
void main(void) {
   gl_FragColor = vec4(col, 1.);
}