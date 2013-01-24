
var allResourcesLoaded = false;

var totalResourceCount = 0;
var resourcesLoaded = 0;

var allSoundsLoaded = false;
var totalSoundCount = 0;
var soundsLoaded = 0;

var sounds = {
//    menu: {url: "menu.mp3"},
    playSong1: {url: "playSong1.mp3"},
    fail: {url: "failure.mp3"},
    success: {url: "success.mp3"},
    menu: {url: "menu.mp3"},
    hitBuilding: {url: "hitbuilding.wav"},
    hitEnemy: {url: "hitenemy.wav"},
    explosion: {url: "explosion.wav"},
    cannon: {url: "shot.wav"},
    bomb: {url: "dropbomb.wav"},
    extraPoints: {url: "extrapoints.wav"},
    shipFaster: {url: "shipfaster.wav"},
    shipHealth: {url: "shiphealth.wav"},
    shipMaxHealth: {url: "shipmaxhealth.wav"},
    cannonFaster: {url: "cannonfaster.wav"},
    cannonDamageUp: {url: "cannondamageup.wav"},
    cannonNew: {url: "cannonnew.wav"},
    bombFaster: {url: "bombfaster.wav"},
    bombDamageUp: {url: "bombdamageup.wav"},
    bombNew: {url: "bombnew.wav"}
//    success: {url: "success.mp3"},
//    failure: {url: "failure.mp3"},
//    win: {url: "win.mp3"},
//    boss: {usr: "boss.mp3"}
};
var images = {
    test: {url: "test.png"}
};
var levels = [
    {url: "level1.json"},
    {url: "level2.json"},
    {url: "level3.json"}
]

var levelArr = [];

var fonts = {
    font: {url: "font"}
};

var objectTemplates = {
    wagon: {url: "wagon.xml"}
};

function resourceLoaded() {
    resourcesLoaded++;
    if (resourcesLoaded == totalResourceCount) {
        allResourcesLoaded = true;
        logit("All resources loaded!");
    } else {
//        logit("Not All resources loaded yet " + resourcesLoaded + " of " + totalResourceCount);
    }
}

function loadSoundResource(info, prefix) {
    var theSound = soundManager.createSound({
        id: info.url,
        url: prefix + info.url,
        autoLoad: true,
        volume: 100,
        onload: function() {
            soundsLoaded++;
            resourceLoaded();
            info.sound = theSound;
            if (soundsLoaded == totalSoundCount) {
                allSoundsLoaded = true;
                logit("All sounds loaded! " + soundsLoaded);
            }
//            logit("Not all sounds loaded yet " + soundsLoaded);
        }
    });
    info.sound = theSound;
}

function loadTextResourceWithAjax(url, successCallback, failureCallback) {
    $.ajax(url, {
        complete: function(jqXhr, textStatus) {
            if (textStatus == "success") {
                if (successCallback) {
                    resourceLoaded();
                    successCallback(jqXhr);
                }
            } else {
                if (failureCallback) {
                    failureCallback(jqXhr, textStatus);
                }
            }
        },
        type: 'GET'
    });
}


function loadResources() {
    $.each(sounds, function(prop, value) {
        totalResourceCount++;
        totalSoundCount++;
    });
    $.each(levels, function(prop, value) {
        totalResourceCount++;
    });

    // Setup the sound manager before loading anything
    soundManager.setup({
        url: 'swf/',
        flashVersion: 9,
        debugMode: true,
        debugFlash: true,
        onready: function() {
            logit("Soundmanager is ready!!!");
            $.each(sounds, function(prop, value) {
//                logit("Loading sound " + prop + " " + value);
                loadSoundResource(value, "sounds/");
            });

        }
    });


    $.each(levels, function(prop, value) {
//        logit("Loading level " + prop + " " + value.url);

        loadTextResourceWithAjax("levels/" + value.url,
            function(jqXhr) {
//                var xml = $.parseXML(jqXhr.responseText);
//                var $xml = $(xml);

//                console.log(xml);

//                var levelJson = xml2json(xml);
//                logit(levelJson);

                var level = JSON.parse(jqXhr.responseText);

                logit("loading level " + prop);

                levelArr[prop] = level;

            },
            function() {
                console.log("Could not load level: " + JSON.stringify(value));
            });
    });

}




