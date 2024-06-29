using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Image))]
	public class ImageBinder : MaskableGraphicBinder<Image>
	{
		[BinderType(typeof(Sprite))] [SerializeField] private string _spriteParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _fillAmountParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<Sprite>(_spriteParameterName, x => UIComponent.sprite = x);
			AddParameter<float>(_fillAmountParameterName, x => UIComponent.fillAmount = x);
		}
	}
}