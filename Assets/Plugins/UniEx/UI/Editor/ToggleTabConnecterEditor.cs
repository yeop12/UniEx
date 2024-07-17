using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UniEx.UI
{
	[CustomEditor(typeof(ToggleTabConnecter))]
	public class ToggleTabConnecterEditor : Editor
	{
		private string[] _enumTypeNames;
		private List<Type> _enumTypes;
		private Type _selectedType;
		private int _selectedTypeIndex;
		private string[] _enumValueNames;
		private SerializedProperty _enumTypeProperty;
		private SerializedProperty _enumValueProperty;
		private SerializedProperty _onValueChangedProperty;

		private void OnEnable()
		{
			_enumTypeProperty = serializedObject.FindProperty("_enumType");
			_enumValueProperty = serializedObject.FindProperty("_enumValue");
			_onValueChangedProperty = serializedObject.FindProperty("_onValueChanged");

			var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp")!;
			_enumTypes = assembly.GetTypes().Where(x => x.IsEnum).ToList();
			_enumTypeNames = new[] { "[None]" }.Concat(_enumTypes.Select(x => x.Name)).ToArray();
			_selectedTypeIndex = _enumTypes.FindIndex(x => x.FullName == _enumTypeProperty.stringValue);
			_selectedType = _selectedTypeIndex == -1 ? null : _enumTypes[_selectedTypeIndex];
			if (_selectedType is not null)
			{
				_enumValueNames = new[] { "[None]" }.Concat(Enum.GetNames(_selectedType)).ToArray();
			}
		}

		public override void OnInspectorGUI()
		{
			var selectedTypeIndex = EditorGUILayout.Popup("Enum type", _selectedTypeIndex + 1, _enumTypeNames) - 1;
			if (selectedTypeIndex != _selectedTypeIndex)
			{
				_selectedTypeIndex = selectedTypeIndex;
				_selectedType = _selectedTypeIndex == -1 ? null : _enumTypes[_selectedTypeIndex];
				_enumTypeProperty.stringValue = _selectedType?.Name;
				if (_selectedType is not null)
				{
					_enumValueNames = new[] { "[None]" }.Concat(Enum.GetNames(_selectedType)).ToArray();
					_enumValueProperty.intValue = -1;
				}
				serializedObject.ApplyModifiedProperties();
			}

			if (_selectedType is not null)
			{
				var selectedValueIndex = EditorGUILayout.Popup("Enum value", _enumValueProperty.intValue + 1, _enumValueNames) - 1;
				if (selectedValueIndex != _enumValueProperty.intValue) 
				{
					_enumValueProperty.intValue = selectedValueIndex;
					serializedObject.ApplyModifiedProperties();
				}
			}

			EditorGUILayout.PropertyField(_onValueChangedProperty);
		}
	}
}
