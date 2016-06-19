using UnityEngine;
using System.Collections;
using Photon;

public class MatchEntry : PunBehaviour, IPunObservable {

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    #endregion

    [SerializeField] private MatchEntryAnimation matchEntryAnim;
    private WaitForSeconds waitToStart;

    private IEnumerator Start()
    {
        waitToStart = new WaitForSeconds(5);
        yield return new WaitForEndOfFrame();
        matchEntryAnim.AnimateEntry();
        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart()
    {
        yield return waitToStart;
        if (photonView.isMine) photonView.RPC("StartMatch", PhotonTargets.All);
    }

    [PunRPC]
    public void StartMatch()
    {
        StopAllCoroutines();
        matchEntryAnim.AnimateExit();
        MatchObserver.Instance.StartMatch();
    }
}
