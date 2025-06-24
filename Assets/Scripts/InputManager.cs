using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire pour utiliser le nouveau système d'input

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Lire la valeur du stick ou des touches WASD
            playerControls.PlayerMovement.Movement.performed += i =>
                movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.Movement.canceled += i =>
                movementInput = Vector2.zero;
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        
        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

}