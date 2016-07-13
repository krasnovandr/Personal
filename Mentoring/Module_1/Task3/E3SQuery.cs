using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Task3
{
	class E3SQuery<T> : IQueryable<T>
	{
		private Expression expression;
		private E3SLinqProvider provider;

		public E3SQuery(Expression expression, E3SLinqProvider provider)
		{
			this.expression = expression;
			this.provider = provider;
		}

		public Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		public Expression Expression
		{
			get
			{
				return expression;
			}
		}

		public IQueryProvider Provider
		{
			get
			{
				return provider;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return provider.Execute<IEnumerable<T>>(expression).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return provider.Execute<IEnumerable>(expression).GetEnumerator();
		}
	}
}
