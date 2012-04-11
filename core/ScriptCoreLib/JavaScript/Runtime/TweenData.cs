using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.JavaScript;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.Shared;
namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script]
	[System.Obsolete("To be moved out of CoreLib or removed")]
    public class TweenData<TValue>
    {
        private bool Dirty;

        protected TValue CurrentValue;
        protected TValue FutureValue;

        private readonly Timer SyncTimer;

        public TweenData()
        {
            SyncTimer = new Timer(
                delegate
                {
                    if (IsCloseEnough)
                    {
                        SyncTimer.Stop();
                        Helper.Invoke(Done);
                    }
                    else
                    {
                        Helper.Invoke(Tick);
                    }
                }
            );
        }

        protected event System.Action Tick;

        public event System.Action Done;

        protected System.Action<Predicate> IsCloseEnoughHandler;

        protected bool IsCloseEnough
        {
            get
            {
                return Predicate.Is(IsCloseEnoughHandler, false);
            }
        }

        protected event System.Action FutureValueChanged;

        public event System.Action ValueChanged;

        public int Speed = 50;

        public TValue Value
        {
            get
            {
                return this.CurrentValue;
            }
            set
            {
        
                if (this.Dirty)
                {
                    this.FutureValue = value;

                    Helper.Invoke(this.FutureValueChanged);

                    if (!IsCloseEnough)
                        this.SyncTimer.StartInterval(Speed);
     

                }
                else
                {
                    this.FutureValue = value;

                    Helper.Invoke(this.FutureValueChanged);

                    this.CurrentValue = this.FutureValue;
                    this.Dirty = true;

                    RaiseValueChanged();
                    
                }
            }
        }

        protected void RaiseValueChanged()
        {
            if (IsCloseEnough)
                this.CurrentValue = this.FutureValue;


            Helper.Invoke(this.ValueChanged);
        }
    }
}
