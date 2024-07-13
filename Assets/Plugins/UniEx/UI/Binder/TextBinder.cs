using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextBinder : MaskableGraphicBinder<TextMeshProUGUI>
	{
		[GetterBinderType(typeof(string))] [SerializeField] private string _textParameterName;
		[GetterBinderType(typeof(float))] [SerializeField] private string _fontSizeParameterName;
		[GetterBinderType(typeof(FontStyles))] [SerializeField] private string _fontStyleParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<string>(_textParameterName, x => UIComponent.text = x);
			AddGetterParameter<float>(_fontSizeParameterName, x => UIComponent.fontSize = x);
			AddGetterParameter<FontStyles>(_fontStyleParameterName, x => UIComponent.fontStyle = x);
		}
	}
}
