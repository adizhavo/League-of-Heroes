using UnityEngine;

public interface Card
{
    int ManaCost {get;}
    string ObjectSpawnCodeCall {get;}

    void Deploy(GridCell deplyCell);
    void Discard();
    bool IsEnabled();
}

public interface Movable
{
    void Position(Vector3 pos, Vector3 scale, bool snap = false);
    bool IsDestroyed();
}