using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Scrollbar))]
	public class ScrollbarBinder : SelectableBinder<Scrollbar>
	{
		[BinderType(typeof(float))] [SerializeField] private string _valueParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _sizeParameterName;
		[BinderType(typeof(int))] [SerializeField] private string _numberOfStepParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<float>(_valueParameterName, x => UIComponent.value = x);
			AddParameter<float>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
			AddParameter<float>(_sizeParameterName, x => UIComponent.size = x);
			AddParameter<int>(_numberOfStepParameterName, x => UIComponent.numberOfSteps = x);
		}
	}
}
