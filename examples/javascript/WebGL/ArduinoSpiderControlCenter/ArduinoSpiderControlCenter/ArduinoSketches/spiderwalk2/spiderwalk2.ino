
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
int leg4up_pos = 75;    
int leg4down_pos = 90;

int legtotal=90;
int legdown_1=100;
int legdown_2=80;

int LeftIRValue = 0;
int RightIRValue = 0;
int LeftLSValue = 0;
int RightLSValue = 0;
long FrontUSValue = 0;
 
 // stand? // 70 avoid // go to light 60
int p = 70;
 // walk?
int po = 0;
int pp = 0;

int counter = 0;


                        int sidewaysrange = 20;
                        int verticalrange = 30;
                        
float leg1up_sideway_deg;
float leg2up_sideway_deg;
float leg3up_sideway_deg;
float leg4up_sideway_deg;

float leg1down_vertical_deg ;
float leg2down_vertical_deg ;
float leg3down_vertical_deg ;
float leg4down_vertical_deg; 


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

int skip = 0;

void PrintValues()
{
  skip++;
  
  if (skip < 90)
     return;
  
  skip = 0;
  
  // Serial API will cost CPU!


  
  /*
      Serial.print(";\t counter: ");
      Serial.print(counter);
      */
    Serial.print(";\t t: ");
  Serial.print(t);
  
  
  
  Serial.print(";\t p: ");
  Serial.print(p);  
  Serial.print(";\t po: ");
  Serial.print(po);  
  Serial.print(";\t pp: ");
  Serial.print(pp);    

  Serial.print(";\t LI: ");
  Serial.print(LeftIRValue);
  Serial.print(";\t RI: ");
  Serial.print(RightIRValue);
  
  // 0 is light, 900 is dark
  Serial.print(";\t LS: ");
  Serial.print(LeftLSValue);
  Serial.print(";\t RS: ");
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


                        
void program_23_high_five_calibration_far()
{
                                leg1up_sideway_deg = sidewaysrange;
                                leg2up_sideway_deg = -sidewaysrange;
                                leg3up_sideway_deg = -sidewaysrange;
                                leg4up_sideway_deg = sidewaysrange;

                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;
    
}

void program_33_high_five_calibration()
{
      
                                leg1up_sideway_deg = -sidewaysrange;
                                leg2up_sideway_deg = sidewaysrange;
                                leg3up_sideway_deg = sidewaysrange;
                                leg4up_sideway_deg = -sidewaysrange;
      
                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;
  
    
}

 void program_43_high_five_calibration_stand()
                            {
                                leg1up_sideway_deg = -sidewaysrange;
                                leg2up_sideway_deg = sidewaysrange;
                                leg3up_sideway_deg = sidewaysrange;
                                leg4up_sideway_deg = -sidewaysrange;

                                leg1down_vertical_deg = 0;
                                leg2down_vertical_deg = 0;
                                leg3down_vertical_deg = 0;
                                leg4down_vertical_deg = 0;
                            }

    void program_53_mayday()
                            {
                                leg1down_vertical_deg = verticalrange;
                                leg2down_vertical_deg = verticalrange;
                                leg3down_vertical_deg = verticalrange;
                                leg4down_vertical_deg = verticalrange;

                                leg1up_sideway_deg = -cos(t * 6) * sidewaysrange;
                                leg2up_sideway_deg = cos(t * 6) * sidewaysrange;
                                leg3up_sideway_deg = cos(t * 6) * sidewaysrange;
                                leg4up_sideway_deg = -cos(t * 6) * sidewaysrange;

                                
                            }


  void program_leg0 (float tphase, float* notify_x, float* notify_y) 
  {
      float deg_sideway = (cos(tphase) * sidewaysrange);
      float deg_vertical = max(0, sin(tphase) * verticalrange);

      *notify_x = deg_sideway;
      *notify_y = deg_vertical;

  }
         
         
 void program_leg__delay_move_hold_commit  (int _delay, int hold, int reverse, float* notify_x, float* notify_y) 
                        {
                          float speedup = 12;
                 float t_accelerated = t * 1;
                            float mod = (pi * (_delay + 1 + hold + 1)) / 9;

                            // error: invalid operands of types 'float' and 'float' to binary 'operator%'
                            float phase = (float)((int)(t_accelerated * 100) % (int)(mod * 100)) * 0.01f;

                            phase = phase * 9;

                            // _delay
                            if (phase < (pi * _delay))
                            {
                              if (reverse > 0)
                                    phase = pi;
                                else
                                    phase = 0;

                                program_leg0(phase, notify_x, notify_y);
                                return;
                            }

                            phase -= (pi * _delay);


                            // move
                            if (phase < (pi))
                            {
                                if (reverse > 0)
                                    phase = pi - phase;
                                program_leg0(phase, notify_x, notify_y);
                                return;
                            }

                            phase -= (pi);



                            // _delay
                            if (phase < (pi * hold))
                            {
                                if (reverse > 0)
                                    phase = 0;
                                else
                                    phase = pi;

                                program_leg0(phase, notify_x, notify_y);
                                return;
                            }

                            phase -= (pi * hold);

                            if (reverse >0)
                                phase = pi - phase;

                            // commit
                            program_leg0((pi + phase), notify_x, notify_y);

                        }         
                            
 void program_13_turn_left  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 0, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 0, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 0, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 0, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }

 void program_14_turn_right  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 1, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 1, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 1, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 1, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }

         void program_15_go_backwards  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 0, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 1, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 0, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 1, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }
      void program_16_go_forwards  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 1, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 0, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 1, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 0, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }                
                      
