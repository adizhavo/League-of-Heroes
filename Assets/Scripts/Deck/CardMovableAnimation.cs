using System;
using UnityEngine;

public class CardMovableAnimation : MovableAnimation {

    public override void AnimateEntry(Vector3 pos, Vector3 scale)
    {
        LeanTween.alpha(Graphic, 0f, 0f);
        LeanTween.alpha(Graphic, 1f, 0.4f);
        gameObject.transform.localScale = new Vector3(0.5f, 1f, 1f);
        LeanTween.scale(Graphic, scale, 0.3f);
        gameObject.transform.position = pos + new Vector3(0f, .2f, 0f);
        LeanTween.move(Graphic, pos, 0.3f).setEase(LeanTweenType.easeOutBack);
    }

    public override void AnimateEntryAndSnap(Vector3 pos, Vector3 scale)
    {
        LeanTween.scale(Graphic, scale, 0.4f);
        transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
        LeanTween.move(Graphic, pos, 0.4f).setEase(LeanTweenType.easeInOutQuad);
    }

    public override void AnimateDestroy(Action callback)
    {
        LeanTween.alpha(Graphic, 0f, 0.2f);
        LeanTween.scale(Graphic, new Vector3(0.5f, 1.5f, 1f), 0.2f);
        LeanTween.move(Graphic, transform.position + new Vector3(0f, .5f, 0f), 0.2f).setOnComplete(
            () =>
            {
                if (callback != null) callback();
            }
        );
    }
}
