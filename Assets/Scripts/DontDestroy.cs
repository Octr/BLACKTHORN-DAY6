using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class DontDestroy : MonoBehaviour
{
    public Dictionary<int, int> stars = new Dictionary<int, int>();
    public int starCount;
    private MainMenu mainMenuManager;
    private static int currLevel = -1;

    int levels;

    public bool BloomActive = true;

    public int planetNumber;
    public Sprite[] planetSprites;
    private Image[] images;
    private SpriteRenderer[] sprites;

    void Awake()
    {
        images = FindObjectsOfType<Image>(includeInactive:true);
        sprites = FindObjectsOfType<SpriteRenderer>(includeInactive: true);
        changeSetting();
        mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenu>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gameManager");

        levels = mainMenuManager.MaxLevel;

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        for (int i = 0; i < mainMenuManager.MaxLevel; i++)
        {
            stars[i] = 0;
        }
        Debug.Log("Level: " + mainMenuManager.MaxLevel);
        Debug.Log("Stars: " + stars.Count);
    }

    private void OnLevelWasLoaded(int level)
    {
        images = FindObjectsOfType<Image>(includeInactive: true);
        sprites = FindObjectsOfType<SpriteRenderer>(includeInactive: true);
        changeSetting();
    }

    public void changeSetting()
    {
        Volume postProcessing = FindObjectOfType<Volume>();
        postProcessing.enabled = BloomActive;
        foreach (Image I in images)
        {
            print(I);
            foreach (Sprite S in planetSprites)
            {
                if (I.sprite == S)
                {
                    if (I.sprite != planetSprites[planetNumber])
                    {
                        I.sprite = planetSprites[planetNumber];
                    }
                }
            }
        }
        foreach (SpriteRenderer I in sprites)
        {
            print(I);
            foreach (Sprite S in planetSprites)
            {
                if (I.sprite == S)
                {
                    if (I.sprite != planetSprites[planetNumber])
                    {
                        I.sprite = planetSprites[planetNumber];
                    }
                }
            }
        }
    }

    public void AddLevel() {
        currLevel++;
    }

    public void SetLevel(int lvl) {
        currLevel = lvl;
        Debug.Log("Set level to " + lvl + " curr level was" + currLevel);
    }

    public void SetStars(int currStars) {
        Debug.Log("Stars Collected: " + currStars + " for level " + (currLevel) + "");
        stars[currLevel] = currStars > stars[currLevel] ? currStars : stars[currLevel];
        Debug.Log("Set stars for level " + (currLevel+1) + " to " + stars[currLevel] + " at index " + (currLevel) );

        starCount = 0;
        for (int I = 0; I < levels; I++)
        {
            starCount += stars[I];
        }
    }
}