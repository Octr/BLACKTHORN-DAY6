using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLevelDisplayer : MonoBehaviour
{
    public Image[] Stars;
    public Sprite[] StarSprites = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStars(int stars) {
        AudioManager.Instance.Play2DSoundEffect(SoundEffect.Collect, 0.8f, 1.2f);
        for(int i = 0; i < 3; i++) {
            Stars[i].sprite = stars > i ? StarSprites[1] : StarSprites[0];
        }
    }
}
