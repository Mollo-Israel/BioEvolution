using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 30f;   // Velocidad de movimiento del Leviat�n

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
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Organismo"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
        }
    }
}
