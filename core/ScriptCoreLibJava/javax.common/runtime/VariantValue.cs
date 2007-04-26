using ScriptCoreLib;



namespace javax.common.runtime.types
{
    /// <summary>
    /// allows to convert seamlessly between long and a hex string
    /// </summary>
    [Script]
    public sealed class VariantValue
    {
        public static VariantValue OfHexValue(string hex)
        {
            VariantValue n = new VariantValue();

            n.HexValue = hex;

            return n;
        }



        public string HexValue
        {
            get { return Convert.ToHexString(_bytes); }
            set
            {
                _bytes = Convert.FromHexString(value);
            }
        }

        sbyte[] _bytes;

        public sbyte[] ByteValue
        {
            get
            {
                return _bytes;
            }
            set
            {
                _bytes = value;
            }
        }



        public void Increment()
        {
            int u = Convert.ToInt32(_bytes[_bytes.Length - 1]);

            if (u == 0xFF)
            {
                u = Convert.ToInt32(_bytes[_bytes.Length - 2]);

                sbyte _n = (sbyte)( u + 1);

                _bytes[_bytes.Length - 2] = _n;
                _bytes[_bytes.Length - 1] = 0;
            }
            else
            {
                sbyte _n = (sbyte)(u + 1);

                _bytes[_bytes.Length - 1] = _n;
            }
        }
    }
}
