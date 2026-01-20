using Asteroids.Objects;
using UnityEngine;
using R3;
using Zenject;

namespace Asteroids.Effect
{
    public class EffectCaller : MonoBehaviour
    {
        [SerializeField] EffectID _onLaunch;
        [SerializeField] EffectID _onDestroy;

        [Inject] EffectPlayer _player;

        private void Awake()
        {
            SpaceObject spaceObject = GetComponent<SpaceObject>();

            spaceObject.OnLaunch
                .Subscribe(_ => _player.Play(_onLaunch, spaceObject.transform.position))
                .AddTo(this);

            spaceObject.OnDestroy
                .Subscribe(_ => _player.Play(_onDestroy, spaceObject.transform.position))
                .AddTo(this);
        }
    }
}
