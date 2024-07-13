using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace UniEx.UI
{
	public class UIBinderEditor<T> : Editor
	{
		private static readonly HashSet<Type> UnityBasicType = new()
		{
			typeof(Sprite), 
			typeof(Texture),
		};

		private const int FindDepth = 4;

		private class ParameterPropertyInfo
		{
			private const int NoneIndex = 0;
			private const int CustomIndex = 1;

			private readonly SerializedProperty _serializedProperty;
			private readonly string[] _items;
			private int _selectedIndex;
			private bool _useProperty;

			public ParameterPropertyInfo(SerializedProperty serializedProperty, FieldInfo fieldInfo, Dictionary<Type, List<string>> getterVariableNamesByType, Dictionary<Type, List<string>> setterVariableNamesByType )
			{
				_serializedProperty = serializedProperty;
				var parameterName = serializedProperty.stringValue;
				_useProperty = string.IsNullOrWhiteSpace(parameterName) is false;
				var binderType = fieldInfo.GetCustomAttribute<BinderTypeAttribute>();
				var variableNamesByType = binderType.IsGetter ? getterVariableNamesByType : setterVariableNamesByType;
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
				if (useProperty != _useProperty)
				{
					_useProperty = useProperty;
					if (useProperty is false)
					{
						_serializedProperty.stringValue = string.Empty;
						_selectedIndex = 0;
					}
				}

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


				EditorGUILayout.EndHorizontal();
			}
		}
		
		private List<ParameterPropertyInfo> _parameterPropertyInfos;
		private SerializedProperty _uiElementProperty;
		private List<SerializedProperty> _otherProperties;

		private void OnEnable() 
		{
			_uiElementProperty = serializedObject.FindProperty("_uiElement");
			_otherProperties = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(x => x.GetCustomAttribute<SerializeField>() is not null &&
				            x.GetCustomAttribute<BinderTypeAttribute>() is null)
				.Select(x => serializedObject.FindProperty(x.Name)).ToList();
			FindParameterPropertyInfos();
		}

		private void FindParameterPropertyInfos() 
		{
			if (_uiElementProperty.objectReferenceValue is null)
			{
				return;
			}

			var fieldsInfo = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(x => x.GetCustomAttribute<BinderTypeAttribute>() is not null);
			var getterVariableNamesByType = new Dictionary<Type, List<string>>();
			var setterVariableNamesByType = new Dictionary<Type, List<string>>();
			FindVariablesRecursive(_uiElementProperty.objectReferenceValue.GetType(), string.Empty, 0, getterVariableNamesByType, true);
			FindVariablesRecursive(_uiElementProperty.objectReferenceValue.GetType(), string.Empty, 0, setterVariableNamesByType, false);
			FindFunctionRecursive(_uiElementProperty.objectReferenceValue.GetType(), string.Empty, 0, setterVariableNamesByType);
			_parameterPropertyInfos = fieldsInfo.Select(x => new ParameterPropertyInfo(serializedObject.FindProperty(x.Name), x, getterVariableNamesByType, setterVariableNamesByType)).ToList();
		}

		private static void FindVariablesRecursive(Type type, string parentName, int depth, Dictionary<Type, List<string>> variableNamesByType, bool isGetter)
		{
			if (depth == FindDepth)
			{
				return;
			}

			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			var modelObjectProperty = type.GetProperty("Model", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (modelObjectProperty is not null)
			{
				properties = properties.Concat(new[] { modelObjectProperty }).ToArray();
			}

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType;
				if (propertyType.IsPrimitive || propertyType.IsValueType || UnityBasicType.Contains(propertyType) || propertyType == typeof(string) || propertyType.IsGenericType || ( propertyType.BaseType is not null && propertyType.BaseType.IsGenericType && propertyType.BaseType.GetGenericTypeDefinition() == typeof(ReactiveProperty<>) ))
				{
					if (isGetter is false && propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
					{
						propertyType = propertyType.GetGenericArguments()[0];
						if (variableNamesByType.TryGetValue(propertyType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[propertyType] = variableNames;
						}

						variableNames.Add($"{parentName}{property.Name}/Value");
					}
					else if (isGetter is false && propertyType.BaseType is not null && propertyType.BaseType.IsGenericType && propertyType.BaseType.GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
					{
						propertyType = propertyType.BaseType.GetGenericArguments()[0];
						if (variableNamesByType.TryGetValue(propertyType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[propertyType] = variableNames;
						}

						variableNames.Add($"{parentName}{property.Name}/Value");
					}
					else
					{
						if ((isGetter && property.CanRead is false) || (isGetter is false && property.CanWrite is false))
						{
							continue;
						}

						if (variableNamesByType.TryGetValue(propertyType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[propertyType] = variableNames;
						}

						variableNames.Add($"{parentName}{property.Name}");
					}
				}
				else
				{
					FindVariablesRecursive(propertyType, $"{parentName}{property.Name}/", depth + 1, variableNamesByType, isGetter);
				}
			}

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			foreach (var field in fields)
			{
				var fieldType = field.FieldType;
				if (fieldType.IsPrimitive || fieldType.IsValueType || UnityBasicType.Contains(fieldType) || fieldType == typeof(string) || fieldType.IsGenericType)
				{
					if (isGetter is false && fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
					{
						fieldType = fieldType.GetGenericArguments()[0];
						if (variableNamesByType.TryGetValue(fieldType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[fieldType] = variableNames;
						}

						variableNames.Add($"{parentName}{field.Name}/Value");
					}
					else if (isGetter is false && fieldType.BaseType is not null && fieldType.BaseType.IsGenericType && fieldType.BaseType.GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
					{
						fieldType = fieldType.BaseType.GetGenericArguments()[0];
						if (variableNamesByType.TryGetValue(fieldType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[fieldType] = variableNames;
						}

						variableNames.Add($"{parentName}{field.Name}/Value");
					}
					else
					{
						if (variableNamesByType.TryGetValue(fieldType, out var variableNames) is false)
						{
							variableNames = new List<string>();
							variableNamesByType[fieldType] = variableNames;
						}

						variableNames.Add($"{parentName}{field.Name}");
					}
				}
				else
				{
					FindVariablesRecursive(fieldType, $"{parentName}{field.Name}/", depth + 1, variableNamesByType, isGetter);
				}
			}
		}

		private static void FindFunctionRecursive(Type type, string parentName, int depth, Dictionary<Type, List<string>> variableNamesByType)
		{
			if (depth == FindDepth)
			{
				return;
			}

			var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(x => x.Attributes.HasFlag(MethodAttributes.SpecialName) is false);
			foreach (var method in methods)
			{
				var parameters = method.GetParameters();
				Type parameterType;
				switch (parameters.Length)
				{
					case 0:
						parameterType = typeof(void);
						break;
					case 1:
						parameterType = parameters[0].ParameterType;
						break;
					default:
						continue;
				}

				if (variableNamesByType.TryGetValue(parameterType, out var variableNames) is false)
				{
					variableNames = new List<string>();
					variableNamesByType[parameterType] = variableNames;
				}

				variableNames.Add($"{parentName}{method.Name}()");
			}

			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(x => x.CanRead);

			var modelObjectProperty = type.GetProperty("Model", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if (modelObjectProperty is not null)
			{
				properties = properties.Concat(new[] { modelObjectProperty }).ToArray();
			}

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType;
				if (propertyType.IsPrimitive || propertyType.IsValueType || UnityBasicType.Contains(propertyType) || propertyType == typeof(string) || propertyType.IsGenericType)
				{
					continue;
				}

				FindFunctionRecursive(propertyType, $"{parentName}{property.Name}/", depth + 1, variableNamesByType);
			}

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

			foreach (var field in fields)
			{
				var fieldType = field.FieldType;
				if (fieldType.IsPrimitive || fieldType.IsValueType || UnityBasicType.Contains(fieldType) || fieldType == typeof(string) || fieldType.IsGenericType)
				{
					continue;
				}

				FindFunctionRecursive(fieldType, $"{parentName}{field.Name}/", depth + 1, variableNamesByType);
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
				FindParameterPropertyInfos();
			}
			EditorGUILayout.EndHorizontal();
			_otherProperties.ForEach(x => EditorGUILayout.PropertyField(x));
			if (_uiElementProperty.objectReferenceValue is not null)
			{
				_parameterPropertyInfos.ForEach(x => x.RenderGUI());
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}
