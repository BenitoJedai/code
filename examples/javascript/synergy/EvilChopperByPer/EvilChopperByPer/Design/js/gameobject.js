
var bulletGeometry = null;
var bombGeometry = null;

var cubeGeometry = null;
var sphereGeometry = null;

var cannonBulletMaterial = null;
var bombMaterial = null;


var materials = {};
var geometries = {};


function ScreenMessage(text, x, y) {
    this.text = text ? text : "Bad text... in screen message";
    this.ticks = 0;
    this.removeMe = false;
    this.duration = 100;
    this.dx = 0.0;
    this.dy = -1;
    this.position = new THREE.Vector2(x, y);
}

ScreenMessage.prototype.step = function() {
    this.ticks++;
    if (this.ticks > this.duration) {
        this.removeMe = true;
    }
    this.position.x += this.dx;
    this.position.y += this.dy;
};

ScreenMessage.prototype.removed = function() {
};


function GameObject(size) {
    if (!size) {
        size = GRID_SIZE;
    }
    this.removeMe = false;
    this.ticks = 0;
    this.threeObject = null;
    this.collisionRect = [0, 0, size, size];
    this.initialPosition = null;
}

GameObject.prototype.step = function() {
};

GameObject.prototype.removed = function() {
    this.removeFromScene(GameState.scene);
};

GameObject.prototype.addToScene = function(scene) {
    scene.add(this.threeObject);
};


GameObject.prototype.removeFromScene = function(scene) {
    scene.remove(this.threeObject);
};

GameObject.prototype.setPosition = function(pos) {
    this.threeObject.position = new THREE.Vector3(pos.x, pos.y, pos.z);
    this.initialPosition = new THREE.Vector3(pos.x, pos.y, pos.z);
};

GameObject.prototype.updatePosition = function(pos) {
    this.threeObject.position = new THREE.Vector3(pos.x, pos.y, pos.z);
};


GameObject.prototype.setScale = function(s) {
    this.threeObject.scale = new THREE.Vector3(s.x, s.y, s.z);
};


function Powerup(type, text, color, emissive, posAmplitude, posFrequency, rotZ) {
    GameObject.call(this);

    this.type = type;
    this.text = text;

    if (!emissive) {
        emissive = 0x000000;
    }

    var geometry = geometries[type];

    if (!geometry) {
        if (stringStartsWith(type, "ship")) {
            geometry = new THREE.CylinderGeometry(0.1, 0.5, 1);
        } else {
            geometry = new THREE.CubeGeometry(1, 1, 1);
        }
        geometries[type] = geometry;
    }
    var material = materials[type];
    if (!material) {
        material = GameState.renderer.getPowerupMaterial(color, emissive);
        materials[type] = material;
    }
    this.threeObject = new THREE.Mesh(geometry, material);

    this.posAmplitude = posAmplitude;
    this.posFrequencey = posFrequency;
    this.rotZ = rotZ;

}
Powerup.prototype = new GameObject();


Powerup.prototype.step = function() {
    this.ticks++;
    this.threeObject.rotation.z += this.rotZ;
    this.threeObject.position.z = this.initialPosition.z + this.posAmplitude * GRID_SIZE * Math.sin(this.ticks * this.posFrequencey);
};



//function ColoredText(type, color, emissive, size, text) {
//    GameObject.call(this, size);
//
//    this.type = type;
//
//    if (!size) {
//        size = GRID_SIZE;
//    }
//
//    if (!emissive) {
//        emissive = 0x000000;
//    }
//
//    var geometry = geometries[type];
//
//    if (!geometry) {
//        geometry = new THREE.TextGeometry(text, {
//            height: size / 4,
//            size: size,
////            hover: size,
//
//            curveSegments: 4,
//
//            bevelThickness: 2,
//            bevelSize: 1.5,
//            bevelSegments: 3,
//            bevelEnabled: true,
//            bend: true,
//
//            font: "helvetiker", 		// helvetiker, optimer, gentilis, droid sans, droid serif
//            weight: "bold",		// normal bold
//            style: "normal"
//        });
////        geometry.cen
//
//        geometry.boundingBox = null;
//        geometry.computeBoundingBox();
//
//        var bb = geometry.boundingBox;
//        var centerX = 0.5 * (bb.max.x + bb.min.x);
//        var centerY = 0.5 * (bb.max.y + bb.min.y);
//        var centerZ = 0.5 * (bb.max.z + bb.min.z);
//
//        var m1 = new THREE.Matrix4();
//        m1.translate(new THREE.Vector3(-centerX, -centerY, -centerZ));
//        geometry.applyMatrix(m1);
//
//        geometries[type] = geometry;
//    }
//    var material = materials[type];
//    if (!material) {
//        if (GameState.renderer instanceof THREE.CanvasRenderer) {
//            if (emissive) {
//                material = new THREE.MeshBasicMaterial({color: color, emissive: emissive});
//            } else {
//                material = new THREE.MeshLambertMaterial({color: color, emissive: emissive});
//            }
//        } else {
//            material = new THREE.MeshLambertMaterial({color: color, emissive: emissive});
//        }
//        materials[type] = material;
//    }
//    this.threeObject = new THREE.Mesh(geometry, material);
//    this.threeObject.rotation.z = -Math.PI / 2;
//}
//ColoredText.prototype = new GameObject();
//
//
//function MovingColoredText(type, color, emissive, size, text, posAmplitude, posFrequency, rotY) {
//    ColoredText.call(this, type, color, emissive, size, text);
//    this.posAmplitude = posAmplitude;
//    this.posFrequencey = posFrequency;
//    this.rotZ = rotY;
//}
//MovingColoredText.prototype = new ColoredText();
//
//MovingColoredText.prototype.step = function() {
//    this.ticks++;
//    this.threeObject.rotation.y += this.rotZ;
//    this.threeObject.position.y = this.initialPosition.y + this.posAmplitude * GRID_SIZE * Math.sin(this.ticks * this.posFrequencey);
//};



function PointLight(color, intensity, distance) {
    GameObject.call(this);

    this.threeObject = new THREE.PointLight(color, intensity, distance);
}
PointLight.prototype = new GameObject();


function MovingObject() {
    GameObject.call(this);
    this.owner = null;
    this.velocity = new THREE.Vector3(0, 0, 0);
}
MovingObject.prototype = new GameObject();

MovingObject.prototype.step = function() {
    this.threeObject.position.addSelf(this.velocity);
};

MovingObject.prototype.setVelocity = function(v) {
    this.velocity.copy(v);
};


function Explosion() {
    MovingObject.call(this);
    this.maxDamage = 1;

    if (sphereGeometry == null) {
        sphereGeometry = new THREE.SphereGeometry(0.5);
    }
    this.material = GameState.renderer.getExplosionMaterial();
    this.threeObject = new THREE.Mesh(sphereGeometry, this.material);
//    var size = 0.25;
//    this.threeObject.scale.set(size, size, size);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = false;

    this.duration = 20;

    this.rndValue1 = globalRnd.random();
    this.rndValue2 = globalRnd.random();
    this.rndValue3 = globalRnd.random();
    this.rndValue4 = globalRnd.random();

    this.explosionLight = null;

}
Explosion.prototype = new MovingObject();


