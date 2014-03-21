using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCSC;

namespace TestMonoPcsc
{
    class Program
    {
        static void CheckErr(SCardError err)
        {
            if (err != SCardError.Success)
                throw new PCSCException(err,
                    SCardHelper.StringifyError(err));
        }

        static void Main(string[] args)
        {
            try
            {
            SCardContext hContext = new SCardContext();
            hContext.Establish(SCardScope.System);

            var szReaders = hContext.GetReaders();

            if (szReaders.Length <= 0)
                throw new PCSCException(SCardError.NoReadersAvailable,
                    "Could not find any Smartcard reader.");

                Console.WriteLine("reader name: " + szReaders[1]);

                // Create a reader object using the existing context
                SCardReader reader = new SCardReader(hContext);

                // Connect to the card
                SCardError err = reader.Connect(szReaders[1],
                    SCardShareMode.Shared,
                    SCardProtocol.T0 | SCardProtocol.T1);
                CheckErr(err);

                IntPtr pioSendPci;
                switch (reader.ActiveProtocol)
                {
                    case SCardProtocol.T0:
                        pioSendPci = SCardPCI.T0;
                        break;
                    case SCardProtocol.T1:
                        pioSendPci = SCardPCI.T1;
                        break;
                    default:
                        throw new PCSCException(SCardError.ProtocolMismatch,
                            "Protocol not supported: "
                            + reader.ActiveProtocol.ToString());
                }

                byte[] pbRecvBuffer = new byte[256];

                // Send SELECT command
                // Select command 0x00, 0xA4, 0x04, 0x00,
                //Length  0x08
                //AID A0A1A2A3A4000301
                byte[] cmd1 = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x08, 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0x00, 0x03, 0x01 };
                err = reader.Transmit(pioSendPci, cmd1, ref pbRecvBuffer);
                CheckErr(err);

                //(6A82: The application to be selected could not be found.)

                Console.Write("Select response: ");
                for (int i = 0; i < pbRecvBuffer.Length; i++)
                    Console.Write("{0:X2} ", pbRecvBuffer[i]);
                Console.WriteLine();

                pbRecvBuffer = new byte[256];

                // Send test command
                byte[] cmd2 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                err = reader.Transmit(pioSendPci, cmd2, ref pbRecvBuffer);
                CheckErr(err);

                Console.Write("Test commad response: ");
                for (int i = 0; i < pbRecvBuffer.Length; i++)
                    Console.Write("{0:X2} ", pbRecvBuffer[i]);
                Console.WriteLine();

                hContext.Release();
                
            }
            catch (PCSCException ex)
            {
                Console.WriteLine("Ouch: "
                    + ex.Message
                    + " (" + ex.SCardError.ToString() + ")");
            }
             Console.ReadLine();
        }

        }

    public interface IMyPCSC
    {
        byte[] SendBytes(byte[] inBytes, SCardReader reader, SCardError err, SCardContext context);
    }
    public class MyPCSCImplementation:IMyPCSC
    {
        public byte[] SendBytes(byte[] inBytes, SCardReader reader, SCardError err, SCardContext context)
        {
            try
            {
                //SCardReader reader = new SCardReader(context);
                //// Connect to the card
                //Console.WriteLine(readerName);
                //SCardError err = reader.Connect(readerName,
                //    SCardShareMode.Shared,
                //    SCardProtocol.T0 | SCardProtocol.T1);

                IntPtr pioSendPci;
                switch (reader.ActiveProtocol)
                {
                    case SCardProtocol.T0:
                        pioSendPci = SCardPCI.T0;
                        break;
                    case SCardProtocol.T1:
                        pioSendPci = SCardPCI.T1;
                        break;
                    default:
                        throw new PCSCException(SCardError.ProtocolMismatch,
                            "Protocol not supported: "
                            + reader.ActiveProtocol.ToString());
                }
                
                byte[] pbRecvBuffer = new byte[256];
                err = reader.Transmit(pioSendPci, inBytes, ref pbRecvBuffer);
                return pbRecvBuffer;
            }
            catch (PCSCException)
            {
                return null;
            }
        }
    }

}
