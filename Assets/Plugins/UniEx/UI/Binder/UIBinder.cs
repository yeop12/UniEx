using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

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

		protected void AddGetterParameter<T>(string parameterName, Action<T> action)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				OnBind(value, parameterName, action);
			};
		}

		protected void AddGetterParameter(string parameterName, Action<object, string> binder)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}
				binder?.Invoke(value, parameterName);
			};
		}

		protected void AddFunctionParameter(string parameterName, Action eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T>(string parameterName, Action<T> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2>(string parameterName, Action<T1, T2> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2, T3>(string parameterName, Action<T1, T2, T3> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2, T3> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2, T3>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2, T3, T4>(string parameterName, Action<T1, T2, T3, T4> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2, T3, T4> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2, T3, T4>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2, T3, T4, T5>(string parameterName, Action<T1, T2, T3, T4, T5> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2, T3, T4, T5> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2, T3, T4, T5>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2, T3, T4, T5, T6>(string parameterName, Action<T1, T2, T3, T4, T5, T6> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2, T3, T4, T5, T6> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2, T3, T4, T5, T6>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}
		
		protected void AddFunctionParameter<T1, T2, T3, T4, T5, T6, T7>(string parameterName, Action<T1, T2, T3, T4, T5, T6, T7> eventFunc)
		{
			_uiElement.OnBind += () =>
			{
				var value = FindValue(parameterName);
				if (value is null)
				{
					return;
				}

				if (value is not ActionWrapper<T1, T2, T3, T4, T5, T6, T7> invokeFunc)
				{
					Debug.LogError($"Parameter's type must be '{nameof(Action<T1, T2, T3, T4, T5, T6, T7>)}'.(Object name : {name}, Parameter name : {parameterName})");
					return;
				}

				invokeFunc.AddEvent(eventFunc);
				AddObserver(new DisposableAction(() =>
				{
					invokeFunc.RemoveEvent(eventFunc);
				}));
			};
		}

		protected object FindValue(string parameterName)
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
				var propertyInfo = value.GetType().GetProperty(parameter, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
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

		protected void AddSetterParameter(string parameterName, UnityEvent setterEvent)
		{
			if (string.IsNullOrWhiteSpace(parameterName))
			{
				return;
			}

			if (_uiElement == null)
			{
				return;
			}

			if (setterEvent is null)
			{
				Debug.LogError($"Setter event is null.(Type : {GetType().Name})");
				return;
			}

			var isFunction = parameterName.EndsWith("()");
			if (isFunction is false)
			{
				Debug.LogError(
					$"Binding function name must be end with '()'.(Object name : {name}, Parameter name : {parameterName})");
				return;
			}

			UnityAction onSetValue = null;

			_uiElement.OnBind += () =>
			{
				var parameters = parameterName.Split('/', StringSplitOptions.RemoveEmptyEntries);
				object value = _uiElement;

				foreach (var (parameter, index) in parameters.Select((x, i) => (x, i)))
				{
					var isLast = index == parameters.Length - 1;
					if (isLast is false)
					{
						var propertyInfo = value.GetType().GetProperty(parameter, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
						if (propertyInfo is null)
						{
							Debug.LogError(
								$"{_uiElement.name} does not contain '{parameter}' variable.(Object name : {name})");
							return;
						}

						value = propertyInfo.GetValue(value);
						if (value is null)
						{
							Debug.LogError($"{name} object's parameter target is null.(Parameter name : {parameter})");
							return;
						}
					}
					else
					{
						var methodInfo = value.GetType().GetMethod(parameter[..^2]);
						if (methodInfo is null)
						{
							Debug.LogError(
								$"{_uiElement.name} does not contain '{parameter}' function.(Object name : {name})");
							return;
						}

						onSetValue = () => { methodInfo.Invoke(value, new object[] { }); };
					}
				}

				setterEvent.AddListener(onSetValue);
			};

			AddObserver(new DisposableAction(() =>
			{
				if (onSetValue is not null)
				{
					setterEvent.RemoveListener(onSetValue);
				}
			}));
		}

		protected void AddSetterParameter<T>(string parameterName, UnityEvent<T> setterEvent)
		{
			if (string.IsNullOrWhiteSpace(parameterName))
			{
				return;
			}

			if (_uiElement == null)
			{
				return;
			}

			if (setterEvent is null)
			{
				Debug.LogError($"Setter event is null.(Type : {GetType().Name})");
				return;
			}

			UnityAction<T> onSetValue = null;

			_uiElement.OnBind += () =>
			{
				var isFunction = parameterName.EndsWith("()");
				var parameters = parameterName.Split('/', StringSplitOptions.RemoveEmptyEntries);
				object value = _uiElement;

				foreach (var (parameter, index) in parameters.Select((x, i) => (x, i)))
				{
					var isLast = index == parameters.Length - 1;
					if (isLast is false)
					{
						var propertyInfo = value.GetType().GetProperty(parameter, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
						if (propertyInfo is null)
						{
							Debug.LogError($"{_uiElement.name} does not contain '{parameter}' variable.(Object name : {name})");
							return;
						}

						value = propertyInfo.GetValue(value);
						if (value is null)
						{
							Debug.LogError($"{name} object's parameter target is null.(Parameter name : {parameter})");
							return;
						}
					}
					else
					{
						if (isFunction)
						{
							var methodInfo = value.GetType().GetMethod(parameter[..^2]);
							if (methodInfo is null)
							{
								Debug.LogError(
									$"{_uiElement.name} does not contain '{parameter}' function.(Object name : {name})");
								return;
							}

							onSetValue = x => { methodInfo.Invoke(value, new object[] { x }); };
						}
						else
						{
							var propertyInfo = value.GetType().GetProperty(parameter);
							if (propertyInfo is null)
							{
								Debug.LogError(
									$"{_uiElement.name} does not contain '{parameter}' variable.(Object name : {name})");
								return;
							}

							onSetValue = x => { propertyInfo.SetValue(value, x); };
						}
					}
				}

				setterEvent.AddListener(onSetValue);
			};

			AddObserver(new DisposableAction(() =>
			{
				if (onSetValue is not null)
				{
					setterEvent.RemoveListener(onSetValue);
				}
			}));
		}
	}
}