Explosion.prototype.addToScene = function(scene) {
    GameState.particleParent.add(this.threeObject);
};

Explosion.prototype.removeFromScene = function(scene) {
    GameState.particleParent.remove(this.threeObject);
};


Explosion.prototype.step = function() {
    MovingObject.prototype.step.call(this);

    if (this.ticks == 0) {

        this.radius = this.threeObject.scale.x;

        if (GameState.availableExplosionLights.length > 0) {
            this.explosionLight = GameState.availableExplosionLights.pop();
            this.explosionLight.distance = this.radius * 10;
            this.explosionLight.intensity = 2;

            this.explosionLight.position.copy(this.threeObject.position.clone().addSelf(new THREE.Vector3(0, 0, this.radius)));
        }

        var pos = this.threeObject.position;
        for (var i=0; i<GameState.solids.length; i++) {
            var solid = GameState.solids[i];

            var solPos = solid.threeObject.position;
            var solScale = solid.threeObject.scale;
            var gx = solPos.x - solScale.x * 0.5;
            var gy = solPos.y - solScale.y * 0.5;
            var or = [gx, gy, solScale.x, solScale.y];

            var dist = rectDistanceToPoint(or, pos);
            if (dist < this.radius) {
                var damFrac = 1.0 - dist / this.radius;
                solid.doDamage(damFrac * this.maxDamage);
                GameState.didDamage();
//                logit("expl hit solid " + dist + " " + damFrac);
            }
        }

        for (var i=0; i<GameState.agents.length; i++) {

            var agent = GameState.agents[i];

            if (agent != this.owner) {

                var solPos = agent.threeObject.position;
                var solScale = agent.threeObject.scale;
                var gx = solPos.x - solScale.x * 0.5;
                var gy = solPos.y - solScale.y * 0.5;
                var or = [gx, gy, solScale.x, solScale.y];

                var dist = rectDistanceToPoint(or, pos);

                var vertDist = solPos.z - pos.z;
                var dist = Math.sqrt(vertDist * vertDist + dist * dist);

                if (dist < this.radius) {
                    var damFrac = 1.0 - dist / this.radius;
                    agent.doDamage(damFrac * this.maxDamage);
                    if (this.owner == GameState.player) {
                        GameState.didDamage();
                    }
//                logit("expl hit solid " + dist + " " + damFrac);
                }
            }
        }

    }
    this.ticks++;

    var frac = this.ticks / this.duration;

    if (this.explosionLight) {
        this.explosionLight.intensity = 2 - frac * 1;
    }

    var rad = frac * this.radius * 2;
    this.setScale(new THREE.Vector3(rad, rad, rad));

    var r = 1.0 - this.rndValue1 * 0.1 - 0.15 * frac;
    var g = 1.0 - this.rndValue2 * 0.1 - 0.85 * frac;
    var b = 1.0 - this.rndValue3 * 0.1 - frac;
    var op = 1.0 - this.rndValue4 * 0.1 - 0.1 * frac;
    var col = new THREE.Color().setRGB(r, g, b);
    this.material.emissive.copy(col);
    this.material.color.copy(col);
    this.material.opacity = op;
    this.ticks++;
    if (this.ticks > this.duration) {
        if (this.explosionLight) {
            GameState.availableExplosionLights.push(this.explosionLight);
            this.explosionLight.position.set(-9999, 9999, 9999);
            this.explosionLight = null;
        }
        this.removeMe = true;
    }
};


function Projectile() {
    MovingObject.call(this);
}
Projectile.prototype = new MovingObject();


Projectile.prototype.step = function() {
    MovingObject.prototype.step.call(this);

    var obj = this.threeObject;
    var pos = obj.position;
    var vel = this.velocity;
    var scale = obj.scale;

    var result = {};
    var cr = [pos.x - scale.x * 0.5 + vel.x, pos.y - scale.y * 0.5 + vel.y, scale.x, scale.y];

//    logit(cr + " " + GameState.solids[0].threeObject.position.y);

    if (checkGameObjectCollision(cr, GameState.solids, result)) {
        for (var i=0; i<result.objects.length; i++) {
            var solid = result.objects[i];
            this.hitSolid(solid);
        }
    }

    var playerObj = GameState.player.threeObject;
    var pScale = playerObj.scale;
    var scale = obj.scale;

    var result = {};
    if (this.owner != GameState.player) {
        result.objects = [];
        var diffZ = Math.abs(obj.position.z - playerObj.position.z);

        var maxDiff = 0.5 * (scale.z + pScale.z);
        if (diffZ < maxDiff) {
            if (checkSingleObjectCollision(cr, GameState.player, result)) {
                for (var i=0; i<result.objects.length; i++) {
                    var agent = result.objects[i];
                    this.hitAgent(agent);
                }
            }
        }

    } else {
        if (checkGameObjectCollision(cr, GameState.agents, result)) {
            for (var i=0; i<result.objects.length; i++) {
                var agent = result.objects[i];
                if (this.owner != agent) {
                    var aObj = agent.threeObject;
                    var diffZ = Math.abs(obj.position.z - aObj.position.z);
                    var oScale = aObj.scale;
                    var maxDiff = 0.5 * (scale.z + oScale.z);

//                    logit("maxdiff: " + maxDiff);
                    if (diffZ < maxDiff) {
                        this.hitAgent(agent);
                    }
                }
            }
        }
    }
};

Projectile.prototype.hitSolid = function(solid) {
//    logit("projectile hit solid ");
    this.removeMe = true;
};

Projectile.prototype.hitAgent = function(agent) {
    logit("projectile hit agent ");
    this.removeMe = true;
};


function CannonBullet() {
    Projectile.call(this);
    if (bulletGeometry == null) {
        bulletGeometry = new THREE.CylinderGeometry(0.1, 0.4, 1);
        var m = new THREE.Matrix4();
        m.rotateX(Math.PI * 0.5);
        bulletGeometry.applyMatrix(m);
    }
    if (cannonBulletMaterial == null) {
        cannonBulletMaterial = GameState.renderer.getCannonBulletMaterial();
    }
    this.threeObject = new THREE.Mesh(bulletGeometry, cannonBulletMaterial);
//    var size = 0.25;
//    this.threeObject.scale.set(size, size, size);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;

    this.duration = 50;
    this.direction = new THREE.Vector3(0, 1, 0);

    this.damage = 0.45;
}
CannonBullet.prototype = new Projectile();

CannonBullet.prototype.hitSolid = function(solid) {
    this.removeMe = true;
    solid.doDamage(this.damage);

    if (this.owner == GameState.player) {
        GameState.didDamage();
    }
    sounds.hitBuilding.sound.play();
};

