
var globalRnd = new MersenneTwister();

var overProjector = null;

var audioPlayer = null;

var renderSettings = new RenderSettings();

var allSettings = [renderSettings];

var objectTextWidths = {};
var screenMessageTextWidths = {};
var largeTextWidths = {};

var usingWebGL = false;


function showModalDialog(title, content, options) {
    var $dialog = $("<div title=\"" + title + "\" >" + content + "</div>");
    $("#layer3div").append($dialog);

    if (!options) {
        options = {
            modal: true,
            buttons: {
                "Ok": function() {
                    $( this ).dialog( "close" );
                }
            }
        }
    }
    $dialog.dialog(options);
    return $dialog;
}

function logit(str) {
    console.log(str);
}


function loadSettingsFromLocalStorage() {
    for (var i=0; i<allSettings.length; i++) {
        var settings = allSettings[i];
        settings.loadFromLocalStorage();
    }
}
function saveSettingsToLocalStorage() {
    for (var i=0; i<allSettings.length; i++) {
        var settings = allSettings[i];
        if (settings.dirty) {
            settings.saveToLocalStorage();
            settings.dirty = false;
        }
    }
}

function playGameSong() {
    var successSong = sounds.success.sound;
    var failSong = sounds.fail.sound;
    var gameSong = sounds.playSong1.sound;
    var menuSong = sounds.menu.sound;

    if (successSong.playState == 1) {
        successSong.stop();
    }
    if (gameSong.playState == 0) {
        gameSong.play();
    }
    if (failSong.playState == 1) {
        failSong.stop();
    }
    if (menuSong.playState == 1) {
        menuSong.stop();
    }

}

function playMenuSong() {
    var successSong = sounds.success.sound;
    var failSong = sounds.fail.sound;
    var gameSong = sounds.playSong1.sound;
    var menuSong = sounds.menu.sound;

    if (successSong.playState == 1) {
        successSong.stop();
    }
    if (gameSong.playState == 1) {
        gameSong.stop();
    }
    if (failSong.playState == 1) {
        failSong.stop();
    }
    if (menuSong.playState == 0) {
        menuSong.play();
    }

}

function playSuccessSong() {
    var successSong = sounds.success.sound;
    var failSong = sounds.fail.sound;
    var gameSong = sounds.playSong1.sound;
    var menuSong = sounds.menu.sound;

    if (successSong.playState == 0) {
        successSong.play();
    }
    if (gameSong.playState == 1) {
        gameSong.stop();
    }
    if (failSong.playState == 1) {
        failSong.stop();
    }
    if (menuSong.playState == 1) {
        menuSong.stop();
    }
}

function playFailSong() {
    var successSong = sounds.success.sound;
    var failSong = sounds.fail.sound;
    var gameSong = sounds.playSong1.sound;
    var menuSong = sounds.menu.sound;

    if (successSong.playState == 1) {
        successSong.stop();
    }
    if (gameSong.playState == 1) {
        gameSong.stop();
    }
    if (failSong.playState == 0) {
        failSong.play();
    }
    if (menuSong.playState == 1) {
        menuSong.stop();
    }
}



