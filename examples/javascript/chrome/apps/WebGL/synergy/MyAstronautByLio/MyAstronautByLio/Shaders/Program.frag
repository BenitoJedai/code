// сделано на основе https://www.shadertoy.com/view/4sfGzj
const int MAX_ITER = 55;
//-------------------------------------------
#define time iGlobalTime

//--------------------------------------------------
// Вращение - пересчет матрицы
//--------------------------------------------------
vec3 rotationCoord(vec3 n, float paramRotate)
{
 vec3 result;
 //--------------------------------------------
   float t = time;
   vec2 sc = vec2(sin(t), cos(t));
   mat3 rotate;
   if(paramRotate <= 0.1)
   {

      rotate = mat3(  1.0,  0.0,  0.0,
                      0.0,  1.0,  0.0,
                      0.0,  0.0,  1.0);   
   }
   else if(paramRotate <= 1.0)
   {
      rotate = mat3(  1.0,  0.0,  0.0,
                      0.0, sc.y,-sc.x,
                      0.0, sc.x, sc.y);
   }
   else if(paramRotate <= 2.0)
   {
       rotate = mat3(  1.0,  0.0,  0.0,
                       0.0, sc.y,sc.x,
                       0.0, -sc.x, sc.y);  
   }
   else if (paramRotate <= 3.0)
   {
      rotate = mat3( sc.y,  0.0, -sc.x,
                     0.0,   1.0,  0.0,
                     sc.x,  0.0, sc.y);   
   }
   else if (paramRotate <= 4.0)
   {
      rotate = mat3( sc.y,  0.0, sc.x,
                     0.0,   1.0,  0.0,
                    -sc.x,  0.0, sc.y);   
   }   
   else if (paramRotate <= 5.0)
   {
       rotate = mat3( sc.y,sc.x,  0.0,
                     -sc.x, sc.y, 0.0,
                      0.0,  0.0,  1.0);  
   }   
   else if (paramRotate <= 6.0)
   {
       rotate = mat3( sc.y,-sc.x, 0.0,
                      sc.x, sc.y, 0.0,
                      0.0,  0.0,  1.0);  
   }     
   else
   {
   mat3 rotate_x = mat3(  1.0,  0.0,  0.0,
                          0.0, sc.y,-sc.x,
                          0.0, sc.x, sc.y);
   //sc = vec2(sin(t), cos(t));
   mat3 rotate_y = mat3( sc.y,  0.0, -sc.x,
                         0.0,   1.0,  0.0,
                         sc.x,  0.0,  sc.y);
  // sc = vec2(sin(t), cos(t));
   mat3 rotate_z = mat3( sc.y, sc.x,  0.0,
                        -sc.x, sc.y,  0.0,
                         0.0,  0.0,   1.0);
   rotate = rotate_z * rotate_y * rotate_z;                
   }
  result = n * rotate;
  return result;
}
//-------------------------------------------------- Получение цвета частей объекта
vec3 getmaterial(vec3 p, float mat)
{
   vec3 p1 = rotationCoord(p, 7.);
    
    if (mat < 0.5)
      return vec3(0.4662, 0.4565, 0.4488);
   else if (mat < 1.5)
      return vec3(0, 0, 0);   
   else if (mat < 2.5)
      return vec3(floor(length(floor(mod(p1, 2.0)+0.5))-0.5));
   else if (mat < 3.5)
      return vec3(0.8392, 0.0629,1.0);
   else if (mat < 4.5)
      return vec3(0.6289, 0.7216, 1.0);  
   return vec3(0.3, 0.9,0.5);
}
//--------------------------------------------------Скругленный целиндр
// capsule in Y axis
float capsuleY(vec3 p, float r, float h)
{
    p.y -= clamp(p.y, 0.0, h);
    return length(p) - r;
}
//--------------------------------------------------Конус
float dist_cone( vec3 p, float r, float h )
{
   vec2 c = normalize( vec2( h, r ) );
    float q = length(p.xy);
    return max( dot(c,vec2(q,p.z)), -(p.z + h) );
}
//--------------------------------------------------
float disttube(vec2 p, float r)
{
   return length(p) - r;
}
//----------------------------------------------------Шар
float distsphere(vec3 p, float r)
{
   return length(p) - r;
}
//----------------------------------------------------Тор
float disttorus(vec3 p, vec2 t)
{
   vec2 q = vec2(length(p.xz) - t.x, p.y);
   return length(q) - t.y;
}
//----------------------------------------------------Куб
float distbox(vec3 p, vec3 b)
{
   return length(max(abs(p) - b, 0.0));
}
//----------------------------------------------------Вращение детали
vec2 rotate(vec2 v, float a) {
   return vec2(cos(a)*v.x + sin(a)*v.y, -sin(a)*v.x + cos(a)*v.y);
}
//----------------------------------------------------

