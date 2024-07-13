using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Slider))]
	public class SliderBinder : SelectableBinder<Slider>
	{
		[GetterBinderType(typeof(float))] [SerializeField] private string _valueParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _minValueParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _maxValueParameterName;
		[GetterBinderType(typeof(bool))] [SerializeField] private string _wholeNumbersParameterName;
		[SetterBinderType(typeof(float))][SerializeField] private string _onValueChangedParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddSetterParameter(_onValueChangedParameterName, UIComponent.onValueChanged);
			AddGetterParameter<bool>(_wholeNumbersParameterName, x => UIComponent.wholeNumbers = x);
			AddGetterParameter<float>(_minValueParameterName, x => UIComponent.minValue = x);
			AddGetterParameter<float>(_maxValueParameterName, x => UIComponent.maxValue = x);
			AddGetterParameter<float>(_valueParameterName, x => UIComponent.value = x);
			AddGetterParameter<float>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
		}
	}
}
