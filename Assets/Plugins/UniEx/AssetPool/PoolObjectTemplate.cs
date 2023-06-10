namespace UniEx
{
	public class PoolObjectTemplate<T> : PoolObject
	{
		protected T Model { get; private set; }

		protected internal sealed override void OnGet(object modelObject)
		{
			base.OnGet(modelObject);
			Model = (T)modelObject;
			OnGet(Model);
		}

		protected virtual void OnGet(T model)
		{

		}
	}
}
