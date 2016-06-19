using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITimer : MonoBehaviour 
{
    [SerializeField] private SessionTimer Timer;
    [SerializeField] private Text UIText;

    private void Update()
    {
        UIText.text = Timer.GetFormattedString();
    }
}
