using System;
using System.Collections;
using UniRx;

namespace UniEx
{
    public interface IGridReactiveCollection : IEnumerable
    {
	    IObservable<object> ObserveAdd();
        IObservable<object> ObserveRemove();
        IObservable<Unit> ObserveReset();
    }
}
