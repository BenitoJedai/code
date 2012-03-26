precision highp float;
	varying vec4 vColor;
	varying vec3 vLightWeighting;
	
	void main(void) {
		//gl_FragColor = vec4(1,1,1,1);
		gl_FragColor = vColor * vec4( vLightWeighting, 1.0 );
	}