using System;
using System.Collections;
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

		public void Init(IEnumerable infos)
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

		public Coroutine Scroll(Direction direction, float time = 0.5f, float offset = 0.0f)
		{
			var rectTransform = GetComponent<RectTransform>();

			var startPosition = rectTransform.anchoredPosition;
			var parentRect = transform.parent.GetComponent<RectTransform>().rect;
			var endPosition = direction switch
			{
				Direction.Up => new Vector2(startPosition.x, -offset),
				Direction.Down => new Vector2(startPosition.x, Mathf.Max(0, rectTransform.rect.height - parentRect.height + offset)),
				Direction.Right => new Vector2(Mathf.Min(0, parentRect.width - rectTransform.rect.width) - offset, startPosition.y),
				Direction.Left => new Vector2(offset, startPosition.y),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};

			IEnumerator UpdateScroll()
			{
				for (var t = 0.0f; t < time; t += Time.deltaTime)
				{
					rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t / time);
					yield return null;
				}

				rectTransform.anchoredPosition = endPosition;
			}

			return StartCoroutine(UpdateScroll());
		}
	}
}
