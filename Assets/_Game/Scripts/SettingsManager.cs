using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TextMeshProUGUI BloomText;

    private void Start()
    {
        BloomText.text = "Bloom: " + DontDestroy.Instance.BloomActive;
    }
    public void ToggleBloom()
    {
        DontDestroy.Instance.BloomActive = !DontDestroy.Instance.BloomActive;
        BloomText.text = "Bloom: " + DontDestroy.Instance.BloomActive;
        DontDestroy.Instance.ChangeSetting();
    }

    public void changePlanet(int amount)
    {
        int newNumber = DontDestroy.Instance.planetNumber += amount;
        newNumber = Mathf.Abs(newNumber % DontDestroy.Instance.planetSprites.Length);
        DontDestroy.Instance.planetNumber = newNumber;
        DontDestroy.Instance.ChangeSetting();
    }
}
