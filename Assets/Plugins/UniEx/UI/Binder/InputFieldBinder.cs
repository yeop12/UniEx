using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_InputField))]
	public class InputFieldBinder : SelectableBinder<TMP_InputField>
	{
		[GetterBinderType(typeof(string))] [SerializeField] private string _textParameterName;
		[GetterBinderType(typeof(string))] [SerializeField] private string _textWithoutNotifyParameterName;
		[SetterBinderType(typeof(string))] [SerializeField] private string _onValueChangedParameterName;
		[SetterBinderType(typeof(string))] [SerializeField] private string _onEndEditParameterName;
		[SetterBinderType(typeof(string))] [SerializeField] private string _onSelectParameterName;
		[SetterBinderType(typeof(string))] [SerializeField] private string _onDeselectParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.onValueChanged);
			AddSetterParameter(_onEndEditParameterName, UIComponent.onEndEdit);
			AddSetterParameter(_onSelectParameterName, UIComponent.onSelect);
			AddSetterParameter(_onDeselectParameterName, UIComponent.onDeselect);
			AddGetterParameter<string>(_textParameterName, x => UIComponent.text = x);
			AddGetterParameter<string>(_textWithoutNotifyParameterName, x => UIComponent.SetTextWithoutNotify(x));
		}
	}
}
