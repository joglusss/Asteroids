using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class MenuContainerHandlerInstaller : MonoInstaller
    {
        [SerializeField] private MenuSceneContainerHandler _menuUIPrefab;

        public override void InstallBindings()
        {
            System.Type[] types = new System.Type[]
          {
                typeof(MenuSceneContainerHandler),
                typeof(IInitializable),
                typeof(ILateDisposable)
          };
            Container.Bind(types).FromComponentsInNewPrefab(_menuUIPrefab).AsSingle();
        }
    }
}

