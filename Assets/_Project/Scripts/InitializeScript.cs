using Asteroids.Objects;
using Asteroids.Visual;
using UnityEngine;

namespace Asteroids.Initialize
{
    public class InitializeScript : MonoBehaviour 
    {
        [SerializeField] UIIndicators _uiIndicators;
        [SerializeField] ShipControl _shipControl;
        [SerializeField] ObjectManager _objectManager;

        private void Start() {
            _objectManager.Init();
            _shipControl.Init();
            _uiIndicators.Init();
        }
    }
}
