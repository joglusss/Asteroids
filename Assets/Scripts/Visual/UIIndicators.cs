using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class UIIndicators : MonoBehaviour, MenuScript.IResetable
{
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] ShipControl m_ShipControl;

    private void Start()
    {
        ObjectManager.singltone.alien.ObjectReturnToQueue += (a) => { ScoreCount += 20; ScoreTextUI(a.transform.position, 20); };
        ObjectManager.singltone.asteroid.ObjectReturnToQueue += (a) => { ScoreCount += 10; ScoreTextUI(a.transform.position, 10); };
        ObjectManager.singltone.smallAsteroid.ObjectReturnToQueue += (a) => { ScoreCount += 5; ScoreTextUI(a.transform.position, 5); };

        ScoreTextQueueClass.ScoreTextContainer = ScoreTextContainer;
        ScoreTextQueue.Initialize();

        ((MenuScript.IResetable)this).InitialazeIRessetable();
    }

    public int ScoreCount { get; private set; }


    [System.Serializable]
    public class ScoreTextQueueClass : MenuScript.IResetable
    {
        [SerializeField] private GameObject ObjectPrefab;

        public static Transform ScoreTextContainer;

        private Queue<TMP_Text> queue;

        public void Initialize()
        {

            if (ObjectPrefab == null)
            {
                Debug.LogError("ObjectPrefab was not found");
                return;
            }

            queue = new Queue<TMP_Text>();

            ((MenuScript.IResetable)this).InitialazeIRessetable();
        }

        private void AddNewObject()
        {
            if (!Instantiate(ObjectPrefab, ScoreTextContainer).TryGetComponent(out TMP_Text newScoreText))
            {
                Debug.LogError("ObjectPrefab doesn't have a component");
                return;
            }

            Queue<TMP_Text> link = queue;

            newScoreText.gameObject.SetActive(false);
            queue.Enqueue(newScoreText);

            StopGameEvent += () => ReturnObject(newScoreText);
        }

        public TMP_Text DrawObject()
        {
            if (queue.Count == 0)
                AddNewObject();

            

            TMP_Text returnScoreText = queue.Dequeue();
            returnScoreText.gameObject.SetActive(true);
            return returnScoreText;
        }

        public TMP_Text ReturnObject(TMP_Text returnScoreText)
        {
            if (queue.Count == 0)
                AddNewObject();

            queue.Enqueue(returnScoreText);
            returnScoreText.gameObject.SetActive(false);
            return returnScoreText;
        }


        event Action StopGameEvent;
        public void StopGame()
        {
            StopGameEvent?.Invoke();
        }

        public void StartGame(){}
    }
    [field: SerializeField] public Transform ScoreTextContainer;
    [field: SerializeField] public ScoreTextQueueClass ScoreTextQueue;

    public void ScoreTextUI(Vector2 position, float count)
    { 
        IEnumerator SetScoreText() { 
            TMP_Text ScoreText = ScoreTextQueue.DrawObject();

            ScoreText.text = "+" + count;
            ScoreText.transform.position = position;

            yield return new WaitForSeconds(1f);

            ScoreTextQueue.ReturnObject(ScoreText);
        }

        if(this.gameObject.activeInHierarchy)
            StartCoroutine(SetScoreText());
    }


    private void Update()
    {
        m_TextMeshPro.text = "HP:" + m_ShipControl.ShipHP + "\n";
        m_TextMeshPro.text += "X:" + m_ShipControl.Position.x + "  Y:" + m_ShipControl.Position.y + "\n";
        m_TextMeshPro.text += "Speed:" + m_ShipControl.Velosity.magnitude + "\n";
        m_TextMeshPro.text += "Angle:" + m_ShipControl.Angle + "\n";
        m_TextMeshPro.text += "Laser cooldown:" + m_ShipControl.CurrentLaserCooldown + "\n";
        m_TextMeshPro.text += "Laser count:" + m_ShipControl.CurrentAvailableLaser + "\n";
        m_TextMeshPro.text += "Score:" + ScoreCount + "\n";
    }

    public void StopGame(){ }

    public void StartGame()
    {
        ScoreCount = 0;
    }
}
