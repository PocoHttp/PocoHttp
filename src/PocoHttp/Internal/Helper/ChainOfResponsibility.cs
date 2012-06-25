using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocoHttp.Internal.Helper
{
	internal class ChainOfResponsibility<T>
	{
		private List<ConditionalAction<T>> _chain = new List<ConditionalAction<T>>();

		private ChainOfResponsibility(ConditionalAction<T> conditionalAction)
		{
			_chain.Add(conditionalAction);
		}

		private class ConditionalAction<TInput>
		{
			public Predicate<TInput> If { get; set; }
			public Action<TInput> Do { get; set; }
		}

		public static ChainOfResponsibility<T> Start(Predicate<T> condition, Action<T> action)
		{
			return new ChainOfResponsibility<T>(new ConditionalAction<T>()
			{
				If = condition,
				Do = action
			});
		}

		public ChainOfResponsibility<T> Then(Predicate<T> condition, Action<T> action)
		{
			_chain.Add(new ConditionalAction<T>()
			{
				If = condition,
				Do = action
			});
			return this;
		}

		public void Run(T input)
		{
			foreach (var conditionalAction in _chain)
			{
				if (conditionalAction.If(input))
				{
					conditionalAction.Do(input);
					break;
				}
			}
		}

		public ChainOfResponsibility<T> Else(Action<T> action)
		{
			return Then((i) => true, action);
		}
	}
}
