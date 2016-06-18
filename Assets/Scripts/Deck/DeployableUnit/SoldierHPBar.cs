using UnityEngine;
using System.Collections;

public class SoldierHPBar : MonoBehaviour 
{
    [SerializeField] private int maxHp;
    public int MaxHp { get { return maxHp; } }

    public void Init(Damagable unitDamage)
    {
        // UI Health bar
    }
}
