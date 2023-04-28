using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_InputField))]
	public class InputFieldBinder : UIBinder<TMP_InputField>
	{
		[SerializeField] private string _textParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<string>(_textParameterName, x => UIComponent.text = x);
		}
	}
}
