using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;
using System.Collections.Generic;

namespace MultitouchExample.Components
{
    public sealed class MySprite1 : Sprite
    {
        private readonly Dictionary<int, Sprite> dots = new Dictionary<int, Sprite>();
        private readonly Dictionary<int, TextField> labels = new Dictionary<int, TextField>();
        private TextFormat labelFormat;
        private uint dotCount;
        private TextField dotsLeft;
        private  const uint LABEL_SPACING = 15;

        public MySprite1()
        {
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

            this.stage.touchBegin +=
                e =>
                {
                    if (this.dotCount == Multitouch.maxTouchPoints) return;
			        var dot = this.getCircle();
			        dot.x = e.stageX;
			        dot.y = e.stageY;
			        this.stage.addChild(dot);
			        dot.startTouchDrag(e.touchPointID, true);
			        this.dots[e.touchPointID] = dot;
			
			        ++this.dotCount;

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
			        label.text = (e.stageX + ", " + e.stageY);
                };

            this.stage.touchEnd +=
                e =>
                {
                    var dot = this.dots[e.touchPointID];
			        var label = this.labels[e.touchPointID];
			
			        this.stage.removeChild(dot);
			        this.stage.removeChild(label);
			
			        this.dots.Remove(e.touchPointID);
			        this.labels.Remove(e.touchPointID);
			
			        --this.dotCount;

			        this.updateDotsLeft();
                };
        }

        private Sprite getCircle(uint circumference = 40)
        {
            var circle = new Sprite();
            circle.graphics.beginFill(0x1695A3);
            circle.graphics.drawCircle(0, 0, circumference);
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
