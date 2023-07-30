using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;


public class GameLogic : Singleton<GameLogic>
{
    public event EventHandler<WinTimerProgressEventArgs> OnWinTimerProgress;
    public bool isTutorial;

    public int currStars;

    public DontDestroy gameManager;
    public class WinTimerProgressEventArgs : EventArgs {
        public float progressNormalized;
    }

    [SerializeField] private float winTimerMax = 3f;

    private float winTimer = 0f;
    private int levelId;

    public AudioSource retrySound;
    public AudioSource homeSound;

    protected override void Awake() {
        base.Awake();
        Scene currentScene = SceneManager.GetActiveScene();
        try {
            levelId = Int32.Parse(Regex.Replace(currentScene.name, "[^0-9]", ""));
        } catch {
            Debug.LogError("Could not parse level id from scene name: " + currentScene.name);
        }
        currStars = 0;
    }

    void Start() {
        gameManager = GameObject.Find("planetsGameManager").GetComponent<DontDestroy>();
        retrySound = GameObject.Find("RetrySound").GetComponent<AudioSource>();
        homeSound = GameObject.Find("HomeSound").GetComponent<AudioSource>();
    }

    void Update() {
        bool allSaved = true;

        if (isTutorial)
        {
            return;
        }

        foreach (Planet planet in FindObjectsOfType<Planet>()) {
            if(!planet.IsSaved()) {
                allSaved = false;
                break;
            }
        }
            if(allSaved) {
            SetWinTimer(winTimer + Time.deltaTime);
            if(winTimer >= winTimerMax) {
                LoadNextLevel();
            }
        } else {
            SetWinTimer(0f);
        }
    }

    private void SetWinTimer(float value) {
        winTimer = value;
        OnWinTimerProgress?.Invoke(this, new WinTimerProgressEventArgs { progressNormalized = value / winTimerMax });
    }

    private void LoadNextLevel() {
        string name = "Level " + (levelId + 1);
        bool exists = false;
        for (int i = 0; true; i++) {
            var s = SceneUtility.GetScenePathByBuildIndex(i);
            if (s.Length <= 0) {
                break;
            } else {
                if (s.Contains(name)) {
                    exists = true;
                    break;
                }
            }
        }
        if(exists) {
            gameManager.SetStars(currStars);
            gameManager.AddLevel();
            SceneManager.LoadScene(name);
        } else {
            gameManager.SetStars(currStars);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void RetryLevel() {
        StartCoroutine(retryLevel(retrySound));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        StartCoroutine(goHome(homeSound));
        //SceneManager.LoadScene("MainMenu");
    }

    IEnumerator retryLevel(AudioSource sound) {
        sound.Play();
        yield return new WaitWhile (()=> sound.isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator goHome(AudioSource sound) {
        sound.Play();
        yield return new WaitWhile (()=> sound.isPlaying);
        SceneManager.LoadScene("MainMenu");
    }
}
