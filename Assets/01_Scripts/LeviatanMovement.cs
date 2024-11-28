using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviatán

    void Update()
    {
        // Mueve el Leviatán de izquierda a derecha
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("muerteLeviatan"))
        {
            // Destruir el Leviatán al tocar el límite
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el Leviatán colisiona con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir el jugador
            Destroy(collision.gameObject);
            // Aquí puedes agregar cualquier lógica adicional que desees, como mostrar una animación o un efecto de "comerse"
            Debug.Log("¡El Leviatán se ha comido al jugador!");

            // Si quieres que también el Leviatán sea destruido después de la colisión, puedes añadir esta línea:
            // Destroy(gameObject);
        }

        // Si el Leviatán colisiona con enemigos
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir al enemigo
            Destroy(collision.gameObject);
            // Aquí puedes agregar lógica para efectos visuales, sonoros, puntuación, etc.
            Debug.Log("¡El Leviatán se ha comido a un enemigo!");
        }
    }
}
