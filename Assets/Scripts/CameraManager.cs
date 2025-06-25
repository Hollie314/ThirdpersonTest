using System;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    InputManager inputManager;
    
    public Transform targetTransform; //The object the camera will follow 
    public Transform cameraPivot; //The object the camera uses to pivot (Look up and down)
    public Transform cameraTransform; //The transform of the actual object in the scene
    public LayerMask collisionLayers; //The layers we want our camera collide with 
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffSet = 0.2f; //How much the camera will jump off the objects its colliding with 
    public float minimumCollisionOffSet = 0.2f;
    public float cameraCollisionRadius = 2;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;
    public float lookAngle; //Camera looking up and down
    public float pivotAngle; //Camera looking left and right
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;
    public void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollision(); 
    }
    
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
            
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraFollowSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x =  pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollision()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = targetTransform.position - cameraTransform.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition),
                collisionLayers))
        {
            float distance = Vector3.Distance(cameraTransform.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffSet)
        {
            targetPosition = targetPosition - minimumCollisionOffSet;
        }
        
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
            
    }
    
}
