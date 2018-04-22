using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerManager : MonoBehaviour
{
  public GameObject tower1Prefab;
  public UnityEvent onTowerAdded;

  private Camera _camera;
  private float _towerRadius;
  private int _layerMask;
  private int _maxRayDistance = 11;
  private List<GameObject> _towers1 = new List<GameObject>();
  private GameManager _gameManager;

  void Start()
  {
    _camera = Camera.main;
    _towerRadius = tower1Prefab.transform.localScale.x / 1.6f;
    _gameManager = GetComponentInParent<GameManager>();
    _layerMask = (1 << LayerMask.NameToLayer("Ground")) |
                 (1 << LayerMask.NameToLayer("Path")) |
                 (1 << LayerMask.NameToLayer("Tower")) |
                 (1 << LayerMask.NameToLayer("NoTowerArea"));
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      RaycastHit[] hits = Physics.SphereCastAll(ray, _towerRadius, _maxRayDistance, _layerMask);
      if (hits.Length != 0)
      {
        TowerUtility.SortFromCenter(ray.origin, hits);
        if (hits[0].collider.gameObject.tag == "Ground" && hits.Length == 1 && _gameManager.CanPlaceTower())
        {
          Vector3 pos = ray.GetPoint(0);
          pos.y = 0.6f;
          _towers1.Add(Instantiate(tower1Prefab, pos, Quaternion.identity));
          onTowerAdded.Invoke();
        }
      }
    }
  }

  public void HandleRythmHit()
  {
    _towers1.ForEach(delegate (GameObject tower)
    {
      tower.GetComponentInChildren<TowerController>().Shoot();
    });
  }
}
