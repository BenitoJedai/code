float time=iGlobalTime;
float cell_size=5.0;
float cull_dist=30.0;
float zoom=1.8;

vec2 rotate(float angle, vec2 v)
{
    return vec2(cos(angle) * v.x + sin(angle) * v.y, cos(angle) * v.y - sin(angle) * v.x);
}

float hash(float n)
{
    return fract(sin(n)*43758.5453);
}

float noise(vec2 p)
{
    return hash(p.x + p.y*57.0);
}

float smoothNoise1(float p)
{
    float p0 = floor(p + 0.0);
    float p1 = floor(p + 1.0);
    return mix(hash(p0), hash(p1), fract(p));
}

float sceneDist(vec3 p, out vec2 tc)
{
    p.xy=rotate(cos(p.z*0.2)*0.8+sin(p.z*0.07),p.xy);
    if(-p.x>p.y)
        p.xy=vec2(p.y,-p.x);
    if(p.x<p.y)
        p.xy=vec2(p.y,p.x);
    p.x-=1.6+cos(p.z*0.06)*0.5+cos(p.z*0.25)*0.5;
    tc.x=p.z;
    tc.y=atan(p.y,p.x);
    return length(p.xy)-0.5+cos(tc.y*30.0)*0.004+cos(tc.x*2.0)*0.002;
}

// Simplified scene, for when detail isn't necessary.
float sceneDist2(vec3 p)
{
    p.xy=rotate(cos(p.z*0.2)*0.8+sin(p.z*0.07),p.xy);
    if(-p.x>p.y)
        p.xy=vec2(p.y,-p.x);
    if(p.x<p.y)
        p.xy=vec2(p.y,p.x);
    p.x-=1.6+cos(p.z*0.06)*0.5+cos(p.z*0.25)*0.5;
    return length(p.xy)-0.5;
}

vec3 sceneNorm(vec3 p)
{
    vec2 eps=vec2(1e-3,0.0);
    vec2 tc;
    float c=sceneDist(p,tc);
    return normalize(vec3(sceneDist(p+eps.xyy,tc)-c,sceneDist(p+eps.yxy,tc)-c,sceneDist(p+eps.yyx,tc)-c));
}

// Extract normal using the simplified version of the scene.
vec3 sceneNorm2(vec3 p)
{
    vec2 eps=vec2(1e-3,0.0);
    float c=sceneDist2(p);
    return normalize(vec3(sceneDist2(p+eps.xyy)-c,sceneDist2(p+eps.yxy)-c,sceneDist2(p+eps.yyx)-c));
}

// Return a light position, which has been snapped to the scene surface.
vec3 lightPositionForCell(float cell)
{
    vec3 p=vec3(cos(cell)*2.0,sin(cell*10.0)*-2.0,(cell+0.5)*cell_size);
    vec3 n=sceneNorm2(p);
    float d=sceneDist2(p);
    p-=n*d;
    return p+n*0.3;
}

vec3 tonemap(vec3 c)
{
    return c/(c+vec3(0.25));
}

