using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using PocoHttp.Grammars;
using PocoHttp.Internal;
using PocoHttp.Internal.LinqHelper;

namespace PocoHttp
{
	public class PocoClient : IPocoClient
	{
		private readonly IPocoRuntime _pocoRuntime;
		private readonly HttpClient _httpClient;

		public PocoClient() : this(PocoRuntime.Current)
		{
			
		}

		public PocoClient(IPocoRuntime runtime)
		{
			_pocoRuntime = runtime;
			if (runtime.Handler == null)
				_httpClient = new HttpClient();
			else
				_httpClient = new HttpClient(runtime.Handler, runtime.DisposeHandler);
	
		}

		public IQueryable Context(Type entityType, Uri uri)
		{

			if(BaseAddress == null &&
				(uri == null || !uri.IsAbsoluteUri))
				throw new InvalidOperationException("BaseAddress is null and uri is null or relative. Set the base address or provide absolute uri.");

			var request = new HttpRequestMessage(HttpMethod.Get, uri);

			var httpProvider = new HttpProvider(
				new HttpQueryContext()
					{
						EntityType = entityType,
						HttpClient = _httpClient,
						Runtime = Runtime,
						Request = request
					}
				);

			return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(entityType), 
				new object[] { httpProvider });
			
		}


		public IQueryable Context(Type entityType)
		{
			return Context(entityType,
				Runtime.UriBuilder.BuildUri(entityType, Runtime.UsePluralUrls));
		}

		public Uri BaseAddress
		{
			get { return _httpClient.BaseAddress; }
			set { _httpClient.BaseAddress = value; }
		}

		public IPocoRuntime Runtime
		{
			get { return _pocoRuntime; }
		}

		public void Dispose()
		{
			if(_httpClient!=null)
				_httpClient.Dispose();		
		}
	}
}
