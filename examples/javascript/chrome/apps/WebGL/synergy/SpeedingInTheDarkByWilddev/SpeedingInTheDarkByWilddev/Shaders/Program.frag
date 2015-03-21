// I'm having a weird problem with a Firefox/Opera/Chrome (not IE) bug and was 
// wondering if anyone else could help me out by reproducing it.

// Change line 48 to float time = mod(iGlobalTime, 2.0);
// and the shader no longer builds.


// Returns the location of the current fragment relative to the center of the screen, where 0.5 is the distance to the nearest screen border.
// This will return values > +-0.5 on the X axis in widescreen, and the Y axis in portrait.
vec2 pixelCoord(vec2 fragCoord) { 
	return ((fragCoord - (iResolution.xy / 2.0)) / min(iResolution.x, iResolution.y)); 
}

struct ray {
	vec3 start;
	vec3 normal;
};
	
// Builds a ray at 0, 0, 0 looking down Z+.
ray lens(vec2 pixelCoord, float fieldOfView) {
	return ray(vec3(0.0, 0.0, 0.0), normalize(vec3(pixelCoord, fieldOfView)));
}

// These functions find the distance to a plane on the given axis at the given location.
// Mask is a predicate, given the other two axes at the point of intersection.
// If it returns false, the plane is not processed.
#define planeX(xLoc, mask) { float distance = (xLoc - ray.start.x) / ray.normal.x; if(distance < nearest && distance >= 0.0) { vec2 intersection = ray.start.yz + ray.normal.yz * distance; if(mask) nearest = distance; } }
#define planeZ(zLoc, mask) { float distance = (zLoc - ray.start.z) / ray.normal.z; if(distance < nearest && distance >= 0.0) { vec2 intersection = ray.start.xy + ray.normal.xy * distance; if(mask) nearest = distance; } }
#define planeY(yLoc, mask) { float distance = (yLoc - ray.start.y) / ray.normal.y; if(distance < nearest && distance >= 0.0) { vec2 intersection = ray.start.xz + ray.normal.xz * distance; if(mask) nearest = distance; } }

#define shade(col) color += intensity * col; intensity = 0.0;

#define light(origin, compute) { vec3 _origin = origin; float along = dot(_origin - ray.start, ray.normal); along = min(nearest, mix(nearest, along, clamp(along, 0.0, 1.0))); vec3 closest = ray.start + ray.normal * along; intensity += compute; }
#define radial(rate, exponent) (1.0 / (1.0 + pow(rate * distance(closest, _origin), exponent)))
#define directional(normal, exponent) (pow(max(0.0, dot(normal, normalize(closest - _origin))), exponent))

#define rotateY(value, angle) { float _angle = angle; float cosine = cos(angle); float sine = sin(angle); value = vec3(cosine * value.x - sine * value.z, value.y, cosine * value.z + sine * value.x); }

