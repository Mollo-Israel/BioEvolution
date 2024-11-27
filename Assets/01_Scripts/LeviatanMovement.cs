using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviat�n
    public float distance = 50f;    // Distancia que recorrer� el Leviat�n hacia la derecha (aj�stalo seg�n sea necesario)
    public string limitTag = "LimiteX"; // Tag para los objetos l�mite que definen hasta d�nde llega el Leviat�n

    private Vector3 startPosition;  // Posici�n inicial del Leviat�n
    private Vector3 targetPosition; // Posici�n objetivo del Leviat�n

    void Start()
    {
        startPosition = transform.position;  // Posici�n inicial del Leviat�n
        // Inicializamos el objetivo a la derecha
        targetPosition = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
    }

    public void MoveLeviatan()
    {
        StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget()
    {
        // Mover el Leviat�n de izquierda a derecha hasta el objetivo
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // M�todo para manejar el trigger y detectar los l�mites de la escena
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el Leviat�n llega al l�mite de la escena (con el tag que definimos)
        if (other.CompareTag(limitTag))
        {
            // Detener el movimiento cuando alcanza el l�mite
            // Dejar de mover el Leviat�n
            StopAllCoroutines();  // Detener el movimiento cuando llegue al l�mite
            Debug.Log("Leviat�n ha llegado al l�mite y se detuvo");
        }
    }
}