function setup() {

    loadSettingsFromLocalStorage();

    var $canvas = $("#canvas");
    var $overCcanvas = $("#overCanvas");

    var canvas = $canvas[0];
    var overCanvas = $overCcanvas[0];

    GameState.canvas = canvas;
    GameState.overCanvas = overCanvas;
    GameState.overCanvasContext = overCanvas.getContext("2d");


    var startTime = Date.now();

    if (Detector.webgl && !renderSettings.forceContext2D) {
        var webGLOptions = {
            addBloom: renderSettings.addBloom,
            addSimulatedAA: renderSettings.addSimulatedAA,
            addVignette: renderSettings.addVignette
        };
        GameState.renderer = new WebGLRenderer3D(canvas, webGLOptions);
        usingWebGL = true;
    } else {
        GameState.renderer = new CanvasRenderer3D(canvas, {});
    }

    function setRendererSize() {
        var w = window.innerWidth;
        var h = window.innerHeight;

        if (!w || !h) {
            w = $document.innerWidth();
            h = $document.innerWidth();
        }
        GameState.overCanvas.width = w;
        GameState.overCanvas.height = h;
        GameState.renderer.resized(w, h);

    }

    var $window = $(window);

    $window.on("resize", function() {
        setRendererSize();
        GameState.renderer.render();
    });

    setRendererSize();

    setupInput();


    var stepTime = -1;
    var millisPerStep = 15;

    function animate(t) {
        requestAnimationFrame( animate );

        if (allResourcesLoaded) {

            if (stepTime == -1) {
                stepTime = t - millisPerStep - 1;

//                GameState.$mainMenu = showModalDialog("Menu", "Are you ready?", {
//                    modal: true,
//                    buttons: {
//                        "Start": function() {
//                            GameState.subState = GameSubState.SHOWING_LEVEL_INTRO;
//                            GameState.counter1 = 0;
//                            $( this ).dialog( "close" );
//                        }
//                    }
//                });

            }

            var diffStepTime = t - stepTime;

            var steps = Math.ceil(diffStepTime / millisPerStep);


            switch (GameState.subState) {
                case GameSubState.COMPLETING:
                case GameSubState.COMPLETING_GAME:
                    playSuccessSong();
                    break;
                case GameSubState.SHOWING_GAME_COMPLETE:
                    playMenuSong();
                    break;
                case GameSubState.PLAYING:
                    playGameSong();
                    break;
                case GameSubState.SHOWING_LEVEL_INTRO:
                    playMenuSong();
                    break;
                case GameSubState.DYING:
                    playFailSong();
                    break;
                case GameSubState.SHOWING_GAME_MENU:
                    playMenuSong();
                    break;
                case GameSubState.SHOWING_LEVEL_COMPLETE:
                    playMenuSong();
                    break;
            }


            if (GameState.currentLevel == null) {
                changeLevel(GameState.initialLevelIndex);

                if (GameState.currentLevel == null) {
                    logit("Failed to load initial level");
                    GameState.currentLevel = {};
                }
            }

            var oldSubState = GameState.subState;

            var steppedLevel = true;

            var actualSteps = steps;
            if (steps > 6) {
                actualSteps = 6;
                stepTime = t - actualSteps * millisPerStep;
            }
            for (var i=0; i<actualSteps; i++) {
                stepTime += millisPerStep;
                stepLevel();

                GameState.step();
                GameState.counter2++;
            }
            switch (GameState.subState) {
                case GameSubState.COMPLETING:
                case GameSubState.COMPLETING_GAME:
                case GameSubState.DYING:
                    paintLargeTextEvent();
                    break;
                case GameSubState.PLAYING:
                    GameState.renderer.render(GameState.scene, GameState.camera);
                    renderOverCanvas();
                    break;
                case GameSubState.SHOWING_GAME_MENU:
                    paintGameMenuOverlay();
                    break;
                case GameSubState.SHOWING_LEVEL_INTRO:
                    paintLevelIntro();
                    break;
                case GameSubState.SHOWING_LEVEL_COMPLETE:
                    paintLevelComplete();
//                    GameState.renderer.render(GameState.completeScene, GameState.camera);
                    break;
                case GameSubState.SHOWING_GAME_COMPLETE:
                    paintGameComplete();
//                    GameState.renderer.render(GameState.completeScene, GameState.camera);
                    break;
            }

            if (steppedLevel) {
                // Only remove justPressed when level has been stepped
                Input.step();
            }
        } else {
            paintLoadingProgress();
        }
    }

    animate();

}

function paintBar(ctx, x, y, w, h, fraction) {
    ctx.strokeRect(x, y, w, h);
    var border = 2;
    ctx.fillRect(x + border, y + border, Math.round(fraction * (w - 2 * border)), h - 2 * border);
}

