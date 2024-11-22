using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float velocidadMovimiento = 5f;

    // Referencia al Rigidbody2D
    private Rigidbody2D rb;

    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Llamar a la función para mover el jugador
        MoverJugador();
    }

    void MoverJugador()
    {
        // Obtener las entradas del usuario (teclas de dirección o 'WASD')
        float movimientoX = Input.GetAxis("Horizontal"); // A/D o Flechas izquierda/derecha
        float movimientoY = Input.GetAxis("Vertical");   // W/S o Flechas arriba/abajo

        // Crear un vector de movimiento
        Vector2 movimiento = new Vector2(movimientoX, movimientoY) * velocidadMovimiento;

        // Aplicar el movimiento al Rigidbody2D
        rb.velocity = movimiento;

        // El Rigidbody2D manejará las colisiones automáticamente con el BoxCollider2D de los límites
    }
}
