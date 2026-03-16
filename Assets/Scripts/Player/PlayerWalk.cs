using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    public InputActionAsset InputActions;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    private InputAction movementAction;
    private InputAction jumpAction;

    private Vector2 movementInput;

    [SerializeField] private float WalkSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private float verticalVelocity;
    private bool isGrounded;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        var playerMap = InputActions.FindActionMap("Player");
        movementAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");

        // If camera not assigned in Inspector, try Main Camera automatically
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform is not assigned on PlayerWalk.");
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        movementInput = movementAction.ReadValue<Vector2>();

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * movementInput.y + cameraRight * movementInput.x;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 move = moveDirection * WalkSpeed;
        move.y = verticalVelocity;

        float speed = movementInput.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsJumping", !isGrounded);

        controller.Move(move * Time.deltaTime);
    }
}