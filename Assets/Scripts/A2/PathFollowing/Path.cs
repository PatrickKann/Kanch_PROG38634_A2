using UnityEngine;

public class Path: MonoBehaviour
{
    [SerializeField]
    private GameObject[] points;

    private Vector3[] waypoints;

    public bool isDebug = true;
    public float radius = 2.0f;

    public int PathLength {
        get { return points.Length; }
    }
    
    public Vector3 GetPoint(int index)
    {
        waypoints = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
            waypoints[i] = points[i].transform.position;

        return waypoints[index];
    }
    
    //private void OnDrawGizmos()
    //{
    //    if (!isDebug) {
    //        return;
    //    }

    //    for (int i = 0; i < waypoints.Length; i++)
    //    {
    //        if (i + 1 < waypoints.Length)
    //        {
    //            Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red);
    //        }
    //    }
    //}
}