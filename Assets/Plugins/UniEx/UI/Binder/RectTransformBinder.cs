using UnityEngine;

namespace UniEx.UI
{
	public class RectTransformBinder : UIBinder<RectTransform>
	{
		[GetterBinderType(typeof(Vector2))] [SerializeField] private string _anchoredPositionParameterName;
		[GetterBinderType(typeof(Vector2))] [SerializeField] private string _sizeDeltaParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<Vector2>(_anchoredPositionParameterName, x => UIComponent.anchoredPosition = x);
			AddGetterParameter<Vector2>(_sizeDeltaParameterName, x => UIComponent.sizeDelta = x);
		}
	}
}
