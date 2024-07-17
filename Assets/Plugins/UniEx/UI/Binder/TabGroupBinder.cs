using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TabGroup))]
	public class TabGroupBinder : UIBinder<TabGroup>
	{
		[GetterBinderType(typeof(IEnumerable<(Enum, object)>))][SerializeField] private string _tabInfosParameterName;
		[GetterBinderType(typeof(Enum))] [SerializeField] private string _selectedKindParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<IEnumerable<(Enum, object)>>(_tabInfosParameterName, x => UIComponent.TabInfos = x);
			AddGetterParameter<Enum>(_selectedKindParameterName, x => UIComponent.SelectedKind = x);
		}
	}
}
