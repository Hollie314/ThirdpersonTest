using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float movementSpeed = 7f;
    public float rotationSpeed = 15f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllInput()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= movementSpeed;

        // Ne pas écraser la vélocité verticale (gravité, saut, etc.)
        playerRigidbody.linearVelocity = new Vector3(moveDirection.x, playerRigidbody.linearVelocity.y, moveDirection.z);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput +
                                  cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }
}