CannonBullet.prototype.hitAgent = function(agent) {
    this.removeMe = true;
    agent.doDamage(this.damage);

    if (this.owner == GameState.player) {
        GameState.didDamage();
    }
    sounds.hitEnemy.sound.play();
};


CannonBullet.prototype.step = function() {
    Projectile.prototype.step.call(this);

//    logit("hfsdj");

    this.ticks++;

    if (this.ticks > this.duration) {
        this.removeMe = true;
    }
};


function Bomb() {
    Projectile.call(this);
    if (bombGeometry == null) {
        bombGeometry = new THREE.CylinderGeometry(0.1, 0.4, 1.5);
//        var m = new THREE.Matrix4();
//        m.rotateX(Math.PI * 0.5);
//        bombGeometry.applyMatrix(m);
    }
    if (bombMaterial == null) {
        bombMaterial = GameState.renderer.getBombMaterial();
    }
    this.threeObject = new THREE.Mesh(bombGeometry, bombMaterial);
//    var size = 0.25;
//    this.threeObject.scale.set(size, size, size);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;

    this.damage = 1;
    this.duration = 1000;
}
Bomb.prototype = new Projectile();

Bomb.prototype.explode = function() {
    var explosion = new Explosion();
    explosion.maxDamage = this.damage;
    GameState.particles.push(explosion);
    explosion.addToScene(GameState.scene);
    explosion.setPosition(this.threeObject.position);
//        bomb.setVelocity({x: this.velocity.x + speed * dx, y: this.velocity.y + speed * dy, z: this.velocity.z});
    var explosionSize = GRID_SIZE + GRID_SIZE * this.damage;
    explosion.setScale({x: explosionSize, y: explosionSize, z: explosionSize});
    explosion.owner = this.owner;

    sounds.explosion.sound.play();

    this.removeMe = true;
};

Bomb.prototype.hitSolid = function(solid) {
    this.explode();
    sounds.hitBuilding.sound.play();
};

Bomb.prototype.hitAgent = function(agent) {
    this.removeMe = true;
    this.explode();
    sounds.hitEnemy.sound.play();
};

Bomb.prototype.step = function() {

    var vel = this.velocity.clone();

    var gravity = -0.1;

    vel.z += gravity;

    this.velocity.set(vel.x, vel.y, vel.z);

    Projectile.prototype.step.call(this);

    this.ticks++;

    if (this.ticks > this.duration) {
        this.removeMe = true;
    }

    var pos = this.threeObject.position;
    var groundHeight = getGroundHeightAt(pos.x, pos.y, GameState.groundLayer); // getQuickGroundHeightAt(pos.x, pos.y);
    if (pos.z < groundHeight) {
        this.explode();
    }
};



//function Particle(type, size, color, emissive) {
//    MovingObject.call(this, size);
//    this.type = type;
//
//    var material = materials[type];
//    if (!material) {
//        material = new THREE.MeshLambertMaterial({color: color, emissive: emissive});
//        materials[type] = material;
//    }
//    var geometry = geometries[type];
//    if (!geometry) {
//
//    }
//    this.threeObject = new THREE.Mesh(goalGeometry, goalMaterial);
//}
//Particle.prototype = new MovingObject();
//
//Particle.prototype.step = function() {
//    MovingObject.prototype.step.call(this);
//    this.threeObject.rotation.z += 0.01;
//};


function LivingObject() {
    MovingObject.call(this);

    this.dropsPowerups = [];

    this.health = 1.0;
    this.maxHealth = this.health;

    this.blinkTicksLeft = 0;
    this.blinkStartTick = 0;
    this.blinkOnLength = 5;
    this.blinkOffLength = 3;

    this.blinkColor = 0xffffff;

    this.origMaterialInfos = [];
    this.blinkingMaterials = [];
}
LivingObject.prototype = new MovingObject();

LivingObject.prototype.step = function() {
    MovingObject.prototype.step.call(this);

    this.ticks++;

    if (this.blinkTicksLeft > 0) {
        this.blinkTicksLeft--;
        if (this.blinkTicksLeft == 0) {
            this.restoreMaterialInfo();
        } else {
            this.blink();
        }
    }
};

LivingObject.prototype.doDamage = function(damage) {
    this.health -= damage;
    this.blinkTicksLeft = Math.min(35, Math.max(15, Math.round(damage * 50)));
    if (this.health <= 0.0 && !this.isDead) {
        this.health = 0;
        this.die();
        this.isDead = true;
    }
};

LivingObject.prototype.addPowerup = function() {
    if (this.dropsPowerups.length > 0) {
        var powerup = sampleData(this.dropsPowerups, globalRnd);

        logit("Adding powerup " + powerup);

        var pos = this.threeObject.position;

        var obj = {
            type: powerup,
            x: pos.x,
            y: pos.y

        };
        var newObjs = [];

        var powerupText = powerupTexts[obj.type]; // "Powerup";
        if (!powerupText) {
            powerupText = "Powerup";
        }

        switch (powerup) {
            case CANNON_FASTER:
            case CANNON_DAMAGE_UP:
            case CANNON_NEW:
                obj.z = PLAYER_Z;
                var o = new Powerup(obj.type, powerupText, 0xffffff, 0x444444, 0.1, 0.02, 0.04);
                obj.width = GRID_SIZE * 0.25;
                obj.height = obj.width * 3;
                newObjs.push(o);
                GameState.pickups.push(o);
                break;
            case BOMB_FASTER:
            case BOMB_DAMAGE_UP:
            case BOMB_NEW:
                obj.z = PLAYER_Z;
                var o = new Powerup(obj.type, powerupText, 0xffff00, 0x444400, 0.1, 0.02, 0.04);
                obj.width = GRID_SIZE * 0.5;
                obj.height = obj.width;
                newObjs.push(o);
                GameState.pickups.push(o);
                break;
            case SHIP_FASTER:
            case SHIP_HEALTH:
            case SHIP_MAX_HEALTH:
                obj.z = PLAYER_Z;
                var o = new Powerup(obj.type, powerupText, 0x00ffff, 0x004444, 0.1, 0.02, 0.04);
                obj.width = GRID_SIZE * 0.5;
                obj.height = obj.width;
                newObjs.push(o);
                GameState.pickups.push(o);
                break;
        }

        for (var i=0; i<newObjs.length; i++) {
            var newObj = newObjs[i];
            var w = obj.width;
            var h = obj.height;
            var d = obj.depth;
            if (d === undefined) {
                d = Math.min(w, h);
            }

            newObj.setPosition({x: obj.x, y: obj.y, z: PLAYER_Z});
            newObj.setScale({x: w, y: h, z: d});
            newObj.addToScene(GameState.scene);
        }
    }
};

LivingObject.prototype.die = function() {
    this.removeMe = true;
    this.addPowerup();
};

