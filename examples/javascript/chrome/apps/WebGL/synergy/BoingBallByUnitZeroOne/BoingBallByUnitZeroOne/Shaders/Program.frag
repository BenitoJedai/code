﻿#define PI 3.1415926536

const vec2 res = vec2(320.0,200.0);
const mat3 mRot = mat3(0.9553, -0.2955, 0.0, 0.2955, 0.9553, 0.0, 0.0, 0.0, 1.0);
const vec3 ro = vec3(0.0,0.0,-4.0);

const vec3 cRed = vec3(1.0,0.0,0.0);
const vec3 cWhite = vec3(1.0);
const vec3 cGrey = vec3(0.66);
const vec3 cPurple = vec3(0.51,0.29,0.51);

const float maxx = 0.378;

//                       _                                       _ _ _ _ _ _ _ 
//       /\             (_)                                     | | | | | | | |
//      /  \   _ __ ___  _  __ _  __ _  __ _  __ _  __ _  __ _  | | | | | | | |
//     / /\ \ | '_ ` _ \| |/ _` |/ _` |/ _` |/ _` |/ _` |/ _` | | | | | | | | |
//    / ____ \| | | | | | | (_| | (_| | (_| | (_| | (_| | (_| | |_|_|_|_|_|_|_|
//   /_/    \_\_| |_| |_|_|\__, |\__,_|\__,_|\__,_|\__,_|\__,_| (_|_|_|_|_|_|_)
//                          __/ |                                              
//                         |___/

//By @unitzeroone
//Check out http://www.youtube.com/watch?feature=player_detailpage&v=ZmIf-5MuQ7c#t=26s for context.
//Decyphering the code&magic numbers and optimizing is left as excercise to the reader ;-)

//-1/5/2013 FIX : Windows was rendering "inverted z checkerboard" on entire screen.
//-1/5/2013 CHANGE : Did a modification for the starting position, so ball doesn't start at bottom right.
//-1/5/2013 CHANGE : Tweaked edge bounce.
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	float asp = iResolution.y/iResolution.x;
	vec2 uv = (fragCoord.xy / iResolution.xy);
	vec2 uvR = floor(uv*res);
	vec2 g = step(2.0,mod(uvR,16.0));
	vec3 bgcol = mix(cPurple,mix(cPurple,cGrey,g.x),g.y);
	uv = uvR/res;
	float xt = mod(iGlobalTime+1.0,6.0);
	float dir = (step(xt,3.0)-.5)*-2.0;
	uv.x -= (maxx*2.0*dir)*mod(xt,3.0)/3.0+(-maxx*dir);
	uv.y -= abs(sin(4.5+iGlobalTime*1.3))*0.5-0.3;
	bgcol = mix(bgcol,bgcol-vec3(0.2),1.0-step(0.12,length(vec2(uv.x,uv.y*asp)-vec2(0.57,0.29))));
	vec3 rd = normalize(vec3((uv*2.0-1.0)*vec2(1.0,asp),1.5));
	float b = dot(rd,ro);
	float t1 = b*b-15.6;
    float t = -b-sqrt(t1);
	vec3 nor = normalize(ro+rd*t)*mRot;
	vec2 tuv = floor(vec2(atan(nor.x,nor.z)/PI+((floor((iGlobalTime*-dir)*60.0)/60.0)*0.5),acos(nor.y)/PI)*8.0);
	fragColor = vec4(mix(bgcol,mix(cRed,cWhite,clamp(mod(tuv.x+tuv.y,2.0),0.0,1.0)),1.0-step(t1,0.0)),1.0);
}