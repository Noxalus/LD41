using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLine : MonoBehaviour
{
    // Tower icon
    public GameObject towerSprite;
    public GameObject cursorSprite;

    // Parameters
    public float beatRate;
    public int maxNumBeats;
    public int cursorPos; 

    private LineRenderer _lineRenderer;
    private float _lineLength;
    private float _lineStart;
    private float _lineEnd;
    private float _elapsedTime;

    void Start()
    {
        _elapsedTime = 0f;

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
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > (1.0f / beatRate))
        {
            _elapsedTime = 0f;
            SpawnIcon();
        }
    }

    void SpawnCursor()
    {
        var cursor = Instantiate(cursorSprite, transform);
        cursor.transform.position += new Vector3(_lineStart + cursorPos / (float) maxNumBeats * _lineLength, 0f, 0f);
    }

    void SpawnIcon()
    {
        var newIcon = Instantiate(towerSprite, transform);
        newIcon.transform.localPosition = new Vector3(_lineEnd, 0.0f, 0.0f);        
        var spriteLerper = newIcon.AddComponent<SpriteLerper>();
        spriteLerper.startPos = new Vector2(_lineEnd, 0.0f);
        spriteLerper.endPos = new Vector2(_lineStart, 0.0f);
        // fixme compute duration given beatRate, maxNumBeats and lineLength
        spriteLerper.duration = 5f;
    }
}
