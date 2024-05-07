using System;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Control))]
	public sealed class ControlBinder : MonoBehaviour
	{
		[SerializeField] private UIElement _uiElement;
		[SerializeField] private string _parameterName;

		private Control _control;
		private PropertyInfo _controlProperty;
		private readonly List<IDisposable> _observers = new();

		private void Awake()
		{
			_control = GetComponent<Control>();
			if (_uiElement is null) 
			{
				Debug.LogError($"{name} does not registered {nameof(_uiElement)}.");
				return;
			}

			var propertyInfo = _uiElement.GetType().GetProperty(_parameterName);
			if (propertyInfo is null)
			{
				Debug.LogError($"{_uiElement.name} does not contain '{_parameterName}' variable.");
				return;
			}

			_controlProperty = propertyInfo;
			_uiElement.OnBind += OnBind;
			_uiElement.OnUnbind += OnUnbind;
		}

		private void OnBind()
		{
			var controlInfo = _controlProperty.GetValue(_uiElement);
			switch (controlInfo)
			{
				case IObservable<object> observable:
					_observers.Add(observable.Subscribe(_control.SetModel));
					break;

				default:
					Debug.LogError($"Parameter's type must be '{typeof(IObservable<object>).Name}'.(UIElement name : {_uiElement.name}, Name : {name}, Parameter name : {_parameterName})");
					break;
			}
		}

		private void OnUnbind()
		{
			_observers.ForEach(x => x.Dispose());
			_observers.Clear();
		}
	}
}
