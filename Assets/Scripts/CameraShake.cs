using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CameraShakeInfo[] ShakeInfos;

    private float ShakeIntensity;    
    private float ShakeDecay;
    private float currentIntensity;
    private bool Shaking = false; 

    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    private static CameraShake instance;
    public static CameraShake Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;
    }

    private void Update () 
    {
        if (!Shaking) return;

        if(currentIntensity > 0)
        {
            transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.y + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.z + Random.Range(-currentIntensity, currentIntensity) * 0.2f,
                OriginalRot.w + Random.Range(-currentIntensity, currentIntensity) * 0.2f);

            currentIntensity -= ShakeDecay;
        }
        else
        {
            Shaking = false;  
            OriginalPos = transform.position;
            OriginalRot = transform.rotation;
        }
    } 

    public void DoShake(ShakeType type)
    {
        for (int i = 0; i < ShakeInfos.Length; i ++)
            if (ShakeInfos[i].Type.Equals(type))
            {
                ShakeIntensity = ShakeInfos[i].ShakeIntensity;
                ShakeDecay = ShakeInfos[i].ShakeDecay;
                currentIntensity = ShakeIntensity;
                Shaking = true;
                return;
            }
    }  
}

public enum ShakeType
{
    Small, 
    Medium, 
    Large
}

[System.Serializable]
public struct CameraShakeInfo
{
    public ShakeType Type;
    public float ShakeIntensity;
    public float ShakeDecay;
}