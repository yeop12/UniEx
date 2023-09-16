using UnityEngine;
using UnityEngine.UI;

namespace UniEx.UI
{
    [RequireComponent(typeof(Toggle))]
    public abstract class Tab : UIElement
    {
        public object ModelObject { get; private set; }

        public virtual void OnInit(object modelObject)
        {
	        ModelObject = modelObject;
        }
    }
}
