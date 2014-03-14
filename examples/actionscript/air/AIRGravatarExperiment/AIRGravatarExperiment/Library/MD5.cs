using System;
using System.Text;


namespace MD5
{



    /*******************************************************
     * Programmed by
     *			 syed Faraz mahmood 
     *				Student NU FAST ICS
     * can be reached at s_faraz_mahmood@hotmail.com
     * 
     * 
     * 
     * *******************************************************/

    /// <summary>
    /// Summary description for MD5.
    /// </summary>
    public class MD5Type
    {
        /***********************VARIABLES************************************/


        /***********************Statics**************************************/
        /// <summary>
        /// lookup table 4294967296*sin(i)
        /// </summary>
        protected readonly static uint[] T = new uint[64] 
			{	0xd76aa478,0xe8c7b756,0x242070db,0xc1bdceee,
				0xf57c0faf,0x4787c62a,0xa8304613,0xfd469501,
                0x698098d8,0x8b44f7af,0xffff5bb1,0x895cd7be,
                0x6b901122,0xfd987193,0xa679438e,0x49b40821,
				0xf61e2562,0xc040b340,0x265e5a51,0xe9b6c7aa,
                0xd62f105d,0x2441453,0xd8a1e681,0xe7d3fbc8,
                0x21e1cde6,0xc33707d6,0xf4d50d87,0x455a14ed,
				0xa9e3e905,0xfcefa3f8,0x676f02d9,0x8d2a4c8a,
                0xfffa3942,0x8771f681,0x6d9d6122,0xfde5380c,
                0xa4beea44,0x4bdecfa9,0xf6bb4b60,0xbebfbc70,
                0x289b7ec6,0xeaa127fa,0xd4ef3085,0x4881d05,
				0xd9d4d039,0xe6db99e5,0x1fa27cf8,0xc4ac5665,
                0xf4292244,0x432aff97,0xab9423a7,0xfc93a039,
                0x655b59c3,0x8f0ccc92,0xffeff47d,0x85845dd1,
                0x6fa87e4f,0xfe2ce6e0,0xa3014314,0x4e0811a1,
				0xf7537e82,0xbd3af235,0x2ad7d2bb,0xeb86d391};

        /*****instance variables**************/
        /// <summary>
        /// X used to proces data in 
        ///	512 bits chunks as 16 32 bit word
        /// </summary>
        protected uint[] X = new uint[16];

        /// <summary>
        /// the finger print obtained. 
        /// </summary>
        protected Digest dgFingerPrint;

        /// <summary>
        /// the input bytes
        /// </summary>
        protected byte[] m_byteInput;



        /**********************EVENTS AND DELEGATES*******************************************/




        /********************************************************************/
        /***********************PROPERTIES ***********************/


        /// <summary>
        /// get/sets as  byte array 
        /// </summary>
        public byte[] ValueAsByte
        {
            get
            {
                byte[] bt = new byte[m_byteInput.Length];
                for (int i = 0; i < m_byteInput.Length; i++)
                    bt[i] = m_byteInput[i];
                return bt;
            }
            set
            {
                /// raise the event to notify the changing

                m_byteInput = new byte[value.Length];
                for (int i = 0; i < value.Length; i++)
                    m_byteInput[i] = value[i];
                dgFingerPrint = CalculateMD5Value();


                /// notify the changed  value
            }
        }

        //gets the signature/figner print as string
        public string FingerPrint
        {
            get
            {
                return dgFingerPrint.ToString();
            }
        }


        /*************************************************************************/
        /// <summary>
        /// Constructor
        /// </summary>
        public MD5Type()
        {
            ValueAsByte = new byte[0];
        }


        /******************************************************************************/
        /*********************METHODS**************************/

