using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : Singleton<PlanetManager>
{
    [SerializeField] public GameObject[] Planets;
    public bool isWeaponSequence;
    public bool isTutorialSequence;

    public void Start()
    {
        BulletShooter.Instance.isUnlocked = true;

        if (isTutorialSequence)
        {
            BulletShooter.Instance.isUnlocked = false;
        }

        GameLogic.Instance.isTutorial = true;
    }

    public void EnablePlanets()
    {
        foreach (GameObject planet in Planets)
        {
            planet.SetActive(true);
            GameLogic.Instance.isTutorial = false;
        }

        if (isWeaponSequence)
        {
            BulletShooter.Instance.isUnlocked = true;
        }


    }
}
