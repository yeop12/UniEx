using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Tab))]
	public class TabBinder : UIBinder<Tab>
	{
		[BinderType(typeof(object))] [SerializeField] private string _modelParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<object>(_modelParameterName, x => UIComponent.OnInit(x));
		}
	}
}
