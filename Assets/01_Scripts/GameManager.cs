using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;          // Para reproducir música
    public AudioClip relojClip;              // Clip para el sonido de reloj
    public AudioClip entradaLeviatanClip;    // Clip para el sonido de la entrada del Leviatán
    public GameObject leviatanPrefab;        // El prefab del Leviatán
    public Transform spawnPoint;             // Punto de aparición del Leviatán
    public float timeLimit = 5f;             // Tiempo en segundos
    public Text timerText;                   // Para mostrar el contador (opcional)

    private float currentTime;               // Tiempo restante
    private bool leviatanSpawned = false;    // Para evitar que el Leviatán se instancie varias veces

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
            SpawnLeviatan();  // Instancia el Leviatán
        }

        if (currentTime > 0)
        {
            timerText.text = currentTime.ToString("F2");  // Actualiza el texto del temporizador
        }
    }

    // Método para el contador de tiempo
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

    // Método para instanciar y mover al Leviatán
    void SpawnLeviatan()
    {
        // Instanciamos el Leviatán en el punto de aparición
        GameObject leviatan = Instantiate(leviatanPrefab, spawnPoint.position, Quaternion.identity);

        // Asignamos un script de movimiento
        LeviatanMovement leviatanMovement = leviatan.AddComponent<LeviatanMovement>();
        leviatanMovement.MoveLeviatan();  // Llamamos al movimiento del Leviatán
    }
}
