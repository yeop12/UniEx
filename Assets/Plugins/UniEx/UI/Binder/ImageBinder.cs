using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Image))]
	public class ImageBinder : UIBinder<Image>
	{
		[SerializeField] private string _spriteParameterName;
		[SerializeField] private string _colorParameterName;
		[SerializeField] private string _fillAmountParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<Sprite>(_spriteParameterName, x => UIComponent.sprite = x);
			AddParameter<Color>(_colorParameterName, x => UIComponent.color = x);
			AddParameter<float>(_fillAmountParameterName, x => UIComponent.fillAmount = x);
		}
	}
}