function paintObjectText(ctx, obj) {
    var object = obj.threeObject;

    var objectPos = object.position.clone();
    var scale = object.scale;
    objectPos.addSelf(new THREE.Vector3(0, -scale.y * 0.5, -scale.z * 0.5));

    var textPos = overProjector.projectVector(objectPos, GameState.camera);

    var canvas = ctx.canvas;
    var w = canvas.width;
    var h = canvas.height;

    var wh = w * 0.5;
    var hh = h * 0.5;

    var text = obj.text;
    paintNiceCenteredText(ctx, textPos.x * wh + wh, -textPos.y * hh + hh, text, "#111111", objectTextWidths);
}

function paintNiceText(ctx, px, py, text, bgFillStyle) {
    var canvas = ctx.canvas;
    var w = canvas.width;
    var h = canvas.height;
    if (px > -100 && px < w + 100 && py > -100 && py < h + 100) {
        var oldFillStyle = ctx.fillStyle;
        ctx.fillStyle = bgFillStyle;
        ctx.fillText(text, px + 2, py + 2);
        ctx.fillStyle = oldFillStyle;
        ctx.fillText(text, px, py);
    }
}

function paintNiceCenteredText(ctx, px, py, text, bgFillStyle, textWidths) {
    if (text) {
        var textWidth = textWidths[text];
        if (!textWidth) {
            textWidth = ctx.measureText(text).width;
            textWidths[text] = textWidth;
        }
        var tpx = px - textWidth * 0.5;
        var tpy = py;

        paintNiceText(ctx, tpx, tpy, text, bgFillStyle);
    }
}


function worldToScreenPosition(pos) {

    var objectPos = pos.clone();
    var tempPos = overProjector.projectVector(objectPos, GameState.camera);

    var canvas = GameState.canvas;
    var w = canvas.width;
    var h = canvas.height;

    var wh = w * 0.5;
    var hh = h * 0.5;
    var px = tempPos.x * wh + wh;
    var py = -tempPos.y * hh + hh;

    return new THREE.Vector2(px, py);
}

function paintHealthBar(ctx, obj) {

    var object = obj.threeObject;

    var objectPos = object.position.clone();
    var scale = object.scale;
    objectPos.addSelf(new THREE.Vector3(0, -scale.y * 0.5, -scale.z * 0.5));


    var barWidth = obj.maxHealth * 40;
    var barHeight = 15;

    var healthBarPos = overProjector.projectVector(objectPos, GameState.camera);

    var canvas = ctx.canvas;
    var w = canvas.width;
    var h = canvas.height;

    var wh = w * 0.5;
    var hh = h * 0.5;
    var px = healthBarPos.x * wh + wh - barWidth * 0.5;
    var py = -healthBarPos.y * hh + hh;

    var frac = obj.health / obj.maxHealth;

    if (frac > 0.85) {
        ctx.fillStyle = "#00ff00";
    } else if (frac > 0.5) {
        ctx.fillStyle = "#ffff00";
    } else {
        ctx.fillStyle = "#ff0000";
    }
    ctx.strokeStyle = "#444444";

    paintBar(ctx, px, py, barWidth, barHeight, frac);
//    ctx.strokeRect(px, py, barWidth, barHeight);
//    var border = 2;
//    ctx.fillRect(px + border, py + border, Math.round(frac * (barWidth - 2 * border)), barHeight - 2 * border);

//    ctx.strokeText("Player", px, py, 200);

}

function paintTexts(ctx, x, y, lineHeight, texts) {
    var currentY = y;
    for (var i=0; i<texts.length; i++) {
        var text = texts[i];
        if (text) {
            ctx.fillText(text, x, currentY);
        }
        currentY += lineHeight;
    }
    return currentY;
}

function paintPressAny(ctx, x, y, text) {
    if (!text) {
        text = "Press any significant key";
    }
    var period = 100;
    var m = GameState.counter1 % period;
    if (m < 70) {
        ctx.fillText(text, x, y);
    }
}

