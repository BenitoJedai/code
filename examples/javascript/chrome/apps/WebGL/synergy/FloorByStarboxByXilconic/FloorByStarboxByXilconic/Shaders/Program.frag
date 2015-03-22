const float PI=3.14159265;

// ==== Brick flooring parameters ==============================
// The width and height of a brick/mortar in [0,1] image space
#define BRICKWIDTH 2.0
#define BRICKHEIGHT 1.28
#define MORTARTHICKNESS 0.16

// Describes the width and height of a single brick 'feature' in image space.
// A 'brick feature' is define as rectangle following:
//    - Horizontally: mortar: [0, 0.5 * MORTARTHICKNESS]
//                    brick : [0.5 * MORTARTHICKNESS, BRICKWIDTH + 0.5 * MORTARTHICKNESS]
//                    mortar: [BRICKWIDTH + 0.5 * MORTARTHICKNESS, BRICKWIDTH + MORTARTHICKNESS]
//    - Vertically  : mortar: [0, 0.5 * MORTARTHICKNESS]
//                    brick : [0.5 * MORTARTHICKNESS, BRICKHEIGHT + 0.5 * MORTARTHICKNESS]
//                    mortar: [BRICKHEIGHT + 0.5 * MORTARTHICKNESS, BRICKHEIGHT + MORTARTHICKNESS]
#define BMWIDTH (BRICKWIDTH + MORTARTHICKNESS)
#define BMHEIGHT (BRICKHEIGHT + MORTARTHICKNESS)

// Describes the relative mortar boundary in normalized 'break feature' space [0,1]
#define MWF (MORTARTHICKNESS * 0.5 / BMWIDTH)
#define MHF (MORTARTHICKNESS * 0.5 / BMHEIGHT)
// ============================================================

// ==== Star on box parameters ================================
const vec3 starColor = vec3(1.0, 0.78, 0.05);
const vec3 starBG = vec3(0.0, 0.0, 1.0);

const float RMIN = 0.2; // Inner radius of the star, in uv space.
const float RMAX = 0.4; // Outer radius of the star, in uv space.
const float NUMBER_OF_STARTPOINTS = 5.0; // Number of star points; Should be integer!

// Angle between 2 star points, in [rad]:
const float STAR_ANGLE = 2.0*PI / NUMBER_OF_STARTPOINTS; 
const float HALF_STAR_ANGLE = 0.5 * STAR_ANGLE;

const vec2 starCentre = vec2(0.5,0.5); // @middle of the screen
const vec3 starCentre3D = vec3(starCentre, 0.0);

// Real world size, dimensions and location of the box with the procedurally
// generated star texture:
const float starImageSize = 2.0; 
const vec3 boxDimensions = vec3(starImageSize, starImageSize, starImageSize);
const vec3 boxLocation = vec3(-25.0, -3.0, 0.0);
// =============================================================

/**
 * Distance function for a box.
 * @param p: worldspace position vector [x,y,z]
 * @param dimensions: worldspace dimensions of the box from its centre.
 * @return: The worldspace distance from the box
 * REMARKS: This function does not properly deal with distance inside a box!
 */
float udBox(vec3 p, vec3 dimensions )
{
  return length(max(abs(p)-dimensions,0.0));
}

/**
 * Distance function for the star-box.
 * @param p: worldspace position vector [x,y,z]
 * @return: [distance, material definition index]
 */
vec2 obj_box(in vec3 p){
    vec3 displacedP = p - boxLocation;
    return vec2(udBox(displacedP, boxDimensions), 1);
}

/**
 * Distance function for the floor at y == -5 (World space).
 * @param p: worldspace position vector [x,y,z]
 * @return: [distance, material definition index]
 */
vec2 obj_floor(in vec3 p)
{
  return vec2(p.y+5.0,0);
}

/**
 * Performs a union of two distance functions with material index information.
 * @param obj0: Return result of some distance function that follows the pattern
 *              [distance, material definition index]
 * @param obj1: Return results of a different distance function that also follows
 *              the pattern [distance, material definition index]
 * @return: The union of these two results (basically returning the nearest result)
 * REMARK: This method does not properly deal with transparent objects!
 */
vec2 obj_union(in vec2 obj0, in vec2 obj1)
{
  if (obj0.x < obj1.x)
  	return obj0;
  else
  	return obj1;
}

/**
 * General distance function used in the shader.
 * @param p: worldspace position vector [x,y,z]
 * @return: [distance, material definition index]
 */
