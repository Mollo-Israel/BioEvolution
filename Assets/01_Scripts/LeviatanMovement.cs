using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviat�n

    void Update()
    {
        // Mueve el Leviat�n de izquierda a derecha
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("muerteLeviatan"))
        {
            // Destruir el Leviat�n al tocar el l�mite
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el Leviat�n colisiona con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir el jugador
            Destroy(collision.gameObject);
            // Aqu� puedes agregar cualquier l�gica adicional que desees, como mostrar una animaci�n o un efecto de "comerse"
            Debug.Log("�El Leviat�n se ha comido al jugador!");

            // Si quieres que tambi�n el Leviat�n sea destruido despu�s de la colisi�n, puedes a�adir esta l�nea:
            // Destroy(gameObject);
        }

        // Si el Leviat�n colisiona con enemigos
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir al enemigo
            Destroy(collision.gameObject);
            // Aqu� puedes agregar l�gica para efectos visuales, sonoros, puntuaci�n, etc.
            Debug.Log("�El Leviat�n se ha comido a un enemigo!");
        }
    }
}
