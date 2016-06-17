using UnityEngine;
using System.Collections;

public interface Damagable
{
    bool IsOpponent();
    void Damage(float value);
}

public class LocalDamagable : Damagable
{
    private float hp = 5f;
    private Soldier soldier;

    public LocalDamagable(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void Damage(float value)
    {
        GameObject text = ObjectFactory.Instance.CreateObjectCode("SmallFloatingText");
        text.GetComponent<FloatingText>().Display(soldier.transform.position, Random.Range(-1, 2), value.ToString());

        hp -= value;
        if (hp <= 0f) soldier.Destroy();
    }

    public bool IsOpponent()
    {
        return false;
    }
}

public class SyncDamagable : Damagable
{
    private float hp = 5f;
    private Soldier soldier;

    public SyncDamagable(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void Damage(float value)
    {
        GameObject text = ObjectFactory.Instance.CreateObjectCode("SmallFloatingText");
        text.GetComponent<FloatingText>().Display(soldier.transform.position, Random.Range(-1, 2), value.ToString());

        hp -= value;
        if (hp <= 0f) soldier.Destroy();
    }

    public bool IsOpponent()
    {
        return true;
    }
}