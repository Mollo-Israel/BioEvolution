using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float maxSpeed = 10f; // Velocidad máxima
    public float impactForce = 0.8f; // Fuerza del impacto
    public float impactDuration = 0.5f; // Duración del impacto en segundos

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isImpacted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Obtener la entrada del jugador
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (!isImpacted)
        {
            // Mover el jugador usando velocity directamente
            rb.velocity = movement * moveSpeed;

            // Limitar la velocidad máxima
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Calcular la dirección del jugador al enemigo
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Aplica una fuerza de retroceso al jugador en la dirección opuesta
            rb.AddForce(pushDirection * impactForce, ForceMode2D.Impulse);

            // Inicia la rutina para restablecer la velocidad
            StartCoroutine(ResetImpact());
        }
    }

    private IEnumerator ResetImpact()
    {
        isImpacted = true;
        yield return new WaitForSeconds(impactDuration);
        isImpacted = false;
        rb.velocity = Vector2.zero; // Restablece la velocidad del jugador
    }
}
