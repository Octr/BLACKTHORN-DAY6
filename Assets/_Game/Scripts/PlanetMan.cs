using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : Singleton<PlanetManager>
{
    [SerializeField] public GameObject[] Planets;

    public void Start()
    {
        GameLogic.Instance.isTutorial = true;   
    }

    public void EnablePlanets()
    {
        foreach (GameObject planet in Planets)
        {
            planet.SetActive(true);
            GameLogic.Instance.isTutorial = false;
        }
    }
}
