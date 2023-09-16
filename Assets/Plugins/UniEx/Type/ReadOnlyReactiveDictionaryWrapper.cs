using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace UniEx
{
	public class ReadOnlyReactiveDictionaryWrapper<TKey, TValue> : IGridReactiveCollection
	{
		private readonly IReadOnlyReactiveDictionary<TKey, TValue> _reactiveDictionary;

		public ReadOnlyReactiveDictionaryWrapper(IReadOnlyReactiveDictionary<TKey, TValue> reactiveDictionary)
		{
			_reactiveDictionary = reactiveDictionary;
		}

		public IEnumerator<object> GetEnumerator() =>
			_reactiveDictionary.Select(x => x.Value).Cast<object>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IObservable<object> ObserveAdd() => _reactiveDictionary.ObserveAdd().Select(x => x.Value as object);

		public IObservable<object> ObserveRemove() =>
			_reactiveDictionary.ObserveRemove().Select(x => x.Value as object);

		public IObservable<Unit> ObserveReset() => _reactiveDictionary.ObserveReset();
	}
}
