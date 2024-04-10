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
    [SerializeField] private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    [SerializeField] private bool canDoubleJump;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    void Update()
    {
        Vector3 lookDirection = cameraTransform.forward;
        //transform.LookAt(transform.position + lookDirection, Vector3.up);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            canDoubleJump = false;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x,0f,movement.y);//kierunek chocdzenia
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


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
        if (inputManager.PlayerChangeWeapon())
        {
            if (inputManager.changeWeapon == true)
            {
                inputManager.changeWeapon = false;
            }else
            {
                inputManager.changeWeapon = false;
            }
            
        }
        if (inputManager.PlayerSprint() && healthSystem.currentStamina>0)//groundedPlayer
        {
            healthSystem.TakeStamina(0.2f);
            playerSpeed = 10f;
        }
        else
        {
            playerSpeed = 2f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (healthSystem.currentStamina<healthSystem.maxStamina || (move == Vector3.zero))
        {
            healthSystem.RegenStamina(0.1f);
        }
    }
    public void GetDamage(float damage)
    {
        healthSystem.currentHealth -= damage;
    }
}
