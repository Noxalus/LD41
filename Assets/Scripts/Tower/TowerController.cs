using System;
using UnityEngine;

public class TowerController : MonoBehaviour
{
  public GameObject tower1Prefab;
  private Camera _camera;
  private float _towerRadius;
  private int _layerMask;
  private int _maxRayDistance;
  // Use this for initialization
  void Start()
  {
    _camera = Camera.main;
    _layerMask = (1 << 8) | (1 << 9) | (1 << 11);
    _maxRayDistance = 11;
    _towerRadius = tower1Prefab.transform.localScale.x / 1.6f;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      RaycastHit[] hits = Physics.SphereCastAll(ray, _towerRadius, _maxRayDistance, _layerMask);
      if (hits.Length != 0)
      {
        SortFromCenter(ray, hits);
        if (hits[0].collider.gameObject.tag == "Ground" && hits.Length == 1)
        {
          Instantiate(tower1Prefab, ray.GetPoint(9), Quaternion.identity);
        }
      }
    }
  }

  private void SortFromCenter(Ray ray, RaycastHit[] hits)
  {
    Array.Sort(hits, delegate (RaycastHit hit1, RaycastHit hit2)
    {
      Vector3 relativeTarget1 = ray.origin - hit1.point;
      Vector3 relativeTarget2 = ray.origin - hit2.point;
      return Mathf.FloorToInt((relativeTarget1.x + relativeTarget1.z) / 2 - (relativeTarget2.x + relativeTarget2.z) / 2);
    });
  }
}
