using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	public abstract class GraphicBinder<T> : UIBinder<T> where T : Graphic
	{
		[BinderType(typeof(bool))] [SerializeField] protected string _raycastTargetParameterName;
		[BinderType(typeof(Color))] [SerializeField] protected string _raycastPaddingName;
		[BinderType(typeof(Vector4))] [SerializeField] protected string _colorParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<bool>(_raycastTargetParameterName, x => UIComponent.raycastTarget = x);
			AddParameter<Color>(_colorParameterName, x => UIComponent.color = x);
			AddParameter<Vector4>(_raycastPaddingName, x => UIComponent.raycastPadding = x);
		}
	}
}
