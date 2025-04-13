using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImmortalBlink : MonoBehaviour
{
    [SerializeField] Image shipImage;
    [SerializeField] ShipControl shipControl;
    [SerializeField] float scale;
    void Start()
    {
        shipControl.ChangeHPEvent += (a) => ShipBlink();
    }

    void ShipBlink() {

        IEnumerator TimeCounter() {

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
}