vec2 astronaut(vec3 p)
{
   float material = 0.;
 //  p.y -= tmpY;    // Изменение положения всего объекта по Y   
   vec3 r1 = vec3(rotate(p.xz, 0.1), p.y); // Тут можно отрегулировать наклон
   vec3 pAbs = p;
   // зеркало
   pAbs.x = abs(pAbs.x);
   float d = 1.;

   // head голова
 //  d = min(d, distsphere(p * tmpSize + tmpOffset, tmpRadius));
   d = min(d, distsphere(p * vec3(1.0) + vec3(0.0, 2.5, 0.0), 1.));
   float maska =   min(d, distsphere(p * vec3(1.2, 2.0, 1.8) + vec3(0, 4.7, -1.), 1.));
   // body тело
   d = min(d, distsphere(p * vec3(1.1, 0.5, 1.1) + vec3(0, 0.2, 0), 1.0));   
   // arms руки
   d = min(d, distsphere(pAbs * vec3(1.8, 1.2, 1.8) + vec3(-1.3, 0.7, 0), 1.0));
   float arms = min(d, distsphere(pAbs * vec3(1.9, -3.2, 1.9) + vec3(-1.0, 0.5, -0.4), 1.0)/1.6);   
   // feet ноги
   d = min(d, distsphere(pAbs * vec3(1.8, -0.6, 1.8) + vec3(-0.8, 0.9, 0), 1.0));
   float feet = min(d, distsphere(pAbs * vec3(3.0, -3., 1.6) + vec3(-1.4, 9.0, -0.5), 1.0)/1.6);
   //Рюкзак
   float baul = min(d, max(distbox(p + vec3(0, 0.8, 0.9), vec3(0.6)), 
                  disttorus(p.yzx + vec3(1.0, 1.4, 0), vec2(0.4, 0.8))));
    // Трубки
    float trubka =  min(d, max(distbox(p + vec3(0, 2.0, 0.9), vec3(0.7)), 
                  disttorus(pAbs.yzx + vec3(1.0, 1.4, 0.5), vec2(1.0, 0.1)))); 
   trubka = min(trubka, max(distsphere(r1 + vec3(0.9, 0.4, 1.6), 1.3), 
                  disttube(r1.xz + vec2(0.1, 1.85), 0.1)));    
   

   if (maska < d) material = 4.0; 
   else if (baul < d) material = 4.0;
   else if(trubka < d) material = 4.0; 
   else if(arms < d) material = 0.0;
   else if (feet < d)material = 0.0;
   else material = 3.0;
   
   d = min(d, maska); 
   d = min(d, baul); 
   d = min(d, trubka);        
   d = min(d, arms);  
   d = min(d, feet);     
   return vec2(d, material);
}
//--------------------------------------------------
//----------------------------------------------------
vec2 rocket(vec3 p)      
{
   p.y = 1.- p.y + 0.5;
   float material = 3.0;
   float korpus = 1., sopla = 1.;
   vec3 r1 = vec3(rotate(p.xz, 0.), p.y);    float d = 1.;
   d =   min(d, capsuleY( p -  vec3(0, 1., 0.0), 0.9, 2.3));     
   korpus = min(d, distbox(p -  vec3(0, 1.7, 0.0), vec3(0.75, 2.0, 0.75))); 
   korpus =   min(korpus, dist_cone(   r1 + vec3(0.0, 0., -5.5) , 0.9, 1.9 ));   

 vec3 pos = r1;
 pos.y = abs(pos.y); 
 korpus =  min(korpus, dist_cone( pos + vec3(0.50, -0.55, 0.0) , 0.5, 1.4 ));   
 korpus =  min(korpus, dist_cone( pos + vec3(-0.50, -0.55, 0.0) , 0.5, 1.4 ));   
 
  p.z = abs(p.z);
  korpus =   min(korpus, distsphere(p + vec3(0.00, -2.7, -0.5), 0.55));   
  korpus =   min(korpus, distsphere(p + vec3(0.00, -1.5, -0.5), 0.55));  
  //--------------------------------
  if(korpus < d)material = 4.0; 
  if(sopla < d)material = 3.0; 
  d = min(d, korpus); 
   return vec2(d, material);

}
//--------------------------------------------------
vec2 station(vec3 p)     
{

   vec3 r1 = vec3(rotate(p.xz, 1.55), p.y); 
   float material = 2.;
   float korpus = 1.;
   float d = 1.;
   korpus = disttorus(p.zxy  * vec3(1.06, -0.42, 1.), vec2(2.5, 0.2));
   korpus = min(korpus, disttorus(p.yzx * vec3(1.06, -0.42, 1.) , vec2(2.5, 0.2))); 
   korpus = min(korpus, disttorus(p  * vec3(1.06, -0.42, 1.), vec2(2.5, 0.2))); 
  d = min(d, distsphere(p  , 2.0));     
  d = min(d, distsphere(p - vec3(0.0, -2.5, 0.0) , 0.5));

  vec3 pAbs = p;
  pAbs.x = abs(pAbs.x); 
  d = min(d, distsphere(pAbs - vec3(2.5, 0.0, 0.0) , 0.4));

  pAbs = r1;
  pAbs.x = abs(pAbs.x);
  d = min(d, distsphere(pAbs - vec3(2.5, 0.0, 0.0) , 0.4)); 
  //--------------------------------
  if(korpus < d)material = 4.0; 
  d = min(d, korpus); 
  return vec2(d, material);

}

