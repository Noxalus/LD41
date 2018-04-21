using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
  public float range;

  private int _layerMask;

  // Use this for initialization
  void Start()
  {
    _layerMask = (1 << 10);

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.A))
    {
      Shoot();
    }
  }

  public void Shoot()
  {
    Debug.Log("Shoot!");
    RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, range, Vector3.down, range, _layerMask);
    if (hits.Length != 0)
    {
      TowerUtility.SortFromCenter(this.transform.position, hits);
      GameObject target = hits[0].collider.gameObject;
    }
  }
}
