<%@ Application Language="C#" %>

<script RunAt="server">

    class DualConsole : System.IO.TextWriter, IDisposable
    {
        public System.IO.TextWriter Channel1;

        public System.IO.TextWriter Channel2;


        public override void Write(char value)
        {
            this.Channel1.Write(value);

            if (this.Channel2 == null)
                System.Diagnostics.Debug.Write(value);
            else
                this.Channel2.Write(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this.Channel1.Write(buffer, index, count);

            if (this.Channel2 == null)
                System.Diagnostics.Debug.Write(new string(buffer, index, count));
            else
                this.Channel2.Write(buffer, index, count);
        }

        public override Encoding Encoding
        {
            get { return Channel1.Encoding; }
        }

        public ScriptCoreLib.Shared.Action Disposing;



        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (Disposing != null)
                Disposing();
        }

        #endregion


        /// <summary>
        /// during using this instance the console output will be mirrored to a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DualConsole StreamToFile(System.IO.FileInfo file)
        {
            DualConsole dual = new DualConsole();
            
            dual.Channel1 = Console.Out;
            dual.Channel2 = new System.IO.StreamWriter(file.OpenWrite());

            Console.SetOut(dual);

            dual.Disposing =
                delegate
                {
                    dual.Channel2.Close();

                    Console.SetOut(dual.Channel1);
                };

            return dual;
        }

        public static DualConsole StreamToDiagnostics()
        {
            DualConsole dual = new DualConsole();
            
            dual.Channel1 = Console.Out;

            Console.SetOut(dual);

            dual.Disposing =
                delegate
                {
                    Console.SetOut(dual.Channel1);
                };

            return dual;
        }
    }

    void Application_Start(object sender, EventArgs e)
    {
        DualConsole.StreamToDiagnostics();

        Console.WriteLine("Application_Start");
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }



    protected void Application_BeginRequest(object sender, EventArgs e)
    {

        Console.WriteLine(this.Context.Request.AppRelativeCurrentExecutionFilePath);
        
        //if (System.Diagnostics.Debugger.IsAttached)
        //    throw new NotSupportedException();
        
        jsc.server.WebTools.VirtualRequest(this.Context);
        
        

    }
</script>

