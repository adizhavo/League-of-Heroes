using UnityEngine;
using System.Collections;

public class PlayerCard : MonoBehaviour, Movable, Card {

    #region Card Implementation
    [SerializeField] private int manaCost;
    [SerializeField] private string callCode;

    public int ManaCost { get { return manaCost; } }
    public string ContentCallCode { get { return callCode; } }

    private enum States
    {
        InDeck,
        Presented,
        Discarted
    }
    private States cardState;

    private bool isEnabled = false;

    public bool IsEnabled()
    {
        return isEnabled;
    }

    public void Discard()
    {
        cardState = States.Discarted;
        Destroy( gameObject );
    }
    #endregion

    #region Movable Implementation
    private Vector3 movePos;
    private Vector3 scale;

    public void Position(Vector3 movePos, Vector3 scale, bool snap = false)
    {
        cardState = States.Presented;

        this.movePos = movePos;
        this.scale = scale;

        if (snap)
        {
            transform.position = movePos;
            transform.localScale = scale;
        }
    }

    public bool IsDestroyed()
    {
        return cardState.Equals(States.Discarted);
    }
    #endregion

    [SerializeField] private float lerpSpeed;

    private void Awake()
    {
        cardState = States.InDeck;
        ManaBar.OnManaLoaded += ChangeState;
    }

    private void OnDestroy()
    {
        ManaBar.OnManaLoaded -= ChangeState;
    }

    private void ChangeState(int block)
    {
        isEnabled = block >= manaCost;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * lerpSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime * lerpSpeed);
    }
}