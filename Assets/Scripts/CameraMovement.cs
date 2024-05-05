using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject earth;

    private bool startMoving = false;
    private Vector3 targetPosition;
    private Vector3 startPosition = new Vector3(0, 2, -5);

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (startMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }

    public void MoveToEarth()
    {
        if (earth != null)
        {
            targetPosition = new Vector3(earth.transform.position.x, earth.transform.position.y, -5);
            startMoving = true;
        }
        else
        {
            Debug.LogError("Earth object not assigned!");
        }
    }

    public void MoveToStart()
    {
        targetPosition = startPosition;
        startMoving = true;
    }
}
