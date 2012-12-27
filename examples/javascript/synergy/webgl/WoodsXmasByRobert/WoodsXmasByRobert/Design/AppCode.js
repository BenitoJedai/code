var container;

var camera;
var scene;
var webglRenderer;
var composer;

var has_gl = 0;

var i;
var r = 0;
var loadedObjects = 0;

var delta
var time;
var oldTime;

var treeArray = [];
var numOfRocks = 25;
var rockArray = [];
var numOfFlowers = 20;
var flowerArray = [];
var flareArray = [];
var numOfSnowflakes = 6000;

var loader;
var tree1Geo;
var tree2Geo;

var groundMesh1, groundMesh2;
var cameraTarget;
var particles;
var uniforms;
var attributes;
var moon;
var cloud;
var sky;
var bird;
var horse;
var sled;
var leftHandle;
var rightHandle;
var allLoaded = false;
var bgSprite;
var loadingSprite;

var mouseXpercent = 0;
var mouseYpercent = 0;
var speedEffector = { value: 1 };

var pointLight;
var snd;
var pauseTime = 39.5;
var unPauseTime = 57;
var havePaused = false;
var haveUnPaused = false;

// Subtitles (start, stop)-pairs, etc...
var cuePoints = [8.43, 13.8, 14, 19.1, 19.7, 24.1, 24.5, 29.6, 30.2, 35.2, 35.7, 41, 43.6, 49.7, 49.9, 56.1, 60, 69];
var subtitleArray = [];
var subtitleIndex = 0;
var subtitleVisible = false;
var starArray = [];

if (Audio != undefined) {
    var a = new Audio();
    var ext = "mp3";
    if (!a.canPlayType("audio/mp3")) ext = "ogg";

    snd = new Audio("sound/unfiltered_mix." + ext);
    snd.volume = 0.9;

    snd.addEventListener("loadeddata", function () {
        checkLoadingDone();
    }, false);
}



init();


