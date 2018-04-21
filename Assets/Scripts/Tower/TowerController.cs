using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, range, Vector3.up, 99, _layerMask);
    if (hits.Length != 0)
    {
      GetComponent<AudioSource>().Play();
      TowerUtility.SortFromCenter(this.transform.position, hits);
      GameObject target = hits[0].collider.gameObject;
      GameObject bullet = Instantiate(prefabBullet, this.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
      Vector3 targetPos = TowerUtility.predictedPosition(target.transform.position + Vector3.up * 0.25f, this.transform.position, target.GetComponent<NavMeshAgent>().velocity, 10f);
      Vector3 shoot = (targetPos - bullet.transform.position).normalized;
      bullet.GetComponent<Rigidbody>().AddForce(shoot * 500.0f);
    }
  }
}
