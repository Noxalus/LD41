using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLerper : MonoBehaviour {

    public Color fromColor;
    public Color toColor;

    public float startX;
    public float endX;
    public float duration;

    private SpriteRenderer _sprite;
    private float _elapsedTime;

    private DifficultyManager _diffcultyManager;

	// Use this for initialization
	void Start () {
        _elapsedTime = 0f;
		_sprite = GetComponent<SpriteRenderer>();
        _diffcultyManager = GetComponentInParent<DifficultyManager>();
	}
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime;
        var alpha = _elapsedTime / duration;

        if (alpha > 1f) {
            DestroyObject(gameObject);
            return;
        }

        var x = Mathf.Lerp(startX, endX, alpha);
		transform.localPosition = new Vector3(x, 0f, 0f);

        _sprite.color = Color.Lerp(
            fromColor,
            toColor,
            (_diffcultyManager.difficulty - _diffcultyManager.minDifficulty) / (_diffcultyManager.maxDifficulty - _diffcultyManager.minDifficulty)
        );
	}
}