//-------------------------------------------------
// Скругленный квадрат
float lengthN(vec3 v, float n)
{
  vec3 tmp = pow(abs(v), vec3(n));
  return pow(tmp.x+tmp.y+tmp.z, 1.0/n);
}
//-------------------------------------------------
// вывод объекта
vec2 renderFunction(vec3 pos)
{
  vec3 pos1 = pos; 
 pos1 = rotationCoord(pos, 3.0);      // Пересчет координат для вращения
 vec2 result;
 vec2 astr =  astronaut(pos1);

 pos1 = rotationCoord(pos, 2.0); 
 pos1.x -= 8.5;
 vec2 roket = rocket(pos1);
 
 pos1 = rotationCoord(pos, 7.0);  
 pos1.x += 4.0;  
 vec2 stat = station(pos1);

  result = (astr.x < roket.x) ? astr : roket;
  result = (result.x < stat.x) ? result : stat;
  return result;


}
//-------------------------------------------------
vec3 getNormal(vec3 p)
{
  const float d = 0.0001;
  return
    normalize
    (
      vec3
      (
        renderFunction(p+vec3(d,0.0,0.0)).x - renderFunction(p+vec3(-d,0.0,0.0)).x,
        renderFunction(p+vec3(0.0,d,0.0)).x - renderFunction(p+vec3(0.0,-d,0.0)).x,
        renderFunction(p+vec3(0.0,0.0,d)).x - renderFunction(p+vec3(0.0,0.0,-d)).x
      )
    );
}
//-------------------------------------------------
vec3 getlighting(in vec3 pos, in vec3 normal, in vec3 lightDir, in vec3 color)
{
   float b = max(0.0, dot(normal, lightDir));
   return b * color;
}
//-------------------------------------------------
vec3 getlightingPhong(in vec3 pos,in vec3 camPos, in vec3 normal, in vec3 lightDir, in vec3 color)
{
   vec3  specColor = vec3(0.2126, 0.9023, 0.2128);
   float specPower = 12.0;
    
    vec3   l = normalize (lightDir-pos);                  
    vec3   v = normalize(camPos-pos);
    vec3   n = normalize (normal); 
    vec3   r = reflect ( -l, n ); 
    vec3 diff = color * max ( dot ( n, l ), 0.0 );
    vec3 spec = specColor * pow ( max ( dot ( l, r ), 0.0 ), specPower );
    
    return diff + spec;
}
//-------------------------------------------------
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
  vec2 pos =   ( 2.0 * fragCoord.xy - iResolution.xy ) / iResolution.y;
  pos.y = 1.0 - pos.y - 1.0;
  
  vec3 camPos =  vec3(0.0, 0.0, 9.0);
  vec3 camDir = vec3(0.0, 0.0, -1.0);
  vec3 camUp = vec3(0.0, 1.0, 0.0);  
  vec3 camSide = cross(camDir, camUp);
  float focus = 1.8;//1.8
 
  vec3 rayDir = normalize(camSide*pos.x + camUp*pos.y + camDir/* *focus*/);
 
  float t = 0.0;
  vec2 object = vec2(1., 1.);
//------------------------------

  vec3 posOnRay = camPos;
//------------------------------ 
  for(int i=0; i<MAX_ITER; ++i)
  {

    object = renderFunction(posOnRay);
    t += object.x;
    posOnRay = camPos + t*rayDir;    
  }
//------------------------------ 

  if(abs(object.x) < 0.01)
  {
    vec3 materialColor = getmaterial(posOnRay.xyz, object.y); 
    vec3 normal = getNormal(posOnRay);                        
  //  vec3 lightDir = -rayDir;
  //  vec3 color = getlighting(posOnRay.xyz, normal, lightDir, materialColor);  

  vec3 lightDir =  vec3(5.0, 0.0, 4.0);
  vec3 color = getlightingPhong(posOnRay, camPos, normal, lightDir, materialColor); // По Фонгу   
  
  fragColor = vec4(color, 1.0);
  }else
  {
   fragColor = vec4(0.5);// 
  }
}
  
