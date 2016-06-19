using UnityEngine;
using System.Collections;

public class SoldierHpUI : MonoBehaviour {

    [SerializeField] private Transform CurrentHp;
    [SerializeField] private Transform Damage;

    public void Init(Transform parent, Vector3 localPos, Vector3 LocalScale)
    {
        transform.SetParent(parent);
        transform.localPosition = localPos;
        transform.localScale = LocalScale;
    }

    public void SetUIBar(float scaleFloat)
    {
        scaleFloat = Mathf.Clamp01(scaleFloat);

        CurrentHp.localScale = new Vector3(scaleFloat, 1f, 1f);
        Damage.localScale = Vector3.Lerp(Damage.localScale, CurrentHp.localScale, Time.deltaTime * 3);
    }

    public void Release()
    {
        gameObject.SetActive(false);
    }
}
