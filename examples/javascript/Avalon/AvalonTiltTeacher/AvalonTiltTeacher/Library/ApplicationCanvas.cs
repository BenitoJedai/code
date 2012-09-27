using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using ScriptCoreLib.Shared.Lambda;

namespace TiltTeacher
{
    public class ApplicationCanvas : Canvas
    {
        const string Vocabulary = @"
essential: oluline asi, tegur
contemporary: kaasaegne
activity: tegevus
goods: tarbekaubad
provide: varustama, pakkuma
factory: vabrik
produce: tootma
amount: hulk
particular: eriline, just see
opinion: arvamus
attitude: suhtumine
employment: teenistus, t��, t��h�ive
affair: asjatalitus, toimetus, tegu, seik, sekeldus
event: s�ndmus
matter: asi, k�simus, asjaolu, t�htsus, olulisus
occupation: tegevusala, elukutse, amet
concern: hool, mure, osav�tt; ettev�te, firma
in conclusion: kokkuv�ttes
similar: sarnane, taoline
turn into smth: muutuma millekski
commodity: kaubaartikkel
relations: sidemed, suhted
monetary value: rahaline v��rtus
overvalue: �le hindama
care about: hoolima
to a certain extent: teatud m��ral
in connection with: seotud millegagi
earn: teenima
profitable: tulutoov, tulus
profession: elukutse
business card: nimekaart, visiitkaart
business hours: t��aeg, lahtiolekuaeg
business park: �rikeskus, �rirajoon
business associate: �ripartner
business circle: �riringkond
business concerns: �rihuvid
business contacts/connections: �risidemed, �rikontaktid
business correspondence: �rikirjavahetus
business depression: majanduskriis
business enterprise: �riettev�te
business entertainment: �rivastuv�tt
business environment: �rikeskkond
business law: �riseadus
business management: �rijuhtimine
business operation, transaction: �ritehing
business recovery: �ritegevuse elavnemine
business relations: �risuhted
business representative: �riesindaja, kaubandusesindaja
business suit: ametir�ivastus
business tax: ettev�tlusmaks
be in business: tegutsema
mind your own business: �ra sega end teiste asjadesse
make it your business to do sth: midagi enese peale v�tma
i'm sick of the whole business: mul on k�rini sellest k�igest
i sent him about his business: saatsin ta.. 
that's my business: see on minu asi
the business of the day: p�evakord
this is no business of mine: see ei l�he mulle korda
to go out of business: �ritegevust l�petama
to mean business: asja t�siselt v�tma
";

        public ApplicationCanvas()
        {
            {
                var r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);
                r.MoveTo(0, 0);
                r.Opacity = 0.9;
                this.SizeChanged += (s, e) => r.SizeTo(this.Width, this.Height / 2);
            }

            {
                var r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);

                this.SizeChanged += (s, e) => r.MoveTo(0, this.Height / 2).SizeTo(this.Width, this.Height / 2);
            }

            var VocabularyLines = Vocabulary.Trim().Split('\n');

            var v = VocabularyLines.Select(
                k =>
                {
                    var verbs = k.Split(':');

                    return new { A = verbs[0].Trim(), B = verbs[1].Trim() };
                }
            ).Randomize().AsCyclicEnumerator();


            var az = 0.5;
            var ax = 0.0;

            var ABCanvas = new Canvas().AttachTo(this);

            var A = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                TextAlignment = System.Windows.TextAlignment.Center,
                Foreground = Brushes.White,
                Text = "suur ettev�te",
                IsReadOnly = true,
                FontSize = 70
            };

            A.AttachTo(ABCanvas);

            var B = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                TextAlignment = System.Windows.TextAlignment.Center,
                Foreground = Brushes.White,
                Text = "large-scale enterprise",
                IsReadOnly = true,
                FontSize = 70
            };

            var MoveNextDisabled = false;
            Action MoveNext = delegate
            {
                if (MoveNextDisabled)
                    return;

                MoveNextDisabled = true;
                v.MoveNext();
                A.Text = v.Current.A;
                B.Text = v.Current.B;

                600.AtDelay(() => MoveNextDisabled = false);
            };

            MoveNext();

            B.AttachTo(ABCanvas);
            B.MoveTo(0, 64);

            Action Update =
                delegate
                {
          

                    if (Math.Abs(ax) > 0.4)
                        MoveNext();

                    az = Math.Min(1, az);
                    az = Math.Max(0, az);

                    var max = this.Height / 6;
                    var min = this.Height / 3;

                    // az = 1 is 0 degrees
                    // az = 0 is 90 degrees

                    az = 1 - az;

                    az -= 0.05;
                    az *= 10;

                    az = 1 - az;

                    az = Math.Min(1, az);
                    az = Math.Max(0, az);

                    //Console.WriteLine(new { az });

                    A.Opacity = Math.Pow(az, 10);
                    var bz = 1 - az;

                    B.Opacity = Math.Pow(bz, 10);

                    A.MoveTo(0, min + az * max - 16).SizeTo(this.Width, 100);
                    B.MoveTo(0, min + (1 - az) * max - 16).SizeTo(this.Width, 100);

                    ABCanvas.MoveTo(this.Width * ax * 0.1, 0);

                };

            this.SizeChanged +=
                delegate
                {
                    Update();
                };

            this.MouseMove += (sender, args) =>
            {
                var p = args.GetPosition(this);

                az = (p.Y / this.Height);
                ax = -1 * ((p.X / this.Width) - 0.5);

                Update();
            };

            this.TouchMove += (sender, args) =>
            {
                var p = args.GetTouchPoint(this).Position;
                az = (p.Y / this.Height);
                ax = -1 * ((p.X / this.Width) - 0.5);

                Update();
            };


            this.Accelerate = (x, y, z) =>
            {
                az = z;
                ax = x;
                Update();
            };
        }

        public readonly Action<double, double, double> Accelerate;

    }
}
