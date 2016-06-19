using System;
using UnityEngine;

public class MovableAnimation : MonoBehaviour {

    [SerializeField] protected GameObject Graphic;

    public virtual void AnimateEntry(Vector3 pos, Vector3 scale)
    {
        LeanTween.alpha(Graphic, 0f, 0f);
        LeanTween.alpha(Graphic, 1f, 0.15f);
        Graphic.transform.localScale = new Vector3(0.2f, 2f, 1f);
        LeanTween.scale(Graphic, Vector3.one, 0.25f);
        Graphic.transform.localPosition += new Vector3(0f, 2f, 0f);
        LeanTween.moveLocal(Graphic, Vector3.zero, 0.25f);
    }

    public virtual void AnimateEntryAndSnap(Vector3 pos, Vector3 scale)
    {
        AnimateEntry(pos, scale);
    }

    public virtual void AnimateDestroy(Action callback)
    {
        LeanTween.alpha(Graphic, 0f, 0.15f);
        LeanTween.scale(Graphic, new Vector3(2f, 0.2f, 1f), 0.3f).setOnComplete( 
            () =>
            {
                if (callback != null) callback();
            }
        );
    }
}
