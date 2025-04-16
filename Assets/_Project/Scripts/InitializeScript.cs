using Asteroids.Objects;
using Asteroids.Visual;
using UnityEngine;

namespace Asteroids.Initialize
{
    public class InitializeScript : MonoBehaviour 
    {
        [SerializeField] UIIndicators m_UIIndicators;
        [SerializeField] ShipControl m_ShipControl;
        [SerializeField] ObjectManager m_ObjectManager;

        private void Start() {
            m_ObjectManager.Init();
            m_ShipControl.Init();
            m_UIIndicators.Init();
        }
    }
}
