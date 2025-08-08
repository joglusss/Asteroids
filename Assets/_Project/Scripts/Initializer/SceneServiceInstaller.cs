using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

public class SceneServiceInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
    }
}
