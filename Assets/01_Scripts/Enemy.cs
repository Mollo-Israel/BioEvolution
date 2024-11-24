using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //tipo de enemigo
    public EnemyType type;

    //vida max de enemigo
    public float maxLife = 3;

    //vida actual esta va perdiendo o dismuniyendo
    float life = 3;

    //velocidad max de enemigo
    public float speed = 2;

    //dano que realiza el enemigo
    public float damage;

    //rango de busqueda del enemigo
    public float range = 4;

    //saber si se encuentra en el rango el player
    bool targetInRange = false;

    //puntos de score que vale cada enemigo
    public int ADNstrongValue;



    // Variables para controlar el movimiento
    private Vector2 direction = Vector2.right; // Dirección inicial (hacia la derecha)
    private float moveRangeX = 5f; // Rango de movimiento en el eje X (distancia izquierda-derecha)
    private float moveStepY = 1f; // Distancia que sube en Y al finalizar un ciclo
    private Vector2 startPosition; // Posición inicial para calcular el rango de movimiento



    //saber PLAYER position
    Transform target;

    //prefab objeto que se genera del adn fuerte
    public GameObject ADNStrong;

    ////efecto de muerte
    //public GameObject explosionEffect;

    ////barra de vida o puntos de vida visuales
    //public Image lifeBar;

    // Start is called before the first frame update
    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        // Configurar los atributos según el tipo de enemigo al inicio
        ModifyAttributes(type);

        // Guardar la posición inicial del enemigo
        startPosition = transform.position;

        // Configurar la dirección inicial
        direction = Vector2.right; // Empieza moviéndose hacia la derecha
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case EnemyType.EVO1:

                ModifyAttributes(type);
                if (targetInRange)
                {
                    RotateToTarget();
                    MoveForward();
                }
                else
                {
                    //Movimiento cuando el player no esta en rango
                    MoveIdle();
                    SearchTarget();
                }

                break;

            case EnemyType.EVO2 :
                ModifyAttributes(type);
                //si encuentra en rango al player
                if (targetInRange)
                {

                    RotateToTarget();
                    MoveForward();
                }
                else
                {
                    //Movimiento cuando el player no esta en rango
                    MoveIdle();
                    SearchTarget();
                }

                break;
            case EnemyType.EVO3:

                ModifyAttributes(type);   
                //si encuentra en rango al player
                if (targetInRange)
                {
                    RotateToTarget();
                    MoveForward();
                }
                else
                {
                    //Movimiento cuando el player no esta en rango
                    MoveIdle();
                    SearchTarget();
                }
                break;
            case EnemyType.EVO4:
                //si encuentra en rango al player
                if (targetInRange)
                {
                    RotateToTarget();
                    MoveForward();
                }
                else
                {
                    MoveIdle();
                    SearchTarget();
                }
                break;
            case EnemyType.NEVO1:
                //movimiento de neutral
                MoveIdle();

                break;
            case EnemyType.NEVO2:
                //movimiento de neutral
                MoveIdle();

                break;
            case EnemyType.NEVO3:
                //movimiento de neutral
                MoveIdle();

                break;
        }
    }
    //aumento de atributos segun el typo de enemigo
    public void ModifyAttributes(EnemyType type)
    {
        if(type == EnemyType.EVO1)
        {
            damage = 1;
            range = 0.5f;
            maxLife = 10;
            speed = 1;
        }
        else
        {
            if(type == EnemyType.EVO2)
            {
                damage = 3;
                range = 1;
                maxLife = 20;
                speed = 2;
            }
            else
            {
                if (type == EnemyType.EVO3)
                {
                    damage = 5;
                    range = 2;
                    maxLife = 40;
                    speed = 3;
                }
                else
                {
                    damage = 10;
                    range = 3;
                    maxLife = 80;
                    speed = 8;
                }
            }
        } 
    }
    public void MoveIdle()
    {
        // Verificar si se alcanzó el límite en X
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRangeX)
        {
            // Cambiar dirección en X (zigzag)
            direction.x *= -1;
        }

        // Verificar si el enemigo está subiendo y llegó al límite superior
        if (direction.y > 0 && transform.position.y >= Camera.main.orthographicSize)
        {
            // Cambiar dirección hacia abajo en el eje Y
            direction.y = -1;
        }

        // Verificar si el enemigo está bajando y llegó al límite inferior
        if (direction.y < 0 && transform.position.y <= -Camera.main.orthographicSize)
        {
            // Cambiar dirección hacia arriba en el eje Y
            direction.y = 1;
        }

        // Mover el enemigo en la dirección actual
        transform.Translate(direction * speed * Time.deltaTime);
    }


    //recibir dano
    public void TakeDamage(float dmg)
    {
        //descontar vida segun el dano que entra
        life -= dmg;

        ////barra de vida actulizar barra de vida visual 
        //lifeBar.fillAmount = life / maxLife;
        //al morir, si la vida actual es igual a 0 o menor
        if (life <= 0)
        {
            //generar adn fuerte al morir el enemigo segun el enemigo que se elimino
            if (type == EnemyType.EVO1)
            {
                //instanciar adnfuerte
                Instantiate(ADNStrong, transform.position, transform.rotation);

                //el adn fuerte que da es de 3
                ADNstrongValue = 3;
                PuntuacionManager.intance.ActualizarADNfuerte(ADNstrongValue);
            }
            else
            {
                if(type == EnemyType.EVO2)
                {
                    //instanciar adnfuerte
                    Instantiate(ADNStrong, transform.position, transform.rotation);

                    //el adn fuerte que da es de 6
                    ADNstrongValue = 6;
                    PuntuacionManager.intance.ActualizarADNfuerte(ADNstrongValue);
                }
                else
                {
                    if(type == EnemyType.EVO3)
                    {
                        //instanciar adnfuerte
                        Instantiate(ADNStrong, transform.position, transform.rotation);

                        //el adn fuerte que da es de 9
                        ADNstrongValue = 9;
                        PuntuacionManager.intance.ActualizarADNfuerte(ADNstrongValue);
                    }
                    else
                    {
                        //instanciar adnfuerte
                        Instantiate(ADNStrong, transform.position, transform.rotation);

                        //el adn fuerte que da es de 20
                        ADNstrongValue = 20;
                        PuntuacionManager.intance.ActualizarADNfuerte(ADNstrongValue);
                    }
                }
            }
            ////el enemigo se destruye
            //Instantiate(explosionEffect, transform.position, transform.rotation);

            //destruir el objeto
            Destroy(gameObject);
        }
    }

    //rotar en angulo del player
    void RotateToTarget()
    {
        Vector2 dir = target.position - transform.position;
        float angleZ = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + 0;
        transform.rotation = Quaternion.Euler(0, 0, -angleZ);
    }
    //mover hacia el player
    void MoveForward()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //buscar al player
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
    //al recibir un impacto
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el enemigo colisiona con la pared limitante izquierda (LimitX-)
        if (collision.gameObject.CompareTag("LimitX-"))
        {
            // Cambiar dirección hacia la derecha
            direction = Vector2.right;
        }
        // Si el enemigo colisiona con la pared limitante derecha (LimitX+)
        else if (collision.gameObject.CompareTag("LimitX+"))
        {
            // Cambiar dirección hacia la izquierda
            direction = Vector2.left;
        }
        // Si el enemigo colisiona con el límite superior (LimitY+)
        else if (collision.gameObject.CompareTag("LimitY+"))
        {
            // Cambiar dirección hacia abajo en el eje Y
            direction.y = -1;
        }
        // Si el enemigo colisiona con el límite inferior (LimitY-)
        else if (collision.gameObject.CompareTag("LimitY-"))
        {
            // Cambiar dirección hacia arriba en el eje Y
            direction.y = 1;
        }
        // Ignorar colisiones con otros enemigos
        else if (collision.gameObject.CompareTag("Enemy"))
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
                Debug.Log($"El enemigo de tipo {type} ha infligido {damage} puntos de daño al jugador.");
                p.TakeDamage(damage);
            }
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
