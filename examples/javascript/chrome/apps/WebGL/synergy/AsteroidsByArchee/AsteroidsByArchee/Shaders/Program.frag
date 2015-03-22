vec2 sp;
vec3 color;
const float pi = 3.14159265359;
float linethickness = 2.0/iResolution.x;
float time;
const float aspect = 16.0/9.0;

float noize(float x)
{
    return fract(sin(x*9342.0)*1435.0);
}
float noize2(float x)
{
    return fract(cos(x*932.0)*2435.0);
}
vec2 noize2d(float x)
{
    return vec2(noize(x),noize2(x));
}

void draw(float aaval,vec3 color2)
{
    color = max(color,color2*aaval);
//    color = (color2-color)*aaval;
}

    
void asteroid(vec2 pos,float rot,float size,float shape)
{
 	vec2 rp = (fract((sp-pos)*vec2(1.0,aspect)+vec2(0.5,0.5)   )-vec2(0.5,0.5))/vec2(1.0,aspect);
    
   if ( length(rp) > size) return; // bounding disk does wonder to FPS
    
    float angle = fract(atan( (rp).x, (rp).y )/pi/2.0+rot);
    float rad = (clamp(sin(angle*pi*7.0)*2.0,-1.0,1.0)+4.0)/10.0 +
        (clamp(sin(angle*pi*(12.0)+shape)*2.0,-1.0,1.0)+4.0)/10.0;
    
    rad *= size;
    float dist = length(rp);
    
    float aaval = 1.0-abs(dist-rad)/linethickness;
    
    draw(aaval,vec3(1.0,shape>1.0?1.0:0.0,shape>0.0?1.0:0.0));
}

vec3 linecolor;
void line(vec2 a,vec2 b)
{
    vec2 s = (fract((sp-a)*vec2(1.0,aspect)+vec2(0.5,0.5))-vec2(0.5,0.5))/vec2(1.0,aspect)+a;
    if ( dot(b-a,s-a) < 0.0 || dot(b-a,s-a)>dot(b-a,b-a)  ) return;
    float aaval = (1.0-(abs( (a.x-b.x)*(s.y-a.y)-(a.y-b.y)*(s.x-a.x)  )/linethickness/length(a-b)*2.0));
    draw(aaval,linecolor);
}

vec2 rotate(vec2 pos,float rot)
{
    return vec2( cos(rot)*pos.x + sin(rot)*pos.y, cos(rot)*pos.y - sin(rot)*pos.x);
}


float rotvel0;
float rot0;
vec2 pos0;
vec2 vel0;
float timeleft;
float currentaccel;


void ship(vec2 pos,float rot,float size)
{
    line(pos+rotate(vec2(0,1.0),rot)*size,pos+rotate(vec2(0.5,-0.3)*size,rot));
    line(pos+rotate(vec2(0,1.0),rot)*size,pos+rotate(vec2(-0.5,-0.3)*size,rot));
    line(pos+rotate(vec2(0.5,-0.1),rot)*size,pos+rotate(vec2(-0.5,-0.1)*size,rot));
	
    linecolor = vec3(1.0,1.0,0.0);
    if (currentaccel!=0.0 && fract(time*8.0)<0.5)
    {
    line(pos+rotate(vec2(0,-1.0),rot)*size,pos+rotate(vec2(0.25,-0.2)*size,rot));
    line(pos+rotate(vec2(0,-1.0),rot)*size,pos+rotate(vec2(-0.25,-0.2)*size,rot));
    }
    
    
}

float timepast;
void move(float accel,float rotaccel,float t)
{
    timepast += t;
    if (timeleft<t) t = timeleft;
    if (timeleft<0.01) return;
    

/*    float dd = 0.01;
   vec2 pos0d =  (
       (vec2(sin(rot0+rotvel0*(t-dd)),cos(rot0+rotvel0*(t-dd)))-vec2(sin(rot0),cos(rot0)))*accel/rotvel0/rotvel0
        + pos0 + (vel0-vec2(cos(rot0),-sin(rot0))*accel/rotvel0 )*(t-dd));*/
    
   pos0 =  (
       (vec2(sin(rot0+rotvel0*t),cos(rot0+rotvel0*t))-vec2(sin(rot0),cos(rot0)))*accel/rotvel0/rotvel0
       + pos0 + (vel0-vec2(cos(rot0),-sin(rot0))*accel/rotvel0 )*t);

//    vel0 = (pos0-pos0d)/dd;
  vel0 = (vel0-vec2(cos(rot0)-cos(rot0+rotvel0*t),-sin(rot0)+sin(rot0+rotvel0*t))*accel/rotvel0 );
    
   rot0 =  rot0 + rotvel0*t + rotaccel*t*t*0.5;
    rotvel0 = rotvel0 + rotaccel*t;
    
    timeleft -= t;
    currentaccel = accel;
}

