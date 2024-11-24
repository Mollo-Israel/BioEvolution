using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionManager : MonoBehaviour
{
    //crar instancia de puntuacion para acceder a esta clase
    public static PuntuacionManager intance;

    // Variable para almacenar la puntuaci�n
    public int puntuacion = 0;

    // Variable para mostrar la puntuaci�n en la UI
    public Text textoPuntuacionadnnormal;

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
        textoPuntuacionadnnormal.text = "X: " + puntuacion.ToString();  // Muestra el puntaje en el formato adecuado
    }
    public void ActualizarADNfuerte(int adnStrong)
    {
        //visualizar texto con adn fuerte

    }
}
