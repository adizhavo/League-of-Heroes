using UnityEngine;

public interface Content 
{
    bool IsObstacle();
}

public class NullContent : Content 
{
    public bool IsObstacle()
    {
        return false;
    }
}

public class SimpleObstacle : Content
{
    public bool IsObstacle()
    {
        return true;
    }
}