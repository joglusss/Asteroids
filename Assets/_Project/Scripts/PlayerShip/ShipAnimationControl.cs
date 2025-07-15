using Asteroids.SceneManage;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShipAnimationControl : MonoBehaviour
    {
        [SerializeField] private float _scaleBlinking;

        private Animator _animator;
        private SpriteRenderer _shipImage;
        private GameSceneService _sceneContainerHandler;
        private bool _coroutineFlag;

        [Inject]
        private void Construct(GameSceneService sceneContainerHandler)
        { 
            _sceneContainerHandler = sceneContainerHandler;

            _animator = GetComponent<Animator>();
            _shipImage = GetComponent<SpriteRenderer>();
        }

        public async void Death()
        {
            _animator.SetBool("Death", true);

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                await Task.Yield();

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) 
                await Task.Yield();

            _sceneContainerHandler.GoToMenu();
        }

        public void SwitchBlinking(bool value)
        {
            if (value)
            {
                _coroutineFlag = true;
                StartCoroutine(TimeCounter());
            }
            else
            {
                _coroutineFlag = false;
            }
        }

        private IEnumerator TimeCounter()
        {

            Color color = _shipImage.color;

            float time = 0.0f;
            while (_coroutineFlag && gameObject.activeInHierarchy)
            {
                color.a = Mathf.Abs(Mathf.Sin(time * _scaleBlinking));
                _shipImage.color = color;

                time += Time.deltaTime;
                yield return null;
            }

            color.a = 1;
            _shipImage.color = color;
        }
    }
}
