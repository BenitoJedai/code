using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace AIRServerSocketExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140911

        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\air\AIRStageWebViewExperiment\AIRStageWebViewExperiment\ApplicationSprite.cs

            // http://stackoverflow.com/questions/3170585/get-ip-address-with-adobe-air-2

            var t = new TextField
            {
                text = new
                {
                    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/Capabilities.html
                    Capabilities.os,
                    Capabilities.version,
                    Capabilities.playerType,

                    Capabilities.cpuArchitecture,
                    Capabilities.touchscreenType,

                    // why is this commented out?
                    //WorkerDomain = WorkerDomain.isSupported,

                    // http://na5.brightidea.com/ct/ct_a_view_idea.bix?c=9D564F43-979A-4E35-AA21-85A61B6AB8DE&idea_id=F353B3CC-5F05-45E7-9A73-1093E0A1F9DD

                    NetworkInfo = NetworkInfo.isSupported,
                    //Worker.current.isPrimordial,

                    this.loaderInfo.bytes.length
                }.ToString(),

                autoSize = TextFieldAutoSize.LEFT
            };

            t.AttachTo(this);



            //V:\web\AIRServerSocketExperiment\ApplicationSprite.as(46): col: 127 Error: Implicit coercion of a value of type __AS3__.vec:Vector.<flash.net:NetworkInterface> to an unrelated type __AS3__.vec:Vector.<*>.

            //            LinqExtensions.WithEach_f7a1155f_06000027(CommonExtensions.AsEnumerable_4ebbe596_06001850(NetworkInfo.networkInfo.findInterfaces()), new __Action_1(class21, __IntPtr.op_Explicit_4ebbe596_06001412("__ctor_b__1_f7a1155f_06000005")));
            //                                                                                                                              ^

            //{ os = Windows 7, version = WIN 13,0,0,133, playerType = Desktop, length = 489435 }
            // { name = {84D54A45-2ACC-42B2-A59B-E01D43897D2D}, displayName = Local Area Connection, active = false }
            // address { address = 169.254.45.8, broadcast = 169.254.255.255 }
            // { name = {978F5176-96B4-49D4-A14D-6CEA5CB3D505}, displayName = Bluetooth Network Connection, active = false }
            // address { address = 169.254.6.204, broadcast = 169.254.255.255 }
            // { name = {CE7A76DF-BCB0-4C3B-8466-D712A03F10A0}, displayName = Wireless Network Connection, active = true }
            // address { address = 192.168.43.252, broadcast = 192.168.43.255 }
            // { name = {846EE342-7039-11DE-9D20-806E6F6E6963}, displayName = Loopback Pseudo-Interface 1, active = true }
            // address { address = ::1, broadcast =  }
            // address { address = 127.0.0.1, broadcast =  }
            // { name = {A5178906-144E-433A-9103-B2EB62A4C21E}, displayName = Teredo Tunneling Pseudo-Interface, active = true }
            // address { address = 2001:0:9d38:6abd:28d1:36ea:3f57:d403, broadcast =  }

            if (Capabilities.playerType == "Desktop")
            {
                // we dont want to crash flash player!
                // http://help.adobe.com/en_US/air/build/WS144092a96ffef7cc16ddeea2126bb46b82f-8000.html

                // VerifyError: Error #1014: Class flash.net::NetworkInterface could not be found.

                var ii = NetworkInfo.networkInfo.findInterfaces();
                for (int ij = 0; ij < ii.length; ij++)
                {
                    var i = ii[ij];

                    t.appendText("\n " + new { i.name, i.displayName, i.active });

                    var aa = i.addresses;

                    for (int aj = 0; aj < aa.length; aj++)
                    {
                        var a = aa[aj];

                        t.appendText("\n address " + new { a.address, a.broadcast, a.ipVersion });
                    }
                }

            }

            //V:\web\AIRServerSocketExperiment\ApplicationSprite___c__DisplayClass3.as(28): col: 105 Error: 
            // Implicit coercion of a value of type __AS3__.vec:Vector.<flash.net:InterfaceAddress> 
            // to an unrelated type __AS3__.vec:Vector.<*>.

            //            LinqExtensions.WithEach_f7a1155f_06000022(CommonExtensions.AsEnumerable_4ebbe596_06001850(i.addresses), new __Action_1(this, __IntPtr.op_Explicit_4ebbe596_06001413(this.__ctor_b__2_f7a1155f_06000006)));
            //                                                                                                        ^





            //i.addresses.AsEnumerable().WithEach(
            //    a =>
            //    {
            //    }
            //);





            ////if (Capabilities.playerType == CapabilitiesPlayerType)
            //var s = new ServerSocket();
            //s.bind(8080);


            //s.connect +=
            //    e =>
            //    {

            //    };

            //s.listen(30);
        }

    }
}
