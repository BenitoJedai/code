using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using ScriptCoreLib;
using System.Drawing;
using ScriptCoreLib.Shared;

namespace FormsExample.js
{
    [Script]
    class SimpleDragComponent : Component
    {
        private Control _Caption;

        public Control Caption
        {
            get { return _Caption; }
            set {

                if (_Caption != null)
                {
                    _Caption.MouseDown -= new MouseEventHandler(_Caption_MouseDown);
                    _Caption.MouseMove -= new MouseEventHandler(_Caption_MouseMove);
                    _Caption.MouseUp -= new MouseEventHandler(_Caption_MouseUp);
                    
                }

                _Caption = value;

                if (_Caption != null)
                {
                    _Caption.MouseDown += new MouseEventHandler(_Caption_MouseDown);
                    _Caption.MouseMove += new MouseEventHandler(_Caption_MouseMove);
                    _Caption.MouseUp += new MouseEventHandler(_Caption_MouseUp);
                }
            }
        }

        public event Action DragStart;
        public event Action DragStop;

        void _Caption_MouseUp(object sender, MouseEventArgs e)
        {
            if (_Drag > 0)
            {
                _Drag--;

                if (_Drag == 0)
                {
                    if (DragStop != null)
                        DragStop();
                }
            }
        }

        void _Caption_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Drag > 0)
            {
                Content.Location = new Point(

                            _DragLocation.X + (e.X - _DragStart.X),
                            _DragLocation.Y + (e.Y - _DragStart.Y)

                        );


            }
        }

        void _Caption_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("mousedown");

            if (_Drag == 0)
            {
                _DragLocation = Content.Location;
                _DragStart = e.Location;

                if (DragStart != null)
                    DragStart();

            }

            _Drag++;            
        }

        private Control _Content;

        public Control Content
        {
            get { return _Content; }
            set { _Content = value; }
        }


        Point _DragLocation;
        Point _DragStart;
        int _Drag;
    }
}
