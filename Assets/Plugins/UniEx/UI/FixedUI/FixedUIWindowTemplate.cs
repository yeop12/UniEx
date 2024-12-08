namespace UniEx.UI
{
	public abstract class FixedUIWindowTemplate<T> : FixedUIWindow
	{
		protected T Model { get; private set; }

		public sealed override void Open(object modelObject)
		{
			base.Open(modelObject);
			Model = (T)modelObject;
			OnOpen(Model);
			InvokeBind();
			OnPostBind();
		}

		protected virtual void OnOpen(T model)
		{

		}

		protected virtual void OnPostBind()
		{

		}
	}
}
