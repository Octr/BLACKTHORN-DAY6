using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Planet : Linkable {
    public const int LAYER = 9;

    [SerializeField] private float baseLinearDrag = 2f;
    [SerializeField] private float linkedLinearDrag = .25f;
    [SerializeField] private float savedLinearDrag = 4f;

    public AudioSource planetAudio;

    public AudioClip kissAudio;
    public AudioClip yayAudio;
    public AudioClip giggleAudio;

    private bool saved = false;

    private void Start() {
        OnActiveLinkAdded += Linkable_OnActiveLinkAdded;
        UpdateDrag();
    }

    private void Linkable_OnActiveLinkAdded(object sender, EventArgs e) {
        UpdateDrag();
    }

    public bool IsSaved() {
        return saved;
    }

    public void SetSaved(bool saved) {
        this.saved = saved;
        UpdateDrag();
    }

    private void UpdateDrag() {
        float drag = saved ? savedLinearDrag : (links.Count > 0 ? linkedLinearDrag : baseLinearDrag);
        GetComponent<Rigidbody2D>().drag = drag;
    }

    public void YayAudio()
    {
        planetAudio.clip = yayAudio;
        planetAudio.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        planetAudio.Play();
    }
}
