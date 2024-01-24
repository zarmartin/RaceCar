using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class WaypointManagerWindow : EditorWindow
{
   [MenuItem("Window/Waypoint Editor Tools")]
   public static void ShowWindow()
    {
        GetWindow<WaypointManagerWindow>("Waypoint Editor Tools");
    }

    public Transform waypointOrigin;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointOrigin"));

        if(waypointOrigin == null)
        {
            EditorGUILayout.HelpBox("Please assing a Waypoint origin transform. ", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            CreateButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void CreateButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        WayPoint _waypoint = waypointObject.GetComponent<WayPoint>();

        if(waypointOrigin.childCount > 1)
        {
            _waypoint.previousWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount - 2).GetComponent<WayPoint>();
            _waypoint.previousWaypoint.nextWaypoint = _waypoint;

            _waypoint.transform.position = _waypoint.previousWaypoint.transform.position;
            _waypoint.transform.forward = _waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = _waypoint.gameObject;
    }
}
