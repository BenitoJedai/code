#define PI	3.14159265359

float num = 0.0;
vec2 axis = vec2(0.0);
int TYPE = 3;

mat2 rotate(float a)
{
	return mat2(cos(a), sin(a), -sin(a), cos(a));	
}

float deSegment( in vec2 p, in vec2 a, in vec2 b )
{
	vec2 pa = p - a;
	vec2 ba = b - a;
	float h = clamp( dot(pa, ba)/dot(ba, ba), 0.0, 1.0 );
	return length( pa - ba * h );
}

// https://www.shadertoy.com/view/4sBSWW
float DigitBin(const in int x)
{
    return x==0?480599.0:x==1?139810.0:x==2?476951.0:x==3?476999.0:x==4?350020.0:x==5?464711.0:x==6?464727.0:x==7?476228.0:x==8?481111.0:x==9?481095.0:0.0;
}

float PrintValue(const in vec2 vPixelCoords, const in vec2 vFontSize, const in float fValue, const in float fMaxDigits, const in float fDecimalPlaces)
{
    vec2 vStringCharCoords = (gl_FragCoord.xy - vPixelCoords) / vFontSize;
    if ((vStringCharCoords.y < 0.0) || (vStringCharCoords.y >= 1.0)) return 0.0;
	float fLog10Value = log2(abs(fValue)) / log2(10.0);
	float fBiggestIndex = max(floor(fLog10Value), 0.0);
	float fDigitIndex = fMaxDigits - floor(vStringCharCoords.x);
	float fCharBin = 0.0;
	if(fDigitIndex > (-fDecimalPlaces - 1.01)) {
		if(fDigitIndex > fBiggestIndex) {
			if((fValue < 0.0) && (fDigitIndex < (fBiggestIndex+1.5))) fCharBin = 1792.0;
		} else {		
			if(fDigitIndex == -1.0) {
				if(fDecimalPlaces > 0.0) fCharBin = 2.0;
			} else {
				if(fDigitIndex < 0.0) fDigitIndex += 1.0;
				float fDigitValue = (abs(fValue / (pow(10.0, fDigitIndex))));
                float kFix = 0.0001;
                fCharBin = DigitBin(int(floor(mod(kFix+fDigitValue, 10.0))));
			}		
		}
	}
    return floor(mod((fCharBin / pow(2.0, floor(fract(vStringCharCoords.x) * 4.0) + (floor(vStringCharCoords.y * 5.0) * 4.0))), 2.0));
}

// https://www.shadertoy.com/view/XtsGRl
#define CHAR_SIZE vec2(3, 7)
#define CHAR_SPACING vec2(4, 8)

float ch_f = 2018596.0;
float ch_o = 711530.0;
float ch_l = 1198375.0;
float ch_d = 1760110.0;
float ch_1 = 730263.0;
float ch_2 = 693543.0;
float ch_3 = 693354.0;
float ch_4 = 1496649.0;
float ch_0 = 711530.0;
float ch_usc = 7.0;

vec2 print_pos =  vec2(35.0, -32.0);

float extract_bit(float n, float b)
{
	return floor(mod(floor(n) / pow(2.0,floor(b)),2.0));   
}

float sprite(float spr, vec2 size, vec2 uv)
{
    uv = floor(uv);
    float bit = (size.x-uv.x-1.0) + uv.y * size.x;
    bool bounds = all(greaterThanEqual(uv,vec2(0)));
    bounds = bounds && all(lessThan(uv,size));
    return extract_bit(spr, bit) * float(bounds);
}

float char(float ch, vec2 uv)
{
    float px = sprite(ch, CHAR_SIZE, uv - print_pos);
    print_pos.x += CHAR_SPACING.x;
    return px;
}

// +++++++++++++++++++++++++++++++++++++++++++++++++++++

vec2 fold1(in vec2 p, in float a)
{
    p.x = abs(p.x);
    vec2 v = vec2(cos(a), sin(a));
  	p -= 2.0 * min(0.0, dot(p, v)) * v;
 	return p;    
}

