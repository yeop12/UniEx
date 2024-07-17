using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniEx.UI
{
	public class TabGroup : MonoBehaviour
	{
		private Dictionary<Enum, Tab> _tabByKind;
		private Dictionary<Enum, object> _tabModelByKind = new();
		private Enum _selectedKind;

		public IEnumerable<(Enum kind, object model)> TabInfos
		{
			get => _tabModelByKind.Select(x => (x.Key, x.Value));
			set
			{
				_tabModelByKind = value.ToDictionary(x => x.kind, x => x.model);
			}
		}

		public Enum SelectedKind
		{
			get => _selectedKind;
			set
			{

				if (_selectedKind != null && _tabByKind.TryGetValue(_selectedKind, out var currentTab)) 
				{
					currentTab.Close();
				}

				_selectedKind = value;

				if (value == null) {
					return;
				}

				if (_tabByKind.TryGetValue(value, out var newTab) is false) {
					Debug.LogError($"Tab kind does not exist.(Name : {name})");
					return;
				}

				_tabModelByKind.TryGetValue(value, out var model);

				newTab.OnInit(model);
			}
		}

		private void Awake()
		{
			RefreshTabList(true);
		}

		private void OnDisable()
		{
			CloseAll();
		}

		public void RefreshTabList(bool closeAll)
		{
			_tabByKind = GetComponentsInChildren<Tab>(true).Where(x => x.IsTarget(this))
				.ToDictionary(x => x.Kind, x => x);
			if (closeAll)
			{
				CloseAll();
			}
		}

		private void CloseAll()
		{
			_tabByKind.Values.ForEach(x => x.Close());
		}
	}
}
