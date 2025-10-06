using Asteroids.SceneManage;
using UnityEngine;
using Zenject;

namespace Asteroids.Total 
{
    public class SceneContainerInstaller : MonoInstaller
    {
        [SerializeField] public SceneContainer _sceneContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneContainer>().FromScriptableObject(_sceneContainer).AsSingle().NonLazy();
        }
    }
}