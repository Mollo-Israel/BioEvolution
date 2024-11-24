using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera; // La cámara principal
    private Vector2 screenBounds; // Los límites de la pantalla
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Obtén los límites de la cámara en coordenadas del mundo
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Calcula la mitad del tamaño del objeto para limitar bien
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        // Restringe el movimiento dentro de los límites
        Vector3 viewPos = transform.position;

        // Restringe las coordenadas X e Y para no salir del área visible
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
