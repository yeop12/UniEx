namespace UniEx.UI
{
	public class ControlItem : UIElement
	{
		public object ModelObject { get; private set; }

		public virtual void OnInit( object modelObject ) 
		{
			ModelObject = modelObject;
		}
	}
}
