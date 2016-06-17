using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {
    [SerializeField] private FloatingMovement dropMovement;
    [SerializeField] private FloatingVisual dropValue;

    public void Display(Vector3 startPos, float direction, string text)
    {
        dropMovement.Initialize(startPos, direction);
        dropValue.SetText(text);
    }

    public void Display(Vector3 startPos, float direction, string text, Color color)
    {
        dropMovement.Initialize(startPos, direction);
        dropValue.SetText(text, color);
    }
}
