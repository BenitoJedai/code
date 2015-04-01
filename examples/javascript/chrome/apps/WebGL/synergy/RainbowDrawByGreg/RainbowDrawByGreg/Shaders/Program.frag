vec3 h(float t)
{
    vec3 p = abs(fract(t + vec3(1., 2. / 3., 1. / 3.)) * 6. - 3.);
	return (clamp(p - 1., 0., 1.));
}

void mainImage( out vec4 o, in vec2 n )
{
    float t = iGlobalTime;
    vec2 m = iMouse.xy;
    float s = .8+.5*sin(t);
    if(length(n-(iResolution.xy-m))>20.*s && length(n-m)>10.*s)
        discard;
	o = vec4(h(t),0);
}