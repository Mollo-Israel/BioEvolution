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
        // Aseg�rate de que el objeto persista entre escenas
        DontDestroyOnLoad(gameObject);

        // Obtener o a�adir el AudioSource
        audioSource = GetComponent<AudioSource>();

        // Reproducir el sonido ambiente en loop
        if (sonidoAmbiente != null && audioSource != null)
        {
            audioSource.clip = sonidoAmbiente;
            audioSource.loop = true;  // Hacer que el sonido se repita
            audioSource.Play();  // Iniciar la reproducci�n del sonido
        }

        // Suscribirnos al evento de la carga de una nueva escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // M�todo que se llama cuando se carga una nueva escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificamos si la escena es la segunda escena, por ejemplo "GameScene"
        if (scene.name == "GameScene")
        {
            // Asegurarnos de que el sonido est� reproduci�ndose
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    // Asegurarnos de que no se suscriba m�s de una vez al evento cuando se destruye el objeto
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
