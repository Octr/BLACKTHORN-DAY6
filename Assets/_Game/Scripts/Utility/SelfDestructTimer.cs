using UnityEngine;

public class SelfDestructTimer : MonoBehaviour
{
    public float selfDestructTime = 3f; // Time in seconds before self-destruction

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private System.Collections.IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}