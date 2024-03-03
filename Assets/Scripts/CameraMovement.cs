using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust as needed for smooth movement
    public Canvas menuCanvas;

    private bool startMoving = false;

    void Update()
    {
        if (startMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -10), step);
        }
    }

    // Call this method when the Play button is clicked
    public void MoveToTarget()
    {
        startMoving = true;
        menuCanvas.enabled = false; // Hides the menu
    }
}
