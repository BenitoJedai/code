using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidRTC
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //0001 020001c5 ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProvider
        //script: error JSC1000: Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer
        //internal compiler error at method
        // assembly: C:\util\jsc\bin\ScriptCoreLib.dll at
        // type: ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood, ScriptCoreLib, Version=4.6.0.0, Culture=neutral, PublicKeyToken=null
        // method: CreatePaddedBuffer
        // Java : Opcode not implemented: stind.i1 at ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.__MD5CryptoServiceProviderByMahmood.CreatePaddedBuffer


        //       WriteMethodLocalVariables { DeclaringType = AndroidRTC.Activities.ApplicationWebServiceActivity+<>c__DisplayClass24, Name = <CreateServer>b__29, LocalIndex = 83, Count = 84 }
        //   script: error JSC1000: Java :
        //BCL needs another method, please define it.
        //Cannot call type without script attribute :
        //System.Threading.Monitor for Void Enter(System.Object, Boolean ByRef) used at
        //AndroidRTC.Activities.ApplicationWebServiceActivity+<>c__DisplayClass24.<CreateServer>b__29 at offset 0018.
        //If the use of this method is intended, an implementation should be provided with the attribute [Script(Implements = typeof(...)] set.You may have mistyped it.

        //        I/System.Console(27031): #10 POST /xml/GetOffer HTTP/1.1 error:
        //I/System.Console(27031): #10 java.lang.ArrayIndexOutOfBoundsException: length=1079; index=1079
        //I/System.Console(27031): #10 java.lang.ArrayIndexOutOfBoundsException: length=1079; index=1079
        //I/System.Console(27031):        at ScriptCoreLib.Shared.BCLImplementation.System.__Convert.ToBase64String(__Convert.java:177)
        //I/System.Console(27031):        at ScriptCoreLib.Library.StringConversions.UTF8ToBase64StringOrDefault(StringConversions.java:131)
        //I/System.Console(27031):        at AndroidRTC.Global.Invoke(Global.java:259)
        //I/System.Console(27031):        at ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions.InternalApplication_BeginRequest(InternalGlobalExtensions.java:345)
        //I/System.Console(27031):        at AndroidRTC.Global.Application_BeginRequest(Global.java:50)
        //I/System.Console(27031):        at AndroidRTC.Activities.ApplicationWebServiceActivity___c__DisplayClass25._CreateServer_b__20(ApplicationWebServiceActivity___c__DisplayClass25.java:399)

        public string sdp;
        public List<DataRTCIceCandidate> sdpCandidates = new List<DataRTCIceCandidate>();

        public async Task Offer()
        {
            if (string.IsNullOrEmpty(sdp))
            {
                Console.WriteLine("(sdp is null)");
                return;
            }

            Console.WriteLine(new { sdp });

            Memory.AllAvailableOffers.Add(

                new DataOffer
                {
                    sdp = sdp,
                    sdpCandidates = sdpCandidates.ToList()
                }
            );
        }

        public string sdpAnwser;
        // RTCIceCandidate
        public List<DataRTCIceCandidate> sdpAnwserCandidates = new List<DataRTCIceCandidate>();

        public async Task CheckAnswer()
        {
            // copy anwser for sdp, if there is one...

            Memory.AllAvailableAnwsers.FirstOrDefault(z => z.sdpOffer.sdp == this.sdp).With(
                x =>
                {

                    this.sdpAnwser = x.sdpAnwser;
                    this.sdpAnwserCandidates = x.sdpAnwserCandidates;
                }
            );

            // and now remove from memory? 
            // pairing complete

        }

        public DataOffer sdpOffer;

        public async Task GetOffer()
        {
            // if there is an offer available already, lets make it known...

            sdpOffer = Memory.AllAvailableOffers.FirstOrDefault();
        }

        public async Task Anwser()
        {
            foreach (var sdpAnwserCandidate in sdpAnwserCandidates)
            {
                Console.WriteLine(new { sdpAnwserCandidate });
            }
            Console.WriteLine(new { sdpAnwser });
            Console.WriteLine(new { sdpOffer });

            Memory.AllAvailableAnwsers.Add(
                new AnwserToOffer
                {
                    sdpOffer = sdpOffer,
                    sdpAnwser = sdpAnwser,

                    sdpAnwserCandidates = sdpAnwserCandidates.ToList()
                }
            );
        }
    }


    public sealed class DataRTCIceCandidate
    {
        public string candidate;
        public string sdpMid;
        //public ushort sdpMLineIndex;
        public int sdpMLineIndex;
    }

    public class AnwserToOffer
    {
        public DataOffer sdpOffer;

        public string sdpAnwser;
        public List<DataRTCIceCandidate> sdpAnwserCandidates;
    }

    public sealed class DataOffer
    {

        public string sdp;
        public List<DataRTCIceCandidate> sdpCandidates = new List<DataRTCIceCandidate>();
    }

    public static class Memory
    {
        public static List<DataOffer> AllAvailableOffers = new List<DataOffer>();

        public static List<AnwserToOffer> AllAvailableAnwsers = new List<AnwserToOffer>();


    }
}
