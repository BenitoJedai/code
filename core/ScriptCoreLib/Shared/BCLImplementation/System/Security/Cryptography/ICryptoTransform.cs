using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/ICryptoTransform.cs

    [Script(Implements = typeof(global::System.Security.Cryptography.ICryptoTransform))]
    public interface __ICryptoTransform : IDisposable
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\CryptoStream.cs
        // can we get it working with webCrypto?

        bool CanReuseTransform
        {
            get;
        }

        bool CanTransformMultipleBlocks
        {
            get;
        }

        int InputBlockSize
        {
            get;
        }

        int OutputBlockSize
        {
            get;
        }

        int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

        byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
    }
}
