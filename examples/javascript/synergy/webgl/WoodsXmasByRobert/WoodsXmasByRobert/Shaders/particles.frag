                  uniform vec3 color;
                  uniform sampler2D texture;

                  varying vec3 vColor;
                  varying float fAlpha;

                  void main() {

                      vec4 outColor = texture2D( texture, gl_PointCoord );
                      if ( outColor.a < 0.3 ) discard; // alpha be gone

                      gl_FragColor = outColor * vec4( color * vColor.xyz, fAlpha );

                  }