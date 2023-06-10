using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace UniEx
{
	public sealed class AssetPoolManager : MonoBehaviour
	{
		[Inject] private DiContainer _container;

		private Transform _cachedTransform;
		private readonly Dictionary<string, PoolObject> _prefabByPath = new();
		private readonly Dictionary<string, List<PoolObject>> _poolObjectsByPath = new();

		private void Awake()
		{
			_cachedTransform = transform;
		}

		private T GetPoolObject<T>(string path, Action<T> onPreprocessing, object modelObject) where T : PoolObject
		{
			if (_poolObjectsByPath.TryGetValue(path, out var poolObjects))
			{
				if (poolObjects.Any())
				{
					var poolObject = poolObjects.Last();
					var result = poolObject as T;
					if (result is not null)
					{
						onPreprocessing?.Invoke(result);
						result.OnGet(modelObject);
						poolObjects.RemoveAt(poolObjects.Count - 1);
					}
					else
					{
						Debug.LogError($"The type of pool object does not match.(Path : {path}, Type : {typeof(T).Name})");
					}
					return result;
				}
			}

			if (_prefabByPath.TryGetValue(path, out var prefab) is false)
			{
				var obj = Addressables  .LoadAssetAsync<GameObject>(path).WaitForCompletion();
				prefab = obj.GetComponent<T>();
				if (prefab == null)
				{
					Destroy(obj);
					Debug.LogError($"PoolObject does not contain '{typeof(T).Name}' script.(Path : {path})");
					return null;
				}

				_prefabByPath[path] = prefab;
			}

			var newPoolObject = _container.InstantiatePrefabForComponent<T>(prefab, _cachedTransform);
			newPoolObject.OnReturn += x => OnReturnPoolObject(path, x);
			onPreprocessing?.Invoke(newPoolObject);
			newPoolObject.OnGet(modelObject);
			return newPoolObject;
		}

		public void Get(string path, object modelObject = default)
		{
			GetPoolObject<PoolObject>(path, null, modelObject);
		}

		public T Get<T>(string path, object modelObject = default) where T : PoolObject
		{
			return GetPoolObject<T>(path, null, modelObject);
		}

		public T Get<T>(string path, Vector3 position, object modelObject = default) where T : PoolObject
		{
			return GetPoolObject<T>(path, x =>
			{
				x.CachedTransform.position = position;
			}, modelObject);
		}

		public T Get<T>(string path, Vector3 position, Quaternion rotation, object modelObject = default) where T : PoolObject
		{
			return GetPoolObject<T>(path, x =>
			{
				x.CachedTransform.SetPositionAndRotation(position, rotation);
			}, modelObject);
		}

		public T Get<T>(string path, Transform parent, Vector3 position, object modelObject = default) where T : PoolObject
		{
			return GetPoolObject<T>(path, x =>
			{
				x.CachedTransform.SetParent(parent);
				x.CachedTransform.position = position;
			}, modelObject);
		}

		public T Get<T>(string path, Transform parent, Vector3 position, Quaternion rotation, object modelObject = default) where T : PoolObject
		{
			return GetPoolObject<T>(path, x => 
			{
				x.CachedTransform.SetParent(parent);
				x.CachedTransform.SetPositionAndRotation(position, rotation);
			}, modelObject);
		}

		private void OnReturnPoolObject(string path, PoolObject poolObject)
		{
			if (_poolObjectsByPath.TryGetValue(path, out var poolObjects) is false)
			{
				poolObjects = new List<PoolObject>();
				_poolObjectsByPath[path] = poolObjects;
			}

			poolObject.CachedTransform.SetParent(_cachedTransform);
			poolObjects.Add(poolObject);
		}
	}
}
