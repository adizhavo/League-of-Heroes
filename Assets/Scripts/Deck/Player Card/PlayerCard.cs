using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;

public class PlayerCard : MonoBehaviour, Card {

    #region Card Implementation
    [SerializeField] private int manaCost;
    [SerializeField] private string objectSpawnCodeCall;

    public int ManaCost { get { return manaCost; } }
    public string ObjectSpawnCodeCall { get { return objectSpawnCodeCall; } }

    private enum States
    {
        InDeck,
        Preview,
        Presented,
        Discarted
    }
    private States cardState;
    private bool isEnabled = false;

    public void Present()
    {
        cardState = States.Preview;
    }

    public void Enable()
    {
        cardState = States.Presented;
    }

    public bool IsEnabled()
    {
        return cardState.Equals(States.Presented) && isEnabled;
    }

    public void Deploy(GridCell deployCell)
    {
        GameObject Unit = PhotonNetwork.Instantiate(objectSpawnCodeCall, deployCell.transform.position, Quaternion.identity, 0);
        if (Unit == null) return;
        if (deployCell.HasObstacle()) { PhotonNetwork.Destroy(Unit); return; }

        Deployable d = Unit.GetComponent<Deployable>();
        if (d == null){ Unit = null; return; }

        d.InitialDeploy(deployCell.CellId);
        d = null;
        Unit = null;
    }

    public void Discard()
    {
        cardState = States.Discarted;
        Destroy( gameObject );
    }
    #endregion

    #region Movable Implementation

    public void Position(Vector3 movePos, Vector3 scale, bool snap = false)
    {
        if (snap)
        {
            LeanTween.alpha(gameObject, 0f, 0f);
            LeanTween.alpha(gameObject, 1f, 0.2f);
            gameObject.transform.localScale = new Vector3(0.5f, 1.5f, 1f);
            LeanTween.scale(gameObject, scale, 0.3f);
            gameObject.transform.position = movePos + new Vector3(0f, .5f, 0f);
            LeanTween.move(gameObject, movePos, 0.3f).setEase(LeanTweenType.easeOutBack);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, movePos.y, transform.position.z);
            LeanTween.scale(gameObject, scale, 0.4f);
            LeanTween.move(gameObject, movePos, 0.4f).setEase(LeanTweenType.easeInOutQuad);
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

//    private void Update()
//    {
//        transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * lerpSpeed);
//        transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime * lerpSpeed);
//    }
}