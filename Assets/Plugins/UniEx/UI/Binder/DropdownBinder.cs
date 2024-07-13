using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class DropdownBinder : SelectableBinder<TMP_Dropdown>
	{
		[GetterBinderType(typeof(int))] [SerializeField] private string _valueParameterName;
		[GetterBinderType(typeof(int))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[GetterBinderType(typeof(IEnumerable<TMP_Dropdown.OptionData>), false)] [SerializeField] private string _optionsParameterName;
		[SetterBinderType(typeof(int))] [SerializeField] private string _onValueChangedParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.onValueChanged);
			AddGetterParameter<IEnumerable<TMP_Dropdown.OptionData>>(_optionsParameterName, x => UIComponent.options = x.ToList());
			AddGetterParameter<int>(_valueParameterName, x => UIComponent.value = x);
			AddGetterParameter<int>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
		}
	}
}
