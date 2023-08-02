using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanetUI : MonoBehaviour
{
    public float Speed;
    float n;
    float RotateRandom;
    int RandomSeed;

    private void Start()
    {
        RotateRandom = Random.Range(1f, 3f);
        RandomSeed = Random.Range(1, 3);
    }

    void Update()
    {
        n += Time.deltaTime * Speed * RotateRandom;

        if(RandomSeed == 2)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -n);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, n);
        }
    }
}
