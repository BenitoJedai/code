using ScriptCoreLib;
using ScriptCoreLib.PHP;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.PHP.Net
{
    public partial class ServerTransport<TType>
    {
        [Script]
        public class Switch<TContext>
        {
            public Switch(string FORMStreamName, string JSONStreamName, EventHandler<Predicate<TType, TContext>> _ValueToContext)
            {
                this.StreamType[FORMStreamName] = true;
                this.StreamType[JSONStreamName] = false;

                ValueToContext = _ValueToContext;
            }

            public IArray<string, bool> StreamType = new IArray<string, bool>();

            public CustomSwitch<string> StreamSwitch = new CustomSwitch<string>();
            public CustomSwitch<TContext> ContextSwitch = new CustomSwitch<TContext>();



     

            public EventHandler<ServerTransport<TType>> this[params TContext[] e]
            {
                set
                {
                    ContextSwitch[e] =
                        delegate
                        {
                            Helper.Invoke(value, InternalHandler);
                        };
                }
            }

            public EventHandler<Predicate<TType, TContext>> ValueToContext;


            ServerTransport<TType> InternalHandler;

            public bool DoStream(string e)
            {
                StreamSwitch[StreamType.Keys] =
                    delegate(CustomSwitch<string>.EventHandlerArgs args)
                    {
                        DoContext(this.StreamType[args.Index]);
                    };

                return StreamSwitch.Run(e);
            }

            private void DoContext(bool isFORM)
            {
                using (ServerTransport<TType> z = new ServerTransport<TType>(isFORM))
                {
                    z.Strict();

                    InternalHandler = z;

                    ContextSwitch.Run(Convert.To(ValueToContext, z.Data));
                }
            }
        }
    }

}
