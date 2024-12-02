using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManag : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip relojClip;
    public AudioClip entradaLeviatanClip;
    public GameObject[] prefabSet1;  // Primer conjunto de prefabs (para ronda 1)
    public GameObject[] prefabSet2;  // Segundo conjunto de prefabs (para ronda 2)
    public GameObject[] prefabSet3;  // Tercer conjunto de prefabs (para ronda 3)
    public GameObject[] prefabSet4;  // Cuarto conjunto de prefabs (para ronda 4)
    public GameObject[] prefabSet5;  // Quinto conjunto de prefabs (para ronda 5)
    public GameObject[] prefabSet6;  // Sexto conjunto de prefabs (para ronda 6)
    public GameObject[] prefabSet7;  // Séptimo conjunto de prefabs (para ronda 7)
    public GameObject[] prefabSet8;  // Octavo conjunto de prefabs (para ronda 8)
    public GameObject[] prefabSet9;  // Noveno conjunto de prefabs (para ronda 9)
    public Transform[] spawnPoints;  // Puntos de spawn (4 puntos)
    public Text timerText;
    public Text roundText;           // Texto para mostrar la ronda actual

    // Para el Leviatán
    public GameObject leviatanPrefab;           // Prefab del Leviatán
    public Transform spawnPointLeviatan;        // Punto de spawn del Leviatán

    public int totalRounds = 9;
    public int totalTime;                      // Tiempo total en segundos
    private int currentRound = 0;
    private bool leviatanSpawned = false;       // Para evitar que el Leviatán se instancie varias veces
    private Color originalColor;                // Color original del texto del temporizador
    private float redThreshold = 8f;            // Umbral para cambiar color a rojo
    private GameObject[] currentPrefabSet;      // Conjunto de prefabs actual según la ronda

    void Start()
    {
        originalColor = timerText.color;        // Guarda el color original del texto
        StartNewRound();                         // Inicia la primera ronda
    }

    void StartNewRound()
    {
        currentRound++;
        if (currentRound > totalRounds)
        {
            Debug.Log("¡El juego ha terminado!");
            return;
        }

        switch (currentRound)
        {
            case 1:
                currentPrefabSet = prefabSet1;
                break;
            case 2:
                currentPrefabSet = prefabSet2;
                break;
            case 3:
                currentPrefabSet = prefabSet3;
                break;
            case 4:
                currentPrefabSet = prefabSet4;
                break;
            case 5:
                currentPrefabSet = prefabSet5;
                break;
            case 6:
                currentPrefabSet = prefabSet6;
                break;
            case 7:
                currentPrefabSet = prefabSet7;
                break;
            case 8:
                currentPrefabSet = prefabSet8;
                break;
            case 9:
                currentPrefabSet = prefabSet9;
                break;
            default:
                currentPrefabSet = prefabSet1;  // Para las rondas posteriores, puede alternar o agregar más sets
                break;
        }

        // Verificar que el conjunto de prefabs actual no esté vacío
        if (currentPrefabSet == null || currentPrefabSet.Length == 0)
        {
            Debug.LogError("El conjunto de prefabs actual está vacío o no asignado.");
            return;
        }

        // Actualiza el texto de la ronda
        roundText.text = "Ronda: " + currentRound;

        // Inicia el temporizador para la ronda actual
        totalTime = 25;  // Cambia este valor por el tiempo que desees por ronda (ej. 2 min y 30 seg)
        leviatanSpawned = false;
        UpdateTimerText();
        StartCoroutine(TimeCountdown());
    }

    void UpdateTimerText()
    {
        int displayMinutes = totalTime / 60;
        int displaySeconds = totalTime % 60;
        timerText.text = string.Format("{0:D2}:{1:D2}", displayMinutes, displaySeconds);

        // Cambia el color del temporizador si el tiempo es menor a 8 segundos
        if (totalTime <= redThreshold)
        {
            timerText.color = Color.Lerp(originalColor, Color.red, 1 - (totalTime / redThreshold));
        }
    }

    IEnumerator TimeCountdown()
    {
        SpawnOrganisms();
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1);
            totalTime--;

            // Reproduce el sonido del reloj cuando el tiempo restante es de 8 segundos
            if (totalTime == 8 && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(relojClip);
            }

            UpdateTimerText();  // Actualiza el texto del temporizador
        }

        // Si el Leviatán no ha sido instanciado, lo hacemos ahora
        if (!leviatanSpawned)
        {
            SpawnLeviatan();
        }

        // Reinicia el contador y comienza una nueva ronda
        StartNewRound();
    }

    void SpawnLeviatan()
    {
        leviatanSpawned = true;
        audioSource.PlayOneShot(entradaLeviatanClip);  // Reproduce el sonido de entrada del Leviatán

        // Instanciamos el Leviatán en el punto de spawn asignado
        if (spawnPointLeviatan != null && leviatanPrefab != null)
        {
            GameObject leviatan = Instantiate(leviatanPrefab, spawnPointLeviatan.position, Quaternion.identity);
            Debug.Log("Leviatán ha entrado en: " + spawnPointLeviatan.position);
        }
        else
        {
            Debug.LogError("El Leviatán o el punto de spawn no están asignados.");
        }

        // Añadimos un tiempo de espera de 15 segundos antes de spawnear los organismos
        StartCoroutine(WaitAndSpawnOrganisms());
    }

    IEnumerator WaitAndSpawnOrganisms()
    {
        yield return new WaitForSeconds(10);
        SpawnOrganisms();
    }

    void SpawnOrganisms()
    {
        // Verificar si tenemos puntos de spawn
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados.");
            return;
        }

        // Para cada punto de spawn, instanciamos 3 organismos
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint == null)
            {
                Debug.LogError("Punto de spawn vacío encontrado.");
                continue;
            }

            for (int i = 0; i < 3; i++)
            {
                // Elegimos aleatoriamente entre los prefabs de la ronda actual
                int prefabIndex = Random.Range(0, currentPrefabSet.Length);
                GameObject organism = Instantiate(currentPrefabSet[prefabIndex], spawnPoint.position, Quaternion.identity);
                Debug.Log("Organismo instanciado en: " + spawnPoint.position);
            }
        }
    }
}
