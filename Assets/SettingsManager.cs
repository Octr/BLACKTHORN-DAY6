using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TextMeshProUGUI BloomText;
    public DontDestroy dontDestroy;

    private void Start()
    {
        dontDestroy = GameObject.FindObjectOfType<DontDestroy>();
        BloomText.text = "Bloom: " + dontDestroy.BloomActive;
    }
    public void ToggleBloom()
    {
        dontDestroy.BloomActive = !dontDestroy.BloomActive;
        BloomText.text = "Bloom: " + dontDestroy.BloomActive;
        dontDestroy.changeSetting();
    }

    public void changePlanet(int amount)
    {
        int newNumber = dontDestroy.planetNumber += amount;
        newNumber = Mathf.Abs(newNumber % dontDestroy.planetSprites.Length);
        dontDestroy.planetNumber = newNumber;
        dontDestroy.changeSetting();
    }
}
