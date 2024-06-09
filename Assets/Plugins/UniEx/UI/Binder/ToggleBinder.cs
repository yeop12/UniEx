using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleBinder : SelectableBinder<Toggle>
	{
		[SerializeField] private string _isOnParameterName;
		[SerializeField] private string _isOnWithoutNotifyParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<bool>(_isOnParameterName, x => UIComponent.isOn = x);
			AddParameter<bool>(_isOnWithoutNotifyParameterName, x => UIComponent.SetIsOnWithoutNotify(x));
		}
	}
}
