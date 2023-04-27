using System;
using UnityEngine;

namespace UniEx.UI
{
	public abstract class UIElement : MonoBehaviour
	{
		public event Action OnBind;
		public event Action OnUnbind;

		protected void InvokeBind()
		{
			OnBind?.Invoke();
		}

		protected void InvokeUnbind()
		{
			OnUnbind?.Invoke();
		}
	}
}
