using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib.ActionScript.flash.events;
using MultitouchTransform.Library;
using System;
using MultitouchTransform;

namespace MultitouchExampleWithFingerTools.Components
{
    public sealed class MySprite1 : Sprite
    {
        public const int DefaultWidth = ApplicationCanvas.DefaultWidth;
        public const int DefaultHeight = ApplicationCanvas.DefaultHeight;

        //private readonly Dictionary<int, Sprite> dots = new Dictionary<int, Sprite>();
        private readonly Dictionary<int, TextField> labels = new Dictionary<int, TextField>();
        private TextFormat labelFormat;
        private uint dotCount;
        private TextField dotsLeft;
        private const uint LABEL_SPACING = 15;

        public MySprite1()
        {
            this.stage.scaleMode = StageScaleMode.NO_SCALE;

            var app = new MultitouchTransform.ApplicationCanvas();

            Func<TouchEvent, Movable> ToMovable =
                e =>
                {

                    var k = this.labels.Keys.ToArray();
                    var i = -1;

                    foreach (var item in k)
                    {
                        i++;

                        if (item == e.touchPointID)
                            break;
                    }

                    return app.Movables[i];
                };


            app.AttachToContainer(this);

            this.labelFormat = new TextFormat();
            labelFormat.color = 0xACF0F2;
            labelFormat.font = "Helvetica";
            labelFormat.size = 11;

            this.dotCount = 0;

            this.dotsLeft = new TextField();
            this.dotsLeft.width = 300;
            this.dotsLeft.defaultTextFormat = this.labelFormat;
            this.dotsLeft.x = 3;
            this.dotsLeft.y = 0;
            this.stage.addChild(this.dotsLeft);
            this.updateDotsLeft();


            Multitouch.inputMode = MultitouchInputMode.TOUCH_POINT;

            this.graphics.beginFill(0x7f0000);
            this.graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);




            this.stage.touchBegin +=
                e =>
                {
                    if (this.dotCount == Multitouch.maxTouchPoints)
                        return;

                    //var dot = this.getCircle();
                    //dot.x = e.stageX;
                    //dot.y = e.stageY;
                    //this.stage.addChild(dot);
                    //dot.startTouchDrag(e.touchPointID, true);
                    //this.dots[e.touchPointID] = dot;

                    //++this.dotCount;

                    var label = this.getLabel(e.stageX + ", " + e.stageY);
                    label.x = 3;
                    label.y = this.dotCount * LABEL_SPACING;
                    this.stage.addChild(label);
                    this.labels[e.touchPointID] = label;

                    this.updateDotsLeft();
                };

            this.stage.touchMove +=
                e =>
                {
                    var label = this.labels[e.touchPointID];


                    ToMovable(e).MoveTo(e.stageX, e.stageY);

                    label.text = (e.touchPointID + ": " + e.stageX + ", " + e.stageY);
                };

            this.stage.touchEnd +=
                e =>
                {
                    //var dot = this.dots[e.touchPointID];
                    var label = this.labels[e.touchPointID];

                    //this.stage.removeChild(dot);
                    this.stage.removeChild(label);

                    //this.dots.Remove(e.touchPointID);
                    this.labels.Remove(e.touchPointID);

                    --this.dotCount;

                    this.updateDotsLeft();
                };
        }

        private Sprite getCircle(uint circumference = 40)
        {
            var h = circumference / 2;

            var circle = new Sprite();
            circle.graphics.beginFill(0x1695A3);
            circle.graphics.drawCircle(0, 0, circumference);

            circle.graphics.drawRect(-h, -circumference * 2, circumference, circumference * 2);
            return circle;
        }

        private TextField getLabel(string initialText)
        {
            var label = new TextField();
            label.defaultTextFormat = this.labelFormat;
            label.selectable = false;
            label.antiAliasType = AntiAliasType.ADVANCED;
            label.text = initialText;
            return label;
        }

        private void updateDotsLeft()
        {
            this.dotsLeft.text = "Touches Remaining: " + (Multitouch.maxTouchPoints - this.dotCount);
        }
    }

}
