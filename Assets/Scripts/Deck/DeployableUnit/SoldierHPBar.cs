using UnityEngine;
using System.Collections;

public class SoldierHPBar : MonoBehaviour 
{
    [SerializeField] private int maxHp;
    public int MaxHp { get { return maxHp; } }

    [SerializeField] private GameObject HpBarPrefab;
    [SerializeField] private Vector3 HpLocalPos;
    [SerializeField] private Vector3 HpLocalScale;

    private SoldierHpUI HpBarGraphic;

    private Damagable unitDamage;

    public void Init(Damagable unitDamage)
    {
        this.unitDamage = unitDamage;
    }

    private void Start()
    {
        HpBarGraphic = (Instantiate(HpBarPrefab) as GameObject).GetComponent<SoldierHpUI>();
        HpBarGraphic.Init(transform, HpLocalPos, HpLocalScale);
    }

    private void Update()
    {
        float xScale = unitDamage.GetHp() / maxHp;
        HpBarGraphic.SetUIBar(xScale);
    }
}