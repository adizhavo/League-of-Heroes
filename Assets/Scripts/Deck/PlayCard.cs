using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour, Movable, Card {

    #region Card Implementation
    [SerializeField] private int manaCost;
    [SerializeField] private string callCode;

    public int ManaCost { get { return manaCost; } }
    public string ContentCallCode { get { return callCode; } }
    #endregion

    #region Movable Implementation
    private Vector3 movePos;
    private Vector3 scale;

    public void Position(Vector3 movePos, Vector3 scale, bool snap = false)
    {
        this.movePos = movePos;
        this.scale = scale;

        if (snap)
        {
            transform.position = movePos;
            transform.localScale = scale;
        }
    }
    #endregion

    [SerializeField] private float lerpSpeed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * lerpSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime * lerpSpeed);
    }
}
