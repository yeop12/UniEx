using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace UniEx.UI
{
	public abstract class UIBinder<TUIComponent> : MonoBehaviour
	{
		[SerializeField] private UIElement _uiElement;

		private readonly List<IDisposable> _observers = new();
		protected TUIComponent UIComponent { get; private set; }

		protected virtual void Awake()
		{
			UIComponent = FindUIComponent();
			_uiElement.OnUnbind += OnUnbind;
		}

		protected virtual TUIComponent FindUIComponent()
		{
			return GetComponent<TUIComponent>();
		}

		protected void AddParameter<T>(string parameterName, Action<T> action)
		{
			if (string.IsNullOrWhiteSpace(parameterName))
			{
				return;
			}

			if (_uiElement is null)
			{
				Debug.LogError($"{name} does not registered {nameof(_uiElement)}.");
				return;
			}

			var propertyInfo = _uiElement.GetType().GetProperty(parameterName);
			if (propertyInfo is null)
			{
				Debug.LogError($"{_uiElement.name} does not contain '{parameterName}' variable.(Object name : {name})");
				return;
			}

			_uiElement.OnBind += OnBind;
			
			void OnBind()
			{
				var value = propertyInfo.GetValue(_uiElement);
				switch (value)
				{
					case T rawValue:
						action.Invoke(rawValue);
						break;

					case IObservable<T> observableValue:
						_observers.Add(observableValue.Subscribe(action));
						break;

					default:
						Debug.LogError($"Parameter's type must be '{typeof(T).Name}' or 'IObservable<{typeof(T).Name}>'.(Object name : {name}, Parameter name : {parameterName})");
						break;
				}
			}
		}

		private void OnUnbind()
		{
			_observers.ForEach(x => x.Dispose());
			_observers.Clear();
		}
	}
}
