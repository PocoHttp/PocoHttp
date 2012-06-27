using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

// from http://blogs.msdn.com/b/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx
namespace PocoHttp.Internal.LinqHelper
{
	internal class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, 
		IOrderedQueryable<T>, IOrderedQueryable
	{
		private QueryProvider _provider;
		private Expression _expression;

		public Query(QueryProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
			this._expression = Expression.Constant(this);
		}

		public Query(QueryProvider provider, Expression expression)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentOutOfRangeException("expression");
			}
			this._provider = provider;
			this._expression = expression;
		}

		Expression IQueryable.Expression
		{
			get { return this._expression; }
		}

		Type IQueryable.ElementType
		{
			get { return typeof(T); }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return this._provider; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)this._provider.Execute(this._expression)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._provider.Execute(this._expression)).GetEnumerator();
		}

		public override string ToString()
		{
			return this._provider.GetQueryText(this._expression);
		}
	}
}
