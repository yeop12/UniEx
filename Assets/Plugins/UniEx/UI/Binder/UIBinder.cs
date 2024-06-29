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
			if (_uiElement is null)
			{
				Debug.LogError($"{name} does not registered {nameof(_uiElement)}.");
				return;
			}
			_uiElement.OnUnbind += OnUnbind;
		}

		protected virtual void Reset()
		{
			_uiElement = GetComponentInParent<UIElement>(true);
		}

		protected virtual TUIComponent FindUIComponent()
		{
			return GetComponent<TUIComponent>();
		}

		protected void AddParameter<T>(string parameterName, Action<T> action)
		{
			var value = FindValue(parameterName);
			if (value is null)
			{
				return;
			}

			_uiElement.OnBind += () => OnBind(value, parameterName, action);
		}

		protected void AddParameter(string parameterName, Action<object, string> binder)
		{
			var value = FindValue(parameterName);
			if (value is null)
			{
				return;
			}

			_uiElement.OnBind += () => binder?.Invoke(value, parameterName);
		}

		private object FindValue(string parameterName )
		{
			if (string.IsNullOrWhiteSpace(parameterName))
			{
				return null;
			}

			if (_uiElement is null)
			{
				Debug.LogError($"{name} does not registered {nameof(_uiElement)}.");
				return null;
			}

			var parameters = parameterName.Split('/', StringSplitOptions.RemoveEmptyEntries);
			object value = _uiElement;

			foreach (var parameter in parameters)
			{
				var propertyInfo = value.GetType().GetProperty(parameter);
				if (propertyInfo is null)
				{
					Debug.LogError($"{_uiElement.name} does not contain '{parameter}' variable.(Object name : {name})");
					return null;
				}

				value = propertyInfo.GetValue(value);
				if (value is null)
				{
					Debug.LogError($"{name} object's parameter target is null.(Parameter name : {parameter})");
					return null;
				}
			}

			return value;
		}

		private void OnBind<T>(object value, string parameterName, Action<T> action)
		{
			switch (value)
			{
				case T rawValue:
					action.Invoke(rawValue);
					break;

				case IObservable<T> observableValue:
					_observers.Add(observableValue.Subscribe(action));
					break;

				default:
					Debug.LogError($"Parameter's type must be '{typeof(T).Name}' or 'IObservable<{typeof(IObservable<T>).Name}>'.(Object name : {name}, Parameter name : {parameterName})");
					break;
			}
		}

		protected void AddObserver(IDisposable observer)
		{
			_observers.Add(observer);
		}

		private void OnUnbind()
		{
			_observers.ForEach(x => x.Dispose());
			_observers.Clear();
		}
	}
}
