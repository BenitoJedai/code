attribute vec3 aVertexPosition;
	attribute vec4 aVertexNormal;
	attribute vec4 aVertexColor;
	
	uniform mat4 uMVMatrix;
	uniform mat4 uPMatrix;
	uniform mat4 uNMatrix;

	varying vec4 vColor;
	
	varying vec3 vLightWeighting;

	void main(void) {
		
		vec3 uAmbientColor = vec3( 0.2, 0.2, 0.2 );
		vec3 uDirectionalColor = vec3( 0.8, 0.8, 0.8 );
		vec3 uLightingDirection = vec3( 0, 1, 0 );
		
		gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);
		gl_PointSize = 2.0;
		//vLightWeighting = vec3(1.0, 1.0, 1.0);
		vec4 transformedNormal = uNMatrix * vec4(aVertexNormal.xyz, 1.0);
		float directionalLightWeighting = max(dot(transformedNormal.xyz, uLightingDirection), 0.0);
		vLightWeighting = uAmbientColor + uDirectionalColor * directionalLightWeighting;
		vColor = aVertexColor;
	}	