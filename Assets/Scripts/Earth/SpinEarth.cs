using UnityEngine;

public class SpinEarth : MonoBehaviour
{
    public float rotationSpeed = 15.0f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
