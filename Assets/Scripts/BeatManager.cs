using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    private float _basePitch;

	// Use this for initialization
	void Start () {
        var audioSource = GetComponent<AudioSource>();
        _basePitch = audioSource.pitch;
        
        var beat = GetComponent<Beat>();
        var bpm = beat.bpm;
        var offset = beat.offset;

        var beatLines = new List<BeatLine>();
		GetComponentsInChildren<BeatLine>(beatLines);

        foreach (var beatLine in beatLines) {
            beatLine.beatRate = bpm / 60f;
            beatLine.Run(offset / (float) bpm / beatLine.beatRate, audioSource);
        }

        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        var audioSource = GetComponent<AudioSource>();
        var difficulty = GetComponentInParent<DifficultyManager>().difficulty;
        audioSource.pitch = _basePitch * difficulty;
	}
}
