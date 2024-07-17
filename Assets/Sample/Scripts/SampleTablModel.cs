using System;
using UniRx;
using UnityEngine;

[Serializable]
public enum SampleTabKind
{
	None,
	First,
	Second,
}

[Serializable]
public class SampleTabFirstModel
{
	[SerializeField] private ColorReactiveProperty _imageReactiveColor;

	public IObservable<Color> ImageReactiveColor => _imageReactiveColor;
}

[Serializable]
public class SampleTabSecondModel
{
	[SerializeField] private ColorReactiveProperty _imageReactiveColor;

	public IObservable<Color> ImageReactiveColor => _imageReactiveColor;
}