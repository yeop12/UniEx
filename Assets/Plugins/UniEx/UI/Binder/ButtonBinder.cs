using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Button))]
	public class ButtonBinder : SelectableBinder<Button>
	{
		[SetterBinderType(typeof(void))] [SerializeField] private string _onClickParameterType;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onClickParameterType, UIComponent.onClick);
		}
	}
}
