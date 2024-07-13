using System;
using UnityEngine;

[Serializable]
public class SampleControlModel
{
	[SerializeField] private Color _imageColor;
	[SerializeField] private string _textText;

	public Color ImageColor => _imageColor;
	public string TextText => _textText;
}