        /// <summary>
        /// calculat md5 signature of the string in Input
        /// </summary>
        /// <returns> Digest: the finger print of msg</returns>
        protected Digest CalculateMD5Value()
        {
            /***********vairable declaration**************/
            byte[] bMsg;	//buffer to hold bits
            uint N;			//N is the size of msg as  word (32 bit) 
            Digest dg = new Digest();			//  the value to be returned

            // jsc can handle local byrefs for now
            var dg_A = dg.A;
            var dg_B = dg.B;
            var dg_C = dg.C;
            var dg_D = dg.D;

            Console.WriteLine(
                "CalculateMD5Value enter " +
                new { dg_A, dg_B, dg_C, dg_D }
                );
            // create a buffer with bits padded and length is alos padded
            bMsg = CreatePaddedBuffer();

            N = (uint)(bMsg.Length * 8) / 32;		//no of 32 bit blocks

            for (uint i = 0; i < N / 16; i++)
            {
                CopyBlock(bMsg, i);

                // jsc do you support ref int and then also ref uint?
                // X:\jsc.svn\examples\javascript\test\TestByRefUInt32\TestByRefUInt32\Class1.cs
                PerformTransformation(ref dg_A, ref dg_B, ref dg_C, ref dg_D);

                //RotateLeft { uiNumber = 2911426732, shift = 21, value = 362131739 }
                //CalculateMD5Value PerformTransformation { i = 0, dg_A = 3649838548, dg_B = 78774415, dg_C = 2550759657, dg_D = 2118318316 }
                //CalculateMD5Value exit { dg_A = 3649838548, dg_B = 78774415, dg_C = 2550759657, dg_D = 2118318316 }
                //CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 }
                //RotateLeft { uiNumber = 1138166239, shift = 7, value = 3951357857 }

                //0:31ms CalculateMD5Value PerformTransformation { i = 0, dg_A = 141088792020, dg_B = 150402629775, dg_C = 148579647721, dg_D = 143852239084 } view-source:36394
                //0:31ms CalculateMD5Value exit { dg_A = 141088792020, dg_B = 150402629775, dg_C = 148579647721, dg_D = 143852239084 } view-source:36394
                //0:33ms CalculateMD5Value enter { dg_A = 1732584193, dg_B = 4023233417, dg_C = 2562383102, dg_D = 271733878 } view-source:36394
                //0:33ms RotateLeft { uiNumber = 9728100831, shift = 7, value = 3951357857 } 

                Console.WriteLine(
                    "CalculateMD5Value PerformTransformation " +
                    new { i, dg_A, dg_B, dg_C, dg_D }
                    );
            }


            // jsc can handle local byrefs for now
            dg.A = dg_A;
            dg.B = dg_B;
            dg.C = dg_C;
            dg.D = dg_D;

            Console.WriteLine(
                "CalculateMD5Value exit " +
                new { dg_A, dg_B, dg_C, dg_D }
                );
            return dg;
        }

        /********************************************************
         * TRANSFORMATIONS :  FF , GG , HH , II  acc to RFC 1321
         * where each Each letter represnets the aux function used
         *********************************************************/



        /// <summary>
        /// perform transformatio using f(((b&c) | (~(b)&d))
        /// </summary>
        public void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            //0:31ms TransF enter { a = 1298439783, b = 3640036287, c = 4287987886, d = 1158240751, k = 15, s = 22, i = 16 } view-source:36394
            //0:31ms TransF exit { a = 3230906716, b = 3640036287, c = 4287987886, d = 1158240751, k = 15, s = 22, i = 16 } view-source:36394
            //0:32ms PerformTransformation exit { AA = 1732584193, A = 1676787624, AAA = 3409371817 } 

            //TransF enter { a = 1298439783, b = 3640036287, c = 4287987886, d = 1158240751, k = 15, s = 22, i = 16 }
            //TransF exit { a = 3063134556, b = 3640036287, c = 4287987886, d = 1158240751, k = 15, s = 22, i = 16 }
            //PerformTransformation exit { AA = 1732584193, A = 3271237212, AAA = 708854109 }



            //   ref$b[0] = (c + ZAAABlo95zeSv7E3WiwqXw((((ref$b[0] + ((((c & d) >>> 0) | ((~c & e) >>> 0)) >>> 0)) + a[0].X[(~~f)]) + DwAABCsrrziddgpvRTLKgg[(~~(h - 1))]), g));

            var X_k = X[k];

            Console.WriteLine(
               "TransF enter " +
               new { a, b, c, d, k, s, i, X_k }
               );

            //int321 = MD5Type.T[(uint((i -1)))];
            //int322 = (b & c);
            //int323 = ((~b) & d);
            //int324 = (int322 | int323);
            //int325 = (((ref_arg1[0] + int324) + int320) + int321);
            //ref_arg1[0] = (b + MD5Helper.RotateLeft_df02d3dd_06000020(int325, s));

