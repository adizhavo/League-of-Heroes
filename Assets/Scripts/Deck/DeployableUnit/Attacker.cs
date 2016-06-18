using UnityEngine;
using System.Collections.Generic;

public class Attacker : MonoBehaviour{

    [SerializeField] private float Damage;
    [SerializeField] private float AttackRate;
    [SerializeField] private AreaDamage AttackArea;

    private float timeCounter = 0f;

    public bool CanAttack(GridCell currentCell, bool colorCells = true)
    { 
        FrameCheck();
        AttackArea.ReadArea(currentCell, colorCells);
        return AttackArea.HasDamagable();
    }

    public void Attack()
    {
        if (timeCounter >= AttackRate - Mathf.Epsilon)
        {
            timeCounter = 0f;
            List<Damagable> targets = AttackArea.GetDamagables();

            for (int i = 0; i < targets.Count; i++)
                targets[i].Damage(Damage);
        }
    }

    private void FrameCheck()
    {
        timeCounter += Time.deltaTime;
        timeCounter = Mathf.Clamp(timeCounter, 0f, AttackRate + Mathf.Epsilon);
    }
}