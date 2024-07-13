using System;
using UniRx;
using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Control))]
	public sealed class ControlBinder : UIBinder<Control>
	{
		[GetterBinderType(typeof(IObservable<object>), false)] [SerializeField] private string _parameterName;

		protected override void Awake() 
		{
			base.Awake();
			AddGetterParameter(_parameterName, OnBindModel);
		}
		
		private void OnBindModel(object value, string parameterName)
		{
			switch (value)
			{
				case IObservable<object> observable:
					AddObserver(observable.Subscribe(UIComponent.SetModel));
					break;

				default:
					Debug.LogError($"Parameter's type must be '{typeof(IObservable<object>).Name}'.(Object name : {name}, Parameter name : {parameterName})");
					break;
			}
		}
	}
}
