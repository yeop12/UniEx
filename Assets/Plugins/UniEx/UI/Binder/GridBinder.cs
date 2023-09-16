using System;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(FixedGrid))]
	public class GridBinder : MonoBehaviour
	{
		[SerializeField] private UIElement _uiElement;
		[SerializeField] private string _paramterName;

		private FixedGrid _grid;
		private PropertyInfo _gridInfosProperty;
		private readonly List<IDisposable> _observers = new();

		private void Awake()
		{
			_grid = GetComponent<FixedGrid>();
			if (_uiElement is null) 
			{
				Debug.LogError($"{name} does not registered {nameof(_uiElement)}.");
				return;
			}

			var propertyInfo = _uiElement.GetType().GetProperty(_paramterName);
			if (propertyInfo is null)
			{
				Debug.LogError($"{_uiElement.name} does not contain '{_paramterName}' variable.");
				return;
			}

			_gridInfosProperty = propertyInfo;
			_uiElement.OnBind += OnBind;
			_uiElement.OnUnbind += OnUnbind;
		}

		private void OnBind()
		{
			var gridInfos = _gridInfosProperty.GetValue(_uiElement);
			switch (gridInfos)
			{
				case IGridReactiveCollection gridReactiveCollection:
					_observers.Add(gridReactiveCollection.ObserveAdd().Subscribe(OnAddModel));
					_observers.Add(gridReactiveCollection.ObserveRemove().Subscribe(OnRemoveModel));
					_observers.Add(gridReactiveCollection.ObserveReset().Subscribe(OnResetModels));
					_grid.Init(gridReactiveCollection);
					break;

				case IEnumerable<object> list:
					_grid.Init(list);
					break;

				default:
					Debug.LogError($"Parameter's type must be '{typeof(IReadOnlyReactiveProperty<>).Name}' or '{typeof(IEnumerable<object>).Name}'.(UIElement name : {_uiElement.name}, Name : {name}, Parameter name : {_paramterName})");
					break;
			}
		}

		private void OnUnbind()
		{
			_observers.ForEach(x => x.Dispose());
			_observers.Clear();
		}

		private void OnAddModel(object value)
		{
			_grid.AddModel(value);
		}

		private void OnRemoveModel( object value )
		{
			_grid.RemoveModel(value);
		}

		private void OnResetModels(Unit unit)
		{
			_grid.Clear();
		}
	}
}