vec2 fold2(in vec2 p, in float a)
{
    p.x = abs(p.x);
    vec2 v = vec2(cos(a), sin(a));
    for(int i = 0; i < 2; i++)
    {	
    	p -= 2.0 * min(0.0, dot(p, v)) * v;
    	v = normalize(vec2(v.x - 1.0, v.y));
    }
 	return p;    
}

vec2 fold3(in vec2 p, in float a)
{
    p.x = abs(p.x);
    vec2 v = vec2(cos(a), sin(a));
    for(int i = 0; i < 3; i++)
    {	
    	p -= 2.0 * min(0.0, dot(p, v)) * v;
    	v = normalize(vec2(v.x - 1.0, v.y));
    }
 	return p;    
}

vec2 fold4(in vec2 p, in float a)
{
    p.x = abs(p.x);
    vec2 v = vec2(cos(a), sin(a));
    for(int i = 0; i < 4; i++)
    {	
    	p -= 2.0 * min(0.0, dot(p, v)) * v;
    	v = normalize(vec2(v.x - 1.0, v.y));
    }
 	return p;    
}

// +++++++++++++++++++++++++++++++++++++++++++++++++++;

vec2 fold(in vec2 p)
{   
    p *= 2.0;
    float t = mod(iGlobalTime, 75.0);
    TYPE = int(floor(t / 15.0));
    float a = mod(t, 15.0) * 24.0;
    num = a;
    a *= PI / 180.0;
    axis = vec2(cos(a), sin(a));
    if (TYPE > 3) return p;
    if (TYPE == 0) p = fold1(p, a);
    if (TYPE == 1) p = fold2(p, a);
    if (TYPE == 2) p = fold3(p, a);
    if (TYPE == 3) p = fold4(p, a);
    // Y axis ajust
    p.y = abs(p.y) - 1.5;
    return p;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 p = (fragCoord.xy *2.0-iResolution.xy)/iResolution.y;
    vec2 q = p;
    p= fold(p);
    vec3 col;
    col = mix(vec3(0.2,0.2,0.4), vec3(0.4,0.2,0.2), step(0.0, p.x * p.y)); 
    col = mix(col, vec3(0.8), smoothstep(0.005, 0.004, abs(q.y)));
    col = mix(col, vec3(0.8), smoothstep(0.005, 0.004, abs(q.x)));
    col = mix(col, vec3(0.8), smoothstep(0.005, 0.004, abs(length(q)-1.0)));
    col = mix(col, vec3(0.0, 1.0, 0.0), smoothstep(0.02, 0.01, abs(p.y)));
    col = mix(col, vec3(1.0, 0.0, 0.0), smoothstep(0.02, 0.01, abs(p.x)));
    col = mix(col, vec3(1.0, 1.0, 0.0), smoothstep(0.08, 0.07, length(p)));
    col = mix(col, vec3(0.0, 0.0, 1.0), smoothstep(0.02, 0.01, deSegment(q, vec2(0.0), axis)));
	col = mix(col, vec3(1.0), PrintValue(vec2(60.0, 30.0), vec2(30.0, 30.0), num, 2.0, 0.0));
    q *= 35.0;
    if      (TYPE == 0) col = mix(col, vec3(1.0), char(ch_1, q));
    else if (TYPE == 1) col = mix(col, vec3(1.0), char(ch_2, q));
    else if (TYPE == 2) col = mix(col, vec3(1.0), char(ch_3, q));
    else if (TYPE == 3) col = mix(col, vec3(1.0), char(ch_4, q));
    else                 col = mix(col, vec3(1.0), char(ch_0, q));
    col = mix(col, vec3(1.0), char(ch_usc, q));
    col = mix(col, vec3(1.0), char(ch_f, q));
    col = mix(col, vec3(1.0), char(ch_o, q));
    col = mix(col, vec3(1.0), char(ch_l, q));
    col = mix(col, vec3(1.0), char(ch_d, q));
 	fragColor = vec4(col, 1.0);
}
