using Asteroids.Objects;
using Asteroids.Ship;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Visual
{
    public class ImmortalBlink : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _shipImage;
        [SerializeField] ShipStat _shipStat;
        [SerializeField] float _scale;

        private void OnEnable()
        {
            _shipStat.HealthChanged += ShipBlink;
        }

        private void OnDisable()
        {
            _shipStat.HealthChanged -= ShipBlink;
        }

        private IEnumerator TimeCounter()
        {

            Color color = _shipImage.color;

            float time = 0.0f;
            while (_shipStat.IsImmortal && _shipStat.gameObject.activeInHierarchy)
            {
                color.a = Mathf.Abs(Mathf.Sin(time * _scale));
                _shipImage.color = color;

                time += Time.deltaTime;
                yield return null;
            }

            color.a = 1;
            _shipImage.color = color;
        }

        private void ShipBlink(int HP)
        {
            if (HP == 0) return;
            StartCoroutine(TimeCounter());
        }
    }

}
