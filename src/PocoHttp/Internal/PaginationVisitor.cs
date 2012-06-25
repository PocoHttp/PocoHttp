using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PocoHttp.Grammars;
using PocoHttp.Internal.Helper;
using PocoHttp.Internal.LinqHelper;

namespace PocoHttp.Internal
{
	internal class PaginationVisitor : ExpressionVisitor
	{
		private StringBuilder sb;
		private PaginationVocabulary _vocabulary;

		internal PaginationVisitor() : this(new PaginationVocabulary())
		{
		
		}

		internal PaginationVisitor(PaginationVocabulary vocabulary)
		{
			_vocabulary = vocabulary;
		}

		internal string Translate(Expression expression)
		{
			this.sb = new StringBuilder();
			expression = Evaluator.PartialEval(expression);
			this.Visit(expression);
			return this.sb.ToString();
		}

		private static Expression StripQuotes(Expression e)
		{
			while (e.NodeType == ExpressionType.Quote)
			{
				e = ((UnaryExpression)e).Operand;
			}
			return e;
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			if(m.Method.DeclaringType == typeof(Queryable))
			{
				ChainOfResponsibility<MethodCallExpression>
					.Start(m1 => m1.Method.Name == "Take", HandleTake)
					.Then(m1 => m1.Method.Name == "Skip", HandleSkip)
					.Then(m1 => m1.Method.Name == "OrderBy", HandleOrderBy)
					.Then(m1 => m1.Method.Name == "OrderByDescending", HandleOrderByDescending)
					.Else(m1 =>
					      	{
					      		throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
					      	})
							.Run(m);
			}
			else
			{				
				throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));				
			}				

			return m;
		}

		private void HandleTake(MethodCallExpression m)
		{
			this.Visit(m.Arguments[0]);
			AddAndIfNeeded();
			sb.Append(_vocabulary.Count).Append("=");
			this.Visit(m.Arguments[1]);
		}

		private void HandleSkip(MethodCallExpression m)
		{
			this.Visit(m.Arguments[0]);
			AddAndIfNeeded();
			sb.Append(_vocabulary.Skip).Append("=");
			this.Visit(m.Arguments[1]);
		}

		private void HandleOrderBy(MethodCallExpression m)
		{
			this.Visit(m.Arguments[0]);
			AddAndIfNeeded();
			sb.Append(_vocabulary.OrderBy).Append("=");
			LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
			this.Visit(lambda.Body);
		}

		private void HandleOrderByDescending(MethodCallExpression m)
		{
			this.Visit(m.Arguments[0]);
			AddAndIfNeeded();
			sb.Append(_vocabulary.OrderByDescending).Append("=");
			LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
			this.Visit(lambda.Body);
			sb.Append(" DESC");

		}


		private void AddAndIfNeeded()
		{
			if (sb.Length > 0)
				sb.Append("&");
		}

		protected override Expression VisitUnary(UnaryExpression u)
		{
			throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			IQueryable q = c.Value as IQueryable;
			if (q != null)
			{
				//throw new NotSupportedException("Constant from IQueryable not supported."); // TODO: lool at this scenario
			}
			else if (c.Value == null)
			{
				sb.Append("NULL");
			}
			else
			{
				switch (Type.GetTypeCode(c.Value.GetType()))
				{
					case TypeCode.Boolean:
						sb.Append(((bool)c.Value) ? 1 : 0);
						break;
					case TypeCode.String:
						sb.Append("'");
						sb.Append(c.Value);
						sb.Append("'");
						break;
					case TypeCode.Object:
						throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
					default:
						sb.Append(c.Value);
						break;
				}
			}
			return c;
		}

		protected override Expression VisitMember(MemberExpression m)
		{
			if (m.Expression == null)
				throw new ArgumentNullException("m.Expression");

			if (m.Expression.NodeType == ExpressionType.Parameter)
			{
				sb.Append(m.Member.Name);
				return m;
			}
			else if (m.Expression.NodeType == ExpressionType.Constant)
			{
				// sb.Append(((ConstantExpression) m.Expression).Value);
				var value = ((ConstantExpression) m.Expression).Value;
				Visit(m.Expression);
				return m;
			}
			throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
		}

	}
}
