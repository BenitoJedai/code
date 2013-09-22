using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebGLDiceByEBLOG;
using WebGLDiceByEBLOG.Design;
using WebGLDiceByEBLOG.HTML.Pages;

namespace WebGLDiceByEBLOG
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        class xdice
        {
            public THREE.CSS3DObject dice;
            public CANNON.RigidBody rigid;
        }

        class xdiceface
        {
            public IHTMLImage img;
            public double[] position;
            public double[] rotation;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://blog.cuartodejuegos.es/wp-content/uploads/2011/06/Story-cubes-caras.jpg
            // http://css-eblog.com/3d/box.html

            var w = Native.window.Width;
            var h = Native.window.Height;

            //      var world, ground, 
            var timeStep = 1 / 60.0;

            //diceRigid, dice,
            //camera, scene, renderer, floorObj,
            //startDiceNum = 3,
            var cubeSize = 3.0;

            #region Cannonの世界を生成
            var world = new CANNON.World();

            //重力を設定
            world.gravity.set(0, -90.82, 0);
            world.broadphase = new CANNON.NaiveBroadphase();
            world.solver.iterations = 10;
            world.solver.tolerance = 0.001;

            //地面用にPlaneを生成
            var plane = new CANNON.Plane();

            //Planeの剛体を質量0で生成する
            var ground = new CANNON.RigidBody(0, plane);

            //X軸に90度（つまり地面）に回転
            ground.quaternion.setFromAxisAngle(new CANNON.Vec3(1, 0, 0), -Math.PI / 2);
            ground.position.set(
                50, 0, 50);

            world.add(ground);
            #endregion


            #region initThree() {


            //var camera = new THREE.OrthographicCamera(-w / 2, w / 2, h / 2, -h / 2, 1, 1000);
            var camera = new THREE.PerspectiveCamera(60, w / (double)h, 0.1, 1000);
            camera.position.set(
                0,
                50,
                0
            );

            camera.lookAt(new THREE.Vector3(0, 0, 0));

            var scene = new THREE.Scene();
            var renderer = new THREE.CSS3DRenderer();
            renderer.setSize(w, h);

            var textureSize = 800;
            var floorEle = new IHTMLDiv();
            //floorEle.style.width = textureSize + "px";
            //floorEle.style.height = textureSize + "px";

            floorEle.style.width = 100 + "px";
            floorEle.style.height = 100 + "px";

            new WebGLDiceByEBLOG.HTML.Images.FromAssets.background().ToBackground(floorEle.style, true);

            //floorEle.style.background = 'url(http://jsrun.it/assets/d/x/0/w/dx0wl.png) left top repeat';
            //floorEle.style.backgroundSize = textureSize / 20 + "px " + textureSize / 20 + "px";
            (floorEle.style as dynamic).backgroundSize = textureSize / 20 + "px " + textureSize / 20 + "px";

            var floorObj = new THREE.CSS3DObject(floorEle);
            //floorObj.position.fromArray(new double[] { 100, 0, 0 });
            //floorObj.rotation.fromArray(new double[] { Math.PI / 2.0, 0, 0 });
            scene.add(floorObj);

            scene.add(camera);

            renderer.domElement.AttachToDocument();

            renderer.render(scene, camera);
            #endregion }



            #region createDice
            Func<xdice> createDice = delegate
            {


                Console.WriteLine("before array");


                //t = new Array(3);
                //p.rotation = t;

                var zero = 0.0;


                #region boxInfo
                var boxInfo = new[] 
                {
                         new xdiceface {
                        img = (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num4(),
                        position= new double [] { 0, 0, -cubeSize },

                        // jsc, please allow 
                        rotation= new double [] { 0, 0, zero }
                    },


                    new  xdiceface{
                        img= (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num2(),
                        position = new double [] { -cubeSize, 0, 0 },
                        rotation =  new double [] { 0, Math.PI / 2, 0 }
                    },
                    new xdiceface{
                        img = (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num5(),
                        position = new double [] { cubeSize, 0, 0 },
                        rotation= new double [] { 0, -Math.PI / 2, 0 }
                    },
                    new xdiceface{
                        img= (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num1(),
                        position= new double [] { 0,  cubeSize, 0 },
                        rotation= new double [] { Math.PI / 2, 0, Math.PI }
                    },
                    new xdiceface {
                        img= (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num6(),
                        position= new double [] { 0, -cubeSize, 0 },
                        rotation= new double [] { - Math.PI / 2, 0, Math.PI }
                    },
                    new xdiceface{
                        img= (IHTMLImage)new WebGLDiceByEBLOG.HTML.Images.FromAssets.num3(),
                        position= new double [] { 0, 0,  cubeSize },
                        rotation= new double [] { 0, Math.PI, 0 }
                    },
               
                };
                #endregion
                Console.WriteLine("after array");

                //for three.js
                //var el, dice,
                //    info, img, face;

                var el = new IHTMLDiv();
                el.style.width = cubeSize * 2 + "px";
                el.style.height = cubeSize * 2 + "px";
                var dice = new THREE.CSS3DObject(el);

                for (var j = 0; j < boxInfo.Length; j++)
                {
                    Console.WriteLine("after array " + new { j });

                    var info = boxInfo[j];

                    info.img.style.SetSize(
                        (int)(cubeSize * 2),
                        (int)(cubeSize * 2)
                    );


                    var face = new THREE.CSS3DObject(info.img);

                    face.position.fromArray(info.position);
                    face.rotation.fromArray(info.rotation);
                    dice.add(face);
                }

                //Create physics.
                var mass = 1;
                var box = new CANNON.Box(new CANNON.Vec3(cubeSize, cubeSize, cubeSize));
                var body = new CANNON.RigidBody(mass, box);

                //body.position.set(x, y, z);
                //body.velocity.set(0, 0, Math.random() * -50 - 30);

                //body.angularVelocity.set(10, 10, 10);
                //body.angularDamping = 0.001;

                return new xdice
                {
                    dice = dice,
                    rigid = body
                };
            };
            #endregion


            //world.allowSleep = true;
            var stopped = false;

            Func<double> random = new Random().NextDouble;

            #region initAnimation
            Action<xdice> initAnimation = y =>
            {
                var position = new
                {
                    x = 5 + random() * 50.0,
                    y = 5 + random() * 50.0,
                    z = 5 + random() * 5.0,
                };

                Console.WriteLine(new { position });
                y.rigid.position.set(position.x, position.y, position.z);

                y.rigid.velocity.set(
                    random() * 20 + 0,
                    random() * 20 + 10,
                    random() * 20 + 10
                    );


                y.rigid.angularVelocity.set(10, 10, 10);
                y.rigid.angularDamping = 0.001;
            };
            #endregion

            //create a dice.

            var AllDice = new List<xdice>();

            for (int i = 0; i < 9; i++)
            {

                var y = createDice();

                initAnimation(y);

                scene.add(y.dice);
                world.add(y.rigid);

                AllDice.Add(y);

            }





            var target = new THREE.Vector3();

            var lon = 50.0;
            var lat = -72.0;





            var drag = false;

            #region onframe
            Native.window.onframe +=
                delegate
                {
                    if (Native.document.pointerLockElement == Native.document.body)
                        lon += 0.00;
                    else
                        lon += 0.01;

                    lat = Math.Max(-85, Math.Min(85, lat));

                    Native.document.title = new { lon = (int)lon, lat = (int)lat }.ToString();


                    var phi = THREE.Math.degToRad(90.0 - lat);
                    var theta = THREE.Math.degToRad(lon);

                    target.x = Math.Sin(phi) * Math.Cos(theta);
                    target.y = Math.Cos(phi);
                    target.z = Math.Sin(phi) * Math.Sin(theta);



                    #region updatePhysics();
                    //物理エンジンの時間を進める
                    world.step(timeStep);

                    //物理エンジンで計算されたbody(RigidBody)の位置をThree.jsのMeshにコピー
                    AllDice.WithEach(
                        y =>
                        {
                            y.rigid.position.copy(y.dice.position);
                            y.rigid.quaternion.copy(y.dice.quaternion);

                            //y.rigid.position.copy(camera.position);
                        }
                    );

                    if (!drag)
                    {
                        camera.lookAt(
                            new THREE.Vector3(
                                AllDice.Average(i => i.rigid.position.x),
                                AllDice.Average(i => i.rigid.position.y),
                                AllDice.Average(i => i.rigid.position.z)
                            )
                        );



                    }
                    else
                    {
                        camera.lookAt(
                            new THREE.Vector3(
                                target.x + camera.position.x,
                                target.y + camera.position.y,
                                target.z + camera.position.z
                            )
                        );
                    }

                    ground.position.copy(floorObj.position);
                    ground.quaternion.copy(floorObj.quaternion);
                    #endregion

                    renderer.render(scene, camera);
                };
            #endregion




            #region sleepy
            AllDice.Last().With(
                x =>
                {

                    x.rigid.addEventListener("sleepy",

                        IFunction.OfDelegate(
                            new Action(
                                delegate
                                {

                                    var px = new THREE.Vector4(1, 0, 0, 0);
                                    var nx = new THREE.Vector4(-1, 0, 0, 0);
                                    var py = new THREE.Vector4(0, 1, 0, 0);
                                    var ny = new THREE.Vector4(0, -1, 0, 0);
                                    var pz = new THREE.Vector4(0, 0, 1, 0);
                                    var nz = new THREE.Vector4(0, 0, -1, 0);
                                    var UP = 0.99;
                                    //tmp;

                                    #region showNum
                                    Action<int> showNum = num =>
                                    {
                                        new { num }.ToString().ToDocumentTitle();
                                    };

                                    if (px.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(5);
                                    }
                                    else if (nx.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(2);
                                    }
                                    else if (py.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(1);
                                    }
                                    else if (ny.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(6);
                                    }
                                    else if (pz.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(3);
                                    }
                                    else if (nz.applyMatrix4(x.dice.matrixWorld).y > UP)
                                    {
                                        showNum(4);
                                    }
                                    #endregion

                                    stopped = true;
                                }
                            )
                        )
                    );
                }
            );

            #endregion

            #region onresize
            Action AtResize = delegate
            {
                camera.aspect = Native.window.Width / Native.window.Height;
                camera.updateProjectionMatrix();
                renderer.setSize(Native.window.Width, Native.window.Height);
            };
            Native.window.onresize +=
              delegate
              {
                  AtResize();
              };
            #endregion


            #region onclick
            var button = new IHTMLDiv();

            button.style.position = IStyle.PositionEnum.absolute;
            button.style.left = "0px";
            button.style.right = "0px";
            button.style.bottom = "0px";
            button.style.height = "3em";
            button.style.backgroundColor = "rgba(0,0,0,0.5)";

            button.AttachToDocument();



            button.onmousedown +=
                e =>
                {
                    e.stopPropagation();
                };

            button.ontouchstart +=
                e =>
                {
                    e.stopPropagation();
                };

            button.ontouchmove +=
                 e =>
                 {
                     e.stopPropagation();
                 };

            button.onclick +=
                delegate
                {
                    stopped = false;

                    AllDice.WithEach(
                       y =>
                       {
                           initAnimation(y);
                       }
                    );

                };
            #endregion

            #region ontouchmove
            var touchX = 0;
            var touchY = 0;

            Native.document.body.ontouchend +=
               e =>
               {
                   drag = false;
               };


            Native.document.body.ontouchstart +=
                e =>
                {
                    drag = true;
                    e.preventDefault();

                    var touch = e.touches[0];

                    touchX = touch.screenX;
                    touchY = touch.screenY;

                };


            Native.document.body.ontouchmove +=
              e =>
              {
                  e.preventDefault();

                  var touch = e.touches[0];

                  lon -= (touch.screenX - touchX) * 0.1;
                  lat += (touch.screenY - touchY) * 0.1;

                  touchX = touch.screenX;
                  touchY = touch.screenY;

              };
            #endregion






            #region camera rotation
            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        drag = true;
                        lon += e.movementX * 0.1;
                        lat -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  drag = Native.document.pointerLockElement != null;
                  e.preventDefault();
              };

            Native.document.body.onmousedown +=
                e =>
                {
                    //e.CaptureMouse();

                    drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                };


            Native.document.body.ondblclick +=
                e =>
                {
                    e.preventDefault();

                    Console.WriteLine("requestPointerLock");
                };

            #endregion






        }

    }
}
