
#include <Servo.h> 

float pi = 3.14;
float t = 0;
float f = 0.5;
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

void Walk(float time)
{
     
    leg1up_pos=20*sin(2*pi*f*t)+90;
    leg1down_pos=20*sin(2*pi*f*t+0.5*pi)+90;
    leg2up_pos=20*sin(2*pi*f*t)+90;
    leg2down_pos=20*sin(2*pi*f*t+0.5*pi)+90;
    leg3up_pos=20*sin(2*pi*f*t)+90;
    leg3down_pos=20*sin(2*pi*f*t-0.5*pi)+90;
    leg4up_pos=20*sin(2*pi*f*t)+90;
    leg4down_pos=20*sin(2*pi*f*t-0.5*pi)+90;
    
    
    leg1up.write(leg1up_pos);               
    leg1down.write(leg1down_pos);
    leg2up.write(leg2up_pos);              
    leg2down.write(leg2down_pos);
    leg3up.write(leg3up_pos);              
    leg3down.write(leg3down_pos);
    leg4up.write(leg4up_pos);              
    leg4down.write(leg4down_pos);
}
 
void loop() 
{ 
    t = float(millis()) / 1000;
    
    ReadSensors();
    ReadUltraSound();
    PrintValues();
    //Walk(t);

} 
