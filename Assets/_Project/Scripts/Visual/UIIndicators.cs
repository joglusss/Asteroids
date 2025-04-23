using UnityEngine;
using TMPro;
using System.Collections;
using Asteroids.Objects;
using Asteroids.Ship;
using Asteroids.Helpers;
using System.Collections.Generic;

namespace Asteroids.Visual
{
    public class UIIndicators : MonoBehaviour
    {
        [SerializeField] ObjectManager _objectManager;
        [SerializeField] TMP_Text _textMeshPro;
        [SerializeField] ShipControl _shipControl;
        [SerializeField] ShipStat _shipStat;
        [SerializeField] ShipWeapon _shipWeapon;
        [SerializeField] Transform _scoreTextContainer;
        [SerializeField] GameObject _scoreTextPrefab;

        private List<TMP_Text> _scoreTextOnScene = new List<TMP_Text>();
        private ScoreTextQueue _scoreTextQueue;
        private Camera _camera;

        public int ScoreCount { get; private set; }

        public void Init()
        {
            _scoreTextQueue = new ScoreTextQueue(_scoreTextPrefab, _scoreTextContainer);
            _camera = Camera.main;
        }

        private void Update()
        {
            _textMeshPro.text = "HP:" + _shipStat.HP + "\n";
            _textMeshPro.text += "X:" + _shipControl.Position.x + "  Y:" + _shipControl.Position.y + "\n";
            _textMeshPro.text += "Speed:" + _shipControl.Velosity.magnitude + "\n";
            _textMeshPro.text += "Angle:" + _shipControl.Angle + "\n";
            _textMeshPro.text += "Laser cooldown:" + _shipWeapon.CurrentLaserCooldown + "\n";
            _textMeshPro.text += "Laser count:" + _shipWeapon.CurrentAvailableLaser + "\n";
            _textMeshPro.text += "Score:" + ScoreCount + "\n";
        }

        private void OnEnable() 
        {
            _objectManager.AlienQueue.ObjectReturnToQueue += ScoreAlien;
            _objectManager.AsteroidQueue.ObjectReturnToQueue += ScoreAsteroid;
            _objectManager.SmallAsteroidQueue.ObjectReturnToQueue += ScoreSmallAsteroid;

            ScoreCount = 0;
        }

        private void OnDisable()
        {
            _objectManager.AlienQueue.ObjectReturnToQueue -= ScoreAlien;
            _objectManager.AsteroidQueue.ObjectReturnToQueue -= ScoreAsteroid;
            _objectManager.SmallAsteroidQueue.ObjectReturnToQueue -= ScoreSmallAsteroid;

            StopAllCoroutines();
            foreach (TMP_Text item in _scoreTextOnScene)
                _scoreTextQueue.ReturnObject(item);
            _scoreTextOnScene.Clear();
        }

        public void ScoreTextUI(Vector2 position, float count)
        {
            position = _camera.WorldToScreenPoint(position);

            if (this.gameObject.activeInHierarchy)
                StartCoroutine(SetScoreText(position, count));
        }

        private IEnumerator SetScoreText(Vector2 position, float count)
        {
            TMP_Text ScoreText = _scoreTextQueue.DrawObject();
            _scoreTextOnScene.Add(ScoreText);

            ScoreText.text = "+" + count;
            ScoreText.transform.position = position;

            yield return new WaitForSeconds(1f);

            _scoreTextOnScene.Remove(ScoreText);
            _scoreTextQueue.ReturnObject(ScoreText);
        }

        private void ScoreAlien(SpaceObject a) { ScoreCount += 20; ScoreTextUI(a.transform.position, 20); }

        private void ScoreAsteroid(SpaceObject a) { ScoreCount += 10; ScoreTextUI(a.transform.position, 10); }

        private void ScoreSmallAsteroid(SpaceObject a) { ScoreCount += 5; ScoreTextUI(a.transform.position, 5); }
    }
}

