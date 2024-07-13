using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Tab))]
	public class TabBinder : UIBinder<Tab>
	{
		[GetterBinderType(typeof(object))] [SerializeField] private string _modelParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<object>(_modelParameterName, x => UIComponent.OnInit(x));
		}
	}
}
