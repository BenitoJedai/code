
var ShaderTest = {

  'hatching' : {

    uniforms: {

      "uDirLightPos":	{ type: "v3", value: new THREE.Vector3() },
      "uDirLightColor": { type: "c", value: new THREE.Color( 0xeeeeee ) },

      "uAmbientLightColor": { type: "c", value: new THREE.Color( 0x050505 ) },

      "uBaseColor":  { type: "c", value: new THREE.Color( 0xff0000 ) },

    },

    vertex_shader: [

      "varying vec3 vNormal;",
      "varying vec3 vRefract;",

      "void main() {",

        "vec4 mPosition = objectMatrix * vec4( position, 1.0 );",
        "vec4 mvPosition = modelViewMatrix * vec4( position, 1.0 );",
        "vec3 nWorld = normalize ( mat3( objectMatrix[0].xyz, objectMatrix[1].xyz, objectMatrix[2].xyz ) * normal );",

        "vNormal = normalize( normalMatrix * normal );",

        "vec3 I = mPosition.xyz - cameraPosition;",
        "vRefract = refract( normalize( I ), nWorld, 1.02 );",

        "gl_Position = projectionMatrix * mvPosition;",

      "}"

    ].join("\n"),

    fragment_shader: [

      "uniform vec3 uBaseColor;",

      "uniform vec3 uDirLightPos;",
      "uniform vec3 uDirLightColor;",

      "uniform vec3 uAmbientLightColor;",

      "varying vec3 vNormal;",

      "varying vec3 vRefract;",

      "void main() {",

        "float directionalLightWeighting = max( dot( normalize( vNormal ), uDirLightPos ), 0.0);",
        "vec3 lightWeighting = uAmbientLightColor + uDirLightColor * directionalLightWeighting;",

        "float intensity = smoothstep( - 0.5, 1.0, pow( length(lightWeighting), 20.0 ) );",
        "intensity += length(lightWeighting) * 0.2;",

        "float cameraWeighting = dot( normalize( vNormal ), vRefract );",
        "intensity += pow( 1.0 - length( cameraWeighting ), 6.0 );",
        "intensity = intensity * 0.2 + 0.3;",

        "if ( intensity < 0.50 ) {",

          "gl_FragColor = vec4( 2.0 * intensity * uBaseColor, 1.0 );",

        "} else {",

          "gl_FragColor = vec4( 1.0 - 2.0 * ( 1.0 - intensity ) * ( 1.0 - uBaseColor ), 1.0 );",

        "}",

      "}"

    ].join("\n")

  }

};

var container, stats;

var camera, scene, renderer;

var material, mesh, shader,light;

var mouseX = 0, mouseY = 0,
lat = 0, lon = 0, phy = 0, theta = 0;

var windowHalfX = window.innerWidth / 2;
var windowHalfY = window.innerHeight / 2;

init();
setInterval( loop, 1000 / 60 );

function init() {

  container = document.getElementById( 'container' );

  camera = new THREE.Camera( 40, windowHalfX / windowHalfY, 1, 3000 );
  camera.position.z = 1000;

  scene = new THREE.Scene();

  light = new THREE.DirectionalLight( 0xffffff );
  light.position.x = 1;
  light.position.y = 0;
  light.position.z = 1;
  scene.addLight( light );

  renderer = new THREE.WebGLRenderer();
  container.appendChild( renderer.domElement );

  var geometry = new Torus( 50, 20, 15, 15 );

  shader = ShaderTest["hatching"];

  material_base = new THREE.MeshShaderMaterial( {

    uniforms: Uniforms.clone( shader.uniforms ),
    vertex_shader: shader.vertex_shader,
    fragment_shader: shader.fragment_shader

  } );

  renderer.initMaterial( material_base, scene.lights, scene.fog );

  for ( var i = 0; i < 100; i ++ ) {

    material = new THREE.MeshShaderMaterial( {

      uniforms: Uniforms.clone( shader.uniforms ),
      vertex_shader: shader.vertex_shader,
      fragment_shader: shader.fragment_shader

    } );

    material.program = material_base.program;

    material.uniforms.uDirLightPos.value = light.position;
    material.uniforms.uDirLightColor.value = light.color;
    material.uniforms.uBaseColor.value = new THREE.Color( Math.random() * 0xffffff );

    mesh = new THREE.Mesh( geometry, material );
    mesh.position.x = Math.random() * 800 - 400;
    mesh.position.y = Math.random() * 800 - 400;
    mesh.position.z = Math.random() * 800 - 400;

    mesh.rotation.x = Math.random() * 360 * Math.PI / 180;
    mesh.rotation.y = Math.random() * 360 * Math.PI / 180;
    mesh.rotation.z = Math.random() * 360 * Math.PI / 180;

    scene.addObject( mesh );

  }


  //stats = new Stats();
  //stats.domElement.style.position = 'absolute';
  //stats.domElement.style.top = '0px';
  //container.appendChild( stats.domElement );

  onWindowResize();

  window.addEventListener( 'resize', onWindowResize, false );

}

function onWindowResize( event ) {

  renderer.setSize( window.innerWidth, window.innerHeight );

}

function loop() {

  var time = new Date().getTime() * 0.0004;

  for ( var i = 0, l = scene.objects.length; i < l; i ++ ) {

    scene.objects[ i ].rotation.x += 0.01;
    scene.objects[ i ].rotation.y += 0.01;

  }

  /*
  light.position.x = Math.sin( time );
  light.position.z = Math.cos( time );
  light.position.y = 0.5;
  light.position.normalize();
  */

  renderer.render( scene, camera );
  //stats.update();

}