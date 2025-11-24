using Asteroids.SceneManage;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Asteroids.Ship
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShipAnimationControl : MonoBehaviour
    {
        [SerializeField] private float _scaleBlinking;

        private Animator _animator;
        private SpriteRenderer _shipImage;

        private bool _timerFlag;

        private void Start()
        { 
            

            _animator = GetComponent<Animator>();
            _shipImage = GetComponent<SpriteRenderer>();
        }

        public async UniTask Death(CancellationToken token)
        {
            _animator.SetBool("Death", true);

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                await UniTask.Yield(token);

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) 
                await UniTask.Yield(token);
        }

        public void SwitchBlinking(bool value)
        {
            if (value)
            {
                _timerFlag = true;
                TimeCounter(this.destroyCancellationToken).Forget();
            }
            else
            {
                _timerFlag = false;
            }
        }

        private async UniTaskVoid TimeCounter(CancellationToken token)
        {
            Color color = _shipImage.color;

            float time = 0.0f;
            while (_timerFlag && gameObject.activeInHierarchy)
            {
                color.a = Mathf.Abs(Mathf.Sin(time * _scaleBlinking));
                _shipImage.color = color;

                time += Time.deltaTime;
                await UniTask.NextFrame(token);
            }

            color.a = 1;
            _shipImage.color = color;
        } 
    }
}
