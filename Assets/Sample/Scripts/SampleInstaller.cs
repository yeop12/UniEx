using UniEx.UI;
using UnityEngine;
using Zenject;

public class SampleInstaller : MonoInstaller
{
	[SerializeField] private FixedUIManager _fixedUIManager;
	[SerializeField] private SampleModel _sampleModel;

    public override void InstallBindings()
    {
	    Container.Bind<FixedUIManager>().FromInstance(_fixedUIManager).AsSingle().NonLazy();
    }

    private void Awake()
    {
	    _fixedUIManager.Open<SampleWindow>(_sampleModel);
    }
}