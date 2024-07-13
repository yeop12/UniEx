using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniEx.UI
{
	public abstract class SelectableBinder<T> : UIBinder<T> where T : Selectable
	{
		[GetterBinderType(typeof(bool))] [SerializeField] protected string _interactableParameterName;
		
		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<bool>(_interactableParameterName, x => UIComponent.interactable = x);
		}
		
		protected void AddSetterParameter(string parameterName, UnityEvent setterEvent)
		{
			if (string.IsNullOrWhiteSpace(parameterName))
			{
				return;
			}

			if (UIElement == null)
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

			UIElement.OnBind += () =>
			{
				var parameters = parameterName.Split('/', StringSplitOptions.RemoveEmptyEntries);
				object value = UIElement;

				foreach (var (parameter, index) in parameters.Select((x, i) => (x, i)))
				{
					var isLast = index == parameters.Length - 1;
					if (isLast is false)
					{
						var propertyInfo = value.GetType().GetProperty(parameter, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
						if (propertyInfo is null)
						{
							Debug.LogError(
								$"{UIElement.name} does not contain '{parameter}' variable.(Object name : {name})");
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
								$"{UIElement.name} does not contain '{parameter}' function.(Object name : {name})");
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

			if (UIElement == null)
			{
				return;
			}

			if (setterEvent is null)
			{
				Debug.LogError($"Setter event is null.(Type : {GetType().Name})");
				return;
			}

			UnityAction<T> onSetValue = null;

			UIElement.OnBind += () =>
			{
				var isFunction = parameterName.EndsWith("()");
				var parameters = parameterName.Split('/', StringSplitOptions.RemoveEmptyEntries);
				object value = UIElement;

				foreach (var (parameter, index) in parameters.Select((x, i) => (x, i)))
				{
					var isLast = index == parameters.Length - 1;
					if (isLast is false)
					{
						var propertyInfo = value.GetType().GetProperty(parameter, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
						if (propertyInfo is null)
						{
							Debug.LogError($"{UIElement.name} does not contain '{parameter}' variable.(Object name : {name})");
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
									$"{UIElement.name} does not contain '{parameter}' function.(Object name : {name})");
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
									$"{UIElement.name} does not contain '{parameter}' variable.(Object name : {name})");
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
