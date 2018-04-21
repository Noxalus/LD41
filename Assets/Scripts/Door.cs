using UnityEngine;

public class Door : MonoBehaviour {

    public float closeInterval;
    public float timeOffset;
    public float openYPosition = -1;
    public float closeYPosition = 1;

    private float _closeCounter;
    private bool _closed;

	void Start ()
    {
        _closeCounter = closeInterval + timeOffset;
        UpdateYPosition(openYPosition);
        _closed = false;
    }

    void Update ()
    {
        _closeCounter -= Time.deltaTime;

        if (_closeCounter < 0)
        {
            _closeCounter = closeInterval;
            SwitchDoor();
        }
	}

    private void SwitchDoor()
    {
        if (_closed)
            UpdateYPosition(openYPosition);
        else
            UpdateYPosition(closeYPosition);

        _closed = !_closed;
    }

    private void UpdateYPosition(float position)
    {
        var newPosition = transform.localPosition;
        newPosition.y = position;
        transform.localPosition = newPosition;
    }
}
