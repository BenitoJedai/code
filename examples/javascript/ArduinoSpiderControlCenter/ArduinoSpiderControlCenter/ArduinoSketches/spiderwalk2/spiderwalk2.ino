
#include <Servo.h> 

float pi = 3.14;
float t = 0;
float f = 0.1;
long LastUStime = 0; 
int USperiod = 500;
 
Servo leg1up; 
Servo leg1down;
Servo leg2up; 
Servo leg2down;
Servo leg3up; 
Servo leg3down;
Servo leg4up; 
Servo leg4down;

const int LeftIRpin = A0;
const int RightIRpin = A1;
const int LeftLSpin = A3;
const int RightLSpin = A4;
const int FrontUSpin = 3;
 
int leg1up_pos = 85;    
int leg1down_pos = 100;
int leg2up_pos = 90;   
int leg2down_pos = 80;
int leg3up_pos = 80;    
int leg3down_pos = 90;
int leg4up_pos = 90;    
int leg4down_pos = 90;

int legtotal=90;
int legdown_1=100;
int legdown_2=80;

int LeftIRValue = 0;
int RightIRValue = 0;
int LeftLSValue = 0;
int RightLSValue = 0;
long FrontUSValue = 0;
 
int p = 1;

void setup() 
{ 
  leg1up.attach(5);
  leg1down.attach(6);
  leg2up.attach(7);
  leg2down.attach(8);
  leg3up.attach(10);
  leg3down.attach(11);
  leg4up.attach(12);
  leg4down.attach(13);
  
  leg1up.write(leg1up_pos);               
  leg1down.write(leg1down_pos);
  leg2up.write(leg2up_pos);              
  leg2down.write(leg2down_pos);
  leg3up.write(leg3up_pos);              
  leg3down.write(leg3down_pos);
  leg4up.write(leg4up_pos);              
  leg4down.write(leg4down_pos);
  
  Serial.begin(9600); 
} 
 
void ReadSensors()
{
  LeftIRValue = analogRead(LeftIRpin);
  RightIRValue = analogRead(RightIRpin);
  LeftLSValue = analogRead(LeftLSpin);
  RightLSValue = analogRead(RightLSpin);
}

void PrintValues()
{
  Serial.print("Front: ");
  Serial.print(FrontUSValue);
  Serial.print(";\t LeftIR: ");
  Serial.print(LeftIRValue);
  Serial.print(";\t RightIR: ");
  Serial.print(RightIRValue);
  
  // 0 is light, 900 is dark
  Serial.print(";\t LeftLS: ");
  Serial.print(LeftLSValue);
  Serial.print(";\t RightLR: ");
  Serial.print(RightLSValue);
  Serial.print(";\t t: ");
  Serial.print(t);
  Serial.print(";\t p: ");
  Serial.print(p);  
  Serial.println(";");
}

void ReadUltraSound()
{
  long duration;
  long PassedTime;
  long CurrentTime = millis();
  
  PassedTime = CurrentTime - LastUStime;
  
  if(PassedTime > USperiod){ 
    pinMode(FrontUSpin, OUTPUT);
    digitalWrite(FrontUSpin, LOW);
    delayMicroseconds(2);
    digitalWrite(FrontUSpin, HIGH);
    delayMicroseconds(5);
    digitalWrite(FrontUSpin, LOW);
    pinMode(FrontUSpin, INPUT);
    duration = pulseIn(FrontUSpin, HIGH);
    FrontUSValue = duration / 29 / 2;
    LastUStime = millis();
  }
}

void Walk01()
{
     if (t > 120)
     {
       // yay :) nothing to do anymore
      leg1down.write(109 - 25); // raise RED leg
      leg2down.write(60); // put that leg more into dirt
      leg3down.write(70);
      leg4down.write(109);       
       return;  
     }
     
   
     if (t > 40)
     {
       // waiting for my time:) 
       // load your debugger!
       p = 2;
       return;
     }

     if (t > 13.2)
       Walk01_both_down_onside();
     else if (t > 12.8)
       Walk01_both_up();
     else if (t > 12.4)
       Walk01_both_down();
     else if (t > 12)
       Walk01_both_up();
     else if (t > 11.6)
       Walk01_both_down();
     else  if (t > 11.2)
       Walk01_L_up();
     else if (t > 10.8)
       Walk01_R_up();
     else    if (t > 10.4)
       Walk01_L_up();
     else if (t > 10.0)
       Walk01_R_up();       
}

void Walk01_L_up()
{
         // lets get up before working out..
      leg1down.write(109 /* leg up */ - 15 * 1);
      leg1up.write(85 /* leg to front */  - 30);
      
      leg2down.write(60 /* leg up */ - 15 * 0);
      leg2up.write(90 /* leg to front */ + 30);
      
      leg3down.write(70);
      leg4down.write(109);

}

