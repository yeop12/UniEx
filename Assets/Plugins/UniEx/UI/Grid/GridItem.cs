namespace UniEx.UI
{
	public abstract class GridItem : UIElement
	{
		public object ModelObject { get; private set; }

		public virtual void OnInit(object modelObject)
		{
			ModelObject = modelObject;
		}
	}
}
