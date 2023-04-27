using System.Threading;
using Cysharp.Threading.Tasks;

namespace UniEx.UI
{
	public abstract class FixedUIWindowTemplate<T> : FixedUIWindow
	{
		protected T Model { get; private set; }

		public sealed override async UniTask OpenAsync(object modelObject, CancellationToken ct)
		{
			await base.OpenAsync(modelObject, ct);
			Model = (T)modelObject;
			await OnOpen(Model, ct);
			InvokeBind();
		}

		protected virtual UniTask OnOpen(T model, CancellationToken ct) => UniTask.CompletedTask;
	}
}
