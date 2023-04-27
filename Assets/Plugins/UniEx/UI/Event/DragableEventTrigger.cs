using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UniEx.UI
{
	public class DragableEventTrigger : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		[SerializeField] private UnityEvent<PointerEventData> _onStartDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onEndDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onClick;

		private RectTransform _rectTransform;
		private bool _isSelecting;

		private void Awake()
		{
			_rectTransform = GetComponent<RectTransform>();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isSelecting is false)
			{
				var screenPoint = RectTransformUtility.WorldToScreenPoint(null, eventData.position);
				if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPoint) is false)
				{
					return;
				}

				_isSelecting = true;
				_onStartDrag.Invoke(eventData);
			}

			_onDrag.Invoke(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_isSelecting is false)
			{
				return;
			}

			_onEndDrag.Invoke(eventData);
			_isSelecting = false;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_onClick.Invoke(eventData);
		}
	}
}
