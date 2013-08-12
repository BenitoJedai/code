using flare.basic;
using flare.core;
using flare.flsl;
using flare.loaders;
using flare.primitives;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;

namespace Flare3DWaterShips
{
    public sealed class ApplicationSprite : Sprite
    {
        // flsl ocean shader.
        const int oceanGridSize = 64;

        private BitmapData bmp = new BitmapData(oceanGridSize, oceanGridSize, false);
        private Point point0 = new Point();
        private Point point1 = new Point();

        public ApplicationSprite()
        {

            var scene = new Viewer3D(this, "", 0.2);
            scene.camera = new Camera3D();
            scene.camera.setPosition(120, 40, -30);
            scene.camera.lookAt(0, 0, 0);

            #region mirrorCam
            var mirrorCam = new Camera3D();
            var mirror = new Texture3D(new Point(512, 512));
            mirror.mipMode = Texture3D.MIP_NONE;
            mirror.wrapMode = Texture3D.WRAP_CLAMP;
            mirror.upload(scene);
            #endregion


            #region oceanShader
            var Ocean = KnownEmbeddedResources.Default["assets/Flare3DWaterShips/ocean07.flsl.compiled"];
            var oceanShader = new FLSLMaterial("oceanShader", Ocean.ToByteArrayAsset());
            var oceanShader_params = oceanShader.@params as dynamic;
            oceanShader_params.skyMap.value = new Texture3D("assets/Flare3DWaterShips/highlights.png", false, Texture3D.FORMAT_CUBEMAP);
            oceanShader_params.normalMap.value = new Texture3D("assets/Flare3DWaterShips/normalmap.jpg");
            oceanShader_params.mirrorMap.value = mirror;
            #endregion

            var water = new Plane("water", 3000, 3000, oceanGridSize - 1, oceanShader, "+xz");
            var skybox = new SkyBox("assets/Flare3DWaterShips/skybox.png", SkyBox.HORIZONTAL_CROSS, null, 1);

            // http://www.flare3d.com/support/index.php?topic=63.0
            // http://www.flare3d.com/docs/tutorials/loadFromBytes/
            var ship0 = new Flare3DWaterShipComponent.ship();
            ship0.load();

            // how to make it safe to provide 3rd party assetslibrary builder
            // code templates? which can be selected by KnownEmbeddedResources pattern match?
            // class XFileTemplate<T> where T like foo "*.zf3d"

            var ship1 = new Flare3DWaterShipComponent.ship();
            ship1.load();
            ship1.x = 40;
            ship1.y = 10;


            var particles = new Flare3DLoader("assets/Flare3DWaterShips/particles.zf3d");
            particles.load();

            #region initWaves():
            var bytes = new ByteArray();
            bytes.endian = Endian.LITTLE_ENDIAN;
            bytes.length = oceanGridSize * oceanGridSize * 12; // 4 btyes * RGB = 12.

            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/display/ShaderData.html
            //   public static class ShaderData
            //   {
            //       public ShaderData(ByteArray byteCode);
            //   }


            var PBWater = KnownEmbeddedResources.Default["assets/Flare3DWaterShips/water.pbj"];
            var shader = new Shader(PBWater.ToByteArrayAsset());
            var shader_data = shader.data as dynamic;

            shader_data.src.input = bmp;

            var surf = new Surface3D("data");

            // http://www.flare3d.com/docs/flare/core/Surface3D.html
            //     public override int addVertexData(uint dataIndex, int size = -1, Vector<double> vector = null);
            surf.addVertexData(dataIndex: (uint)Surface3D.COLOR0, size: 3);
            surf.vertexBytes = bytes;
            surf.upload(scene);

            water.surfaces[0].sources[Surface3D.COLOR0] = surf;
            #endregion

            ship0.addChild(particles);

            scene.addChild(skybox);
            scene.addChild(water);
            scene.addChild(ship0);
            scene.addChild(ship1);

            var st = new Stopwatch();
            st.Start();

            ship1.setScale(2, 2, 2);

            scene.addEventListener(Scene3D.RENDER_EVENT,

                listener: new Action<Event>(
                    delegate
                    {
                        // render the big waves.
                        #region  renderWaves()
                        {
                            var timer = st.ElapsedMilliseconds;
                            point0.y = timer / 400;
                            point1.y = timer / 640;

                            // flash natives: apply params?
                            bmp.perlinNoise(3, 3, 2, 0, false, true, 7, true, new[] { point0, point1 });

                            var job = new ShaderJob(shader, bytes, oceanGridSize, oceanGridSize);
                            //job.addEventListener( ShaderEvent.COMPLETE, shaderCompleteEvent, false, 0, true );
                            job.complete +=
                                delegate
                                {
                                    if (surf.vertexBuffer != null)
                                        surf.vertexBuffer.uploadFromByteArray(bytes, 0, 0, oceanGridSize * oceanGridSize);
                                };

                            job.start();
                        }
                        #endregion

                        // copy from the main camera and invert in Y axis.
                        mirrorCam.copyTransformFrom(scene.camera);
                        mirrorCam.transform.appendScale(1, -1, 1);

                        // setup the mirror cam to start rendering on the mirror texture.
                        scene.setupFrame(mirrorCam);
                        scene.context.setRenderToTexture(mirror.texture, true);
                        scene.context.clear(0, 0, 0, 0);

                        // get the pixel color height.
                        var color = bmp.getPixel(oceanGridSize / 2, oceanGridSize / 2) & 0xff;

                        // ! .0 marks it as double. otherwise ship will digitally step on water
                        var height = color / 255.0 * 20 + 1;

                        // draw objects into the mirror texture.
                        ship0.y = -height;
                        ship0.draw();
                        ship0.y = height;

                        ship1.z = st.ElapsedMilliseconds * 0.001 - 100;

                        ship1.y = -height;
                        ship1.draw();
                        ship1.y = height;


                        // get back to the main render.
                        scene.context.setRenderToBackBuffer();
                        scene.setupFrame(scene.camera);
                    }
                ).ToFunction()
            );

      
        }








    }
}
