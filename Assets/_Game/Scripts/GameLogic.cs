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

    public class WinTimerProgressEventArgs : EventArgs {
        public float progressNormalized;
    }

    [SerializeField] private float winTimerMax = 3f;

    private float winTimer = 0f;
    private int levelId;

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
            DontDestroy.Instance.SetStars(currStars);
            DontDestroy.Instance.AddLevel();
            SceneManager.LoadScene(name);
        } else {
            DontDestroy.Instance.SetStars(currStars);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void RetryLevel() {
        StartCoroutine(retryLevel());
    }

    public void GoHome()
    {
        StartCoroutine(goHome());
    }

    IEnumerator retryLevel() {
        AudioSource retryAudio = AudioManager.Instance.Play2DSoundEffect(SoundEffect.Replay, 1f, 0.8f, 1.2f);
        yield return new WaitWhile (()=> retryAudio.isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator goHome() {
        AudioSource homeAudio = AudioManager.Instance.Play2DSoundEffect(SoundEffect.Home, 1f, 0.8f, 1.2f);
        yield return new WaitWhile (()=> homeAudio.isPlaying);
        SceneManager.LoadScene("MainMenu");
    }
}
