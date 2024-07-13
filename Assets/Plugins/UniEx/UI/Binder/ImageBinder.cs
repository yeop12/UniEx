using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Image))]
	public class ImageBinder : MaskableGraphicBinder<Image>
	{
		[GetterBinderType(typeof(Sprite))] [SerializeField] private string _spriteParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _fillAmountParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<Sprite>(_spriteParameterName, x => UIComponent.sprite = x);
			AddGetterParameter<float>(_fillAmountParameterName, x => UIComponent.fillAmount = x);
		}
	}
}