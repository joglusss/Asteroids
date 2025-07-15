using Asteroids.SceneManage;
using System;
using UnityEngine;
using Zenject;

namespace Asteroids.Installers
{
    public class MenuSceneServiceInstaller : MonoInstaller
    {
        [SerializeField] private MenuSceneService _menuUIPrefab;

        public override void InstallBindings()
        {
            System.Type[] types = new System.Type[]
          {
                typeof(MenuSceneService),
                typeof(IInitializable),
                typeof(IDisposable)
          };
            Container.Bind(types).FromComponentsInNewPrefab(_menuUIPrefab).AsSingle();
        }
    }
}

