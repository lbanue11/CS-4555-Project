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
    
    // variables to store the input values for movement
    private Vector2 movementInput;

    // variables to control the speed of movement
    public float WalkSpeed = 5;
    
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
    }
    
    // read the input values for movement and move the character controller
    private void Update()
    {
        // read the input values for movement
        movementInput = movementAction.ReadValue<Vector2>();

        // create a movement vector relative to the player's facing direction
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        
        // Actually moves the player in the direction of the movement vector, multiplied by the walk speed and delta time
        controller.Move(move * (WalkSpeed * Time.deltaTime));
    }
    
}