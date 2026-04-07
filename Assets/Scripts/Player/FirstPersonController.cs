using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonController : MonoBehaviour
{

    // -- Movement Settings --
    public float speed = 5f;
    public float gravity = -9.81f;
    // -- Look Settings --
    public float lookSensitivity = 2f;
    public float maxLookAngle = 90f;
    public float jumpForce = 5f;
    // -- References --
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private GlobalControls controls;
    // -- Internal State --

    private Vector2 moveInput; // Stores the current movement input
    private Vector2 lookInput; // Stores the current look input
    private float verticalVelocity; // Used for gravity
    private float cameraPitch; // Current vertical rotation of the camera

// -- Initialization --
    private void Awake() // Called when the script instance is being loaded
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component attached to the player

        if (cameraTransform == null) // Check if the playerCamera reference is assigned and log an error if it's missing, then try to find a camera in the children and assign it if found.
        {
            Debug.LogError("Player Camera reference is missing!");
            Camera childCamera = GetComponentInChildren<Camera>();
            if (childCamera != null)            {
                cameraTransform = childCamera.transform;
                Debug.Log("Player Camera reference set to child camera.");
            }
            else
            {
                Debug.LogError("No camera found in children. Please assign a camera to the Player Camera reference.");
            }
        }
    controls = new GlobalControls(); // Initialize the input controls

    controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Set up the movement input callback to update the moveInput variable when the Move action is performed
    controls.Player.Move.canceled += ctx => moveInput = Vector2.zero; // Set up the movement input callback to reset the moveInput variable to zero when the Move action is canceled
    controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Set up the look input callback to update the lookInput variable when the Look action is performed
    controls.Player.Look.canceled += ctx => lookInput = Vector2.zero; // Set up the look input callback to reset the lookInput variable to zero when the Look action is canceled
    controls.Player.Jump.performed += ctx => {
            if (controller.isGrounded)
            {
                verticalVelocity = jumpForce; // Set the vertical velocity to the jump force when the Jump action is performed and the player is grounded
            }
        };
    }

    private void OnEnable() // Called when the object becomes enabled and active
    {
        controls.Enable(); // Enable the input controls
    }

    private void OnDisable() // Called when the behaviour becomes disabled or inactive
    {
        controls.Disable(); // Disable the input controls
    }  


    private void Start() // Called before the first frame update
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }


    private void Update() // Called once per frame
    {
        HandleMovement(); // Handle player movement
        HandleLook(); // Handle player looking around
    }

   private void HandleLook()
{
    if (cameraTransform == null) return;

    float mouseX = lookInput.x * lookSensitivity;
    float mouseY = lookInput.y * lookSensitivity;

    transform.Rotate(Vector3.up * mouseX);

    cameraPitch -= mouseY;
    cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

    cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
}

    private void HandleMovement()
    {
        if(controller == null) return; // If the CharacterController reference is missing, exit the method to prevent errors

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y; // Calculate the movement direction based on input and player orientation
        controller.Move(move * speed * Time.deltaTime); // Move the player using the CharacterController
        if (controller.isGrounded) // Check if the player is grounded
        {
            verticalVelocity = -2f; // Set a small negative vertical velocity to keep the player grounded and allow for smooth movement over uneven terrain
        }
        
        
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity to vertical velocity when not grounded
            Vector3 gravityMove = Vector3.up * verticalVelocity; // Create a movement vector for gravity
            controller.Move(gravityMove * Time.deltaTime); // Move the player based on gravity
    }

    


}