float codeTex(vec2 p)
{
    p.x=mod(p.x,256.0);
    float f=pow(clamp(smoothNoise1(p.x*16.0)+0.3,0.0,1.0),16.0)*0.75;
    return 1.0-(1.0-smoothstep(0.04,0.05,abs(p.y-0.4)))*f*step(0.6,fract(p.x*0.1));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = fragCoord.xy / iResolution.xy;
    uv=uv*2.0-vec2(1.0);
    uv.y*=iResolution.y/iResolution.x;
    uv*=1.2;
    vec2 coord=uv;
    vec3 ro=vec3(0.0,7.0,-time*1.0);
    vec3 rd=normalize(vec3(coord,-1.0*zoom));
    rd.yz=rotate(0.7,rd.yz);

    // Create the background dots.
    {
        vec2 bc=floor(coord.xy*9.0);
        vec2 bp=fract(coord.xy*9.0);
        fragColor.rgb = 10.3*vec3(0.01,0.01,0.02)*
            (1.0-smoothstep(0.0,0.2,distance(bp,vec2(0.5))))*pow(0.5+0.5*cos(time*0.2+bc.y*9.5+bc.x*17.0),128.0);
    }

    // Raymarch the scene.
    vec2 tc=vec2(0.0);
    float t=2.0;
    for(int i=0;i<80;i+=1)
    {
        vec3 rp=ro+rd*t;
        vec3 lp = lightPositionForCell(floor(rp.z/cell_size));
        float d=min(sceneDist(rp,tc), distance(lp,rp)-0.4)*0.75;
        if (abs(d)<1e-4)
            break;
        t+=d;
        if(t>cull_dist)
            break;
    }

    // Accumulate glow from the orbs.
    vec3 rp=ro+rd*t;
    vec3 glow=vec3(0.0);
    float cell=floor(ro.z/cell_size);
    for(int i=0;i<6;i+=1)
    {
        vec3 lp=lightPositionForCell(cell-float(i));
        vec3 lpn=sceneNorm(lp);
        vec3 p=lp-ro;
        p.yz=rotate(-0.7,p.yz);
        vec2 pp=p.xy/-p.z*zoom;
        if(p.z<0.0)
            glow+=(1.0-smoothstep(0.0,40.0,-p.z))*
            0.05*vec3(0.6,0.8,1.0)*vec3(pow(1.0-smoothstep(0.0,0.3,distance(coord,pp)),2.0))*pow(max(0.0,0.5+0.5*dot(lpn,-rd)),2.0);
    }

    vec3 dc=vec3(1.0)*0.04*codeTex(tc);

    float lightscale=mix(0.85,1.0,0.5+0.5*cos(time*32.0));

    if(t<cull_dist)
    {
        // Shade the scene surface.
        vec3 rn=sceneNorm(rp);
        fragColor.rgb=dc*mix(vec3(0.04),vec3(0.2,0.2,0.22),max(0.0,dot(rn,normalize(vec3(3.0,1.0,1.0)))));

        for(int j=-1;j<=1;j+=1)
        {
            vec3 lp=lightPositionForCell(floor(rp.z/cell_size)+float(j));
            if(distance(lp,rp)<0.41)
            {
                fragColor.rgb+=lightscale*2.0*vec3(0.3,0.7,1.0)*mix(0.1,1.0,pow(1.0-dot(normalize(rp-lp),-rd),1.0));
            }
            else
            {
                vec3 ld=normalize(lp-rp);
                fragColor.rgb+=lightscale*60.0*dc*vec3(0.3,0.7,1.0)*vec3(max(0.0,dot(ld,rn)))*pow(max(0.0,1.0-distance(rp,lp)*0.2),4.0);
            }
        }
    }

    // Create the wires.
    {
        float mask=1.0;
        float tt=(3.0-ro.y)/rd.y;
        vec2 cc=(ro+rd*tt).xz;
        vec3 dust=vec3(0.0);
        for (int i=0;i<12;i+=1)
        {
            float s=(0.5+0.5*sin(float(i)*7.0))*0.007;
            float sp=mix(1.0,4.0,0.5+0.5*cos(float(i)))*0.5;
            float d=abs(cc.x+cos(float(i+1)+cc.y*0.1*sp+time*0.03*(sp+cos(float(i))))*1.3);
            float cable=smoothstep(s+0.00,s+0.011,d);

            dust+=mask*0.05*sqrt(glow)*((1.0-smoothstep(s+0.00,s+0.005,d))-pow(1.0-smoothstep(0.0,s*0.5+0.005,d),1.0));

            mask=min(mask,cable);
            mask=min(mask,0.75+0.25*smoothstep(0.0,0.3,abs(cc.x+cos(float(i+1)+cc.y*0.1*sp+time*0.03*(sp+cos(float(i))))*1.3)));
        }
        // Mask out the wire shapes.
        fragColor.rgb*=mix(0.0,1.0,mask);
        // Add some fake lighting.
        fragColor.rgb+=dust;
    }

    fragColor.rgb+=glow+0.6*vec3(0.3,0.7,1.0)*mix(vec3(0.01),vec3(0.0),min(1.0,abs(coord.x)));
    fragColor.rgb=tonemap(fragColor.rgb*10.0)+noise(fragCoord.xy)/255.0;
}

