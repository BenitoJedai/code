
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
 
int p = 43;

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

                        int sidewaysrange = 14;
                        int verticalrange = 22;
                        
float leg1up_sideway_deg;
float leg2up_sideway_deg;
float leg3up_sideway_deg;
float leg4up_sideway_deg;

float leg1down_vertical_deg ;
float leg2down_vertical_deg ;
float leg3down_vertical_deg ;
float leg4down_vertical_deg; 


                        
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
                 float t_accelerated = t * 8;
                            float mod = (pi * (_delay + 1 + hold + 1));

                            // error: invalid operands of types 'float' and 'float' to binary 'operator%'
                            float phase = (float)((int)(t_accelerated * 100) % (int)(mod * 100)) * 0.01f;

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
                            
     int po;
     
void program()
{
  
  // send data only when you receive data:
        if (Serial.available() > 0) {
                // read the incoming byte:
                 po = Serial.read();
        }
        
        if (po != 0)
            p = po;
        

        if (p == 43) program_43_high_five_calibration_stand();
        if (p == 53) program_53_mayday();
        if (p == 13) program_13_turn_left();
        if (p == 14) program_14_turn_right();
        if (p == 15) program_15_go_backwards();
        if (p == 16) program_16_go_forwards();
                        
  // if (p == 23) 
//  program_16_go_forwards ();
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
    
    ReadSensors();
//    ReadUltraSound();
    PrintValues();
    
   

  program();
} 
