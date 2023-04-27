using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Slider))]
	public class SliderBinder : UIBinder<Slider>
	{
		[SerializeField] private string _valueParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<float>(_valueParameterName, x => UIComponent.value = x);
		}
	}
}
