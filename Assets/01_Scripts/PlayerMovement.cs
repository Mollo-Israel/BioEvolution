using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera; // La c�mara principal
    private Vector2 screenBounds; // Los l�mites de la pantalla
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Obt�n los l�mites de la c�mara en coordenadas del mundo
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Calcula la mitad del tama�o del objeto para limitar bien
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {
        // Restringe el movimiento dentro de los l�mites
        Vector3 viewPos = transform.position;

        // Restringe las coordenadas X e Y para no salir del �rea visible
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
