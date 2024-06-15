using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Animator))]
    public class AnimatorBinder : UIBinder<Animator>
    {
	    [SerializeField] private string _boolParameterName;
	    [SerializeField] private string _intParameterName;
	    [SerializeField] private string _floatParameterName;
	    [SerializeField] private string _triggerParameterName;
		
		protected override void Awake()
		{
			base.Awake();
			AddParameter<(string name, bool value)>(_boolParameterName, x =>
			{
				if (string.IsNullOrWhiteSpace(x.name) is false)
				{
					UIComponent.SetBool(x.name, x.value);
				}
			});
			AddParameter<(string name, int value)>(_intParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x.name) is false) 
				{
					UIComponent.SetInteger(x.name, x.value);
				}
			});
			AddParameter<(string name, float value)>(_floatParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x.name) is false) 
				{
					UIComponent.SetFloat(x.name, x.value);
				}
			});
			AddParameter<string>(_triggerParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x) is false) 
				{
					UIComponent.SetTrigger(x);
				}
			});
		}
    }
}
