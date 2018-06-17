using UnityEngine;
using UnityEngine.UI;

public class UITraps : MonoBehaviour
{
    Text myUIText;
    int trapsPlaced = 0;

    private void Start()
    {
        myUIText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        myUIText.text = "Place five traps!";
    }
}