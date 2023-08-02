using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Credits;
    public GameObject LevelSel;
    public TextMeshProUGUI LevelIndicator;
    public ButtonMenuSelect LevelPlayButton;

    public DontDestroy gameManager;

    public int MaxLevel;
    int m=1;
    public StarsDisplayer starsDisplayer;
    public TextMeshProUGUI starCountText;

    private void Awake() {
        
        if (PlayerPrefs.HasKey("StarCount")) {
            gameManager.starCount = PlayerPrefs.GetInt("StarCount");
        }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<DontDestroy>();        
        starCountText.text = gameManager.starCount.ToString() + "/" + MaxLevel*3;
    }

    public void ToggleLevelSelector()
    {        
        Credits[1].SetActive(false);
        LevelSel.SetActive(!LevelSel.activeSelf);
    }

    public void ToggleCredits()
    {
        LevelSel.SetActive(false);
        //Credits[0].SetActive(!Credits[0].activeSelf);
        Credits[1].SetActive(!Credits[1].activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        LevelIndicator.text = m.ToString();
    }
    public void Up()
    {
        m++;
        if (m > MaxLevel)
        {
            m = 1;
        }        
        if(gameManager.levelsUnlocked[m-1]) {
            LevelPlayButton.String_ = "PLAY";
        } else {
            LevelPlayButton.String_ = "LOCKED";
        }
        starsDisplayer.LevelSelected(m-1);
    }
    public void Down()
    {
        m--;
        if (m<1)
        {
            m = MaxLevel;
        }        
        if(gameManager.levelsUnlocked[m-1]) {
            LevelPlayButton.String_ = "PLAY";
        } else {
            LevelPlayButton.String_ = "LOCKED";
        }
        starsDisplayer.LevelSelected(m-1);
    }

    public void PlayButtonSound() {
        AudioManager.Instance.Play2DSoundEffect(SoundEffect.blipSelect);
    }

    public void StartGame()
    {
        gameManager.SetLevel(0);
        SceneManager.LoadScene("Level 1");
    }

    public void PlayLevel()
    {
        if(DontDestroy.Instance.levelsUnlocked[m-1]) {
            gameManager.SetLevel(m-1);
            SceneManager.LoadScene("Level " + m);

            if(m >= 6)
            {
                BulletShooter.Instance.isUnlocked = true;
            }            
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
