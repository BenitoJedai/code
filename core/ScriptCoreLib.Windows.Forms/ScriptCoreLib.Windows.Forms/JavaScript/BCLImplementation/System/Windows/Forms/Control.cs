﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal class __Control
    {
        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection
        {
            public __ControlCollection(Control owner)
            {

            }

            public void Add(Control e)
            {
            }
        }

        public void PerformLayout()
        {

        }
        public void SuspendLayout()
        {

        }
        public void ResumeLayout(bool b)
        {

        }

        public __Control()
        {
            this.Controls = new Control.ControlCollection(this);
        }

        public Control.ControlCollection Controls { get; set; }
        public Point Location { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Size Size { get; set; }
        public int TabIndex { get; set; }
        public bool AutoSize { get; set; }
        public Color ForeColor { get; set; }

        #region 
        static public implicit operator Control(__Control e)
        {
            return (Control)(object)e;
        }

        static public implicit operator __Control(Control e)
        {
            return (__Control)(object)e;
        }
        #endregion

    }
}
