using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	public class MaskableGraphicBinder<T> : GraphicBinder<T> where T : MaskableGraphic
	{
		[GetterBinderType(typeof(bool))] [SerializeField] protected string _maskableParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<bool>(_maskableParameterName, x => UIComponent.maskable = x);
		}
	}
}
