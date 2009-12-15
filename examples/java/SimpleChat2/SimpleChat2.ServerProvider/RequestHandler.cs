using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SimpleChat2.Network;

namespace SimpleChat2.ServerProvider
{
	public abstract partial class RequestHandler : Component
	{
		public RequestHandler()
		{
			InitializeComponent();
		}

		public RequestHandler(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}


		RequestDispatcher _Dispatcher;

		public RequestDispatcher Dispatcher
		{
			get
			{
				return _Dispatcher;
			}
			set
			{
				if (_Dispatcher != null)
				{
					_Dispatcher.Handlers.Remove(this);
				}
				_Dispatcher = value;
				if (_Dispatcher != null)
				{
					_Dispatcher.Handlers.Add(this);
				}
			}
		}

		public interface IResponse : IDefaultRequestPath
		{
			string Content { get; set; }

			Action InvokeAction { get; set; }

			void Invoke();
		}

		public abstract Type GetTargetType
		{
			get;
		}

		public virtual void RegisterInvokeAction(RequestHandler.IResponse e)
		{
		}
	}
}