vec2 distance_to_obj(in vec3 p)
{
    return obj_union(obj_floor(p), obj_box(p));
}

/**
 * Map the given location to the 'brick feature' space of the floor.
 * @param p: worldspace position vector [x,y,z]
 * @return: Return the feature space coordinates, range [0,1].
 */
vec2 getBrickFeatureCoordinate(in vec3 p){
    vec2 textureCoordinates = p.xz / vec2(BMWIDTH, BMHEIGHT);
    
    if (mod(textureCoordinates.y * 0.5, 1.0) > 0.5)
    {
        textureCoordinates.x += 0.5;
    }
    
    float xBrick = floor(textureCoordinates.x);
    textureCoordinates.x -= xBrick;
    float yBrick = floor(textureCoordinates.y);
    textureCoordinates.y -= yBrick;
    
    return textureCoordinates;
}

/**
 * Determine the color of the brick floor.
 * @param p: worldspace position vector [x,y,z]
 * @return The base RGB color at the given location.
 */
vec3 floor_color(in vec3 p)
{
    // Material color information:
    vec4 brickColor = vec4(0.5, 0.15, 0.14, 1.0); // RGBA
    vec4 mortarColor = vec4(0.5, 0.5, 0.5, 1.0); // RGBA
    
    vec2 textureCoordinates = getBrickFeatureCoordinate(p);
    
    // Step functions describing when inside brick and when inside mortar:
    float widthIndex = step(MWF, textureCoordinates.x) - step(1.0-MWF, textureCoordinates.x);
    float heightIndex = step(MHF, textureCoordinates.y) - step(1.0-MHF, textureCoordinates.y);
    
    // Decide color for mortar or brick.
    // widthIndex * heightIndex is basically AND-ing if inside width/height of a brick.
    return mix(mortarColor.rgb, brickColor.rgb, widthIndex*heightIndex);
}

/**
 * Creates a 3D texture coordinate with respect to the centre of the star
 * in the XY plane z==0.0
 * @param r: Radius in texture coordinate space
 * @param angle: Angle in [rad], with respect to the top of the star.
 * @return: A 3D texture coordinate point
 */
vec3 Get3DStarPointFromAngular(float r, float angle){
    return starCentre3D + r * vec3(sin(angle), 
	                               cos(angle), 
								   0.0);
}

/**
 * Returns the color for the given location for the Cure with a star.
 * @param p: worldspace position vector [x,y,z]
 * @return: RGB color at that point
 */
vec3 cube_color(in vec3 p)
{
    // Cube plane in YZ plane in range [boxLocation.ZY - starImageSize, boxLocation.ZY + starImageSize].
    // Z is horizontal axis, Y is vertical axis.
    // Map this range to [0, 1]:
    vec2 uv = clamp((p.zy - boxLocation.zy + starImageSize)/ (2.0 * starImageSize), 0.0, 1.0);

    // p0: Top of the star.
    // p1: First clockwise notch of the star.
    vec3 p0 = Get3DStarPointFromAngular(RMAX, 0.0);
    vec3 p1 = Get3DStarPointFromAngular(RMIN, HALF_STAR_ANGLE);
	
    vec3 edgeP0P1 = p1-p0; // Vector p0->p1
    
	// Because the star is a rotational symmetric shape, mapping the uv 
	// texture coordinates to polar coordinate system makes it easier
	// to define a feature space that makes up a star point.
	// 
    // Get angle and radius of uv point:
    // Note: Angle is w.r.t edge starCentre->p0
    vec2 uvWithRespectTostarCentre = uv - starCentre;
	// Angle w.r.t. edge starCentre->p0, in [rad: -PI, PI]
    float angle = atan(uvWithRespectTostarCentre.x, uvWithRespectTostarCentre.y);
    float r = length(uvWithRespectTostarCentre);
    
    // Map angle to feature space of 0 to STAR_ANGLE * 0.5, using mirroring 
	// as it's a symmetric triangle around HALF_STAR_ANGLE.
    float a = mod(angle, STAR_ANGLE);
    if(a >= HALF_STAR_ANGLE){
        a = STAR_ANGLE - a; // mirror
    }
    // a now in range [0, 0.5] * STAR_ANGLE
    
	// uv mapped into the feature space as pUV
	vec3 pUV = Get3DStarPointFromAngular(r, a);
    vec3 edgep0pUV = pUV - p0; // Vector p0->pUV
    
    // Determine if puv is inside the star or not, using corss product.
    // Cross product yields negative z when inside star and positive z when outside star.
    float in_out = step(0.0, cross(edgeP0P1, edgep0pUV).z);
    return mix(starColor, starBG, in_out);
}

