using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Tipo de enemigo
    public EnemyType type;

    // Vida del enemigo
    public float maxLife = 3;
    private float life;

    // Velocidad y da�o
    public float speed = 2;
    public float damage;

    // Rango de visi�n
    public float range = 4;
    private bool targetInRange = false;

    // Puntos de ADN fuerte
    public int ADNstrongValue;

    // Variables para movimiento
    private Vector2 targetPosition;  // Posici�n hacia la cual se mover�
    private Transform target;  // Jugador
    private Rigidbody2D rb;  // Rigidbody del enemigo

    // Prefab ADN fuerte que se genera al morir
    public GameObject ADNStrong;

    // Configuraci�n inicial
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        // Inicializamos los atributos del enemigo seg�n el tipo
        ModifyAttributes(type);

        // Establecer la vida inicial
        life = maxLife;

        // Obtener el Rigidbody del enemigo
        rb = GetComponent<Rigidbody2D>();

        // Definir una posici�n objetivo inicial
        targetPosition = GetRandomTargetPosition();
    }

    // Actualizaci�n en cada frame
    void Update()
    {
        // Comportamiento seg�n el tipo de enemigo
        switch (type)
        {
            case EnemyType.EVO1:
            case EnemyType.EVO2:
            case EnemyType.EVO3:
            case EnemyType.EVO4:
                ModifyAttributes(type);
                if (targetInRange)
                {
                    // Si el jugador est� dentro del rango, perseguirlo
                    MoveTowardsPlayer();
                    RotateToTarget();  // Rotar hacia el jugador
                }
                else
                {
                    // Si no, moverse aleatoriamente
                    MoveToRandomPosition();
                    RotateMovement();  // Rotar hacia la direcci�n de movimiento aleatorio
                    SearchTarget();
                }
                break;

            case EnemyType.NEVO1:
            case EnemyType.NEVO2:
            case EnemyType.NEVO3:
                MoveToRandomPosition(); // Movimiento aleatorio
                RotateMovement();  // Rotar hacia el movimiento aleatorio
                break;
        }
    }

    // Ajustar atributos seg�n el tipo de enemigo
    public void ModifyAttributes(EnemyType type)
    {
        if (type == EnemyType.EVO1)
        {
            damage = 1;
            range = 0.5f;
            maxLife = 10;
            speed = 0.5f;
        }
        else if (type == EnemyType.EVO2)
        {
            damage = 3;
            range = 1;
            maxLife = 20;
            speed = 1;
        }
        else if (type == EnemyType.EVO3)
        {
            damage = 5;
            range = 2;
            maxLife = 40;
            speed = 2;
        }
        else
        {
            damage = 10;
            range = 3;
            maxLife = 80;
            speed = 3;
        }
    }

    // Movimiento aleatorio dentro de un rango
    void MoveToRandomPosition()
    {
        // Si el enemigo ha llegado a la posici�n objetivo, establecer una nueva posici�n aleatoria
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            targetPosition = GetRandomTargetPosition();
        }

        // Mover hacia la posici�n objetivo
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;  // Usamos la velocidad del Rigidbody para movimiento m�s fluido
    }

    // Obtener una posici�n aleatoria dentro de un rango determinado
    Vector2 GetRandomTargetPosition()
    {
        float randomX = Random.Range(-5f, 5f);  // Ajusta el rango de X seg�n lo que necesites
        float randomY = Random.Range(-3f, 3f);  // Ajusta el rango de Y seg�n lo que necesites
        return new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    // Movimiento hacia el jugador cuando est� dentro del rango
    void MoveTowardsPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;  // Mover al jugador con la velocidad del Rigidbody
    }

    // Buscar al jugador dentro del rango
    void SearchTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= range)
        {
            targetInRange = true;
        }
        else
        {
            targetInRange = false;
        }
    }

    // Colisiones con el jugador
    void OnCollisionEnter2D(Collision2D collision)
{

     // Ignorar colisiones con otros enemigos
     if (collision.gameObject.CompareTag("Enemy"))
     {
         Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
     }
     // Ignorar colisiones con Food
     else if (collision.gameObject.CompareTag("Food"))
     {
         Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
     }
     // Ignorar colisiones con Organismo
     else if (collision.gameObject.CompareTag("Organismo"))
     {
         Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
     }
     // Si colisiona con el jugador
     else if (collision.gameObject.CompareTag("Player"))
     {
         Player p = collision.gameObject.GetComponent<Player>();
         if (p != null)
         {
             Debug.Log($"El enemigo de tipo {type} ha infligido {damage} puntos de da�o al jugador.");
             p.TakeDamage(damage);
         }
     }
}

    // Al recibir da�o
    public void TakeDamage(float dmg)
    {
        life -= dmg;

        if (life <= 0)
        {
            // Generar ADN fuerte al morir
            Instantiate(ADNStrong, transform.position, transform.rotation);

            ADNstrongValue = type switch
            {
                EnemyType.EVO1 => 3,
                EnemyType.EVO2 => 6,
                EnemyType.EVO3 => 9,
                _ => 20,
            };

            PuntuacionManager.intance.ActualizarADNfuerte(ADNstrongValue);

            Destroy(gameObject);
        }
    }

    // Rotaci�n hacia el jugador
    void RotateToTarget()
    {
        // Calcular la direcci�n hacia el jugador
        Vector2 dir = target.position - transform.position;

        // Calcular el �ngulo de rotaci�n (en grados) hacia el jugador
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Aplicar la rotaci�n al enemigo
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Rotaci�n hacia el movimiento del enemigo
    void RotateMovement()
    {
        // Calcular la direcci�n de movimiento (movimiento aleatorio)
        Vector2 dir = rb.velocity;

        if (dir.sqrMagnitude > 0.01f) // Evitar rotaci�n cuando no hay movimiento
        {
            // Calcular el �ngulo de rotaci�n basado en la direcci�n del movimiento
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Aplicar la rotaci�n al enemigo
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }



}
public enum EnemyType
{
    EVO1,
    EVO2,
    EVO3,
    EVO4,
    NEVO1,
    NEVO2,
    NEVO3
}
