using UnityEngine;

public class MouseLerpMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust this to set the movement speed of the object
    public float turningSpeed = 10f; // Adjust this to set the turning speed of the object
    public float smoothingFactor = 0.1f; // Adjust this to set the smoothness of the interpolation

    private Vector3 targetPosition;
    private float targetRotation;

    void Start()
    {
        Cursor.visible = false;
        targetPosition = transform.position;
        targetRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the target position based on the mouse cursor position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        // Calculate the target rotation based on the direction towards the target position
        Vector3 direction = targetPosition - transform.position;
        targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Update the object's position and rotation smoothly using Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
        float newRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime * turningSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }
}