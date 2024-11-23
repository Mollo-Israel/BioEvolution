using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Organismo : MonoBehaviour
{
    // Variable para definir la cantidad de puntos que da este organismo
    public int puntos = 1;

    // Referencia al PuntuacionManager (para aumentar la puntuaci�n)
    public PuntuacionManager puntuacionManager;

    // M�todo que se llama cuando hay una colisi�n
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Llamar al m�todo de aumentar puntuaci�n en el PuntuacionManager
            puntuacionManager.AumentarPuntuacion(puntos);

            // Destruir el organismo (o hacer que desaparezca)
            Destroy(gameObject);
        }
    }
}
