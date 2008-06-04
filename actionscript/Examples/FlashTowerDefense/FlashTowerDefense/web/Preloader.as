package {
    import flash.display.DisplayObject;
    import flash.display.MovieClip;
    import flash.events.IOErrorEvent;
    import flash.utils.getDefinitionByName;

	import FlashTowerDefense.ActionScript.FlashTowerDefense;
	
    // Must be dynamic!
    [SWF(backgroundColor = 0x00cccccc, frameRate = 30, width = 320, height = 240)]
    public dynamic class Preloader extends MovieClip {
        // Keep track to see if an ad loaded or not
        private var did_load:Boolean;


        // Substitute these for what's in the MochiAd code
        public static var GAME_OPTIONS:Object = {id: "408b0484d7f64aad", res:"320x240"};

        public function Preloader() {
            super();

            var f:Function = function(ev:IOErrorEvent):void {
                // Ignore event to prevent unhandled error exception
            }
            loaderInfo.addEventListener(IOErrorEvent.IO_ERROR, f);

            var opts:Object = {};
            for (var k:String in GAME_OPTIONS) {
                opts[k] = GAME_OPTIONS[k];
            }

            opts.ad_started = function ():void {
                did_load = true;
            }

            opts.ad_finished = function ():void {
                // don't directly reference the class, otherwise it will be
                // loaded before the preloader can begin
                // var mainClass:Class = Class(getDefinitionByName(MAIN_CLASS));
                
                var app:Object = new FlashTowerDefense.ActionScript.FlashTowerDefense();
                parent.addChild(app as DisplayObject);
            }

            opts.clip = this;
            MochiAd.showPreGameAd(opts);
        }


    }

}
