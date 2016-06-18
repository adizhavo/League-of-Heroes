using UnityEngine;
using System.Collections;

public interface Damagable
{
    void Damage(float value);
    void FixDamages();
    float GetHp();
    bool IsOpponent(); 
}

public class LocalDamagable : Damagable
{
    private SoldierHPBar soldierHp;

    private float frameHp;

    private Deployable deployable;

    public LocalDamagable(Deployable soldier, SoldierHPBar soldierHp)
    {
        this.deployable = soldier;
        this.soldierHp = soldierHp;
        this.soldierHp.Init(this);

        frameHp = soldierHp.MaxHp;
    } 

    public float GetHp()
    {
        return frameHp;
    }

    public void Damage(float value)
    {
        GameObject text = ObjectFactory.Instance.CreateObjectCode("SmallFloatingText");
        text.GetComponent<FloatingText>().Display(deployable.GetPosition(), Random.Range(-1, 2), value.ToString());

        frameHp -= value;
        if (frameHp <= 0f) deployable.Destroy();
    }

    public void FixDamages()
    {
        frameHp = soldierHp.MaxHp;
    }

    public virtual bool IsOpponent()
    {
        return false;
    }
}

public class SyncDamagable : LocalDamagable
{
    public SyncDamagable(Deployable soldier, SoldierHPBar soldierHp) : base(soldier, soldierHp)
    {
    }

    public override bool IsOpponent()
    {
        return true;
    }
}