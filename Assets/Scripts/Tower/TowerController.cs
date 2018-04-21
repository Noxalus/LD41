﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
  public float range;
  public GameObject prefabBullet;

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
    RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, range, Vector3.up, 99, _layerMask);
    if (hits.Length != 0)
    {
      Debug.Log("Find enemy to target !");
      TowerUtility.SortFromCenter(this.transform.position, hits);
      GameObject target = hits[0].collider.gameObject;
      GameObject bullet = Instantiate(prefabBullet, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
      Vector3 shoot = (target.transform.position - bullet.transform.position).normalized;
      bullet.GetComponent<Rigidbody>().AddForce(shoot * 600.0f);
    }
  }
}
