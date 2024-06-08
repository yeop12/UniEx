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
	public sealed class FixedUIManager : MonoBehaviour
	{
		[SerializeField] private FixedUISetting _setting;
		
		private Dictionary<string, int> _canvasOrderByLayerName;
		private readonly Dictionary<Type, FixedUIWindow> _uiWindows = new();
		private readonly Dictionary<Type, CancellationTokenSource> _loadingUiWindows = new();
		
		[Inject] private DiContainer _container;

		private void Awake()
		{
			if (_setting?.LayerNames.Any() ?? false)
			{
				_canvasOrderByLayerName = _setting.LayerNames.Select((layerName, index) => (layerName, index))
					.ToDictionary(x => x.layerName, x => x.index * 1000000);
			}
			else
			{
				_canvasOrderByLayerName = new Dictionary<string, int> { { "Default", 0 } };
			}
		}

		public async UniTask LoadAsync<T>(CancellationToken ct) where T : FixedUIWindow
		{
			var type = typeof(T);
			if (_uiWindows.ContainsKey(type))
			{
				return;
			}

			try
			{
				if (Attribute.GetCustomAttribute(type, typeof(AddressableInfo)) is AddressableInfo addressableInfo)
				{
					var obj = await Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key)
						.WithCancellation(ct);
					var uiComponent = obj.GetComponent<T>();
					if (uiComponent is null)
					{
						Addressables.Release(obj);
						Debug.LogError($"{type.Name} does not contain script.");
					}
					else
					{
						var fixedUIWindow = _container.InstantiatePrefabForComponent<T>(uiComponent, transform);
						_uiWindows.Add(type, fixedUIWindow);
					}
				}
				else
				{
					Debug.LogError($"{type.Name} class does not contain AddressAttribute.");
				}
			}
			catch (Exception e) 
			{
				Debug.LogError(e);
			}
		}
		
		public async UniTask<T> OpenAsync<T>(object modelObject, string layerName = "Default") where T : FixedUIWindow
		{
			var type = typeof(T);
			if (_loadingUiWindows.TryGetValue(type, out var cts))
			{
				cts.Cancel();
			}

			cts = new CancellationTokenSource();
			_loadingUiWindows[type] = cts;

			using var removeLoadingUiWindowAction = new DisposableAction(() =>
			{
				cts.Dispose();
				_loadingUiWindows.Remove(type);
			});

			FixedUIWindow fixedUIWindow = null;

			try
			{
				if (_uiWindows.TryGetValue(type, out fixedUIWindow))
				{
					if (fixedUIWindow.IsOpened)
					{
						Debug.LogError($"{type.Name} is already opened.");
					}
					else
					{
						fixedUIWindow.Open(modelObject);
					}
				}
				else
				{
					if (Attribute.GetCustomAttribute(type, typeof(AddressableInfo)) is AddressableInfo addressableInfo)
					{
						var obj = await Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key)
							.WithCancellation(cts.Token);
						var uiComponent = obj.GetComponent<T>();
						if (uiComponent is null)
						{
							Addressables.Release(obj);
							Debug.LogError($"{type.Name} does not contain script.");
						}
						else
						{
							fixedUIWindow = _container.InstantiatePrefabForComponent<T>(uiComponent, transform);
							_uiWindows.Add(type, fixedUIWindow);
							fixedUIWindow.Open(modelObject);
						}
					}
					else
					{
						Debug.LogError($"{type.Name} class does not contain AddressAttribute.");
					}
				}
			}
			catch (OperationCanceledException)
			{
				fixedUIWindow?.Close(true);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			if (fixedUIWindow is not null)
			{
				fixedUIWindow.Canvas.sortingOrder = _canvasOrderByLayerName[layerName];
				++_canvasOrderByLayerName[layerName];
			}

			return fixedUIWindow as T;
		}

		public T Open<T>(object modelObject, string layerName = "Default") where T : FixedUIWindow
		{
			var type = typeof(T);
			if (_loadingUiWindows.TryGetValue(type, out var cts))
			{
				cts.Cancel();
			}

			FixedUIWindow fixedUIWindow = null;

			try
			{
				if (_uiWindows.TryGetValue(type, out fixedUIWindow))
				{
					if (fixedUIWindow.IsOpened)
					{
						Debug.LogError($"{type.Name} is already opened.");
					}
					else
					{
						fixedUIWindow.Open(modelObject);
					}
				}
				else
				{
					if (Attribute.GetCustomAttribute(type, typeof(AddressableInfo)) is AddressableInfo addressableInfo)
					{
						var obj = Addressables.LoadAssetAsync<GameObject>(addressableInfo.Key).WaitForCompletion();
						var uiComponent = obj.GetComponent<T>();
						if (uiComponent is null)
						{
							Addressables.Release(obj);
							Debug.LogError($"{type.Name} does not contain script.");
						}
						else
						{
							fixedUIWindow = _container.InstantiatePrefabForComponent<T>(uiComponent, transform);
							_uiWindows.Add(type, fixedUIWindow);
							fixedUIWindow.Open(modelObject);
						}
					}
					else
					{
						Debug.LogError($"{type.Name} class does not contain AddressAttribute.");
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			if (fixedUIWindow is not null)
			{
				fixedUIWindow.Canvas.sortingOrder = _canvasOrderByLayerName[layerName];
				++_canvasOrderByLayerName[layerName];
			}

			return fixedUIWindow as T;
		}

		public void Close<T>()
		{
			var type = typeof(T);
			if (_loadingUiWindows.TryGetValue(type, out var cts))
			{
				cts.Cancel();
			}
			else if (_uiWindows.TryGetValue(type, out var uiWindow))
			{
				uiWindow.Close();
			}
			else
			{
				Debug.LogError($"{type.Name} does not loaded.");
			}
		}
	}
}
