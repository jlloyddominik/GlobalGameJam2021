using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public InputAction LookInput;
    public bool mouseXInvert = false;
    public float mouseX_speed = 100f, mouseY_speed = 100f;
    private float xRotation = 0f;

    public Transform Player;

    public void Awake()
    {
        LookInput.Enable();
    }

    void Update()
    {
        Vector2 input = LookInput.ReadValue<Vector2>();
        Vector2 _mouseInput = new Vector3(input.x* mouseX_speed, input.y* mouseY_speed);
        if (mouseXInvert == false)
        {
            xRotation -= _mouseInput.y;
        }
        else
        {
            xRotation += _mouseInput.y;
        }

        //Bandaid fix.
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        Player.Rotate(Vector3.up * _mouseInput.x);
    }
}