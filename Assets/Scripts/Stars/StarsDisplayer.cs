using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarsDisplayer : MonoBehaviour
{
    public DontDestroy gameManager;
    public TextMeshProUGUI starsText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("planetsGameManager").GetComponent<DontDestroy>();
        SetStarsText(gameManager.stars[0]);
    }

    // Update is called once per frame
    public void LevelSelected(int lvl) {
        SetStarsText(gameManager.stars[lvl]);
    }

    private void SetStarsText(int stars) {
        starsText.text = "STARS: " + stars.ToString() + "/3";
    }
}