/**
 * Determine the base surface color for the given location and material id.
 * @param materialIndex: The material identifier.
 * @param position: worldspace position vector [x,y,z]
 * @return The base RGB color at the given location.
 */
vec3 GetMaterialColor(float materialIndex, in vec3 position){
	if (materialIndex == 0.0){
		return floor_color(position);
	}
    else if (materialIndex == 1.0){
        return cube_color(position);
    }
	return vec3(0,0,0);
}

/**
 * Calculated the bump height at the given location for the brick surface.
 * @param p: worldspace position vector [x,y,z], peturbed with bumpmapping.
 * @return The bump height at the given location, in range [0,1]
 */
float calculateBumpHeight(in vec3 p){
    vec2 textureCoordinates = getBrickFeatureCoordinate(p);
    
    float hu, hv;
    
    hu = smoothstep(0.0, MWF, textureCoordinates.x) - smoothstep(1.0-MWF, 1.0, textureCoordinates.x);
    hv = smoothstep(0.0, MWF, textureCoordinates.y) - smoothstep(1.0-MWF, 1.0, textureCoordinates.y);
    
    return hu*hv;
}

/**
 * Determine normal using dFdx and dFdy, as presented in "Texturing & Modelling: A Procedural Approach 3rd ed."
 * @param p: worldspace position vector [x,y,z], peturbed with bumpmapping.
 * @return The new surface normal for the floor, taking the mortar grooves into account.
 */
vec3 calculateNormal(in vec3 p){
    // Note: You could use facefoward instead of having to multiply dFdy with -1.0.
    //       This however does increase the required parameters for this method.
    
    // Accuracy note: Estimate derivative by comparing to value calculated in a neighboring pixel.
    //                This can lead to a more coarse estimation than doing a custom derivative instead.
 	return cross(dFdx(p), -1.0*dFdy(p));   
}

/**
 * Performs bump mapping for the floor (Material index == 0).
 * @param p: worldspace position vector [x,y,z]
 * @param globalNormalVector: Surface normal vector at @paramref(p)
 * @param useCustomDerivative: True if custom derivative code should be used; False if dFdx and dFdy should be used instead.
 * @return The new surface normal for the floor, taking the mortar grooves into account.
 */
vec3 floor_bumpmap(in vec3 p, in vec3 globalNormalVector, bool useCustomDerivative){
    float heightScaling = 0.01;
    float heightIncrement = calculateBumpHeight(p);
    
    if(useCustomDerivative)
    {
        float dhdx = heightScaling * (calculateBumpHeight(vec3(p.x + 0.02, p.y, p.z)) - heightIncrement);
        float dhdz = heightScaling * (calculateBumpHeight(vec3(p.x, p.y, p.z + 0.02)) - heightIncrement);

        vec3 vector_dhdx = vec3(0.02, dhdx, 0.0);
        vec3 vector_dhdz = vec3(0.0, dhdz, -0.02);
    
    	return normalize(cross(vector_dhdx, vector_dhdz));
    }
    else
    {
        return normalize(calculateNormal(p + globalNormalVector * (heightIncrement*heightScaling)));
    }
}

/**
 * Calculate the surface normal for the Box with the Star.
 * @param p: worldspace position vector [x,y,z]
 * @param globalNormalVector: Estimated normal vector prior bumpmapping
 * @return: The normal vector at the given location taking bumpmapping into account.
 */
