
precision mediump float;

// Varyings

varying float v_light;

// Main

void main() {

	float r, g, b;
	r = g = b = v_light;

	css_ColorMatrix = mat4( r, 0.0, 0.0, 0.0,
							0.0, g, 0.0, 0.0,
							0.0, 0.0, b, 0.0,
							0.0, 0.0, 0.0, 1.0 );

}
