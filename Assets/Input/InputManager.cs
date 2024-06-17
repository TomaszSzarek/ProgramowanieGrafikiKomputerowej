using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public bool changeWeapon = true;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private CustomInput customInput;


    private void Awake()
    {
        if(_instance != null && _instance!= this) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        customInput = new CustomInput();
    }

    private void OnEnable()
    {
        customInput.Enable();
    }

    private void OnDisable()
    {
        customInput.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return customInput.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return customInput.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return customInput.Player.Jump.triggered;
    }

    public bool PlayerShot()
    {
        if (changeWeapon)
        {
            return customInput.Player.Shot.IsPressed();
        }
        else
        {
            return customInput.Player.Shot.triggered;
        }
    }
    public bool PlayerSprint()
    {
        return customInput.Player.Sprint.IsPressed();
    }
    public bool PlayerChangeWeapon()
    {
        return customInput.Player.ChangeWeapon.triggered;
    }
    //laser
    public bool PlayerReload()
    {
        return customInput.Player.Reload.IsPressed();
    }
}
