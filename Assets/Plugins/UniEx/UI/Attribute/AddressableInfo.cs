using System;

namespace UniEx.UI
{
	public class AddressableInfo : Attribute
	{
		public string Key { get; }

		public AddressableInfo(string key)
		{
			Key = key;
		}
	}
}
