using Asteroids.SceneManage;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Installers
{
    public class MenuContainerHandlerInstaller : MonoInstaller
    {
        [SerializeField] private SceneContainer _sceneContainer;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startGameButton;

        public override void InstallBindings()
        {
            Container.Bind<SceneContainer>().FromScriptableObject(_sceneContainer).AsSingle();
            Container.Bind<Button>().WithId("Exit").FromInstance(_exitButton).AsTransient();
            Container.Bind<Button>().WithId("Start").FromInstance(_startGameButton).AsTransient();

            Container.BindInterfacesTo<MenuSceneContainerHandler>().FromNew().AsSingle();
        }
    }

}

