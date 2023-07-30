using UnityEngine;

public class GameObjectRotation : MonoBehaviour
{
    public bool rotateOnX = true;  // Enable rotation around the X-axis
    public bool rotateOnY = true;  // Enable rotation around the Y-axis
    public bool rotateOnZ = true;  // Enable rotation around the Z-axis

    public float rotationSpeedX = 30f; // Rotation speed around the X-axis
    public float rotationSpeedY = 30f; // Rotation speed around the Y-axis
    public float rotationSpeedZ = 30f; // Rotation speed around the Z-axis

    public bool useLocalRotation = true; // Use local rotation instead of global rotation

    private Quaternion initialRotation;

    private void Start()
    {
        if (useLocalRotation)
        {
            initialRotation = transform.localRotation;
        }
        else
        {
            initialRotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (useLocalRotation)
        {
            RotateGameObjectLocally();
        }
        else
        {
            RotateGameObjectGlobally();
        }
    }

    private void RotateGameObjectLocally()
    {
        Vector3 localEulerAngles = initialRotation.eulerAngles;

        if (rotateOnX)
        {
            localEulerAngles.x += rotationSpeedX * Time.deltaTime;
        }

        if (rotateOnY)
        {
            localEulerAngles.y += rotationSpeedY * Time.deltaTime;
        }

        if (rotateOnZ)
        {
            localEulerAngles.z += rotationSpeedZ * Time.deltaTime;
        }

        transform.localRotation = Quaternion.Euler(localEulerAngles);
    }

    private void RotateGameObjectGlobally()
    {
        Vector3 eulerAngles = initialRotation.eulerAngles;

        if (rotateOnX)
        {
            eulerAngles.x += rotationSpeedX * Time.deltaTime;
        }

        if (rotateOnY)
        {
            eulerAngles.y += rotationSpeedY * Time.deltaTime;
        }

        if (rotateOnZ)
        {
            eulerAngles.z += rotationSpeedZ * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