function init() {

    container = document.createElement('div');
    document.body.appendChild(container);

    scene = new THREE.Scene();
    scene.fog = new THREE.FogExp2(0x000000, 0.0004);

    camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 100000);
    camera.position.z = 0;
    camera.position.x = 0;
    camera.position.y = 0;

    cameraTarget = new THREE.Vector3();
    cameraTarget.z = -400;
    camera.lookAt(cameraTarget);
    scene.add(camera);

    setupSubtitles();

    // Lights
    var ambient = new THREE.AmbientLight(0x000000);
    scene.add(ambient);

    pointLight = new THREE.PointLight(0xccccff, 7.0, 10000);
    pointLight.position.set(0, 5000, 200);
    scene.add(pointLight);

    // Loading stuff
    var bgImage = new THREE.Texture(generateTexture());
    bgImage.needsUpdate = true;

    bgSprite = new THREE.Sprite({ map: bgImage, useScreenCoordinates: true });
    bgSprite.position.set(window.innerWidth >> 1, window.innerHeight >> 1, 0);
    bgSprite.scale.set(1000, 1000);
    scene.add(bgSprite);

    var loadingImage = THREE.ImageUtils.loadTexture("img/loading.png");

    loadingSprite = new THREE.Sprite({ map: loadingImage, useScreenCoordinates: true });
    loadingSprite.position.set(window.innerWidth >> 1, window.innerHeight >> 1, 1);
    scene.add(loadingSprite);


    // Ground 
    var plane = new THREE.PlaneGeometry(8000, 20000, 9, 24);

    for (i = 0, l = plane.vertices.length; i < l; i++) {
        var y = Math.floor(i / 10);
        var x = i - (y * 10);
        if (x == 4 || x == 5) {
            plane.vertices[i].z = 0;
        } else {
            plane.vertices[i].z = (Math.random() * 480) - 240;
        }
        if (y == 0 || y == 24) {
            plane.vertices[i].z = -60;
        }
    }

    groundMesh1 = new THREE.Mesh(plane, new THREE.MeshLambertMaterial({ color: 0x333333 }));

    groundMesh1.rotation.set(-Math.PI / 2, 0, 0);
    groundMesh1.position.y = -300;
    groundMesh1.position.z = -10000;
    scene.add(groundMesh1);

    groundMesh2 = new THREE.Mesh(plane, new THREE.MeshLambertMaterial({ color: 0x333333 }));

    groundMesh2.rotation.set(-Math.PI / 2, 0, 0);
    groundMesh2.position.y = -300;
    groundMesh2.position.z = -30000;
    scene.add(groundMesh2);

    // Trees
    loader = new THREE.JSONLoader();
    loader.load("models/treeDead.js", treeLoaded);

    // Rocks
    loader.load("models/rock.js", rockLoaded);

    // Flowers
    loader.load("models/weeds01.js", flowerLoaded);
    loader.load("models/glowbulb.js", flower2Loaded);



    // Horse
    loader.load("models/horse.js", horseLoaded);

    // Moon
    var moonPlane = new THREE.PlaneGeometry(1000, 1000);
    moon = new THREE.Mesh(moonPlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/moon.png"), transparent: true, opacity: 0.3, fog: false, blending: THREE.AdditiveBlending }));
    moon.position.set(300, 4300, -4600);
    moon.lookAt(camera.position);
    scene.add(moon);

    // Cloud
    var cloudPlane = new THREE.PlaneGeometry(12500, 1880);
    cloud = new THREE.Mesh(cloudPlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/cloud.png"), transparent: true, opacity: 0.17, fog: false }));
    cloud.position.set(300, 5350, -4450);
    cloud.lookAt(camera.position);
    scene.add(cloud);

    // Sky
    var skyPlane = new THREE.PlaneGeometry(9000, 6000);
    sky = new THREE.Mesh(skyPlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/sky.jpg"), opacity: 0.57, fog: false }));
    sky.scale.set(4, 2.5, 2.5);
    sky.position.set(0, 7500, -6000);
    sky.lookAt(camera.position);
    scene.add(sky);

    // Flares
    var flarePlane = new THREE.PlaneGeometry(96, 96);
    for (i = 0; i < 3; i++) {
        var flare = new THREE.Mesh(flarePlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/flare.jpg"), transparent: true, depthWrite: false, opacity: 0.5, blending: THREE.AdditiveBlending }));
        scene.add(flare);
        flareArray.push(flare);

        spawnFlare(i, i * 300);
    };

    // Snowflakes - Particles
    var map = THREE.ImageUtils.loadTexture("img/snowflake.png");

    attributes = {

        size: { type: 'f', value: [] },
        customColor: { type: 'c', value: [] },
        time: { type: 'f', value: [] },

    };

    uniforms = {

        color: { type: "c", value: new THREE.Color(0xcccccc) },
        texture: { type: "t", value: map },
        globalTime: { type: "f", value: 0.0 },
        speed: { type: "f", value: 0.0 }

    };

    var shaderMaterial = new THREE.ShaderMaterial({

        uniforms: uniforms,
        attributes: attributes,
        vertexShader: document.getElementById('vertexshader').textContent,
        fragmentShader: document.getElementById('fragmentshader').textContent,

        blending: THREE.AdditiveBlending,
        depthTest: true,
        transparent: true,

    });


    var geometry = new THREE.Geometry();

    for (i = 0; i < numOfSnowflakes; i++) {
        var vector = new THREE.Vector3(Math.random() * 2000 - 1000, -1400, Math.random() * 3000)

        geometry.vertices.push(vector);
    }

    particles = new THREE.ParticleSystem(geometry, shaderMaterial);

    particles.position.y = 1120;
    particles.position.z = -3000;

    var vertices = geometry.vertices;
    var values_size = attributes.size.value;
    var values_color = attributes.customColor.value;
    var values_time = attributes.time.value;

    for (var v = 0; v < vertices.length; v++) {

        values_size[v] = 50 + Math.random() * 80;
        values_color[v] = new THREE.Color(0xffffff);
        values_color[v].setHSV(0.0, 0.0, 0.05 + Math.random() * 0.9);

        values_time[v] = Math.random();

    }

    scene.add(particles);


    try {
        webglRenderer = new THREE.WebGLRenderer({ clearColor: 0x000000, clearAlpha: 1.0 });
        webglRenderer.setSize(window.innerWidth, window.innerHeight);
        webglRenderer.autoClear = false;
        container.appendChild(webglRenderer.domElement);
        has_gl = 1;

        // THREEx.WindowResize(webglRenderer, camera);

        // postprocessing
        var renderModel = new THREE.RenderPass(scene, camera);
        var effectFilm = new THREE.FilmPass(1.0, 0.025, 648, false);

        var shaderVignette = THREE.VignetteShader;
        var effectVignette = new THREE.ShaderPass(shaderVignette);
        effectVignette.uniforms["offset"].value = 0.95;
        effectVignette.uniforms["darkness"].value = 2.5;

        var effectCopy = new THREE.ShaderPass(THREE.CopyShader);
        effectCopy.renderToScreen = true;

        composer = new THREE.EffectComposer(webglRenderer);

        composer.addPass(renderModel);
        composer.addPass(effectFilm);
        composer.addPass(effectVignette);
        composer.addPass(effectCopy);

    }
    catch (e) {
        debugger;
    }

}

function spawnFlare(id, delay) {

    var flare = flareArray[id];

    var alphaInTween = new TWEEN.Tween(flare.material)
        .to({ opacity: 0.35 }, 1000)
        .delay(delay)
        .easing(TWEEN.Easing.Cubic.EaseOut);

    var alphaOutTween = new TWEEN.Tween(flare.material)
        .to({ opacity: 0 }, 1000)
        .delay(500)
        .easing(TWEEN.Easing.Cubic.EaseIn)
        .onComplete(function () {
            flare.material.opacity = 0;

            flare.position.set(Math.random() * 800 - 400, -300 + 48, camera.position.z - (800 + Math.random() * 2500));
            flare.lookAt(camera.position);
        });

    alphaInTween.chain(alphaOutTween);
    alphaOutTween.chain(alphaInTween);

    alphaInTween.start();

}

function setupSubtitles() {

    var textPlane = new THREE.PlaneGeometry(512, 80);
    for (i = 0; i < 8; i++) {
        var sub = new THREE.Mesh(textPlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/txt" + i + ".png"), transparent: true, depthTest: false }));
        sub.position.z = -800;
        sub.position.y = -550;
        sub.visible = false;
        camera.add(sub);
        subtitleArray.push(sub);
    };

    var endPlane = new THREE.PlaneGeometry(500, 100);

    var end = new THREE.Mesh(endPlane, new THREE.MeshBasicMaterial({ map: THREE.ImageUtils.loadTexture("img/xmas.png"), transparent: true, opacity: 1.0, depthTest: false }));
    end.position.z = -400;
    end.position.y = 100;
    end.visible = false;
    camera.add(end);
    subtitleArray.push(end);

    // stars
    var starImage = THREE.ImageUtils.loadTexture("img/flare.jpg");

    for (i = 0; i < 10; i++) {

        var star = new THREE.Sprite({ map: starImage, useScreenCoordinates: false, blending: THREE.AdditiveBlending });
        star.position.set(i * 40 - 200, Math.random() * -40 + 10, 1 + i);
        var scale = 0.25 + Math.random() * 0.4;
        star.rotation = Math.random() * Math.PI;
        star.rnd = Math.random() * 5;
        star.times = 0;
        star.scale.set(scale, scale, scale);
        star.visible = false;

        end.add(star);

        starArray.push(star);

    };

}

function runSubtitles() {

    if (snd.currentTime >= cuePoints[subtitleIndex]) {
        var index = Math.floor(subtitleIndex / 2);
        var sub = subtitleArray[index];

        var last = false;
        if (index >= subtitleArray.length - 1) {
            last = true;
            if (!subtitleVisible) {
                sub.material.opacity = 0;
                var opacityTween = new TWEEN.Tween(sub.material)
                .to({ opacity: 1.0 }, 2000)
                .easing(TWEEN.Easing.Sinusoidal.EaseIn);
                opacityTween.start();

                for (i = 0; i < starArray.length; i++) {
                    starArray[i].visible = true;
                    starArray[i].times = 0;

                    var timesTween = new TWEEN.Tween(starArray[i])
                    .to({ times: 1 }, 2000)
                    .easing(TWEEN.Easing.Sinusoidal.EaseIn);
                    timesTween.start();
                };
            } else {
                var opacityTween = new TWEEN.Tween(sub.material)
                .to({ opacity: 0 }, 2000)
                .easing(TWEEN.Easing.Sinusoidal.EaseIn)
                .onComplete(function () {
                    sub.visible = false;
                }
                );
                opacityTween.start();

                for (i = 0; i < starArray.length; i++) {
                    var star = starArray[i];
                    var timesTween = new TWEEN.Tween(star)
                    .to({ times: 0 }, 2000)
                    .easing(TWEEN.Easing.Sinusoidal.EaseIn)
                    .onComplete(function () {
                        star.visible = false;
                    }
                    );
                    timesTween.start();
                };
            };
        };

        if (subtitleVisible && !last) {
            sub.visible = subtitleVisible = false;
        } else {
            sub.visible = subtitleVisible = true;
        }


        ++subtitleIndex;
    }

    // pause, unpause
    if (snd.currentTime >= pauseTime && snd.currentTime < unPauseTime && !havePaused) {
        var effectTween = new TWEEN.Tween(speedEffector)
            .to({ value: 0 }, 4000)
            .easing(TWEEN.Easing.Sinusoidal.EaseInOut);
        effectTween.start();
        havePaused = true;
    }

    if (snd.currentTime >= unPauseTime && havePaused && !haveUnPaused) {
        var effectTween = new TWEEN.Tween(speedEffector)
            .to({ value: 1 }, 1000)
            .easing(TWEEN.Easing.Sinusoidal.EaseIn);
        effectTween.start();
        haveUnPaused = true;
    }

    // stars
    if (Math.floor(subtitleIndex / 2) > subtitleArray.length - 2 && subtitleArray[subtitleArray.length - 1].visible) {
        for (i = 0; i < starArray.length; i++) {

            var star = starArray[i];
            star.opacity = Math.sin((time / 500) + i + star.rnd) * star.times;

        };
    };

}

function generateTexture() {

    var canvas = document.createElement('canvas');
    canvas.width = 32;
    canvas.height = 32;

    var context = canvas.getContext('2d');

    context.fillStyle = "#000000";
    context.fillRect(0, 0, 32, 32);

    return canvas;

}

function checkLoadingDone() {

    ++loadedObjects;

    if (loadedObjects < 8) { return };

    allLoaded = true;
    scene.remove(loadingSprite);
    delete loadingSprite;

    var alphaTween = new TWEEN.Tween(bgSprite)
        .to({ opacity: 0 }, 3000)
        .delay(3000)
        .easing(TWEEN.Easing.Sinusoidal.EaseIn)
        .onComplete(removeLoading);
    alphaTween.start();

    var tempTween = new TWEEN.Tween(speedEffector)
        .to({ value: 1 }, 1000)
        .delay(2000)
        .easing(TWEEN.Easing.Linear.EaseNone)
        .onComplete(function () {
            snd.play();
            container.style.cursor = 'url(img/pointer.png),pointer';
        }
        );
    tempTween.start();

}

function removeLoading() {
    scene.remove(bgSprite);
    delete bgSprite;
}

function treeLoaded(geometry) {
    tree1Geo = geometry;
    loader.load("models/treeEvergreenHigh.js", tree2Loaded);
}

function tree2Loaded(geometry) {
    tree2Geo = geometry;
    setupTrees();
    checkLoadingDone();
}

function setupTrees() {

    var gridSize = 500;

    for (var x = 0; x < 8; x++) {
        for (var z = 0; z < 12; z++) {
            var geo = tree2Geo;
            if (Math.random() < 0.25 && x != 0 && x != 7) { geo = tree1Geo };

            var mesh = new THREE.Mesh(geo, new THREE.MeshFaceMaterial());
            var scale = 1.2 + Math.random();
            mesh.scale.set(scale, scale * 2, scale);

            //
            if (x < 4) {
                var posx = (x * gridSize) - (gridSize * 4) - 100 + Math.random() * 100 - 50;
            } else {
                var posx = (x * gridSize) - 1400 + Math.random() * 100 - 50;
            };

            var posz = -(z * gridSize) + Math.random() * 100 - 50;

            mesh.position.set(posx, -400 - (Math.random() * 80), posz);

            mesh.rotation.set((Math.random() * 0.2) - 0.1, Math.random() * Math.PI, (Math.random() * 0.2) - 0.1);

            scene.add(mesh);

            treeArray.push(mesh);

        };
    };

}

function rockLoaded(geometry) {

    for (i = 0; i < numOfRocks; ++i) {

        var mesh = new THREE.Mesh(geometry, new THREE.MeshLambertMaterial({ color: 0x444444 }));

        var scale = 1 + (Math.random() * 0.5);

        mesh.scale.set(scale, scale, scale);
        mesh.rotation.set(0, Math.random() * Math.PI, 0);
        mesh.position.set((Math.random() * 4000) - 2000, -400, (Math.random() * 6000) - 6000);
        if (mesh.position.x < 450 && mesh.position.x > 0) {
            mesh.position.x += 450;
        }
        if (mesh.position.x > -450 && mesh.position.x < 0) {
            mesh.position.x -= 450;
        }

        scene.add(mesh);

        rockArray.push(mesh);
    }

    checkLoadingDone();

}

function flower2Loaded(geometry) {
    flowerLoaded(geometry, true);
}

function flowerLoaded(geometry, halfScale) {
    var half = Math.floor(numOfFlowers / 2);

    for (i = 0; i < half; ++i) {

        var mesh = new THREE.Mesh(geometry, new THREE.MeshLambertMaterial({ color: 0x444444 }));
        var scale = 1 + (Math.random() * 1);
        if (halfScale) {
            scale *= 0.6;
        }
        mesh.scale.set(scale, scale, scale);
        mesh.rotation.set((Math.random() * 0.6) - 0.3, Math.random() * Math.PI, (Math.random() * 0.6) - 0.3);
        mesh.position.set((Math.random() * 1000) - 500, -310, (Math.random() * 6000) - 6000);

        if (mesh.position.x < 100 && mesh.position.x > 0) {
            mesh.position.x += 100;
        }
        if (mesh.position.x > -100 && mesh.position.x < 0) {
            mesh.position.x -= 100;
        }

        scene.add(mesh);

        flowerArray.push(mesh);
    }

    checkLoadingDone();

}



function horseLoaded(geometry) {

    horse = new THREE.MorphAnimMesh(geometry, new THREE.MeshLambertMaterial({ color: 0x090601, morphTargets: true }));

    horse.duration = 1000;

    horse.scale.set(2.5, 1.8, 2);
    horse.rotation.y = Math.PI;
    horse.position.set(0, -350, -700);

    scene.add(horse);

    checkLoadingDone();

    // Handles
    var plane = new THREE.PlaneGeometry(700, 10, 40, 1);

    for (i = 0, l = Math.floor(plane.vertices.length / 2) ; i < l; i++) {

        var offset = Math.sin(i / 14) * 100;

        plane.vertices[i].y -= offset;
        plane.vertices[i + 41].y -= offset;

        var r = Math.random() * 4;

        plane.vertices[i].z -= (i / 5) + (offset * -1) / 8;
        plane.vertices[i + 41].z += (i / 5) - (offset * -1) / 8;

    }

    var material = new THREE.MeshBasicMaterial({ color: 0x090601, side: THREE.DoubleSide });

    leftHandle = new THREE.Mesh(plane, material);
    leftHandle.position.y = -120;
    leftHandle.position.z = -350;
    leftHandle.position.x = -30;
    leftHandle.rotation.y = -(Math.PI / 2) + 0.075;
    leftHandle.rotation.x = Math.PI * 2 - 0.075;

    scene.add(leftHandle);

    rightHandle = new THREE.Mesh(plane, material);
    rightHandle.position.y = -120;
    rightHandle.position.z = -350;
    rightHandle.position.x = 30;
    rightHandle.rotation.y = -(Math.PI / 2) - 0.075;
    rightHandle.scale.z = -1;
    rightHandle.rotation.x = Math.PI * 2 - 0.075;
    scene.add(rightHandle);

}
