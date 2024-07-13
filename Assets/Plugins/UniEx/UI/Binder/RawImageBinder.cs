using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(RawImage))]
	public class RawImageBinder : MaskableGraphicBinder<RawImage>
	{
		[GetterBinderType(typeof(Texture))] [SerializeField] private string _textureParameterName;
		[GetterBinderType(typeof(Rect))] [SerializeField] private string _uvRectParameterName;

		protected override void Awake() {
			base.Awake();
			AddGetterParameter<Texture>(_textureParameterName, x => UIComponent.texture = x);
			AddGetterParameter<Rect>(_uvRectParameterName, x => UIComponent.uvRect = x);
		}
	}
}
