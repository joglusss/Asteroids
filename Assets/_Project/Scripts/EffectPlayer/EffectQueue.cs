using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Effect
{
    class EffectQueue
    {
        public readonly EffectID ID;

        private readonly ParticleSystem _particleSystem;
        private Queue<ParticleSystem> _queue = new();

        public EffectQueue(EffectID id, ParticleSystem particleSystem)
        { 
            ID = id;
            _particleSystem = particleSystem;
        }

        public void Play(Vector3 position)
        {
            if (_queue.Count == 0)
            {
                InstantiateEffect();
            }

            ParticleSystem particleSystem = _queue.Dequeue();

            particleSystem.transform.position = position;
            LifeTime(particleSystem).Forget();
        }

        private void InstantiateEffect()
        {
            _queue.Enqueue(UnityEngine.Object.Instantiate(_particleSystem));
        }

        private async UniTaskVoid LifeTime(ParticleSystem effect)
        {
            effect.gameObject.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(effect.main.duration), cancellationToken: effect.gameObject.GetCancellationTokenOnDestroy());

            effect.gameObject.SetActive(false);
            _queue.Enqueue(effect);
        }
    }
}
