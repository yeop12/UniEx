using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

namespace UniEx.UI
{
	public class UIBinderEditor<T> : Editor
	{
		private static readonly HashSet<Type> UnityBasicType = new() { typeof(Sprite) };

		private class ParameterPropertyInfo
		{
			private const int NoneIndex = 0;
			private const int CustomIndex = 1;

			private readonly SerializedProperty _serializedProperty;
			private readonly string[] _items;
			private int _selectedIndex;
			private bool _useProperty;

			public ParameterPropertyInfo(SerializedProperty serializedProperty, FieldInfo fieldInfo, Dictionary<Type, List<string>> variableNamesByType)
			{
				_serializedProperty = serializedProperty;
				var parameterName = serializedProperty.stringValue;
				_useProperty = string.IsNullOrWhiteSpace(parameterName) is false;
				var binderType = fieldInfo.GetCustomAttribute<BinderType>();
				_items = new[] {"None", "Custom"}.Concat(variableNamesByType.Where(x => binderType.IsTargetType(x.Key)).SelectMany(x => x.Value)).ToArray();
				_selectedIndex = Array.FindIndex(_items, x => x == parameterName);
				if (_selectedIndex < 0)
				{
					_selectedIndex = string.IsNullOrWhiteSpace(parameterName) ? NoneIndex : CustomIndex;
				}
			}

			public void RenderGUI()
			{
				EditorGUILayout.BeginHorizontal("Box");
				var useProperty = EditorGUILayout.Toggle(_serializedProperty.displayName, _useProperty);
				if (_useProperty)
				{
					var selectedIndex = EditorGUILayout.Popup(_selectedIndex, _items);
					if (selectedIndex != _selectedIndex)
					{
						_selectedIndex = selectedIndex;
						_serializedProperty.stringValue = selectedIndex is NoneIndex or CustomIndex ? string.Empty : _items[selectedIndex];
					}

					if (_selectedIndex == CustomIndex)
					{
						EditorGUILayout.PropertyField(_serializedProperty, GUIContent.none);
					}
				}

				if (useProperty != _useProperty)
				{
					_useProperty = useProperty;
					if (useProperty is false)
					{
						_serializedProperty.stringValue = string.Empty;
					}
				}

				EditorGUILayout.EndHorizontal();
			}
		}
		
		private List<ParameterPropertyInfo> _parameterPropertyInfos;
		private SerializedProperty _uiElementProperty;
		

		private void OnEnable() 
		{
			var fieldsInfo = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
			_uiElementProperty = serializedObject.FindProperty("_uiElement");
			var variableNamesByType = new Dictionary<Type, List<string>>();
			FindVariablesRecursive(_uiElementProperty.objectReferenceValue.GetType(), string.Empty, 0, variableNamesByType);
			_parameterPropertyInfos = fieldsInfo.Select(x => new ParameterPropertyInfo(serializedObject.FindProperty(x.Name), x, variableNamesByType)).ToList();
		}

		private static void FindVariablesRecursive(Type type, string parentName, int depth, Dictionary<Type, List<string>> variableNamesByType)
		{
			if (depth == 4)
			{
				return;
			}

			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			var modelObjectProperty = type.GetProperty("Model", BindingFlags.NonPublic | BindingFlags.Instance);
			if (modelObjectProperty is not null)
			{
				properties = properties.Concat(new[] { modelObjectProperty }).ToArray();
			}

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType;
				if (propertyType.IsPrimitive || propertyType.IsValueType || UnityBasicType.Contains(propertyType) || propertyType == typeof(string) || propertyType.IsGenericType)
				{
					if (variableNamesByType.TryGetValue(propertyType, out var variableNames) is false)
					{
						variableNames = new List<string>();
						variableNamesByType[propertyType] = variableNames;
					}

					variableNames.Add($"{parentName}{property.Name}");
				}
				else
				{
					FindVariablesRecursive(propertyType, $"{parentName}{property.Name}/", depth + 1, variableNamesByType);
				}
			}

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			foreach (var field in fields)
			{
				var fieldType = field.FieldType;
				if (fieldType.IsPrimitive || fieldType.IsValueType || UnityBasicType.Contains(fieldType) || fieldType == typeof(string) || fieldType.IsGenericType)
				{
					if (variableNamesByType.TryGetValue(fieldType, out var variableNames) is false)
					{
						variableNames = new List<string>();
						variableNamesByType[fieldType] = variableNames;
					}

					variableNames.Add($"{parentName}{field.Name}");
				}
				else
				{
					FindVariablesRecursive(fieldType, $"{parentName}{field.Name}/", depth + 1, variableNamesByType);
				}
			}
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(_uiElementProperty);
			if (GUILayout.Button("Find", GUILayout.Width(60)))
			{
				var uiElement = (target as MonoBehaviour)!.GetComponentInParent<UIElement>();
				_uiElementProperty.objectReferenceValue = uiElement;
			}
			EditorGUILayout.EndHorizontal();

			if (_uiElementProperty.objectReferenceValue is not null)
			{
				_parameterPropertyInfos.ForEach(x => x.RenderGUI());
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}
