using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_Dropdown))]
	public class DropdownBinder : SelectableBinder<TMP_Dropdown>
	{
		[SerializeField] private string _valueParameterName;
		[SerializeField] private string _valueWithoutNotifyParameterName;
		[SerializeField] private string _optionsParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<int>(_valueParameterName, x => UIComponent.value = x);
			AddParameter<int>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
			AddParameter<List<TMP_Dropdown.OptionData>>(_optionsParameterName, x => UIComponent.options = x);
		}
	}
}
