using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPendulum : MonoBehaviour
{

    public float SpeedRotation = 1f;
    private float rotationZ = 1f;
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f,0f,rotationZ);
        rotationZ += SpeedRotation;
    }
}
