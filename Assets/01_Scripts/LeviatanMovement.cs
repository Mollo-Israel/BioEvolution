using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviatán
    public float distance = 50f;    // Distancia que recorrerá el Leviatán hacia la derecha (ajústalo según sea necesario)
    public string limitTag = "LimiteX"; // Tag para los objetos límite que definen hasta dónde llega el Leviatán

    private Vector3 startPosition;  // Posición inicial del Leviatán
    private Vector3 targetPosition; // Posición objetivo del Leviatán

    void Start()
    {
        startPosition = transform.position;  // Posición inicial del Leviatán
        // Inicializamos el objetivo a la derecha
        targetPosition = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
    }

    public void MoveLeviatan()
    {
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        // Mover el Leviatán de izquierda a derecha hasta el objetivo
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Método para manejar el trigger y detectar los límites de la escena
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el Leviatán llega al límite de la escena (con el tag que definimos)
        if (other.CompareTag(limitTag))
        {
            // Detener el movimiento cuando alcanza el límite
            // Dejar de mover el Leviatán
            StopAllCoroutines();  // Detener el movimiento cuando llegue al límite
            Debug.Log("Leviatán ha llegado al límite y se detuvo");
        }
    }
}
