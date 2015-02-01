using System;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using WebGLCelShader.HTML.Pages;
using WebGLCelShader.Shaders;

// upgrade to nuget?
//using THREE = WebGLCelShader.Design.THREE;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebGLCelShader
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        // all we see is red now. why?

        /* Source: http://www.ro.me/tech/demos/6/index.html
         */

        sealed class MyUniforms
        {
            public sealed class MyUniform
            {
                public string type;
                public object value;

            }

            public MyUniform uDirLightPos = new MyUniform { type = "v3", value = new THREE.Vector3() };
            public MyUniform uDirLightColor = new MyUniform { type = "c", value = new THREE.Color(0xeeeeee) };
            public MyUniform uAmbientLightColor = new MyUniform { type = "c", value = new THREE.Color(0x050505) };
            public MyUniform uBaseColor = new MyUniform { type = "c", value = new THREE.Color(0xff0000) };
        }

        public Application(IDefault page = null)
        {
            // https://github.com/mrdoob/three.js/wiki/Migration

            var size = 600;

            var windowHalfX = size / 2;
            var windowHalfY = size / 2;

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            var container = new IHTMLDiv();

            container.AttachToDocument();
            container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);
            container.style.backgroundColor = JSColor.Black;

            var camera = new THREE.PerspectiveCamera(40, Native.window.aspect, 1, 3000);
            camera.position.z = 1000;

            var scene = new THREE.Scene();

            var light = new THREE.DirectionalLight(0xffffff);
            light.position.x = 1;
            light.position.y = 0;
            light.position.z = 1;
            //scene.addLight(light);
            scene.add(light);

            var renderer = new THREE.WebGLRenderer();

            var geometry = new THREE.TorusGeometry(50, 20, 15, 15);

            var uniforms = new MyUniforms();

            // ShaderMaterial
            var material_base = new THREE.ShaderMaterial(
                new
                {
                    uniforms = THREE.UniformsUtils.clone(uniforms),
                    vertex_shader = new GeometryVertexShader().ToString(),
                    fragment_shader = new GeometryFragmentShader().ToString()
                }
            );

            //renderer.initMaterial(material_base, scene.lights, scene.fog);

            #region addObject
            var r = new Random();
            Func<float> Math_random = () => (float)r.NextDouble();


            for (var i = 0; i < 100; i++)
            {
                //var material_uniforms = (MyUniforms)THREE.__ThreeExtras.Uniforms.clone(uniforms);
                var material_uniforms = (MyUniforms)THREE.UniformsUtils.clone(uniforms);

                var material = new THREE.ShaderMaterial(
                     new
                     {
                         uniforms = material_uniforms,
                         vertex_shader = new GeometryVertexShader().ToString(),
                         fragment_shader = new GeometryFragmentShader().ToString()
                     }
                 );


                material.program = material_base.program;

                material_uniforms.uDirLightPos.value = light.position;
                material_uniforms.uDirLightColor.value = light.color;
                material_uniforms.uBaseColor.value = new THREE.Color((int)(Math_random() * 0xffffff));

                var mesh = new THREE.Mesh(geometry, material);
                mesh.position.x = Math_random() * 800f - 400f;
                mesh.position.y = Math_random() * 800f - 400f;
                mesh.position.z = Math_random() * 800f - 400f;

                mesh.rotation.x = Math_random() * 360f * (float)Math.PI / 180f;
                mesh.rotation.y = Math_random() * 360f * (float)Math.PI / 180f;
                mesh.rotation.z = Math_random() * 360f * (float)Math.PI / 180f;

                //scene.addObject(mesh);
                scene.add(mesh);

            }
            #endregion


            ///////////////////////////////

            var c = 0;

            container.appendChild(renderer.domElement);

            #region AtResize
            Action AtResize = delegate
            {
                container.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                camera.aspect = (int)Native.window.aspect;
                camera.updateProjectionMatrix();

                renderer.setSize(Native.window.Width, Native.window.Height);
            };

            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };

            AtResize();
            #endregion

            #region IsDisposed
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                renderer.domElement.Orphanize();
            };
            #endregion

            #region tick

            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                c++;


                var l = scene.children.Length;

                Native.document.title = new { l }.ToString();

                var time = new IDate().getTime() * 0.0004;

                //var l = scene.objects.Length;

                for (var i = 0; i < l; i++)
                {

                    scene.children[i].rotation.x += 0.01f;
                    scene.children[i].rotation.y += 0.01f;

                }

                /*
                light.position.x = Math.sin( time );
                light.position.z = Math.cos( time );
                light.position.y = 0.5;
                light.position.normalize();
                */

                renderer.render(scene, camera);

            };

            #endregion

            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();


                };
            #endregion
        }

        public Action Dispose;

    }


}


public static class X
{
    public static void DebuggerBreakIfMissing(this object i)
    {
        if (i == null)
            Debugger.Break();
    }

    public static TaskAwaiter<IHTMLScript> GetAwaiter(this IHTMLScript i)
    {
        var y = new TaskCompletionSource<IHTMLScript>();
        i.onload += delegate { y.SetResult(i); };
        return y.Task.GetAwaiter();
    }

    public static TaskAwaiter<IHTMLImage> GetAwaiter(this IHTMLImage i)
    {
        var y = new TaskCompletionSource<IHTMLImage>();
        i.InvokeOnComplete(y.SetResult);
        return y.Task.GetAwaiter();
    }

    public static TaskAwaiter<TResult[][]> GetAwaiter<TResult>(this IEnumerable<Task<TResult[]>> i)
    {
        return Task.WhenAll(i).GetAwaiter();
    }
}

