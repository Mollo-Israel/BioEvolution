using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public AudioSource audioSource;          // Para reproducir m�sica
    public AudioClip relojClip;              // Clip para el sonido de reloj
    public AudioClip entradaLeviatanClip;    // Clip para el sonido de la entrada del Leviat�n
    public GameObject leviatanPrefab;        // El prefab del Leviat�n
    public Transform spawnPoint;             // Punto de aparici�n del Leviat�n
    public int minutes = 0;                  // Minutos en el temporizador
    public int seconds = 5;                  // Segundos en el temporizador
    public Text timerText;                   // Texto del temporizador en la UI

    private int totalTime;                   // Tiempo total en segundos
    private bool leviatanSpawned = false;    // Para evitar que el Leviat�n se instancie varias veces

    void Start()
    {
        totalTime = (minutes * 60) + seconds;  // Convierte minutos y segundos a total en segundos
        UpdateTimerText();                    // Actualiza el texto del temporizador
        StartCoroutine(TimeCountdown());      // Inicia el contador
    }

    void UpdateTimerText()
    {
        // Formatea el texto como mm:ss
        int displayMinutes = totalTime / 60;
        int displaySeconds = totalTime % 60;
        timerText.text = string.Format("{0:D2}:{1:D2}", displayMinutes, displaySeconds);
    }

    IEnumerator TimeCountdown()
    {
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1);
            totalTime--;

            // Reproducir el sonido del reloj si quedan 5 segundos
            if (totalTime == 5 && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(relojClip);
            }

            UpdateTimerText(); // Actualiza el texto del temporizador
        }

        if (!leviatanSpawned)
        {
            SpawnLeviatan();  // Instancia el Leviat�n
        }
    }

    void SpawnLeviatan()
    {
        leviatanSpawned = true;

        // Reproducir el sonido de entrada del Leviat�n
        audioSource.PlayOneShot(entradaLeviatanClip);

        // Instanciamos el Leviat�n en el punto de aparici�n
        GameObject leviatan = Instantiate(leviatanPrefab, spawnPoint.position, Quaternion.identity);
    }
}
