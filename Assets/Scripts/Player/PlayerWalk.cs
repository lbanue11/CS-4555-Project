using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    // variable to reference the input action asset that contains the player controls
    public InputActionAsset InputActions;
    // variable to reference the character controller component on the player game object
    private CharacterController controller;
    
    // Movement variables
    private InputAction movementAction;
    private InputAction jumpAction;
    
    // variables to store the input values for movement
    private Vector2 movementInput;

    // variables to control the speed of movement
    [SerializeField] private float WalkSpeed = 5f;
    
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    
    private float verticalVelocity;
    private bool isGrounded;
    
    // enable the player input action map when the script is enabled
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    // disable the player input action map when the script is disabled
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    // initialize the character controller and input actions
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        // find the player input action map and the movement action
        var playerMap = InputActions.FindActionMap("Player");
        movementAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
    }
    
    // read the input values for movement and move the character controller
    private void Update()
    {
        
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Reset vertical velocity if grounded aka not falling or jumping
        if (isGrounded && verticalVelocity < 0)
        {            
            verticalVelocity = -2f;
        }
        
        // jump if the jump action is triggered and the player is grounded
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        verticalVelocity += gravity * Time.deltaTime;
        
        // read the input values for movement
        movementInput = movementAction.ReadValue<Vector2>();

        // create a movement vector relative to the player's facing direction
        Vector3 move = (transform.right * movementInput.x + transform.forward * movementInput.y) * WalkSpeed;
        
        // move for jumping and falling
        move.y = verticalVelocity;
        
        // Actually moves the player in the direction of the movement vector, multiplied by the walk speed and delta time
        controller.Move(move * Time.deltaTime);
    }
    
}