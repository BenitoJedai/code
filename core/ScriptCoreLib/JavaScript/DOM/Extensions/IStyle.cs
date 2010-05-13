using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;

namespace ScriptCoreLib.JavaScript.DOM
{

	[Script]
	public static class IStyleExtensions
	{
		// 2009.09.23:
		// yay. just when C# 4 is almost out of beta we
		// actually get to use C# 3 features arond here! :)

		/// <summary>
		/// Sets the MatrixTransform on this element. IE is not supported at this time
		/// due to bad its bad implementation.
		/// </summary>
		/// <param name="s"></param>

		/// <param name="Matrix">M11, M12, M21, M22, Dx, Dy</param>
		public static void SetMatrixTransform(this IStyle s, params double[] Matrix)
		{
			// see: https://developer.mozilla.org/en/CSS/-moz-transform
			// see: http://www.w3.org/TR/SVG11/coords.html#TransformMatrixDefined

			// filter:progid:DXImageTransform.Microsoft.Matrix(M11='1.0', sizingmethod='auto expand');
			//    -moz-transform: matrix(1, 0, 0.6, 1, 15em, 0);
			// -webkit-transform: matrix(1, 0, 0.6, 1,  250, 0);

			// style.MozTransform = “rotate(45deg)”;
			// http://mozillalinks.org/wp/2008/09/firefox-31-gets-cool-web-page-transformations-support/

			// http://www.zachstronaut.com/lab/isocube.html
			// http://paulbakaus.com/tag/internet-explorer/

			var code = @"
q.MozTransformOrigin = ""0 0"";
q.MozTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + ""px,"" + m[5] + ""px)"";

q.WebkitTransformOrigin = ""0 0"";
q.WebkitTransform = ""matrix("" + m[0] + "","" + m[1] + "","" + m[2] + "","" + m[3] + "","" + m[4] + "","" + m[5] + "")"";
				";

			new IFunction("q", "m", code).apply(null, s, Matrix);
		}
	}

    public partial class IStyle
    {
	
        const string px = "px";


        [Script(DefineAsStatic = true)]
        public void Apply(EventHandler<IStyle> e)
        {
            e(this);
        }

        [Script(DefineAsStatic = true)]
        public void ToCenter(IHTMLElement target, int width, int height)
        {
            this.position = IStyle.PositionEnum.absolute;

            this.SetLocation(
                (target.clientWidth - width) / 2,
                (target.clientHeight - height) / 2,
                width, height);
        }



        [Script(DefineAsStatic = true)]
        public void SetLocation(int left, int top)
        {
            this.position = IStyle.PositionEnum.absolute;

            this.left = (left + "") + px;
            this.top = (top + "") + px;


        }
        [Script(DefineAsStatic = true)]
        public void SetLocation(int left, int top, int width, int height)
        {
            SetLocation(left, top);
            SetSize(width, height);
        }

        [Script(DefineAsStatic = true)]
        public void SetLocation(IHTMLElement e, int marginx, int marginy)
        {
            SetLocation(e.offsetLeft - marginx, e.offsetTop - marginy);
            SetSize(e.clientWidth + marginx * 2, e.clientHeight + marginy * 2);
        }

        
        //[Script(DefineAsStatic = true)]
        //public void SetLocation(IStyle e)
        //{
        //    this.left = e.left;
        //    this.top = e.top;
        //    this.width = e.width;
        //    this.height = e.height;
        //}

        //[Script(DefineAsStatic = true)]
        //public void SetLocation(Rectangle r)
        //{
        //    SetLocation(r.Left, r.Top, r.Width, r.Height);
        //}

        [Script(DefineAsStatic = true)]
        public void SetSize(int width, int height)
        {
            this.width = width + px;
            this.height = height + px;
        }

        //[Script(DefineAsStatic = true)]
        //public void SetSize(IHTMLElement.Image e)
        //{
        //    SetSize(e.width, e.height);
        //}

        [Script(DefineAsStatic = true)]
        public void SetSize(IHTMLElement e)
        {
            SetSize(e.clientWidth, e.clientHeight);
        }

        #region Opacity
        [Script(OptimizedCode = @"
            a0.filter = 'Alpha(Opacity=' + (a1 * 100) + ')';
            a0.opacity = a1;
        ")]
        internal static void __opacity_internal(object a0, double a1) { }

        /// <summary>
        /// decimal value from 0 to 1
        /// </summary>
        public double Opacity
        {
            [Script(DefineAsStatic = true)]
            set
            {
                __opacity_internal(this, value);
            }
        }
        #endregion

        #region float


        [Script(IsStringEnum = true)]
        public enum FloatEnum { left, right, none }

        [Script(OptimizedCode = @"
            a0.cssFloat = a1;
            a0.styleFloat = a1;
        ")]
        internal static void __float_internal(object a0, FloatEnum a1) { }

        /// <summary>
        /// <see>http://www.w3.org/TR/REC-CSS1#float</see>
        /// </summary>
        public FloatEnum Float
        {
            [Script(DefineAsStatic = true)]
            set
            {
                __float_internal(this, value);
            }
        }

        #endregion


        [Script(DefineAsStatic=true)]
        public void SetLocation(Rectangle size)
        {
            SetLocation(size.Left, size.Top, size.Width, size.Height);
        }

		[System.Obsolete("To be moved to ScriptCoreLib.Ultra assembly as an extension method")]
		[Script(DefineAsStatic = true)]
        public void SetBackground(string src, bool repeat)
        {
            this.backgroundImage = "url(" + src + ")";

            if (repeat)
                this.backgroundRepeat = "";
            else
                this.backgroundRepeat = "no-repeat";
        }
    }
}
