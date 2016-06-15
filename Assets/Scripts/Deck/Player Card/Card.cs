using UnityEngine;

public interface Card
{
    int ManaCost {get;}
    string ContentCallCode {get;}

    bool IsEnabled();
    void Discard();
}

public interface Movable
{
    void Position(Vector3 pos, Vector3 scale, bool snap = false);
    bool IsDestroyed();
}