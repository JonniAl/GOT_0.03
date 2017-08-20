using System.Collections.Generic;
using UnityEngine;

public class SpawningPoint : MonoBehaviour
{
    public static List<Vector3> spawningPointPosition = new List<Vector3>();
    private static List<Quaternion> spawningPointRotation = new List<Quaternion>();

    private void Awake()
    {
        spawningPointPosition.Add(transform.position);
        spawningPointRotation.Add(transform.rotation);

        Destroy(gameObject);
    }

    public static List<Vector3> GetSpawningPointsPosition()
    {
        return new List<Vector3>(spawningPointPosition);
    }

    public static List<Quaternion> GetSpawningPointsRotation()
    {
        return new List<Quaternion>(spawningPointRotation);
    }

    public static void ClearSpawningPoints()
    {
        spawningPointPosition.Clear();
        spawningPointRotation.Clear();
    }  
}
