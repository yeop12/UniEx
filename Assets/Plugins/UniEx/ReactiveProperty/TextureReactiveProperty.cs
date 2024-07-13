using System;
using UniRx;
using UnityEngine;

namespace UniEx.UniRx
{
	[Serializable]
	public class TextureReactiveProperty : ReactiveProperty<Texture>
	{
		public TextureReactiveProperty()
		{

		}

		public TextureReactiveProperty(Texture initialValue) : base(initialValue)
		{

		}
	}
}
