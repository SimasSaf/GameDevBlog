using UnityEngine;
using UnityEngine.InputSystem;

public class CannonMovement : MonoBehaviour
{
    public float speed = 100f; // Control the rotation speed
    public Transform earthTransform; // The Earth, or the point to rotate around
    public InputActionReference move; // Input action reference for movement

    private Vector2 moveDirection;

    private void OnEnable()
    {
        move.action.Enable(); // Ensure the action is enabled when this script is enabled
    }

    private void OnDisable()
    {
        move.action.Disable(); // Ensure the action is disabled when this script is disabled
    }

    void Update()
    {
        // Properly read the vector value from the input action
        moveDirection = move.action.ReadValue<Vector2>();

        // Calculate the rotation amount based on horizontal and vertical input
        float rotationAmount = (moveDirection.x + moveDirection.y) * speed * Time.deltaTime;

        // Log the current direction for debugging purposes
        Debug.Log("Move Direction: " + moveDirection + ", Rotating: " + rotationAmount + " degrees");

        // Rotate the cannon around the Earth based on the calculated rotation amount
        transform.RotateAround(earthTransform.position, Vector3.forward, rotationAmount);
    }
}
