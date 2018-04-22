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
    Vector3? target = PointToFoe();
    if (!target.HasValue)
    {
      return;
    }
    transform.parent.rotation = Quaternion.Euler(
      Vector3.up *
      Vector2.Angle(
        TowerUtility.Vector2ToVector3(this.transform.position),
        TowerUtility.Vector2ToVector3(target.Value)));
    if (Input.GetKeyDown(KeyCode.A))
    {
      Shoot();
    }
  }

  public void Shoot()
  {
    Vector3? target = PointToFoe();
    if (!target.HasValue)
    {
      return;
    }
    GetComponent<AudioSource>().Play();
    GameObject bullet = Instantiate(prefabBullet, this.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
    Vector3 shoot = (target.Value - bullet.transform.position).normalized;
    bullet.GetComponent<Rigidbody>().AddForce(shoot * 500.0f);

  }

  private Vector3? PointToFoe()
  {
    RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, range, Vector3.up, 99, _layerMask);
    if (hits.Length == 0)
    {
      return null;
    }
    TowerUtility.SortFromCenter(this.transform.position, hits);
    GameObject target = hits[0].collider.gameObject;
    Vector3 targetPos = TowerUtility.predictedPosition(target.transform.position + Vector3.up * 0.25f, this.transform.position, target.GetComponent<NavMeshAgent>().velocity, 10f);
    return targetPos;
  }
}
