using System;

namespace UniEx.UI
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AddressableInfo : Attribute
	{
		public string Key { get; }

		public AddressableInfo(string key)
		{
			Key = key;
		}
	}
}
