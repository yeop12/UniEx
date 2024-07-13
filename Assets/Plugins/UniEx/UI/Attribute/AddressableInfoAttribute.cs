using System;

namespace UniEx.UI
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AddressableInfoAttribute : Attribute
	{
		public string Key { get; }

		public AddressableInfoAttribute(string key)
		{
			Key = key;
		}
	}
}
