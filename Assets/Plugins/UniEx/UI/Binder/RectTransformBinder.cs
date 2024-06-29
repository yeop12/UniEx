using UnityEngine;

namespace UniEx.UI
{
	public class RectTransformBinder : UIBinder<RectTransform>
	{
		[BinderType(typeof(Vector2))] [SerializeField] private string _anchoredPositionParameterName;
		[BinderType(typeof(Vector2))] [SerializeField] private string _sizeDeltaParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<Vector2>(_anchoredPositionParameterName, x => UIComponent.anchoredPosition = x);
			AddParameter<Vector2>(_sizeDeltaParameterName, x => UIComponent.sizeDelta = x);
		}
	}
}
