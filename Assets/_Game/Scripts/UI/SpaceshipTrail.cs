using UnityEngine;

public class SpaceshipTrail : MonoBehaviour
{
    public int trailLength = 20; // Adjust this to set the length of the trail

    private LineRenderer lineRenderer;
    private Vector3[] trailPositions;
    private float originalStartWidth;
    private float originalEndWidth;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        originalStartWidth = lineRenderer.startWidth;
        originalEndWidth = lineRenderer.endWidth;

        trailPositions = new Vector3[trailLength];
        for (int i = 0; i < trailLength; i++)
        {
            trailPositions[i] = transform.position;
        }
    }

    void Update()
    {
        // Update the trail positions
        for (int i = trailPositions.Length - 1; i > 0; i--)
        {
            trailPositions[i] = trailPositions[i - 1];
        }
        trailPositions[0] = transform.position;

        // Update the line renderer with the trail positions
        lineRenderer.positionCount = trailLength;
        lineRenderer.SetPositions(trailPositions);

        // Restore the original width each frame to avoid overwriting
        lineRenderer.startWidth = originalStartWidth;
        lineRenderer.endWidth = originalEndWidth;
    }
}
