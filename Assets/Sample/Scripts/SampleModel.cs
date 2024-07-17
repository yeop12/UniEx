using System;
using System.Collections.Generic;
using TMPro;
using UniEx.UniRx;
using UnityEngine;
using UniRx;

public class SampleModel : MonoBehaviour
{
	[Header("[ Image ]")]
	[Header("Value")]
	[SerializeField] private Sprite _imageSprite;
	[SerializeField] private float _imageFillAmount;
	[SerializeField] private Color _imageColor;
	public Sprite ImageSprite => _imageSprite;
	public float ImageFillAmount => _imageFillAmount;
	public Color ImageColor => _imageColor;
	
	[Header("Reactive")]
	[SerializeField] private SpriteReactiveProperty _imageReactiveSprite;
	[SerializeField] private FloatReactiveProperty _imageReactiveFillAmount;
	[SerializeField] private ColorReactiveProperty _imageReactiveColor;
	public IObservable<Sprite> ImageReactiveSprite => _imageReactiveSprite;
	public IObservable<float> ImageReactiveFillAmount => _imageReactiveFillAmount;
	public IObservable<Color> ImageReactiveColor => _imageReactiveColor;
	
	
	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ RawImage ]")]
	[Header("Value")]
	[SerializeField] private Texture _rawImageTexture;
	[SerializeField] private Rect _rawImageUVRect;
	[SerializeField] private Color _rawImageColor;
	public Texture RawImageTexture => _rawImageTexture;
	public Rect RawImageUVRect => _rawImageUVRect;
	public Color RawImageColor => _rawImageColor;

	[Header("Reactive")]
	[SerializeField] private TextureReactiveProperty _rawImageReactiveTexture;
	[SerializeField] private RectReactiveProperty _rawImageReactiveUVRect;
	[SerializeField] private ColorReactiveProperty _rawImageReactiveColor;
	public IObservable<Texture> RawImageReactiveTexture => _rawImageReactiveTexture;
	public IObservable<Rect> RawImageReactiveUVRect => _rawImageReactiveUVRect;
	public IObservable<Color> RawImageReactiveColor => _rawImageReactiveColor;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Text ]")]
	[Header("Value")]
	[SerializeField] private string _textText;
	[SerializeField] private float _textFontSize;
	[SerializeField] private FontStyles _textFontStyle;
	[SerializeField] private Color _textColor;
	public string TextText => _textText;
	public float TextFontSize => _textFontSize;
	public FontStyles TextFontStyle => _textFontStyle;
	public Color TextColor => _textColor;

	[Header("Reactive")]
	[SerializeField] private StringReactiveProperty _textReactiveText;
	[SerializeField] private FloatReactiveProperty _textReactiveFontSize;
	[SerializeField] private FontStylesReactiveProperty _textReactiveFontStyle;
	[SerializeField] private ColorReactiveProperty _textReactiveColor;
	public IObservable<string> TextReactiveText => _textReactiveText;
	public IObservable<float> TextReactiveFontSize => _textReactiveFontSize;
	public IObservable<FontStyles> TextReactiveFontStyle => _textReactiveFontStyle;
	public IObservable<Color> TextReactiveColor => _textReactiveColor;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ GameObject ]")]
	[Header("Value")]
	[SerializeField] private bool _gameObjectActive;
	public bool GameObjectActive => _gameObjectActive;

	[Header("Reactive")]
	[SerializeField] private BoolReactiveProperty _gameObjectReactiveActive;
	public IObservable<bool> GameObjectReactiveActive => _gameObjectReactiveActive;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Outline ]")]
	[Header("Value")]
	[SerializeField] private Vector2 _outlineEffectDistance;
	[SerializeField] private Color _outlineEffectColor;
	public Vector2 OutlineEffectDistance => _outlineEffectDistance;
	public Color OutlineEffectColor => _outlineEffectColor;

	[Header("Reactive")] 
	[SerializeField] private Vector2ReactiveProperty _outlineReactiveEffectDistance;
	[SerializeField] private ColorReactiveProperty _outlineReactiveEffectColor;
	public IObservable<Vector2> OutlineReactiveEffectDistance => _outlineReactiveEffectDistance;
	public IObservable<Color> OutlineReactiveEffectColor => _outlineReactiveEffectColor;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Slider ]")]
	[Header("Value")]
	[SerializeField] private float _sliderValue;
	[SerializeField] private float _sliderValueWithoutNotify;
	[SerializeField] private float _sliderMinValue;
	[SerializeField] private float _sliderMaxValue;
	[SerializeField] private bool _sliderWholeNumbers;
	public float SliderValue => _sliderValue;
	public float SliderValueWithoutNotify => _sliderValueWithoutNotify;
	public float SliderMinValue => _sliderMinValue;
	public float SliderMaxValue => _sliderMaxValue;
	public bool SliderWholeNumbers => _sliderWholeNumbers;
	public float SliderOnValueChanged
	{
		set => Debug.Log($"[Value] Slider 'onValueChanged' : {value}");
	}

	[Header("Reactive")]
	[SerializeField] private FloatReactiveProperty _sliderReactiveValue;
	[SerializeField] private FloatReactiveProperty _sliderReactiveValueWithoutNotify;
	[SerializeField] private FloatReactiveProperty _sliderReactiveMinValue;
	[SerializeField] private FloatReactiveProperty _sliderReactiveMaxValue;
	[SerializeField] private BoolReactiveProperty _sliderReactiveWholeNumbers;
	[SerializeField] private FloatReactiveProperty _sliderReactiveOnValueChanged;
	public IObservable<float> SliderReactiveValue => _sliderReactiveValue;
	public IObservable<float> SliderReactiveValueWithoutNotify => _sliderReactiveValueWithoutNotify;
	public IObservable<float> SliderReactiveMinValue => _sliderReactiveMinValue;
	public IObservable<float> SliderReactiveMaxValue => _sliderReactiveMaxValue;
	public IObservable<bool> SliderReactiveWholeNumbers => _sliderReactiveWholeNumbers;
	public FloatReactiveProperty SliderReactiveOnValueChanged => _sliderReactiveOnValueChanged;

	
	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Scrollbar ]")]
	[Header("Value")]
	[SerializeField] private float _scrollbarValue;
	[SerializeField] private float _scrollbarValueWithoutNotify;
	[SerializeField] private float _scrollbarSize;
	[SerializeField] private int _scrollbarNumberOfStep;
	public float ScrollbarValue => _scrollbarValue;
	public float ScrollbarValueWithoutNotify => _scrollbarValueWithoutNotify;
	public float ScrollbarSize => _scrollbarSize;
	public int ScrollbarNumberOfStep => _scrollbarNumberOfStep;
	public float ScrollbarOnValueChanged
	{
		set => Debug.Log($"[Value] Scrollbar 'onValueChanged' : {value}");
	}

	[Header("Reactive")]
	[SerializeField] private FloatReactiveProperty _scrollbarReactiveValue;
	[SerializeField] private FloatReactiveProperty _scrollbarReactiveValueWithoutNotify;
	[SerializeField] private FloatReactiveProperty _scrollbarReactiveSize;
	[SerializeField] private IntReactiveProperty _scrollbarReactiveNumberOfStep;
	[SerializeField] private FloatReactiveProperty _scrollbarReactiveOnValueChanged;
	public IObservable<float> ScrollbarReactiveValue => _scrollbarReactiveValue;
	public IObservable<float> ScrollbarReactiveValueWithoutNotify => _scrollbarReactiveValueWithoutNotify;
	public IObservable<float> ScrollbarReactiveSize => _scrollbarReactiveSize;
	public IObservable<int> ScrollbarReactiveNumberOfStep => _scrollbarReactiveNumberOfStep;
	public FloatReactiveProperty ScrollbarReactiveOnValueChanged => _scrollbarReactiveOnValueChanged;

	
	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Toggle ]")]
	[Header("Value")]
	[SerializeField] private bool _toggleIsOn;
	[SerializeField] private bool _toggleIsOnWithoutNotify;
	public bool ToggleIsOn => _toggleIsOn;
	public bool ToggleIsOnWithoutNotify => _toggleIsOnWithoutNotify;
	public bool ToggleOnValueChanged
	{
		set => Debug.Log($"[Value] Toggle 'onValueChanged' : {value}");
	}
	
	[Header("Reactive")]
	[SerializeField] private BoolReactiveProperty _toggleReactiveIsOn;
	[SerializeField] private BoolReactiveProperty _toggleReactiveIsOnWithoutNotify;
	[SerializeField] private BoolReactiveProperty _toggleReactiveOnValueChanged;
	public IObservable<bool> ToggleReactiveIsOn => _toggleReactiveIsOn;
	public IObservable<bool> ToggleReactiveIsOnWithoutNotify => _toggleReactiveIsOnWithoutNotify;
	public BoolReactiveProperty ToggleReactiveOnValueChanged => _toggleReactiveOnValueChanged;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Dropdown ]")]
	[Header("Value")]
	[SerializeField] private int _dropdownValue;
	[SerializeField] private int _dropdownValueWithoutNotify;
	[SerializeField] private List<TMP_Dropdown.OptionData> _dropdownOptions;
	public int DropdownValue => _dropdownValue;
	public int DropdownValueWithoutNotify => _dropdownValueWithoutNotify;
	public IReadOnlyList<TMP_Dropdown.OptionData> DropdownOptions => _dropdownOptions;
	public int DropdownOnValueChanged
	{
		set => Debug.Log($"[Value] Dropdown 'onValueChanged' : {value}");
	}

	[Header("Reactive")]
	[SerializeField] private IntReactiveProperty _dropdownReactiveValue;
	[SerializeField] private IntReactiveProperty _dropdownReactiveValueWithoutNotify;
	[SerializeField] private List<TMP_Dropdown.OptionData> _dropdownReactiveOptions;
	[SerializeField] private IntReactiveProperty _dropdownReactiveOnValueChanged;
	public IObservable<int> DropdownReactiveValue => _dropdownReactiveValue;
	public IObservable<int> DropdownReactiveValueWithoutNotify => _dropdownReactiveValueWithoutNotify;
	public IReadOnlyList<TMP_Dropdown.OptionData> DropdownReactiveOptions => _dropdownReactiveOptions;
	public IntReactiveProperty DropdownReactiveOnValueChanged => _dropdownReactiveOnValueChanged;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ InputField ]")]
	[Header("Value")]
	[SerializeField] private string _inputFieldText;
	[SerializeField] private string _inputFieldTextWithoutNotify;
	public string InputFieldText => _inputFieldText;
	public string InputFieldTextWithoutNotify => _inputFieldTextWithoutNotify;
	public string InputFieldOnValueChanged
	{
		set => Debug.Log($"[Value] InputField 'onValueChanged' : {value}");
	}
	public string InputFieldOnEndEdit
	{
		set => Debug.Log($"[Value] InputField 'onEndEdit' : {value}");
	}
	public string InputFieldOnSelect
	{
		set => Debug.Log($"[Value] InputField 'onSelect' : {value}");
	}
	public string InputFieldOnDeselect
	{
		set => Debug.Log($"[Value] InputField 'onDeselect' : {value}");
	}
	
	[Header("Reactive")]
	[SerializeField] private StringReactiveProperty _inputFieldReactiveText;
	[SerializeField] private StringReactiveProperty _inputFieldReactiveTextWithoutNotify;
	[SerializeField] private StringReactiveProperty _inputFieldReactiveOnValueChanged;
	[SerializeField] private StringReactiveProperty _inputFieldReactiveOnEndEdit;
	[SerializeField] private StringReactiveProperty _inputFieldReactiveOnSelect;
	[SerializeField] private StringReactiveProperty _inputFieldReactiveOnDeselect;
	public IObservable<string> InputFieldReactiveText => _inputFieldReactiveText;
	public IObservable<string> InputFieldReactiveTextWithoutNotify => _inputFieldReactiveTextWithoutNotify;
	public StringReactiveProperty InputFieldReactiveOnValueChanged => _inputFieldReactiveOnValueChanged;
	public StringReactiveProperty InputFieldReactiveOnEndEdit => _inputFieldReactiveOnEndEdit;
	public StringReactiveProperty InputFieldReactiveOnSelect => _inputFieldReactiveOnSelect;
	public StringReactiveProperty InputFieldReactiveOnDeselect => _inputFieldReactiveOnDeselect;


	public void OnClickButton()
	{
		Debug.Log($"Button 'onClick'");
	}


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Animator ]")]
	[Header("Value")]
	[SerializeField] private string _animatorBoolName;

	[SerializeField] private bool _animatorBoolValue;
	[SerializeField] private string _animatorFloatName;

	[SerializeField] private float _animatorFloatValue;
	[SerializeField] private string _animatorTriggerName;
	public (string, bool) AnimatorBool => (_animatorBoolName, _animatorBoolValue);
	public (string, float) AnimatorFloat => (_animatorFloatName, _animatorFloatValue);
	public string AnimatorTrigger => _animatorTriggerName;

	[Header("Reactive")]
	[SerializeField] private StringReactiveProperty _animatorReactiveBoolName;
	[SerializeField] private BoolReactiveProperty _animatorReactiveBoolValue;
	[SerializeField] private StringReactiveProperty _animatorReactiveFloatName;
	[SerializeField] private FloatReactiveProperty _animatorReactiveFloatValue;
	[SerializeField] private StringReactiveProperty _animatorReactiveTriggerName;

	public IObservable<(string, bool)> AnimatorReactiveBool =>
		_animatorReactiveBoolName.CombineLatest(_animatorReactiveBoolValue, (name, value) => (name, value));
	public IObservable<(string, float)> AnimatorReactiveFloat =>
		_animatorReactiveFloatName.CombineLatest(_animatorReactiveFloatValue, (name, value) => (name, value));
	public IObservable<string> AnimatorReactiveTrigger => _animatorReactiveTriggerName;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Control ]")]
	[Header("Reactive")]
	[SerializeField] private BoolReactiveProperty _controlReactiveSwitch;
	[SerializeField] private SampleControlModel _controlModel;
	public IObservable<SampleControlModel> ControlModel => _controlReactiveSwitch.Select(x => x ? _controlModel : null);


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Grid ]")]
	[Header("Value")]
	[SerializeField] private List<SampleGridModel> _gridModels;
	public IReadOnlyList<SampleGridModel> GridModels => _gridModels;


	[Header("--------------------------------------------------------------------------------------------")]
	[Header("[ Tab ]")]
	[Header("Value")]
	[SerializeField] private SampleTabFirstModel _tabFirstModel;
	[SerializeField] private SampleTabSecondModel _tabSecondModel;
	[SerializeField] private SampleTabKind _tabKind;
	public IEnumerable<(Enum, object)> TabGroupTabInfos => new List<(Enum, object)>()
		{ (SampleTabKind.First, _tabFirstModel), (SampleTabKind.Second, _tabSecondModel) };
	public SampleTabKind TabGroupSelectedKind
	{
		get => _tabKind;
		set => _tabKind = value;
	}

	[Header("Reactive")]
	[SerializeField] private SampleTabFirstModel _tabReactiveFirstModel;
	[SerializeField] private SampleTabSecondModel _tabReactiveSecondModel;
	[SerializeField] private SampleTabKindReactiveProperty _tabReactiveKind;
	public IEnumerable<(Enum, object)> TabGroupReactiveTabInfos => new List<(Enum, object)>()
		{ (SampleTabKind.First, _tabReactiveFirstModel), (SampleTabKind.Second, _tabReactiveSecondModel) };
	public IObservable<Enum> TabGroupReactiveSelectedKind => _tabReactiveKind.Select(x => (Enum)x);
	public void SetTabGroupReactiveSelectedKind(Enum value)
	{
		if (value is null)
		{
			return;
		}
		_tabReactiveKind.Value = (SampleTabKind)value;
	}
}
