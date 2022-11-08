using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
    
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    private GunScript gunScript;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        gunScript = GetComponentInChildren<GunScript>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Reload.performed += ctx => gunScript.ManualReload();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        motor.Jetpack(onFoot.Fly.ReadValue<float>());
        gunScript.MyInput(onFoot.Shoot.ReadValue<float>());
    }

     void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();

    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
