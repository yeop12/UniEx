using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextBinder : MaskableGraphicBinder<TextMeshProUGUI>
	{
		[BinderType(typeof(string))] [SerializeField] private string _textParameterName;
		[BinderType(typeof(float))] [SerializeField] private string _fontSizeParameterName;
		[BinderType(typeof(FontStyles))] [SerializeField] private string _fontStyleParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<string>(_textParameterName, x => UIComponent.text = x);
			AddParameter<float>(_fontSizeParameterName, x => UIComponent.fontSize = x);
			AddParameter<FontStyles>(_fontStyleParameterName, x => UIComponent.fontStyle = x);
		}
	}
}
