using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

public class GameSceneServiceInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameSceneService>().AsSingle();
    }
}
