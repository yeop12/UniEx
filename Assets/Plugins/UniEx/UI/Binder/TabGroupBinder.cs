using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TabGroup))]
	public class TabGroupBinder : UIBinder<TabGroup>
	{
		[GetterBinderType(typeof(bool))] [SerializeField] private string _allowSwitchOffParameterName;
		[GetterBinderType(typeof(int))] [SerializeField] private string _selectedIndexParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<bool>(_allowSwitchOffParameterName, x => UIComponent.allowSwitchOff = x);
			AddGetterParameter<int>(_selectedIndexParameterName, x => UIComponent.SelectedIndex = x);
		}
	}
}
