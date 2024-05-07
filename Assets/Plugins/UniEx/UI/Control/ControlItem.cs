namespace UniEx.UI
{
	public abstract class ControlItem : UIElement
	{
		public object ModelObject { get; private set; }

		public virtual void OnInit( object modelObject ) 
		{
			ModelObject = modelObject;
		}
	}
}
