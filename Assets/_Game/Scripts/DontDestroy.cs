using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;

public class DontDestroy : Singleton<DontDestroy>
{
    public Dictionary<int, int> stars = new Dictionary<int, int>();
    public bool[] levelsUnlocked;
    public int starCount;
    private MainMenu mainMenuManager;
    private static int currLevel = -1;

    int levels;

    public bool BloomActive = true;

    public int planetNumber;
    public Sprite[] planetSprites;
    private Image[] images;
    private SpriteRenderer[] sprites;

    protected override void Awake()
    {
        base.Awake();
        images = FindObjectsOfType<Image>(includeInactive: true);
        sprites = FindObjectsOfType<SpriteRenderer>(includeInactive: true);
        ChangeSetting();
        mainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenu>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gameManager");

        levels = mainMenuManager.MaxLevel;
        levelsUnlocked = new bool[levels];

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
 
        DontDestroyOnLoad(this.gameObject);

        //Check for stars saved in player prefs
        if (PlayerPrefs.HasKey("Stars")) {
            stars = Serializer.DeserializeDictionary(PlayerPrefs.GetString("Stars"));
        } else {
            for (int i = 0; i < mainMenuManager.MaxLevel; i++)
            {
                stars[i] = 0;
            }
        }

        //check for levels unlocked in player prefs
        if (PlayerPrefs.HasKey("LevelsUnlocked")) {
            levelsUnlocked = Serializer.DeserializeBoolArray(PlayerPrefs.GetString("LevelsUnlocked"));
        } else {
            levelsUnlocked[0] = true;
            for (int i = 1; i < mainMenuManager.MaxLevel; i++)
            {
                levelsUnlocked[i] = false;
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        images = FindObjectsOfType<Image>(includeInactive: true);
        sprites = FindObjectsOfType<SpriteRenderer>(includeInactive: true);
        ChangeSetting();
    }

    public void ChangeSetting()
    {
        Volume postProcessing = FindObjectOfType<Volume>();
        postProcessing.enabled = BloomActive;

        foreach (Image I in images)
        {
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

    public void AddLevel()
    {
        if( (currLevel+1) <= levelsUnlocked.Length && !levelsUnlocked[currLevel + 1]) {
            levelsUnlocked[currLevel + 1] = true;
            PlayerPrefs.SetString("LevelsUnlocked", Serializer.SerializeBoolArray(levelsUnlocked));
            PlayerPrefs.Save();
        }
        currLevel++;
    }

    public void SetLevel(int lvl)
    {
        currLevel = lvl;
    }

    public void SetStars(int currStars)
    {
        stars[currLevel] = Mathf.Max(currStars, stars[currLevel]);
        PlayerPrefs.SetString("Stars", Serializer.SerializeDictionary(stars));

        starCount = 0;
        for (int I = 0; I < levels; I++)
        {
            starCount += stars[I];
        }
        PlayerPrefs.SetInt("StarCount", starCount);
        PlayerPrefs.Save();
    }
}
