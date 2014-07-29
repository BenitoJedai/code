using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using java.math;
using com.mastercard.api.payments.v2.client;
using com.mastercard.api.payments.v2;

namespace com.mastercard
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://developer.mastercard.com/portal/display/api/Software+Development+Kit
            // http://newsroom.mastercard.com/2014/07/11/masterpass-continues-global-expansion-product-enhancements-planned-for-august/

            {
                //FullName = System.Reflection.TargetInvocationException, InnerException = System.IO.FileNotFoundException: The JVM/ CLR feature is not available within this build.

                System.Console.WriteLine(
                   typeof(object).AssemblyQualifiedName
                );

                // jsc java natives/ where is the ctor we think of?
                PaymentClient client = new PaymentClient(getPrivateKey(), "my-client-id", false);
                PurchaseRequest purchaseRequest = new PurchaseRequest();

                Amount amount = new Amount();
                amount.setCurrency("GBP");
                amount.setValue(new BigInteger("441250"));
                purchaseRequest.setAmount(amount);

                MerchantIdentity merchantIdentity = new MerchantIdentity();
                merchantIdentity.setClientId("my-merchant-id");
                merchantIdentity.setPassword("my-merchant-password");

                purchaseRequest.setMerchantIdentity(merchantIdentity);
                purchaseRequest.setClientReference(getClientReference());

                Card card = new Card();
                card.setAccountNumber("6XXXXXXXXXXXXXXX");
                card.setExpiryMonth("01");
                card.setExpiryYear("11");
                card.setSecurityCode("111");

                purchaseRequest.setFundingCard(card);

                Purchase purchase = client.createPurchase(purchaseRequest);

                CLRProgram.CLRMain();
            }


        }

        private static string getClientReference()
        {
            throw new NotImplementedException();
        }

        private static object getPrivateKey()
        {
            throw new NotImplementedException();
        }

        public delegate XElement XElementFunc();

        [SwitchToCLRContext]
        static class CLRProgram
        {
            public static XElement XML { get; set; }

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            public static void CLRMain()
            {
                System.Console.WriteLine(
                    typeof(object).AssemblyQualifiedName
                );



                MessageBox.Show("click to close");

            }
        }


    }
