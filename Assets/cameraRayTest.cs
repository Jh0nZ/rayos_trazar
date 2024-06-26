using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRayTest : MonoBehaviour
{
    public int debugPointCountX = 10;
    public int debugPointCountY = 10;
    public float sphereSize = 0.1f;
    public Material sphereMaterial; 
    void Update()
    {
        Camera cam = Camera.main;
        Transform camt = cam.transform;
        float planeHeight = cam.nearClipPlane * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2;
        float planeWidth = planeHeight * cam.aspect;

        Vector3 bottomLeftLocal = new Vector3(-planeWidth / 2, -planeHeight / 2, cam.nearClipPlane);

        for (int x = 0; x < debugPointCountX; x++)
        {
            for (int y = 0; y < debugPointCountY; y++)
            {
                float tx = (float)x / (debugPointCountX - 1f);
                float ty = (float)y / (debugPointCountY - 1f);

                Vector3 pointLocal = bottomLeftLocal + new Vector3(planeWidth * tx, planeHeight * ty, 0);
                Vector3 point = camt.position + camt.right * pointLocal.x + camt.up * pointLocal.y + camt.forward * pointLocal.z;
                
                DrawPoint(point);
            }
        }
    }

    // Example placeholder for DrawPoint function
    void DrawPoint(Vector3 point)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = point;
        sphere.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);

        if (sphereMaterial != null)
        {
            sphere.GetComponent<Renderer>().material = sphereMaterial;
        }

        // Optionally, you can also destroy the collider component of the sphere
        Destroy(sphere.GetComponent<Collider>());
    }
}
