using System;
using TMPro;
using UniRx;

namespace UniEx.UniRx
{
	[Serializable]
	public class FontStylesReactiveProperty : ReactiveProperty<FontStyles>
	{
		public FontStylesReactiveProperty()
		{

		}

		public FontStylesReactiveProperty(FontStyles initialValue) : base(initialValue)
		{

		}
	}
}