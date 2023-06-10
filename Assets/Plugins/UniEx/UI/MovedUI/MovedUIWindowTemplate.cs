using UnityEngine;

namespace UniEx.UI
{
	public abstract class MovedUIWindowTemplate<T> : MovedUIWindow
	{
		protected T Model { get; private set; }

		public sealed override void Open(object modelObject, Transform target)
		{
			base.Open(modelObject, target);
			Model = (T)modelObject;
			OnInit(Model);
			InvokeBind();
		}

		protected virtual void OnInit(T model)
		{

		}
	}
}
