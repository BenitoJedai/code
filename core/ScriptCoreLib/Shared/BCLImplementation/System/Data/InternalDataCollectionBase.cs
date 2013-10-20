using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.InternalDataCollectionBase))]
    public class __InternalDataCollectionBase : IEnumerable
    {


        //no implementation for System.Data.InternalDataCollectionBase e55de979-3346-3493-ade7-ecc252f2d229
        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.InternalDataCollectionBase.GetEnumerator()]

        //script: error JSC1000: No implementation found for this native method, please implement [System.Data.InternalDataCollectionBase.get_Count()]

        public virtual int Count
        {
            get { return 0; }
        }

        public IEnumerator GetEnumerator()
        {
            return GetInternalList().GetEnumerator();
        }

        public virtual IEnumerable GetInternalList() { return null; }
    }
}
