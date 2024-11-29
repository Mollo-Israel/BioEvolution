using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip sonidoAmbiente;  // Sonido de ambiente
    public AudioClip buttonStart;     // Sonido del bot�n
    public AudioSource audioSource;   // Referencia al AudioSource

    // Se llama al iniciar el juego
    public void Start()
    {
        // Asignar el clip de sonido de ambiente al AudioSource
        audioSource.clip = sonidoAmbiente;
        audioSource.loop = true;  // Hacer que el sonido se repita en bucle
        audioSource.Play();  // Iniciar la reproducci�n del sonido
    }

    // M�todo para reproducir el sonido de inicio (cuando el jugador hace clic en "Start")
    public void Play()
    {
        // Reproducir el sonido del bot�n (por ejemplo, un sonido de clic)
        audioSource.PlayOneShot(buttonStart);

        // Llamar a la funci�n con retraso para cargar la escena despu�s de 2 segundos
        StartCoroutine(CargarEscenaConRetraso());
    }

    // Corutina para retrasar la carga de la escena
    private IEnumerator CargarEscenaConRetraso()
    {
        // Espera 2 segundos antes de cambiar de escena
        yield return new WaitForSeconds(1);

        // Cargar la siguiente escena (asumiendo que la secuencia de escenas est� configurada en el Build Settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // M�todo para salir de la aplicaci�n (funciona solo en la construcci�n final)
    public void Exit()
    {
        Application.Quit();

        // Si estamos en el editor de Unity, esta l�nea no cerrar� la aplicaci�n,
        // pero podemos simular la salida con un mensaje de consola:
#if UNITY_EDITOR
        Debug.Log("Aplicaci�n cerrada");
#endif
    }
}
