#define pi 3.14159265358979

float _CircleRadius = 0.06;
float _OutlineWidth = 0.02;
vec4 _OutlineColor = vec4(0.,0.,0.,0.);
float _LineWidth = 0.05;
vec4 _LineColor = vec4(186.0/256.0,42.0/256.0,42.0/256.0,0.);
float _Antialias = 0.01;
vec4 _BackgroundColor = vec4(227./256.,206./256.,178./256.,0.);

float line(vec2 pos, vec2 point1, vec2 point2, float width) {   		
    vec2 dir0 = point2 - point1;
    vec2 dir1 = pos - point1;
    float h = clamp(dot(dir0, dir1)/dot(dir0, dir0), 0.0, 1.0);
    return (length(dir1 - dir0 * h) - width * 0.5);
}

float circle(vec2 pos, vec2 center, float radius) {
    float d = length(pos - center) - radius;
    return d;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    vec2 originalPos = (2.0 * fragCoord - iResolution.xy)/iResolution.yy;
    vec2 pos = originalPos;
    
    // Twist
    //pos.x += 0.5 * sin(5.0 * pos.y);

    vec2 split = vec2(0, 0); 
    if (iMouse.z > 0.0) {       
        split = (-iResolution.xy + 2.0 * iMouse.xy) / iResolution.yy;
    }
    
    // Background
    vec3 col = _BackgroundColor.rgb * (1.0-0.2*length(originalPos));
    float speed = 0.3;
    float l = 0.8;
    vec3 p0 = vec3(l, 0. + speed * iGlobalTime, pi * 0.5  + speed * iGlobalTime);
    vec3 p1 = vec3(l, pi * 0.5 +speed * iGlobalTime, pi +speed * iGlobalTime);
    vec3 p2 = vec3(l, pi * 0.5 +speed * iGlobalTime, pi + pi * 0.66 + speed * iGlobalTime);
    vec3 p3 = vec3(l, pi * 0.5 +speed * iGlobalTime, pi + pi * 1.33 + speed * iGlobalTime);
    
    vec2 point0 = vec2(cos(p0.z), sin(p0.z)) * sin(p0.y) * p0.x;
    vec2 point1 = vec2(cos(p1.z), sin(p1.z)) * sin(p1.y) * p1.x;
    vec2 point2 = vec2(cos(p2.z), sin(p2.z)) * sin(p2.y) * p2.x;
    vec2 point3 = vec2(cos(p3.z), sin(p3.z)) * sin(p3.y) * p3.x;

    float d = line(pos, point0, point1, _LineWidth);
    d = min(d, line(pos, point1, point2, _LineWidth));
    d = min(d, line(pos, point2, point3, _LineWidth));
    d = min(d, line(pos, point0, point2, _LineWidth));
    d = min(d, line(pos, point0, point3, _LineWidth));
    d = min(d, line(pos, point1, point3, _LineWidth));
    d = min(d, circle(pos, point0, _CircleRadius));
    d = min(d, circle(pos, point1, _CircleRadius));
    d = min(d, circle(pos, point2, _CircleRadius));
    d = min(d, circle(pos, point3, _CircleRadius));	

    if (originalPos.x < split.x) {
        col = mix(_OutlineColor.rgb, col, step(0., d - _OutlineWidth));
	    col = mix(_LineColor.rgb, col, step(0., d));
    } else if (originalPos.y > split.y) {
        float w = _Antialias;
        col = mix(_OutlineColor.rgb, col, smoothstep(-w, w, d - _OutlineWidth));
        col = mix(_LineColor.rgb, col, smoothstep(-w, w, d));      
    } else {
        float w = fwidth(0.5*d) * 2.0;
        col = mix(_OutlineColor.rgb, col, smoothstep(-w, w, d - _OutlineWidth));
        col = mix(_LineColor.rgb, col, smoothstep(-w, w, d));
    }
    
    // Draw split lines
    col = mix(vec3(0), col, smoothstep(0.005, 0.007, abs(originalPos.x - split.x)));
    col = mix(col, vec3(0), (1. - smoothstep(0.005, 0.007, abs(originalPos.y - split.y))) * step(split.x, originalPos.x));

    fragColor = vec4(col, 1.0);
}