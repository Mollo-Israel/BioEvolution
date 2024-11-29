using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip sonidoAmbiente;  // Sonido de ambiente
    public AudioClip buttonStart;     // Sonido del botón
    public AudioSource audioSource;   // Referencia al AudioSource

    // Se llama al iniciar el juego
    public void Start()
    {
        // Asignar el clip de sonido de ambiente al AudioSource
        audioSource.clip = sonidoAmbiente;
        audioSource.loop = true;  // Hacer que el sonido se repita en bucle
        audioSource.Play();  // Iniciar la reproducción del sonido
    }

    // Método para reproducir el sonido de inicio (cuando el jugador hace clic en "Start")
    public void Play()
    {
        // Reproducir el sonido del botón (por ejemplo, un sonido de clic)
        audioSource.PlayOneShot(buttonStart);

        // Llamar a la función con retraso para cargar la escena después de 2 segundos
        StartCoroutine(CargarEscenaConRetraso());
    }

    // Corutina para retrasar la carga de la escena
    private IEnumerator CargarEscenaConRetraso()
    {
        // Espera 2 segundos antes de cambiar de escena
        yield return new WaitForSeconds(1);

        // Cargar la siguiente escena (asumiendo que la secuencia de escenas está configurada en el Build Settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Método para salir de la aplicación (funciona solo en la construcción final)
    public void Exit()
    {
        Application.Quit();

        // Si estamos en el editor de Unity, esta línea no cerrará la aplicación,
        // pero podemos simular la salida con un mensaje de consola:
#if UNITY_EDITOR
        Debug.Log("Aplicación cerrada");
#endif
    }
}
