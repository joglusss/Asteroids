using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

public class GameSceneContainerHandlerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameSceneContainerHandler>().AsSingle();
    }
}