vec3 starBox_bumpmap(in vec3 p, in vec3 globalNormalVector){
    // Cube plane in YZ plane in range [boxLocation.ZY - starImageSize, boxLocation.ZY + starImageSize].
    // Z is horizontal axis, Y is vertical axis.
    // Map this range to [0, 1]:
    vec2 uv = clamp((p.zy - boxLocation.zy + starImageSize)/ (2.0 * starImageSize), 0.0, 1.0);

    // p0: Top of the star.
    // p1: First clockwise notch of the star.
    vec3 p0 = Get3DStarPointFromAngular(RMAX, 0.0);
    vec3 p1 = Get3DStarPointFromAngular(RMIN, HALF_STAR_ANGLE);
	
    vec3 edgeP0P1 = p1-p0; // Vector p0->p1
    
	// Because the star is a rotational symmetric shape, mapping the uv 
	// texture coordinates to polar coordinate system makes it easier
	// to define a feature space that makes up a star point.
	// 
    // Get angle and radius of uv point:
    // Note: Angle is w.r.t edge starCentre->p0
    vec2 uvWithRespectTostarCentre = uv - starCentre;
	// Angle w.r.t. edge starCentre->p0, in [rad: -PI, PI]
    float angle = atan(uvWithRespectTostarCentre.x, uvWithRespectTostarCentre.y);
    float r = length(uvWithRespectTostarCentre);
    
    // Map angle to feature space of 0 to STAR_ANGLE * 0.5, using mirroring 
	// as it's a symmetric triangle around HALF_STAR_ANGLE.
    float a = mod(angle, STAR_ANGLE);
    if(a >= HALF_STAR_ANGLE){
        a = STAR_ANGLE - a; // mirror
    }
    // a now in range [0, 0.5] * STAR_ANGLE
    
	// uv mapped into the feature space as pUV
	vec3 pUV = Get3DStarPointFromAngular(r, a);
    vec3 edgep0pUV = pUV - p0; // Vector p0->pUV
    
    // Determine if puv is inside the star or not, using corss product.
    // Cross product yields negative z when inside star and positive z when outside star.
    float in_out = step(0.0, cross(edgeP0P1, edgep0pUV).z);
    
    // Bump normal map:
    float orientationAngle = angle;
    if(orientationAngle < 0.0){
        orientationAngle += 2.0*PI;
    }
    float starSectionIndex = floor(orientationAngle / HALF_STAR_ANGLE);
	
    float rPn1, rPn2;
    if (1.0 == mod(starSectionIndex, 2.0)){
        // odd section (first clockwise is odd)
        rPn1 = RMIN;
        rPn2 = RMAX;
    }
    else {
        // even section (second clockwise is even)
        rPn1 = RMAX;
        rPn2 = RMIN;
    }
	
	// The section's face is described by the following three vertices in order:
	vec3 pnsc = vec3(starCentre, 0.8);
	vec3 pn1 = Get3DStarPointFromAngular(rPn1, (starSectionIndex + 1.0) * HALF_STAR_ANGLE);
	vec3 pn2 = Get3DStarPointFromAngular(rPn2, starSectionIndex * HALF_STAR_ANGLE);
    
	// Find the normal using the normalized cross-product of the edge vectors.
	// See: http://www.opengl.org/wiki/Calculating_a_Surface_Normal
    // Swizzle coordinates to map them back into real world:
    vec3 vPnscPn1 = pn1.zyx - pnsc.zyx;
    vec3 vPnscPn2 = pn2.zyx - pnsc.zyx;
    
    vec3 normalVector = normalize(globalNormalVector + cross(vPnscPn1, vPnscPn2));
    
    return mix(normalVector, globalNormalVector, in_out);
}

/**
 * Checks for bump map modifications to the given normal vector at the given location.
 * @param materialIndex: Index of the material (Produced by distance_to_obj)
 * @param position: worldspace position vector [x,y,z]
 * @param globalNormalVector: Surface normal vector at @paramref(position)
 * @param useCustomDerivative: True if custom derivative code should be used; False if dFdx and dFdy should be used instead.
 * @return The new surface normal, taking bump mapping into account.
 */
vec3 EvaluateBumpMap(float materialIndex, in vec3 position, in vec3 globalNormalVector, bool useCustomDerivative){
    if (materialIndex == 0.0){
        return floor_bumpmap(position, globalNormalVector, useCustomDerivative);
    }
    if (materialIndex == 1.0){
        return starBox_bumpmap(position, globalNormalVector);
    }
    return globalNormalVector;
}

/**
 * Estimates the surface normal of the distance field at a given position.
 * @param position: Final raymarched position.
 * @param originalDistance: distance from 'position' to the nearest object.
 * @return The unit normal vector.
 */
