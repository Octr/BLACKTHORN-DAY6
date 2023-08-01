using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DontDestroy : Singleton<DontDestroy>
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

    protected override void Awake()
    {
        base.Awake();
        images = FindObjectsOfType<Image>(includeInactive: true);
        sprites = FindObjectsOfType<SpriteRenderer>(includeInactive: true);
        ChangeSetting();
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
        currLevel++;
    }

    public void SetLevel(int lvl)
    {
        currLevel = lvl;
    }

    public void SetStars(int currStars)
    {
        stars[currLevel] = Mathf.Max(currStars, stars[currLevel]);

        starCount = 0;
        for (int I = 0; I < levels; I++)
        {
            starCount += stars[I];
        }
    }
}
