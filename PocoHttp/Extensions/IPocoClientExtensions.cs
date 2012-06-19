using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp
{
	public static class IPocoClientExtensions
	{
		public static IQueryable<TEntity> Context<TEntity>(this IPocoClient pocoClient)
		{
			return (IQueryable<TEntity>) pocoClient.Context(typeof(TEntity));
		}

		public static IQueryable<TEntity> Context<TEntity>(this IPocoClient pocoClient, Uri uri)
		{
			return (IQueryable<TEntity>) pocoClient.Context(typeof(TEntity), uri);
		}
		
	}

}
