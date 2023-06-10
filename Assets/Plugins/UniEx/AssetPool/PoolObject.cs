using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniEx
{
	public class PoolObject : MonoBehaviour
	{
		private readonly List<IDisposable> _disposers = new();
		public Transform CachedTransform { get; private set; }

		public event Action<PoolObject> OnReturn;
		                        
		protected virtual void Awake()
		{
			CachedTransform = transform;
		}
		
		protected internal virtual void OnGet(object modelObject)
		{
			gameObject.SetActive(true);
		}

		protected void AddDisposer(IDisposable disposer)
		{
			_disposers.Add(disposer);
		}

		public virtual void Return()
		{
			gameObject.SetActive(false);
			OnReturn?.Invoke(this);
			_disposers.ForEach(x => x.Dispose());
			_disposers.Clear();
		}
	}
}
