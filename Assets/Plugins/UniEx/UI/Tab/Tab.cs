using System;
using UnityEngine;

namespace UniEx.UI
{
    public abstract class Tab : UIElement
    {
	    [SerializeField] private TabGroup _tabGroup;

        public object ModelObject { get; private set; }
        public abstract Enum Kind { get; }

        public virtual void OnInit(object modelObject)
        {
	        gameObject.SetActive(true);
	        ModelObject = modelObject;
        }

        public bool IsTarget(TabGroup tabGroup) => _tabGroup == tabGroup;

        public virtual void Close()
        {
	        gameObject.SetActive(false);
            InvokeUnbind();
        }
    }
}
