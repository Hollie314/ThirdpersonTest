using System;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    InputManager inputManager;
    
    public Transform targetTransform; //The object the camera will follow 
    public Transform cameraPivot; //The object the camera uses to pivot (Look up and down)
    private Vector3 cameraFollowVelocity = Vector3.zero;
    
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;
    public float lookAngle; //Camera looking up and down
    public float pivotAngle; //Camera looking left and right

    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }
    
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraFollowSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x =  pivotAngle;
        Quaternion targetRotation2 = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
    
}
