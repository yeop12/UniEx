using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace UniEx
{
	public class SelectGridReactiveCollection<TInput, TOutput> : IGridReactiveCollection
	{
		private readonly IGridReactiveCollection _reactiveCollection;
		private readonly Func<TInput, TOutput> _selector;
		
		public SelectGridReactiveCollection( IGridReactiveCollection reactiveCollection, Func<TInput, TOutput> selector)
		{
			_reactiveCollection = reactiveCollection;
			_selector = selector;
		}

		public IEnumerator<object> GetEnumerator() => _reactiveCollection.Cast<TInput>()
			.Select(x => _selector.Invoke(x)).Cast<object>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IObservable<object> ObserveAdd() =>
			_reactiveCollection.ObserveAdd().Select(x => (object)_selector.Invoke((TInput)x));

		public IObservable<object> ObserveRemove() =>
			_reactiveCollection.ObserveRemove().Select(x => (object)_selector.Invoke((TInput)x));

		public IObservable<Unit> ObserveReset() => _reactiveCollection.ObserveReset();
	}
}
