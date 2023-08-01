using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 1.0f;
    public float minZoomSize = 2.0f;
    public float maxZoomSize = 10.0f;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Adjust the camera's orthographic size based on the scroll input
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scrollInput * zoomSpeed, minZoomSize, maxZoomSize);
        }
    }
}