function paintWebGLWarning(ctx, x, y, lineHeight) {
    if (!usingWebGL) {
        paintTexts(ctx, x, y, lineHeight, ["NOT Using WebGL", "It will look extremely crappy", "Try Chrome or Firefox :)"])
    }
}


function paintGameMenuOverlay() {
    var ctx = GameState.overCanvasContext;
    var canvas = ctx.canvas;

    var w = canvas.width;
    var h = canvas.height;

    ctx.fillStyle = "#222222";
    ctx.fillRect(0, 0, w, h);

    ctx.fillStyle = "#dddddd";
    ctx.font = '30px sans-serif';
    ctx.fillText("EVIL CHOPPER", 50, 50);
    ctx.font = '20px sans-serif';
    var currentY = 150;
    var lineHeight = 30;
    var texts = [
        "Why did they have to laugh?",
        "Now they all have to die..."
    ];
    currentY = paintTexts(ctx, 50, currentY, lineHeight, texts);
    paintPressAny(ctx, 50, currentY + lineHeight);

    ctx.fillStyle = "#ff0000";
    paintWebGLWarning(ctx, 50, currentY + lineHeight * 3, lineHeight);
}

function paintLevelIntro() {
    var ctx = GameState.overCanvasContext;
    var canvas = ctx.canvas;

    var w = canvas.width;
    var h = canvas.height;

    ctx.fillStyle = "#000000";
    ctx.strokeStyle = "#aaaaaa";
    ctx.fillRect(0, 0, w, h);

    ctx.fillStyle = "#dddddd";
    ctx.font = '30px sans-serif';
    ctx.fillText("Level " + (GameState.levelIndex + 1), 50, 50);

    ctx.font = '20px sans-serif';
    var currentY = 150;
    var lineHeight = 30;
    var texts = [];
    switch (GameState.levelIndex) {
        case 0:
            texts = [
                "Arrow keys or ASDW to steer",
                "Z, C, B to fire cannon(s)",
                "X, V, N to drop bomb(s)"
            ];
            break;
        case 1:
            texts = [
                ""
            ];
            break;
    }
    currentY = paintTexts(ctx, 50, currentY, lineHeight, texts);
    paintPressAny(ctx, 50, currentY + lineHeight);

    ctx.fillStyle = "#ff0000";
    paintWebGLWarning(ctx, 50, currentY + lineHeight * 3, lineHeight);

}

function paintLevelComplete() {
    var ctx = GameState.overCanvasContext;
    var canvas = ctx.canvas;

    var w = canvas.width;
    var h = canvas.height;

    ctx.fillStyle = "#000000";
    ctx.strokeStyle = "#aaaaaa";
    ctx.fillRect(0, 0, w, h);

    ctx.fillStyle = "#dddddd";
    ctx.font = '30px sans-serif';
    ctx.fillText("Level Complete!", 50, 50);

    ctx.font = '20px sans-serif';
    var currentY = 150;
    var lineHeight = 30;
    var texts = [
    ];
    currentY = paintTexts(ctx, 50, currentY, lineHeight, texts);
    paintPressAny(ctx, 50, currentY + lineHeight);

}

function paintGameComplete() {
    var ctx = GameState.overCanvasContext;
    var canvas = ctx.canvas;

    var w = canvas.width;
    var h = canvas.height;

    ctx.fillStyle = "#000000";
    ctx.strokeStyle = "#aaaaaa";
    ctx.fillRect(0, 0, w, h);

    ctx.fillStyle = "#dddddd";
    ctx.font = '30px sans-serif';
    ctx.fillText("COMPLETE!!!!", 50, 50);

    ctx.font = '20px sans-serif';
    var currentY = 150;
    var lineHeight = 30;
    var texts = [
        "Thanks to all Ludum Dare organizers and participants!",
        "See you all next time!"
    ];
    currentY = paintTexts(ctx, 50, currentY, lineHeight, texts);
    paintPressAny(ctx, 50, currentY + lineHeight, "Press Escape or Mouse to continue");
}