void Walk01_R_up()
{
    // lets get up before working out..
      leg1down.write(109 /* leg up */ - 15 * 0);
      leg1up.write(85 /* leg to front */  - 30);
      
      leg2down.write(60 /* leg up */ + 15 * 1);
      leg2up.write(90 /* leg to front */ + 30);
      
      leg3down.write(70);
      leg4down.write(109);
}

void Walk01_both_up()
{
    // lets get up before working out..
      leg1down.write(109 /* leg up */ - 15 * 1);
      leg1up.write(85 /* leg to front */  - 30);
      
      leg2down.write(60 /* leg up */ + 15 * 1);
      leg2up.write(90 /* leg to front */ + 30);
      
      leg3down.write(70);
      leg4down.write(109);
}

void Walk01_both_down()
{
    // lets get up before working out..
      leg1down.write(109 /* leg up */ - 15 * 0);
      leg1up.write(85 /* leg to front */  - 30);
      
      leg2down.write(60 /* leg up */ + 15 * 0);
      leg2up.write(90 /* leg to front */ + 30);
      
      leg3down.write(70);
      leg4down.write(109);
}

void Walk01_both_down_onside()
{
    // lets get up before working out..
      leg1down.write(109 /* leg up */ - 15 * 0);
      leg1up.write(85 /* leg to front */  - 30 * 0);
      
      leg2down.write(60 /* leg up */ + 15 * 0);
      leg2up.write(90 /* leg to front */ + 30 * 0);
      
      leg3down.write(70);
      leg4down.write(109);
}

void Walk00()
{
     if (t > 160)
     {
       p = 1;
       return;
     }
   
    // RED: OK
//     leg1up_pos=20*sin(2*pi*f*t)+90;
    leg1down_pos=30*sin(2*pi*f*t+0.5*pi)+110;
    
    // GREEN: // this is in reverse
//    leg2up_pos=20*sin(2*pi*f*t)+90;
    leg2down_pos=30*sin(2*pi*f*t-0.5*pi)+60;
    
    
    
    
    // BLUE: OK // this is in reverse
//    leg3up_pos=30*sin(2*pi*f*t)+110;
    leg3down_pos=30*sin(2*pi*f*t-0.5*pi)+80;
    
    // WHITE OK:
//    leg4up_pos=20*sin(2*pi*f*t)+90;
    leg4down_pos=30*sin(2*pi*f*t+0.5*pi)+100;
    
    
    
  
    // RED    
    //leg1up.write(leg1up_pos);    
    leg1down.write(leg1down_pos);
          
    
    // GREEN
    //leg2up.write(leg2up_pos);        
    leg2down.write(leg2down_pos);


    // BLUE    
    //leg3up.write(leg3up_pos);    
    leg3down.write(leg3down_pos);
            
    
    // WHITE
    //    leg4up.write(leg4up_pos);      
    leg4down.write(leg4down_pos);
	
}

void Walk02()
{
     if (t > 120)
     {
       p = 1;
       return;
     }
   
    // RED: OK
//     leg1up_pos=20*sin(2*pi*f*t)+90;
    leg1down_pos=10*sin(2*pi*f*t+0.5*pi)+110-10;
    
    // GREEN: // this is in reverse
//    leg2up_pos=20*sin(2*pi*f*t)+90;
    leg2down_pos=10*sin(2*pi*f*t-0.5*pi)+60+10;
    
    
    
    
    // BLUE: OK // this is in reverse
//    leg3up_pos=30*sin(2*pi*f*t)+110;
    leg3down_pos=20*sin(2*pi*f*t-0.5*pi)+80+10;
    
    // WHITE OK:
//    leg4up_pos=20*sin(2*pi*f*t)+90;
    leg4down_pos=20*sin(2*pi*f*t+0.5*pi)+100-10;
    
    
    
  
    // RED    
    //leg1up.write(leg1up_pos);    
    leg1down.write(leg1down_pos);
          
    
    // GREEN
    //leg2up.write(leg2up_pos);        
    leg2down.write(leg2down_pos);


    // BLUE    
    //leg3up.write(leg3up_pos);    
    leg3down.write(leg3down_pos);
            
    
    // WHITE
    //    leg4up.write(leg4up_pos);      
    leg4down.write(leg4down_pos);
	
}

void loop() 
{ 
    t = float(millis()) / 1000;
    
    ReadSensors();
//    ReadUltraSound();
    PrintValues();
    
    if (p == 0) Walk00();
    if (p == 1) Walk01();
    if (p == 2) Walk02();

} 
