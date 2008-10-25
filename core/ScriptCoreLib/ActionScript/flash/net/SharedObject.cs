using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.flash.net
{
	// http://livedocs.adobe.com/flex/3/langref/flash/net/SharedObject.html
	[Script(IsNative = true)]
	public class SharedObject : EventDispatcher
	{
		#region Properties
		/// <summary>
		/// Indicates the object on which callback methods are invoked.
		/// </summary>
		public object client { get; set; }

		/// <summary>
		/// [read-only] The collection of attributes assigned to the data property of the object; these attributes can be shared and stored.
		/// </summary>
		public object data { get; private set; }

		/// <summary>
		/// [static] The default object encoding (AMF version) for all local shared objects created in the SWF file.
		/// </summary>
		public static uint defaultObjectEncoding { get; set; }

		/// <summary>
		/// [write-only] Specifies the number of times per second that a client's changes to a shared object are sent to the server.
		/// </summary>
		public double fps { get; set; }

		/// <summary>
		/// The object encoding (AMF version) for this shared object.
		/// </summary>
		public uint objectEncoding { get; set; }

		/// <summary>
		/// [read-only] The current size of the shared object, in bytes.
		/// </summary>
		public uint size { get; private set; }

		#endregion


		#region Methods
		/// <summary>
		/// For local shared objects, purges all of the data and deletes the shared object from the disk.
		/// </summary>
		public void clear()
		{
		}

		/// <summary>
		/// Immediately writes a locally persistent shared object to a local file.
		/// </summary>
		public string flush(int minDiskSpace)
		{
			return default(string);
		}

		/// <summary>
		/// Immediately writes a locally persistent shared object to a local file.
		/// </summary>
		public string flush()
		{
			return default(string);
		}

		/// <summary>
		/// [static] Returns a reference to a locally persistent shared object that is only available to the current client.
		/// </summary>
		public static SharedObject getLocal(string name, string localPath, bool secure)
		{
			return default(SharedObject);
		}

		/// <summary>
		/// [static] Returns a reference to a locally persistent shared object that is only available to the current client.
		/// </summary>
		public static SharedObject getLocal(string name, string localPath)
		{
			return default(SharedObject);
		}

		/// <summary>
		/// [static] Returns a reference to a locally persistent shared object that is only available to the current client.
		/// </summary>
		public static SharedObject getLocal(string name)
		{
			return default(SharedObject);
		}

		#endregion


		#region Methods
		/// <summary>
		/// Indicates to the server that the value of a property in the shared object has changed.
		/// </summary>
		public void setDirty(string propertyName)
		{
		}

		/// <summary>
		/// Updates the value of a property in a shared object and indicates to the server that the value of the property has changed.
		/// </summary>
		public void setProperty(string propertyName, object value)
		{
		}

		/// <summary>
		/// Updates the value of a property in a shared object and indicates to the server that the value of the property has changed.
		/// </summary>
		public void setProperty(string propertyName)
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
		/// Dispatched when a SharedObject instance is reporting its status or error condition.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<NetStatusEvent> netStatus;

		/// <summary>
		/// Dispatched when a remote shared object has been updated by the server.
		/// </summary>
		[method: Script(NotImplementedHere = true)]
		public event Action<SyncEvent> sync;

		#endregion

	
	}
}
