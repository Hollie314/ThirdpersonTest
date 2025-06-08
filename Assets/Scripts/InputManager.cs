using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire pour utiliser le nouveau système d'input

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // Lire la valeur du stick ou des touches WASD
            playerControls.PlayerMovement.Movement.performed += context =>
                movementInput = context.ReadValue<Vector2>();

            playerControls.PlayerMovement.Movement.canceled += context =>
                movementInput = Vector2.zero;
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
    }
}