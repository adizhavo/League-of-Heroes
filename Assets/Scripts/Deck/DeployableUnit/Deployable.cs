using UnityEngine;

public interface Deployable
{
    GridCell MovingCell {get;}
    void Deploy(IntVector2 initialCellId);
    void Deploy(GridCell initialCell);
}