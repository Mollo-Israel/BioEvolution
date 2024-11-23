using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionManager : MonoBehaviour
{
    // Variable para almacenar la puntuaci�n
    public int puntuacion = 0;

    // Variable para mostrar la puntuaci�n en la UI
    public Text textoPuntuacion;

    void Start()
    {
        // Inicializar el texto de puntuaci�n al comienzo del juego
        ActualizarPuntuacion();
    }

    // M�todo para aumentar la puntuaci�n
    public void AumentarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;  // Aumenta la puntuaci�n por la cantidad de ADN consumido
        ActualizarPuntuacion();  // Actualiza el texto de la puntuaci�n en la UI
    }

    // M�todo para actualizar el texto en pantalla
    void ActualizarPuntuacion()
    {
        textoPuntuacion.text = "Puntuaci�n: " + puntuacion.ToString();  // Muestra el puntaje en el formato adecuado
    }
}
