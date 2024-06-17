using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private InputManager inputManager;
    private Transform cameraTransform;
    [SerializeField] private bool canDoubleJump;

    //private float groundDrag;
    [SerializeField] private bool groundedPlayer;
    public LayerMask ground;
    [SerializeField] public float playerHight;
    private Vector3 lookDirection;

    private BloodScreenEffectUI be;
    [SerializeField] private GameObject Laser;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        playerHight = controller.height * 0.5f + 0.4f;
    }

    void Update()
    {
        groundedPlayer =  Physics.Raycast(transform.position,Vector3.down,playerHight, ground);
        //lookDirection = cameraTransform.forward;
        //transform.LookAt(transform.position + lookDirection, Vector3.up);
        if (groundedPlayer)
        {
            playerVelocity.y = 0f;
            canDoubleJump = false;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new(movement.x,0f,movement.y);//kierunek chocdzenia
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        Jump();
        Sprint();
        UseLaser();
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        ChcangeVipon();
        if (healthSystem.currentStamina < healthSystem.maxStamina && !inputManager.PlayerSprint() && groundedPlayer)
        {
            healthSystem.RegenStamina(0.1f);
        }
    }

    public void ChcangeVipon()
    {
        if (inputManager.PlayerChangeWeapon())
        {
            if (inputManager.changeWeapon == true)
            {
                inputManager.changeWeapon = false;
            }
            else
            {
                inputManager.changeWeapon = true;
            }
        }
    }

    public void Jump()
    {
        if (inputManager.PlayerJumpedThisFrame() && (groundedPlayer || canDoubleJump) && healthSystem.currentStamina >= 10f)
        {
            healthSystem.TakeStamina(10f);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            canDoubleJump = true;
            if (!groundedPlayer)
            {
                canDoubleJump = false;
            }

        }
    }

    public void Sprint()
    {
        if (inputManager.PlayerSprint() && healthSystem.currentStamina > 0 && groundedPlayer)
        {
            healthSystem.TakeStamina(0.2f);
            playerSpeed = 10f;
        }
        else
        {
            playerSpeed = 2f;
        }
    }

    public void GetDamage(float damage)
    {
        healthSystem.currentHealth -= damage;
        be = BloodScreenEffectUI.Instance;
        be.TriggerBloodEffect();
    }

    public void UseLaser()
    {
        if (inputManager.PlayerReload())
        {
            Laser.SetActive(true);
        }
        else
        {
            Laser.SetActive(false);
        }
    }
}