LivingObject.prototype.storeMaterialInfo = function(material) {
    this.origMaterialInfos.push({emissive: material.emissive, color: material.color});
    this.blinkingMaterials.push(material);
};

LivingObject.prototype.restoreMaterialInfo = function() {
    for (var i=0; i<this.blinkingMaterials.length; i++) {
        var mat = this.blinkingMaterials[i];
        var info = this.origMaterialInfos[i];
        if (mat.emissive) {
            mat.emissive.copy(info.emissive);
        }
        if (mat.color) {
            mat.color.copy(info.color);
        }
    }
};

LivingObject.prototype.blink = function() {
    var period = this.blinkOnLength + this.blinkOffLength;
    var phase = this.ticks % period;

    if (phase == 0) {
        for (var i=0; i<this.blinkingMaterials.length; i++) {
            var mat = this.blinkingMaterials[i];
            mat.emissive = new THREE.Color(this.blinkColor);
            mat.color = new THREE.Color(this.blinkColor);
        }
    } else if (phase == this.blinkOnLength) {
        this.restoreMaterialInfo();
    }
};

function Building(c) {
    LivingObject.call(this);
};
Building.prototype = new LivingObject();


Building.prototype.die = function() {
    if (!this.removeMe) {
        this.removeMe = true;

        this.addPowerup();
        var explosion = new Explosion();
        explosion.maxDamage = 0;
        explosion.setPosition(this.threeObject.position);
//        bomb.setVelocity({x: this.velocity.x + speed * dx, y: this.velocity.y + speed * dy, z: this.velocity.z});
        var explosionSize = Math.max(this.threeObject.scale.x, this.threeObject.scale.y);
        explosion.setScale({x: explosionSize, y: explosionSize, z: explosionSize});
        explosion.owner = this;

//        logit(explosion);

        sounds.explosion.sound.play();
        if (explosionSize < 888 && explosionSize > 0.01) {
            explosion.addToScene(GameState.scene);
            GameState.particles.push(explosion);
        }
    } else {
//        logit("Trying to add an explosin a lot! ");
//        logit(this);
    }
};


function House1(c) {
    Building.call(this);

    this.maxHealth = 5;
    this.health = this.maxHealth;

    if (cubeGeometry == null) {
        cubeGeometry = new THREE.CubeGeometry(1, 1, 1);
    }
    var material = GameState.renderer.getHouse1Material();
    this.storeMaterialInfo(material);
    this.threeObject = new THREE.Mesh(cubeGeometry, material);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;
}
House1.prototype = new Building(true);




function House2() {
    Building.call(this);

    this.maxHealth = 5;
    this.health = this.maxHealth;

    if (cubeGeometry == null) {
        cubeGeometry = new THREE.CubeGeometry(1, 1, 1);
    }
    var material = GameState.renderer.getHouse2Material();
    this.storeMaterialInfo(material);
    this.threeObject = new THREE.Mesh(cubeGeometry, material);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;
}
House2.prototype = new Building();


function DefenceTower(level, isBoss) {
    Building.call(this);

    this.health = 1.5 + level * 0.5;
    if (isBoss) {
        this.health *= 2;
    }
    this.maxHealth = this.health;

    this.fireInterval = 80 - level * 15;
    this.damage = 0.1 + 0.1 * level;
    this.defenceRadius = GRID_SIZE * (8 + 2 * level);
    this.bulletSpeed = 3 + level;
    this.bulletDuration = 80;
    this.fireCounter = 1000;

    this.geometry = new THREE.CylinderGeometry(0.5 + level * 0.05, 0.8 + level * 0.1, 1 + level * 0.05);

    var m = new THREE.Matrix4();
    m.rotateX(Math.PI / 2);
    this.geometry.applyMatrix(m);
    if (isBoss) {
        this.material = GameState.renderer.getDefenceTowerBossMaterial();
    } else {
        this.material = GameState.renderer.getDefenceTowerMaterial();
    }

    this.storeMaterialInfo(this.material);

    this.threeObject = new THREE.Mesh(this.geometry, this.material);
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;
}
DefenceTower.prototype = new Building();

DefenceTower.prototype.step = function() {
    LivingObject.prototype.step.call(this);

    var diffVec = GameState.player.threeObject.position.clone();
    diffVec.subSelf(this.threeObject.position);

    var dist = diffVec.length();

    this.fireCounter++;
    if (dist < this.defenceRadius && this.fireCounter > this.fireInterval) {
        this.fireCounter = 0;
        diffVec.normalize();
        var bullet = new CannonBullet();
        bullet.damage = this.damage;
        bullet.addToScene(GameState.scene);
        bullet.setPosition(this.threeObject.position);
        var speed = this.bulletSpeed;
        bullet.duration = this.bulletDuration;
//        logit("Firing bullet " + diffVec.x + " " + diffVec.y + " " + diffVec.z + speed );
        bullet.setVelocity({x: diffVec.x * speed, y: diffVec.y * speed, z: diffVec.z * speed});
        var bulletSize = (0.25 + 0.2 * bullet.damage) * GRID_SIZE;
        bullet.setScale({x: bulletSize, y: bulletSize, z: bulletSize});
        bullet.owner = this;

        var lookAtPos = bullet.threeObject.position.clone().addSelf(diffVec);
        bullet.threeObject.lookAt(lookAtPos);


        GameState.projectiles.push(bullet);

        if (this.cannonPlayCounter > 10) {
            sounds.cannon.sound.play();
            this.cannonPlayCounter = 0;
        }

    }
};


function Chopper(level, isBoss) {
    LivingObject.call(this);

    this.defenceRadius = GRID_SIZE * (10 + level * 2);
    this.lowerFightRadius = GRID_SIZE * 4;
    this.upperFightRadius = this.defenceRadius - GRID_SIZE * 2;

    this.maxSpeed = 1 + 0.3 * level;


    this.fireInterval = 80 - 10 * level;
    this.fireCounter = 889;
    this.bulletDuration = 80;
    this.bulletSpeed = 3 + 0.5 * level;
    this.damage = 0.2;

    this.health = 1.5 + level * 0.5;
    if (isBoss) {
        this.health *= 2;
    }
    this.maxHealth = this.health;

    if (!cubeGeometry) {
        cubeGeometry = new THREE.CubeGeometry(1, 1, 1);
    }
    if (!sphereGeometry) {
        sphereGeometry = new THREE.SphereGeometry(0.5);
    }

    if (isBoss) {
        this.bodyMaterial = GameState.renderer.getChopperBossMaterial();
    } else {
        this.bodyMaterial = GameState.renderer.getChopperBodyMaterial();
    }


    this.threeObject = new THREE.Object3D();
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;


    this.mainBody = new THREE.Mesh(sphereGeometry, this.bodyMaterial);
    this.mainBody.scale.set(1, 1, 1.2);
    this.mainBody.position.set(0, 0, 0.2);
    this.mainBody.castShadow = true;
    this.mainBody.receiveShadow = true;
    this.threeObject.add(this.mainBody);

    this.backBody = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.backBody.scale.set(0.3, 0.2, 1.2);
    this.backBody.position.set(0, 0, -0.6);
    this.backBody.castShadow = true;
    this.backBody.receiveShadow = true;
    this.threeObject.add(this.backBody);

    this.rotor = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.rotor.scale.set(0.1, 0.3, 2);
    this.rotor.position.set(0, 0.5, 0);
    this.rotor.castShadow = true;
    this.rotor.receiveShadow = true;
    this.threeObject.add(this.rotor);

    this.backRotor = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.backRotor.scale.set(0.1, 0.4, 0.2);
    this.backRotor.position.set(-0.2, 0.2, -1.2);
    this.backRotor.castShadow = true;
    this.backRotor.receiveShadow = true;
    this.threeObject.add(this.backRotor);

//    var m = new THREE.Matrix4();
//    m.rotate(Math.PI * 0.5);
//    this.threeObject.applyMatrix(m);


    this.storeMaterialInfo(this.bodyMaterial);
}
Chopper.prototype = new LivingObject();

