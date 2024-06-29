using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
	[RequireComponent(typeof(RawImage))]
	public class RawImageBinder : MaskableGraphicBinder<RawImage>
	{
		[BinderType(typeof(Texture))] [SerializeField] private string _textureParameterName;
		[BinderType(typeof(Rect))] [SerializeField] private string _uvRectParameterName;

		protected override void Awake() {
			base.Awake();
			AddParameter<Texture>(_textureParameterName, x => UIComponent.texture = x);
			AddParameter<Rect>(_uvRectParameterName, x => UIComponent.uvRect = x);
		}
	}
}
