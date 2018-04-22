using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    public float minDifficulty;
    public float maxDifficulty;
    public float timeSpan;

    public float difficulty { get {
        return Mathf.Lerp(minDifficulty, maxDifficulty, Mathf.Clamp01(_time / timeSpan));
    } }

    private static float _time;

	// Use this for initialization
	void Start () {
        _time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		_time += Time.deltaTime;
	}
}
