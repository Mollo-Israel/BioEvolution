using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public TiendaSkills tienda;

    public float impactForce = 5f; // Fuerza del impacto
    public float impactDuration = 0.5f; // Duraci�n del impacto en segundos
    private Vector2 movement;
    private bool isImpacted = false;

    public float velocidadMovimiento = 5f;
    public int nivelMejorVelocidad = 0;

    // Referencia al Rigidbody2D
    private Rigidbody2D rb;

    // Referencia al texto de puntuaci�n
    public Text textoPuntuacion;

    // Referencia a las barras de vida y comida
    public Image barraVida;
    public Image barraComida;

    // Texto barra de vida
    public Text textoVida;

    // Texto comida
    public Text textoComida;

    // Valores de vida y comida
    private float vidaMaxima = 25f;
    private float vidaActual = 25f;
    private float comidaMaxima = 10f;
    private float comidaActual = 5f;

    // Puntos de ADN
    public int puntos = 0;

    // L�mites del �rea donde el jugador puede moverse (en el eje X y Y)
    public float limiteIzquierdo = -24f;
    public float limiteDerecho = 24f;
    public float limiteInferior = -20f;
    public float limiteSuperior = 20f;


    public Text textoCostoMejora;
    public Text textoPuntos;
    // Costo de la mejora
    private int costoMejoraVelocidad = 5;
    private int costodefensa = 8;

    // Temporizador para descontar vida
    private float timerComida = 0f;
    private float tiempoRestarVida = 10f; // Tiempo en segundos para restar vida cada vez que la comida est� a 0

    public float tiempoParaPerderComida = 5f;  // Por ejemplo, cada 5 segundos perderemos 1 de comida

    private float timerComidaGradual = 0f;  // Temporizador para reducir la comida


    private bool isPredatorModeActive = false;
    private float predatorModeTimer = 0f;
    private float velocidadPredatorMode = 10f;  // Incremento de velocidad por PredatorMode
    private float vidaOriginal;
    private float danoOriginal;

    // PowerUp 2x
    private bool is2xActive = false;
    private float twoXTimer = 0f;
    private float comidaMultiplicador = 1f;  // Multiplicador de comida
    private float adnMultiplicador = 1f;  // Multiplicador de ADN

    void Start()
    {
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();

        // Asignar vida al player
        vidaActual = vidaMaxima;

        // Inicializar Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Inicializar valores de vida y comida
        // Actualizar UI inicial
        ActualizarBarras();
        ActualizarTextoPuntuacion();

    }

    void Update()
    {
        barraComida.fillAmount = comidaActual / comidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();

        // Solo llamamos a la funci�n para mover el jugador, sin modificar rb.velocity aqu�.
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Llamar a la funci�n para descontar comida
        DescontarComida();

        // Temporizador para descontar vida si comida est� en 0
        if (comidaActual == 0)
        {
            timerComida += Time.deltaTime;

            // Si han pasado 10 segundos, restar vida
            if (timerComida >= tiempoRestarVida)
            {
                RestarVida(1f); // Resta 10 de vida
                timerComida = 0f; // Reinicia el temporizador
            }
        }
    }

    void DescontarComida()
    {
        // Si la comida es mayor que 0, se sigue descontando gradualmente
        if (comidaActual > 0)
        {
            timerComidaGradual += Time.deltaTime;

            // Si han pasado los segundos necesarios para reducir la comida
            if (timerComidaGradual >= tiempoParaPerderComida)
            {
                // Reducir la comida gradualmente
                comidaActual -= 1; // Resta 1 de comida cada vez
                timerComidaGradual = 0f; // Reinicia el temporizador

                // Actualiza el texto y la barra de comida
                textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString();
                barraComida.fillAmount = comidaActual / comidaMaxima;
            }
        }

        // Si la comida es 0, restamos vida
        if (comidaActual <= 0)
        {
            comidaActual = 0; // Asegura que la comida no baje de 0
            textoComida.text = comidaActual.ToString() + "/" + comidaMaxima.ToString(); // Actualiza el texto de la comida
            barraComida.fillAmount = comidaActual / comidaMaxima; // Actualiza la barra de comida

            // Temporizador para restar vida gradualmente cuando no hay comida
            timerComida += Time.deltaTime;

            if (timerComida >= tiempoRestarVida) // Si el temporizador excede el tiempo para perder vida
            {
                RestarVida(1f); // Resta 1 de vida cada ciclo
                timerComida = 0f; // Reinicia el temporizador
            }

            // Si la vida llega a 0, el jugador muere
            if (vidaActual <= 0)
            {
                Destroy(gameObject); // Destruye el jugador
                SceneManager.LoadScene("Start_Game");
            }
        }
    }


    void MoverJugador()
    {
        // Obtener las entradas del usuario (teclas de direcci�n o 'WASD')
        float movimientoX = Input.GetAxis("Horizontal"); // A/D o Flechas izquierda/derecha
        float movimientoY = Input.GetAxis("Vertical");   // W/S o Flechas arriba/abajo

        // Crear un vector de movimiento
        movement = new Vector2(movimientoX, movimientoY) * velocidadMovimiento;

        // Aplicar el movimiento al Rigidbody2D
        rb.velocity = movement;

        // Limitar la posici�n del jugador dentro de los l�mites definidos
        Vector2 posicionJugador = rb.position;
        posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteIzquierdo, limiteDerecho);
        posicionJugador.y = Mathf.Clamp(posicionJugador.y, limiteInferior, limiteSuperior);

        // Actualizar la posici�n del jugador para que no sobrepase los l�mites
        rb.position = posicionJugador;

        // Rotaci�n del jugador seg�n la direcci�n del movimiento
        if (movimientoX != 0 || movimientoY != 0)
        {
            // Calcular el �ngulo de rotaci�n basado en la direcci�n del movimiento
            float angulo = Mathf.Atan2(movimientoY, movimientoX) * Mathf.Rad2Deg;  // Convertir de radianes a grados

            // Aplicar la rotaci�n al jugador (esto hace que el jugador rote hacia la direcci�n en la que se mueve)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        }

        // Si no hay movimiento, el jugador mantiene la rotaci�n predeterminada (como se hac�a antes)
        else
        {
            // Aseguramos que el jugador no gire si no hay movimiento
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            // Calcular la direcci�n del jugador al enemigo
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Aplica una fuerza de retroceso al jugador en la direcci�n opuesta
            rb.AddForce(pushDirection * impactForce, ForceMode2D.Impulse);

            // Inicia la rutina para restablecer la velocidad
            StartCoroutine(ResetImpact());
        }
        // Si colisiona con un organismo
        if (collision.gameObject.CompareTag("Organismo"))
        {
            puntos += 1; // Aumenta los puntos de ADN
            ActualizarTextoPuntuacion(); // Actualiza el texto en pantalla
            Destroy(collision.gameObject); // Destruye el organismo
            //tienda.ActualizarTextoPuntos();
        }

        // Si colisiona con comida
        if (collision.gameObject.CompareTag("Food"))
        {
            comidaActual += 1;
            Destroy(collision.gameObject); // Destruye la comida
        }

    }
    void FixedUpdate()
    {
        if (!isImpacted)
        {
            // Mover el jugador usando velocity directamente
            rb.velocity = movement * velocidadMovimiento;

            // Limitar la velocidad m�xima
            if (rb.velocity.magnitude > 10.0f)
            {
                rb.velocity = rb.velocity.normalized * 10.0f;
            }

            // Rotar al jugador seg�n la direcci�n del movimiento
            if (movement.magnitude > 0)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                transform.rotation = Quaternion.identity; // Mantener la rotaci�n predeterminada si no se mueve
            }

            Vector2 posicionJugador = rb.position;
            posicionJugador.x = Mathf.Clamp(posicionJugador.x, limiteIzquierdo, limiteDerecho);
            posicionJugador.y = Mathf.Clamp(posicionJugador.y, limiteInferior, limiteSuperior);

            // Actualizar la posici�n del jugador despu�s de la restricci�n
            rb.position = posicionJugador;
        }
    }
    private IEnumerator ResetImpact()
    {
        isImpacted = true;
        yield return new WaitForSeconds(impactDuration);
        isImpacted = false;
    }

    public void TakeDamage(float dmg)
    {
        vidaActual -= dmg;
        barraVida.fillAmount = vidaActual / vidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
        if (vidaActual <= 0)
        {
            SceneManager.LoadScene("Start_Game");
            // Destroy(gameObject);
        }
    }

    void RestarVida(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }
        barraVida.fillAmount = vidaActual / vidaMaxima;
        textoVida.text = vidaActual.ToString() + "/" + vidaMaxima.ToString();
    }

    void ActualizarTextoPuntuacion()
    {
        textoPuntuacion.text = "X" + puntos.ToString();
    }

    void ActualizarBarras()
    {
        // Actualiza las barras de vida y comida
        if (barraVida != null)
            barraVida.fillAmount = vidaActual / vidaMaxima;

        if (barraComida != null)
            barraComida.fillAmount = comidaActual / comidaMaxima;
    }
    public void ComprarMejoraVelocidad()
    {
        if (puntos >= costoMejoraVelocidad)
        {
            puntos -= costoMejoraVelocidad;
            nivelMejorVelocidad++;
            velocidadMovimiento += 1f; // Incrementa la velocidad de movimiento
            ActualizarTextoPuntuacion();
            tienda.ActualizarTextoPuntos(); // Actualiza los puntos despu�s de la compra
            // Metodo o if para el limite de nivel de la habilidad (speed)
        }
    }

}
