using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	public abstract class GraphicBinder<T> : UIBinder<T> where T : Graphic
	{
		[GetterBinderType(typeof(bool))] [SerializeField] protected string _raycastTargetParameterName;
		[GetterBinderType(typeof(Vector4))] [SerializeField] protected string _raycastPaddingName;
		[GetterBinderType(typeof(Color))] [SerializeField] protected string _colorParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<bool>(_raycastTargetParameterName, x => UIComponent.raycastTarget = x);
			AddGetterParameter<Color>(_colorParameterName, x => UIComponent.color = x);
			AddGetterParameter<Vector4>(_raycastPaddingName, x => UIComponent.raycastPadding = x);
		}
	}
}
