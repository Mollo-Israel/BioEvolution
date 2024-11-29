using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip sonidoAmbiente;  // El clip de sonido que quieres reproducir
    private AudioSource audioSource;  // El AudioSource para reproducir el sonido

    void Start()
    {
        // Asegúrate de que el objeto persista entre escenas
        DontDestroyOnLoad(gameObject);

        // Obtener o añadir el AudioSource
        audioSource = GetComponent<AudioSource>();

        // Reproducir el sonido ambiente en loop
        if (sonidoAmbiente != null && audioSource != null)
        {
            audioSource.clip = sonidoAmbiente;
            audioSource.loop = true;  // Hacer que el sonido se repita
            audioSource.Play();  // Iniciar la reproducción del sonido
        }

        // Suscribirnos al evento de la carga de una nueva escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Método que se llama cuando se carga una nueva escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificamos si la escena es la segunda escena, por ejemplo "GameScene"
        if (scene.name == "GameScene")
        {
            // Asegurarnos de que el sonido esté reproduciéndose
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    // Asegurarnos de que no se suscriba más de una vez al evento cuando se destruye el objeto
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
