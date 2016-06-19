using UnityEngine;
using System.Collections;

public class SoldierHPBar : MonoBehaviour 
{
    [SerializeField] private int maxHp;
    public int MaxHp { get { return maxHp; } }

    [SerializeField] private Transform CurrentHp;
    [SerializeField] private Transform Damage;

    private Damagable unitDamage;

    public void Init(Damagable unitDamage)
    {
        this.unitDamage = unitDamage;
    }

    private void Update()
    {
        float xScale = unitDamage.GetHp() / maxHp;
        xScale = Mathf.Clamp01(xScale);

        CurrentHp.localScale = new Vector3(xScale, 1f, 1f);
        Damage.localScale = Vector3.Lerp(Damage.localScale, CurrentHp.localScale, Time.deltaTime * 3);
    }
}