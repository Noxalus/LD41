using System;
using UnityEngine;

public class TowerUtility
{
  public static void SortFromCenter(Vector3 origin, RaycastHit[] hits)
  {
    Array.Sort(hits, delegate (RaycastHit hit1, RaycastHit hit2)
    {
      Vector3 relativeTarget1 = origin - hit1.point;
      Vector3 relativeTarget2 = origin - hit2.point;
      return Mathf.FloorToInt((relativeTarget1.x + relativeTarget1.z) / 2 - (relativeTarget2.x + relativeTarget2.z) / 2);
    });
  }

  public static Vector3 predictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
  {
    Vector3 displacement = targetPosition - shooterPosition;
    float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
    //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
    if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
    {
      Debug.Log("Position prediction is not feasible.");
      return targetPosition;
    }
    //also Sine Formula
    float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
    return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
  }

  public static Vector2 Vector2ToVector3(Vector3 vec3)
  {
    return new Vector2(vec3.x, vec3.z);
  }
}