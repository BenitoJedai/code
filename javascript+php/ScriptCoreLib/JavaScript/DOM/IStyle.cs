using ScriptCoreLib.JavaScript;


namespace ScriptCoreLib.JavaScript.DOM
{


    [Script(HasNoPrototype=true)]
    public partial class IStyle
    {
        
        public string clear;

        #region white-space

        public WhiteSpaceEnum whiteSpace;

        [Script(IsStringEnum=true)]
        public enum WhiteSpaceEnum
        {
            /// <summary>
            /// White-space is ignored by the browser
            /// </summary>
            normal  	,
            /// <summary>
            /// White-space is preserved by the browser. Acts like the <pre> tag in HTML
            /// </summary>

            pre 	,
            /// <summary>
            /// The text will never wrap, it continues on the same line until a <br> tag is encountered
            /// </summary>
            nowrap 	
        }
        #endregion


        #region cursor
        // TODO support per member values in enum
        [Script(IsStringEnum=true)]
        public enum CursorEnum
        {
            @default, auto, crosshair, pointer, move, text, wait, help 

        }

        /// <summary>
        /// <see>http://www.w3schools.com/css/pr_class_cursor.asp</see>
        /// </summary>
        public CursorEnum cursor;
        #endregion

        // http://www.w3schools.com/css/css_background.asp
        public string background;
        public string backgroundImage;
        public string backgroundColor;
        //public string backgroundLeft;
        //public string backgroundTop;
        public string backgroundRepeat;
        public string backgroundPosition;

        #region display

        /// <summary>
        /// http://www.w3schools.com/css/pr_class_display.asp
        /// </summary>
        [Script(IsStringEnum = true)]
        public enum DisplayEnum
        {
            none,
            block,
            inline,
            [Script(ExternalTarget = "")]
            empty

        }
        public DisplayEnum display; 
        #endregion

        #region visibility
        [Script(IsStringEnum = true)]
        public enum VisibilityEnum
        {
            visible,
            hidden,
            collapse
        }
        public VisibilityEnum visibility;
        #endregion


        #region padding
        public string padding;
        public string paddingTop;
        public string paddingBottom;
        public string paddingLeft;
        public string paddingRight;
        #endregion

        #region fontFamily
        [Script(IsStringEnum = true)]
        public enum FontFamilyEnum
        {
            Times, Helvetica, /*Zapf-Chancery,*/ Western, 
            [Script(ExternalTarget="Consolas, Courier New, Courier")]
            Consolas,
            Courier, Verdana, Tahoma, Arial,
            Fixedsys
        }
        public FontFamilyEnum fontFamily;

        //[Script(ExternalTarget = "fontFamily")]
        //public string fontFamilyValue;
        #endregion

        
        public string fontWeight;
        public string fontSize;

        public string margin;
        public string marginLeft;
        public string marginTop;
        public string marginRight;
        public string marginBottom;

        #region border
        public string border;
        public string borderStyle;
        public string borderRight;
        public string borderLeft;
        public string borderTop;
        public string borderBottom;
        public string borderWidth;
        public string borderColor;
        #endregion

        public string color;







        public string right;
        public string bottom;
        public string left;
        public string top;
        public string width;
        public string height;

        #region position
        [Script(IsStringEnum = true)]
        public enum PositionEnum
        {
            absolute,
            relative
        }

        public PositionEnum position;
        #endregion


        #region overflow
        [Script(IsStringEnum=true)]
        public enum OverflowEnum
        {
            visible, hidden, scroll, auto
        }

        public OverflowEnum overflow;

        #endregion

        public string outline;


        #region textAlgin
        [Script(IsStringEnum = true)]
        public enum TextAlignEnum { left, right, center, justify }
        public TextAlignEnum textAlign;
        #endregion

        public string textDecoration;

        public string verticalAlign;

        public int zIndex;

    }
}
