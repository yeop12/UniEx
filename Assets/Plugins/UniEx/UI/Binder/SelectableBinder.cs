using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	public abstract class SelectableBinder<T> : UIBinder<T> where T : Selectable
	{
		[BinderType(typeof(bool))] [SerializeField] protected string _interactableParameterName;
		
		protected override void Awake()
		{
			base.Awake();
			AddParameter<bool>(_interactableParameterName, x => UIComponent.interactable = x);
		}
	}
}
