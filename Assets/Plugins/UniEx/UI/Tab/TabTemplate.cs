namespace UniEx.UI
{
	public abstract class TabTemplate<T> : Tab
	{
		public T Model { get; private set; }

		public override void OnInit(object modelObject)
		{
			base.OnInit(modelObject);
			Model = (T)modelObject;
			OnInit(Model);
			InvokeBind();
		}

		protected virtual void OnInit(T model)
		{
		}
	}
}