vec3 EsitmateDistanceFieldNormal(in vec3 position, float originalDistance){
	// Note: Parameter 'originalDistance' can be removed be calling internally at loss of performance:
	//       float originalDistance = distance_to_obj(position);
	
	// Quick trick for generating small permutation of 'position'
	const float derivativeDelta = 0.02;
	const vec2 e = vec2(derivativeDelta,0); 
	
	// Perform a discrete forward derivative:
	vec3 n = vec3(originalDistance - distance_to_obj(position - e.xyy).x,
				  originalDistance - distance_to_obj(position - e.yxy).x,
				  originalDistance - distance_to_obj(position - e.yyx).x);
	// Note: discrete central derivative could be used instead for more accuracy at cost of performace.
	
	return normalize(n); // Normalization helps saving 3 divisions by 'derivativeDelta'
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	// Normalized fragment coordinate in range [-0.5, 0.5]
    vec2 vPos = fragCoord.xy/iResolution.xy - 0.5;
    
    if(abs(vPos.x) < 0.001){
        fragColor = vec4(1.0,1.0,1.0,1.0);
        return;
    }

	// See the following URL for naming definitions:
	//   http://uploads.gamedev.net/monthly_03_2013/ccs-191720-0-04862100-1363766190.png
    // Camera up vector, using positive y as 'up':
    vec3 cameraUpVector = vec3(0,1,0);
  
    // Point where the camera is looking at:
    vec3 cameraTarget = vec3(0,0,0);

    // Map current clicked mouse position on radians [0, 2*PI] horizontally and [0, 0.5*PI) vertically:
    // Note: 0.5 gives rendering problems if iMouse.Y == iResolution.y
	//       This is due to cameraViewDirection and cameraUpVector align perfectly, which causes the
	//       cross-product to yield 0. This prevents spanning the horizontal and vertical axis of the
	//       camera.
    float mx = iMouse.x/iResolution.x * 2.0*PI;
    float my = iMouse.y/iResolution.y * 0.49*PI; 
    
    vec3 eyeWorldPosition = vec3(cos(my)*cos(mx),
								 sin(my),
								 cos(my)*sin(mx)) * 6.0;

    // Camera setup.
	// 1. Get the unit vector for the central viewing direction of the camera:
    vec3 cameraViewDirection = normalize(cameraTarget - eyeWorldPosition);
	
	// 2. Spanning the horizontal (u) and veritcal (v) unit vectors describing the camera view-plane axis:
    vec3 u = normalize(cross(cameraUpVector, cameraViewDirection));
    vec3 v = normalize(cross(cameraViewDirection, u));
	
	// 3. Determine the 'projection window' coordinate
	// 3.1 Determine the 'project window' center / the camera position:
    vec3 cameraPosition = eyeWorldPosition + cameraViewDirection;
	// 3.2 Map vPos onto the 'project window', taking aspect ratio into account:
    vec3 evaluatedCoordinate = cameraPosition +
							   vPos.x * u * iResolution.x/iResolution.y + // horizontal component
							   vPos.y * v; // vertical component
    vec3 rayCastDirection = normalize(evaluatedCoordinate-eyeWorldPosition);

    // Distance-aided ray marching
    const float maxd = 100.0; //Max drawing distance from camera center
    const float inverseMax = 1.0 / maxd;
    
    vec2 d = vec2(0.0, 0.0);
    vec3 colorRGB, rayPosition, normal;

    float rayDistanceTraveled = 1.0;
    for(int i = 0; i < 256; i++) // maximum value affects horizon mapping/warping
    {
    	rayDistanceTraveled += d.x;
    	rayPosition = eyeWorldPosition + rayCastDirection * rayDistanceTraveled;
    	d = distance_to_obj(rayPosition);
        
        if ((abs(d.x) < .001) || (rayDistanceTraveled > maxd)) 
    		break;
  	}
  
  	if (rayDistanceTraveled < maxd)
  	{
    	// y is used to manage materials.
		colorRGB = GetMaterialColor(d.y, rayPosition);
    
    	normal = EsitmateDistanceFieldNormal(rayPosition, d.x);
      
    	normal = EvaluateBumpMap(d.y, rayPosition, normal, vPos.x > 0.0);
      
		// Rotating point light around [0,10,0]:
		const float lightRadius = 20.0;
		vec3 lightPosition = vec3(sin(iGlobalTime)*lightRadius, 10.0, cos(iGlobalTime)*lightRadius);
		
		// Do simple phong lighting:
    	float b = clamp(dot(normal, normalize(eyeWorldPosition - rayPosition + lightPosition)),0.0,1.0);    	
    	fragColor=vec4((b*colorRGB + pow(b,16.0)) * (1.0 - rayDistanceTraveled * inverseMax), 1.0);
  	}
  	else 
    	fragColor=vec4(0,0,0,1); //background color
}
