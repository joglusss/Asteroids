using Asteroids.Input;
using UnityEngine;
using Zenject;

public class InputStorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInputStorage>().AsSingle();
    }
}
