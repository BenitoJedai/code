using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.net
{
    // http://livedocs.adobe.com/flex/3/langref/flash/net/NetConnection.html
    [Script(IsNative=true)]
    public class NetConnection : EventDispatcher
    {
        #region Properties
        /// <summary>
        /// Indicates the object on which callback methods should be invoked.
        /// </summary>
        public object client { get; set; }

        /// <summary>
        /// [read-only] Indicates whether the application is connected to a server through a persistent RTMP connection (true) or not (false).
        /// </summary>
        public bool connected { get; private set; }

        /// <summary>
        /// [read-only] If a successful connection is made, indicates the method that was used to make it: a direct connection, the CONNECT method, or HTTP tunneling.
        /// </summary>
        public string connectedProxyType { get; private set; }

        /// <summary>
        /// [static] The default object encoding for NetConnection objects.
        /// </summary>
        public static uint defaultObjectEncoding { get; set; }

        /// <summary>
        /// The object encoding for this NetConnection instance.
        /// </summary>
        public uint objectEncoding { get; set; }

        /// <summary>
        /// Determines which fallback methods are tried if an initial connection attempt to the server fails.
        /// </summary>
        public string proxyType { get; set; }

        /// <summary>
        /// [read-only] The URI passed to the NetConnection.connect() method.
        /// </summary>
        public string uri { get; private set; }

        /// <summary>
        /// [read-only] Indicates whether a secure connection was made using native Transport Layer Security (TLS) rather than HTTPS.
        /// </summary>
        public bool usingTLS { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a context header to the Action Message Format (AMF) packet structure.
        /// </summary>
        public void addHeader(string operation, bool mustUnderstand, object param)
        {
        }

        /// <summary>
        /// Adds a context header to the Action Message Format (AMF) packet structure.
        /// </summary>
        public void addHeader(string operation, bool mustUnderstand)
        {
        }

        /// <summary>
        /// Adds a context header to the Action Message Format (AMF) packet structure.
        /// </summary>
        public void addHeader(string operation)
        {
        }

        /// <summary>
        /// Invokes a command or method on Flash Media Server or on an application server running Flash Remoting.
        /// </summary>
        public void call(string command, Responder responder, /* params */ object arguments)
        {
        }

        /// <summary>
        /// Invokes a command or method on Flash Media Server or on an application server running Flash Remoting.
        /// </summary>
        public void call(string command, Responder responder)
        {
        }

        /// <summary>
        /// Closes the connection that was opened locally or to the server and dispatches a netStatus event with a code property of NetConnection.Connect.Closed.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Creates a bidirectional connection between a Flash Player or AIR an AIR application and a Flash Media Server application.
        /// </summary>
        public void connect(string command, /* params */ object arguments)
        {
        }

        /// <summary>
        /// Creates a bidirectional connection between a Flash Player or AIR an AIR application and a Flash Media Server application.
        /// </summary>
        public void connect(string command)
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a NetConnection object.
        /// </summary>
        public NetConnection()
        {
        }

        #endregion

        #region Events
        /// <summary>
        /// Dispatched when an exception is thrown asynchronously — that is, from native asynchronous code.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<AsyncErrorEvent> asyncError;

        /// <summary>
        /// Dispatched when an input or output error occurs that causes a network operation to fail.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<IOErrorEvent> ioError;

        /// <summary>
        /// Dispatched when a NetConnection object is reporting its status or error condition.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<NetStatusEvent> netStatus;

        /// <summary>
        /// Dispatched if a call to NetConnection.call() attempts to connect to a server outside the caller's security sandbox.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<SecurityErrorEvent> securityError;

        #endregion

      
    }
}
