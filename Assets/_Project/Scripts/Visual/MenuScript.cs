using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Asteroids.Objects;
using System;

namespace Asteroids.Visual
{
    public class MenuScript : MonoBehaviour
    {
        [SerializeField] ShipControl shipControl;

        private Action<int> changeHPDelegate;
        private Action<InputAction.CallbackContext> escapeDelegate;
        private void Start()
        {
            changeHPDelegate = (a) => { if (a == 0) GoToMenu(); };
            shipControl.ChangeHPEvent += changeHPDelegate;

            escapeDelegate = (a) => { if (!this.gameObject.activeInHierarchy) GoToMenu(); };
            InputSystem.actions.FindAction("Escape").performed += escapeDelegate;
        }

        private void OnDestroy()
        {
            shipControl.ChangeHPEvent -= changeHPDelegate;
            InputSystem.actions.FindAction("Escape").performed -= escapeDelegate;
        }

        [SerializeField] GameObject MenuContainer;
        [SerializeField] UIIndicators uIIndicators;
        [SerializeField] TMP_Text ScoreText;




        public void GoToMenu()
        {
            ScoreText.text = "SCORE:" + uIIndicators.ScoreCount;
            MenuContainer.SetActive(true);
            GameContainer.SetActive(false);
            
            IResetable.OnStopGame();
        }

        [SerializeField] GameObject GameContainer;
        public void GoToGame()
        {
            MenuContainer.SetActive(false);
            GameContainer.SetActive(true);

            IResetable.OnStartGame();

        }


        public void Exit()
        {
            Application.Quit();
        }

        
    }

    public interface IResetable
    {
        private static List<IResetable> list { get; set; } = new List<IResetable>();

        public static void OnStopGame()
        {
            for (int i = 0; i < list.Count; i++)
            { 
                list[i].StopGame(); 
            }
        }
        public static void OnStartGame()
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].StartGame();
            }
        }

        public void InitialazeIRessetable()
        {
            list.Add(this);
        }

        public void StopGame();
        public void StartGame();
    }
}

