using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(Outline))]
	public class OutlineBinder : UIBinder<Outline>
	{
		[SerializeField] private string _effectColorParameterName;
		[SerializeField] private string _effectDistanceParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<Color>(_effectColorParameterName, x => UIComponent.effectColor = x);
			AddParameter<Vector2>(_effectDistanceParameterName, x => UIComponent.effectDistance = x);
		}
	}
}
