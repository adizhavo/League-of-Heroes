using UnityEngine;

public interface Deployable
{
    GridCell CurrentCell {get;}
    void InitialDeploy(IntVector2 initialCellId);
    void Destroy();
    Vector3 GetPosition();
}