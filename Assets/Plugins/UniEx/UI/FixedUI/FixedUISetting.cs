using System.Collections.Generic;
using UnityEngine;

namespace UniEx.UI
{
	[CreateAssetMenu(fileName = "FixedUISetting", menuName = "UniEx/FixedUISetting")]
	public class FixedUISetting : ScriptableObject
	{
		[SerializeField] private List<string> _layerNames;

		public IReadOnlyList<string> LayerNames => _layerNames;
	}
}
