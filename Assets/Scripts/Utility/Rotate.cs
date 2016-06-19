using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField] private float RotationSpeed;

	void Update () 
    {
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
	}
}
