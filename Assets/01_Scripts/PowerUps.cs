using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public GameObject powerUp1Prefab;
    public GameObject powerUp2Prefab;
    public GameObject powerUp3Prefab;
    public GameObject powerUp4Prefab;

    // Probabilidades individuales para cada PowerUp
    [Range(0, 100)]
    public float powerUp1Probability = 25f; // Probabilidad del PowerUp 1
    [Range(0, 100)]
    public float powerUp2Probability = 25f; // Probabilidad del PowerUp 2
    [Range(0, 100)]
    public float powerUp3Probability = 25f; // Probabilidad del PowerUp 3
    [Range(0, 100)]
    public float powerUp4Probability = 25f; // Probabilidad del PowerUp 4

    public float timeBtwSpawn = 20.0f; // Tiempo entre spawns (cada 20 segundos)
    public float increaseInterval = 10.0f; // Intervalo de tiempo después del cual aumentan las probabilidades
    public float increaseAmount = 10.0f; // Cuánto aumentan las probabilidades
    public float decreaseAmount = 5.0f; // Cuánto disminuye la probabilidad después de spawn
    private float elapsedTime = 0f; // Tiempo total transcurrido

    private float timer = 0;

    public static PowerUps instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        // Solo intentamos spawnear si el tiempo ha pasado
        if (timer < timeBtwSpawn)
        {
            timer += Time.deltaTime; // Aumenta el temporizador con el tiempo transcurrido
        }
        else
        {
            // Reinicia el temporizador y aumenta el tiempo total transcurrido
            timer = 0;
            elapsedTime += timeBtwSpawn;

            // Incrementar las probabilidades de los power-ups con baja probabilidad
            IncreaseProbabilities();

            // Spawn fijo de PowerUp
            SpawnPowerUps();
        }
    }

    void SpawnPowerUps()
    {
        // Elige un PowerUp aleatorio según las probabilidades
        int powerUpIndex = GetRandomPowerUpIndex();

        // Generar una posición aleatoria dentro de los puntos de spawn
        float x = Random.Range(leftPoint.position.x, rightPoint.position.x);
        Vector3 newPos = new Vector3(x, transform.position.y, transform.position.z);

        // Instanciar el PowerUp
        switch (powerUpIndex)
        {
            case 0:
                Instantiate(powerUp1Prefab, newPos, Quaternion.identity);
                break;
            case 1:
                Instantiate(powerUp2Prefab, newPos, Quaternion.identity);
                break;
            case 2:
                Instantiate(powerUp3Prefab, newPos, Quaternion.identity);
                break;
            case 3:
                Instantiate(powerUp4Prefab, newPos, Quaternion.identity);
                break;
        }

        // Después de spawn, disminuir la probabilidad del PowerUp spawneado
        DecreaseProbability(powerUpIndex);
    }

    // Función que obtiene el índice del PowerUp a spawnear según las probabilidades
    int GetRandomPowerUpIndex()
    {
        // Generar un número aleatorio entre 0 y 100 para seleccionar el PowerUp
        float rand = Random.Range(0f, 100f);
        float cumulativeProbability = 0f;

        // Comparar el número aleatorio con las probabilidades de cada PowerUp
        cumulativeProbability += powerUp1Probability;
        if (rand < cumulativeProbability)
            return 0;

        cumulativeProbability += powerUp2Probability;
        if (rand < cumulativeProbability)
            return 1;

        cumulativeProbability += powerUp3Probability;
        if (rand < cumulativeProbability)
            return 2;

        cumulativeProbability += powerUp4Probability;
        if (rand < cumulativeProbability)
            return 3;

        return 3; // Si no se seleccionó ninguno, devolver el último (por seguridad)
    }

    // Aumenta las probabilidades de power-ups de baja probabilidad después de cierto tiempo
    void IncreaseProbabilities()
    {
        // Si ha pasado el tiempo para aumentar las probabilidades
        if (elapsedTime >= increaseInterval)
        {
            IncreaseProbability(ref powerUp1Probability);
            IncreaseProbability(ref powerUp2Probability);
            IncreaseProbability(ref powerUp3Probability);
            IncreaseProbability(ref powerUp4Probability);

            elapsedTime = 0f; // Resetear el contador de tiempo
        }
    }

    // Función para aumentar la probabilidad
    void IncreaseProbability(ref float probability)
    {
        if (probability < 100) // Evitar que la probabilidad supere 100%
        {
            probability += increaseAmount; // Aumentar la probabilidad
            if (probability > 100)
            {
                probability = 100;
            }
        }
    }

    // Disminuir la probabilidad de un PowerUp después de que ha sido spawneado
    void DecreaseProbability(int powerUpIndex)
    {
        float[] probabilities = { powerUp1Probability, powerUp2Probability, powerUp3Probability, powerUp4Probability };
        if (probabilities[powerUpIndex] > 0)
        {
            probabilities[powerUpIndex] -= decreaseAmount; // Disminuir la probabilidad
            if (probabilities[powerUpIndex] < 0) // Limitar a la probabilidad mínima
            {
                probabilities[powerUpIndex] = 0;
            }
        }

        // Actualizar las probabilidades después de decrementar
        powerUp1Probability = probabilities[0];
        powerUp2Probability = probabilities[1];
        powerUp3Probability = probabilities[2];
        powerUp4Probability = probabilities[3];
    }
}
