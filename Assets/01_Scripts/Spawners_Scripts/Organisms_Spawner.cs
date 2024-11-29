using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organisms_Spawner : MonoBehaviour   
{

    // Prefab del organismo
    public GameObject organismoPrefab;

    // Tiempo entre los spawns
    public float tiempoEntreSpawns = 0.5f;

    // Rango de aparici�n en el eje X
    public float rangoXD = 22f;
    public float rangoXI = -20f;

    // Rango de aparici�n en el eje Y (la posici�n siempre ser� en la parte superior)
    public float rangoY = 5f;

    // Velocidad con la que los organismos caen
    public float velocidadCaida = 2f;

    private void Start()
    {
        // Comienza el proceso de spawn con un intervalo
        InvokeRepeating("GenerarOrganismo", 0f, tiempoEntreSpawns);
    }

    // M�todo para generar un organismo en una posici�n aleatoria
    void GenerarOrganismo()
    {
        // Calcula una posici�n aleatoria dentro del rango X
        float posX = Random.Range(rangoXI, rangoXD);

        // La posici�n Y siempre ser� el valor m�ximo
        float posY = rangoY;

        // Generar el nuevo organismo
        GameObject organismo = Instantiate(organismoPrefab, new Vector3(posX, posY, 0f), Quaternion.identity);

        // A�adir movimiento hacia abajo
        organismo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocidadCaida);
    }
}
