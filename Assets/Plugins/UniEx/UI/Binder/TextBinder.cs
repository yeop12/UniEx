using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TextBinder : UIBinder<TextMeshProUGUI>
	{
		[SerializeField] private string _textParameterName;
		[SerializeField] private string _colorParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<string>(_textParameterName, x => UIComponent.text = x);
			AddParameter<Color>(_colorParameterName, x => UIComponent.color = x);
		}
	}
}
