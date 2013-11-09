using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebGLGoldDropletTransactions;
using WebGLGoldDropletTransactions.Data;
using WebGLGoldDropletTransactions.Design;
using WebGLGoldDropletTransactions.HTML.Pages;
using WebGLOBJExperiment;

namespace WebGLGoldDropletTransactions
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.header.style.backgroundColor = "";

            page.header.css.style.transition = "background-color 200ms linear";
            page.header.css.style.backgroundColor = "rgba(255, 255, 0, 0.2)";

            page.header.css.hover.style.backgroundColor = "rgba(255, 255, 0, 1.0)";


            var oo = new List<THREE.Object3D>();

            var window = Native.window;

            var camera = new THREE.PerspectiveCamera(
                45,
                page.header.clientWidth / (double)page.header.clientHeight,
                1,
                2000
                );
            camera.position.z = 400;

            // scene

            var scene = new THREE.Scene();

            var ambient = new THREE.AmbientLight(0x101030);
            scene.add(ambient);

            var directionalLight = new THREE.DirectionalLight(0xffeedd);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);

            var renderer = new THREE.WebGLRenderer();

            //renderer.domElement.AttachToDocument();
            renderer.domElement.AttachTo(page.header);
            renderer.setSize(page.header.clientWidth, page.header.clientHeight);
            //renderer.setSize(window.Width, window.Height);
            //renderer.domElement.style.SetLocation(0, 0);


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();

            Native.window.onframe +=
                delegate
                {

                    oo.WithEach(
                        x =>
                            x.rotation.y = st.ElapsedMilliseconds * 0.001
                    );


                    camera.position.x += (mouseX - camera.position.x) * .05;
                    camera.position.y += (-mouseY - camera.position.y) * .05;

                    camera.lookAt(scene.position);

                    renderer.render(scene, camera);


                };

            Native.window.onresize +=
                delegate
                {
                    camera.aspect = page.header.clientWidth / (double)page.header.clientHeight;

                    camera.updateProjectionMatrix();

                    //renderer.setSize(window.Width, window.Height);
                    renderer.setSize(page.header.clientWidth, page.header.clientHeight);

                };




            var data = Book1.GetDataSet();

            var g = new DataGridView
            {
                BackgroundColor = Color.Transparent,


                // does this work?
                DefaultCellStyle = new DataGridViewCellStyle
                {

                    SelectionBackColor = Color.Yellow,

                    BackColor = Color.Transparent
                },


                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                AllowUserToAddRows = false,
                RowHeadersVisible = false,

                ColumnHeadersVisible = false,

                DataSource = data,
                DataMember = "Assets",
            };

            // this should be the one that maximizes itself onto the parent which is supposed to be absolute in size
            g.GetHTMLTargetContainer().With(
                div =>
                {
                    //div.style.reset();

                    // no scrollbars, thanks
                    div.style.overflow = IStyle.OverflowEnum.hidden;
                    (div.style as dynamic).zIndex = "";

                    div.style.position = IStyle.PositionEnum.relative;
                    div.style.left = "";
                    div.style.top = "";
                    div.style.right = "";
                    div.style.bottom = "";

                    page.assets.Clear();
                    div.AttachTo(page.assets);
                }
            );


            var r = new Random();

            foreach (var i in Enumerable.Range(0, 32))
            {
                new sack_of_gold2().Source.Task.ContinueWithResult(
                   o =>
                   {
                       o.position.y = -80;
                       scene.add(o);
                       oo.Add(o);

                       o.position.x = r.Next(-250, 250);
                       o.position.z = r.Next(-400, 200);
                       o.scale = new THREE.Vector3(0.5, 0.5, 0.5);
                   }
               );

            }



        }

    }
}
