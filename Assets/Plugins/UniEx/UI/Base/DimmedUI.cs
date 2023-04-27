using UnityEngine;
using UnityEngine.EventSystems;

namespace UniEx.UI
{
	public class DimmedUI : MonoBehaviour, IPointerClickHandler
	{
		private FixedUIWindow _window;

		private void Awake()
		{
			_window = GetComponentInParent<FixedUIWindow>();
			if (_window is null)
			{
				Debug.LogError($"The dimmed ui should be positioned below the FixedUIWindow.");
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_window?.Close();
		}
	}
}
