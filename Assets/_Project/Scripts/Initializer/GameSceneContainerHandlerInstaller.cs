using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

public class GameSceneContainerHandlerInstaller : MonoInstaller
{
    [SerializeField] private SceneContainer _sceneContainer;

    public override void InstallBindings()
    {
        Container.Bind<SceneContainer>().FromScriptableObject(_sceneContainer).AsSingle();
        Container.BindInterfacesAndSelfTo<GameSceneContainerHandler>().FromNew().AsSingle();
    }
}
