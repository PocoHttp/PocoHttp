using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using PocoHttp.Grammars;
using PocoHttp.Routing;

namespace PocoHttp
{
	public class PocoConfiguration
	{

		public PocoConfiguration()
		{
			Grammar = new ODataGrammar();
			UsePluralUrls = true;
			UriBuilder = new EntityUriBuilder();
			RequestSetup = (request) => {};
			DisposeHandler = true;
			CustomFormatters = new MediaTypeFormatter[0];
			ResponseReader = (response, formatters, type) =>
			                 	{
									return response.Content.ReadAsAsync(
										typeof(IEnumerable<>).MakeGenericType(type),
										formatters);
			                 	};
		}

		/// <summary>
		/// Http Data grammar.
		/// </summary>
		public IHttpDataGrammar Grammar {get; set; }

		/// <summary>
		/// Whether use for example http://myserver/api/car or http://myserver/api/cars
		/// to retrieve Car objects
		/// </summary>
		public bool UsePluralUrls { get; set; }

		public IEntityUriBuilder UriBuilder { get; set; }

		/// <summary>
		/// And extensibility point for extra setup required
		/// </summary>
		public Action<HttpRequestMessage> RequestSetup { get; set; }

		/// <summary>
		/// Handler to be used in HttpClient
		/// </summary>
		public HttpMessageHandler Handler { get; set; }

		/// <summary>
		/// Whether handler should be disposed with HttpClieny
		/// </summary>
		public bool DisposeHandler { get; set; }

		/// <summary>
		/// If is set, it will be used for media type formatting and content negotiation.
		/// </summary>
		public IEnumerable<MediaTypeFormatter> CustomFormatters { get; set; }

		/// <summary>
		/// The function responsibe for reading the response content
		/// and returning entities
		/// </summary>
		public Func<HttpResponseMessage, IEnumerable<MediaTypeFormatter>, Type, Task<object>> ResponseReader { get; set; }

		
	}
}
