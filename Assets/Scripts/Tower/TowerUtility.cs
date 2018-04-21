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
}