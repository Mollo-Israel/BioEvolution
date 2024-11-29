using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiendaSkills : MonoBehaviour
{
    public Player jugador;  // Referencia al jugador
    public Button[] botonesHabilidades;  // Array de botones para comprar habilidades
    public int puntosParaComprar = 5;  // Puntos necesarios para comprar una habilidad
    public Text textoPuntosJugador;  // Texto para mostrar los puntos del jugador

    // Enum que define las habilidades disponibles
    public enum Habilidad
    {
        AumentarVelocidad,
        Defensa,
        // Agrega más habilidades aquí
    }

    void Start()
    {
        // Asignar las funciones a los botones (debe ser el mismo número de botones que habilidades)
        for (int i = 0; i < botonesHabilidades.Length; i++)
        {
            int index = i;  // Capturar el índice para usar en el listener
            botonesHabilidades[i].onClick.AddListener(() => ComprarHabilidad((Habilidad)index));
        }

        
    }

    // Método que se llama al comprar una habilidad
    public void ComprarHabilidad(Habilidad habilidad)
    {
        // Verificar si el jugador tiene suficientes puntos
        if (jugador.puntos >= puntosParaComprar)
        {
            // Restar los puntos del jugador
            jugador.puntos -= puntosParaComprar;

            // Activar la habilidad correspondiente usando un switch
            switch (habilidad)
            {
                case Habilidad.AumentarVelocidad:
                    jugador.velocidadMovimiento += 2f;  // Aumentar la velocidad del jugador
                    
                    break;

                case Habilidad.Defensa:
                    jugador.velocidadMovimiento += 2f;  // Probar y cambiar 
                    Debug.Log("Defensa Aumentado");
                    break;

                default:
                    Debug.Log("Habilidad no reconocida");
                    break;
            }

            // Actualizar la UI de puntos en el jugador
            ActualizarTextoPuntos();
        }
        else
        {
            // Si no tiene puntos suficientes
            Debug.Log("No tienes suficientes puntos para comprar esta habilidad.");
        }
    }

    // Método para actualizar el texto de los puntos del jugador
    public void ActualizarTextoPuntos()
    {
        if (textoPuntosJugador != null)
        {
            textoPuntosJugador.text = "X" + jugador.puntos.ToString();
        }
    }
}

