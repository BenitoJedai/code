using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TestDictionaryKeys
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();
        static readonly TextBox t = new TextBox();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            t.AttachTo(this);

            var Gray = Colors.Gray;

            var x = new Dictionary<Color, Image>
			{
				{ Gray, null }
			};




            var xe = new __Enumerator<Color, Image>(x);

            while (xe.MoveNext())
            {
                var item = xe.Current;
                t.Text += new { item.Key }.ToString();

            }

            //foreach (var v in x)

            //while (xe.MoveNext())
            //{
            //    var v = xe.Current;

            //    // wtf?
            //    var Key = v.Key;
            //    t.Text += new { Key }.ToString();

            //}

            //            Implementation not found for type import :
            //type: System.Collections.Generic.Dictionary`2+KeyCollection[[System.Windows.Media.Color, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35],[System.Windows.Controls.Image, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]
            //method: Enumerator GetEnumerator()
            //Did you forget to add the [Script] attribute?


            Test(Gray, x);
            t.Text += "|";

            TestGeneric(Gray, x);
        }

        private void Test(Color Gray, Dictionary<Color, Image> x)
        {
            var e = ((IEnumerable<Color>)(object)x.Keys).GetEnumerator();

            if (e.MoveNext())
            {
                var XKey = e.Current;

                t.Text += new
                {
                    XKey
                }.ToString();

            }


        }

        //        An unhandled exception of type 'System.TypeLoadException' occurred in jsc.meta.exe

        //Additional information: Method 'get_Current' in type '__Enumerator`2' from assembly 'TestDictionaryKeys.ApplicationSprite, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.

        private void TestGeneric<TKey, TValue>(Color Gray, Dictionary<TKey, TValue> x)
        //where TKey : struct
        {

            var e = ((IEnumerable<TKey>)(object)x.Keys).GetEnumerator();
            //var e = ((IEnumerable<Color>)(object)x.Keys).GetEnumerator();

            if (e.MoveNext())
            {
                var XKey = e.Current;

                //          key1 = enumerator_10.get_Current_100666248();
                //this.t.Text = this.t.Text+ new TestDictionaryKeys_ApplicationSprite__f__AnonymousType_85_0_1_33554436(null).toString();

                new a<TKey>(XKey);

                //Write<TKey>(XKey);

            }



        }

        static void g<TKey>(TKey XKey)
        {
            var x = XKey;
            new a<TKey>(x);
        }

        class a<TKey>
        {
            public a(TKey XKey)
            {
                t.Text += new
                {

                    XKey
                }.ToString();
            }
        }

        private void Write<TKey>(TKey XKey)
        {
            t.Text += new
            {

                XKey
            }.ToString();
        }


        #region __Enumerator
        //public __Dictionary<TKey, TValue>.__Enumerator GetEnumerator()
        //{
        //    return new __Enumerator(this);
        //}

        [Script(Implements = typeof(global::System.Collections.Generic.Dictionary<,>.Enumerator)
            //,IsDebugCode = true
            )]
        public class __Enumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
        {
            public IEnumerator<KeyValuePair<TKey, TValue>> list;

            public __Enumerator() : this(null) { }

            public __Enumerator(Dictionary<TKey, TValue> e)
            {
                if (e == null)
                    return;

                var a = new global::System.Collections.Generic.List<KeyValuePair<TKey, TValue>>();

                var Keys = (ICollection<TKey>)e.Keys;
                foreach (var Key in Keys)
                {
                    // Tested by X:\jsc.svn\examples\actionscript\Test\TestDictionaryKeys\TestDictionaryKeys\ApplicationCanvas.cs

                    var kv = new KeyValuePair<TKey, TValue>(Key, e[Key]);

                    a.Add(kv);
                }


                this.list = a.GetEnumerator();

            }



            public KeyValuePair<TKey, TValue> Current { get { return list.Current; } }

            public void Dispose()
            {
                list.Dispose();
            }

            public bool MoveNext()
            {
                return list.MoveNext();
            }



            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Reset()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            #endregion
        }
        #endregion
    }
}
