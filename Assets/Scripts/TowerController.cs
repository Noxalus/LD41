using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

  private Camera _camera;
  private int _layerMask;
  private int _maxRayDistance;
  // Use this for initialization
  void Start()
  {
    _camera = Camera.main;
    _layerMask = 1 << 8;
    _maxRayDistance = 11;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, _maxRayDistance, _layerMask))
      {
        Debug.Log("Did Hit");
      }
    }
  }
}
