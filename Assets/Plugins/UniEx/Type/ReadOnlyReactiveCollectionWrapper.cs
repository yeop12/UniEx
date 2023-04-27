using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace UniEx
{
	public class ReadOnlyReactiveCollectionWrapper<T> : IReadOnlyReactiveCollection<object>
	{
		private readonly IReadOnlyReactiveCollection<T> _reactiveCollection;

		public ReadOnlyReactiveCollectionWrapper(IReadOnlyReactiveCollection<T> reactiveCollection)
		{
			_reactiveCollection = reactiveCollection;
		}

		public IEnumerator<object> GetEnumerator() => _reactiveCollection.Cast<object>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IObservable<CollectionAddEvent<object>> ObserveAdd() => _reactiveCollection.ObserveAdd()
			.Select(x => new CollectionAddEvent<object>(x.Index, x.Value));

		public IObservable<int> ObserveCountChanged(bool notifyCurrentCount = false) =>
			_reactiveCollection.ObserveCountChanged();

		public IObservable<CollectionMoveEvent<object>> ObserveMove() => _reactiveCollection.ObserveMove()
			.Select(x => new CollectionMoveEvent<object>(x.OldIndex, x.NewIndex, x.Value));

		public IObservable<CollectionRemoveEvent<object>> ObserveRemove() => _reactiveCollection.ObserveRemove()
			.Select(x => new CollectionRemoveEvent<object>(x.Index, x.Value));

		public IObservable<CollectionReplaceEvent<object>> ObserveReplace() => _reactiveCollection.ObserveReplace()
			.Select(x => new CollectionReplaceEvent<object>(x.Index, x.OldValue, x.NewValue));

		public IObservable<Unit> ObserveReset() => _reactiveCollection.ObserveReset();

		public int Count => _reactiveCollection.Count;

		public object this[int index] => _reactiveCollection[index];
	}
}