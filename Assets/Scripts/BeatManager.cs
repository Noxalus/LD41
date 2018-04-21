using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var beat = GetComponent<Beat>();
        var bpm = beat.bpm;

        var beatLines = new List<BeatLine>();
		GetComponentsInChildren<BeatLine>(beatLines);

        foreach (var beatLine in beatLines) {
            beatLine.beatRate = bpm / 60f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
