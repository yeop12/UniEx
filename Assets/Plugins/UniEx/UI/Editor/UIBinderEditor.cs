using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UniEx.UI
{
	public class UIBinderEditor<T> : Editor
	{
		private class ParameterPropertyInfo
		{
			private readonly SerializedProperty _serializedProperty;
			private bool _useProperty;

			public ParameterPropertyInfo(SerializedProperty serializedProperty)
			{
				_serializedProperty = serializedProperty;
				_useProperty = string.IsNullOrWhiteSpace(serializedProperty.stringValue) is false;
			}

			public void RenderGUI()
			{
				EditorGUILayout.BeginVertical("Box");
				var useProperty = EditorGUILayout.Toggle(_serializedProperty.displayName, _useProperty);
				if (_useProperty)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(_serializedProperty, new GUIContent("Name"));
					EditorGUI.indentLevel--;
				}

				if (useProperty != _useProperty)
				{
					_useProperty = useProperty;
					if (useProperty is false)
					{
						_serializedProperty.stringValue = string.Empty;
					}
				}

				EditorGUILayout.EndVertical();
			}
		}
		
		private List<ParameterPropertyInfo> _parameterPropertyInfos;
		private SerializedProperty _uiElementProperty;
		

		private void OnEnable() 
		{
			var fieldsInfo = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			_uiElementProperty = serializedObject.FindProperty("_uiElement");
			_parameterPropertyInfos = fieldsInfo.Select(x => new ParameterPropertyInfo(serializedObject.FindProperty(x.Name))).ToList();
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(_uiElementProperty);
			_parameterPropertyInfos.ForEach(x => x.RenderGUI());
			serializedObject.ApplyModifiedProperties();
		}
	}
}
