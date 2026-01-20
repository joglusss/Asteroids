using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Asteroids.SceneManage;
using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Total 
{
    public class SceneContainerInstaller : MonoInstaller
    {
        [SerializeField] private SceneContainer _sceneContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneContainer>().FromScriptableObject(_sceneContainer).AsSingle().NonLazy();
            
            LoadNextScene(Container.Resolve<List<IReadyFlag>>(),this.GetCancellationTokenOnDestroy()).Forget();
        }
        
        public async UniTaskVoid LoadNextScene(List<IReadyFlag> list, CancellationToken token)
        {
            while(list.Any(x => x.IsReady == false))
                await UniTask.Yield(token);
        
            _sceneContainer.LoadMenuScene();
        }
        
    }
}