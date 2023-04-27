using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniEx.UI
{
	public abstract class MovedUIWindowTemplate<T> : MovedUIWindow
	{
		protected T Model { get; private set; }

		public sealed override async UniTask OpenAsync(object modelObject, Transform target, CancellationToken ct)
		{
			await base.OpenAsync(modelObject, target, ct);
			Model = (T)modelObject;
			await OnInit(Model, ct);
			InvokeBind();
		}

		protected virtual UniTask OnInit(T model, CancellationToken ct) => UniTask.CompletedTask;
	}
}
