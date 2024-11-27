using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;          // Para reproducir m�sica
    public AudioClip relojClip;              // Clip para el sonido de reloj
    public AudioClip entradaLeviatanClip;    // Clip para el sonido de la entrada del Leviat�n
    public GameObject leviatanPrefab;        // El prefab del Leviat�n
    public Transform spawnPoint;             // Punto de aparici�n del Leviat�n
    public float timeLimit = 5f;             // Tiempo en segundos
    public Text timerText;                   // Para mostrar el contador (opcional)

    private float currentTime;               // Tiempo restante
    private bool leviatanSpawned = false;    // Para evitar que el Leviat�n se instancie varias veces

    void Start()
    {
        currentTime = timeLimit;  // Inicializa el contador
        audioSource = GetComponent<AudioSource>();
        timerText.text = currentTime.ToString("F2");  // Muestra el tiempo inicial
        StartCoroutine(TimeCountdown());   // Inicia el contador de tiempo
    }

    void Update()
    {
        if (currentTime <= 0f && !leviatanSpawned)
        {
            leviatanSpawned = true;
            SpawnLeviatan();  // Instancia el Leviat�n
        }

        if (currentTime > 0)
        {
            timerText.text = currentTime.ToString("F2");  // Actualiza el texto del temporizador
        }
    }

    // M�todo para el contador de tiempo
    IEnumerator TimeCountdown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 5f && !audioSource.isPlaying)  // Reproducir sonido cuando queden 5 segundos
            {
                audioSource.PlayOneShot(relojClip);
            }
            yield return null;
        }

        // Reproducir el sonido cuando el tiempo llega a 0
        audioSource.PlayOneShot(entradaLeviatanClip);
    }

    // M�todo para instanciar y mover al Leviat�n
    void SpawnLeviatan()
    {
        // Instanciamos el Leviat�n en el punto de aparici�n
        GameObject leviatan = Instantiate(leviatanPrefab, spawnPoint.position, Quaternion.identity);

        // Asignamos un script de movimiento
        LeviatanMovement leviatanMovement = leviatan.AddComponent<LeviatanMovement>();
        leviatanMovement.MoveLeviatan();  // Llamamos al movimiento del Leviat�n
    }
}
