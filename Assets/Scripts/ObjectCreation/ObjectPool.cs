using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToPool;
    [SerializeField] private int PoolSize;
    [SerializeField] private string ParentName;

    private GameObject[] Pool;
    private GameObject Parent;

    private void Awake()
    {
        if (Parent == null && !string.IsNullOrEmpty(ParentName))
            Parent = new GameObject(ParentName);

        CreatePool();

    }

    private void CreatePool()
    {
        Pool = new GameObject[PoolSize];
        for (int index = 0; index < PoolSize; index++)
        {
            GameObject objectInstance = Instantiate(ObjectToPool) as GameObject;
            objectInstance.transform.localPosition = Vector3.zero;
            objectInstance.SetActive(false);
            Pool[index] = objectInstance;

            if (Parent != null)
                objectInstance.transform.SetParent(Parent.transform);
        }
    }

    public GameObject GetObjectInPool(bool activate = true)
    {
        for (int index = 0; index < PoolSize; index++)
        {
            if (!Pool[index].activeSelf)
            {
                Pool[index].SetActive(activate);
                return Pool[index];
            }
        }

        return null;
    }
}
