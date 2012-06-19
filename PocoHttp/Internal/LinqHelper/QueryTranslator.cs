using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PocoHttp.Internal.LinqHelper
{
	internal class QueryTranslator : ExpressionVisitor
	{
		private StringBuilder sb;

		internal QueryTranslator()
		{
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
			if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
			{
				sb.Append("SELECT * FROM (");
				this.Visit(m.Arguments[0]);
				sb.Append(") AS T WHERE ");
				LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
				this.Visit(lambda.Body);
				return m;
			}
			throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
		}

		protected override Expression VisitUnary(UnaryExpression u)
		{
			switch (u.NodeType)
			{
				case ExpressionType.Not:
					sb.Append(" NOT ");
					this.Visit(u.Operand);
					break;
				default:
					throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
			}
			return u;
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			sb.Append("(");
			this.Visit(b.Left);
			switch (b.NodeType)
			{
				case ExpressionType.And:
					sb.Append(" AND ");
					break;
				case ExpressionType.Or:
					sb.Append(" OR");
					break;
				case ExpressionType.Equal:
					sb.Append(" eq ");
					break;
				case ExpressionType.NotEqual:
					sb.Append(" ne ");
					break;
				case ExpressionType.LessThan:
					sb.Append(" lt ");
					break;
				case ExpressionType.LessThanOrEqual:
					sb.Append(" le ");
					break;
				case ExpressionType.GreaterThan:
					sb.Append(" gt ");
					break;
				case ExpressionType.GreaterThanOrEqual:
					sb.Append(" ge ");
					break;
				default:
					throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
			}
			this.Visit(b.Right);
			sb.Append(")");
			return b;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			IQueryable q = c.Value as IQueryable;
			if (q != null)
			{
				// assume constant nodes w/ IQueryables are table references
				sb.Append("SELECT * FROM ");
				sb.Append(q.ElementType.Name);
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
