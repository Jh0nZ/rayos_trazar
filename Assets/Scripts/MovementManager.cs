using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float speed = 10.0f;
    public float xMin = -4.0f;
    public float xMax = 8.0f;
    public float yMin = -3.0f;
    public float yMax = 5.0f;
    public float zMin = -5.0f;
    public float zMax = 5.0f;
    private GameObject currentObject; // Objeto actual que se moverá
    void Update()
    {
        if (currentObject == null)
        {
            // Si no hay objeto actual, no se hace ningún movimiento
            return;
        }

        // Movimiento en el eje X
        float moveX = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveX = -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveX = speed * Time.deltaTime;
        }

        // Movimiento en el eje Y
        float moveY = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveY = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveY = -speed * Time.deltaTime;
        }
        // Movimiento en el eje Z
        float moveZ = 0;
        if (Input.GetKey(KeyCode.E))
        {
            moveZ = speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveZ = -speed * Time.deltaTime;
        }

        // Nueva posición del objeto
        Vector3 newPosition = currentObject.transform.position + new Vector3(moveX, moveY, moveZ);

        // Aplicar límites
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);

        // Actualizar posición del objeto
        currentObject.transform.position = newPosition;
    }
    public void CambiarCurrentObject(GameObject nuevoObjeto)
    {
        currentObject = nuevoObjeto;
    }
    public void ResetObjectPosition()
    {
        if (currentObject != null)
        {
            currentObject.transform.position = Vector3.zero;
        }
    }
}
