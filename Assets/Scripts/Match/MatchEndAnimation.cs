using System;
using UnityEngine;
using UnityEngine.UI;

public class MatchEndAnimation : MonoBehaviour {

    [SerializeField] private GameObject Result;
    [SerializeField] private GameObject BackFade;
    [SerializeField] private Text winnerText;

    public void Display(string text)
    {
        winnerText.text = text.ToUpper();
    }

    public void AnimateEntry(Action callback = null)
    {
        BackFade.SetActive(true);
        Result.SetActive(true);

        LeanTween.alpha(BackFade, 0f, 0f);
        LeanTween.alpha(BackFade, 0f, 0.3f).setDelay(Mathf.Epsilon);

        Vector3 playerPos = Result.transform.position;
        Result.transform.localScale = Vector3.zero;
        LeanTween.scale(Result, Vector3.one, 0.3f).setOnComplete(
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

    public void AnimateExit(Action callback = null)
    {
        LeanTween.alpha(BackFade, 0f, 0.3f);
        LeanTween.move(Result, Vector3.zero, 0.3f).setOnComplete(
            () =>
            {
                Result.SetActive(false);
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