Chopper.prototype.step = function() {

    var pos = this.threeObject.position;
    var px = pos.x;
    var py = pos.y;

    var vx = this.velocity.x;
    var vy = this.velocity.y;


    var newVx = vx;
    var newVy = vy;

    var maxSpeed = this.maxSpeed;

    var playerPos = GameState.player.threeObject.position;

    var diffVec = playerPos.clone();
    diffVec.subSelf(this.threeObject.position);

    var dist = diffVec.length();

    this.fireCounter++;

    var moveVec = new THREE.Vector2(0, 0);

    this.threeObject.up.set(0, 0, 1);
    this.threeObject.lookAt(playerPos);

    newVx = 0;
    newVy = 0;
    if (dist < this.defenceRadius) {

        for (var i=0; i<GameState.flyers.length; i++) {
            var flyer = GameState.flyers[i];
            if (flyer != this) {
                var fDiffVec = flyer.threeObject.position.clone();
                fDiffVec.subSelf(pos);
                var fDist = fDiffVec.length();
                if (fDist > 0.01 && fDist < GRID_SIZE * 4) {
                    fDiffVec.normalize();
                    moveVec.x += -fDiffVec.x * maxSpeed;
                    moveVec.y += -fDiffVec.y * maxSpeed;
                }
            }
        }


//        logit(diffVec.y);
        if (diffVec.y > -GRID_SIZE * 4) {
            moveVec.y += maxSpeed;
        }

        diffVec.normalize();

        if (dist < this.lowerFightRadius) {
            moveVec.x += -diffVec.x * maxSpeed;
            moveVec.y += -diffVec.y * maxSpeed;
        } else if (dist > this.upperFightRadius) {
            moveVec.x += diffVec.x * maxSpeed;
            moveVec.y += diffVec.y * maxSpeed;
        }


        if (moveVec.length() > 0.001) {
            moveVec.normalize().multiplyScalar(maxSpeed);
        }

        newVx = moveVec.x;
        newVy = moveVec.y;


//        this.threeObject.rotation.z += 0.03;

        if (this.fireCounter > this.fireInterval) {
            this.fireCounter = 0;
            var bullet = new CannonBullet();
            bullet.damage = this.damage;
            bullet.addToScene(GameState.scene);
            bullet.setPosition(this.threeObject.position);
            var speed = this.bulletSpeed;
            bullet.duration = this.bulletDuration;
//        logit("Firing bullet " + diffVec.x + " " + diffVec.y + " " + diffVec.z + speed );
            bullet.setVelocity({x: diffVec.x * speed, y: diffVec.y * speed, z: diffVec.z * speed});
            var bulletSize = (0.25 + 0.2 * bullet.damage) * GRID_SIZE;
            bullet.setScale({x: bulletSize, y: bulletSize, z: bulletSize});
            bullet.owner = this;

            var lookAtPos = bullet.threeObject.position.clone().addSelf(diffVec);
            bullet.threeObject.lookAt(lookAtPos);

            GameState.projectiles.push(bullet);

            if (this.cannonPlayCounter > 10) {
                sounds.cannon.sound.play();
                this.cannonPlayCounter = 0;
            }

        }


    }


    this.rotor.rotation.y += 0.5 + globalRnd.random() * 0.01; // mod(this.ticks * 0.5, Math.PI * 2);
    this.backRotor.rotation.x += 0.5 + globalRnd.random() * 0.01; // mod(this.ticks * 0.5, Math.PI * 2);
//    this.threeObject.rotation.setZ(this.turnAngle);


//    if (px + newVx < maxBorderDist || px + newVx > GameState.groundLayer.width * GRID_SIZE - maxBorderDist) {
//        newVx = 0;
//    }

    // Check if we hit something solid
    pos = this.threeObject.position;
    var scale = this.threeObject.scale;
    px = pos.x;
    py = pos.y;

    var pr = [px - scale.x * 0.5 + newVx, py - scale.y * 0.5 + newVy, scale.x, scale.y];

    var result = {};
    if (checkGameObjectCollision(pr, GameState.solids, result)) {

        // Check only moving along y
        prx = [px - scale.x * 0.5 + newVx, py - scale.y * 0.5, scale.x, scale.y];
        pry = [px - scale.x * 0.5, py - scale.y * 0.5 + newVy, scale.x, scale.y];

        var collideX = checkGameObjectCollision(prx, GameState.solids, result);
        var collideY = checkGameObjectCollision(pry, GameState.solids, result);

        if (collideX) {
            newVx = 0;
        }
        if (collideY) {
            newVy = 0;
        }
    }


    vx = newVx;
    vy = newVy;

    this.velocity.x = vx;
    this.velocity.y = vy;

    LivingObject.prototype.step.call(this);

};




var AccelerationState = {
    NONE: 0,
    FORWARD: 1,
    BACKWARD: 2
};

var SideMovementState = {
    NONE: 0,
    LEFT: 1,
    RIGHT: 2
};


