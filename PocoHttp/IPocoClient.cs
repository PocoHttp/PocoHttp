using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp
{
	public interface IPocoClient : IDisposable
	{
		/// <summary>
		/// Creates a Queryable context similar to other ORM context
		/// </summary>
		/// <param name="entityType">type of the entity</param>
		/// <returns></returns>
		IQueryable Context(Type entityType);

		/// <summary>
		/// Creates a Queryable context similar to other ORM context
		/// </summary>
		/// <param name="entityType">type of the entity</param>
		/// <param name="uri">Uri to be used instead of the one based on conventions and type name.
		/// If it is relative and a base URI is provided, they will be appended
		/// </param>
		/// <returns></returns>
		IQueryable Context(Type entityType, Uri uri);

		/// <summary>
		/// Base address of all HTTP calls.
		/// If null, Uri needs to be provided to the Context call or
		/// </summary>
		Uri BaseAddress { get; set; }
	}
}
