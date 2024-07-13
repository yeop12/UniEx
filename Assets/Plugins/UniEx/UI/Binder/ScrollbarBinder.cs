using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Scrollbar))]
	public class ScrollbarBinder : SelectableBinder<Scrollbar>
	{
		[GetterBinderType(typeof(float))] [SerializeField] private string _valueParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _sizeParameterName;
		[GetterBinderType(typeof(int))] [SerializeField] private string _numberOfStepParameterName;
		[SetterBinderType(typeof(float))][SerializeField] private string _onValueChangedParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.onValueChanged);
			AddGetterParameter<float>(_sizeParameterName, x => UIComponent.size = x);
			AddGetterParameter<int>(_numberOfStepParameterName, x => UIComponent.numberOfSteps = x);
			AddGetterParameter<float>(_valueParameterName, x => UIComponent.value = x);
			AddGetterParameter<float>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
		}
	}
}
