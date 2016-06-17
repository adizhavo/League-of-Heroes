using UnityEngine;
using System.Collections.Generic;

public class Attacker : MonoBehaviour {

    [SerializeField] private float Damage;
    [SerializeField] private float AttackRate;

    private float timeCounter = 0f;

    public bool CanAttack()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > AttackRate)
        {
            timeCounter = 0f;
            return true;
        }
        return false;
    }

    public void Attack(List<Damagable> targets)
    {
        for (int i = 0; i < targets.Count; i ++)
            targets[i].Damage(Damage);
    }
}
