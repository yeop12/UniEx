using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleBinder : SelectableBinder<Toggle>
	{
		[GetterBinderType(typeof(bool))] [SerializeField] private string _isOnParameterName;
		[GetterBinderType(typeof(bool))] [SerializeField] private string _isOnWithoutNotifyParameterName;
		[SetterBinderType(typeof(bool))][SerializeField] private string _onValueChangedParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.onValueChanged);
			AddGetterParameter<bool>(_isOnParameterName, x => UIComponent.isOn = x);
			AddGetterParameter<bool>(_isOnWithoutNotifyParameterName, x => UIComponent.SetIsOnWithoutNotify(x));
		}
	}
}
