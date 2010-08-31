using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MultitouchFingerTools.Library
{
    public class DoubleVector2
    {
        public double X;
        public double Y;

        public static implicit operator DoubleVector2(XElement e)
        {
            var n = new DoubleVector2
            {
                X = double.Parse(e.Element("X").Value),
                Y = double.Parse(e.Element("Y").Value)
            };


            return n;
        }

        public static implicit operator XElement(DoubleVector2 n)
        {
            var e = new XElement("DoubleVector2",
                new XElement("X", "" + n.X),
                new XElement("Y", "" + n.Y)
            );

            return e;
        }

    }


}
