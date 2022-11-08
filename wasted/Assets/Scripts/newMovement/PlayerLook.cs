using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 Input)
    {
        float mouseX = Input.x;
        float mouseY = Input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        yRotation += (mouseX * Time.deltaTime) * xSensitivity;
       // yRotation = Mathf.Clamp(yRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

// transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
