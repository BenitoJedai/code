using System;
using System.Collections.Generic;
using System.Text;

#if !NoAttributes
using ScriptCoreLib;
using System.Runtime.CompilerServices;
#endif

namespace FlashTowerDefense.Shared
{


    public partial class SharedClass1
    {
        // members defined over here can be used on client and on server
        // x

        public string Hello;

        /// <summary>
        /// this interface is to be used in a generator
        /// </summary>
        public partial interface IMessages
        {
            // this will generate lots of overkill boilerplate code :)

            // client -> server 
            void TeleportTo(int x, int y);
            // server -> others
            void UserTeleportTo(int user, int x, int y);
        }


    }
}
