using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestInitializeArray
{
    class Program
    {
        static byte[] foo;

        static void Main(string[] args)
        {

            var code = new byte[]
                        {
                            //		[0x00000000]	{[0x0000] nop        //0 -0}	jsc.ILInstruction
		                    /* 0x00000000*/	0x00	,

                            //		[0x00000001]	{[0x0001] ldarg.0    //1 -0}	jsc.ILInstruction
		                    /* 0x00000001*/	0x02	,

                            //		[0x00000002]	{[0x0002] dup        //2 -1}	jsc.ILInstruction
		                    /* 0x00000002*/	0x25	,
		
                            //		[0x00000003]	{[0x0003] ldfld      //1 -1}	jsc.ILInstruction
		                    /* 0x00000003*/	0x7b	,

		                    /*              0x00000004*/	0x02	,
		                    /*              0x00000005*/	0x00	,
		                    /*              0x00000006*/	0x00	,
		                    /*              0x00000007*/	0x04	,
		
                            //		[0x00000004]	{[0x0008] ldarg.1    //1 -0}	jsc.ILInstruction
		                    /* 0x00000008*/	0x03	,

                            //		[0x00000005]	{[0x0009] call       //1 -2}	jsc.ILInstruction
		                    /* 0x00000009*/	0x28	,

		                    /*              0x0000000a*/	0x10	,
		                    /*              0x0000000b*/	0x00	,
		                    /*              0x0000000c*/	0x00	,
		                    /*              0x0000000d*/	0x0a	,

                            //		[0x00000006]	{[0x000e] castclass  //1 -1}	jsc.ILInstruction
		                    /* 0x0000000e*/	0x74	,

		                    /*              0x0000000f*/	0x02	,
		                    /*              0x00000010*/	0x00	,
		                    /*              0x00000011*/	0x00	,
		                    /*              0x00000012*/	0x01	,

                            //		[0x00000007]	{[0x0013] stfld      //0 -2}	jsc.ILInstruction
		                    /* 0x00000013*/	0x7d	,

		                    /*              0x00000014*/	0x02	,
		                    /*              0x00000015*/	0x00	,
		                    /*              0x00000016*/	0x00	,
		                    /*              0x00000017*/	0x04	,

                            //		[0x00000008]	{[0x0018] ret        //0 -0}	jsc.ILInstruction
		                    /* 0x00000018*/	0x2a	
                        };


            foo = code;

        }
    }
}
