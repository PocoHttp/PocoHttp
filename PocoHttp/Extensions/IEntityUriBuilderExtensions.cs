using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Routing
{
	public static class IEntityUriBuilderExtensions
	{
		public static Uri BuildUri<TEntity>(this IEntityUriBuilder uriBuilder, 
			bool usePluralUris)
		{
			return uriBuilder.BuildUri(typeof (TEntity), usePluralUris);
		}
	}
}
