using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviat�n
    public string limitTag = "LimitX+"; // Tag del l�mite donde el Leviat�n ser� destruido

    void Update()
    {
        // Mueve el Leviat�n de izquierda a derecha
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(limitTag))
        {
            // Destruir el Leviat�n al tocar el l�mite
            Destroy(gameObject);
        }
    }
}
