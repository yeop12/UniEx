namespace UniEx.UI
{
	public abstract class GridItemTemplate<T> : GridItem
	{
		public T Model { get; private set; }

		public sealed override void OnInit(object modelObject)
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
