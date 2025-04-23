using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace Asteroids.Visual
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject[] MenuContainer;
        [SerializeField] private GameObject[] GameContainer;
        [SerializeField] private UIIndicators uIIndicators;
        [SerializeField] private TMP_Text ScoreText;

        private void Start()
        {
            InputSystem.actions.FindAction("Escape").performed += Escape;
        }

        private void OnDestroy()
        {
            InputSystem.actions.FindAction("Escape").performed -= Escape;
        }

        public void GoToMenu()
        {
            ScoreText.text = "SCORE:" + uIIndicators.ScoreCount;
            foreach (GameObject item in MenuContainer) 
                item.SetActive(true);
            foreach (GameObject item in GameContainer)
                item.SetActive(false);
        }

        public void GoToGame()
        {
            foreach (GameObject item in MenuContainer)
                item.SetActive(false);
            foreach (GameObject item in GameContainer)
                item.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void Escape(InputAction.CallbackContext a) => GoToMenu();
    }
}

