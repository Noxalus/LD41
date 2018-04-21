using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
  public GameObject tower1Prefab;
  private Camera _camera;
  private float _towerRadius;
  private int _layerMask;
  private int _maxRayDistance;
  private List<GameObject> towers1;
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
        TowerUtility.SortFromCenter(ray.origin, hits);
        if (hits[0].collider.gameObject.tag == "Ground" && hits.Length == 1)
        {
          towers1.Add(Instantiate(tower1Prefab, ray.GetPoint(9), Quaternion.identity));
        }
      }
    }
  }

  public void HandleRythmHit()
  {
    towers1.ForEach(delegate (GameObject tower)
    {
      tower.GetComponent<TowerController>().Shoot();
    });
  }
}
