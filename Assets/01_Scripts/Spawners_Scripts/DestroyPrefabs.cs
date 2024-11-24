using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefabs : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that touched the ground has the tag "Point"
        if (collision.gameObject.CompareTag("Suelo"))
        {
            // Destroy the object
            Destroy(collision.gameObject);
        }
    }
}
