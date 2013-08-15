﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebGLCity.Components
{
    [System.ComponentModel.DesignerCategory("Code")]
    [Obsolete("Shall this be autogenerated from IDL, how to autoregister to toolbox?")]
    public class XTHREEWebGLRenderer : Component
    {
        public readonly THREE.WebGLRenderer renderer;

        public XTHREEWebGLRenderer()
        {
            try
            {
                renderer = new THREE.WebGLRenderer(
                  new { antialias = false, alpha = false }
               );
            }
            catch
            {
                // running in CLR/Designer?
            }

        }

        System.Drawing.Color InternalClearColor;

        public System.Drawing.Color ClearColor
        {
            get { return InternalClearColor; }
            set
            {
                InternalClearColor = value;

                if (renderer != null)
                    renderer.setClearColor(new THREE.Color(value.ToArgb()));
            }
        }
    }
}
