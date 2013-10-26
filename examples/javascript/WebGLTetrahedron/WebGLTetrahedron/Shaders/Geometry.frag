  #ifdef GL_ES
  
  precision lowp float;

  // fcks up nexus?
  // precision highp float;
  #endif

  varying vec4 vColor;

  void main(void) {
    gl_FragColor = vColor;
  }