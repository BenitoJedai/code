using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

#if !NoAttributes
using ScriptCoreLib;
#endif

namespace FlashTowerDefense.Shared
{


    public partial class SharedClass1
    {
        #region RemoteMessages

#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteMessages : IMessages
        {
            public Action<SendArguments> Send;
            #region SendArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class SendArguments
            {
                public Messages i;
                public object[] args;
            }
            #endregion

            public void TeleportTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
            }
            public void UserTeleportTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserTeleportTo, args = new object[] { user, x, y } });
            }
            public void WalkTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.WalkTo, args = new object[] { x, y } });
            }
            public void UserWalkTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserWalkTo, args = new object[] { user, x, y } });
            }
            public void CancelServerRandomNumbers()
            {
                Send(new SendArguments { i = Messages.CancelServerRandomNumbers, args = new object[] { } });
            }
            public void ReadyForServerRandomNumbers()
            {
                Send(new SendArguments { i = Messages.ReadyForServerRandomNumbers, args = new object[] { } });
            }
            public void TakeBox(int box)
            {
                Send(new SendArguments { i = Messages.TakeBox, args = new object[] { box } });
            }
            public void UserTakeBox(int user, int box)
            {
                Send(new SendArguments { i = Messages.UserTakeBox, args = new object[] { user, box } });
            }
            public void FiredShotgun()
            {
                Send(new SendArguments { i = Messages.FiredShotgun, args = new object[] { } });
            }
            public void UserFiredShotgun(int user)
            {
                Send(new SendArguments { i = Messages.UserFiredShotgun, args = new object[] { user } });
            }
            public void ServerRandomNumbers(double[] e)
            {
                var args = new object[e.Length];
                Array.Copy(e, args, e.Length);
                Send(new SendArguments { i = Messages.ServerRandomNumbers, args = args });
            }
        }
        #endregion


        #region RemoteEvents

#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public sealed partial class RemoteEvents
        {
            private readonly Dictionary<Messages, Action<DispatchHelper>> DispatchTable;
            private readonly Dictionary<Messages, Converter<object, Delegate>> DispatchTableDelegates;
            #region DispatchHelper

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public partial class DispatchHelper
            {
                public Converter<uint, int> GetInt32;
                public Converter<uint, double> GetDouble;
                public Converter<uint, string> GetString;
                public Converter<uint, int[]> GetInt32Array;
                public Converter<uint, double[]> GetDoubleArray;
                public Converter<uint, string[]> GetStringArray;
                public Converter<uint, object[]> GetArray;
            }
            #endregion

            public bool Dispatch(Messages e, DispatchHelper h)
            {
                if (!DispatchTableDelegates.ContainsKey(e)) return false;
                if (DispatchTableDelegates[e](null) == null) return false;
                if (!DispatchTable.ContainsKey(e)) return false;
                DispatchTable[e](h);
                return true;
            }
            #region TeleportToArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class TeleportToArguments
            {
                public int x;
                public int y;
            }
            #endregion

            public event Action<TeleportToArguments> TeleportTo;
            #region UserTeleportToArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserTeleportToArguments
            {
                public int user;
                public int x;
                public int y;
            }
            #endregion

            public event Action<UserTeleportToArguments> UserTeleportTo;
            #region WalkToArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class WalkToArguments
            {
                public int x;
                public int y;
            }
            #endregion

            public event Action<WalkToArguments> WalkTo;
            #region UserWalkToArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserWalkToArguments
            {
                public int user;
                public int x;
                public int y;
            }
            #endregion

            public event Action<UserWalkToArguments> UserWalkTo;
            #region CancelServerRandomNumbersArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class CancelServerRandomNumbersArguments
            {
            }
            #endregion

            public event Action<CancelServerRandomNumbersArguments> CancelServerRandomNumbers;
            #region ReadyForServerRandomNumbersArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ReadyForServerRandomNumbersArguments
            {
            }
            #endregion

            public event Action<ReadyForServerRandomNumbersArguments> ReadyForServerRandomNumbers;
            #region TakeBoxArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class TakeBoxArguments
            {
                public int box;
            }
            #endregion

            public event Action<TakeBoxArguments> TakeBox;
            #region UserTakeBoxArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserTakeBoxArguments
            {
                public int user;
                public int box;
            }
            #endregion

            public event Action<UserTakeBoxArguments> UserTakeBox;
            #region FiredShotgunArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class FiredShotgunArguments
            {
            }
            #endregion

            public event Action<FiredShotgunArguments> FiredShotgun;
            #region UserFiredShotgunArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class UserFiredShotgunArguments
            {
                public int user;
            }
            #endregion

            public event Action<UserFiredShotgunArguments> UserFiredShotgun;
            #region ServerRandomNumbersArguments

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed partial class ServerRandomNumbersArguments
            {
                public double[] e;
            }
            #endregion

            public event Action<ServerRandomNumbersArguments> ServerRandomNumbers;
            public RemoteEvents()
            {
                DispatchTable = new Dictionary<Messages, Action<DispatchHelper>>
                    {
                        { Messages.TeleportTo, e => { TeleportTo(new TeleportToArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                        { Messages.UserTeleportTo, e => { UserTeleportTo(new UserTeleportToArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                        { Messages.WalkTo, e => { WalkTo(new WalkToArguments { x = e.GetInt32(0), y = e.GetInt32(1) }); } },
                        { Messages.UserWalkTo, e => { UserWalkTo(new UserWalkToArguments { user = e.GetInt32(0), x = e.GetInt32(1), y = e.GetInt32(2) }); } },
                        { Messages.CancelServerRandomNumbers, e => { CancelServerRandomNumbers(new CancelServerRandomNumbersArguments {  }); } },
                        { Messages.ReadyForServerRandomNumbers, e => { ReadyForServerRandomNumbers(new ReadyForServerRandomNumbersArguments {  }); } },
                        { Messages.TakeBox, e => { TakeBox(new TakeBoxArguments { box = e.GetInt32(0) }); } },
                        { Messages.UserTakeBox, e => { UserTakeBox(new UserTakeBoxArguments { user = e.GetInt32(0), box = e.GetInt32(1) }); } },
                        { Messages.FiredShotgun, e => { FiredShotgun(new FiredShotgunArguments {  }); } },
                        { Messages.UserFiredShotgun, e => { UserFiredShotgun(new UserFiredShotgunArguments { user = e.GetInt32(0) }); } },
                        { Messages.ServerRandomNumbers, e => { ServerRandomNumbers(new ServerRandomNumbersArguments { e = e.GetDoubleArray(0) }); } },
                    }
                ;
                DispatchTableDelegates = new Dictionary<Messages, Converter<object, Delegate>>
                    {
                        { Messages.TeleportTo, e => TeleportTo },
                        { Messages.UserTeleportTo, e => UserTeleportTo },
                        { Messages.WalkTo, e => WalkTo },
                        { Messages.UserWalkTo, e => UserWalkTo },
                        { Messages.CancelServerRandomNumbers, e => CancelServerRandomNumbers },
                        { Messages.ReadyForServerRandomNumbers, e => ReadyForServerRandomNumbers },
                        { Messages.TakeBox, e => TakeBox },
                        { Messages.UserTakeBox, e => UserTakeBox },
                        { Messages.FiredShotgun, e => FiredShotgun },
                        { Messages.UserFiredShotgun, e => UserFiredShotgun },
                        { Messages.ServerRandomNumbers, e => ServerRandomNumbers },
                    }
                ;
            }
        }
        #endregion
    }
}