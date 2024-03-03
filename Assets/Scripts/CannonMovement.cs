using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public float speed = 100f; // Speed of rotation around the planet
    public Transform earthTransform; // The Earth's transform

    void Update()
    {
        // Get horizontal input from the keyboard
        float input = Input.GetAxis("Horizontal"); // Use only keyboard input

        // Rotate the cannon around the planet based on keyboard input
        transform.RotateAround(earthTransform.position, Vector3.forward, -input * speed * Time.deltaTime);
    }
}
