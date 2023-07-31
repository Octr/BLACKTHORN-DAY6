using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
    }
}
