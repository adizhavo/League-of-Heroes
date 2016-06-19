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
        matchEntryAnim.AnimateEntry(null);
        StartCoroutine(WaitToStart());
    }

    private void DispatchMatchStart()
    {
       photonView.RPC("StartMatch", PhotonTargets.All);
    }

    private IEnumerator WaitToStart()
    {
        yield return waitToStart;
        matchEntryAnim.AnimateExit(DispatchMatchStart);
    }

    [PunRPC]
    public void StartMatch()
    {
        MatchObserver.Instance.StartMatch();
    }
}
