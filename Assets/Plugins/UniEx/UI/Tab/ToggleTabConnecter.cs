using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleTabConnecter : MonoBehaviour
	{
		[SerializeField] private string _enumType;
		[SerializeField] private int _enumValue = -1;
		[SerializeField] private UnityEvent<Enum> _onValueChanged;

		private Enum _enum;

		public Enum TabKind
		{
			set
			{
				if (_toggle != null)
				{
					_toggle.SetIsOnWithoutNotify(Equals(_enum, value));
				}
			}
		}

		public UnityEvent<Enum> OnValueChanged => _onValueChanged;

		private Toggle _toggle;

		private void Awake()
		{
			_toggle = GetComponent<Toggle>();
			var enumType = AppDomain.CurrentDomain.GetAssemblies()
				.FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp")?.GetType(_enumType);
			if (enumType != null)
			{
				_enum = _enumValue < 0 ? null : Enum.GetValues(enumType).GetValue(_enumValue) as Enum;
			}
		}

		private void OnEnable()
		{
			_toggle.onValueChanged.AddListener(OnToggleValueChanged);
		}

		private void OnDisable()
		{
			_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}

		private void OnToggleValueChanged(bool value)
		{
			_onValueChanged?.Invoke(value ? _enum : null);
		}
	}
}
