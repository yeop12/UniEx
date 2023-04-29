using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UniEx.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class FixedUIWindow : UIElement
	{
		[SerializeField] private RenderMode _renderMode;

		[Inject] protected FixedUIManager UIManager { get; private set; }

		protected object ModelObject { get; private set; }

		public Canvas Canvas { get; private set; }
		public bool IsOpened { get; private set; }
		public event Action OnClosed;

		protected virtual void Awake()
		{
			Canvas = GetComponent<Canvas>();
			if (_renderMode != RenderMode.ScreenSpaceOverlay)
			{
				Canvas.worldCamera = Camera.main;
				Canvas.planeDistance = 10.0f;
			}
		}

		public virtual UniTask OpenAsync(object modelObject, CancellationToken ct)
		{
			gameObject.SetActive(true);
			ModelObject = modelObject;
			IsOpened = true;
			return UniTask.CompletedTask;
		}

		public virtual void Close(bool force = false)
		{
			if (force is false && IsOpened is false)
			{
				Debug.LogError($"{name} is already closed.");
				return;
			}

			InvokeUnbind();
			gameObject.SetActive(false);
			IsOpened = false;
			OnClosed?.Invoke();
		}
	}
}
