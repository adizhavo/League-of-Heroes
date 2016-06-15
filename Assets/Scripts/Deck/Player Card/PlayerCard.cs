using UnityEngine;
using System.Collections;

public class PlayerCard : MonoBehaviour, Movable, Card {

    #region Card Implementation
    [SerializeField] private int manaCost;
    [SerializeField] private string objectSpawnCodeCall;

    public int ManaCost { get { return manaCost; } }
    public string ObjectSpawnCodeCall { get { return objectSpawnCodeCall; } }

    private enum States
    {
        InDeck,
        Presented,
        Discarted
    }
    private States cardState;

    private bool isEnabled = false;

    public void Deploy(GridCell deployCell)
    {
        GameObject Unit = ObjectFactory.Instance.CreateObjectCode(objectSpawnCodeCall);
        if (Unit == null) return;

        Deployable d = Unit.GetComponent<Deployable>();
        if (d == null){ Unit = null; return; }

        d.Deploy(deployCell);
        d = null;
        Unit = null;
    }

    public void Discard()
    {
        cardState = States.Discarted;
        Destroy( gameObject );
    }

    public bool IsEnabled()
    {
        return isEnabled;
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
    [SerializeField] private Sprite cardSprite;
    public Sprite CardSprite { get { return cardSprite; } } 

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