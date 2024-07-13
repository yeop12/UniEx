using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Outline))]
	public class OutlineBinder : UIBinder<Outline>
	{
		[GetterBinderType(typeof(Color))] [SerializeField] private string _effectColorParameterName;
		[GetterBinderType(typeof(Vector2))] [SerializeField] private string _effectDistanceParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<Color>(_effectColorParameterName, x => UIComponent.effectColor = x);
			AddGetterParameter<Vector2>(_effectDistanceParameterName, x => UIComponent.effectDistance = x);
		}
	}
}
