using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLevelDisplayer : MonoBehaviour
{
    public Image[] Stars;
    public Sprite[] StarSprites = new Sprite[2];

    private AudioSource starSound;
    // Start is called before the first frame update
    void Start()
    {
        starSound = GameObject.Find("StarCollected").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStars(int stars) {
        starSound.Play();
        for(int i = 0; i < 3; i++) {
            Stars[i].sprite = stars > i ? StarSprites[1] : StarSprites[0];
        }
    }
}
