using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionManager : MonoBehaviour
{
    // Variable para almacenar la puntuación
    public int puntuacion = 0;

    // Variable para mostrar la puntuación en la UI
    public Text textoPuntuacion;

    void Start()
    {
        // Inicializar el texto de puntuación al comienzo del juego
        ActualizarPuntuacion();
    }

    // Método para aumentar la puntuación
    public void AumentarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;  // Aumenta la puntuación por la cantidad de ADN consumido
        ActualizarPuntuacion();  // Actualiza el texto de la puntuación en la UI
    }

    // Método para actualizar el texto en pantalla
    void ActualizarPuntuacion()
    {
        textoPuntuacion.text = "Puntuación: " + puntuacion.ToString();  // Muestra el puntaje en el formato adecuado
    }
}
