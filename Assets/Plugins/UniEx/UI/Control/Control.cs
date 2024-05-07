using UnityEngine;

namespace UniEx.UI
{
	public sealed class Control : MonoBehaviour
	{
		[SerializeField] private ControlItem _controlItem;

		public void SetModel(object model)
		{
			_controlItem.gameObject.SetActive(model is not null);
			if (model is not null)
			{
				_controlItem.OnInit(model);
			}
		}
	}
}
