using System;
using System.Collections;
using UniRx;

namespace UniEx
{
	public class ReadOnlyReactiveCollectionWrapper<T> : IGridReactiveCollection
	{
		private readonly IReadOnlyReactiveCollection<T> _reactiveCollection;

		public ReadOnlyReactiveCollectionWrapper(IReadOnlyReactiveCollection<T> reactiveCollection)
		{
			_reactiveCollection = reactiveCollection;
		}

		public IObservable<object> ObserveAdd() => _reactiveCollection.ObserveAdd()
			.Select(x => x.Value as object);
		
		public IObservable<object> ObserveRemove() => _reactiveCollection.ObserveRemove()
			.Select(x => x.Value as object);
		
		public IObservable<Unit> ObserveReset() => _reactiveCollection.ObserveReset();

		public IEnumerator GetEnumerator() => _reactiveCollection.GetEnumerator();
	}
}