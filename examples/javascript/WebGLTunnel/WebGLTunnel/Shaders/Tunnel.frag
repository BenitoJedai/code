	#ifdef GL_ES
	precision mediump float;
	#endif
 
	uniform sampler2D uSampler;
 
	varying vec4 vColor;
	varying vec2 vTextureCoord;
 
	void main(void) {
		// -- get the pixel from the texture
		vec4 textureColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
		// -- multiply the texture pixel with the vertex color
		gl_FragColor = vColor * textureColor;
	}