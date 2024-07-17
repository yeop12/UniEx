using UniRx;
using UnityEditor;

namespace UniEx.UniRx
{
	[CustomPropertyDrawer(typeof(TextureReactiveProperty))]
	[CustomPropertyDrawer(typeof(SpriteReactiveProperty))]
	[CustomPropertyDrawer(typeof(FontStylesReactiveProperty))]
	public partial class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
	{
	}
}