            var T_i = T[i - 1];

            Console.WriteLine("TransF " + new { T_i });

            var b_c = (b & c);

            Console.WriteLine("TransF " + new { b_c });

            var b_d = (~(b) & d);

            Console.WriteLine("TransF " + new { b_d });

            var b_c_b_d = b_c | b_d;

            Console.WriteLine("TransF " + new { b_c_b_d });

            // ((((ref$b[0] + m) + i) + j) & 0xffffffff) >>> 0
            // X:\jsc.svn\examples\javascript\test\TestUInt32AddOvf\TestUInt32AddOvf\Application.cs

            var uiNumber = a
                + b_c_b_d
                + X_k + T_i;

            Console.WriteLine("TransF " + new { uiNumber });

            a = b + MD5Helper.RotateLeft(
                uiNumber,
                s
            );



            Console.WriteLine(
               "TransF exit " +
               new { a, b, c, d, k, s, i }
               );
        }

        /// <summary>
        /// perform transformatio using g((b&d) | (c & ~d) )
        /// </summary>
        protected void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + MD5Helper.RotateLeft((a + ((b & d) | (c & ~d)) + X[k] + T[i - 1]), s);
        }

        /// <summary>
        /// perform transformatio using h(b^c^d)
        /// </summary>
        protected void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + MD5Helper.RotateLeft((a + (b ^ c ^ d) + X[k] + T[i - 1]), s);
        }

        /// <summary>
        /// perform transformatio using i (c^(b|~d))
        /// </summary>
        protected void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + MD5Helper.RotateLeft((a + (c ^ (b | ~d)) + X[k] + T[i - 1]), s);
        }



        /// <summary>
        /// Perform All the transformation on the data
        /// </summary>
        /// <param name="A">A</param>
        /// <param name="B">B </param>
        /// <param name="C">C</param>
        /// <param name="D">D</param>
        protected void PerformTransformation(ref uint A, ref uint B, ref uint C, ref uint D)
        {
            // X:\jsc.svn\examples\javascript\test\TestByRefUInt32\TestByRefUInt32\Class1.cs
            //script: error JSC1000: opcode unsupported - [0x0002] ldind.u4   +1 -1{[0x0001] ldarg.1    +1 -0}

            // 
            //// saving  ABCD  to be used in end of loop

            uint AA, BB, CC, DD;

            AA = A;

            Console.WriteLine("PerformTransformation entr " + new { AA, A });

            BB = B;
            CC = C;
            DD = D;

            /* Round 1 
                * [ABCD  0  7  1]  [DABC  1 12  2]  [CDAB  2 17  3]  [BCDA  3 22  4]
                * [ABCD  4  7  5]  [DABC  5 12  6]  [CDAB  6 17  7]  [BCDA  7 22  8]
                * [ABCD  8  7  9]  [DABC  9 12 10]  [CDAB 10 17 11]  [BCDA 11 22 12]
                * [ABCD 12  7 13]  [DABC 13 12 14]  [CDAB 14 17 15]  [BCDA 15 22 16]
                *  * */
            TransF(ref A, B, C, D, 0, 7, 1); TransF(ref D, A, B, C, 1, 12, 2); TransF(ref C, D, A, B, 2, 17, 3); TransF(ref B, C, D, A, 3, 22, 4);
            TransF(ref A, B, C, D, 4, 7, 5); TransF(ref D, A, B, C, 5, 12, 6); TransF(ref C, D, A, B, 6, 17, 7); TransF(ref B, C, D, A, 7, 22, 8);
            TransF(ref A, B, C, D, 8, 7, 9); TransF(ref D, A, B, C, 9, 12, 10); TransF(ref C, D, A, B, 10, 17, 11); TransF(ref B, C, D, A, 11, 22, 12);
            TransF(ref A, B, C, D, 12, 7, 13); TransF(ref D, A, B, C, 13, 12, 14); TransF(ref C, D, A, B, 14, 17, 15); TransF(ref B, C, D, A, 15, 22, 16);
            /** rOUND 2
                **[ABCD  1  5 17]  [DABC  6  9 18]  [CDAB 11 14 19]  [BCDA  0 20 20]
                *[ABCD  5  5 21]  [DABC 10  9 22]  [CDAB 15 14 23]  [BCDA  4 20 24]
                *[ABCD  9  5 25]  [DABC 14  9 26]  [CDAB  3 14 27]  [BCDA  8 20 28]
                *[ABCD 13  5 29]  [DABC  2  9 30]  [CDAB  7 14 31]  [BCDA 12 20 32]
            */
            TransG(ref A, B, C, D, 1, 5, 17); TransG(ref D, A, B, C, 6, 9, 18); TransG(ref C, D, A, B, 11, 14, 19); TransG(ref B, C, D, A, 0, 20, 20);
            TransG(ref A, B, C, D, 5, 5, 21); TransG(ref D, A, B, C, 10, 9, 22); TransG(ref C, D, A, B, 15, 14, 23); TransG(ref B, C, D, A, 4, 20, 24);
            TransG(ref A, B, C, D, 9, 5, 25); TransG(ref D, A, B, C, 14, 9, 26); TransG(ref C, D, A, B, 3, 14, 27); TransG(ref B, C, D, A, 8, 20, 28);
            TransG(ref A, B, C, D, 13, 5, 29); TransG(ref D, A, B, C, 2, 9, 30); TransG(ref C, D, A, B, 7, 14, 31); TransG(ref B, C, D, A, 12, 20, 32);
            /*  rOUND 3
                * [ABCD  5  4 33]  [DABC  8 11 34]  [CDAB 11 16 35]  [BCDA 14 23 36]
                * [ABCD  1  4 37]  [DABC  4 11 38]  [CDAB  7 16 39]  [BCDA 10 23 40]
                * [ABCD 13  4 41]  [DABC  0 11 42]  [CDAB  3 16 43]  [BCDA  6 23 44]
                * [ABCD  9  4 45]  [DABC 12 11 46]  [CDAB 15 16 47]  [BCDA  2 23 48]
             * */
            TransH(ref A, B, C, D, 5, 4, 33); TransH(ref D, A, B, C, 8, 11, 34); TransH(ref C, D, A, B, 11, 16, 35); TransH(ref B, C, D, A, 14, 23, 36);
            TransH(ref A, B, C, D, 1, 4, 37); TransH(ref D, A, B, C, 4, 11, 38); TransH(ref C, D, A, B, 7, 16, 39); TransH(ref B, C, D, A, 10, 23, 40);
            TransH(ref A, B, C, D, 13, 4, 41); TransH(ref D, A, B, C, 0, 11, 42); TransH(ref C, D, A, B, 3, 16, 43); TransH(ref B, C, D, A, 6, 23, 44);
            TransH(ref A, B, C, D, 9, 4, 45); TransH(ref D, A, B, C, 12, 11, 46); TransH(ref C, D, A, B, 15, 16, 47); TransH(ref B, C, D, A, 2, 23, 48);
            /*ORUNF  4
                *[ABCD  0  6 49]  [DABC  7 10 50]  [CDAB 14 15 51]  [BCDA  5 21 52]
                *[ABCD 12  6 53]  [DABC  3 10 54]  [CDAB 10 15 55]  [BCDA  1 21 56]
                *[ABCD  8  6 57]  [DABC 15 10 58]  [CDAB  6 15 59]  [BCDA 13 21 60]
                *[ABCD  4  6 61]  [DABC 11 10 62]  [CDAB  2 15 63]  [BCDA  9 21 64]
                         * */
            TransI(ref A, B, C, D, 0, 6, 49); TransI(ref D, A, B, C, 7, 10, 50); TransI(ref C, D, A, B, 14, 15, 51); TransI(ref B, C, D, A, 5, 21, 52);
            TransI(ref A, B, C, D, 12, 6, 53); TransI(ref D, A, B, C, 3, 10, 54); TransI(ref C, D, A, B, 10, 15, 55); TransI(ref B, C, D, A, 1, 21, 56);
            TransI(ref A, B, C, D, 8, 6, 57); TransI(ref D, A, B, C, 15, 10, 58); TransI(ref C, D, A, B, 6, 15, 59); TransI(ref B, C, D, A, 13, 21, 60);
            TransI(ref A, B, C, D, 4, 6, 61); TransI(ref D, A, B, C, 11, 10, 62); TransI(ref C, D, A, B, 2, 15, 63); TransI(ref B, C, D, A, 9, 21, 64);

            var AAA = A + AA;
            Console.WriteLine("PerformTransformation exit " + new { AA, A, AAA });

            A = AAA;


            B = B + BB;
            C = C + CC;
            D = D + DD;


        }


        /// <summary>
        /// Create Padded buffer for processing , buffer is padded with 0 along 
        /// with the size in the end
        /// </summary>
        /// <returns>the padded buffer as byte array</returns>
        protected byte[] CreatePaddedBuffer()
        {
            uint pad;		//no of padding bits for 448 mod 512 
            byte[] bMsg;	//buffer to hold bits
            ulong sizeMsg;		//64 bit size pad
            uint sizeMsgBuff;	//buffer size in multiple of bytes
            int temp = (448 - ((m_byteInput.Length * 8) % 512)); //temporary 


            pad = (uint)((temp + 512) % 512);		//getting no of bits to  be pad
            if (pad == 0)				///pad is in bits
                pad = 512;			//at least 1 or max 512 can be added

            sizeMsgBuff = (uint)((m_byteInput.Length) + (pad / 8) + 8);
            sizeMsg = (ulong)m_byteInput.Length * 8;
            bMsg = new byte[sizeMsgBuff];	///no need to pad with 0 coz new bytes 
            // are already initialize to 0 :)




            ////copying string to buffer 
            for (int i = 0; i < m_byteInput.Length; i++)
            {
                var value = m_byteInput[i];
                bMsg[i] = value;

                Console.WriteLine("CreatePaddedBuffer " + new { i, value });

            }

            bMsg[m_byteInput.Length] |= 0x80;		///making first bit of padding 1,

            //wrting the size value
            for (int i = 8; i > 0; i--)
            {

                var offset = sizeMsgBuff - i;

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140314
                // X:\jsc.svn\examples\javascript\Test\TestBitShiftRight\TestBitShiftRight\Application.cs

                // 0:32ms CreatePaddedBuffer { i = 4, offset = 60, value = 40, sizeMsg = 40 } 
                var value = (byte)(sizeMsg >> ((8 - i) * 8) & 0x00000000000000ff);

                // 0:51ms CreatePaddedBuffer { offset = 60, value = 40 } 
                Console.WriteLine("CreatePaddedBuffer " + new { i, offset, value, sizeMsg });


                bMsg[offset] = value;
            }

            return bMsg;
        }


        /// <summary>
        /// Copies a 512 bit block into X as 16 32 bit words
        /// </summary>
        /// <param name="bMsg"> source buffer</param>
        /// <param name="block">no of block to copy starting from 0</param>
        protected void CopyBlock(byte[] bMsg, uint block)
        {

            block = block << 6;
            for (uint j = 0; j < 61; j += 4)
            {
                var value3 = ((uint)bMsg[block + (j + 3)]) << 24;
                var value2 = ((uint)bMsg[block + (j + 2)]) << 16;
                var value1 = ((uint)bMsg[block + (j + 1)]) << 8;

                //0:38ms CopyBlock { offset = 13, value = 0, value3 = 0, value2 = 0, value1 = 0, value0 = 0 } view-source:36394
                //0:38ms CopyBlock { offset = 14, value = 40, value3 = 0, value2 = 0, value1 = 0, value0 = 40 } view-source:36394
                //0:38ms CopyBlock { offset = 15, value = 40, value3 = 0, value2 = 0, value1 = 0, value0 = 40 } 

                //CreatePaddedBuffer { i = 2, offset = 62, value = 0, sizeMsg = 40 }
                //CreatePaddedBuffer { i = 1, offset = 63, value = 0, sizeMsg = 40 }

                // suspect.
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140314
                var value0 = (((uint)bMsg[block + (j)]));

                var value = value3 |
                        value2 |
                        value1 |
                        value0;

                var offset = j >> 2;

                Console.WriteLine("CopyBlock " + new { offset, value, value3, value2, value1, block, j, value0 });
                X[offset] = value;

            }
        }
    }


}
