using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsDisplayer : MonoBehaviour
{
    public TextMeshProUGUI starsText;
    // Start is called before the first frame update
    void Start()
    {
        SetStarsText(DontDestroy.Instance.stars[0]);
    }

    // Update is called once per frame
    public void LevelSelected(int lvl) {
        SetStarsText(DontDestroy.Instance.stars[lvl]);
    }

    private void SetStarsText(int stars) {
        starsText.text = "STARS: " + stars.ToString() + "/3";
    }
}
