




using System;

namespace MD5
{



    /*******************************************************
     * Programmed by
     *			 syed Faraz mahmood
     *					Student NU FAST ICS
     * can be reached at s_faraz_mahmood@hotmail.com
     * 
     * 
     * 
     * *******************************************************/

    /// <summary>
    /// constants for md5
    /// </summary>
    public enum MD5InitializerConstant : uint
    {
        A = 0x67452301,
        B = 0xEFCDAB89,
        C = 0x98BADCFE,
        D = 0X10325476
    }

    /// <summary>
    /// Represent digest with ABCD
    /// </summary>
    sealed public class Digest
    {
        public uint A;
        public uint B;
        public uint C;
        public uint D;

        public Digest()
        {
            A = (uint)MD5InitializerConstant.A;
            B = (uint)MD5InitializerConstant.B;
            C = (uint)MD5InitializerConstant.C;
            D = (uint)MD5InitializerConstant.D;
        }
        public override string ToString()
        {
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Int32.cs

            string st;
            st = MD5Helper.ReverseByte(A).ToString("X8") +
                MD5Helper.ReverseByte(B).ToString("X8") +
                MD5Helper.ReverseByte(C).ToString("X8") +
                MD5Helper.ReverseByte(D).ToString("X8");
            return st;

        }

    }


    /// <summary>
    /// helper class providing suporting function
    /// </summary>
    sealed public class MD5Helper
    {

        private MD5Helper() { }

        /// <summary>
        /// Left rotates the input word
        /// </summary>
        /// <param name="uiNumber">a value to be rotated</param>
        /// <param name="shift">no of bits to be rotated</param>
        /// <returns>the rotated value</returns>
        public static uint RotateLeft(uint uiNumber, ushort shift)
        {
            // 0:26ms RotateLeft { uiNumber = 3614090487, shift = 7, value = -21 } view-source:36394
            // RotateLeft { uiNumber = 3614090487, shift = 7, value = 3042081771 }


            //      int320 = ((uiNumber >> ((32 - shift) & 31)) | (uiNumber << (shift & 31)));
            //  { uiNumber = 3614090487, shift = 7, value = 4294967275 }
            // RotateLeft { uiNumber = 3614090487, shift = 7, value = 3042081771 }

            var neg_shift = 32 - shift;

            // http://help.adobe.com/en_US/AS2LCR/Flash_10.0/help.html?content=00000638.html
            var value0 = (uiNumber >> neg_shift);
            var value1 = (uiNumber << shift);

            var value = value0 | value1;

            Console.WriteLine(
               "RotateLeft " +
               new { uiNumber, shift, value, neg_shift, value0, value1 }
               );

            return value;
        }

        /// <summary>
        /// perform a ByteReversal on a number
        /// </summary>
        /// <param name="uiNumber">value to be reversed</param>
        /// <returns>reversed value</returns>
        public static uint ReverseByte(uint uiNumber)
        {
            return (((uiNumber & 0x000000ff) << 24) |
                        (uiNumber >> 24) |
                    ((uiNumber & 0x00ff0000) >> 8) |
                    ((uiNumber & 0x0000ff00) << 8));
        }
    }






}