using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour
{
    public static ObjectFactory Instance;

    [SerializeField] private PoolType[] Pools;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject CreateObjectCode(string PoolCode, bool activate = true)
    {
        for (int pool = 0; pool < Pools.Length; pool++)
        {
            if (PoolCode.Equals(Pools[pool].CallCode))
            {
                return Pools[pool].Pool.GetObjectInPool(activate);
            }
        }

        return null;
    }
}

[System.Serializable]
public struct PoolType
{
    public string CallCode;
    public ObjectPool Pool;
}
