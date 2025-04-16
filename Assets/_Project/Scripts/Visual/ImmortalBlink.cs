using Asteroids.Objects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Visual
{
    public class ImmortalBlink : MonoBehaviour, IResetable
    {
        [SerializeField] Image shipImage;
        [SerializeField] ShipControl shipControl;
        [SerializeField] float scale;

        private Action<int> changeHpDelegate;

        void Start()
        {
            ((IResetable)this).InitialazeIRessetable();
        }

        void ShipBlink()
        {

            IEnumerator TimeCounter()
            {

                Color color = shipImage.color;

                float time = 0.0f;
                while (time < shipControl.ImmortalTime && shipControl.gameObject.activeInHierarchy)
                {
                    color.a = Mathf.Abs(Mathf.Sin(time * scale));
                    shipImage.color = color;

                    time += Time.deltaTime;
                    yield return null;
                }

                color.a = 1;
                shipImage.color = color;
            }

            StartCoroutine(TimeCounter());
        }

        public void StopGame()
        {
            shipControl.ChangeHPEvent -= changeHpDelegate;
        }

        public void StartGame()
        {
            changeHpDelegate = (a) => ShipBlink();

            shipControl.ChangeHPEvent += changeHpDelegate;

        }
    }

}
