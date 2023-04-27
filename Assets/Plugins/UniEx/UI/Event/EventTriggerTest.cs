using UnityEngine;
using UnityEngine.EventSystems;

namespace UniEx.UI
{
	public class EventTriggerTest : MonoBehaviour
	{
		public void OnPointerEnter(BaseEventData data)
		{
			Debug.LogError("OnPointerEnter");
		}

		public void OnPointerExit(BaseEventData data)
		{
			Debug.LogError("OnPointerExit");
		}

		public void OnPointerDown(BaseEventData data)
		{
			Debug.LogError("OnPointerDown");
		}

		public void OnPointerClick(BaseEventData data)
		{
			Debug.LogError("OnPointerClick");
		}

		public void OnPointerUp(BaseEventData data)
		{
			Debug.LogError("OnPointerUp");
		}

		public void OnBeginDrag(BaseEventData data)
		{
			Debug.LogError("OnBeginDrag");
		}

		public void OnDrag(BaseEventData data)
		{
			Debug.LogError("OnDrag");
		}

		public void OnEndDrag(BaseEventData data)
		{
			Debug.LogError("OnEndDrag");
		}

		public void OnDrop(BaseEventData data)
		{
			Debug.LogError("OnDrop");
		}

		public void OnMove(BaseEventData data)
		{
			Debug.LogError("OnMove");
		}

		public void OnSelect(BaseEventData data)
		{
			Debug.LogError("OnSelect");
		}

		public void OnDeselect(BaseEventData data)
		{
			Debug.LogError("OnDeselect");
		}
	}
}