function Player() {
    LivingObject.call(this);

    this.health = getMaxHealth();
    this.maxHealth = getMaxHealth();

    this.accelerationState = AccelerationState.NONE;
    this.sideMovementState = SideMovementState.NONE;

    if (!cubeGeometry) {
        cubeGeometry = new THREE.CubeGeometry(1, 1, 1);
    }
    if (!sphereGeometry) {
        sphereGeometry = new THREE.SphereGeometry(0.5);
    }

    this.bodyMaterial = GameState.renderer.getPlayerShipBodyMaterial();
    this.turnAngle = 0.0;
    this.targetTurnAngle = 0.0;
    this.pitchAngle = 0.0;
    this.targetPitchAngle = 0.0;

    this.threeObject = new THREE.Object3D();
    this.threeObject.castShadow = true;
    this.threeObject.receiveShadow = true;

    this.mainBody = new THREE.Mesh(sphereGeometry, this.bodyMaterial);
    this.mainBody.scale.set(1, 1.2, 1);
    this.mainBody.position.set(0, 0.2, 0);
    this.mainBody.castShadow = true;
    this.mainBody.receiveShadow = true;
    this.threeObject.add(this.mainBody);

    this.backBody = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.backBody.scale.set(0.2, 1.2, 0.3);
    this.backBody.position.set(0, -0.6, 0);
    this.backBody.castShadow = true;
    this.backBody.receiveShadow = true;
    this.threeObject.add(this.backBody);

    this.rotor = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.rotor.scale.set(2, 0.3, 0.1);
    this.rotor.position.set(0, 0, 0.5);
    this.rotor.castShadow = true;
    this.rotor.receiveShadow = true;
    this.threeObject.add(this.rotor);

    this.backRotor = new THREE.Mesh(cubeGeometry, this.bodyMaterial);
    this.backRotor.scale.set(0.1, 0.4, 0.2);
    this.backRotor.position.set(-0.2, -1.2, 0.2);
    this.backRotor.castShadow = true;
    this.backRotor.receiveShadow = true;
    this.threeObject.add(this.backRotor);

    this.storeMaterialInfo(this.bodyMaterial);

    this.cannonFireCounters = createFilledArray(GameState.cannonRateLevels.length, 1000);
    this.bombFireCounters = createFilledArray(GameState.bombRateLevels.length, 1000);

    this.hiddenBySolids = [];

    this.cannonPlayCounter = 10;
    this.bombPlayCounter = 10;

    this.projector = new THREE.Projector();
//    this.leftArm = new THREE.Mesh(armGeometry, armMaterial);
//    this.leftArm.position = new THREE.Vector3(0, PLAYER_HEIGHT * 0.3, PLAYER_WIDTH);
//    this.threeObject.add(this.leftArm);

}

Player.prototype = new LivingObject();


function getMaxHealth() {
    return 1.0 +  + (GameState.healthLevel + GameState.gatheredHealthLevel) * 0.25;
}

function getMaxSpeed() {
    var totalLevel = GameState.gatheredSpeedLevel + GameState.speedLevel;
    return CAMERA_SPEED * (4 + totalLevel * 0.2);
}

//30 - fireIntervalLevel * 2;

function getCannonDamage(index) {
    var gLevel = GameState.gatheredCannonDamageLevels[index];
    var level = GameState.cannonDamageLevels[index];
    var totalLevel = (typeof(gLevel) !== 'undefined' ? gLevel : 0) + (typeof(level) !== 'undefined' ? level : 0);
    return 0.2 + totalLevel * 0.2;
}
function getCannonFireInterval(index) {
    var gLevel = GameState.gatheredCannonRateLevels[index];
    var level = GameState.cannonRateLevels[index];
    var totalLevel = (typeof(gLevel) !== 'undefined' ? gLevel : 0) + (typeof(level) !== 'undefined' ? level : 0);
    return Math.max(10, 30 - totalLevel * 2);
}

function getBombMaxDamage(index) {
    var gLevel = GameState.gatheredCannonDamageLevels[index];
    var level = GameState.cannonDamageLevels[index];
    var totalLevel = (typeof(gLevel) !== 'undefined' ? gLevel : 0) + (typeof(level) !== 'undefined' ? level : 0);
    return 1 + totalLevel * 0.2;
}

function getBombFireInterval(index) {
    var gLevel = GameState.gatheredBombRateLevels[index];
    var level = GameState.bombRateLevels[index];
    var totalLevel = (typeof(gLevel) !== 'undefined' ? gLevel : 0) + (typeof(level) !== 'undefined' ? level : 0);
    return Math.max(30, 80 - totalLevel * 5);
}


function getBombRadius(index) {
    return level;
}

Player.prototype.checkHidden = function() {

//    var canvas = GameState.canvas;
//
//    var vector = new THREE.Vector3( canvas.width * 0.5, canvas.height * 0.5, 0.5 );
//    this.projector.unprojectVector( vector, GameState.camera );



    var plPos = GameState.player.threeObject.position;
    var camPos = GameState.camera.position;

    var dir = plPos.clone().subSelf(camPos).normalize();
    var ray = new THREE.Ray( camPos, dir); //vector.subSelf( GameState.camera.position ).normalize() );

//    logit(ray);

//    logit(" " + dir.x + " " + dir.y + " " + dir.z);

    for (var i=0; i<this.hiddenBySolids.length; i++) {
        var h = this.hiddenBySolids[i];
        for (var j=0; j<h.blinkingMaterials.length; j++) {
            var mat = h.blinkingMaterials[j];
            mat.opacity = 1;
        }
    }

    this.hiddenBySolids = [];

    var objects = [];
    for (var i=0; i<GameState.solids.length; i++) {
        var solid = GameState.solids[i];
        var threeObj = solid.threeObject;
        threeObj._theSolid = solid;

        if (threeObj.position.y < plPos.y) {
            this.hiddenBySolids.push(solid);
            for (var j=0; j<solid.blinkingMaterials.length; j++) {
                var mat = solid.blinkingMaterials[j];
                mat.opacity = 0.5;
            }
        } else {
            objects.push(threeObj);
        }
    }

    var intersects = ray.intersectObjects( objects, true );

    if (intersects.length > 0) {
//        logit("solid intersected player!");
        for (var i=0; i<intersects.length; i++) {
            var inter = intersects[i];
            var obj = inter.object;
            var solid = obj._theSolid;
            this.hiddenBySolids.push(solid);
            for (var j=0; j<solid.blinkingMaterials.length; j++) {
                var mat = solid.blinkingMaterials[j];
                mat.opacity = 0.5;
            }
        }
    }

};

//Player.prototype.die = function() {
//    this.removeMe = true;
//    this.isDead = true;
//    if (GameState.subState == GameSubState.PLAYING) {
//        GameState.subState = GameSubState.DYING;
//        GameState.counter1 = 0;
//    }
//};

