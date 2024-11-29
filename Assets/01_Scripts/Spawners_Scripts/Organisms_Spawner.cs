using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organisms_Spawner : MonoBehaviour   
{

    // Prefab del organismo
    public GameObject organismoPrefab;

    // Tiempo entre los spawns
    public float tiempoEntreSpawns = 0.5f;

    // Rango de aparición en el eje X
    public float rangoXD = 22f;
    public float rangoXI = -20f;

    // Rango de aparición en el eje Y (la posición siempre será en la parte superior)
    public float rangoY = 5f;

    // Velocidad con la que los organismos caen
    public float velocidadCaida = 2f;

    private void Start()
    {
        // Comienza el proceso de spawn con un intervalo
        InvokeRepeating("GenerarOrganismo", 0f, tiempoEntreSpawns);
    }

    // Método para generar un organismo en una posición aleatoria
    void GenerarOrganismo()
    {
        // Calcula una posición aleatoria dentro del rango X
        float posX = Random.Range(rangoXI, rangoXD);

        // La posición Y siempre será el valor máximo
        float posY = rangoY;

        // Generar el nuevo organismo
        GameObject organismo = Instantiate(organismoPrefab, new Vector3(posX, posY, 0f), Quaternion.identity);

        // Añadir movimiento hacia abajo
        organismo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocidadCaida);
    }
}
