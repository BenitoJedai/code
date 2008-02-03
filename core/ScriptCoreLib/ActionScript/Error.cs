﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/Error.html
    [Script(IsNative = true)]
    public class Error
    {
        public Error()
        {

        }

        public Error(string message)
        {

        }
        
        /// <summary>
        /// Contains the message associated with the Error object.
        /// </summary>
        public string message { get; set; }


    }
}
