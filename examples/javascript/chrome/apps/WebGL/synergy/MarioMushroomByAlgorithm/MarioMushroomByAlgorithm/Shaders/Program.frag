#define B 0.
#define d 1.
#define o 2.
#define _ 3.
#define R iResolution
#define C fragCoord
#define F float
#define Q(x,a,b,c,d,e,f,g,h) if(y==x)m=(a+4.*(b+4.*(c+4.*(d+4.*(e+4.*(f+4.*(g+h*4.)))))));

void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    F t=step(abs(C.x-R.x/2.),R.y/2.), x=(C.x-(R.x-R.y)/2.)/R.y, m=0.;
    int y = int((1.-C.y/R.y)*16.);
	Q(0, _,_,_,_,_,B,B,B)
	Q(1, _,_,_,B,B,B,d,o)
	Q(2, _,_,B,B,d,d,d,o)
	Q(3, _,B,B,o,d,d,o,o)
	Q(4, _,B,d,o,o,o,o,o)
	Q(5, B,B,d,d,o,o,d,d)
	Q(6, B,d,d,d,o,d,d,d)
	Q(7, B,d,d,d,o,d,d,d)
	Q(8, B,d,d,o,o,d,d,d)
	Q(9, B,o,o,o,o,o,d,d)
	Q(10,B,o,o,B,B,B,B,B)
	Q(11,B,B,B,B,_,_,B,_)
	Q(12,_,B,B,_,_,_,B,_)
	Q(13,_,_,B,_,_,_,_,_)
	Q(14,_,_,B,B,_,_,_,_)
	Q(15,_,_,_,B,B,B,B,B)
    fragColor=vec4(t*mod(floor(m/pow(4.,F(int((x*.5-step(.5,x)*(x-.5))*32.)))),4.)/3.+(1.-t)*1.);
}