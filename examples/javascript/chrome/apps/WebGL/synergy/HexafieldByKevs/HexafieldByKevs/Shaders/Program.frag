/* Hexafield distance field ray-marching demo - by Kevin Roast
   Uses code by other authors - as credited in method comments.
   http://www.kevs3d.co.uk/dev/shaders
*/

#ifdef GL_ES
precision highp float;
#endif

#define PI 3.14159265
#define GAMMA 0.8
#define CONTRAST 1.1
#define SATURATION 1.2
#define BRIGHTNESS 1.2
#define AO_SAMPLES 5
#define RAY_DEPTH 256
#define MAX_DEPTH 100.0
#define SHADOW_RAY_DEPTH 16
#define DISTANCE_MIN 0.001

const vec2 delta = vec2(DISTANCE_MIN, 0.);


vec3 RotateZ(vec3 p, float a)
{
   float c,s;
   vec3 q=p;
   c = cos(a);
   s = sin(a);
   p.x = c * q.x - s * q.y;
   p.y = s * q.x + c * q.y;
   return p;
}

float HexPrism(vec3 p, vec2 h)
{
   vec3 q = abs(p);
   return max(q.y-h.y,max(q.x+q.z*0.57735,q.z*1.1547)-h.x);
}

float Plane(vec3 p, vec3 n)
{
   return dot(p, n);
}

vec3 ReplicateXZ(vec3 p, vec3 c)
{
   return vec3(mod(p.x, c.x) - 0.5 * c.x, p.y, mod(p.z, c.z) - 0.5 * c.z);
}

float Dist(vec3 pos)
{
   pos = RotateZ(pos, sin(iGlobalTime)*0.25);
   vec3 q1 = ReplicateXZ(pos, vec3(3.5,0.,2.));
   vec3 q2 = ReplicateXZ(pos + vec3(3.5/2.0,0.,1.), vec3(3.5,0.,2.));
   return
      min(
         // ground plane - offset in the Y axis
         Plane(pos-vec3(0.,-0.5,0.), vec3(0.,1.,0.)),
         min(
            // subtract one hex prism from another to make a hollow shape
            max(
               HexPrism(q1, vec2(1.0,0.5)),
               -HexPrism(q1-vec3(0.,0.35,0.), vec2(0.7,0.4))
            ),
            // TODO: animate!!
            min(
               HexPrism(q2, vec2(1.0,0.5)),
               HexPrism(q2-vec3(0.,0.35,0.), vec2(0.7,0.4))
            )
         )
      );
}

// Based on original by IQ - optimized to remove a divide
float CalcAO(vec3 p, vec3 n)
{
   float r = 0.0;
   float w = 1.0;
   for (int i=1; i<=AO_SAMPLES; i++)
   {
      float d0 = float(i) * 0.2; // 1.0/5.0
      r += w * (d0 - Dist(p + n * d0));
      w *= 0.5;
   }
   return 1.0 - clamp(r,0.0,1.0);
}

float CalcSSS(vec3 ro, vec3 rd)
{
   float total = 0.0;
   float weight = 0.5;
   for (int i=1; i<=AO_SAMPLES; i++)
   {
      float delta = pow(float(i), 2.5) * DISTANCE_MIN * 64.0;
      total += -weight * min(0.0, Dist(ro+rd * delta));
      weight *= 0.5;
   }
   return clamp(total, 0.0, 1.0);
}

// Based on original code by IQ
float SoftShadow(vec3 ro, vec3 rd, float k)
{
   float res = 1.0;
   float t = 0.05;         // min-t see http://www.iquilezles.org/www/articles/rmshadows/rmshadows.htm
   for (int i=0; i<SHADOW_RAY_DEPTH; i++)
   {
      float h = Dist(ro + rd * t);
      res = min(res, k*h/t);
      t += h;
      if (t > 10.0) break; // max-t
   }
   return clamp(res, 0.0, 1.0);
}

vec3 GetNormal(vec3 pos)
{
   vec3 n;
   n.x = Dist( pos + delta.xyy ) - Dist( pos - delta.xyy );
   n.y = Dist( pos + delta.yxy ) - Dist( pos - delta.yxy );
   n.z = Dist( pos + delta.yyx ) - Dist( pos - delta.yyx );
   
   return normalize(n);
}

const vec3 lightColour = vec3(0.0, 0.8, 2.0);
const vec3 lightDir = vec3(10.0, 6.0, 2.0);
const float diffuse = 0.25;
const vec3 sssColour = vec3(1.1,1.5,2.2);
const float ambientFactor = 0.25;

vec4 Shading(vec3 pos, vec3 rd, vec3 norm)
{
   vec3 light = lightColour * max(0.0, dot(norm, normalize(lightDir)));
   vec3 heading = normalize(-rd + lightDir);
   light = (diffuse * light);
   light *= SoftShadow(pos, lightDir, 16.0);
   light = mix(light, sssColour, CalcSSS(pos, rd));
   light += CalcAO(pos, norm) * ambientFactor;
   return vec4(light, 1.0);
}

// Original method by David Hoskins
vec3 PostEffects(vec3 rgb, vec2 xy)
{
	rgb = pow(rgb, vec3(GAMMA));
	rgb = mix(vec3(.5), mix(vec3(dot(vec3(.2125, .7154, .0721), rgb*BRIGHTNESS)), rgb*BRIGHTNESS, SATURATION), CONTRAST);
	rgb *= .4+0.5*pow(40.0*xy.x*xy.y*(1.0-xy.x)*(1.0-xy.y), 0.2 );	
	return rgb;
}

// Camera function by TekF
// Compute ray from camera parameters
vec3 GetRay(vec3 dir, vec2 pos)
{
   pos = pos - 0.5;
   pos.x *= iResolution.x/iResolution.y;
   
   dir = normalize(dir);
   vec3 right = normalize(cross(vec3(0.,1.,0.),dir));
   vec3 up = normalize(cross(dir,right));
   
   return dir + right*pos.x + up*pos.y;
}

vec4 March(vec3 ro, vec3 rd)
{
   float t = 0.0;
   float d = 1.0;
   for (int i=0; i<RAY_DEPTH; i++)
   {
      vec3 p = ro + rd * t;
      d = Dist(p);
      if (abs(d) < DISTANCE_MIN)
      {
         return vec4(p, 1.0);
      }
      t += d;
      if (t >= MAX_DEPTH) break;
   }
   return vec4(0.0);
}

const vec3 cameraPos = vec3(7.5,10.0,0.0);
const vec3 cameraLookAt = vec3(0.0,-100.0,-100.0);

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
   vec3 off = vec3(0.0);
   off.z -= iGlobalTime*5.0;
   
   vec4 res = vec4(0.0);
   vec2 p = fragCoord.xy / iResolution.xy;
   vec3 ro = cameraPos + off;
   vec3 rd = normalize(GetRay(cameraLookAt-cameraPos, p));
   res = March(ro, rd);
   res.xyz = clamp(Shading(res.xyz, rd, GetNormal(res.xyz)).xyz, 0.0, 1.0);
   res.xyz = PostEffects(res.xyz, p);
   
   fragColor = vec4(res.rgb, 1.0);
}