using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(FixedGrid))]
	public class GridBinder : UIBinder<FixedGrid>
	{
		[BinderType(typeof(IEnumerable), false)] 
		[SerializeField] private string _parameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter(_parameterName, OnBindModel);
		}

		private void OnBindModel(object value, string parameterName)
		{
			switch (value)
			{
				case IGridReactiveCollection gridReactiveCollection:
					AddObserver(gridReactiveCollection.ObserveAdd().Subscribe(OnAddModel));
					AddObserver(gridReactiveCollection.ObserveRemove().Subscribe(OnRemoveModel));
					AddObserver(gridReactiveCollection.ObserveReset().Subscribe(OnResetModels));
					UIComponent.Init(gridReactiveCollection);
					break;

				case IEnumerable list:
					UIComponent.Init(list);
					break;

				default:
					Debug.LogError($"Parameter's type must be '{nameof(IGridReactiveCollection)}' or '{nameof(IEnumerable)}'.(Object name : {name}, Parameter name : {parameterName})");
					break;
			}
		}

		private void OnAddModel(object value)
		{
			UIComponent.AddModel(value);
		}

		private void OnRemoveModel(object value)
		{
			UIComponent.RemoveModel(value);
		}

		private void OnResetModels(Unit unit)
		{
			UIComponent.Clear();
		}
	}
}
