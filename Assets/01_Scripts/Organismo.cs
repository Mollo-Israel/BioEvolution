using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Organismo : MonoBehaviour
{
    // Referencia al texto de puntuación (debe arrastrarse desde el inspector)
    public Text textoPuntuacion;

    // Cantidad de puntos que otorga este organismo
    public int puntos = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colisiona tiene el tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Asegurarse de que el texto tiene el formato esperado
            if (textoPuntuacion.text.Contains(":"))
            {
                // Dividir el texto y obtener la puntuación actual
                string[] partes = textoPuntuacion.text.Split(':');
                if (partes.Length > 1)
                {
                    // Parsear la puntuación actual y sumarle los puntos de este organismo
                    int puntuacionActual = int.Parse(partes[1].Trim());
                    puntuacionActual += puntos;

                    // Actualizar el texto con la nueva puntuación
                    textoPuntuacion.text = "X: " + puntuacionActual;
                }
            }
            else
            {
                // Si el formato no es correcto, inicializar la puntuación
                textoPuntuacion.text = "X: " + puntos;
            }

            // Destruir el organismo (prefab)
            Destroy(gameObject);
        }
    }

}
