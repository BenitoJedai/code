using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.flash.text
{
    // http://livedocs.adobe.com/flex/3/langref/flash/text/TextField.html
    [Script(IsNative = true)]
    public class TextField : InteractiveObject
    {
        #region Events
        /// <summary>
        /// Dispatched after a control's value is modified.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> change;

        /// <summary>
        /// Dispatched when a user clicks a hyperlink in an HTML-enabled text field, where the URL begins with "event:".
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TextEvent> link;

        /// <summary>
        /// Dispatched by a TextField object after the user scrolls.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> scroll;

        /// <summary>
        /// Flash Player dispatches the textInput event when a user enters one or more characters of text.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<TextEvent> textInput;

        #endregion

        #region Properties
        /// <summary>
        /// When set to true and the text field is not in focus, Flash Player highlights the selection in the text field in gray.
        /// </summary>
        public bool alwaysShowSelection { get; set; }

        /// <summary>
        /// The type of anti-aliasing used for this text field.
        /// </summary>
        public string antiAliasType { get; set; }

        /// <summary>
        /// Controls automatic sizing and alignment of text fields.
        /// </summary>
        public string autoSize { get; set; }

        /// <summary>
        /// Specifies whether the text field has a background fill.
        /// </summary>
        public bool background { get; set; }

        /// <summary>
        /// The color of the text field background.
        /// </summary>
        public uint backgroundColor { get; set; }

        /// <summary>
        /// Specifies whether the text field has a border.
        /// </summary>
        public bool border { get; set; }

        /// <summary>
        /// The color of the text field border.
        /// </summary>
        public uint borderColor { get; set; }

        /// <summary>
        /// [read-only] An integer (1-based index) that indicates the bottommost line that is currently visible in the specified text field.
        /// </summary>
        public int bottomScrollV { get; private set; }

        /// <summary>
        /// [read-only] The index of the insertion point (caret) position.
        /// </summary>
        public int caretIndex { get; private set; }

        /// <summary>
        /// A Boolean value that specifies whether extra white space (spaces, line breaks, and so on) in a text field with HTML text should be removed.
        /// </summary>
        public bool condenseWhite { get; set; }

        /// <summary>
        /// [read-only]
        /// </summary>
        public ContextMenu contextMenu { get; private set; }

        /// <summary>
        /// Specifies the format applied to newly inserted text, such as text inserted with the replaceSelectedText() method or text entered by a user.
        /// </summary>
        public TextFormat defaultTextFormat { get; set; }

        /// <summary>
        /// Specifies whether the text field is a password text field.
        /// </summary>
        public bool displayAsPassword { get; set; }

        /// <summary>
        /// Specifies whether to render by using embedded font outlines.
        /// </summary>
        public bool embedFonts { get; set; }

        /// <summary>
        /// The type of grid fitting used for this text field.
        /// </summary>
        public string gridFitType { get; set; }

        /// <summary>
        /// Contains the HTML representation of the text field's contents.
        /// </summary>
        public string htmlText { get; set; }

        /// <summary>
        /// [read-only] The number of characters in a text field.
        /// </summary>
        public int length { get; private set; }

        /// <summary>
        /// The maximum number of characters that the text field can contain, as entered by a user.
        /// </summary>
        public int maxChars { get; set; }

        /// <summary>
        /// [read-only] The maximum value of scrollH.
        /// </summary>
        public int maxScrollH { get; private set; }

        /// <summary>
        /// [read-only] The maximum value of scrollV.
        /// </summary>
        public int maxScrollV { get; private set; }

        /// <summary>
        /// A Boolean value that indicates whether Flash Player should automatically scroll multiline text fields when the user clicks a text field and rolls the mouse wheel.
        /// </summary>
        public bool mouseWheelEnabled { get; set; }

        /// <summary>
        /// Indicates whether the text field is a multiline text field.
        /// </summary>
        public bool multiline { get; set; }

        /// <summary>
        /// [read-only] Defines the number of text lines in a multiline text field.
        /// </summary>
        public int numLines { get; private set; }

        /// <summary>
        /// Indicates the set of characters that a user can enter into the text field.
        /// </summary>
        public string restrict { get; set; }

        /// <summary>
        /// The current horizontal scrolling position.
        /// </summary>
        public int scrollH { get; set; }

        /// <summary>
        /// The vertical position of text in a text field.
        /// </summary>
        public int scrollV { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the text field is selectable.
        /// </summary>
        public bool selectable { get; set; }

        /// <summary>
        /// [read-only] The zero-based character index value of the first character in the current selection.
        /// </summary>
        public int selectionBeginIndex { get; private set; }

        /// <summary>
        /// [read-only] The zero-based character index value of the last character in the current selection.
        /// </summary>
        public int selectionEndIndex { get; private set; }

        /// <summary>
        /// The sharpness of the glyph edges in this text field.
        /// </summary>
        public double sharpness { get; set; }

        /// <summary>
        /// Attaches a style sheet to the text field.
        /// </summary>
        public StyleSheet styleSheet { get; set; }

        /// <summary>
        /// A string that is the current text in the text field.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// The color of the text in a text field, in hexadecimal format.
        /// </summary>
        public uint textColor { get; set; }

        /// <summary>
        /// [read-only] The height of the text in pixels.
        /// </summary>
        public double textHeight { get; private set; }

        /// <summary>
        /// [read-only] The width of the text in pixels.
        /// </summary>
        public double textWidth { get; private set; }

        /// <summary>
        /// The thickness of the glyph edges in this text field.
        /// </summary>
        public double thickness { get; set; }

        /// <summary>
        /// The type of the text field.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Specifies whether to copy and paste the text formatting along with the text.
        /// </summary>
        public bool useRichTextClipboard { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the text field has word wrap.
        /// </summary>
        public bool wordWrap { get; set; }

        #endregion


        #region Methods
        /// <summary>
        /// Appends the string specified by the newText parameter to the end of the text of the text field.
        /// </summary>
        public void appendText(string newText)
        {
        }

        /// <summary>
        /// Returns a rectangle that is the bounding box of the character.
        /// </summary>
        public Rectangle getCharBoundaries(int charIndex)
        {
            return default(Rectangle);
        }

        /// <summary>
        /// Returns the zero-based index value of the character at the point specified by the x and y parameters.
        /// </summary>
        public int getCharIndexAtPoint(double x, double y)
        {
            return default(int);
        }

        /// <summary>
        /// Given a character index, returns the index of the first character in the same paragraph.
        /// </summary>
        public int getFirstCharInParagraph(int charIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Returns a DisplayObject reference for the given id, for an image or SWF file that has been added to an HTML-formatted text field by using an <img> tag.
        /// </summary>
        public DisplayObject getImageReference(string id)
        {
            return default(DisplayObject);
        }

        /// <summary>
        /// Returns the zero-based index value of the line at the point specified by the x and y parameters.
        /// </summary>
        public int getLineIndexAtPoint(double x, double y)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the zero-based index value of the line containing the character specified by the charIndex parameter.
        /// </summary>
        public int getLineIndexOfChar(int charIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the number of characters in a specific text line.
        /// </summary>
        public int getLineLength(int lineIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Returns metrics information about a given text line.
        /// </summary>
        public TextLineMetrics getLineMetrics(int lineIndex)
        {
            return default(TextLineMetrics);
        }

        /// <summary>
        /// Returns the character index of the first character in the line that the lineIndex parameter specifies.
        /// </summary>
        public int getLineOffset(int lineIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the text of the line specified by the lineIndex parameter.
        /// </summary>
        public string getLineText(int lineIndex)
        {
            return default(string);
        }

        /// <summary>
        /// Given a character index, returns the length of the paragraph containing the given character.
        /// </summary>
        public int getParagraphLength(int charIndex)
        {
            return default(int);
        }

        /// <summary>
        /// Returns a TextFormat object that contains formatting information for the range of text that the beginIndex and endIndex parameters specify.
        /// </summary>
        public TextFormat getTextFormat(int beginIndex, int endIndex)
        {
            return default(TextFormat);
        }

        /// <summary>
        /// Returns a TextFormat object that contains formatting information for the range of text that the beginIndex and endIndex parameters specify.
        /// </summary>
        public TextFormat getTextFormat(int beginIndex)
        {
            return default(TextFormat);
        }

        /// <summary>
        /// Returns a TextFormat object that contains formatting information for the range of text that the beginIndex and endIndex parameters specify.
        /// </summary>
        public TextFormat getTextFormat()
        {
            return default(TextFormat);
        }

        /// <summary>
        /// Replaces the current selection with the contents of the value parameter.
        /// </summary>
        public void replaceSelectedText(string value)
        {
        }

        /// <summary>
        /// Replaces the range of characters that the beginIndex and endIndex parameters specify with the contents of the newText parameter.
        /// </summary>
        public void replaceText(int beginIndex, int endIndex, string newText)
        {
        }

        /// <summary>
        /// Sets as selected the text designated by the index values of the first and last characters, which are specified with the beginIndex and endIndex parameters.
        /// </summary>
        public void setSelection(int beginIndex, int endIndex)
        {
        }

        /// <summary>
        /// Applies the text formatting that the format parameter specifies to the specified text in a text field.
        /// </summary>
        public void setTextFormat(TextFormat format, int beginIndex, int endIndex)
        {
        }

        /// <summary>
        /// Applies the text formatting that the format parameter specifies to the specified text in a text field.
        /// </summary>
        public void setTextFormat(TextFormat format, int beginIndex)
        {
        }

        /// <summary>
        /// Applies the text formatting that the format parameter specifies to the specified text in a text field.
        /// </summary>
        public void setTextFormat(TextFormat format)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new TextField instance.
        /// </summary>
        public TextField()
        {
        }

        #endregion

    }
}
