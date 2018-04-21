using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        SpawnCursor();
    }

    void Update()
    {
        if (!_running)
            return;

        var deltaTime = _audioSource.time - _lastUpdateTime;
        _lastUpdateTime = _audioSource.time;

        _elapsedTime += deltaTime;
        if (_elapsedTime > (1.0f / beatRate))
        {
            _elapsedTime -= (1f / beatRate);
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

    void OnMiss()
    {
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
        spriteLerper.startX = _lineEnd;
        spriteLerper.endX = _lineStart;
        spriteLerper.duration = maxNumBeats / beatRate;
    }
}