Player.prototype.step = function() {

    if ((this.ticks % 5) == 0) {
        this.checkHidden();
    }

    var pos = this.threeObject.position;
    var px = pos.x;
    var py = pos.y;

    var lightPosition = GameState.playerLight.position;
    lightPosition.x = px;
    lightPosition.y = py + 50;
    lightPosition.z = 200;

    var vx = this.velocity.x;
    var vy = this.velocity.y;
    var oldVy = vy;
    var oldVx = vx;

    var leftDown = Input.isDown(Input.ARROW_LEFT) || Input.isDown(Input.A);
    var rightDown = Input.isDown(Input.ARROW_RIGHT) || Input.isDown(Input.D);
    var upDown = Input.isDown(Input.ARROW_UP) || Input.isDown(Input.W);
    var downDown = Input.isDown(Input.ARROW_DOWN) || Input.isDown(Input.S);

    var fireDown = Input.isDown(Input.SPACE) || Input.isDown(Input.Z) || Input.isDown(Input.C) || Input.isDown(Input.B);
    var bombDown = Input.isDown(Input.ENTER) || Input.isDown(Input.X) || Input.isDown(Input.V) || Input.isDown(Input.N);


    this.threeObject.rotation.z = this.turnAngle;



    var canMoveHoriz = true;
    var canMoveVert = true;

    px += PLAYER_WIDTH * 0.5;

    var newVx = vx;
    var newVy = vy;

    var maxSpeed = CAMERA_SPEED * 4;

    var maxSpeed = getMaxSpeed();

    if (upDown) {
        newVy = maxSpeed;
        this.accelerationState = AccelerationState.FORWARD;
    } else if (downDown) {
        newVy = -maxSpeed;
        this.accelerationState = AccelerationState.BACKWARD;
    } else {
        if (GameState.cameraStopped) {
            newVy = 0;
        } else {
            newVy = CAMERA_SPEED;
        }
        this.accelerationState = AccelerationState.NONE;
    }

    if (leftDown) {
        newVx = -maxSpeed;
        this.sideMovementState = SideMovementState.LEFT;
    } else if (rightDown) {
        newVx = maxSpeed;
        this.sideMovementState = SideMovementState.RIGHT;
    } else {
        newVx = 0;
        this.sideMovementState = SideMovementState.NONE;
    }

    // Check camera movement
    // Object must not get outside of the camera


    switch (this.accelerationState) {
        case AccelerationState.NONE:
            this.targetPitchAngle = 0;
            break;
        case AccelerationState.BACKWARD:
            this.targetPitchAngle = Math.PI / 6;
            break;
        case AccelerationState.FORWARD:
            this.targetPitchAngle = -Math.PI / 6;
            break;
    }


    switch (this.sideMovementState) {
        case SideMovementState.NONE:
            this.targetTurnAngle = 0;
            break;
        case SideMovementState.LEFT:
            this.targetTurnAngle = -Math.PI / 6;
            break;
        case SideMovementState.RIGHT:
            this.targetTurnAngle = Math.PI / 6;
            break;
    }

    var f = 0.97;
    var invF = 1.0 - f;
    this.turnAngle = f * this.turnAngle +  invF * this.targetTurnAngle;
    this.pitchAngle = f * this.pitchAngle +  invF * this.targetPitchAngle;
    this.threeObject.rotation.set(this.pitchAngle, this.turnAngle, 0);


    this.rotor.rotation.z += 0.5 + globalRnd.random() * 0.01; // mod(this.ticks * 0.5, Math.PI * 2);
    this.backRotor.rotation.x += 0.5 + globalRnd.random() * 0.01; // mod(this.ticks * 0.5, Math.PI * 2);

//    this.rotor.rotation.z = mod(this.ticks * 0.5, Math.PI * 2);
//    this.backRotor.rotation.x = mod(this.ticks * 0.5, Math.PI * 2);
//    this.threeObject.rotation.setZ(this.turnAngle);

    var camY = GameState.camera.position.y;

    var maxCamOffsetForward = GRID_SIZE * 15;
    var maxCamOffsetBackward = 0;

    if (py + newVy < camY - maxCamOffsetBackward) {
        newVy = Math.max(CAMERA_SPEED * 1.25, newVy);
    }
    if (py + newVy > camY + maxCamOffsetForward) {
        newVy = Math.min(0, newVy);
    }
    var maxBorderDist = GRID_SIZE * 2;

    if (px + newVx < maxBorderDist || px + newVx > GameState.groundLayer.width * GRID_SIZE - maxBorderDist) {
        newVx = 0;
    }

    // Check if we hit something solid
    pos = this.threeObject.position;
    var scale = this.threeObject.scale;
    px = pos.x;
    py = pos.y;

    var pr = [px - scale.x * 0.5 + newVx, py - scale.y * 0.5 + newVy, scale.x, scale.y];

    var result = {};
    if (checkGameObjectCollision(pr, GameState.solids, result)) {

        // Check only moving along y
        prx = [px - scale.x * 0.5 + newVx, py - scale.y * 0.5, scale.x, scale.y];
        pry = [px - scale.x * 0.5, py - scale.y * 0.5 + newVy, scale.x, scale.y];

        var collideX = checkGameObjectCollision(prx, GameState.solids, result);
        var collideY = checkGameObjectCollision(pry, GameState.solids, result);

        if (collideX) {
            newVx = 0;
        }
        if (collideY) {
            newVy = 0;
        }
    }

    if (newVy != 0 && newVx != 0) {
        var correctedVel = new THREE.Vector2(newVx, newVy).normalize().multiplyScalar(maxSpeed);
        newVx = correctedVel.x;
        newVy = correctedVel.y;
    }

    vx = newVx;
    vy = newVy;

    this.velocity.x = vx;
    this.velocity.y = vy;

    LivingObject.prototype.step.call(this);

    var cannonCount = this.cannonFireCounters.length;
    var cannonAngleInc = Math.PI * 2 / cannonCount;

    this.cannonPlayCounter++;

    for (var i=0; i<cannonCount; i++) {
        this.cannonFireCounters[i]++;

        var fireIntervalLevel = 1 + i;

        var interval = getCannonFireInterval(i);

        if (fireDown && this.cannonFireCounters[i] > interval) {
            this.cannonFireCounters[i] = 0;

            var angle = Math.PI * 0.5 + i * cannonAngleInc;

            var dx = Math.cos(angle);
            var dy = Math.sin(angle);

            var speed = 10;

            var bullet = new CannonBullet();
            bullet.damage = getCannonDamage(i);
            bullet.addToScene(GameState.scene);
            bullet.setPosition(this.threeObject.position);


            bullet.setVelocity({x: this.velocity.x + dx * speed, y: this.velocity.y + dy * speed, z: this.velocity.z});
            var bulletSize = (0.25 + 0.2 * bullet.damage) * GRID_SIZE;
            bullet.setScale({x: bulletSize, y: bulletSize, z: bulletSize});
            bullet.owner = GameState.player;

            var direction = new THREE.Vector3(dx, dy, 0);
            var lookAtPos = bullet.threeObject.position.clone().addSelf(direction);
            bullet.threeObject.lookAt(lookAtPos);

            GameState.projectiles.push(bullet);

            if (this.cannonPlayCounter > 10) {
                sounds.cannon.sound.play();
                this.cannonPlayCounter = 0;
            }

        }

    }

    var bombCount = this.bombFireCounters.length;
    var cannonAngleInc = Math.PI * 0.2 / bombCount;

    this.bombPlayCounter++;

    for (var i=0; i<bombCount; i++) {
        this.bombFireCounters[i]++;

        var interval = getBombFireInterval(i);

        if (bombDown && this.bombFireCounters[i] > interval) {


            this.bombFireCounters[i] = 0;

            var angle = Math.PI * 0.5;
            if (bombCount > 1) {
                var frac = i / (bombCount - 1);
                angle = Math.PI * (0.5 - 0.2 + 0.4 * frac);
            }
            var dx = Math.cos(angle);
            var dy = Math.sin(angle);

            var speed = 3;

            var bomb = new Bomb();
            bomb.damage = getBombMaxDamage(i);
            bomb.addToScene(GameState.scene);
            bomb.setPosition(this.threeObject.position);
            bomb.setVelocity({x: this.velocity.x * 0.25 + speed * dx, y: this.velocity.y * 0.25 + speed * dy, z: this.velocity.z});
            var bombSize = (0.25 + 0.05 * bomb.damage) * GRID_SIZE;

            bomb.setScale({x: bombSize, y: bombSize, z: bombSize});
            bomb.owner = GameState.player;

            if (this.bombPlayCounter > 10) {
                sounds.bomb.sound.play();
                this.bombPlayCounter = 0;
            }

            GameState.projectiles.push(bomb);
//            logit("Adding bomb");
        }

    }



//        var toRemove = [];
//
//        var pickup1 = sounds.pickup1; // resources["pickup1"];
//
//        if (allSoundsLoaded) {
////            pickup1.play();
//        }
//        for (var i=0; i<result.objects.length; i++) {
//            toRemove.push(result.objects[i]);
//            result.objects[i].removeFromScene(GameState.scene);
//        }
//        arrayDeleteAll(GameState.bosses, toRemove);
//        if (GameState.bosses.length == 0) {
//            if (levelArr[GameState.levelIndex + 1]) {
//                GameState.subState = GameSubState.COMPLETING;
//            } else {
//                GameState.subState = GameSubState.COMPLETING_GAME;
//            }
//            GameState.counter1 = 0;
//        } else {
//            if (GameState.bosses.length == 1) {
//                $messages.append("<p>" + "Checkpoint! " +
//                    "One is left..." + "</p>");
//            } else {
//                $messages.append("<p>" + "Checkpoint! " +
//                    GameState.bosses.length + " are left..." + "</p>");
//            }
//            GameState.latestMessageTick = GameState.permanentCounter1;
//        }
//    }


    result = {};
    if (checkGameObjectCollision(pr, GameState.pickups, result)) {
        var toRemove = [];
        for (var i=0; i<result.objects.length; i++) {
            var o = result.objects[i];

            givePowerup(o.type);
            o.removeMe = true;
        }
    }

};


