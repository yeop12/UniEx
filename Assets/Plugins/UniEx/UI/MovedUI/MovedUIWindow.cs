using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UniEx.UI
{
	public abstract class MovedUIWindow : UIElement
	{
		[Inject(Id = "MovedUICanvasRectTransform")] private RectTransform _canvasRectTransform;
		[Inject(Id = "FieldCamera")] private Camera _fieldCamera;

		private Transform _targetTransform;
		private RectTransform _rectTransform;

		public bool IsOpened { get; private set; }

		public event Action OnClosed;

		protected virtual void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public virtual UniTask OpenAsync(object modelObject, Transform targetTransform, CancellationToken ct)
		{
			if (IsOpened)
			{
				Debug.LogError($"Hud object is already opened.(Name : {name})");
				return UniTask.CompletedTask;
			}

			_targetTransform = targetTransform;
			gameObject.SetActive(true);
			IsOpened = true;
			return UniTask.CompletedTask;
		}

		public virtual void Close(bool force = false)
		{
			if (force is false && IsOpened is false)
			{
				Debug.LogError($"Hud object is already closed.(Name : {name})");
				return;
			}

			gameObject.SetActive(false);
			IsOpened = false;
			OnClosed?.Invoke();
		}

		protected virtual void LateUpdate()
		{
			var screenPoint = _fieldCamera.WorldToScreenPoint(_targetTransform.position);
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, screenPoint, null, out var localPoint))
			{
				_rectTransform.localPosition = localPoint;
			}
		}
	}
}
