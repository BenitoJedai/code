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
    internal static class Extensions
    {
        public static void SendMessage(this Connection c, SharedClass1.Messages m)
        {
            c.Send(new Message(((int)m).ToString()));
        }
    }
}
