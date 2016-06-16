using UnityEngine;

public interface Movable
{
    void Position(Vector3 pos, Vector3 scale, bool snap = false);
    bool IsDestroyed();
}

public interface Card : Movable
{
    int ManaCost {get;}
    string ObjectSpawnCodeCall {get;}

    void Present();
    void Enable();
    bool IsEnabled();
    void Deploy(GridCell deplyCell);
    void Discard();
}