void program_17_go_left  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 1, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 1, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 0, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 0, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }                         
      void program_18_go_right  () 
                            {
                                program_leg__delay_move_hold_commit(1, 2, 0, &leg1up_sideway_deg , &leg1down_vertical_deg );
                                program_leg__delay_move_hold_commit(3, 0, 0, &leg2up_sideway_deg , &leg2down_vertical_deg );
                                program_leg__delay_move_hold_commit(2, 1, 1, &leg3up_sideway_deg , &leg3down_vertical_deg );
                                program_leg__delay_move_hold_commit(0, 3, 1, &leg4up_sideway_deg , &leg4down_vertical_deg );                                
                            }                        

int program_60_walk_to_light_skip = 0;

 void program_60_walk_to_light  () 
          {
            // light on right: LS: 956;	 RS: 777;

if (LeftLSValue < 600 || RightLSValue < 600)
{
 program_43_high_five_calibration_stand();
return; 
}


int sensitivity = 200;

if ((LeftLSValue - RightLSValue) > sensitivity)
{
                        program_14_turn_right();  
return;
}


if ((LeftLSValue - RightLSValue) < -sensitivity)
{
                              program_13_turn_left();
return;
}



            program_16_go_forwards();
          }
          
     int program_70_walk_or_mayday_skip = 0;
     int program_70_walk_or_mayday_avoid = 0;
     
 void program_70_walk_or_mayday  () 
          {
/* obstacle on left
 t: 100.16;
 p: 16;
 po: 43;
 pp: 43;
 LI: 240;
 RI: 64;
 LS: 826;
 RS: 892;
 
 on right:
 ;
 t: 147.36;
 p: 16;
 po: 43;
 pp: 43;
 LI: 18;
 RI: 204;
 LS: 895;
 RS: 865
 
 on both:
 ;
 t: 184.00;
 p: 16;
 po: 43;
 pp: 43;
 LI: 211;
 RI: 218;
 LS: 836;
 RS: 866;
*/ 


           if (LeftIRValue > 200 || RightIRValue > 200)
           {
             program_70_walk_or_mayday_skip = min(80, program_70_walk_or_mayday_skip + 1);
             
           }
            else
            {
               program_70_walk_or_mayday_skip = max(0, program_70_walk_or_mayday_skip - 3);
             
          }             

if (program_70_walk_or_mayday_avoid != 0)          
{
      program_71_avoid_collision  () ;
      return;
}
          
          if (program_70_walk_or_mayday_skip < 40)
              program_60_walk_to_light();
              else
              program_71_avoid_collision  () ;
           
           
}


void program_71_avoid_collision  () 
{
  // /;)
  

if (program_70_walk_or_mayday_avoid == 0)
{
   if (           LeftIRValue < RightIRValue)
  program_70_walk_or_mayday_avoid = -2000;
  else
    program_70_walk_or_mayday_avoid = 2000;
}
else
{
  if (program_70_walk_or_mayday_avoid < 0)
    program_70_walk_or_mayday_avoid =   min(0, program_70_walk_or_mayday_avoid + 1);
  else
  program_70_walk_or_mayday_avoid =   max(0, program_70_walk_or_mayday_avoid - 1);
}

  if (abs(program_70_walk_or_mayday_avoid) < 1500 && abs(program_70_walk_or_mayday_avoid) > 500)
  {

  if (           program_70_walk_or_mayday_avoid < 0)
    program_13_turn_left();
                        else 
                        program_14_turn_right();  

  }
  else
  {
program_43_high_five_calibration_stand();
  }
  return;
  
  
}
     
void program()
{
  
  // send data only when you receive data:
  
  // is this causing freeze bug?
        if (Serial.available() > 0) {
                // read the incoming byte:
                 po = Serial.read();
        }

        if (p != 0)
            pp = p;
            
        if (po != 0)
            pp = po;
        

                        if (pp == 43) program_43_high_five_calibration_stand();
                        if (pp == 53) program_53_mayday();
                        if (pp == 13) program_13_turn_left();
                        if (pp == 14) program_14_turn_right();
                        if (pp == 15) program_15_go_backwards();
                        if (pp == 16) program_16_go_forwards();
                        if (pp == 17) program_17_go_left();
                        if (pp == 18) program_18_go_right();
if (pp == 60) program_60_walk_to_light();
if (pp == 70) program_70_walk_or_mayday();
                        
                        
                        
  // if (p == 23) 
  // program_16_go_forwards ();
//  program_15_go_backwards();
//  program_13_turn_left();
    // program_14_turn_right();
//  program_43_high_five_calibration_stand();
  // program_53_mayday();
//   program_23_high_five_calibration_far();
// program_33_high_five_calibration();
   
       // yay :) nothing to do anymore
      leg1down.write(leg1down_pos + 13 - leg1down_vertical_deg); // RED
      leg1up.write(leg1up_pos - 14 - leg1up_sideway_deg);
      
      leg2down.write(leg2down_pos - 13 + leg2down_vertical_deg); // GREEN
      leg2up.write(leg2up_pos + 14 - leg2up_sideway_deg);
      
      leg3down.write(leg3down_pos - 13 + leg3down_vertical_deg); // BLUE
      leg3up.write(leg3up_pos + 14 - leg3up_sideway_deg);
      
      leg4down.write(leg4down_pos + 13 - leg4down_vertical_deg); // WHITE    
      leg4up.write(leg4up_pos - 14 - leg4up_sideway_deg);
}

void loop() 
{ 
    t = float(millis()) / 1000;

   // counter = ((int)t) % 24;
    
    ReadSensors();
//    ReadUltraSound();
    PrintValues();
    
   

  program();
} 