void  shipmove(float t)
{
	 pos0 = vec2(0.5,0.3); // START POS
	 vel0 = vec2(0.0,0.0);
    rot0=0.0;
    rotvel0 = 0.1;
    timeleft = t;
    timepast = 0.0;

    move(0.0,-1.0,2.0); // turn left
    
    move(-0.15,0.0,0.5); // accel
    
    move(0.0,1.0,3.0);  // turn right for 2.3 secs
    
    move(0.0,0.0,1.5); // do nothing
    move(-0.15,0.0,1.4); // accel
    
    move(0.0,0.0,2.0); // do nothing
    move(0.0,-1.0,1.8); // turn left
    move(-0.15,0.0,1.4); // accel

    move(0.0,0.0,17.0-timepast); // do nothing
    move(-0.15,0.0,2.0); // accel
    move(0.0,0.0,2.0); // do nothing
    move(-0.15,0.0,2.4); // accel
    
    move(0.0,1.0,2.9);  
    move(0.0,0.0,30.0-timepast); // do nothing
    move(-0.15,0.0,4.0); // accel
    move(0.0,-1.0,3.5);  
    move(-0.15,0.0,3.0); // accel
    
    move(0.0,0.0,9999.0);
    
}

float deathtime = 41.4;

vec2 exploc;
float exptime=-99.0;

void explosion(vec2 location,float t)
{
    if (t<time && t>exptime)
    {
        exptime = t;
        exploc = location;
    }
}

void bullet(float phase)
{
    float shottime = fract(time+phase);
    float timeofshot = time-shottime;
   if (timeofshot<3.0 || timeofshot>deathtime) return;
    
	shipmove(timeofshot);
    
    float bulletspeed = 0.35;
    float bulletlength = 0.025;
    vec2 shotdir = vec2(sin(rot0),cos(rot0));
    pos0 += (vel0+shotdir*bulletspeed)*shottime + shotdir*0.03;
    line(pos0,pos0+shotdir*bulletlength);
    
}

void movingasteroid(float shape,vec2 vel,float explodetime,float die0,float die1)
{
    float rotvel = 0.1-fract(shape*0.31)*0.2;
    float size = 0.05;
    vec2 pos = vel*time;
    if (explodetime<time) 
    {
        explosion(vel*explodetime,explodetime);
        
        size *= 0.71;
        pos += vec2(0.05,0.02)*(time-explodetime);
		if (die1>time) asteroid(pos,-0.2*(time-explodetime),size,shape);        
        
        explosion(vel*die1 + vec2(0.05,0.02)*(die1-explodetime),die1);
        
        pos += vec2(0.05,0.02)*(time-explodetime)*-2.0;
        
        explosion(vel*die0 + -vec2(0.05,0.02)*(die0-explodetime),die0);
    }
    if (die0>time) asteroid(pos,time*rotvel+0.1*(time-explodetime),size,shape);
}

void drawexplosion()
{
    if (exptime+0.5<time) return;
    
    float ltime = time-exptime;
    float maxrad = 0.16*ltime+0.05;
    
    if (maxrad<length((fract((sp-exploc)*vec2(1.0,aspect)+vec2(0.5,0.5)   )-vec2(0.5,0.5))/vec2(1.0,aspect))) return;
        
    for(float a=0.0;a<6.2;a+=0.5)
    {
        vec2 dir=vec2(cos(a),sin(a));
    	float expspeed = 0.13+sin(a*5.0)*0.02;
        line(exploc+dir*ltime*expspeed,exploc+dir*(ltime*expspeed+0.04));
    }
    
}


float endtime = deathtime+5.0;
void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    time = iGlobalTime+0.0;
    
    time = mod(time,4.0+endtime*1.25);
    if (time>endtime)
    {
        if (time>2.0+endtime)
        {
            time = max(endtime - (time-2.0-endtime)*4.0,0.0);
            
        }
        else
            time = endtime;
    }
    
    color = vec3(0,0,0);
	sp = fragCoord.xy / iResolution.xy;
    sp.y /= aspect;
 
    
    shipmove(time);

	linecolor = vec3(1.0,1.0,1.0);    
    if (time<deathtime) ship(pos0,rot0,0.05);

    linecolor = vec3(1.0,1.0,1.0);    
    bullet(0.0);
    bullet(0.2);
    bullet(0.4);
  
    movingasteroid(0.0, vec2(0.1,0.1), 5.7,27.0,18.0); // red
    movingasteroid(1.0, vec2(-0.07,-0.1), 4.5,39.5,9.5); // purple
    movingasteroid(2.0, vec2(-0.09,0.05), 6.6,99.4,23.0); // white
    
    shipmove(deathtime);
    explosion(pos0,deathtime);
    
    linecolor = vec3(1.0,1.0,0.0);
    drawexplosion();
    
	fragColor = vec4(color.x,color.y,color.z,0);
}