using UnityEngine;

namespace UniEx.UI
{
	[RequireComponent(typeof(Animator))]
    public class AnimatorBinder : UIBinder<Animator>
    {
	    [GetterBinderType(typeof((string, bool)))] [SerializeField] private string _boolParameterName;
		[GetterBinderType(typeof((string, int)))] [SerializeField] private string _intParameterName;
		[GetterBinderType(typeof((string, float)))] [SerializeField] private string _floatParameterName;
		[GetterBinderType(typeof(string))] [SerializeField] private string _triggerParameterName;
		
		protected override void Awake()
		{
			base.Awake();
			AddGetterParameter<(string name, bool value)>(_boolParameterName, x =>
			{
				if (string.IsNullOrWhiteSpace(x.name) is false)
				{
					UIComponent.SetBool(x.name, x.value);
				}
			});
			AddGetterParameter<(string name, int value)>(_intParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x.name) is false) 
				{
					UIComponent.SetInteger(x.name, x.value);
				}
			});
			AddGetterParameter<(string name, float value)>(_floatParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x.name) is false) 
				{
					UIComponent.SetFloat(x.name, x.value);
				}
			});
			AddGetterParameter<string>(_triggerParameterName, x => 
			{
				if (string.IsNullOrWhiteSpace(x) is false) 
				{
					UIComponent.SetTrigger(x);
				}
			});
		}
    }
}
