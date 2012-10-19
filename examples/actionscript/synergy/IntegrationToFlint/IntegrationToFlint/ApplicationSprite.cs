using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using org.flintparticles.threeD.emitters;
using org.flintparticles.threeD.renderers;
using ScriptCoreLib.ActionScript.flash.text;
using org.flintparticles.threeD.actions;
using org.flintparticles.threeD.geom;
using org.flintparticles.threeD.zones;
using org.flintparticles.threeD.particles;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using org.flintparticles.common.particles;

namespace IntegrationToFlint
{
    internal sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;

        private Emitter3D emitter;
        private Bitmap bitmap;
        private DisplayObjectRenderer renderer;

        public ApplicationSprite()
        {
            // see also: http://zproxy.wordpress.com/2009/05/29/using-flint-particle-system-from-jsc/

            
            bitmap = new ActionScript.Images._184098();

            renderer = new DisplayObjectRenderer();
            renderer.camera.dolly(-400);
            renderer.camera.projectionDistance = 400;
            //renderer.y = 175;
            renderer.y = 0;
            renderer.x = 250;
            addChild(renderer);

            emitter = new Emitter3D();
            emitter.addAction(new Move());
            emitter.addAction(new DeathZone(new FrustrumZone(renderer.camera, new ScriptCoreLib.ActionScript.flash.geom.Rectangle(-2900, -2150, 5800, 4300)), true));
            emitter.position = new Vector3D(0, 0, 0, 1);

            var __JSC_should_detect_element_type_for_Vector_of_Particle = default(Particle);

            var particles = Particle3DUtils.createRectangleParticlesFromBitmapData(
                bitmap.bitmapData, 
                20, 
                emitter.particleFactory, 
                new Vector3D(-192, 127, 0)
            );

            emitter.addParticles(particles, false);

            renderer.addEmitter(emitter);
            emitter.start();

            stage.click += explode;

            var txt = new TextField();
            txt.text = "Click on the image";
            addChild(txt);

        }

        public void explode(MouseEvent ev)
        {
            var p = renderer.globalToLocal(new ScriptCoreLib.ActionScript.flash.geom.Point(ev.stageX, ev.stageY));
            emitter.addAction(new Explosion(8, new Vector3D(p.x, -p.y, 50), 500));
            stage.click -= explode;
        }

    }
}
