﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
 
   [Script(Implements = typeof(global::System.Windows.Forms.Padding))]
    internal class __Padding
    {
    
    
        public static readonly Padding Empty;

        public __Padding(int all)
        {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }

        public __Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
        
        
        public int Bottom { get; set; }

        public int Horizontal { get; private set; }        

        public int Left { get; set; }
       
        public int Right { get; set; }
       
        public int Top { get; set; }

        public override string ToString()
        {
            return "" + "L=" + Left + " T=" + Top + " R=" + Right + " B=" + Bottom;

        }
    }
}
