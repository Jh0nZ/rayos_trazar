using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pruebas : EditorWindow
{
    private GameObject currentPrimitive;

    [MenuItem("Window/Create Primitives")]
    public static void ShowWindow()
    {
        GetWindow<Pruebas>("Create Primitives");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create a Primitive", EditorStyles.boldLabel);
        if (GUILayout.Button("Create Cube at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Cube);
        }

        if (GUILayout.Button("Create Sphere at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Sphere);
        }

        if (GUILayout.Button("Create Cylinder at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Cylinder);
        }

        if (GUILayout.Button("Create Capsule at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Capsule);
        }

        if (GUILayout.Button("Create Plane at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Plane);
        }

        if (GUILayout.Button("Create Quad at (0, 0, 0)"))
        {
            CreatePrimitive(PrimitiveType.Quad);
        }
    }

    private void CreatePrimitive(PrimitiveType type)
    {
        // Destroy the previous primitive if it exists
        if (currentPrimitive != null)
        {
            DestroyImmediate(currentPrimitive);
        }

        // Create a new primitive
        currentPrimitive = GameObject.CreatePrimitive(type);
        currentPrimitive.transform.position = Vector3.zero;
    }
}
