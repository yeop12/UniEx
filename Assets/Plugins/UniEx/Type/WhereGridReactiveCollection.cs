using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace UniEx
{
	public class WhereGridReactiveCollection<T> : IGridReactiveCollection
	{
		private readonly IGridReactiveCollection _reactiveCollection;
		private readonly Predicate<T> _predicate;

		public WhereGridReactiveCollection(IGridReactiveCollection reactiveCollection, Predicate<T> predicate)
		{
			_reactiveCollection = reactiveCollection;
			_predicate = predicate;
		}

		public IEnumerator<object> GetEnumerator() => _reactiveCollection.Cast<T>().Where(x => _predicate.Invoke(x))
			.Cast<object>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IObservable<object> ObserveAdd() =>
			_reactiveCollection.ObserveAdd().Where(x => _predicate.Invoke((T)x));

		public IObservable<object> ObserveRemove() =>
			_reactiveCollection.ObserveRemove().Where(x => _predicate.Invoke((T)x));

		public IObservable<Unit> ObserveReset() => _reactiveCollection.ObserveReset();
	}
}
