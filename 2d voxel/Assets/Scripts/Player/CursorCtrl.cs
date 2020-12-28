using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCtrl : MonoBehaviour
{
    private Vector3 mouseInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseInput *= 0.1f;

        transform.position = mouseInput + transform.position;
        GameRef.cursorPos = transform.position;
    }
}
