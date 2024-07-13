using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace UniEx.UI
{
	[RequireComponent(typeof(Canvas))]
	public sealed class MovedUIManager : MonoBehaviour
	{
		private readonly Dictionary<Type, MovedUIWindow> _prefabs = new();
		private readonly Dictionary<Type, UniTaskCompletionSource<MovedUIWindow>> _loadingUIObjects = new();
		private readonly Dictionary<Type, List<MovedUIWindow>> _uiObjectPools = new();

		private Transform _parent;
		[Inject] private DiContainer _container;

		private void Awake()
		{
			_parent = transform;
			var rectTransform = GetComponent<RectTransform>();
			_container.Bind<RectTransform>().WithId("MovedUICanvasRectTransform").FromInstance(rectTransform).AsSingle().NonLazy();
		}

		public async UniTask LoadAsync<T>(CancellationToken ct ) where T : MovedUIWindow 
		{
			var type = typeof(T);
			MovedUIWindow movedUIWindow = null;

			try
			{
				movedUIWindow = GetUIObjectInPool(type);
				if (movedUIWindow is null)
				{
					MovedUIWindow prefab = null;
					if (_loadingUIObjects.TryGetValue(type, out var loadingUIObject))
					{
						prefab = await loadingUIObject.Task;
					}
					else if (_prefabs.TryGetValue(type, out prefab) is false)
					{
						if (Attribute.GetCustomAttribute(type, typeof(AddressableInfoAttribute)) is AddressableInfoAttribute addressableInfo)
						{
							var tcs = new UniTaskCompletionSource<MovedUIWindow>();
							_loadingUIObjects[type] = tcs;
							var obj = await Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key);
							var uiComponent = obj.GetComponent<T>();
							if (uiComponent is not null)
							{
								_prefabs[type] = uiComponent;
								_uiObjectPools[type] = new List<MovedUIWindow>();
								prefab = uiComponent;
								_loadingUIObjects.Remove(type);
								tcs.TrySetResult(uiComponent);
							}
							else
							{
								Addressables.Release(obj);
								_loadingUIObjects.Remove(type);
								var ex = new Exception($"{type.Name} does not contain script.");
								tcs.TrySetException(ex);
								throw ex;
							}
						}
						else
						{
							throw new Exception($"{type.Name} class does not contain AddressAttribute.");
						}
					}

					movedUIWindow = _container.InstantiatePrefabForComponent<T>(prefab, _parent);
					movedUIWindow.OnClosed += () => _uiObjectPools[type].Add(movedUIWindow);
				}

				ct.ThrowIfCancellationRequested();
			}
			catch (OperationCanceledException)
			{
				movedUIWindow?.Close(true);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		public async UniTask<T> OpenAsync<T>(object modelObject, Transform targetTransform, CancellationToken ct) where T : MovedUIWindow
		{
			var type = typeof(T);
			MovedUIWindow movedUIWindow = null;

			try
			{
				if (targetTransform is null)
				{
					throw new ArgumentNullException(nameof(targetTransform));
				}

				movedUIWindow = GetUIObjectInPool(type);
				if (movedUIWindow is null)
				{
					MovedUIWindow prefab = null;
					if (_loadingUIObjects.TryGetValue(type, out var loadingUIObject))
					{
						prefab = await loadingUIObject.Task;
					}
					else if (_prefabs.TryGetValue(type, out prefab) is false)
					{
						if (Attribute.GetCustomAttribute(type, typeof(AddressableInfoAttribute)) is AddressableInfoAttribute addressableInfo)
						{
							var tcs = new UniTaskCompletionSource<MovedUIWindow>();
							_loadingUIObjects[type] = tcs;
							var obj = await Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key);
							var uiComponent = obj.GetComponent<T>();
							if (uiComponent is not null)
							{
								_prefabs[type] = uiComponent;
								_uiObjectPools[type] = new List<MovedUIWindow>();
								prefab = uiComponent;
								_loadingUIObjects.Remove(type);
								tcs.TrySetResult(uiComponent);
							}
							else
							{
								Addressables.Release(obj);
								_loadingUIObjects.Remove(type);
								var ex = new Exception($"{type.Name} does not contain script.");
								tcs.TrySetException(ex);
								throw ex;
							}
						}
						else
						{
							throw new Exception($"{type.Name} class does not contain AddressAttribute.");
						}
					}

					movedUIWindow = _container.InstantiatePrefabForComponent<T>(prefab, _parent);
					movedUIWindow.OnClosed += () => _uiObjectPools[type].Add(movedUIWindow);
				}

				movedUIWindow.Open(modelObject, targetTransform);
				ct.ThrowIfCancellationRequested();
			}
			catch (OperationCanceledException)
			{
				movedUIWindow?.Close(true);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return movedUIWindow as T;
		}

		public T Open<T>(object modelObject, Transform targetTransform) where T : MovedUIWindow
		{
			var type = typeof(T);
			MovedUIWindow movedUIWindow = null;

			try
			{
				if (targetTransform is null)
				{
					throw new ArgumentNullException(nameof(targetTransform));
				}

				movedUIWindow = GetUIObjectInPool(type);
				if (movedUIWindow is null)
				{
					MovedUIWindow prefab = null;
					if (_loadingUIObjects.TryGetValue(type, out var loadingUIObject))
					{
						throw new Exception("Prefab is loading asynchronously.");
					}
					else if (_prefabs.TryGetValue(type, out prefab) is false)
					{
						if (Attribute.GetCustomAttribute(type, typeof(AddressableInfoAttribute)) is AddressableInfoAttribute addressableInfo)
						{
							var tcs = new UniTaskCompletionSource<MovedUIWindow>();
							_loadingUIObjects[type] = tcs;
							var obj = Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key).WaitForCompletion();
							var uiComponent = obj.GetComponent<T>();
							if (uiComponent is not null)
							{
								_prefabs[type] = uiComponent;
								_uiObjectPools[type] = new List<MovedUIWindow>();
								prefab = uiComponent;
								_loadingUIObjects.Remove(type);
								tcs.TrySetResult(uiComponent);
							}
							else
							{
								Addressables.Release(obj);
								_loadingUIObjects.Remove(type);
								var ex = new Exception($"{type.Name} does not contain script.");
								tcs.TrySetException(ex);
								throw ex;
							}
						}
						else
						{
							throw new Exception($"{type.Name} class does not contain AddressAttribute.");
						}
					}

					movedUIWindow = _container.InstantiatePrefabForComponent<T>(prefab, _parent);
					movedUIWindow.OnClosed += () => _uiObjectPools[type].Add(movedUIWindow);
				}

				movedUIWindow.Open(modelObject, targetTransform);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return movedUIWindow as T;
		}

		private MovedUIWindow GetUIObjectInPool(Type type)
		{
			if (_uiObjectPools.TryGetValue(type, out var uiObjectPool) is false) return null;
			if (uiObjectPool.Any() is false) return null;
			var uiObject = uiObjectPool.Last();
			uiObjectPool.RemoveAt(uiObjectPool.Count - 1);
			return uiObject;
		}
	}
}