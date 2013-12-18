precision mediump float;


  varying vec3 col;
void main(void) {
   gl_FragColor = vec4(col, 1.);
}