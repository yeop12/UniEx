using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UniEx.UI
{
	public class ScrollableEventTrigger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		[SerializeField] private ScrollRect _scrollRect;
		[SerializeField] private UnityEvent<PointerEventData> _onStartDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onEndDrag;
		[SerializeField] private UnityEvent<PointerEventData> _onClick;

		private RectTransform _rectTransform;
		private bool _isDragging;
		private bool _isSelecting;

		private void Awake()
		{
			_scrollRect = GetComponentInParent<ScrollRect>();
			_rectTransform = GetComponent<RectTransform>();
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_scrollRect.OnBeginDrag(eventData);
			_isDragging = true;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (_isDragging)
			{
				_scrollRect.OnDrag(eventData);
				var screenPoint = RectTransformUtility.WorldToScreenPoint(null, transform.position);
				screenPoint.y = eventData.position.y;
				if (RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPoint) is false)
				{
					_scrollRect.OnEndDrag(eventData);
					_isDragging = false;
					_onStartDrag?.Invoke(eventData);
					_isSelecting = true;
				}
			}

			if (_isSelecting)
			{
				_onDrag?.Invoke(eventData);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (_isDragging)
			{
				_scrollRect.OnEndDrag(eventData);
				_isDragging = false;
			}

			if (_isSelecting)
			{
				_onEndDrag?.Invoke(eventData);
				_isSelecting = false;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_onClick?.Invoke(eventData);
		}
	}
}