namespace UniEx
{
	public class PoolObjectTemplate<T> : PoolObject
	{
		public T Model { get; private set; }

		protected internal sealed override void OnGet(object modelObject)
		{
			base.OnGet(modelObject);
			Model = (T)modelObject;
			OnGetImpl(Model);
		}

		protected virtual void OnGetImpl(T model)
		{

		}
	}
}