function checkMultiGameObjectCollision(arr1, arr2, result) {
    result.pairs = [];
    for (var j=0; j<arr1.length; j++) {
        var obj1 = arr1[j];

        var scale1 = obj1.threeObject.scale;
        var pos1 = obj1.threeObject.position;
        var vel1 = obj1.velocity;

        var cr1 = null;
        if (vel1) {
            cr1 = [pos1.x - scale1.x * 0.5 + vel1.x, pos1.x - scale1.y * 0.5 + vel1.y, scale1.x, scale1.y];
        } else {
            cr1 = [pos1.x - scale1.x * 0.5, pos1.x - scale1.y * 0.5, scale1.x, scale1.y];
        }
        for (var i=0; i<arr2.length; i++) {
            var obj2 = arr2[i];
            var pos = obj2.threeObject.position;
            var scale = obj2.threeObject.scale;
            var gx = pos.x - scale.x * 0.5;
            var gy = pos.y - scale.y * 0.5;
            var cr2 = [gx, gy, scale.x, scale.y];
            if (rectCollide(cr1, cr2)) {
                result.pairs.push([obj1, obj2]);
                return true;
            }
        }
    }
    return false;
}

function checkSingleObjectCollision(pr, obj, result) {
    var pos = obj.threeObject.position;
    var scale = obj.threeObject.scale;
    var gx = pos.x - scale.x * 0.5;
    var gy = pos.y - scale.y * 0.5;
    var or = [gx, gy, scale.x, scale.y];
    if (rectCollide(pr, or)) {
        result.objects.push(obj);
        return true;
    }
}

function checkGameObjectCollision(pr, arr, result) {
    var r = false;
    result.objects = [];
    for (var i=0; i<arr.length; i++) {
        var obj = arr[i];
        if (checkSingleObjectCollision(pr, obj, result)) {
            r = true;
            return true;
        }
    }
    return r;
}


function giveScore(score) {
    var soundInfo = sounds.extraPoints;

    if (soundInfo) {
        soundInfo.sound.play();
    } else {
        logit("Cound not find sound for points ");
    }

}

function givePowerup(type) {
    var soundInfo = sounds[type];

    if (soundInfo) {
        soundInfo.sound.play();
    } else {
        logit("Cound not find sound for pickup " + type);
    }

    var player = GameState.player;
    switch (type) {
        case CANNON_FASTER:
            GameState.gatheredCannonRateLevels[findMinIndex(GameState.gatheredCannonRateLevels)]++;
            break;
        case CANNON_DAMAGE_UP:
            GameState.gatheredCannonDamageLevels[findMinIndex(GameState.gatheredCannonDamageLevels)]++;
            break;
        case CANNON_NEW:
            GameState.gatheredCannonDamageLevels.push(0);
            GameState.gatheredCannonRateLevels.push(0);
            player.cannonFireCounters.push(1000);
            break;
        case BOMB_FASTER:
            GameState.gatheredBombRateLevels[findMinIndex(GameState.gatheredBombRateLevels)]++;
            break;
        case BOMB_DAMAGE_UP:
            GameState.gatheredBombDamageLevels[findMinIndex(GameState.gatheredBombDamageLevels)]++;
            break;
        case BOMB_NEW:
            GameState.gatheredBombDamageLevels.push(0);
            GameState.gatheredBombRateLevels.push(0);
            player.bombFireCounters.push(1000);
            break;
        case SHIP_FASTER:
            GameState.gatheredSpeedLevel++;
            break;
        case SHIP_MAX_HEALTH:
            GameState.gatheredHealthLevel++;
            player.maxHealth = getMaxHealth();
            player.health = player.maxHealth;
            break;
        case SHIP_HEALTH:
            player.health += 0.5;
            if (player.health > player.maxHealth) {
                player.health = player.maxHealth;
            }
            break;
    }
    var sp = getGoodScreenMessagePos();
    var message = new ScreenMessage(powerupTexts[type], sp.x, sp.y);
    GameState.screenMessages.push(message);

}


function getGoodScreenMessagePos() {
    var sp = worldToScreenPosition(GameState.player.threeObject.position);
    sp.y -= 0;
    for (var i=0; i<GameState.screenMessages.length; i++) {
        var m = GameState.screenMessages[i];
        if (Math.abs(m.position.y - sp.y) < 30) {
            sp.y += 30;
        }
    }
    return sp;
}