using System;
using UniRx;

namespace UniEx.UniRx
{
	[Serializable]
	public class SampleTabKindReactiveProperty : ReactiveProperty<SampleTabKind>
	{
		public SampleTabKindReactiveProperty() 
		{

		}

		public SampleTabKindReactiveProperty( SampleTabKind initialValue ) : base(initialValue) 
		{

		}
	}
}
