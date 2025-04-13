using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    [SerializeField] ShipControl shipControl;
    private void Start()
    {
        shipControl.ChangeHPEvent += (a) => { if (a == 0) GoToMenu(); };
        InputSystem.actions.FindAction("Escape").performed += (a) => { if (!this.gameObject.activeInHierarchy) GoToMenu(); };
    }


    [SerializeField] GameObject MenuContainer;
    [SerializeField] UIIndicators uIIndicators;
    [SerializeField] TMP_Text ScoreText;


    private bool isFirstStart = true;

    public void GoToMenu() {
        ScoreText.text = "SCORE:" + uIIndicators.ScoreCount;
        MenuContainer.SetActive(true);
        GameContainer.SetActive(false);
        IResetable.OnStopGame();
    }

    [SerializeField] GameObject GameContainer;
    public void GoToGame() {
        MenuContainer.SetActive(false);
        GameContainer.SetActive(true);
        if (!isFirstStart)
        {
            IResetable.OnStartGame();
        }
        isFirstStart = false;
    }

    
    public void Exit() {
        Application.Quit();
    }

    public interface IResetable {


        private static List<IResetable> list { get; set; } = new List<IResetable>();

        public static void OnStopGame()
        {
            foreach (IResetable i in list)
            {
                i.StopGame();
            }
        }
        public static void OnStartGame()
        {
            foreach (IResetable i in list)
            {
                i.StartGame();
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


