using UnityEngine;
using System.Collections;

public class CardPositioner : MonoBehaviour {

    [SerializeField] private Transform[] cardPositions;

    public Vector3 GetPosition(int index)
    {
        return index < cardPositions.Length ? cardPositions[index].position : Vector3.zero;
    }
}
