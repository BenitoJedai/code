// Created by Eduardo Castineyra - casty/2015
// Creative Commons Attribution 4.0 International License


vec2 pV[4];
// |0  |1
//
// |2  |3

vec2 pH[3];
//	- 2
//	- 1
//	- 0

vec2 uv;
vec2 pixel = 2.0/iResolution.xy;
int SIZE = 8;
vec2 SEGMENT = pixel * vec2(SIZE, 1.0);
float KERNING = 1.3;
const ivec2 DIGITS = ivec2(2, 4);

void fillNumbers(){
    pV[0] = vec2(0, SIZE);  pV[1] = vec2(SIZE - 1, SIZE);
    pV[2] = vec2(0, 0); 	pV[3] = vec2(SIZE - 1, 0);
    
    for (int i = 0; i < 3; i++)
    	pH[i] = vec2(0, SIZE * i);
	}

vec2 step2(vec2 edge, vec2 v){
    return vec2(step(edge.x, v.x), step(edge.y, v.y));
	}

float segmentH(vec2 pos){
    vec2 sv = step2(pos, uv) - step2(pos + SEGMENT.xy, uv);
    return step(1.1, length(sv));
	}

float segmentV(vec2 pos){
    vec2 sv = step2(pos, uv) - step2(pos + SEGMENT.yx, uv);
    return step(1.1, length(sv));
	}

float digit(int d, vec2 pos){
    vec4 sv = vec4(1.0, 0.0, 1.0, 0.0);
    vec3 sh = vec3(1.0);
    float c = 0.0;
    if (d == 0){sv = vec4(1, 1, 1, 1); sh = vec3(1, 0, 1);}
    if (d == 1){sv = vec4(0, 1, 0, 1); sh = vec3(0, 0, 0);}
    if (d == 2){sv = vec4(0, 1, 1, 0); sh = vec3(1, 1, 1);}
    if (d == 3){sv = vec4(0, 1, 0, 1); sh = vec3(1, 1, 1);}
    if (d == 4){sv = vec4(1, 1, 0, 1); sh = vec3(0, 1, 0);}
    if (d == 5){sv = vec4(1, 0, 0, 1); sh = vec3(1, 1, 1);}
    if (d == 6){sv = vec4(1, 0, 1, 1); sh = vec3(1, 1, 1);}
    if (d == 7){sv = vec4(0, 1, 0, 1); sh = vec3(0, 0, 1);}
    if (d == 8){sv = vec4(1, 1, 1, 1); sh = vec3(1, 1, 1);}
    if (d == 9){sv = vec4(1, 1, 0, 1); sh = vec3(1, 1, 1);}
    
    for (int i = 0; i < 4; i++)
        c += segmentV(pos + pixel.x * pV[i]) * sv[i];

    for (int i = 0; i < 3; i++)
        c += segmentH(pos + pixel.x * pH[i]) * sh[i];
    
	return c;
	}

float printNumber(float n, vec2 pos){
    float d = floor(n);
    float c = 0.0;
    for (int i = 0; i < DIGITS.x; i++){
        c += digit(int(mod(d, 10.0)), pos - KERNING * pixel * vec2(SIZE * (i + 1), 0.0));
        d = floor(d /10.0);
    	}
    
    for (int i = 0; i < DIGITS.y; i++){
        n *= 10.0;
        c += digit(int(mod(floor(n), 10.0)), pos + KERNING * pixel * vec2(SIZE * (i + 1), 0.0));
    	}
   	return c;
	}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    uv = fragCoord.xy / iResolution.xy;
    vec2 mouse = iMouse.xy / iResolution.xy;
    
    fillNumbers();
    
    fragColor = vec4(printNumber(mouse.x * 100.0, mouse) + 
                     printNumber(mouse.y * 100.0, mouse - pixel * vec2(0.0, SIZE * 5)));
    
}