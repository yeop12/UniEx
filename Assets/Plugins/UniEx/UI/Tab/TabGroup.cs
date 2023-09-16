using UnityEngine.UI;

namespace UniEx.UI
{
	public class TabGroup : ToggleGroup
	{
		public int SelectedIndex
		{
			get => m_Toggles.FindIndex(x => x.isOn);
			set
			{
				if (value < 0 || value >= m_Toggles.Count)
				{
					return;
				}

				var toggle = m_Toggles[value];
				if (toggle.isOn)
				{
					toggle.SetIsOnWithoutNotify(false);
				}

				m_Toggles[value].isOn = true;
			}
		}
	}
}
