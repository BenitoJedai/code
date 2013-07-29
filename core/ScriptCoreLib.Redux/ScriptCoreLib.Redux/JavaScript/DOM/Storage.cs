﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true)]
    public class Storage
    {
        public readonly uint length;

        public Storage() { }

        public string this[string key] { get { return null; } set { } }

        // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs
        [Script(DefineAsStatic = true)]
        public string this[object key] { get { return this[key.ToString()]; } set { this[key.ToString()] = value; } }

        public void clear() { }
        public string key(uint index) { return null; }
        public void removeItem(string key) { }


        [Script(DefineAsStatic = true)]
        public void removeItem(object key) { removeItem(key.ToString()); }
    }
}
