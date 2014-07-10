﻿precision mediump float;

// Uniform values from CSS

uniform float amount;

// Varyings passed in from vertex shader

varying vec2 v_uv;
varying float v_height;
varying float v_light;

// Main

void main() {

	const float a = 1.0;
	float r, g, b;

	// Depth variant

	/*
	float n = 1.0 - v_height;
	float v = mix( 1.0, n, amount );

	r = g = b = v;
	*/

	// Light variant

	float n = v_light;
	float v = mix( 1.0, n * n, amount );

	r = g = b = sqrt( v );

	// Set color matrix

	css_ColorMatrix = mat4( r, 0.0, 0.0, 0.0,
							0.0, g, 0.0, 0.0,
							0.0, 0.0, b, 0.0,
							0.0, 0.0, 0.0, a );

}