using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Asteroids.Effect
{
    public class EffectPlayer
    {
        private Dictionary<EffectID, EffectQueue> _particles = new();

        [Inject]
        private void Construct(
            [Inject(Id = EffectID.ShotSparks)] ParticleSystem shotSpark,
            [Inject(Id = EffectID.Explode)] ParticleSystem explode,
            [Inject(Id = EffectID.SmallExplode)] ParticleSystem smallExplode
            )
        {
            _particles.Add(EffectID.ShotSparks, new EffectQueue(EffectID.ShotSparks, shotSpark));
            _particles.Add(EffectID.Explode, new EffectQueue(EffectID.Explode, explode));
            _particles.Add(EffectID.SmallExplode, new EffectQueue(EffectID.SmallExplode, smallExplode));
        }

        public void Play(EffectID id, Vector3 position)
        {
            _particles[id].Play(position);
        }
    }
}