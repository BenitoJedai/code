const float soft = 0.01;
const float lineWidth = 0.03;
const float pointWidth = 0.11;

const vec3 backgroundColor = vec3(0.05);
const vec3 gridColor = vec3(0.0, 0.0, 0.6);
const vec3 lineColor = vec3(0.2, 0.2, 1.0);
const vec3 pointColor = vec3(0.8, 0.9, 1.0);
const vec3 highlightColor = vec3(0.0, 0.0, 1.0);

// define the visible area
// - padding to match the screen-ratio might be added...
const vec2 bot = vec2(-4,-4);
const vec2 top = vec2(4,4);

vec2 calcProjPoint(vec2 a, vec2 b, vec2 c)
{
    float d = dot(b - a, c - a) / length(b - a);
    d /= length(b - a);
    return a + d * (b - a);
}

vec3 drawDot(vec3 col, vec2 coord, vec2 p, float thickness)
{
    float dst = distance(coord, p);
    dst = 1.0 - smoothstep(thickness - soft, thickness + soft, dst);
    return mix(col, pointColor, dst);
}

vec3 drawDotAnimated(vec3 col, vec2 coord, vec2 p, float thickness)
{
    float dst = distance(coord, p);
    float inside = dst;
    dst = 1.0 - smoothstep(thickness - soft, thickness + soft, dst);
    inside = inside / (thickness + soft)+ iGlobalTime * 0.5;
    
    vec3 color = mix(pointColor, highlightColor, sin(radians(inside*360.0))*0.5 + 0.5);
    return mix(col, color, dst);
}

vec3 drawLine(vec3 col, vec2 coord, vec2 p1, vec2 p2, float thickness)
{
    float d = dot(coord - p1, p2 - p1) / length(p2 - p1);
    d /= length(p2 - p1);
    d = clamp(step(0.0, d) * d, 0.0, 1.0);
    d = distance(p1 + d * (p2 - p1), coord);
    
    float dst = 1.0 - smoothstep(thickness - soft, thickness + soft, d);
    
    return mix(col, lineColor, dst);
}

vec3 drawLineDashed(vec3 col, vec2 coord, vec2 p1, vec2 p2, float thickness, float strips)
{
    float lenLine = length(p2 - p1);
    float d = dot(coord - p1, p2 - p1) / lenLine;
    d /= lenLine;
    float seg = clamp(d, 0.0, 1.0);
    
    d = clamp(step(0.0, d) * d, 0.0, 1.0);
    d = distance(p1 + d * (p2 - p1), coord);
    
    float dst = 1.0 - smoothstep(thickness - soft, thickness + soft, d);

    float relLinePos = distance(coord, p1) / lenLine * dst;
    
    float pattern = step(0.5, mod((seg * strips - 0.25 - iGlobalTime), 1.0));
    dst *= pattern;
    
    vec3 color = mix(pointColor, highlightColor, seg);
    return mix(col, color, dst);
}

vec3 drawGrid(vec3 col, vec2 coord)
{
    float factor = distance(coord - 0.5, floor(coord)) + 0.25;
    float anim = (sin(iGlobalTime * 2.0) * 0.5 + 0.5);
    anim = anim * 0.4 + 0.6;
    factor = pow(factor, 8.0 * anim);
    return mix(col, gridColor, factor);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.xy;
    
    vec2 area = top - bot;
    
    float ratioScreen = iResolution.x/ iResolution.y;
    float ratioArea = area.x / area.y;
    
    vec2 padding = vec2(ratioScreen / ratioArea, ratioArea /ratioScreen);
    padding = max(vec2(0.0), area * padding - area);

    area += padding;
    vec2 coord = uv * area + bot - (padding * 0.5);

    vec2 a = vec2(0,0);
	vec2 b = vec2(bot.x - (padding.x * 0.5),0);
    
    vec2 c = (iMouse.xy/ iResolution.xy)* area + bot - (padding * 0.5);

    if (c.x > 0.0)
    {
		b.x += area.x;
    }
    
    vec2 d = calcProjPoint(a, b, c);
    
    vec3 col = backgroundColor;
    
    col = drawGrid(col, coord);

    col = drawLine(col, coord, a, b, lineWidth);
    col = drawLine(col, coord, a, c, lineWidth);
    
    col = drawLineDashed(col, coord, c, d, 0.03, 2.0);

    col = drawDot(col, coord, a, pointWidth);
    col = drawDot(col, coord, b, pointWidth);
    col = drawDot(col, coord, c, pointWidth);

    col = drawDotAnimated(col, coord, d, 0.14);

    fragColor = vec4(col,1.0);
}