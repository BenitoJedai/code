using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;


namespace AlphaTest.ActionScript
{
    namespace n1
    {
        [Script]
        public class MyClass : IEnumerable<string>
        {
            public MyClass(): this("DefaultKey")
            {

            }

            public MyClass(string key) : this(key, "DefaultValue")
            {

            }

            public MyClass(string key, string value)
            {

            }


            public void Add(string v)
            {

            }

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                throw new System.NotImplementedException("ctor");
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new System.NotImplementedException("ctor");
            }

            #endregion
        }

    }

    namespace n2
    {
        [Script]
        public class MyClass : n1.MyClass, IDisposable
        {
            public MyClass() : this("DefaultValue2")
            {

            }

            public MyClass(string value2)
                : base("DefaultKey2", value2)
            { 
            }

           
            #region IDisposable Members

            public void Dispose()
            {
                
            }

            #endregion
        }
    }

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class AlphaTest : Sprite
    {
        public AlphaTest()
        {
            var c1 = new n1.MyClass ("key1")
            {
                "hey",
                "ho"
            };

            var c2 = new n2.MyClass 
            {
                "hey",
                "ho"
            };



            for (var j = 0.0; j < 1; j += 0.1)
            {
                this.graphics.beginFill(0xff0000, j);
                this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
                this.graphics.endFill();
            }


            var step = 100;
            for (int i = 0; i < 4; i++)
            {
                addChild(
                   new TextField
                   {
                       text = "hello world",
                       x = step * i,
                       y = 20,
                       textColor = 0x00ff00,
                       sharpness = 400
                   }
               );
            }



            addChild(
                    new TextField
                    {
                        text = "powered by jsc",
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff
                    }
                );


        }
    }

}
