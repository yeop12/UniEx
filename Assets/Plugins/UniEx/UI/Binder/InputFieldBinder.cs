using TMPro;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(TMP_InputField))]
	public class InputFieldBinder : SelectableBinder<TMP_InputField>
	{
		[BinderType(typeof(string))] [SerializeField] private string _textParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<string>(_textParameterName, x => UIComponent.text = x);
		}
	}
}
