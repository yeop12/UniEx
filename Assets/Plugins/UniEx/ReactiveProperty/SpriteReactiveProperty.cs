using System;
using UniRx;
using UnityEngine;

namespace UniEx.UniRx
{
	[Serializable]
	public class SpriteReactiveProperty : ReactiveProperty<Sprite>
	{
		public SpriteReactiveProperty()
		{

		}

		public SpriteReactiveProperty(Sprite initialValue) : base(initialValue)
		{

		}
	}
}
