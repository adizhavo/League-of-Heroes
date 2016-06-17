using UnityEngine;

public interface Deployable
{
    GridCell MovingCell {get;}
    void InitialDeploy(IntVector2 initialCellId);
}