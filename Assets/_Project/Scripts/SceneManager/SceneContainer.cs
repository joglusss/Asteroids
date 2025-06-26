using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Asteroids.SceneManage
{
    [CreateAssetMenu(fileName = "SceneContainer", menuName = "Scene Container")]
    public class SceneContainer : ScriptableObject, IInitializable
    {
        [field: SerializeField] private string _menuScene;
        [field: SerializeField] private string _gameScene;

        public void Initialize()
        {
            LoadScene(_menuScene);
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }

        public void ReloadCurrentScene() => LoadScene(SceneManager.GetActiveScene().name);

        public void LoadMenuScene() => LoadScene(_menuScene);

        public void LoadGameScene() => LoadScene(_gameScene);
    }

}
