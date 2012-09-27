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
employment: teenistus, töö, tööhõive
affair: asjatalitus, toimetus, tegu, seik, sekeldus
event: sündmus
matter: asi, küsimus, asjaolu, tähtsus, olulisus
occupation: tegevusala, elukutse, amet
concern: hool, mure, osavõtt; ettevõte, firma
in conclusion: kokkuvõttes
similar: sarnane, taoline
turn into smth: muutuma millekski
commodity: kaubaartikkel
relations: sidemed, suhted
monetary value: rahaline väärtus
overvalue: üle hindama
care about: hoolima
to a certain extent: teatud määral
in connection with: seotud millegagi
earn: teenima
profitable: tulutoov, tulus
profession: elukutse
business card: nimekaart, visiitkaart
business hours: tööaeg, lahtiolekuaeg
business park: ärikeskus, ärirajoon
business associate: äripartner
business circle: äriringkond
business concerns: ärihuvid
business contacts/connections: ärisidemed, ärikontaktid
business correspondence: ärikirjavahetus
business depression: majanduskriis
business enterprise: äriettevõte
business entertainment: ärivastuvõtt
business environment: ärikeskkond
business law: äriseadus
business management: ärijuhtimine
business operation, transaction: äritehing
business recovery: äritegevuse elavnemine
business relations: ärisuhted
business representative: äriesindaja, kaubandusesindaja
business suit: ametirõivastus
business tax: ettevõtlusmaks
be in business: tegutsema
mind your own business: ära sega end teiste asjadesse
make it your business to do sth: midagi enese peale võtma
i'm sick of the whole business: mul on kõrini sellest kõigest
i sent him about his business: saatsin ta.. 
that's my business: see on minu asi
the business of the day: päevakord
this is no business of mine: see ei lähe mulle korda
to go out of business: äritegevust lõpetama
to mean business: asja tõsiselt võtma
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
                Text = "suur ettevõte",
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