function paintLoadingProgress() {
    var ctx = GameState.overCanvasContext;
    var canvas = ctx.canvas;

    var w = canvas.width;
    var h = canvas.height;

    ctx.fillStyle = "#555555";
    ctx.fillRect(0, 0, w, h);

    ctx.strokeStyle = "#eeeeee";
    ctx.fillStyle = "#eeeeee";
    var fraction = resourcesLoaded / totalResourceCount;
    ctx.font = '20px sans-serif';
    ctx.fillText("Loading...", 50, 100);

    ctx.fillStyle = "#888888";
    ctx.strokeStyle = "#aaaaaa";
    paintBar(ctx, 50, 50, w - 100, 20, fraction);

    ctx.fillStyle = "#ff0000";
    var lineHeight = 30;
    paintWebGLWarning(ctx, 50, 200, lineHeight);

}

function paintLargeTextEvent() {

    var completeText = "";

    var fillStyle = "#eeeeee";
    switch (GameState.subState) {
        case GameSubState.COMPLETING:
            completeText = "GREAT!";
            break;
        case GameSubState.COMPLETING_GAME:
            completeText = "FANTASTIC!";
            break;
        case GameSubState.DYING:
            completeText = "OH NOOOO!!!!";
            fillStyle = "#ee0202";
            break;
    }

    GameState.renderer.render(GameState.scene, GameState.camera);
    renderOverCanvas();
    GameState.overCanvasContext.font = '30px sans-serif';
    GameState.overCanvasContext.fillStyle = fillStyle;
    var w = GameState.overCanvas.width;
    var h = GameState.overCanvas.height;
    paintNiceCenteredText(GameState.overCanvasContext, w * 0.5, h * 0.25, completeText, "#111111", largeTextWidths);
}

function renderOverCanvas() {
    var canvas = GameState.overCanvas;
    var ctx = GameState.overCanvasContext;
//    ctx.moveTo(0, 0);
//    ctx.lineTo(800, 200);
//    ctx.stroke();
//    ctx.closePath();

    if (!overProjector) {
        overProjector = new THREE.Projector();
    }

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    paintHealthBar(ctx, GameState.player);

    for (var i=0; i<GameState.solids.length; i++) {
        var solid = GameState.solids[i];
        if (solid.health < solid.maxHealth) {
            paintHealthBar(ctx, solid);
        }
    }
    for (var i=0; i<GameState.agents.length; i++) {
        var agent = GameState.agents[i];
        if (agent != GameState.player && agent.health < agent.maxHealth) {
            paintHealthBar(ctx, agent);
        }
    }

    ctx.font = '12px sans-serif';
    ctx.fillStyle = "#eeeeee";
    for (var i=0; i<GameState.pickups.length; i++) {
        paintObjectText(ctx, GameState.pickups[i]);
    }

    ctx.font = '20px sans-serif';
    ctx.fillStyle = "#eeeeee";
    for (var i=0; i<GameState.screenMessages.length; i++) {
        var message = GameState.screenMessages[i];
        paintNiceCenteredText(ctx, message.position.x, message.position.y, message.text, "#050505", screenMessageTextWidths);
//        paintObjectText(ctx, GameState.pickups[i]);
    }

    if (GameState.hitComboTicksLeft > 0) {
        ctx.font = '20px sans-serif';
        var frac = GameState.hitComboTicksLeft / HIT_COMBO_TICKS;
        ctx.fillStyle = "#eeeeee";
        ctx.strokeStyle = "#ffffff";
//        paintNiceText(ctx, 50, 70, "Hit combo", "#222222");
        paintBar(ctx, 50, 50, 100, 20, frac);
    }

    if (GameState.killComboTicksLeft > 0) {
        ctx.font = '20px sans-serif';
        var frac = GameState.killComboTicksLeft / KILL_COMBO_TICKS;
        ctx.fillStyle = "#eeeeee";
        ctx.strokeStyle = "#ffffff";
//        paintNiceText(ctx, 50, 70, "Kill combo", "#222222");
        paintBar(ctx, 50, 80, 100, 20, frac);
    }

}