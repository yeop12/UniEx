using System;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(ToggleTabConnecter))]
	public class ToggleTabConnecterBinder : UIBinder<ToggleTabConnecter>
	{
		[GetterBinderType(typeof(Enum))][SerializeField] private string _tabKindParameterName;
		[SetterBinderType(typeof(Enum))][SerializeField] private string _onValueChangedParameterName;

		protected override void Awake() 
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.OnValueChanged);
			AddGetterParameter<Enum>(_tabKindParameterName, x => UIComponent.TabKind = x);
		}
	}
}
