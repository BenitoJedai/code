using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.System;
using ScriptCoreLib.JavaScript.Runtime;


namespace ScriptCoreLib.JavaScript.Net
{

    [Script]
    public class ClientTansport<TType>
        where TType : class
    {
        public MyTransportDescriptor<TType> Descriptor;

        public string Url;

        public event EventHandler<ClientTansport<TType>> BeforeSend;
        public event EventHandler<ClientTansport<TType>> Complete;
        public event EventHandler<ClientTansport<TType>> Working;

        public IXMLHttpRequest Request;

        public ScriptException LastException;

        public static bool IsOnline
        {
            get
            {
                return Native.Document.location.IsHTTP;
            }
        }

        public static bool IsOffline
        {
            get
            {
                return !IsOnline;
            }
        }

        public TType Data
        {
            get
            {
                if (Descriptor == null)
                    return null;

                return Descriptor.Data;
            }
            set
            {
                if (Descriptor == null)
                    throw new ScriptException("no descriptor set");

                Descriptor.Data = value;
            }
        }



        public ClientTansport(string query):this("", query)
        {
        }

        public ClientTansport(string query, bool fileupload)
            : this("", query, fileupload)
        {
        }

        public Timer Worker = new Timer();

        public IHTMLForm Form;

        void Callback(string base64)
        {
            ResetCallbackFrame();

            ResponseText = Convert.FromBase64String(base64);

            GotResponse();
        }

        void ResetCallbackFrame()
        {
            Console.Log("resetting form callback iframe");

            ((IHTMLElement)Form.firstChild).innerHTML = "<iframe name='" + Descriptor.Callback + "'></iframe>";


        }

        IHTMLElement FormHiddenChild
        {
            get
            {
                return (IHTMLElement)Form.firstChild;
            }
        }

        IHTMLIFrame CallbackFrame
        {
            get
            {
                return (IHTMLIFrame)FormHiddenChild.firstChild;
            }
        }

        public ClientTansport(string url, string query, bool fileupload)
        {
            Url = url + "?" + query;
            Descriptor = new MyTransportDescriptor<TType>();
            Worker.Tick += new EventHandler<Timer>(Worker_Tick);

            if (fileupload)
            {
                IHTMLElement template = Native.Document.getElementById(Helper.FormTemplateID);

                if (template == null)
                    throw new ScriptException("form template not found");

                Form = (IHTMLForm)template.cloneNode(false);
                Form.action = Url;
                Form.appendChild(new IHTMLElement());
                FormHiddenChild.style.display = IStyle.DisplayEnum.none;

                Descriptor.Callback = Expando.GetUniqueID("callback");
                Expando.ExportCallback<string>(Descriptor.Callback, Callback);
                ResetCallbackFrame();

            }
            else
            {
                Request = new IXMLHttpRequest();
            }
        }

        public ClientTansport(string url, string query)
            : this(url, query, false)
        {

        }

        void Worker_Tick(Timer e)
        {
            Helper.Invoke(Working, this);
            //if (Working != null)
                //Working(this);
        }

        public string ToJSON()
        {
            return Expando.Of(Descriptor).ToJSON();

        }

        public string StatusString
        {
            get
            {
                return Request.statusText;
            }
        }


        public string ResponseText;

        public bool IsVerbose;

        double _timestart;

        public double TimeElapsed;

        public bool DemandHeader = true;

        public void Send()
        {


            _timestart = IDate.Now.getTime();
            TimeElapsed = 0;

            LastException = null;

            if (BeforeSend != null)
                BeforeSend(this);

            if (Descriptor.Description == null)
            {
                string err = "header not set";

                Console.LogError(err);

                if (DemandHeader)
                {
                    throw new ScriptException(err);
                }
            }


            string json = ToJSON();

            if (IsVerbose)
            {
                Console.Log(" => [" + Descriptor.Description + "] " + json.Length + " bytes");
            }

            Worker.StartInterval();



            if (IsVerbose)
            {
             
                Console.WriteLine("var data = " + json + ";");
                Console.Log(json.Length + " bytes sent");
            }

            if (Form == null)
            {

                Request.open(HTTPMethodEnum.POST, Url);
                Request.send(json);
                Request.InvokeOnComplete(
                    delegate
                    {
                        ResponseText = Request.responseText;

                        GotResponse();
                    });
            }
            else
            {
                IHTMLInput z = new IHTMLInput(HTMLInputTypeEnum.hidden);

                z.name = Helper.FormTemplateJSONField;
                z.value = Convert.ToBase64String( json );

                Form.appendChild(z);
                Form.target = Descriptor.Callback;
                Form.submit();

                z.Dispose();
            }
        }

        private void GotResponse()
        {
            TimeElapsed = IDate.Now.getTime() - _timestart;

            Worker.Stop();

            

            if (IsVerbose)
            {
                Console.Log(" <= [" + this.Descriptor.Description + "] " + TimeElapsed + " ms, " + this.ResponseText.Length + " bytes");
                Console.Log("json: " + this.ResponseText);
            }

            Descriptor = null;


            try
            {
                   Descriptor = Expando.FromJSONProtocolString(ResponseText).To<MyTransportDescriptor<TType>>();
                


            }
            catch (ScriptException exc)
            {
                LastException = exc;

                //if (IsVerbose)
                //{
                //    Console.LogError("unable to spawn from json, " + exc.Message);
                //    Console.LogError("stream -> " + ResponseText);
                //}
            }

            Helper.Invoke(Complete, this);

            //if (Complete != null)
            //    Complete(this);
        }







        public static void Send(string query, 
            EventHandler<ClientTansport<TType>> before)
 
        {
            ClientTansport<TType> c = new ClientTansport<TType>(query);

            if (before != null)
                c.BeforeSend += before;
            else
                Console.LogError("Send without before send handler");

            c.Send();
        }

        public void DisableButtonsWhileBusy(params IHTMLButton[] e)
        {
            // remember buttons to be removed from this list?

            this.BeforeSend +=
                delegate
                {
                    foreach (IHTMLButton v in e)
                    {
                        v.disabled = true;
                    }
                };

            this.Complete +=
                delegate
                {
                    foreach (IHTMLButton v in e)
                    {
                        v.disabled = false;
                    }
                };
        }

        public void ToConsole()
        {
            
        }

        public void AttachTo(IHTMLButton e)
        {
            this.DisableButtonsWhileBusy(e);

            e.onclick +=
               delegate
               {
                   this.Send();
               };            
        }
    }

}
