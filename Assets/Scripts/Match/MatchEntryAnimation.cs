using System;
using UnityEngine;
using UnityEngine.UI;

public class MatchEntryAnimation : MonoBehaviour {

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Opponent;
    [SerializeField] private GameObject VS;
    [SerializeField] private GameObject BackFade;
    [SerializeField] private Text playerText;
    [SerializeField] private Text opponentText;

    private void Start()
    {
        playerText.text = PlayerInRoom.Instance.Player.name;
        opponentText.text = PlayerInRoom.Instance.Opponent.name;
    }

    public void AnimateEntry(Action callback)
    {
        VS.SetActive(true);
        BackFade.SetActive(true);
        Player.SetActive(true);
        Opponent.SetActive(true);

        LeanTween.alpha(BackFade, 0f, 0f);
        LeanTween.alpha(BackFade, 0f, 0.3f).setDelay(Mathf.Epsilon);

        VS.transform.localScale = Vector3.zero;
        LeanTween.alpha(VS, 0f, 0f);
        LeanTween.alpha(VS, 0f, 0.3f).setDelay(Mathf.Epsilon);
        LeanTween.scale(VS, Vector3.one, 0.3f);

        Vector3 playerPos = Player.transform.position;
        Player.transform.position += new Vector3(5f, 0f, 0f);
        LeanTween.move(Player, playerPos, 0.3f);

        Vector3 opponentPos = Opponent.transform.position;
        Opponent.transform.position += new Vector3(-5f, 0f, 0f);
        LeanTween.move(Opponent, opponentPos, 0.3f).setOnComplete(
            () =>
            {
                if (callback != null)
                {
                    callback();
                    callback = null;
                }
            }
        );
    }

    public void AnimateExit(Action callback)
    {
        LeanTween.alpha(VS, 0f, 0.3f);
        LeanTween.alpha(BackFade, 0f, 0.3f);
        LeanTween.scale(VS, Vector3.zero, 0.3f);

        LeanTween.move(Player, Player.transform.position + new Vector3(-5f, 0f, 0f), 0.3f);
        LeanTween.move(Opponent, Opponent.transform.position + new Vector3(5f, 0f, 0f), 0.3f).setOnComplete(
            () =>
            {
                VS.SetActive(false);
                Player.SetActive(false);
                Opponent.SetActive(false);
                BackFade.SetActive(false);

                if (callback != null)
                {
                    callback();
                    callback = null;
                }
            }
        );
    }
}