#define keyframe(duration, location, yaw, pitch) { float _duration = duration; vec3 _location = location; float _yaw = yaw; float _pitch = pitch; if(time < _duration && time > 0.0) { time /= _duration; ray.start = mix(previousLocation, _location, time); rotateY(ray.normal, mix(previousYaw, _yaw, time)) time = -1.0; } else { time -= _duration; } previousLocation = _location; previousYaw = _yaw; previousPitch = _pitch; }

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	ray ray = lens(pixelCoord(fragCoord), 0.5);
    
    // Camera
    // --------------------------------------------------
    
    float time = iGlobalTime;
    vec3 previousLocation;
    float previousYaw;
    float previousPitch;
    
    // Starting room.
    keyframe(0.0, vec3(1.5, 0.0, -1.0), 0.5, 0.0)
    keyframe(2.0, vec3(-0.375, 0.0, 1.5), 0.0, 0.0)
        
    // Corridor.
    keyframe(1.5, vec3(-3.0, 0.0, 2.25), 1.0, 0.0)
    keyframe(1.5, vec3(-5.5, 0.0, 2.25), 0.6, 0.0)
        
    // Around first bend.
    keyframe(1.5, vec3(-5.5, 0.0, 6.875), -0.1, 0.0)     
        
    // Turn towards stairwell.
    keyframe(0.5, vec3(-5.5, 0.0, 7.875), 0.9, 0.0)     
        
    // Occluders.
    // --------------------------------------------------
    // IE11 doesn't allow infinity.
    float nearest = 10000.0;
    
    // Front wall of starting room.	
    planeZ(1.0, 
            intersection.x > -6.5           
            && intersection.x < 2.0
            && intersection.y < 1.0
            && intersection.y > -1.0
            && (
            	intersection.y >= 0.75 
            	|| intersection.x >= 0.25
                || intersection.x < -6.125
                || ( intersection.x >= -4.875 && intersection.x < -1.0 )
            )
	)     
        
    // Ceiling of starting area.
    planeY(1.0, 
            intersection.x > -11.0
            && intersection.x < 4.5
            && intersection.y > -1.0
            && intersection.y < 14.75
	)        
        
    // Floor of starting area.
    planeY(-1.0, 
            intersection.x > -11.0
            && intersection.x < 4.5
            && intersection.y > -1.0
            && intersection.y < 14.75
	)    
     
	// Right wall of starting room.
    planeX(2.0, 
            intersection.x < 1.0
            && intersection.x > -1.0
          	&& intersection.y > -1.0
           	&& intersection.y < 3.25
           	&& (
                intersection.x > 0.5
                || intersection.y > 2.875
                || intersection.y < 1.625
            )
	)     
        
    // Left wall of starting room.
    planeX(-1.5, 
            intersection.x < 1.0
            && intersection.x > -1.0
          	&& intersection.y > -1.0
           	&& intersection.y < 1.0
	) 
        
    // Left doorframe of starting room.
    planeX(-1.0, 
           intersection.x >= -1.0
           && intersection.x < 0.75
           && intersection.y >= 1.0
           && intersection.y <= 1.25
	)           
        
    // Right doorframe of starting room.
    planeX(0.25, 
           intersection.x >= -1.0
           && intersection.x < 0.75
           && intersection.y >= 1.0
           && intersection.y <= 1.25
	)         
        
    // Top of doorframes on starting level.
    planeY(0.75, 
           intersection.x >= -1.0
           && intersection.x < 0.25
           && intersection.y >= 1.0
           && intersection.y <= 1.25
	)           
        
    // Back wall of corridor.	
    planeZ(3.25, 
            intersection.x >= -4.5
           && intersection.x <= 4.5
           && intersection.y >= -1.0
           && intersection.y <= 1.0
	)           
        
    // Left wall of corridor.	
    planeX(-6.5, 
           intersection.x <= 1.0
           && intersection.x >= -1.0
           && intersection.y >= 1.25
           && intersection.y <= 14.75
           && (
               // Above doors.
               intersection.x > 0.75
               // Make a hole for the doorway you pass.
               || intersection.y < 3.625
               || (
                   intersection.y > 4.875
               // Make a hole for the doorway to the stairwell.
                   && intersection.y < 7.25
               )
               || intersection.y > 8.5
           )
	)            
        
    // Right wall of corridor.	
    planeX(-4.5, 
            intersection.x <= 0.75
           && intersection.x >= -1.0
           && intersection.y >= 3.25
           && intersection.y <= 14.75
	)  
        
    // Left wall of corridor.	
    planeX(-6.75, 
           intersection.x <= 1.0
           && intersection.x >= -1.0
           && intersection.y >= 1.25
           && intersection.y <= 10.5
           && (
               // Above doors.
               intersection.x > 0.75
               // Make a hole for the doorway you pass.
               || intersection.y < 3.625
               || (
                   intersection.y > 4.875
               // Make a hole for the doorway to the stairwell.
                   && intersection.y < 7.25
               )
               || intersection.y > 8.5
           )
	)         
        
    // Lights.
    // --------------------------------------------------
    float intensity = 0.0;
    vec3 color = vec3(0.0);
    
    // Starting room.
    light(vec3(0.5, 0.9, 0.0), radial(8.0, 2.0) + radial(1.0, 3.0) * directional(vec3(0.0, -1.0, 0.0), 2.0))
    shade(vec3(0.3, 0.6, 1.0));
    
    // Corridor outside starting room.
    light(vec3(-1.0, 1.0, 2.25), radial(8.0, 2.0) + radial(1.0, 3.0) * directional(vec3(0.0, -1.0, 0.0), 2.0))
    light(vec3(-3.5, 1.0, 2.25), radial(8.0, 2.0) + radial(1.0, 3.0) * directional(vec3(0.0, -1.0, 0.0), 2.0))
    
    // Around corner.
	light(vec3(-5.5, 1.0, 4.25), radial(8.0, 2.0) + radial(1.0, 3.0) * directional(vec3(0.0, -1.0, 0.0), 2.0))        
    light(vec3(-5.5, 1.0, 7.875), radial(8.0, 2.0) + radial(1.0, 3.0) * directional(vec3(0.0, -1.0, 0.0), 2.0))        
    shade(vec3(0.8, 0.7, 0.3));    
    
    // Post processing.
    // --------------------------------------------------    
    
    // In addition to distance fog, this puts some very nice fake AO in corners of the room because they're further than the centre of the walls.
    color *= vec3(1.0 / (1.0 + nearest * 10.0));
    
    // This darkens the scene considerably however so boost the brightness after.
    color *= 16.0;
    
    // Draw a backdrop if no occluder was hit.
    if(nearest == 1.0 / 0.0)
        color += mix(vec3(0.1, 0.1, 0.01), vec3(0.0, 0.01, 0.0), pow(ray.normal.y +0.5, 0.5));
    
    fragColor = vec4(pow(color, vec3(1.0 / 2.2)), 1.0);
}