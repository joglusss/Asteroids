using Asteroids.Objects;
using Asteroids.SceneManage;
using Asteroids.Ship;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Visual
{
    public class ImmortalBlink : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _shipImage;
        [SerializeField] private float _scale;

        private bool _coroutineFlag;


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
                color.a = Mathf.Abs(Mathf.Sin(time * _scale));
                _shipImage.color = color;

                time += Time.deltaTime;
                yield return null;
            }

            color.a = 1;
            _shipImage.color = color;
        }
    }

}
