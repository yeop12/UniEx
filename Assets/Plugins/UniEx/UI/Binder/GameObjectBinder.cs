using UnityEngine;

namespace UniEx.UI
{
	public class GameObjectBinder : UIBinder<GameObject>
	{
		[SerializeField] private GameObject _target;
		[SerializeField] private bool _useInverseValue;
		[SerializeField] private string _activationParameterName;

		protected override void Awake()
		{
			base.Awake();
			AddParameter<bool>(_activationParameterName, x => UIComponent.SetActive(_useInverseValue ? !x : x));
		}

		protected override GameObject FindUIComponent()
		{
			return _target;
		}
	}
}
