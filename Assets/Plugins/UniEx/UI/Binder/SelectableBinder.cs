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
	}
}
