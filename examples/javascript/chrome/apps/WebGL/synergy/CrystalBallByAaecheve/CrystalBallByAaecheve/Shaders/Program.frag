//aaecheve 2013

//const vec3 lightPosition = vec3(0.1, 0.5, 0.5);
const vec3 cameraPosition = vec3(0.5, 0.5, 1);
const float zDepth = -3.0;
const float zMin = -1.0;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

//ray-sphere intersection
float intersect(vec3 rayOrigin, vec3 rayDir, vec3 sphereCenter, float radius)
{
  float a = dot(rayDir, rayDir);
  float b = dot(rayOrigin - sphereCenter, rayDir);
  float c = dot(rayOrigin - sphereCenter, rayOrigin - sphereCenter) - radius*radius;
  
  float discr = b*b - a*c;
  if(discr < 0.0)
    return -1.0;
  
    discr = sqrt(discr);
    float t0 = (-b - discr) / a;
    float t1 = (-b + discr) / a;
  
	return min(t0, t1);
}

//Blinn phong shading
vec4 getColor(vec3 p, vec3 center, vec4 diffuseColor, vec3 lightPosition)
{
  vec3 n = p - center;
  n = normalize(n);
  vec3 l = lightPosition - p;
  l = normalize(l);  
  vec3 v = cameraPosition - p;
  v = normalize(v);
  vec3 h = v + l;
  h = normalize(h);
  
  float cosDiff = dot(n,l);	
  vec4 toonColor = diffuseColor;
  if(cosDiff < 0.0)
	  toonColor = 0.05 * diffuseColor;
  else if(cosDiff < 0.25)
	  toonColor = 0.25 * diffuseColor;
  else if(cosDiff < 0.5)
	  toonColor = 0.5 * diffuseColor;
  else if(cosDiff < 0.75)
	  toonColor = 0.75 * diffuseColor;
  
  float cosSpec = dot(n,h);
  if(cosDiff > 0.98)
	  toonColor = toonColor + vec4(1.0,1.0,1.0,0.0);  	  
  
  return toonColor;	  
}  

vec4 drawSphere(vec3 rayPos, vec3 rayDir, vec3 pos, float r, vec4 color, vec3 light)
{
  float t = intersect(rayPos, rayDir, pos, r);
  if(t > -1.0)
    return getColor(rayPos + t*rayDir, pos, color, light);
  else
    return vec4(-1,-1,-1,-1);
}

float xPos(float t)
{
	float speedX = sin(cos(t)/10.0)/10.0;
	return 0.5 + sin(speedX * t)/3.0;
}

float yPos(float t)
{
	float speedY = cos(cos(t/4.0))/4.0;
    return 0.35 + cos(speedY * t)/8.0;
}

float zPos(float t)
{
	float speedZ = cos(sin(t/7.0))/7.0;
	return (zDepth + zMin)/2.0 + (-0.4*(zDepth + zMin))*sin(speedZ * t);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
  vec3 normalizedPosition = vec3(fragCoord.xy / iResolution.x, 0);
  vec2 normalizedMouse = iMouse.xy / iResolution.x;
  float randVal = rand(normalizedPosition.xy);  
	
  vec3 mouseRay = vec3(normalizedMouse, 0) - cameraPosition;	
  mouseRay = normalize(mouseRay);
	
  vec3 rayDir = normalizedPosition - cameraPosition;
  rayDir = normalize(rayDir);

  vec4 outColor = vec4(0.05,0.05,0.05,1);  	
  //Sphere
  
  vec3 pos = vec3(xPos(iGlobalTime),yPos(iGlobalTime),zPos(iGlobalTime));
  
  //Future Sphere
  float deltaFuture = 1.0;	
  vec3 posFuture = vec3(xPos(iGlobalTime + deltaFuture),yPos(iGlobalTime + deltaFuture),zPos(iGlobalTime + deltaFuture));
	
  float r = 0.15;

  vec4 color = vec4(1.0,0.0,0.0,1.0);
  
  //Mouse intersection
  float tMouse = intersect(cameraPosition, mouseRay, pos, r);
  if(tMouse > -1.0)
	  color = vec4(0.0,1.0,0.0,1.0);
  
  //Light
  vec3 lightPosition = vec3(0.5, 5.0, pos.z + 1.5);	
    
  
  //Y=0 plane
  float color0 = 0.7;	
  float t0 = -cameraPosition.y / rayDir.y;	
  vec3 p0 = cameraPosition + t0 * rayDir;
  if(p0.x > 0.0 && p0.x < 1.0 && p0.z > zDepth && p0.z < 0.0)	
  {
	  outColor = vec4(color0,color0,color0,1.0);	  
	  vec3 shadowDir = normalize(lightPosition - p0);
	  float tShadow = intersect(p0, shadowDir, pos, r);
	  if(tShadow > -1.0)
		 outColor = vec4(0.1,0.1,0.1,1.0);
		
  }
  //X=0 plane
  color0 = 0.5;	
  t0 = -cameraPosition.x / rayDir.x;	
  p0 = cameraPosition + t0 * rayDir;
  if(p0.y > 0.0 && p0.y < 1.0 && p0.z > zDepth && p0.z < 0.0)	
  {
	  outColor = vec4(color0,color0,color0,1.0);
	  vec3 refDir = reflect(rayDir, vec3(1,0,0));
	  refDir.y = refDir.y + randVal/80.0;
	  vec4 refColor = drawSphere(p0, refDir, posFuture, r, color, lightPosition);
	  if(refColor.x > -1.0)
  	  {
		 outColor = color0 * refColor; 
	  }
  }
  //X=1 plane
  color0 = 0.5;	
  t0 = (1.0-cameraPosition.x) / rayDir.x;	
  p0 = cameraPosition + t0 * rayDir;
  if(p0.y > 0.0 && p0.y < 1.0 && p0.z > zDepth && p0.z < 0.0)	
  {
	  outColor = vec4(color0,color0,color0,1.0);
	  vec3 refDir = reflect(rayDir, vec3(1,0,0));
	  refDir.y = refDir.y + randVal/80.0;
	  vec4 refColor = drawSphere(p0, refDir, posFuture, r, color, lightPosition);
	  if(refColor.x > -1.0)
  	  {
		 outColor = color0 * refColor; 
	  }
  }	
  //Draw sphere	
  vec4 sphereColor = drawSphere(cameraPosition, rayDir, pos, r, color, lightPosition);
  
  if(sphereColor.x > -1.0)
  {
    outColor = sphereColor;
  }
  
  	
  if(abs(length(normalizedPosition.xy - normalizedMouse)) < 0.01)
  	  outColor = mix(outColor, vec4(1.0,1.0,0.0,1.0), 0.5);
    
  fragColor = outColor;
  
}