using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Nonoba.api;
using FlashTowerDefense.Shared;

namespace FlashTowerDefense.ActionScript.Client
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class ClientExtensions
    {
        public static void SendMessage(this Connection c, SharedClass1.Messages m, params object[] e)
        {
            var i = new Message(((int)m).ToString());

            foreach (var z in e)
            {
                i.Add(z);
            }

            c.Send(i);
        }
    }
}
