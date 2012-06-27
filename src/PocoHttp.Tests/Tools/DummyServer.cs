using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PocoHttp.Tests.Tools
{
	public class DummyServer : HttpMessageHandler
	{
		private HttpResponseMessage _message;

		public DummyServer()
		{
			
		}

		public DummyServer(HttpResponseMessage message)
		{
			_message = message;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var task = new TaskCompletionSource<HttpResponseMessage>();

			if(_message == null)
			{
				_message = request.CreateResponse(HttpStatusCode.OK);
			}
			else
			{
				_message.RequestMessage = request;
			}
			task.SetResult(_message);

			return task.Task;
		}

	}
}
