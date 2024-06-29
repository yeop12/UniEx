using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Slider))]
	public class SliderBinder : SelectableBinder<Slider>
	{
		[BinderType(typeof(float))] [SerializeField] private string _valueParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _valueWithoutNotifyParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _minValueParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _maxValueParameterName;
		[BinderType(typeof(bool))] [SerializeField] private string _wholeNumbersParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<float>(_valueParameterName, x => UIComponent.value = x);
			AddParameter<float>(_valueWithoutNotifyParameterName, x => UIComponent.SetValueWithoutNotify(x));
			AddParameter<float>(_minValueParameterName, x => UIComponent.minValue = x);
			AddParameter<float>(_maxValueParameterName, x => UIComponent.maxValue = x);
			AddParameter<bool>(_wholeNumbersParameterName, x => UIComponent.wholeNumbers = x);
		}
	}
}
