/* this is my version of the Steam logo
 * I didn't even look at someone's code, so no infringement intended :p
 *
 * Célestin Marot (yakoudbz)
 *
 *********************/

// parameters (can be modified easily ! )
const float r0=0.5;   // radius of big circle
const float sr0=r0*0.6+0.06;
const float vsr0=r0*0.5;
const float L0=1.0;   // lenght of the crank
const float r1=0.33;   // radius of satelite
const float sr1=r1*0.6+0.06;
const float vsr1=r1*0.5;
const float L1=2.0;   // length of the rod
const float r2=0.33;   // radius of translating circle
const float sr2=r2*0.6+0.06;
const float vsr2=r2*0.5;
const vec2 c0=vec2(1.0,0.0); // coordinate of the big circle
const float zoom=0.5;
float blur=8.0;      // blur = smooth edge

float st=L0*sin(iGlobalTime);
float ct=L0*cos(iGlobalTime);
vec2 pix; // the position of the current pixel

// to draw concentrics circles (r=radius, sr=small_radius,vsr=very small radius)
float circles(vec2 center, float r, float sr, float vsr){
    float dist=distance(center,pix);

    return smoothstep(dist-blur,dist,r) * smoothstep(sr-blur,sr,dist) + smoothstep(dist-blur,dist,vsr);
}


// to draw a triangle
// got to pass vertex in the clockwise order, the diagonal must be bc for a rectangle
float triangle(vec2 a, vec2 b, vec2 c){
    vec2 v0=c-a;
    vec2 v1=b-a;
    vec2 p=pix-a;

    float area=v0.x*v1.y - v0.y*v1.x;

    float alpha = (v0.x*p.y - v0.y*p.x)/area;
    float beta = (v1.y*p.x - v1.x*p.y)/area;
    float gamma = 1.0 - alpha - beta;

    return smoothstep(-2.0*blur,blur,alpha) * smoothstep(-2.0*blur,blur,beta) * step(0.0,gamma);
}


void mainImage( out vec4 fragColor, in vec2 fragCoord ) {
    blur/=iResolution.y;
    pix=(2.0*fragCoord.xy-iResolution.xy)/(zoom*iResolution.y)-c0;
    
    // main circle
    float shade=circles(vec2(0.0,0.0),r0,sr0,vsr0);

    // satelite circle
    vec2 c1=vec2(ct,st);
    shade+=circles(c1,r1,sr1,vsr1);

    // translate circle
    vec2 c2=vec2(ct-sqrt(L1*L1-st*st),0.0);
    shade+=circles(c2,r2,sr2,vsr2);

    vec2 diff=c1/L0; // is normalized that way
    diff=vec2(-diff.y,diff.x);

    shade+=triangle(-sr0*diff,sr0*diff,c1-sr1*diff) * step(r0,length(pix)) * step(r1,distance(pix,c1));
    shade+=triangle(c1+sr1*diff,c1-sr1*diff,sr0*diff)* step(r0,length(pix)) * step(r1,distance(pix,c1));

    // diff=r*||c2->c1||  (r is the radius of the interior disk c1 & c2 )
    diff=(c1-c2)/L1;
    
    // we change c1 & c2's position just to make sure the blur won't go further than the circle
    c1=c1-blur*diff;
    c2=c2+blur*diff;

    diff=vec2(-diff.y,diff.x); // vec perpendicular to c2->c1

    shade+=triangle(c2-vsr2*diff,c2+vsr2*diff,c1-vsr1*diff);
    shade+=triangle(c1+vsr1*diff,c1-vsr1*diff,c2+vsr2*diff);
    
    fragColor = vec4(shade,shade,shade, 1.0 );

}