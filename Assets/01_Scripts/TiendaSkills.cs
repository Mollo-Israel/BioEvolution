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
    public AudioSource audioSource;  // Referencia al componente AudioSource
    public AudioClip sonidoCompraVelocidad;  // Sonido para la compra de velocidad

    // Array de objetos hijos que representan las diferentes colas de evolución
    public GameObject[] colas;  // Arreglo de las colas de los diferentes niveles (de 1 a 5)

    public Sprite nuevoSprite;  // Nuevo sprite para el jugador

    // Nivel actual de la habilidad de velocidad
    public int nivelCola = 0;

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

        // Inicialmente deshabilitar todas las colas
        foreach (GameObject cola in colas)
        {
            if (cola != null)
            {
                // Desactivar todos los objetos cola al principio
                cola.SetActive(false);

                // Desactivar también SpriteRenderer y Collider2D de cada cola, por si están activos al principio
                SpriteRenderer spriteRenderer = cola.GetComponent<SpriteRenderer>();
                Collider2D collider = cola.GetComponent<Collider2D>();

                if (spriteRenderer != null)
                    spriteRenderer.enabled = false;

                if (collider != null)
                    collider.enabled = false;
            }
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

                    // Verificar si la cola puede evolucionar
                    if (nivelCola < colas.Length)
                    {
                        // Desactivar la cola anterior (si hay una)
                        if (nivelCola > 0 && colas[nivelCola - 1] != null)
                        {
                            colas[nivelCola - 1].SetActive(false);  // Desactivar la cola anterior

                            // Deshabilitar también su SpriteRenderer y Collider2D si estaban activados
                            SpriteRenderer spriteRenderer = colas[nivelCola - 1].GetComponent<SpriteRenderer>();
                            Collider2D collider = colas[nivelCola - 1].GetComponent<Collider2D>();

                            if (spriteRenderer != null)
                                spriteRenderer.enabled = false;

                            if (collider != null)
                                collider.enabled = false;
                        }

                        // Activar la siguiente cola
                        if (colas[nivelCola] != null)
                        {
                            colas[nivelCola].SetActive(true);  // Activar la siguiente cola

                            // Habilitar el SpriteRenderer y Collider2D del nuevo objeto de cola
                            SpriteRenderer spriteRenderer = colas[nivelCola].GetComponent<SpriteRenderer>();
                            Collider2D collider = colas[nivelCola].GetComponent<Collider2D>();

                            if (spriteRenderer != null)
                                spriteRenderer.enabled = true;

                            if (collider != null)
                                collider.enabled = true;
                        }

                        // Incrementar el nivel de la cola
                        nivelCola++;
                    }

                    // Reproducir el sonido de compra de velocidad
                    if (audioSource != null && sonidoCompraVelocidad != null)
                    {
                        audioSource.PlayOneShot(sonidoCompraVelocidad);
                    }
                    break;

                case Habilidad.Defensa:
                    jugador.velocidadMovimiento += 2f;  // Aumentar defensa (puedes cambiar el comportamiento aquí)
                    Debug.Log("Defensa Aumentada");
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

