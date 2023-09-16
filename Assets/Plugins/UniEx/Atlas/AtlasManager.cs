using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace UniEx
{
	public class AtlasManager
	{
		private readonly Dictionary<string, SpriteAtlas> _atlases = new();
		private readonly Dictionary<string, UniTaskCompletionSource<SpriteAtlas>> _loadingAtlases = new();
		
		public Sprite GetSprite(string atlasName, string spriteName)
		{
			if (_atlases.TryGetValue(atlasName, out var atlas))
			{
				return atlas.GetSprite(spriteName);
			}

			try
			{
				var path = $"Atlas/{atlasName}";
				atlas = Addressables.LoadAssetAsync<SpriteAtlas>(path).WaitForCompletion();
				_atlases.Add(atlasName, atlas);
				return atlas.GetSprite(spriteName);
			}
			catch (Exception e)
			{
				Debug.LogError($"Loading addressable is failed.(Msg : {e})");
			}

			return null;
		}

		public async UniTask<Sprite> GetSpriteAsync(string atlasName, string spriteName)
		{
			if (_atlases.TryGetValue(atlasName, out var atlas))
			{
				return atlas.GetSprite(spriteName);
			}

			try
			{
				var path = $"Atlas/{atlasName}";
				if (_loadingAtlases.TryGetValue(atlasName, out var loadingTcs))
				{
					atlas = await loadingTcs.Task;
					return atlas.GetSprite(spriteName);
				}
				else
				{
					loadingTcs = new UniTaskCompletionSource<SpriteAtlas>();
					_loadingAtlases.Add(atlasName, loadingTcs);
					atlas = await Addressables.LoadAssetAsync<SpriteAtlas>(path);
					_atlases.Add(atlasName, atlas);
					loadingTcs.TrySetResult(atlas);
					_loadingAtlases.Remove(atlasName);
					return atlas.GetSprite(spriteName);
				}
			}
			catch (Exception e)
			{
				Debug.LogError($"Loading addressable is failed.(Msg : {e})");
			}

			return null;
		}

		public void Clear()
		{
			_atlases.Values.ForEach(Object.Destroy);
			_atlases.Clear();
		}
	}
}
