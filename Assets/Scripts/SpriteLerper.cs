using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLerper : MonoBehaviour {

    public Vector2 startPos;
    public Vector2 endPos;
    public float duration;

    private SpriteRenderer _sprite;
    private float _elapsedTime;

	// Use this for initialization
	void Start () {
        _elapsedTime = 0f;
		_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime;
        var alpha = _elapsedTime / duration;

        if (alpha > 1f) {
            DestroyObject(gameObject);
            return;
        }

        var pos = Vector2.Lerp(startPos, endPos, alpha);
		transform.localPosition = new Vector3(pos.x, pos.y, transform.position.z);
	}
}
