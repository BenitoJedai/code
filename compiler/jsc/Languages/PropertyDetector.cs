using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace jsc.Languages
{
    public class PropertyDetector
    {
        public PropertyInfo SetProperty;
        public PropertyInfo GetProperty;

        public Type PropertyType
        {
            get
            {
                if (SetProperty != null)
                    return SetProperty.PropertyType;

                if (GetProperty != null)
                    return GetProperty.PropertyType;

                return null;
            }
        }

        public PropertyDetector(MethodBase m)
        {
            if (m.IsConstructor)
                return;

            var any = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            #region set
            {
                var prefix = "set_";
                if (m.Name.StartsWith(prefix))
                {
                    try
                    {

                        SetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
                    }
                    catch (AmbiguousMatchException)
                    {

                    }
                }
            }
            #endregion

            #region get
            {
                var prefix = "get_";
                if (m.Name.StartsWith(prefix))
                {
                    try
                    {
                        GetProperty = m.DeclaringType.GetProperty(m.Name.Substring(prefix.Length), any);
                    }
                    catch (AmbiguousMatchException)
                    {

                    }
                }
            }
            #endregion

        }
    }
}
