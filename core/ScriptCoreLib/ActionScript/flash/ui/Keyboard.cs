using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.ui
{
    // http://livedocs.adobe.com/flex/3/langref/flash/ui/Keyboard.html
    [Script(IsNative=true)]
    public static class Keyboard
    {
        #region Properties
        /// <summary>
        /// [static] [read-only] Specifies whether the Caps Lock key is activated (true) or not (false).
        /// </summary>
        public static bool capsLock { get; private set; }

        /// <summary>
        /// [static] [read-only] Specifies whether the Num Lock key is activated (true) or not (false).
        /// </summary>
        public static bool numLock { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// [static] Constant associated with the key code value for the A key (65).
        /// </summary>
        public static readonly uint A = 65;

        /// <summary>
        /// [static] Constant associated with the key code value for the Alternate (Option) key (18).
        /// </summary>
        public static readonly uint ALTERNATE = 18;

        /// <summary>
        /// [static] Constant associated with the key code value for the B key (66).
        /// </summary>
        public static readonly uint B = 66;

        /// <summary>
        /// [static] Constant associated with the key code value for the ` key (192).
        /// </summary>
        public static readonly uint BACKQUOTE = 192;

        /// <summary>
        /// [static] Constant associated with the key code value for the \ key (220).
        /// </summary>
        public static readonly uint BACKSLASH = 220;

        /// <summary>
        /// [static] Constant associated with the key code value for the Backspace key (8).
        /// </summary>
        public static readonly uint BACKSPACE = 8;

        /// <summary>
        /// [static] Constant associated with the key code value for the C key (67).
        /// </summary>
        public static readonly uint C = 67;

        /// <summary>
        /// [static] Constant associated with the key code value for the Caps Lock key (20).
        /// </summary>
        public static readonly uint CAPS_LOCK = 20;

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly Array CharCodeStrings;

        /// <summary>
        /// [static] Constant associated with the key code value for the , key (188).
        /// </summary>
        public static readonly uint COMMA = 188;

        /// <summary>
        /// [static] Constant associated with the Mac command key (15).
        /// </summary>
        public static readonly uint COMMAND = 15;

        /// <summary>
        /// [static] Constant associated with the key code value for the Control key (17).
        /// </summary>
        public static readonly uint CONTROL = 17;

        /// <summary>
        /// [static] Constant associated with the key code value for the D key (68).
        /// </summary>
        public static readonly uint D = 68;

        /// <summary>
        /// [static] Constant associated with the key code value for the Delete key (46).
        /// </summary>
        public static readonly uint DELETE = 46;

        /// <summary>
        /// [static] Constant associated with the key code value for the Down Arrow key (40).
        /// </summary>
        public static readonly uint DOWN = 40;

        /// <summary>
        /// [static] Constant associated with the key code value for the E key (69).
        /// </summary>
        public static readonly uint E = 69;

        /// <summary>
        /// [static] Constant associated with the key code value for the End key (35).
        /// </summary>
        public static readonly uint END = 35;

        /// <summary>
        /// [static] Constant associated with the key code value for the Enter key (13).
        /// </summary>
        public static readonly uint ENTER = 13;

        /// <summary>
        /// [static] Constant associated with the key code value for the = key (187).
        /// </summary>
        public static readonly uint EQUAL = 187;

        /// <summary>
        /// [static] Constant associated with the key code value for the Escape key (27).
        /// </summary>
        public static readonly uint ESCAPE = 27;

        /// <summary>
        /// [static] Constant associated with the key code value for the F key (70).
        /// </summary>
        public static readonly uint F = 70;

        /// <summary>
        /// [static] Constant associated with the key code value for the F1 key (112).
        /// </summary>
        public static readonly uint F1 = 112;

        /// <summary>
        /// [static] Constant associated with the key code value for the F10 key (121).
        /// </summary>
        public static readonly uint F10 = 121;

        /// <summary>
        /// [static] Constant associated with the key code value for the F11 key (122).
        /// </summary>
        public static readonly uint F11 = 122;

        /// <summary>
        /// [static] Constant associated with the key code value for the F12 key (123).
        /// </summary>
        public static readonly uint F12 = 123;

        /// <summary>
        /// [static] Constant associated with the key code value for the F13 key (124).
        /// </summary>
        public static readonly uint F13 = 124;

        /// <summary>
        /// [static] Constant associated with the key code value for the F14 key (125).
        /// </summary>
        public static readonly uint F14 = 125;

        /// <summary>
        /// [static] Constant associated with the key code value for the F15 key (126).
        /// </summary>
        public static readonly uint F15 = 126;

        /// <summary>
        /// [static] Constant associated with the key code value for the F2 key (113).
        /// </summary>
        public static readonly uint F2 = 113;

        /// <summary>
        /// [static] Constant associated with the key code value for the F3 key (114).
        /// </summary>
        public static readonly uint F3 = 114;

        /// <summary>
        /// [static] Constant associated with the key code value for the F4 key (115).
        /// </summary>
        public static readonly uint F4 = 115;

        /// <summary>
        /// [static] Constant associated with the key code value for the F5 key (116).
        /// </summary>
        public static readonly uint F5 = 116;

        /// <summary>
        /// [static] Constant associated with the key code value for the F6 key (117).
        /// </summary>
        public static readonly uint F6 = 117;

        /// <summary>
        /// [static] Constant associated with the key code value for the F7 key (118).
        /// </summary>
        public static readonly uint F7 = 118;

        /// <summary>
        /// [static] Constant associated with the key code value for the F8 key (119).
        /// </summary>
        public static readonly uint F8 = 119;

        /// <summary>
        /// [static] Constant associated with the key code value for the F9 key (120).
        /// </summary>
        public static readonly uint F9 = 120;

        /// <summary>
        /// [static] Constant associated with the key code value for the G key (71).
        /// </summary>
        public static readonly uint G = 71;

        /// <summary>
        /// [static] Constant associated with the key code value for the H key (72).
        /// </summary>
        public static readonly uint H = 72;

        /// <summary>
        /// [static] Constant associated with the key code value for the Home key (36).
        /// </summary>
        public static readonly uint HOME = 36;

        /// <summary>
        /// [static] Constant associated with the key code value for the I key (73).
        /// </summary>
        public static readonly uint I = 73;

        /// <summary>
        /// [static] Constant associated with the key code value for the Insert key (45).
        /// </summary>
        public static readonly uint INSERT = 45;

        /// <summary>
        /// [static] Constant associated with the key code value for the J key (74).
        /// </summary>
        public static readonly uint J = 74;

        /// <summary>
        /// [static] Constant associated with the key code value for the K key (75).
        /// </summary>
        public static readonly uint K = 75;

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_BEGIN = "Begin";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_BREAK = "Break";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_CLEARDISPLAY = "ClrDsp";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_CLEARLINE = "ClrLn";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_DELETE = "Delete";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_DELETECHAR = "DelChr";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_DELETELINE = "DelLn";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_DOWNARROW = "Down";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_END = "End";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_EXECUTE = "Exec";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F1 = "F1";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F10 = "F10";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F11 = "F11";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F12 = "F12";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F13 = "F13";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F14 = "F14";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F15 = "F15";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F16 = "F16";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F17 = "F17";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F18 = "F18";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F19 = "F19";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F2 = "F2";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F20 = "F20";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F21 = "F21";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F22 = "F22";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F23 = "F23";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F24 = "F24";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F25 = "F25";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F26 = "F26";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F27 = "F27";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F28 = "F28";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F29 = "F29";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F3 = "F3";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F30 = "F30";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F31 = "F31";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F32 = "F32";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F33 = "F33";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F34 = "F34";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F35 = "F35";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F4 = "F4";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F5 = "F5";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F6 = "F6";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F7 = "F7";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F8 = "F8";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_F9 = "F9";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_FIND = "Find";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_HELP = "Help";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_HOME = "Home";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_INSERT = "Insert";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_INSERTCHAR = "InsChr";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_INSERTLINE = "InsLn";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_LEFTARROW = "Left";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_MENU = "Menu";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_MODESWITCH = "ModeSw";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_NEXT = "Next";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PAGEDOWN = "PgDn";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PAGEUP = "PgUp";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PAUSE = "Pause";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PREV = "Prev";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PRINT = "Print";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_PRINTSCREEN = "PrntScrn";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_REDO = "Redo";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_RESET = "Reset";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_RIGHTARROW = "Right";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_SCROLLLOCK = "ScrlLck";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_SELECT = "Select";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_STOP = "Stop";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_SYSREQ = "SysReq";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_SYSTEM = "Sys";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_UNDO = "Undo";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_UPARROW = "Up";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string KEYNAME_USER = "User";

        /// <summary>
        /// [static] Constant associated with the key code value for the L key (76).
        /// </summary>
        public static readonly uint L = 76;

        /// <summary>
        /// [static] Constant associated with the key code value for the Left Arrow key (37).
        /// </summary>
        public static readonly uint LEFT = 37;

        /// <summary>
        /// [static] Constant associated with the key code value for the [ key (219).
        /// </summary>
        public static readonly uint LEFTBRACKET = 219;

        /// <summary>
        /// [static] Constant associated with the key code value for the M key (77).
        /// </summary>
        public static readonly uint M = 77;

        /// <summary>
        /// [static] Constant associated with the key code value for the - key (189).
        /// </summary>
        public static readonly uint MINUS = 189;

        /// <summary>
        /// [static] Constant associated with the key code value for the N key (78).
        /// </summary>
        public static readonly uint N = 78;

        /// <summary>
        /// [static] Constant associated with the key code value for the 0 key (48).
        /// </summary>
        public static readonly uint NUMBER_0 = 48;

        /// <summary>
        /// [static] Constant associated with the key code value for the 1 key (49).
        /// </summary>
        public static readonly uint NUMBER_1 = 49;

        /// <summary>
        /// [static] Constant associated with the key code value for the 2 key (50).
        /// </summary>
        public static readonly uint NUMBER_2 = 50;

        /// <summary>
        /// [static] Constant associated with the key code value for the 3 key (51).
        /// </summary>
        public static readonly uint NUMBER_3 = 51;

        /// <summary>
        /// [static] Constant associated with the key code value for the 4 key (52).
        /// </summary>
        public static readonly uint NUMBER_4 = 52;

        /// <summary>
        /// [static] Constant associated with the key code value for the 5 key (53).
        /// </summary>
        public static readonly uint NUMBER_5 = 53;

        /// <summary>
        /// [static] Constant associated with the key code value for the 6 key (54).
        /// </summary>
        public static readonly uint NUMBER_6 = 54;

        /// <summary>
        /// [static] Constant associated with the key code value for the 7 key (55).
        /// </summary>
        public static readonly uint NUMBER_7 = 55;

        /// <summary>
        /// [static] Constant associated with the key code value for the 8 key (56).
        /// </summary>
        public static readonly uint NUMBER_8 = 56;

        /// <summary>
        /// [static] Constant associated with the key code value for the 9 key (57).
        /// </summary>
        public static readonly uint NUMBER_9 = 57;

        /// <summary>
        /// [static] Constant associated with the pseudo-key code for the the number pad (21).
        /// </summary>
        public static readonly uint NUMPAD = 21;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 0 key on the number pad (96).
        /// </summary>
        public static readonly uint NUMPAD_0 = 96;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 1 key on the number pad (97).
        /// </summary>
        public static readonly uint NUMPAD_1 = 97;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 2 key on the number pad (98).
        /// </summary>
        public static readonly uint NUMPAD_2 = 98;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 3 key on the number pad (99).
        /// </summary>
        public static readonly uint NUMPAD_3 = 99;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 4 key on the number pad (100).
        /// </summary>
        public static readonly uint NUMPAD_4 = 100;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 5 key on the number pad (101).
        /// </summary>
        public static readonly uint NUMPAD_5 = 101;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 6 key on the number pad (102).
        /// </summary>
        public static readonly uint NUMPAD_6 = 102;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 7 key on the number pad (103).
        /// </summary>
        public static readonly uint NUMPAD_7 = 103;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 8 key on the number pad (104).
        /// </summary>
        public static readonly uint NUMPAD_8 = 104;

        /// <summary>
        /// [static] Constant associated with the key code value for the number 9 key on the number pad (105).
        /// </summary>
        public static readonly uint NUMPAD_9 = 105;

        /// <summary>
        /// [static] Constant associated with the key code value for the addition key on the number pad (107).
        /// </summary>
        public static readonly uint NUMPAD_ADD = 107;

        /// <summary>
        /// [static] Constant associated with the key code value for the decimal key on the number pad (110).
        /// </summary>
        public static readonly uint NUMPAD_DECIMAL = 110;

        /// <summary>
        /// [static] Constant associated with the key code value for the division key on the number pad (111).
        /// </summary>
        public static readonly uint NUMPAD_DIVIDE = 111;

        /// <summary>
        /// [static] Constant associated with the key code value for the Enter key on the number pad (108).
        /// </summary>
        public static readonly uint NUMPAD_ENTER = 108;

        /// <summary>
        /// [static] Constant associated with the key code value for the multiplication key on the number pad (106).
        /// </summary>
        public static readonly uint NUMPAD_MULTIPLY = 106;

        /// <summary>
        /// [static] Constant associated with the key code value for the subtraction key on the number pad (109).
        /// </summary>
        public static readonly uint NUMPAD_SUBTRACT = 109;

        /// <summary>
        /// [static] Constant associated with the key code value for the O key (79).
        /// </summary>
        public static readonly uint O = 79;

        /// <summary>
        /// [static] Constant associated with the key code value for the P key (80).
        /// </summary>
        public static readonly uint P = 80;

        /// <summary>
        /// [static] Constant associated with the key code value for the Page Down key (34).
        /// </summary>
        public static readonly uint PAGE_DOWN = 34;

        /// <summary>
        /// [static] Constant associated with the key code value for the Page Up key (33).
        /// </summary>
        public static readonly uint PAGE_UP = 33;

        /// <summary>
        /// [static] Constant associated with the key code value for the .
        /// </summary>
        public static readonly uint PERIOD = 190;

        /// <summary>
        /// [static] Constant associated with the key code value for the Q key (81).
        /// </summary>
        public static readonly uint Q = 81;

        /// <summary>
        /// [static] Constant associated with the key code value for the ' key (222).
        /// </summary>
        public static readonly uint QUOTE = 222;

        /// <summary>
        /// [static] Constant associated with the key code value for the R key (82).
        /// </summary>
        public static readonly uint R = 82;

        /// <summary>
        /// [static] Constant associated with the key code value for the Right Arrow key (39).
        /// </summary>
        public static readonly uint RIGHT = 39;

        /// <summary>
        /// [static] Constant associated with the key code value for the ] key (221).
        /// </summary>
        public static readonly uint RIGHTBRACKET = 221;

        /// <summary>
        /// [static] Constant associated with the key code value for the S key (83).
        /// </summary>
        public static readonly uint S = 83;

        /// <summary>
        /// [static] Constant associated with the key code value for the ; key (186).
        /// </summary>
        public static readonly uint SEMICOLON = 186;

        /// <summary>
        /// [static] Constant associated with the key code value for the Shift key (16).
        /// </summary>
        public static readonly uint SHIFT = 16;

        /// <summary>
        /// [static] Constant associated with the key code value for the / key (191).
        /// </summary>
        public static readonly uint SLASH = 191;

        /// <summary>
        /// [static] Constant associated with the key code value for the Spacebar (32).
        /// </summary>
        public static readonly uint SPACE = 32;

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_BEGIN = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_BREAK = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_CLEARDISPLAY = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_CLEARLINE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_DELETE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_DELETECHAR = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_DELETELINE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_DOWNARROW = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_END = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_EXECUTE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F1 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F10 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F11 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F12 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F13 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F14 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F15 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F16 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F17 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F18 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F19 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F2 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F20 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F21 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F22 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F23 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F24 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F25 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F26 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F27 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F28 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F29 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F3 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F30 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F31 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F32 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F33 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F34 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F35 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F4 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F5 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F6 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F7 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F8 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_F9 = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_FIND = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_HELP = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_HOME = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_INSERT = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_INSERTCHAR = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_INSERTLINE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_LEFTARROW = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_MENU = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_MODESWITCH = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_NEXT = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PAGEDOWN = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PAGEUP = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PAUSE = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PREV = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PRINT = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_PRINTSCREEN = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_REDO = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_RESET = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_RIGHTARROW = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_SCROLLLOCK = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_SELECT = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_STOP = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_SYSREQ = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_SYSTEM = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_UNDO = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_UPARROW = "?";

        /// <summary>
        /// [static]
        /// </summary>
        public static readonly string STRING_USER = "?";

        /// <summary>
        /// [static] Constant associated with the key code value for the T key (84).
        /// </summary>
        public static readonly uint T = 84;

        /// <summary>
        /// [static] Constant associated with the key code value for the Tab key (9).
        /// </summary>
        public static readonly uint TAB = 9;

        /// <summary>
        /// [static] Constant associated with the key code value for the U key (85).
        /// </summary>
        public static readonly uint U = 85;

        /// <summary>
        /// [static] Constant associated with the key code value for the Up Arrow key (38).
        /// </summary>
        public static readonly uint UP = 38;

        /// <summary>
        /// [static] Constant associated with the key code value for the V key (86).
        /// </summary>
        public static readonly uint V = 86;

        /// <summary>
        /// [static] Constant associated with the key code value for the W key (87).
        /// </summary>
        public static readonly uint W = 87;

        /// <summary>
        /// [static] Constant associated with the key code value for the X key (88).
        /// </summary>
        public static readonly uint X = 88;

        /// <summary>
        /// [static] Constant associated with the key code value for the Y key (89).
        /// </summary>
        public static readonly uint Y = 89;

        /// <summary>
        /// [static] Constant associated with the key code value for the Z key (90).
        /// </summary>
        public static readonly uint Z = 90;

        #endregion

    }
}
