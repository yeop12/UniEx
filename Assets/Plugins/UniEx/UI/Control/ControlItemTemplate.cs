namespace UniEx.UI
{
	public abstract class ControlItemTemplate<T> : ControlItem
	{
		public T Model { get; private set; }

		public sealed override void OnInit(object modelObject)
		{
			base.OnInit(modelObject);
			Model = (T)modelObject;
			OnInit(Model);
			InvokeUnbind();
			InvokeBind();
		}
		 
		protected virtual void OnInit(T model)
		{
		}

		protected virtual void OnDisable() 
		{
			InvokeUnbind();
		}
	}
}
