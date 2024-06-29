using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TabGroup))]
	public class TabGroupBinder : UIBinder<TabGroup>
	{
		[BinderType(typeof(bool))] [SerializeField] private string _allowSwitchOffParameterName;
		[BinderType(typeof(int))] [SerializeField] private string _selectedIndexParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<bool>(_allowSwitchOffParameterName, x => UIComponent.allowSwitchOff = x);
			AddParameter<int>(_selectedIndexParameterName, x => UIComponent.SelectedIndex = x);
		}
	}
}
