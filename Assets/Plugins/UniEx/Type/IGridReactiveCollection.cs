using System;
using System.Collections.Generic;
using UniRx;

namespace UniEx
{
    public interface IGridReactiveCollection : IEnumerable<object>
    {
	    IObservable<object> ObserveAdd();
        IObservable<object> ObserveRemove();
        IObservable<Unit> ObserveReset();
    }
}
