using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace UniEx.UI
{
	public sealed class FixedGrid : MonoBehaviour
	{
		[SerializeField] private GridItem _originalGridItem;

		private readonly List<GridItem> _gridItems = new();
		private List<object> _models;
		private Transform _parent;

		[Inject] private DiContainer _container;

		private void Awake()
		{
			_parent = transform;
		}

		public void Init<T>(IEnumerable<T> infos)
		{
			_models = infos.Cast<object>().ToList();
			var needCount = _models.Count - _gridItems.Count;
			if (needCount > 0)
			{
				ExpandGridCapacity(needCount);
			}

			for (var i = 0; i < _gridItems.Count; ++i)
			{
				var gridItem = _gridItems[i];
				var isActivated = i < _models.Count;
				gridItem.gameObject.SetActive(isActivated);
				if (isActivated)
				{
					gridItem.OnInit(_models[i]);
				}
			}
		}

		private void ExpandGridCapacity(int addCount)
		{
			for (var i = 0; i < addCount; ++i)
			{
				var gridItem = _container.InstantiatePrefabForComponent<GridItem>(_originalGridItem, _parent);
				_gridItems.Add(gridItem);
			}
		}

		public TModel FindModel<TModel>(int index)
		{
			if (index < 0 || index >= _models.Count)
			{
				Debug.LogError($"Index out of range.(Index : {index})");
				return default;
			}

			return (TModel)_models[index];
		}

		public TModel FindModel<TModel>(Predicate<TModel> match)
		{
			return _models.Cast<TModel>().FirstOrDefault(match.Invoke);
		}

		public IEnumerable<TModel> FindModels<TModel>(Predicate<TModel> match)
		{
			return _models.Cast<TModel>().Where(match.Invoke);
		}

		public TGridItem FindGridItem<TGridItem>(int index) where TGridItem : GridItem
		{
			if (index < 0 || index >= _models.Count)
			{
				Debug.LogError($"Index out of range.(Index : {index})");
				return default;
			}

			return _gridItems[index] as TGridItem;
		}

		public TGridItem FindGridItem<TGridItem>(Predicate<TGridItem> match) where TGridItem : GridItem
		{
			return _gridItems.Where(x => x.gameObject.activeSelf).Cast<TGridItem>().FirstOrDefault(match.Invoke);
		}

		public IEnumerable<TGridItem> FindGridItems<TGridItem>(Predicate<TGridItem> match) where TGridItem : GridItem
		{
			return _gridItems.Where(x => x.gameObject.activeSelf).Cast<TGridItem>().Where(match.Invoke);
		}

		public TGridItem FindGridItem<TGridItem, TModel>(Predicate<TModel> match) where TGridItem : GridItem
		{
			return _gridItems.Where(x => x.gameObject.activeSelf).Cast<TGridItem>().FirstOrDefault(x => match.Invoke((TModel)x.ModelObject));
		}

		public IEnumerable<TGridItem> FindGridItems<TGridItem, TModel>(Predicate<TModel> match) where TGridItem : GridItem
		{
			return _gridItems.Where(x => x.gameObject.activeSelf).Cast<TGridItem>().Where(x => match.Invoke((TModel)x.ModelObject));
		}

		public void AddModel(object model)
		{
			_models.Add(model);
			var needCount = _models.Count - _gridItems.Count;
			if (needCount > 0)
			{
				ExpandGridCapacity(needCount);
			}

			var index = _models.Count - 1;
			var gridItem = _gridItems[index];
			gridItem.gameObject.SetActive(true);
			gridItem.OnInit(model);
		}

		public void RemoveModel(object model)
		{
			var index = _models.IndexOf(model);
			if (index < 0)
			{
				Debug.LogError("Model does not exist.");
				return;
			}

			_models.RemoveAt(index);
			for (var i = index; i < _gridItems.Count; ++i)
			{
				var gridItem = _gridItems[i];
				var isActivated = i < _models.Count;
				gridItem.gameObject.SetActive(isActivated);
				if (isActivated)
				{
					gridItem.OnInit(_models[i]);
				}
			}
		}

		public void Clear()
		{
			_models.Clear();
			_gridItems.ForEach(x => x.gameObject.SetActive(false));
		}
	}
}
