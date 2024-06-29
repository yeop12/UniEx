using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class DropdownBinder : SelectableBinder<TMP_Dropdown>
	{
		[BinderType(typeof(List<TMP_Dropdown.OptionData>))] [SerializeField] private string _valueParameterName;
		[BinderType(typeof(int))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[BinderType(typeof(int))] [SerializeField] private string _optionsParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<List<TMP_Dropdown.OptionData>>(_optionsParameterName, x => UIComponent.options = x);
			AddParameter<int>(_valueParameterName, x => UIComponent.value = x);
			AddParameter<int>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
		}
	}
}
