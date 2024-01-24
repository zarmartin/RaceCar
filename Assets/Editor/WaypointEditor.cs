using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGismos(WayPoint waypoint, GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.blue * 0.5f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.waypointWidth / 2f),
                        waypoint.transform.position - (waypoint.transform.right * waypoint.waypointWidth / 2f));

        if(waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 _offset = waypoint.transform.right * waypoint.waypointWidth / 2f;
            Vector3 _offsetTo = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.waypointWidth / 2f;

            Gizmos.DrawLine(waypoint.transform.position + _offset, waypoint.previousWaypoint.transform.position + _offsetTo);
        }

        if(waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 _offset = waypoint.transform.right * -waypoint.waypointWidth / 2f;
            Vector3 _offsetTo = waypoint.previousWaypoint.transform.right * -waypoint.previousWaypoint.waypointWidth / 2f;

            Gizmos.DrawLine(waypoint.transform.position + _offset, waypoint.previousWaypoint.transform.position + _offsetTo);
        }
    }
}
