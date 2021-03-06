﻿using Devdeb.Network.TCP.Communication;
using Devdeb.Network.TCP.Rpc.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using IServiceProvider = Devdeb.DependencyInjection.IServiceProvider;

namespace Devdeb.Network.TCP.Rpc.Controllers
{
	public class ControllersRouter
	{
		private readonly Dictionary<int, IControllerHandler> _controllerHandlers;

		public ControllersRouter(IEnumerable<IControllerHandler> controllerHandlers)
		{
			if (controllerHandlers == null)
				throw new ArgumentNullException(nameof(controllerHandlers));

			IControllerHandler[] sortedControllerHandlers = controllerHandlers.OrderBy(x => x.GetType().GenericTypeArguments[0].FullName).ToArray();

			_controllerHandlers = new Dictionary<int, IControllerHandler>(sortedControllerHandlers.Length);
			for (int i = 0; i < sortedControllerHandlers.Length; i++)
				_controllerHandlers.Add(i, sortedControllerHandlers[i]);
		}

		public void RouteToController(
			IServiceProvider serviceProvider,
			TcpCommunication tcpCommunication,
			CommunicationMeta meta,
			byte[] buffer,
			int offset
		)
		{
			_controllerHandlers[meta.ControllerId].HandleRequest(serviceProvider, tcpCommunication, meta, buffer, offset);
		}
	}
}
