using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DigitalRuby.Tween;
using UnityEngine.SceneManagement;
public class BeatLine : MonoBehaviour
{
    // Sprites
    public GameObject towerIconPrefab;
    public GameObject cursorPrefab;

    // Inputs
    public KeyCode keyCode;

    // Events
    public UnityEvent onMiss;
    public UnityEvent onHit;

    // Parameters
    public float beatRate { get; set; }
    public int maxNumBeats;
    public int cursorPos;
    public int spawnProbability;

    private bool _running;

    private LineRenderer _lineRenderer;
    private float _lineLength;
    private float _lineStart;
    private float _lineEnd;
    private float _lastUpdateTime;
    private float _elapsedTime;

    private SpriteRenderer _cursor;

    private AudioSource _audioSource;
    private DifficultyManager _difficultyManager;

    public void Run(float timeOffset, AudioSource audioSource)
    {
        _running = true;
        _elapsedTime = timeOffset;
        _audioSource = audioSource;
        _lastUpdateTime = _audioSource.time;
    }

    void Start()
    {
        _running = false;
        _lineRenderer = GetComponent<LineRenderer>();
        var positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        _lineStart = positions[0].x;
        _lineEnd = positions[positions.Length - 1].x;
        _lineLength = _lineEnd - _lineStart;

        _difficultyManager = GetComponentInParent<DifficultyManager>();

        SpawnCursor();
    }

    void Update()
    {
        if (!_running)
            return;

        var difficulty = _difficultyManager.difficulty;

        var deltaTime = _audioSource.time - _lastUpdateTime;
        if (deltaTime < 0f) {
            deltaTime = _audioSource.time +
                        _audioSource.clip.length - _lastUpdateTime;
        }

        _lastUpdateTime = _audioSource.time;

        _elapsedTime += deltaTime;
        if (_elapsedTime > (1f / beatRate) / difficulty)
        {
            _elapsedTime -= (1f / beatRate) / difficulty;
            SpawnIcon();
        }

        if (Input.GetKeyDown(keyCode))
            KeyPressed();
    }

    void KeyPressed()
    {
        var spriteLerpers = new List<SpriteLerper>();
        GetComponentsInChildren<SpriteLerper>(spriteLerpers);

        var sprites = new List<SpriteRenderer>();
        foreach (var s in spriteLerpers)
            sprites.Add(s.gameObject.GetComponent<SpriteRenderer>());

        var spritesInBounds = new List<SpriteRenderer>();

        foreach (var sprite in sprites)
        {
            if (IsWithinCursorBounds(sprite))
                spritesInBounds.Add(sprite);
        }

        if (spritesInBounds.Count == 0)
            OnMiss();
        else
            OnHit(spritesInBounds[0]);
    }

    bool IsWithinCursorBounds(SpriteRenderer sprite)
    {
        return _cursor.bounds.Intersects(sprite.bounds);
    }

    void shakeRight(Vector3 p0, int count)
    {
        if (count-- < 0)
        {
            transform.localPosition = p0;
            return;
        }

        var magnitude = 0.25f;
        var p1 = transform.localPosition;
        var p2 = p0 + new Vector3(magnitude, 0f, 0f);

        gameObject.Tween("ShakeBar", p1, p2, 0.01f, TweenScaleFunctions.CubicEaseIn, t => {
            transform.position = t.CurrentValue;
        }, t1 => shakeLeft(p0, count));
    }

    void shakeLeft(Vector3 p0, int count)
    {
        var magnitude = 0.25f;
        var p1 = transform.localPosition;
        var p2 = p0 - new Vector3(magnitude, 0f, 0f);

        gameObject.Tween("ShakeBar", p1, p2, 0.01f, TweenScaleFunctions.CubicEaseIn, t => {
            transform.position = t.CurrentValue;
        }, t1 => shakeRight(p0, count));
    }

    void OnMiss()
    {
        shakeRight(transform.localPosition, 5);

        Debug.Log("OnMiss");
        onMiss.Invoke();
    }

    void OnHit(SpriteRenderer sprite)
    {
        Debug.Log("OnHit");
        DestroyObject(sprite.gameObject);

        onHit.Invoke();
    }

    void SpawnCursor()
    {
        _cursor = Instantiate(cursorPrefab, transform).GetComponent<SpriteRenderer>();
        _cursor.transform.localPosition = new Vector3(_lineStart + cursorPos / (float) maxNumBeats * _lineLength, 0f, 0f);
    }

    void SpawnIcon()
    {
        if (Random.value >= spawnProbability / 100f)
            return;

        var newIcon = Instantiate(towerIconPrefab, transform);
        newIcon.transform.localPosition = new Vector3(_lineEnd, 0.0f, 0.0f);        
        var spriteLerper = newIcon.AddComponent<SpriteLerper>();
        spriteLerper.fromColor = new Color(0f, 1f, 1f, 1f);
        spriteLerper.toColor = new Color(1f, 0.5f, 0.2f, 1f);
        spriteLerper.startX = _lineEnd;
        spriteLerper.endX = _lineStart;
        spriteLerper.duration = maxNumBeats / beatRate / _difficultyManager.difficulty;
    }
}
