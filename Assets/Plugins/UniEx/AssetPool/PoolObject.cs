using System;
using UnityEngine;

namespace UniEx
{
	public class PoolObject : MonoBehaviour
	{
		public Transform CachedTransform { get; private set; }

		public event Action<PoolObject> OnReturn;

		protected virtual void Awake()
		{
			CachedTransform = transform;
		}
		
		protected internal virtual void OnGet()
		{
			gameObject.SetActive(true);
		}

		public virtual void Return()
		{
			gameObject.SetActive(false);
			OnReturn?.Invoke(this);
		}
	}
}
