using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public float sensitivity = 10f;
    private float titleAngle;

    void Update()
    {
        var mouseY = Input.GetAxis("Mouse Y");
        titleAngle -= mouseY * sensitivity;
        titleAngle = Mathf.Clamp(titleAngle, -30f, 90f);

        transform.localRotation = Quaternion.Euler(titleAngle, 0, 0);
    }
}
