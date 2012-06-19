using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

// from http://blogs.msdn.com/b/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx
namespace PocoHttp.Internal.LinqHelper
{
	internal abstract class QueryProvider : IQueryProvider
	{
		protected QueryProvider()
		{
		}

		IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
		{
			return new Query<T>(this, expression);
		}

		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			Type elementType = TypeSystem.GetElementType(expression.Type);
			try
			{
				return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType), new object[] { this, expression });
			}
			catch (TargetInvocationException tie)
			{
				throw tie.InnerException;
			}
		}

		T IQueryProvider.Execute<T>(Expression expression)
		{
			return (T)this.Execute(expression);
		}

		object IQueryProvider.Execute(Expression expression)
		{
			return this.Execute(expression);
		}

		public abstract string GetQueryText(Expression expression);
		public abstract object Execute(Expression expression);
	}
}
