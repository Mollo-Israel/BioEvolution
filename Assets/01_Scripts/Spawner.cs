using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public AudioSource audioSource;          // Para reproducir música
    public AudioClip relojClip;              // Clip para el sonido de reloj
    public AudioClip entradaLeviatanClip;    // Clip para el sonido de la entrada del Leviatán
    public GameObject leviatanPrefab;        // El prefab del Leviatán
    public Transform spawnPoint;             // Punto de aparición del Leviatán
    public int minutes = 2;                  // Cambia los minutos a 2
    public int seconds = 30;                 // Cambia los segundos a 30
    public Text timerText;                   // Texto del temporizador en la UI

    private int totalTime;                   // Tiempo total en segundos
    private bool leviatanSpawned = false;    // Para evitar que el Leviatán se instancie varias veces

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
            totalTime--;  // Disminuye el tiempo en cada segundo

            // Mostrar en la consola el tiempo restante para depuración
            Debug.Log("Tiempo restante: " + totalTime);

            // Reproducir el sonido del reloj si quedan 5 segundos
            if (totalTime == 8 && !audioSource.isPlaying)  // Verifica si quedan 5 segundos y si no está sonando
            {
                Debug.Log("Reproduciendo sonido de reloj...");
                audioSource.PlayOneShot(relojClip);  // Reproduce el sonido del reloj
            }

            UpdateTimerText(); // Actualiza el texto del temporizador
        }

        // Si el Leviatán aún no ha sido instanciado
        if (!leviatanSpawned)
        {
            SpawnLeviatan();  // Instancia el Leviatán
        }
    }

    void SpawnLeviatan()
    {
        leviatanSpawned = true;  // Marca el Leviatán como instanciado


        Debug.Log("Reproduciendo sonido de entrada del Leviatán...");
        audioSource.PlayOneShot(entradaLeviatanClip);  // Reproduce el sonido de entrada del Leviatán


        // Instanciamos el Leviatán en el punto de aparición
        GameObject leviatan = Instantiate(leviatanPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Leviatán instanciado");
    }
}

