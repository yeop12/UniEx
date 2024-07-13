using System;

namespace UniEx.UI
{
	public class SetterBinderTypeAttribute : BinderTypeAttribute
	{
		private readonly Type _type;
		public override bool IsGetter => false;

		public SetterBinderTypeAttribute(Type type )
		{
			_type = type;
		}

		public override bool IsTargetType(Type type) => _type == type;
	}
}
