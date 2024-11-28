using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeviatanMovement : MonoBehaviour
{
    public float moveSpeed = 3f;   // Velocidad de movimiento del Leviatán
    public string limitTag = "LimitX+"; // Tag del límite donde el Leviatán será destruido

    void Update()
    {
        // Mueve el Leviatán de izquierda a derecha
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(limitTag))
        {
            // Destruir el Leviatán al tocar el límite
            Destroy(gameObject);
        }
    }
}
